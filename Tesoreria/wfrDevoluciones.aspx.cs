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
                

                  
                  var idEmp = Session["idEmpresa"] as int?;

                  var rol = Session["rol"];
                  rol = "Promotores";
                  
                    //-----------------------------------------

               
                 // ViewState["PrefacuturaDatos"] = new DatosPrefactura();
                    ViewState["detalles"] = new List<Datosdetalle>();
                    ViewState["detallesImpuestos"] = new List<DatosdetalleRT>();//para impuestos
                    ViewState["CfdiRelacionado"] = new List<string>();
                    ViewState["iva"] = 0M;
                    ViewState["total"] = 0M;
                    ViewState["subtotal"] = 0M;
                    ViewState["descuento"] = 0M;
                    ViewState["DPagos"] = new List<DatosPagos>();
                   

                    this.UpdateTotales();

                    string idDevolucionesString = this.Request.QueryString["idDevoluciones"];
                    long idDevoluciones;
                    if (!string.IsNullOrEmpty(idDevolucionesString) && long.TryParse(idDevolucionesString, out idDevoluciones))
                    {
                        FillView(idDevoluciones);
                    }
                    
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
                          
            List<DatosPagos> DPagos = ViewState["DPagos"] as List<DatosPagos>;
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
            ViewState["DPagos"] = DPagos;


            if (DPagos != null)
                if (DPagos.Count > 0)
                    fact.Datospagos = DPagos;

            return fact;
        }
        protected void btnGenerarFactura_Click(object sender, EventArgs e)
        {

            Response.Redirect("wfrPrefacturaConsultaDevolucion.aspx?");
            //this.GuardarFactura();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            
            
         
         
            
        }

      


        protected void ddlMoneda_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            OperacionesCatalogos OP = new OperacionesCatalogos();
            using (OP as IDisposable)
            {

                CatalogosSAT.c_Moneda mone = OP.Consultar_Moneda(ddlMoneda.SelectedValue);
               ViewState["DecimalMoneda"] = mone.Decimales;


                if (this.ddlMoneda.SelectedValue != "MXN")
                {
                    this.txtTipoCambio.Visible = true;
                    this.lblTipoCambio.Visible = true;

                    CatalogosSAT.Divisas D = OP.Consultar_TipoDivisa(this.ddlMoneda.SelectedValue);
                    if (D != null)
                        txtTipoCambio.Text = D.PesosDivisa.ToString();
                    else
                        txtTipoCambio.Text = "";


                }
                else
                {
                    txtTipoCambio.Text = "";

                    this.txtTipoCambio.Visible = false;
                    this.lblTipoCambio.Visible = false;
                }
            }
             */ 
        }

       

        protected void gvDetalles_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            
        }

      protected void btnSeleccionarConcepto_Click(object sender, EventArgs e)
        {
            
           
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
        }

        #region Helper Methods
        /*
        private void GetTipoCambio()
        {
            OperacionesCatalogos OP=new OperacionesCatalogos();
             using (OP as IDisposable)
            {
                  
             string tipoCambio = OP.TipoCambio();
                this.txtTipoCambio.Text = tipoCambio;
            }
        }
        */
        private bool ValidarFactura()
        {
           
            
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
                            string idPrefacturaString = this.Request.QueryString["idPrefactura"];
                            if(!string.IsNullOrEmpty(idPrefacturaString))
                                pre.GuardarDevoluiciones(fact, Convert.ToInt64(idPrefacturaString));
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
                    ActualizarSaldosMaster();
                }
               // this.lblError.Text = string.Empty;
            }
             
        }

    

      
     
      


     

        private void BindDPagosToGridView()
        {

            List<DatosPagos> DPagos = ViewState["DPagos"] as List<DatosPagos>;
            if (DPagos != null && DPagos.Count > 0)
            {
                int noColumns = this.gvDPagos.Columns.Count;
                this.gvDPagos.Columns[noColumns - 1].Visible = this.gvDPagos.Columns[noColumns - 2].Visible = true;
            }
            else
            {
                int noColumns = this.gvDPagos.Columns.Count;
                this.gvDPagos.Columns[noColumns - 1].Visible = this.gvDPagos.Columns[noColumns - 2].Visible = false;
            }

            this.gvDPagos.DataSource = DPagos;
            this.gvDPagos.DataBind();

        }
        private void UpdateTotales()
        {
            
        }

        #endregion

        protected void gvBuscarConceptos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
           
        }

       
        protected void ddlMetodoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            if (ddlMetodoPago.SelectedValue == "PPD")
            {
                divParcialidades.Visible = true;
            }

            else
            {
                divParcialidades.Visible = false;

            }
           */

        }

        //---------------------------------------------------------------------------
        public void ActualizarSaldosMaster()
        {
      
        }

        //------------------------------------------------------------------------------

       
       
        protected void ckeckImpuestos_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void btnAgregarImpuesto_Click(object sender, EventArgs e)
        {
            
           
     
        }

        protected void gvImpuestos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
            
        }

        protected void cbImpuestos_CheckedChanged(object sender, EventArgs e)
        {
            
           
        
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

          
        }

      


        protected void btnBuscarConceptoHistorico_Click(object sender, EventArgs e)
        {
           
   
        }

        protected void ddlConceptos_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
         }


        protected void ddlTipoFactor_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void ddlTipoImpuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

     
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

        protected void cbCfdiRelacionados_CheckedChanged(object sender, EventArgs e)
        {
        }

        protected void btnCfdiRelacionado_Click(object sender, EventArgs e)
        {
           

        }

        protected void gvCfdiRelacionado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
          
       
        }

        protected void txtFechaPago_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddlLinea_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void cbDPagos_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        protected void gvDPagos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("EliminarDPago"))
            {
                List<DatosPagos> DPagos = ViewState["DPagos"] as List<DatosPagos>;

                DPagos.RemoveAt(Convert.ToInt32(e.CommandArgument));
                ViewState["DPagos"] = DPagos;


                this.BindDPagosToGridView();
 
            }
            if (e.CommandName.Equals("Pago"))
            {

                var id = this.gvDPagos.DataKeys[Convert.ToInt32(e.CommandArgument)-1].Values["idPagos"];
                string monto = this.gvDPagos.Rows[Convert.ToInt32(e.CommandArgument)-1].Cells[1].Text;
                long idDevoluciones;
                var cliente = new Prefactura();
                using (cliente as IDisposable)
                {
                    cliente.CambiarestatusDevolucionPago(Convert.ToInt64(id), true);
                    decimal total = Convert.ToDecimal(txtSaldoPendiente.Text) - Convert.ToDecimal(monto.Replace("$", ""));
                     string idDevolucionesString = this.Request.QueryString["idDevoluciones"];

                     if (!string.IsNullOrEmpty(idDevolucionesString) && long.TryParse(idDevolucionesString, out idDevoluciones))
                     {
                         cliente.SetDecolucionesOperacionesPendiente(idDevoluciones, total);
                         txtSaldoPendiente.Text = total.ToString();

                         FillView(idDevoluciones);
                     }
                }
            }

        }

        protected void gvDPagos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[6].Text == "EM")
                    e.Row.Cells[6].Text = "Empresa";
                if (e.Row.Cells[6].Text == "PM")
                    e.Row.Cells[6].Text = "Promotor";
                if (e.Row.Cells[6].Text == "CL")
                    e.Row.Cells[6].Text = "Cliente";
                if (e.Row.Cells[6].Text == "CT")
                    e.Row.Cells[6].Text = "Contacto";
     
            }
        }
        protected void ddlDPMetodoPagoComi_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            if (ddlDPMetodoPagoComi.SelectedValue == "Transferencia")
            {
                DivBancoComi.Visible = true;
            }
            else
                DivBancoComi.Visible = false;
             */ 
        }
        //******************************************************************

        private void FillView( long id)
        {
            var sesion = Session["sessionRGV"] as Sesion;

            var factu = new Prefactura();

            using (factu as IDisposable)
            {
                
                var ventas = factu.GetDecolucionesOperaciones(id);


                List<PrePagos> lista;

                {
                    lista = ventas.ToList();
                }
                


                ViewState["facturas"] = lista;

                this.gvDPagos.DataSource = lista;
                this.gvDPagos.DataBind();
                txtSaldoPendiente.Text=  factu.GetDecolucionesOperacionesPendiente(id).ToString();

                 var d= factu.GetDecoluciones(id);
                 if (!string.IsNullOrEmpty(d.IdPreFacturas))
                 {
                     var lis = factu.GetPAGO(d.IdPreFacturas);
                     this.gvPagos.DataSource = lis;
                     this.gvPagos.DataBind();
              
                 }
            }

        }
        protected void gvDPagos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvDPagos.DataSource = ViewState["facturas"];
            this.gvDPagos.PageIndex = e.NewPageIndex;
            this.gvDPagos.DataBind();
        
        }
     
    }
}