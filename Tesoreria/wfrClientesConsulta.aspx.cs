using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Contract;

namespace GAFWEB
{
    public partial class wfrClientesConsulta : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                
                this.GetClientes();
            }
        }

        protected void gvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName.Equals("EditarCliente"))
            {
                DataKey key = this.gvClientes.DataKeys[Convert.ToInt32(e.CommandArgument)];
                if (key != null)
                {
                    int idClientePromotor = Convert.ToInt32(key.Value);
                    Response.Redirect("wfrClientes.aspx?idClientePromotor=" + idClientePromotor);
                }
            }
            else if (e.CommandName.Equals("Eliminar"))
            {
                
                try
                {
                    var cliente = new GAFClientes();
                    using (cliente as IDisposable)
                    {
                        int idCliente = Convert.ToInt32(e.CommandArgument);
                        var cl = cliente.GetCliente(idCliente);
                        cliente.EliminarCliente(cl);
                        
                    }
                    this.GetClientes();
                }
                catch (FaultException fe)
                {
                    lblError.Text = fe.Message;
                }
                catch(Exception ee)
                {
                    lblError.Text = "Ocurrió un error al eliminar el cliente";
                   
                }


            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            this.GetClientes();
        }

        protected void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("wfrClientes.aspx?idCliente=0");
        }

        protected void gvClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvClientes.DataSource = ViewState["clientes"];
            this.gvClientes.PageIndex = e.NewPageIndex;
            this.gvClientes.DataBind();
        }

        #region Helper Methods

        private void GetClientes()
        {
           
           
            var cliente =new GAFClientes();
            using (cliente as IDisposable)
            {
                var clientes = cliente.GetClientePromotorAll( txtBusqueda.Text);
                ViewState["clientes"] = clientes;
                this.gvClientes.DataSource = clientes;
                this.gvClientes.DataBind();
            }
        }

        #endregion

       

      
    }
}