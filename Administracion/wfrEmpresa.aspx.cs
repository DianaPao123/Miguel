using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GAFContract;
using GAFBusiness;
using ClienteNtLink;
using System.Net;

namespace GAFWEB
{
    public partial class wfrEmpresa : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                var Empresa = new GAFEmpresa();
                empresa empresa;
                List<Catalogo_Giros> giro = new List<Catalogo_Giros>();
                using (Empresa as IDisposable)
                {
                   giro= Empresa.GetListGiro();
                   ddlGiro.DataSource = giro;
                   ddlGiro.DataBind();

                }
                string idEmpresaString = this.Request.QueryString["idEmpresa"];
                int idEmpresa;
                if(!string.IsNullOrEmpty(idEmpresaString) && int.TryParse(idEmpresaString, out idEmpresa))
                {
                    this.pnlSucursal.Visible = false;
                     using (Empresa as IDisposable)
                    {
                       empresa= Empresa.GetById(idEmpresa);
                        //empresa = clienteServicio.ObtenerEmpresaById(idEmpresa);
                       // var sistema = clienteServicio.ObtenerSistemaById((int)empresa.idSistema.Value);
                    }
                    this.txtRFC.Enabled = false;

                    this.pnlSucursal.Visible = true;

                    this.FillView(empresa);
                    empresa.Logo = null;
                    ViewState["empresa"] = empresa;
                }
                else
                {
                    this.txtRFC.Enabled = true;
                    this.pnlSucursal.Visible = true;
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var empresa = ViewState["empresa"] as empresa;
         //   var cliente = NtLinkClientFactory.Cliente();
            var Empresa = new GAFEmpresa();
                 
            byte[] cert = null;
            byte[] key = null;
            
            if (this.fuLogoEmpresa.HasFile)
            {
                if (this.fuLogoEmpresa.FileBytes.Length > (50 * 1024))
                {
                    this.lblError.Text = "El tamaño del archivo de logo no debe exceder los 50 Kb.";
                    return;
                }
            }
            if (this.fuCertificado.HasFile)
            {
                cert = this.fuCertificado.FileBytes;
            }
            if (this.fuLlave.HasFile)
            {
                key = this.fuLlave.FileBytes;
            }
            if(empresa != null)
            {
                
                empresa modEmpresa = this.GetEmpresaFromView();
                string valRFC = validaRFC(modEmpresa.RFC);
                if (valRFC == "OK")
                {
                    if (!string.IsNullOrEmpty(this.txtPassWordLlave.Text) && this.fuCertificado.HasFile && this.fuLlave.HasFile)
                    {
                        modEmpresa.PassKey = this.txtPassWordLlave.Text;
                    }
                    else
                    {
                        modEmpresa.PassKey = empresa.PassKey;
                    }
                    modEmpresa.IdEmpresa = empresa.IdEmpresa;
                    modEmpresa.idSistema = empresa.idSistema;
                    modEmpresa.Folio = empresa.Folio;///cambio para que no afecte folio empresa
                    using (Empresa as IDisposable)
                    {
                        try
                        {
                            var extension = Path.GetExtension(fuLlave.FileName).ToLower();
                            Empresa.GuardarEmpresa(modEmpresa, cert, key, this.txtPassWordLlave.Text, this.GetLogoBytes(), extension);
                            SaveCliente(modEmpresa);//actualiza los clientes con mismo RFC de la empresa
                            this.Response.Redirect("wfrEmpresasConsulta.aspx");
                        }
                        catch (FaultException ex)
                        {
                            this.lblError.Text = ex.Message;
                        }
                    }
                }
                else
                    this.lblError.Text = valRFC;
            }
            else
            {
                try
                {
                    empresa emp = this.GetEmpresaFromView();
                    emp.Linea = "A";
                    //---------------------------
                    string valRFC=validaRFC(emp.RFC);
               //-----------------------------
                    if (valRFC == "OK")
                    {
                        var extension = Path.GetExtension(fuLlave.FileName).ToLower();
                        Empresa.GuardarEmpresa(emp, cert, key, this.txtPassWordLlave.Text, this.GetLogoBytes(), extension);
                        SaveCliente(emp);//actualiza los clientes con mismo RFC de la empresa
                      
                        var sucursal = new Sucursales
                                           {
                                               Nombre = this.txtSucursal.Text,
                                               LugarExpedicion = this.txtLugarExpedicion.Text,
                                               IdEmpresa = emp.IdEmpresa
                                           };
                        var GAFSucursal = new GAFSucursales();

                        GAFSucursal.SaveSucursal(sucursal);

                        this.Response.Redirect("wfrEmpresasConsulta.aspx");
                    }
                    else
                        this.lblError.Text = valRFC;
                }
                catch (FaultException ex)
                {
                    this.lblError.Text = ex.Message;
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("wfrEmpresasConsulta.aspx");
        }

        #region Helper Methods

        private byte[] GetLogoBytes()
        {
            return this.fuLogoEmpresa.HasFile ? fuLogoEmpresa.FileBytes : null;
        }

        private void FillView(empresa empresa)
        {
            this.txtRFC.Text = empresa.RFC;
            this.txtRazonSocial.Text = empresa.RazonSocial;
            this.txtDireccion.Text = empresa.Direccion;
            this.txtColonia.Text = empresa.Colonia;
            this.txtMunicipio.Text = empresa.Ciudad;
            this.txtEstado.Text = empresa.Estado;
            this.txtCP.Text = empresa.CP;
            this.txtEmail.Text = empresa.Email;
            this.txtWeb.Text = empresa.Pagina;
            this.txtContacto.Text = empresa.Contacto;
            this.txtTelefono.Text = empresa.Telefono;
            this.txtInt.Text = empresa.NoInt;
            this.txtLocalidad.Text = empresa.Localidad;
            this.txtReferencia.Text = empresa.Referencia;
            this.txtExt.Text = empresa.NoExt;
            this.ddlGiro.SelectedValue = empresa.Id_Giro.ToString();
            /*
            ListItem li = new ListItem(empresa.RegimenFiscal, empresa.RegimenFiscal);
            if (ddlRegimen.Items.Contains(li))
            {
                this.ddlRegimen.Text = empresa.RegimenFiscal;
            }
            else
            {
                this.ddlRegimen.SelectedValue = "Otro";
                this.tdRegimen.Visible = true;
                this.txtRegimen.Text = empresa.RegimenFiscal;
            }
             */ 
            this.ddlOrientacion.SelectedValue = empresa.Orientacion.ToString();
            this.txtLeyendaPie.Text = empresa.LeyendaInferior;
            this.txtLeyendaSuperior.Text = empresa.LeyendaSuperior;
            this.txtCURP.Text = empresa.CURP;
            this.lblVencimiento.Text = empresa.VencimientoCert;

            var GAFSucursal = new GAFSucursales();
           var suc=  GAFSucursal.GetSucursal(empresa.IdEmpresa);
           txtSucursal.Text = suc.Nombre;
           txtLugarExpedicion.Text = suc.LugarExpedicion;
        }

        private empresa GetEmpresaFromView()
        {
            var sistema = Session["idSistema"] as long?;
            sistema = 0;
            var empresa = new empresa
                              {
                RFC = this.txtRFC.Text,
                RazonSocial = this.txtRazonSocial.Text,
                Direccion = this.txtDireccion.Text,
                Colonia = this.txtColonia.Text,
                Ciudad = this.txtMunicipio.Text,
                Estado = this.txtEstado.Text,
                Telefono = this.txtTelefono.Text,
                CP = this.txtCP.Text,
                Email = this.txtEmail.Text,
                Pagina = this.txtWeb.Text,
                Contacto = this.txtContacto.Text,
                PassKey = this.txtPassWordLlave.Text,
                RegimenFiscal =  ddlRegimen.SelectedValue,
                idSistema = sistema.Value,
                LeyendaSuperior = txtLeyendaSuperior.Text,
                LeyendaInferior = txtLeyendaPie.Text,
                Orientacion = int.Parse(ddlOrientacion.SelectedValue),
                CURP = this.txtCURP.Text,
                Linea = "A",
                Id_Giro=Convert.ToInt64( ddlGiro.SelectedValue)
            };
            /*
            if (ddlRegimen.SelectedValue == "Otro")
            {
                empresa.RegimenFiscal = txtRegimen.Text;
            }
             */ 
            empresa.NoExt = string.IsNullOrEmpty(txtExt.Text) ? null : txtExt.Text;
            empresa.NoInt = string.IsNullOrEmpty(txtInt.Text) ? null : txtInt.Text;
            empresa.Localidad = string.IsNullOrEmpty(txtLocalidad.Text) ? null : txtLocalidad.Text;
            empresa.Referencia = string.IsNullOrEmpty(txtReferencia.Text) ? null : txtReferencia.Text;
            return empresa;
        }

        private IEnumerable<string> GetDocumentos()
        {
            
            return ConfigurationManager.AppSettings["Documentos"].Split('|').ToList();
        }

       

        #endregion

        protected void ddlRegimen_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            if (ddlRegimen.SelectedValue == "Otro")
            {
                tdRegimen.Visible = true;
                valRegimen.Enabled = true;
            }
            else
            {
                tdRegimen.Visible = false;
                valRegimen.Enabled = false;
            }
             */
        }

        protected void btnValidar_Click(object sender, EventArgs e)
        {
            var empresa = ViewState["empresa"] as empresa;
          //  var cliente = NtLinkClientFactory.Cliente();
            var Empresa = new GAFEmpresa();
            byte[] cert = null;
            byte[] key = null;
            if (this.fuCertificado.HasFile)
            {
                
                cert = this.fuCertificado.FileBytes;
            }
            if (this.fuLlave.HasFile)
            {
                key = this.fuLlave.FileBytes;
            }
            if (empresa != null)
            {
                empresa modEmpresa = this.GetEmpresaFromView();
                if (!string.IsNullOrEmpty(this.txtPassWordLlave.Text) && this.fuCertificado.HasFile && this.fuLlave.HasFile)
                {
                    modEmpresa.PassKey = this.txtPassWordLlave.Text;
                }
                else
                {
                    modEmpresa.PassKey = empresa.PassKey;
                }
                modEmpresa.IdEmpresa = empresa.IdEmpresa;
                modEmpresa.idSistema = empresa.idSistema;
                var extension = Path.GetExtension(fuLlave.FileName).ToLower();
                lblAdvertencia.Text = Empresa.ValidaCSD(modEmpresa, cert, key, this.txtPassWordLlave.Text, extension);
            }    
        }
        //------------------------------------------
        public string validaRFC(string RFC)
        {
            /*
            try
            {
                ClienteTimbradoNtlink clienteSW = new ClienteTimbradoNtlink();

                ServicePointManager.DefaultConnectionLimit = 200;
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) =>
                {
                    return true;
                };

                string r = clienteSW.ValidaTimbraRFC(RFC);
                return r;
            }
            catch (FaultException ex)
            {
                this.lblError.Text = ex.Message;
                return ex.Message;
            }
           */
            return "OK";
        }

        private void SaveCliente(empresa emp)
        {


            var cli = new GAFClientes();
            using (cli as IDisposable)
            {
                var c = cli.GetCliente(emp.RFC);
                if (c != null)
                {
                    clientes modCliente = c;
                    modCliente.Ciudad = emp.Ciudad;
                    modCliente.Colonia = emp.Colonia;
                    modCliente.CP = emp.CP;
                    modCliente.CURP = emp.CURP;
                    modCliente.Direccion = emp.Direccion;
                    modCliente.Email = emp.Email;
                    modCliente.Estado = emp.Estado;
                    modCliente.NoExt = emp.NoExt;
                    modCliente.NoInt = emp.NoInt;
                    modCliente.RazonSocial = emp.RazonSocial;

                    cli.SaveCliente(modCliente);
                }
            }

        }
        

    }
}
