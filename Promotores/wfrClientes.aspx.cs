using System;
using System.ServiceModel;
using Contract;
using Business;
using System.Text.RegularExpressions;

namespace GAFWEB
{
    public partial class wfrClientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
               
                
                string idClienteString = Request.QueryString["idCliente"];

                int idCliente;
                if(!string.IsNullOrEmpty(idClienteString) && int.TryParse(idClienteString, out idCliente))
                {

                    if (idCliente != 0)
                    {
                        var sesion = Session["sessionRGV"] as Sesion;

                        var Cliente = new GAFClientes();
                        ClientePromotor clienteP;
                         clientes cliente;
                        using (Cliente as IDisposable)
                        {
                            cliente = Cliente.GetCliente(idCliente);
                        }

                        this.FillView(cliente);
                        clienteP = Cliente.GetClientePromotorRelacion(cliente.idCliente, sesion.Id);
                        if (clienteP != null)
                        { FillView2(clienteP);
                        if (clienteP.Validado == true)
                        {
                            txtRFC.Enabled = false;
                            txtPPromotor.Enabled = false;
                            txtPCliente.Enabled = false;
                            txtPEmpresa.Enabled = false;
                            txtPContacto.Enabled = false;
                        }
                        }
                        ViewState["cliente"] = cliente;

                        btnSave.Visible = true;
                    }
                }
               
                 
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
           
            this.lblMensaje.Text = "";
            lblError.Text = "";
            decimal p = Convert.ToDecimal(txtPPromotor.Text);
            decimal cl = Convert.ToDecimal(txtPCliente.Text);
            decimal en = Convert.ToDecimal(txtPEmpresa.Text);
            decimal cont = Convert.ToDecimal(txtPContacto.Text);

            decimal suma = p + cl + en + cont;
            if (suma != 16)
                lblError.Text = "Error la suma de porcentaje es distinta a 16";
            else
            {

                var sesion = Session["sessionRGV"] as Sesion;

                var cli = new GAFClientes();
                using (cli as IDisposable)
                {
                    var c = cli.GetCliente(txtRFC.Text);
                    if (c != null)
                        ViewState["cliente"] = c;
                }

                var cliente = ViewState["cliente"] as clientes;
                if (cliente != null)
                {
                    clientes modCliente = this.GetClientFromView();
                    modCliente.idCliente = cliente.idCliente;
                    var clientepromotor = new ClientePromotor
                                  {
                                      PorcentajePromotor = Convert.ToDecimal(txtPPromotor.Text),
                                      PorcentajeCliente = Convert.ToDecimal(txtPCliente.Text),
                                      PorcentajeEmpresa = Convert.ToDecimal(txtPEmpresa.Text),
                                      PorcentajeContacto = Convert.ToDecimal(txtPContacto.Text),
                                      RazonSocial = this.txtRazonSocial.Text,
                                      IdPromotor = sesion.Id,
                                      idClientePromotor = 0
                                  };



                    try
                    {
                        var client = new GAFClientes();
                        using (client as IDisposable)
                        {
                            int z = client.SaveCliente(modCliente);
                            if (z != 0)
                            {
                                ClientePromotor cp = client.GetClientePromotorRelacion(z, sesion.Id);
                                if (cp != null) //si existe el pormotor con cliente
                                {
                                    cp.PorcentajeCliente = clientepromotor.PorcentajeCliente;
                                    cp.PorcentajeContacto = clientepromotor.PorcentajeContacto;
                                    cp.PorcentajeEmpresa = clientepromotor.PorcentajeEmpresa;
                                    cp.PorcentajePromotor = clientepromotor.PorcentajePromotor;
                                    client.SaveClientePromotores(cp);
                                }
                                else //no existe
                                {

                                    clientepromotor.idCliente = z;
                                    client.SaveClientePromotores(clientepromotor);
                                }
                            }

                        }

                        //Response.Redirect("wfrClientesConsulta.aspx");
                        Response.Redirect("Default.aspx");
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
                            int z = client.SaveCliente(this.GetClientFromView());
                            if (z != 0)
                            {
                                var clientepromotor = new ClientePromotor
                                {
                                    PorcentajePromotor = Convert.ToDecimal(txtPPromotor.Text),
                                    PorcentajeCliente = Convert.ToDecimal(txtPCliente.Text),
                                    PorcentajeEmpresa = Convert.ToDecimal(txtPEmpresa.Text),
                                    PorcentajeContacto = Convert.ToDecimal(txtPContacto.Text),
                                    RazonSocial = this.txtRazonSocial.Text,
                                    IdPromotor = sesion.Id,
                                    idClientePromotor = 0
                                };
                                ClientePromotor cp = client.GetClientePromotorRelacion(z, sesion.Id);
                                if (cp != null) //si existe el pormotor con cliente
                                {
                                    cp.PorcentajeCliente = clientepromotor.PorcentajeCliente;
                                    cp.PorcentajeContacto = clientepromotor.PorcentajeContacto;
                                    cp.PorcentajeEmpresa = clientepromotor.PorcentajeEmpresa;
                                    cp.PorcentajePromotor = clientepromotor.PorcentajePromotor;
                                    client.SaveClientePromotores(cp);
                                }
                                else //no existe
                                {
                                    clientepromotor.idCliente = z;
                                    client.SaveClientePromotores(clientepromotor);
                                }

                                Response.Redirect("Default.aspx");
                                //Response.Redirect("wfrClientesConsulta.aspx");
                            }
                        }
                    }
                    catch (FaultException ex)
                    {
                        this.lblError.Text = ex.Message;
                    }
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
            this.txtInt.Text = cliente.NoInt;
            this.txtExt.Text = cliente.NoExt;
            if (cliente.RegimenFiscal != null)
                this.ddlRegimen.SelectedValue = cliente.RegimenFiscal;

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
                                   Linea="A",
                                  RegimenFiscal = ddlRegimen.SelectedValue

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
                        if (clienteP != null)
                        {  FillView2(clienteP);
                        if (clienteP.Validado == true)
                        {
                            txtRFC.Enabled = false;
                            txtPPromotor.Enabled = false;
                            txtPCliente.Enabled = false;
                            txtPEmpresa.Enabled = false;
                            txtPContacto.Enabled = false;
                        }
                        }     
                        this.lblMensaje.Text = " IMPORTANTE: REVISA QUE TUS DATOS ESTÉN CORRECTOS";
                    }

                    btnSave.Visible = true;
                         

                }
                
            }
        }
         private void FillView2(ClientePromotor cliente)
        {
           // this.ddlEmpresa.SelectedValue = cliente.idempresa.ToString();
            this.txtPCliente .Text = cliente.PorcentajeCliente.ToString();
            this.txtPContacto.Text = cliente.PorcentajeContacto.ToString();
            this.txtPEmpresa.Text = cliente.PorcentajeEmpresa.ToString();
            this.txtPPromotor.Text = cliente.PorcentajePromotor.ToString();
         }
       
    }
}