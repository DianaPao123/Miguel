using System;
using System.Web.UI.WebControls;
using Contract;
using Business;

namespace GAFWEB
{
    public partial class wfrSucursalesConsulta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var perfil = Session["perfil"] as string;
            var sistema = Session["idSistema"] as long?;
            if(!this.IsPostBack)
            {
              //  var cliente = NtLinkClientFactory.Cliente();
                var Empresa = new GAFEmpresa();
                var Sucursal = new GAFSucursales();
                var idEmpresa = int.Parse(Request.QueryString["idEmpresa"]);
                   
                using (Empresa as IDisposable)
                {
                    hdIdempresa.Value = idEmpresa.ToString();
                    var emp = Empresa.GetById(idEmpresa);
                    this.lblEmpresa.Text = emp.RazonSocial;
                  
                }
                using (Sucursal as IDisposable)
                {
                    this.gvSucursales.DataSource = Sucursal.GetSucursalLista(idEmpresa);
                    ViewState["sucursales"] = this.gvSucursales.DataSource;
                    this.gvSucursales.DataBind();
                }
            }
        }

        protected void gvSucursales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var idSucursal = (long)this.gvSucursales.DataKeys[Convert.ToInt32(e.CommandArgument)].Value;
            if(e.CommandName.Equals("EditarSucursal"))
            {
                this.Response.Redirect("wfrSucursales.aspx?idSucursal=" + idSucursal + "&idEmpresa="+ hdIdempresa.Value);
            }
        }

        protected void btnNuevaSucursal_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("wfrSucursales.aspx?idEmpresa=" + hdIdempresa.Value);
        }

        protected void gvSucursales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSucursales.DataSource = ViewState["sucursales"];
            this.gvSucursales.PageIndex = e.NewPageIndex;
            this.gvSucursales.DataBind();
        }
    }
}