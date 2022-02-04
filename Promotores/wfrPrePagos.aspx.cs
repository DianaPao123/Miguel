using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using System.IO;

using System.Data;
using Business;
using System.Configuration;
using Contract;
using CatalogosSAT;
using System.ServiceModel;
using System.Globalization;
using DobleClip;
namespace GAFWEB
{
    public partial class wfrPrePagos : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            string clientScript = ClientScript.GetPostBackEventReference(btnGenerar, null);
            Botones botonRGV = new Botones();
            botonRGV.PrevenirDoubleEnvio(btnGenerar, clientScript, "Confirma que deseas generar el pago");

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                Session["Cantidad_de_Procesamientos"] = 0;
                 // int idEmpresa ;
                 // var Empresa = new GAFEmpresa();
                 // using (Empresa as IDisposable)
                 // {
                 //     this.ddlEmpresa.Items.Clear();
                 //     this.ddlEmpresa.DataSource = Empresa.GetListForLine("A");
                 //     this.ddlEmpresa.DataBind();
                List<Catalogo_Giros> giro = new List<Catalogo_Giros>();
                int idEmpresa;
                var Empresa = new GAFEmpresa();
                using (Empresa as IDisposable)
                {
                    giro = Empresa.GetListGiro();
                    ddlGiro.DataSource = giro;
                    ddlGiro.DataBind();
                    this.ddlEmpresa.Items.Clear();
                    this.ddlEmpresa.DataSource = Empresa.GetListGiroempresaBy(Convert.ToInt64(ddlGiro.SelectedValue));
                    this.ddlEmpresa.DataBind();


                      idEmpresa = Convert.ToInt16(ddlEmpresa.SelectedValue);


                      if (ddlEmpresa.Items.Count > 0)
                      {
                          var cliente = new GAFClientes();
                          using (cliente as IDisposable)
                          {
                              var sesion = Session["sessionRGV"] as Sesion;

                              ddlClientes.Items.Clear();
                              var clientes = cliente.GetListClientePromotor(sesion.Id);
                              this.ddlClientes.DataSource = clientes;
                              this.ddlClientes.DataBind();
                          }
                      }


                     
                  }
                                      OperacionesCatalogos OP=new OperacionesCatalogos();
                                      using (OP as IDisposable)
                                      {
                                          ddlMonedaP.DataSource = OP.Consultar_MonedaAll();
                                          ddlMonedaP.DataTextField = "Descripción";
                                          ddlMonedaP.DataValueField = "c_Moneda1";
                                          ddlMonedaP.DataBind();
                                          ddlMonedaP.SelectedValue = "MXN";
                                      }
                                      ViewState["DPagos"] = new List<DatosPagos>();
                                      ViewState["Pagos"] = new List<Pagos>();
                 
