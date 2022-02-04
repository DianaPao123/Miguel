using System;
using System.ServiceModel;
using Contract;
using Business;
using System.Text.RegularExpressions;

namespace GAFWEB
{
    public partial class wfrClientesNuevos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {


                string idClienteString = Request.QueryString["idClientePromotor"];

                int idCliente;
                if (!string.IsNullOrEmpty(idClienteString) && int.TryParse(idClienteString, out idCliente))
                {

                    if (idCliente != 0)
                    {
                        // var sesion = Session["sessionRGV"] as Sesion;

                        var Cliente = new GAFClientes();
                        clientes cliente;
                        using (Cliente as IDisposable)
                        {

                            cliente = Cliente.GetCliente((int)idCliente);
                            ViewState["cliente"] = cliente;
                        }

                        this.FillView(cliente);
                        // clienteP = Cliente.GetClientePromotorRelacion(cliente.idCliente, sesion.Id);
                    }


                }
               
               
                 
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            var cliente = ViewState["cliente"] as clientes;
            if (cliente != null)
            {
                clientes modCliente = this.GetClientFromView();

                modCliente.idCliente = cliente.idCliente;
                modCliente.idVendedor = cliente.idVendedor;
                modCliente.Tipo = cliente.Tipo;
                // modCliente.CURP = txtCurp.Text;//agregando curp

                try
                {
                    var client = new GAFClientes();

                    using (client as IDisposable)
                    {
                        client.SaveCliente(modCliente);
                    }
                    Response.Redirect("wfrClientesConsulta.aspx");
                }
                catch (FaultException ex)
                {
                    this.lblError.Text = ex.Message;
                }

            }
            else
            {
                try
                {
                    var client = new GAFClientes();
                    using (client as IDisposable)
                    {
                        client.SaveCliente(this.GetClientFromView());
                    }
                    Response.Redirect("wfrClientesConsulta.aspx");
                }
                catch (FaultException ex)
                {
                    this.lblError.Text = ex.Message;
                }
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
          //  Response.Redirect("wfrClientesConsulta.aspx");
            Response.Redirect("Default.aspx");
        }

        #region HelperMethods
        //----------------------------------------------------


        //---------------------------------------------------------------
        private void FillView(clientes cliente)
        {
           // this.ddlEmpresa.SelectedValue = cliente.idempresa.ToString();
            this.txtRFC.Text = cliente.RFC;
            this.txtRazonSocial.Text = cliente.RazonSocial;
            this.txtDireccion.Text = cliente.Direccion;
            this.txtColonia.Text = cliente.Colonia;
            this.txtMunicipio.Text = cliente.Ciudad;
            this.txtEstado.Text = cliente.Estado;
            this.txtPais.Text = cliente.Pais;
            this.txtCP.Text = cliente.CP;
            //this.txtTelefono.Text = cliente.Telefonos;
            this.txtEmail.Text = cliente.Email;
          //  this.txtWeb.Text = cliente.Pagina;
            //this.txtContacto.Text = cliente.Contacto;
         //   this.txtDiasRevision.Text = cliente.DiasRevision.ToString();
         //   this.txtCuentaContable.Text = cliente.Cuenta;
            this.txtBcc.Text = cliente.Bcc;
          //  this.txtInt.Text = cliente.NoInt;
          //  this.txtExt.Text = cliente.NoExt;
          //  this.txtLocalidad.Text = cliente.Localidad;
          //  this.txtReferencia.Text = cliente.Referencia;
           // this.txtCuentaDeposito.Text = cliente.CuentaPago;
         //   this.txtCurp.Text = cliente.CURP;
         //   this.ddlNacionalidad.SelectedValue = cliente.Nacionalidad;//para constacia de retenciones
         //   this.txtNumRegIdTrib.Text = cliente.NumRegIdTrib;//para constacia de retenciones
        }

        private clientes GetClientFromView()
        {
          
            var cliente = new clientes
                              {
                                  RFC = this.txtRFC.Text,
                                  RazonSocial = this.txtRazonSocial.Text,
                                  Direccion = this.txtDireccion.Text,
                                  Colonia = this.txtColonia.Text,
                                  Ciudad = this.txtMunicipio.Text,
                                  Estado = this.txtEstado.Text,
                                  Pais = this.txtPais.Text,
                                  CP = this.txtCP.Text,
                                 // Telefonos = this.txtTelefono.Text,
                                  Email = this.txtEmail.Text,
                               //   Pagina = this.txtWeb.Text,
                                //  Contacto = this.txtContacto.Text,
                               //   Cuenta = this.txtCuentaContable.Text,
                                   Bcc = txtBcc.Text,
                                   Linea=ddlLinea.SelectedValue
                                  };

            
            cliente.NoExt = string.IsNullOrEmpty(txtExt.Text) ? null : txtExt.Text;
            cliente.NoInt = string.IsNullOrEmpty(txtInt.Text) ? null : txtInt.Text;
               
            return cliente;
        }

        #endregion

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            this.lblError.Text = "";
            this.lblMensaje.Text = "";

            Regex reg = new Regex("^[A-Z,Ñ,&amp;]{3,4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]{2}[0-9,A]$");
            if (!reg.IsMatch(txtRFC.Text))
            {
                this.lblError.Text = "El RFC es inválido";
            }
            else
            {
                var sesion = Session["sessionRGV"] as Sesion;

                var Cliente = new GAFClientes();
                clientes cliente;
                ClientePromotor clienteP;
                using (Cliente as IDisposable)
                {
                    cliente = Cliente.GetCliente(txtRFC.Text);

                    if (cliente != null)
                    {
                        FillView(cliente);
                        clienteP = Cliente.GetClientePromotorRelacion(cliente.idCliente, sesion.Id);
                       
                        this.lblMensaje.Text = " IMPORTANTE: REVISA QUE TUS DATOS ESTÉN CORRECTOS";
                    }

                }
                btnSave.Visible = true;
            }
        }
       
    }
}