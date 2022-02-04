using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using CatalogosSAT;
using System.Data;
using Business;
using Contract;

namespace GAFWEB
{
    public partial class wfrDevoluciones : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!this.IsPostBack)
            {
                try
                {


                    DivBancoEmpresa.Visible = false;
                    DivBancoPromotor.Visible = false;
                    DivBancoCliente.Visible = false;
                    DivBancoContacto.Visible = false;
                  //  DivBancoComi.Visible = false;
                    //DivBancoComiP.Visible = false;
                  // var perfil = Session["perfil"] as string;
                   //var sistema = Session["idSistema"] as long?;
                  var idEmp = Session["idEmpresa"] as int?;

                    //-------------------configuracion de rol---------------------------
              

                    
                     
                        //------------catalogos grandes----------------------
                      OperacionesCatalogos OP=new OperacionesCatalogos();
                      using (OP as IDisposable)
                      {
                                                 


                      ////-----------------------------------------
                          ddlBancoEmpresa.DataSource = OP.ConsultarBancosAll();
                          ddlBancoEmpresa.DataTextField = "Descripcion";
                          ddlBancoEmpresa.DataValueField = "Descripcion";
                          ddlBancoEmpresa.DataBind();

                          ddlBancoPromotor.DataSource = ddlBancoEmpresa.DataSource;
                          ddlBancoPromotor.DataTextField = "Descripcion";
                          ddlBancoPromotor.DataValueField = "Descripcion";
                          ddlBancoPromotor.DataBind();

                          ddlBancoCliente.DataSource = ddlBancoEmpresa.DataSource;
                          ddlBancoCliente.DataTextField = "Descripcion";
                          ddlBancoCliente.DataValueField = "Descripcion";
                          ddlBancoCliente.DataBind();
                          ddlBancoContacto.DataSource = ddlBancoEmpresa.DataSource;
                          ddlBancoContacto.DataTextField = "Descripcion";
                          ddlBancoContacto.DataValueField = "Descripcion";
                          ddlBancoContacto.DataBind();
                    }
                  //--------------------------------para editar---------------------------------
                      string totalString = this.Request.QueryString["total"];
                    decimal total;
                    if (!string.IsNullOrEmpty(totalString) && decimal.TryParse(totalString, out total))
                    {
                       var DD=Session["DevolucionesTotales"] as DatosDevoluciones;

                        txtDevolverEmpresa.Text = DD.totalEmpresa.ToString();
                        txtDevolverPromotor.Text =DD.totalPromotor.ToString();
                        if (DD.cliente != null)
                        {
                            ddlCliente.DataSource = DD.cliente;
                            ddlCliente.DataTextField = "nombre";
                            ddlCliente.DataValueField = "totalCliente";
                            ddlCliente.DataBind();
                            //      txtDevolverCliente.Text =DD.cliente.ToString();
                        }
                        txtDevolverCliente.Text = ddlCliente.SelectedValue;
                        txtDevolverContacto.Text = DD.totalContacto.ToString();
                        string IdPRefacturas = "";
                        foreach (var d in DD.idPrefacturas)
                        {
                            IdPRefacturas = IdPRefacturas + d + "|";
                        }
                        ViewState["IdPRefacturas"] = IdPRefacturas;
                       Session.Remove("DevolucionesTotales");
                       
 
                    }


                    ViewState["DPagosEmpresa"] = new List<DatosPagos>();
                    ViewState["DPagosPromotor"] = new List<DatosPagos>();
                    ViewState["DPagosCliente"] = new List<DatosPagos>();
                    ViewState["DPagosContacto"] = new List<DatosPagos>();

                }
                catch (Exception ex)
                {
                    //Logger.Error(ex.Message);
                }
            }
        }



        private DatosPrefactura GetFactura()
        {

            var sesion = Session["sessionRGV"] as Sesion;
          
            DatosPrefactura fact = new DatosPrefactura();
            fact.Usuario = sesion.Id; //Guid.Parse("33760C0C-E45C-4210-8081-81C80827FA73");// System.Guid.NewGuid(); ///cambiar al verdadero

            List<DatosPagos> DPagosTotal = new List<DatosPagos>();
          
            List<DatosPagos> DPagosEmpresa = ViewState["DPagosEmpresa"] as List<DatosPagos>;
            List<DatosPagos> DPagosPromotor = ViewState["DPagosPromotor"] as List<DatosPagos>;
            List<DatosPagos> DPagosCliente = ViewState["DPagosCliente"] as List<DatosPagos>;
            List<DatosPagos> DPagosContacto = ViewState["DPagosContacto"] as List<DatosPagos>;
           /*
            DatosPagos DP = new DatosPagos();
            DP.Tipo = "I";
            DP.Para = "C";
            DP.Descripcion = txtObservacionesComi.Text;
            DP.Fecha = Convert.ToDateTime(txtFechaComi.Text);
            DP.MetododePago = ddlDPMetodoPagoComi.SelectedValue;
            DP.Monto = Convert.ToDecimal(txtMontoComi.Text);
            if (!string.IsNullOrEmpty(txtClabeComi.Text))
            {
                DP.Banco = ddlBancoComi.SelectedValue;
                DP.ClaveBancaria = txtClabeComi.Text;
                DP.beneficiario = txtbeneficiarioComi.Text;
            }
            DPagos.Add(DP);
             */ 
            /*
            DatosPagos DP2 = new DatosPagos();
            DP2.Tipo = "I";
            DP2.Para = "P";
            DP2.Descripcion = txtObservacionesComiP.Text;
            DP2.Fecha = Convert.ToDateTime(txtFechaComiP.Text);
            DP2.MetododePago = ddlDPMetodoPagoComiP.SelectedValue;
            DP2.Monto = Convert.ToDecimal(txtMontoComiP.Text);
            if (!string.IsNullOrEmpty(txtClabeComiP.Text))
            {
                DP2.Banco = ddlBancoComiP.SelectedValue;
                DP2.ClaveBancaria = txtClabeComiP.Text;
                DP.beneficiario = txtbeneficiarioComiP.Text;
            }
            DPagos.Add(DP2);
             */
           var IdPRefacturas=ViewState["IdPRefacturas"];
            fact.IdPrefacturas = IdPRefacturas.ToString();

            ViewState["DPagosEmpresa"] = DPagosEmpresa;
            ViewState["DPagosPromotor"] = DPagosPromotor;
            ViewState["DPagosCliente"] = DPagosCliente;
            ViewState["DPagosContacto"] = DPagosContacto;

            foreach (var DP in DPagosEmpresa)
            {
                DPagosTotal.Add(DP);
            }
            foreach (var DP in DPagosPromotor)
            {
                DPagosTotal.Add(DP);
            }
            foreach (var DP in DPagosCliente)
            {
                DPagosTotal.Add(DP);
            }
            foreach (var DP in DPagosContacto)
            {
                DPagosTotal.Add(DP);
            }
            if (DPagosTotal != null)
                if (DPagosTotal.Count > 0)
                    fact.Datospagos = DPagosTotal;

            return fact;
        }
        protected void btnGenerarFactura_Click(object sender, EventArgs e)
        {
            this.GuardarFactura();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
                     
         
         
            
        }

       

        #region Helper Methods
       
        private bool ValidarFactura()
        {
            List<DatosPagos> DPagos = ViewState["DPagosEmpresa"] as List<DatosPagos>;
            if (DPagos == null)
            {
                this.lblError.Text = "Escribe los pagos de la empresa";
                return false;
            }
            if (DPagos.Count < 1)
            {
                this.lblError.Text = "Escribe los pagos de la empresa";
                return false;
            }
            List<DatosPagos> DPagosPromotor = ViewState["DPagosPromotor"] as List<DatosPagos>;
            if (DPagosPromotor == null)
            {
                this.lblError.Text = "Escribe los pagos del promotor";
                return false;
            }
            if (DPagosPromotor.Count < 1)
            {
                this.lblError.Text = "Escribe los pagos del promotor";
                return false;
            }
            List<DatosPagos> DPagosCliente = ViewState["DPagosCliente"] as List<DatosPagos>;
            if (DPagosCliente == null)
            {
                this.lblError.Text = "Escribe los pagos del cliente";
                return false;
            }
            if (DPagosCliente.Count < 1)
            {
                this.lblError.Text = "Escribe los pagos del cliente";
                return false;
            }
            List<DatosPagos> DPagosContacto = ViewState["DPagosContacto"] as List<DatosPagos>;
            if (DPagosContacto == null)
            {
                this.lblError.Text = "Escribe los pagos del contacto";
                return false;
            }
            if (DPagosContacto.Count < 1)
            {
                this.lblError.Text = "Escribe los pagos del contacto";
                return false;
            }
            /*
            if ((ViewState["DPagos"] as List<DatosPagos>).Count > 0)
            {
                decimal TotalPagos=0.0M;
                decimal TotalPagosIva = 0.0M;
                if(!string.IsNullOrEmpty(txtMontoComi.Text))
                TotalPagosIva = Convert.ToDecimal(txtMontoComi.Text);
                  foreach (var DP in DPagos)
                 {
                      if(DP.Tipo=="F")
                       TotalPagos = TotalPagos + DP.Monto;
                   //   if (DP.Tipo == "I")
                   //       TotalPagosIva = TotalPagosIva + DP.Monto;
                 }
                  if (TotalPagos > (Convert.ToDecimal(lblTotal.Text.Replace("$", "")) - Convert.ToDecimal(lblTraslados.Text.Replace("$", ""))))
                 {
                     this.lblError.Text = "La devolución factura no pueden ser mayor al total menos el iva de la factura";
                     return false;
                 }
                  if (TotalPagosIva > Convert.ToDecimal(lblTraslados.Text.Replace("$", "")))
                  {
                      this.lblError.Text = "La devolución de comisión no pueden ser mayor al iva de la factura";
                      return false;
                  }

            }
             */ 
          
           
            
            return true;
        }
        //-------------------------------------------


        private void GuardarFactura()
        {
            
            bool error = false;
            if (ValidarFactura())
            {
                var detalles = ViewState["detalles"] as List<Datosdetalle>;
                var iniciales = Session["iniciales"] as string;
                 try
                {
                    var fact = GetFactura();
             
                        Prefactura pre = new Prefactura();
                        using (pre as IDisposable)
                        {
                           // string idPrefacturaString = this.Request.QueryString["idPrefactura"];
                           // if(!string.IsNullOrEmpty(idPrefacturaString))
                            pre.GuardarDevoluiciones(fact, Convert.ToDecimal(txtDevolverEmpresa.Text));
                                btnGenerarFactura.Visible = false;
                         //   else
                         //   pre.GuardarPrefactura(fact,0,null);
                        }
                   
                  
                   // this.ClearAll();
                }
                catch (FaultException ae)
                {
                    error = true;
                    this.lblError.Text = ae.Message;
                }
                catch (ApplicationException ae)
                {
                    error = true;
                    //Logger.Error(ae.Message);
                    if (ae.InnerException != null)
                    {
                        //Logger.Error(ae.InnerException.Message);
                    }
                    this.lblError.Text = ae.Message;
                }
                catch (Exception ae)
                {
                    error = true;
                    //Logger.Error(ae.Message);
                    if (ae.InnerException != null)
                    {
                        //Logger.Error(ae.InnerException.Message);
                    }
                    this.lblError.Text = "Error al generar las devoluciones:" + ae.Message;
                }
                if (!error)
                {
                    this.lblError.Text = "Devoluciones se guardaron correctamente";
                   
                }
               // this.lblError.Text = string.Empty;
            }
             
        }
        private void BindDPagosContactoToGridView()
        {

            List<DatosPagos> DPagosContacto = ViewState["DPagosContacto"] as List<DatosPagos>;
            if (DPagosContacto != null && DPagosContacto.Count > 0)
            {
                int noColumns = this.gvDPagosContacto.Columns.Count;
                this.gvDPagosContacto.Columns[noColumns - 1].Visible = this.gvDPagosContacto.Columns[noColumns - 2].Visible = true;
            }
            else
            {
                int noColumns = this.gvDPagosContacto.Columns.Count;
                this.gvDPagosContacto.Columns[noColumns - 1].Visible = this.gvDPagosContacto.Columns[noColumns - 2].Visible = false;
            }

            this.gvDPagosContacto.DataSource = DPagosContacto;
            this.gvDPagosContacto.DataBind();

        }    
      
        private void BindDPagosClienteToGridView()
        {

            List<DatosPagos> DPagosCliente = ViewState["DPagosCliente"] as List<DatosPagos>;
            if (DPagosCliente != null && DPagosCliente.Count > 0)
            {
                int noColumns = this.gvDPagosCliente.Columns.Count;
                this.gvDPagosCliente.Columns[noColumns - 1].Visible = this.gvDPagosCliente.Columns[noColumns - 2].Visible = true;
            }
            else
            {
                int noColumns = this.gvDPagosCliente.Columns.Count;
                this.gvDPagosCliente.Columns[noColumns - 1].Visible = this.gvDPagosCliente.Columns[noColumns - 2].Visible = false;
            }

            this.gvDPagosCliente.DataSource = DPagosCliente;
            this.gvDPagosCliente.DataBind();

        }    
        private void BindDPagosPromotorToGridView()
        {

            List<DatosPagos> DPagosPromotor = ViewState["DPagosPromotor"] as List<DatosPagos>;
            if (DPagosPromotor != null && DPagosPromotor.Count > 0)
            {
                int noColumns = this.gvDPagosPromotor.Columns.Count;
                this.gvDPagosPromotor.Columns[noColumns - 1].Visible = this.gvDPagosPromotor.Columns[noColumns - 2].Visible = true;
            }
            else
            {
                int noColumns = this.gvDPagosPromotor.Columns.Count;
                this.gvDPagosPromotor.Columns[noColumns - 1].Visible = this.gvDPagosPromotor.Columns[noColumns - 2].Visible = false;
            }

            this.gvDPagosPromotor.DataSource = DPagosPromotor;
            this.gvDPagosPromotor.DataBind();

        }    

        private void BindDPagosToGridView()
        {

            List<DatosPagos> DPagos = ViewState["DPagosEmpresa"] as List<DatosPagos>;
            if (DPagos != null && DPagos.Count > 0)
            {
                int noColumns = this.gvDPagosEmpresa.Columns.Count;
                this.gvDPagosEmpresa.Columns[noColumns - 1].Visible = this.gvDPagosEmpresa.Columns[noColumns - 2].Visible = true;
            }
            else
            {
                int noColumns = this.gvDPagosEmpresa.Columns.Count;
                this.gvDPagosEmpresa.Columns[noColumns - 1].Visible = this.gvDPagosEmpresa.Columns[noColumns - 2].Visible = false;
            }

            this.gvDPagosEmpresa.DataSource = DPagos;
            this.gvDPagosEmpresa.DataBind();

        }
        private void UpdateTotales()
        {
            
            CultureInfo cul = CultureInfo.CreateSpecificCulture("es-MX");
           // var cliente = NtLinkClientFactory.Cliente();
       
     
        }

        #endregion

       
       
                
      
        
        private static string numerodecimales(decimal d, int moneda)
        {
            string D = "0";
            if (moneda == 1)
                D = d.ToString("F1");
            if (moneda == 2)
                D = d.ToString("F2");
            if (moneda == 3)
                D = d.ToString("F3");
            if (moneda == 4)
                D = d.ToString("F4");
            if (moneda == 5)
                D = d.ToString("F5");
            if (moneda == 6)
                D = d.ToString("F6");
            return (D);
        }



        protected void gvDPagosEmpresa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("EliminarDPago"))
            {
                List<DatosPagos> DPagos = ViewState["DPagosEmpresa"] as List<DatosPagos>;

                DPagos.RemoveAt(Convert.ToInt32(e.CommandArgument));
                ViewState["DPagosEmpresa"] = DPagos;


                this.BindDPagosToGridView();
 
            }

        }
        protected void btnAgregarDpagoEmpresa_Click(object sender, EventArgs e)
        {
            this.lblError.Text = "";
            List<DatosPagos> DPagos = ViewState["DPagosEmpresa"] as List<DatosPagos>;
            decimal sumatoria = 0.0M;
            foreach (var dp in DPagos)
            {
                sumatoria = sumatoria + dp.Monto;
            }
            if (sumatoria + Convert.ToDecimal(txtDPMontoEmpresa.Text) > Convert.ToDecimal(txtDevolverEmpresa.Text))
            {
                this.lblError.Text = "La devolución factura no pueden ser mayor al total de la factura";
              
            }
            else
            {
            DatosPagos DP = new DatosPagos();
            DP.Tipo = "F";
            DP.Para = "EM";
            DP.Descripcion = txtDPObservacionesEmpresa.Text;
           // DP.Fecha = Convert.ToDateTime( txtDPFecha.Text);
            DP.MetododePago = ddlDPMetodoPagoEmpresa.SelectedValue;
            DP.Monto = Convert.ToDecimal(txtDPMontoEmpresa.Text);
            if (!string.IsNullOrEmpty(txtClaveBancariaEmpresa.Text))
            {
                DP.Banco = ddlBancoEmpresa.SelectedValue;
                DP.ClaveBancaria = txtClaveBancariaEmpresa.Text;
                DP.beneficiario = txtbeneficiarioEmpresa.Text;
            }
            
            DPagos.Add(DP);
            ViewState["DPagosEmpresa"] = DPagos;
            BindDPagosToGridView();
            txtDPObservacionesEmpresa.Text = "";
            //txtDPFecha.Text = "";
            txtDPMontoEmpresa.Text = "";
            txtClaveBancariaEmpresa.Text = "";
            txtbeneficiarioEmpresa.Text = "";
            }
        }

        protected void ddlDPMetodoPagoEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDPMetodoPagoEmpresa.SelectedValue == "Transferencia")
            {
                DivBancoEmpresa.Visible = true;
            }
            else
                DivBancoEmpresa.Visible = false;
        }

       
        //******************************************************************
      
        protected void ddlDPMetodoPagoPromotor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDPMetodoPagoPromotor.SelectedValue == "Transferencia")
            {
                DivBancoPromotor.Visible = true;
            }
            else
                DivBancoPromotor.Visible = false;
        }

        protected void btnAgregarDpagoPromotor_Click(object sender, EventArgs e)
        {
            this.lblError.Text = "";
            List<DatosPagos> DPagosPromotor = ViewState["DPagosPromotor"] as List<DatosPagos>;
            decimal sumatoria = 0.0M;
            foreach (var dp in DPagosPromotor)
            {
                sumatoria = sumatoria + dp.Monto;
            }
            if (sumatoria + Convert.ToDecimal(txtDPMontoPromotor.Text) > Convert.ToDecimal(txtDevolverPromotor.Text))
            {
                this.lblError.Text = "La devolución factura no pueden ser mayor al total de la factura";

            }
            else
            {
                DatosPagos DP = new DatosPagos();
                DP.Tipo = "F";
                DP.Para = "PM";
                DP.Descripcion = txtDPObservacionesPromotor.Text;
                // DP.Fecha = Convert.ToDateTime( txtDPFecha.Text);
                DP.MetododePago = ddlDPMetodoPagoPromotor.SelectedValue;
                DP.Monto = Convert.ToDecimal(txtDPMontoPromotor.Text);
                if (!string.IsNullOrEmpty(txtClaveBancariaPromotor.Text))
                {
                    DP.Banco = ddlBancoPromotor.SelectedValue;
                    DP.ClaveBancaria = txtClaveBancariaPromotor.Text;
                    DP.beneficiario = txtbeneficiarioPromotor.Text;
                }

                DPagosPromotor.Add(DP);
                ViewState["DPagos"] = DPagosPromotor;
                BindDPagosPromotorToGridView();
                txtDPObservacionesPromotor.Text = "";
                //txtDPFecha.Text = "";
                txtDPMontoPromotor.Text = "";
                txtClaveBancariaPromotor.Text = "";
                txtbeneficiarioPromotor.Text = "";
            }
        }
        protected void gvDPagosPromotor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("EliminarDPago"))
            {
                List<DatosPagos> DPagos = ViewState["DPagosPromotor"] as List<DatosPagos>;

                DPagos.RemoveAt(Convert.ToInt32(e.CommandArgument));
                ViewState["DPagosPromotor"] = DPagos;


                this.BindDPagosPromotorToGridView();

            }

        }

        protected void ddlDPMetodoPagoCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDPMetodoPagoCliente.SelectedValue == "Transferencia")
            {
                DivBancoCliente.Visible = true;
            }
            else
                DivBancoCliente.Visible = false;
        }

        protected void btnAgregarDpagoCliente_Click(object sender, EventArgs e)
        {
            this.lblError.Text = "";
            List<DatosPagos> DPagosCliente = ViewState["DPagosCliente"] as List<DatosPagos>;
            decimal sumatoria = 0.0M;
            foreach (var dp in DPagosCliente)
            {
                if( ddlCliente.SelectedItem.Text==dp.Cliente)
                sumatoria = sumatoria + dp.Monto;
            }
            if (sumatoria + Convert.ToDecimal(txtDPMontoCliente.Text) > Convert.ToDecimal(txtDevolverCliente.Text))
            {
                this.lblError.Text = "La devolución factura no pueden ser mayor al total de la factura";

            }
            else
            {
                DatosPagos DP = new DatosPagos();
                DP.Tipo = "F";
                DP.Para = "CL";
                DP.Descripcion = txtDPObservacionesCliente.Text;
                // DP.Fecha = Convert.ToDateTime( txtDPFecha.Text);
                DP.MetododePago = ddlDPMetodoPagoCliente.SelectedValue;
                DP.Monto = Convert.ToDecimal(txtDPMontoCliente.Text);
                if (!string.IsNullOrEmpty(txtClaveBancariaCliente.Text))
                {
                    DP.Banco = ddlBancoCliente.SelectedValue;
                    DP.ClaveBancaria = txtClaveBancariaCliente.Text;
                    DP.beneficiario = txtbeneficiarioCliente.Text;
                }
                DP.Cliente = ddlCliente.SelectedItem.Text;
                DPagosCliente.Add(DP);
                ViewState["DPagos"] = DPagosCliente;
                BindDPagosClienteToGridView();
                txtDPObservacionesCliente.Text = "";
                //txtDPFecha.Text = "";
                txtDPMontoCliente.Text = "";
                txtClaveBancariaCliente.Text = "";
                txtbeneficiarioCliente.Text = "";
            }
        }

        protected void gvDPagosCliente_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("EliminarDPago"))
            {
                List<DatosPagos> DPagosCliente = ViewState["DPagosCliente"] as List<DatosPagos>;

                DPagosCliente.RemoveAt(Convert.ToInt32(e.CommandArgument));
                ViewState["DPagosCliente"] = DPagosCliente;


                this.BindDPagosClienteToGridView();

            }

        }

        protected void ddlDPMetodoPagoContacto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDPMetodoPagoContacto.SelectedValue == "Transferencia")
            {
                DivBancoContacto.Visible = true;
            }
            else
                DivBancoContacto.Visible = false;
        }

        protected void btnAgregarDpagoContacto_Click(object sender, EventArgs e)
        {
            this.lblError.Text = "";
            List<DatosPagos> DPagosContacto = ViewState["DPagosContacto"] as List<DatosPagos>;
            decimal sumatoria = 0.0M;
            foreach (var dp in DPagosContacto)
            {
                sumatoria = sumatoria + dp.Monto;
            }
            if (sumatoria + Convert.ToDecimal(txtDPMontoContacto.Text) > Convert.ToDecimal(txtDevolverContacto.Text))
            {
                this.lblError.Text = "La devolución factura no pueden ser mayor al total de la factura";

            }
            else
            {
                DatosPagos DP = new DatosPagos();
                DP.Tipo = "F";
                DP.Para = "CT";
                DP.Descripcion = txtDPObservacionesContacto.Text;
                // DP.Fecha = Convert.ToDateTime( txtDPFecha.Text);
                DP.MetododePago = ddlDPMetodoPagoContacto.SelectedValue;
                DP.Monto = Convert.ToDecimal(txtDPMontoContacto.Text);
                if (!string.IsNullOrEmpty(txtClaveBancariaContacto.Text))
                {
                    DP.Banco = ddlBancoContacto.SelectedValue;
                    DP.ClaveBancaria = txtClaveBancariaContacto.Text;
                    DP.beneficiario = txtbeneficiarioContacto.Text;
                }

                DPagosContacto.Add(DP);
                ViewState["DPagos"] = DPagosContacto;
                BindDPagosContactoToGridView();
                txtDPObservacionesContacto.Text = "";
                //txtDPFecha.Text = "";
                txtDPMontoContacto.Text = "";
                txtClaveBancariaContacto.Text = "";
                txtbeneficiarioContacto.Text = "";
            }
        }
        protected void gvDPagosContacto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("EliminarDPago"))
            {
                List<DatosPagos> DPagosContacto = ViewState["DPagosContacto"] as List<DatosPagos>;

                DPagosContacto.RemoveAt(Convert.ToInt32(e.CommandArgument));
                ViewState["DPagosContacto"] = DPagosContacto;


                this.BindDPagosContactoToGridView();

            }

        }

        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDevolverCliente.Text = ddlCliente.SelectedValue;
                    
        }
    }
}