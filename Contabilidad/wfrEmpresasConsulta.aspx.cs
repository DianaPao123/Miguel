using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using Business;

namespace GAFWEB
{
    public partial class wfrEmpresasConsulta : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                this.FillView();
            }
        }

        protected void btnNuevaEmpresa_Click(object sender, EventArgs e)
        {
            Response.Redirect("wfrEmpresa.aspx");
        }

        protected void gvEmpresas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("EditarEmpresa"))
            {
                DataKey key = this.gvEmpresas.DataKeys[Convert.ToInt32(e.CommandArgument)];
                if (key != null)
                {
                    int idCliente = Convert.ToInt32(key.Value);
                    Response.Redirect("wfrEmpresa.aspx?idEmpresa=" + idCliente);
                }
            }
            if (e.CommandName.Equals("EditarSucursal"))
            {
                DataKey key = this.gvEmpresas.DataKeys[Convert.ToInt32(e.CommandArgument)];
                if (key != null)
                {
                    int idCliente = Convert.ToInt32(key.Value);
                    Response.Redirect("wfrSucursalesConsulta.aspx?idEmpresa=" + idCliente);
                }
            }
            if (e.CommandName.Equals("EditarConceptos"))
            {
                DataKey key = this.gvEmpresas.DataKeys[Convert.ToInt32(e.CommandArgument)];
                if (key != null)
                {
                    int idCliente = Convert.ToInt32(key.Value);
                    Response.Redirect("wfrConceptos.aspx?idEmpresa=" + idCliente);
                }
            }
        }
        protected void gvEmpresas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmpresas.DataSource = ViewState["empresas"];
            this.gvEmpresas.PageIndex = e.NewPageIndex;
            this.gvEmpresas.DataBind();
        }

        #region HelperMethods

        private void FillView()
        {
             var empresa = new GAFEmpresa();
           
            using( empresa as IDisposable)
            {
                gvEmpresas.DataSource = empresa.GetListForLine(ddlLinea.SelectedValue);
                // this.gvEmpresas.DataSource = cliente.ListaEmpresas(Session["perfil"] as string, idEmpresa.Value, sistema.Value, null);
                ViewState["empresas"] = this.gvEmpresas.DataSource;
                this.gvEmpresas.DataBind();
            
            }
           
            /*
            try{
            //var sistema = Session["idSistema"] as long?;
           // var idEmpresa = Session["idEmpresa"] as int?;
              gvEmpresas.DataSource = empresa.GetListForLine(ddlLinea.SelectedValue);
               // this.gvEmpresas.DataSource = cliente.ListaEmpresas(Session["perfil"] as string, idEmpresa.Value, sistema.Value, null);
                ViewState["empresas"] = this.gvEmpresas.DataSource;
                this.gvEmpresas.DataBind();
             }
            finally {
                GC.Collect();
            }
             */ 
        }

        #endregion

        protected void ddlLinea_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillView();
       
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}