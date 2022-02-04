using System;
using System.ServiceModel;
using Contract;
using Business;
using CatalogosSAT;

namespace GAFWEB
{
    public partial class wfrSucursales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //var cliente = NtLinkClientFactory.Cliente();
                var Sucursal = new GAFSucursales();
              
                string idSucursalString = this.Request.QueryString["idSucursal"];
                int idSucursal;
                if (!string.IsNullOrEmpty(idSucursalString) && int.TryParse(idSucursalString, out idSucursal) && this.Request.QueryString["idEmpresa"] != null)
                {
                    using (Sucursal as IDisposable)
                    {
                        Sucursales sucursal = Sucursal.GetSucursal(idSucursal);
                       

                        this.txtNombre.Text = sucursal.Nombre;
                        this.txtDomicilio.Text = sucursal.Direccion;
                        ViewState["sucursal"] = sucursal;
                        //-----------------------------
                        OperacionesCatalogos OP=new OperacionesCatalogos();
                        using (OP as IDisposable)
                         {
                          ddlEstado.DataSource = OP.Consultar_EstadosALL();
                          ddlEstado.DataTextField = "NombredelEstado";
                          ddlEstado.DataValueField = "c_Estado1";
                          ddlEstado.DataBind();
                         }
                        if (!string.IsNullOrEmpty(sucursal.Estado))
                            this.ddlEstado.SelectedValue = sucursal.Estado;
                        using (OP as IDisposable)
                        {
                            ddlMunicipio.DataSource = OP.Consultar_MunicipioALL(ddlEstado.SelectedValue);
                            ddlMunicipio.DataTextField = "Descripción";
                            ddlMunicipio.DataValueField = "c_Municipio1";
                            ddlMunicipio.DataBind();
                        }
                        if (!string.IsNullOrEmpty(sucursal.Municipio))
                            this.ddlMunicipio.SelectedValue = sucursal.Municipio;
                        using (OP as IDisposable)
                        {
                            ddlCP.DataSource = OP.Consultar_CPALL(ddlEstado.SelectedValue, ddlMunicipio.SelectedValue);
                            ddlCP.DataTextField = "c_CP1";
                            ddlCP.DataValueField = "c_CP1";
                            ddlCP.DataBind();
                        }
                        if (!string.IsNullOrEmpty(sucursal.LugarExpedicion))
                            this.ddlCP.SelectedValue = sucursal.LugarExpedicion;
                        //-------------------------------

                    }
                }
                else
                {
                    OperacionesCatalogos OP = new OperacionesCatalogos();
                     
                    using (OP as IDisposable)
                    {
                        ddlEstado.DataSource = OP.Consultar_EstadosALL();
                        ddlEstado.DataTextField = "NombredelEstado";
                        ddlEstado.DataValueField = "c_Estado1";
                        ddlEstado.DataBind();
                    
                        ddlMunicipio.DataSource = OP.Consultar_MunicipioALL(ddlEstado.SelectedValue);
                        ddlMunicipio.DataTextField = "Descripción";
                        ddlMunicipio.DataValueField = "c_Municipio1";
                        ddlMunicipio.DataBind();
                          ddlCP.DataSource = OP.Consultar_CPALL(ddlEstado.SelectedValue, ddlMunicipio.SelectedValue);
                        ddlCP.DataTextField = "c_CP1";
                        ddlCP.DataValueField = "c_CP1";
                        ddlCP.DataBind();
                      }
                }
                txtIdEmpresa.Value = this.Request.QueryString["idEmpresa"];
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var sucursal = ViewState["sucursal"] as Sucursales ?? new Sucursales
                                                                      {
                                                                          IdEmpresa = int.Parse(txtIdEmpresa.Value)
                                                                      };
            sucursal.Nombre = this.txtNombre.Text;
            sucursal.LugarExpedicion = this.ddlCP.SelectedValue;
            sucursal.Estado = this.ddlEstado.SelectedValue;
            sucursal.Municipio = this.ddlMunicipio.SelectedValue;
            sucursal.Direccion = this.txtDomicilio.Text;
            
           //  var cliente = NtLinkClientFactory.Cliente();
            var Sucursal = new GAFSucursales();

            using (Sucursal as IDisposable)
             {
                 try
                 {
                     if (Sucursal.SaveSucursal(sucursal))
                     {
                         this.Response.Redirect("wfrSucursalesConsulta.aspx?idEmpresa="  + txtIdEmpresa.Value);
                     }
                     else
                     {
                         this.lblError.Text = "No se puedo guardar la sucursal";
                     }
                 }
                 catch (FaultException ex)
                 {
                     this.lblError.Text = ex.Message;
                 }
             }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("wfrSucursalesConsulta.aspx?idEmpresa=" + txtIdEmpresa.Value);
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperacionesCatalogos OP = new OperacionesCatalogos();
            using (OP as IDisposable)
             {

                 ddlMunicipio.DataSource = OP.Consultar_MunicipioALL(ddlEstado.SelectedValue);
                 ddlMunicipio.DataTextField = "Descripción";
                 ddlMunicipio.DataValueField = "c_Municipio1";
                 ddlMunicipio.DataBind();
                 ddlCP.DataSource = OP.Consultar_CPALL(ddlEstado.SelectedValue, ddlMunicipio.SelectedValue);
                 ddlCP.DataTextField = "c_CP1";
                 ddlCP.DataValueField = "c_CP1";
                 ddlCP.DataBind();
             }

        }

        protected void ddlMunicipio_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperacionesCatalogos OP = new OperacionesCatalogos();
            using (OP as IDisposable)
            {

                ddlCP.DataSource = OP.Consultar_CPALL(ddlEstado.SelectedValue, ddlMunicipio.SelectedValue);
                ddlCP.DataTextField = "c_CP1";
                ddlCP.DataValueField = "c_CP1";
                ddlCP.DataBind();
            }

        }

        protected void ddlCP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}