                  this.FillView();
                  string idPrefacturaString = this.Request.QueryString["idPrefactura"];
                  int idPrefactura;
                  if (!string.IsNullOrEmpty(idPrefacturaString) && int.TryParse(idPrefacturaString, out idPrefactura))
                  {
                     
                      Editar(idPrefactura);
                      ViewState["idPrefacturaStringxx"] = idPrefacturaString;
                  }

            }
        }


        private void FillView()
        {



            var factu = new Prefactura();
             var M = new Movimientos();

             using (factu as IDisposable)
             {

                 var ventas = factu.GetListPagoPorPagarPrefactura(int.Parse(ddlEmpresa.SelectedValue), 0, int.Parse(ddlClientes.SelectedValue));


                 List<vPrefacturaPorPagar> lista;

                 {
                     lista = ventas.ToList();
                 }

                 foreach (var x in lista)
                 {
                     x.SaldoAnteriorPago = Decimal.Round((decimal)x.SaldoAnteriorPago, 2);
                 }

                 ViewState["pagosPendientes"] = lista;

                 this.gvPagos.DataSource = lista;
                 this.gvPagos.DataBind();

             }

            
        }

        


        protected void ddlOrigenLineaEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            
        }

        

        protected void ddlOrigenEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
      
        }

        protected void btnConsultarMovimiento_Click(object sender, EventArgs e)
        {
            this.FillView();
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
             Session["Cantidad_de_Procesamientos"] = Convert.ToInt32(Session["Cantidad_de_Procesamientos"].ToString()) + 1;
             if (Session["Cantidad_de_Procesamientos"].ToString() == "1")
             {
                 this.GuardarFactura();
             }
         
        }

        protected void btnSubir_Click(object sender, EventArgs e)
        {



            if (archivoPagos.HasFile)//por si hay archivo
            {
                string nombreArchivo = archivoPagos.FileName;
                var archivo = this.archivoPagos.FileBytes;
                ViewState["NombreArchivo"] = nombreArchivo;
                ViewState["BytesArchivo"] = archivo;

                this.lblError.Text = "Imagen cargada correctamente";
               
            }

        }
        private bool ValidarFactura()
        {
            
            if ((ViewState["detalles"] as List<Datosdetalle>).Count == 0)
            {
                this.lblError.Text = "La factura no puede estar vacía";
                return false;
            }

         //string nombreArchivo = ViewState["NombreArchivo"] as string;
         
         //   if (string.IsNullOrEmpty(nombreArchivo))//por si hay archivo
         //   {
         //       this.lblError.Text = "Falta cargar la imagen";
         //       return false;
         //   }
            //if (string.IsNullOrEmpty(this.txtFolio.Text))
            //{
            //    this.lblError.Text = "Escribe el folio de la factura";
            //    return false;
            //}
            /*
            List<Pagos> pagos = ViewState["Pagos"] as List<Pagos>;
            if (pagos == null)
            {
                this.lblError.Text = "Escribe los pagos";
                return false;

            }
            if (pagos.Count < 1)
            {
                this.lblError.Text = "Escribe los pagos";
                return false;

            }
             */
           

            return true;
        }
        //-------------------------------------------

        private void GuardarFactura()
        {
            AgregarConceptoUnico();

            bool error = false;
            if (ValidarFactura())
            {
                var detalles = ViewState["detalles"] as List<Datosdetalle>;
                var iniciales = Session["iniciales"] as string;
                var fact = GetFactura(iniciales, detalles);
                try
                {
                    string idPrefacturaString = "";
                        GAFFactura pre = new GAFFactura();
                        //Prefactura pre = new Prefactura();
                        using (pre as IDisposable)
                        {
                            string s = ""; 
                            var idx= ViewState["idPrefacturaStringxx"];
                            if (idx != null)
                            { idPrefacturaString = idx.ToString(); }
                           // string idPrefacturaString = this.Request.QueryString["idPrefactura"];
                            if (!string.IsNullOrEmpty(idPrefacturaString))
                                s = pre.GuardarPrefactura(fact, Convert.ToInt64(idPrefacturaString), llenarComplementoPagos());
                            else
                                s = pre.GuardarPrefactura(fact, 0, llenarComplementoPagos());
                            if (s != "OK")
                            {
                                error = true;
                                this.lblError.Text = s;
                            }
                            else
                                this.ClearAll();
                        }
                    



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
                    this.lblError.Text = "Error al generar el comprobante:" + ae.Message;
                }
                if (!error)
                {
                    this.lblError.Text = "Comprobante generado correctamente" + " <B>Prefolio:" + fact.PreFolio + "</B>";
                   // ActualizarSaldosMaster();
                }
                // this.lblError.Text = string.Empty;
            }

            Session["Cantidad_de_Procesamientos"] = 0;
        }

        private facturaComplementos llenarComplementoPagos()
        {
            try
            {
                facturaComplementos comple = new facturaComplementos();
                //List<Pagos> pagos = ViewState["Pagos"] as List<Pagos>;
                List<PagoDoctoRelacionado> documentos = ViewState["PagoDoctoRelacionado"] as List<PagoDoctoRelacionado>;
                /*
                foreach (var x in pagos)
                {
                    x.DoctoRelacionado = null;
                    foreach (var d in documentos)
                    {
                        if (x.id == d.ID)
                        {
                            if (x.DoctoRelacionado == null)
                                x.DoctoRelacionado = new List<PagoDoctoRelacionado>();
                            x.DoctoRelacionado.Add(d);
                        }
                    }
                }
                */

                List<Pagos> pagos = new List<Pagos>();
                Pagos p = new Pagos();
                p.fechaPago = txtFechaPagoP.Text;
                DateTime s = Convert.ToDateTime(p.fechaPago);
                TimeSpan ts = new TimeSpan(12, 0, 0);
                s = s.Date + ts;
                p.fechaPago = s.ToString();

                p.formaDePagoP = ddlFormaPagoP.SelectedValue;
                p.monedaP = ddlMonedaP.SelectedValue;
                p.monto = txtMonto.Text;
                if (!string.IsNullOrEmpty(txtTipoCambioP.Text))
                p.tipoCambioP = txtTipoCambioP.Text;
                p.rutaImagen = ViewState["NombreArchivo"] as string;
                pagos.Add(p);

                comple.pagos = pagos;
                return comple;
            }

            catch (Exception ae)
            {
                return null;
            }
        }


        private void ClearAll()
        {
           // this.Clear();
            ViewState["PrefolioPago"]=null;
            ViewState["FolioPrePago"]=null;
           
            txtFechaPagoP.Text = "";
            txtMonto.Text = "";
            txtTipoCambioP.Text = "";
            var detalles = new List<Datosdetalle>();
            ViewState["detalles"] = detalles;
            ddlMonedaP.SelectedValue = "MXN";
            txtTipoCambioP.Visible = false;
            var Impuestos = new List<DatosdetalleRT>();
            ViewState["detallesImpuestos"] = Impuestos;//para impuestos
            var Relacionado = new List<string>();
            ViewState["CfdiRelacionado"] = Relacionado;
            var DPagos = new List<DatosPagos>();
            ViewState["DPagos"] = DPagos;
            var Pagos = new List<Pagos>();
            ViewState["Pagos"] = Pagos;
            var PagoDocto = new List<PagoDoctoRelacionado>();
            ViewState["PagoDoctoRelacionado"] = PagoDocto;
            List<vPrefacturaPorPagar> lista = new List<vPrefacturaPorPagar>();
            ViewState["pagosPendientes"] = lista;

            ViewState["NombreArchivo"] = "";
            ViewState["BytesArchivo"] = "";
          //  BindPagosToGridView();
         

            //  cbImpuestos.Checked = false;
            //cbCfdiRelacionados.Checked = false;
        }
        private DatosPrefactura GetFactura(string iniciales, List<Datosdetalle> detalles)
        {
            var sesion = Session["sessionRGV"] as Sesion;


            DatosPrefactura fact = new DatosPrefactura();
            fact.CFDI = "P";

            fact.TipoDocumento = TipoDocumento.ComplementoPagos;
            fact.IdEmpresa = int.Parse(this.ddlEmpresa.SelectedValue);
            fact.Importe = decimal.Parse("0.00", NumberStyles.Currency);
            fact.SubTotal = decimal.Parse("0.00", NumberStyles.Currency);
            fact.Total = decimal.Parse("0.00", NumberStyles.Currency);
            fact.MonedaID = "XXX";//this.ddlMoneda.SelectedValue;
            fact.idcliente = int.Parse(this.ddlClientes.SelectedValue);
           // fact.Fecha = DateTime.Now;

            //fact.Folio = this.txtFolio.Text.PadLeft(4, '0');
            var prefol = ViewState["PrefolioPago"];
            var fol = ViewState["FolioPrePago"];
            if (prefol == null)
            {
                var prefactu = new Prefactura();
                using (prefactu as IDisposable)
                {
                    fact.Folio = prefactu.GetNextFolio(Convert.ToInt16(this.ddlEmpresa.SelectedValue));
                    fact.PreFolio = prefactu.GetNextPreFolio("P", 0);
                }
            }
            else
            {
                fact.Folio = fol.ToString();
                fact.PreFolio = prefol.ToString();
            }
            // fact.Serie = string.IsNullOrEmpty(this.txtSerie.Text) ? null : this.txtSerie.Text;
            // nProducto = detalles.Count;
            fact.captura = DateTime.Now;
            fact.Cancelado = 0;
            fact.Usuario = sesion.Id;//Guid.Parse("33760C0C-E45C-4210-8081-81C80827FA73");// System.Guid.NewGuid(); ///cambiar al verdadero
            // fact.LugarExpedicion = this.ddlSucursales.SelectedValue;
            // fact.Proyecto = this.txtProyecto.Text;
            fact.MonedaS = "XXX"; //this.ddlMoneda.SelectedItem.Text;
            fact.UsoCFDI = "P01";// se define para complemento de pago
            fact.Estatus = 0;//Convert.ToInt16( ddlStatusFactura.SelectedValue);
            fact.TipoDeComprobante = "Pago";

            var Empresa = new GAFEmpresa();
            using (Empresa as IDisposable)
            {
                var emp = Empresa.GetById(Convert.ToInt16(this.ddlEmpresa.SelectedValue));
                fact.LugarExpedicion = emp.CP;
            }
            fact.Fecha = DateTime.Now;

            var descuento = ViewState["descuento"];
            if (descuento != null)
                fact.Descuento = Convert.ToDecimal(descuento);

           
            if (detalles != null)
                if (detalles.Count > 0)
                    fact.Detalles = detalles;

            List<DatosPagos> DPagos = ViewState["DPagos"] as List<DatosPagos>;
            /*DatosPagos DP = new DatosPagos();
            DP.Tipo = "I";
            DP.Descripcion = txtObservacionesComi.Text;
            DP.Fecha = Convert.ToDateTime(txtFechaComi.Text);
            DP.MetododePago = ddlDPMetodoPagoComi.SelectedValue;
            DP.Monto = Convert.ToDecimal(txtMontoComi.Text);
            if (!string.IsNullOrEmpty(txtClabeComi.Text))
            {
                DP.Banco = ddlBancoComi.SelectedValue;
                DP.ClaveBancaria = txtClabeComi.Text;
            }
            DPagos.Add(DP);
            ViewState["DPagos"] = DPagos;
           */

            if (DPagos != null)
                if (DPagos.Count > 0)
                    fact.Datospagos = DPagos;


            string nombreArchivo = ViewState["NombreArchivo"] as string;
            var archivo = ViewState["BytesArchivo"] as byte[];

            if (!string.IsNullOrEmpty(nombreArchivo))//por si hay archivo
            {
                // string nombreArchivo = archivoPagos.FileName;
                // var archivo = this.archivoPagos.FileBytes;
                string ruta = ConfigurationManager.AppSettings["RutaPago"];

                DateTime localDate = DateTime.Now;
                string fecha = localDate.Year.ToString() + localDate.Month.ToString() + localDate.Day.ToString() + localDate.Hour.ToString() + localDate.Minute.ToString();
                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);
                nombreArchivo = fecha + nombreArchivo;
                ruta = Path.Combine(ruta, nombreArchivo);
                File.WriteAllBytes(ruta, archivo);
                ViewState["NombreArchivo"] = ruta;

            }
                 

            return fact;

        }

        protected void ddlFormaPagoP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlMonedaP_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperacionesCatalogos OP = new OperacionesCatalogos();
            using (OP as IDisposable)
            {


                if (this.ddlMonedaP.SelectedValue != "MXN")
                {
                    this.txtTipoCambioP.Visible = true;
                    this.lblTipoCambioP.Visible = true;

                    CatalogosSAT.Divisas D = OP.Consultar_TipoDivisa(this.ddlMonedaP.SelectedValue);
                    if (D != null)
                        txtTipoCambioP.Text = D.PesosDivisa.ToString();
                    else
                        txtTipoCambioP.Text = "";


                }
                else
                {
                    txtTipoCambioP.Text = "";

                    this.txtTipoCambioP.Visible = false;
                    this.lblTipoCambioP.Visible = false;
                }
            }
        }

        protected void btnAgregarPagos_Click(object sender, EventArgs e)
        {
            List<Pagos> pagos = ViewState["Pagos"] as List<Pagos>;
            int t = pagos.Count();

            Pagos p = new Pagos();
            p.id = (t + 1).ToString();
            //p.cadPago = txtCadPago.Text;
            //p.certPago = txtCertPago.Text;
          //  p.ctaBeneficiario = txtCtaBeneficiario.Text;
          //  p.ctaOrdenante = txtCtaOrdenante.Text;
            p.fechaPago = txtFechaPagoP.Text;
            p.formaDePagoP = ddlFormaPagoP.SelectedValue;
            p.monedaP = ddlMonedaP.SelectedValue;
            p.monto = txtMonto.Text;
          //  p.nomBancoOrdExt = txtNomBancoOrdExt.Text;
          //  p.numOperacion = txtNumOperacion.Text;
          //  p.rfcEmisorCtaBen = txtRfcEmisorCtaBen.Text;
          //  p.rfcEmisorCtaOrd = txtRfcEmisorCtaOrd.Text;
            // p.selloPago = txtSelloPago.Text;
         //   p.tipoCadPago = txtTipoCadPago.Text;
            p.tipoCambioP = txtTipoCambioP.Text;

          //  ddlID.Items.Add(p.id);

            pagos.Add(p);
            ViewState["Pagos"] = pagos;
            //BindPagosToGridView();

            txtFechaPagoP.Text = "";
            txtMonto.Text = "";
          //  txtRfcEmisorCtaOrd.Text = "";
           // txtCtaOrdenante.Text = "";
          //  txtCtaBeneficiario.Text = "";
            // txtCertPago.Text = "";
            // txtSelloPago.Text = "";
            txtTipoCambioP.Text = "";
           // txtNumOperacion.Text = "";
           // txtNomBancoOrdExt.Text = "";
           // txtRfcEmisorCtaBen.Text = "";
           // txtTipoCadPago.Text = "";
            // txtCadPago.Text = "";
       
        }
        /*
        private void BindPagosToGridView()
        {
            List<Pagos> pagos = ViewState["Pagos"] as List<Pagos>;

            if (pagos != null && pagos.Count > 0)
            {
                int noColumns = this.gvPagos.Columns.Count;
                this.gvPagos.Columns[noColumns - 1].Visible = this.gvPagos.Columns[noColumns - 2].Visible = true;
            }
            else
            {
                int noColumns = this.gvPagos.Columns.Count;
                this.gvPagos.Columns[noColumns - 1].Visible = this.gvPagos.Columns[noColumns - 2].Visible = false;
            }


            this.gvPagos.DataSource = pagos;
            this.gvPagos.DataBind();
        }
        */

        protected void gvPagos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("EliminarPago"))
            {
                var pagos = ViewState["Pagos"] as List<Pagos>;
                var documentos = ViewState["PagoDoctoRelacionado"] as List<PagoDoctoRelacionado>;
                Pagos dt = pagos.ElementAt(Convert.ToInt32(e.CommandArgument));
                string x = dt.id;
                pagos.RemoveAt(Convert.ToInt32(e.CommandArgument));
                //ddlID.Items.Remove(x);
                ViewState["Pagos"] = pagos;
                //BindPagosToGridView();
                /*
                documentos.RemoveAll(p => p.ID == x);
                ViewState["PagoDoctoRelacionado"] = documentos;
               
                this.BindDocumentosToGridView();
                UpdateTotales();
                 */ 
            }
        }

        protected void gvPagos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void AgregarConceptoUnico()
        {
            ViewState["detalles"] = new List<Datosdetalle>();
            List<Datosdetalle> detalles = ViewState["detalles"] as List<Datosdetalle>;

            Datosdetalle detalle = new Datosdetalle()
            {
                Partida = detalles.Count + 1,
                ConceptoCantidad = 1,
                ConceptoDescripcion = "Pago",
                ConceptoClaveProdServ = "84111506",
                ConceptoValorUnitario = 0,
                ConceptoImporte = 0,
                Conceptoidproducto = 0,
                ConceptoClaveUnidad = "ACT"

            };

            detalles.Add(detalle);
            ViewState["detalles"] = detalles;
        }

        protected void gvPagos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvPagos_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            this.FillView();
        }

        protected void ddlGiro_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Empresa = new GAFEmpresa();
            using (Empresa as IDisposable)
            {
                this.ddlEmpresa.Items.Clear();
                this.ddlEmpresa.DataSource = Empresa.GetListGiroempresaBy(Convert.ToInt64(ddlGiro.SelectedValue));
                this.ddlEmpresa.DataBind();
               

            }
           

        }
        //----------------


        private int Editar(long id)
        {
            var pre = new Prefactura();

            using (pre as IDisposable)
            {
                var fact = pre.GetPreFacturaInsert(id);

                if (fact == null)
                    return 0;

                else
                {

                    var Empresa = new GAFEmpresa();
                    using (Empresa as IDisposable)
                    {
                        var em = Empresa.GetById(fact.IdEmpresa);
                        ddlGiro.SelectedValue = em.Id_Giro.ToString();
                        this.ddlEmpresa.Items.Clear();
                        this.ddlEmpresa.DataSource = Empresa.GetListGiroempresaBy(Convert.ToInt64(ddlGiro.SelectedValue));
                        this.ddlEmpresa.DataBind();
                    }
                    ddlEmpresa.SelectedValue = fact.IdEmpresa.ToString();
                    if (ddlEmpresa.Items.Count > 0)
                    {
                        var cliente = new GAFClientes();
                        using (cliente as IDisposable)
                        {
                            var sesion = Session["sessionRGV"] as Sesion;

                            ddlClientes.Items.Clear();
                            var clientes = cliente.GetListClientePromotor(sesion.Id);
                            this.ddlClientes.DataSource = clientes;
                            this.ddlClientes.DataBind();
                            ddlClientes.SelectedValue = fact.idcliente.ToString();

                        }

          
                    }
                    var pa = pre.GetPreComplementoPagos(id);

                    if (pa != null)
                        if(pa.Count>0)
                    {
                        txtMonto.Text = pa[0].Monto.ToString();
                        txtFechaPagoP.Text = pa[0].FechaPago.ToString();
                        ddlFormaPagoP.SelectedValue = pa[0].FormaDePagoP;
                        ddlMonedaP.SelectedValue = pa[0].MonedaP;
                        if (pa[0].TipoCambioP != null)
                        {
                            txtTipoCambioP.Visible = true;
                            txtTipoCambioP.Text = pa[0].TipoCambioP.ToString();
                        }
                    }
                    ViewState["PrefolioPago"]= fact.PreFolio;
                    ViewState["FolioPrePago"] = fact.Folio;
                    
                    return 1;
                }
            }

        }

        protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}