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
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using DobleClip;
namespace GAFWEB
{
    public partial class wfrPagos : System.Web.UI.Page
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            string clientScript = ClientScript.GetPostBackEventReference(btnGenerar, null);
            Botones botonRGV = new Botones();
            botonRGV.PrevenirDoubleEnvio(btnGenerar, clientScript, "Confirma que deseas generar el pago");
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
       //     btnGenerar.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnGenerar, null) + ";");
            
              if (!this.IsPostBack)
            {
                Session["Cantidad_de_Procesamientos"] = 0;

               
                RelacionadosDIV.Style["display"] = "none";

                string id = Request.QueryString["id"];
                 long ID=0;

                 if (!string.IsNullOrEmpty(id) && long.TryParse(id, out ID))
                 {

                       List<Catalogo_Giros> giro = new List<Catalogo_Giros>();
               
                  var Empresa = new GAFEmpresa();
                  using (Empresa as IDisposable)
                  {
                      //giro = Empresa.GetListGiro();
                      // ddlGiro.DataSource = giro;
                      // ddlGiro.DataBind();

                      this.ddlEmpresa.Items.Clear();
                      // this.ddlEmpresa.DataSource = Empresa.GetListGiroempresaBy(Convert.ToInt64(ddlGiro.SelectedValue));
                      // this.ddlEmpresa.DataSource = Empresa.GetListempresa();
                      this.ddlEmpresa.DataSource = Empresa.GetListForLine(ddlLinea.SelectedValue);
                      this.ddlEmpresa.DataBind();

                      int idEmpresa = Convert.ToInt16(ddlEmpresa.SelectedValue);
                      if (ddlEmpresa.Items.Count > 0)
                      {
                          var cliente = new GAFClientes();
                          using (cliente as IDisposable)
                          {
                              ddlClientes.Items.Clear();
                              // var clientes = cliente.GetList();
                              var clientes = cliente.GetListLinea(ddlLineaCliente.SelectedValue);
                              this.ddlClientes.DataSource = clientes;
                              this.ddlClientes.DataBind();
                          }
                          var prefactu = new Prefactura();
                          using (prefactu as IDisposable)
                          {
                              this.txtPreFolio.Text = prefactu.GetNextFolio(idEmpresa);
                          }

                         
                      }
                    

                  }

                    
                    
                  
                     this.FillView();

                        OperacionesCatalogos OP=new OperacionesCatalogos();
                        using (OP as IDisposable)
                        {
                            ddlMonedaDR.DataSource = OP.Consultar_MonedaAll();
                            ddlMonedaDR.DataTextField = "Descripción";
                            ddlMonedaDR.DataValueField = "c_Moneda1";
                            ddlMonedaDR.DataBind();
                            ddlMonedaDR.SelectedValue = "MXN";
                            ddlMonedaP.DataSource = ddlMonedaDR.DataSource;
                            ddlMonedaP.DataTextField = "Descripción";
                            ddlMonedaP.DataValueField = "c_Moneda1";
                            ddlMonedaP.DataBind();
                            ddlMonedaP.SelectedValue = "MXN";
                      
                        }
                 }
            }
        }
        /*
        private void PreventingDoubleSubmit(Button button, string clientScript)
        {
           
            StringBuilder sb = new StringBuilder();
            sb.Append("if(confirm('Confirma que deseas generar el pago')){");
            sb.Append("if (typeof(Page_ClientValidate) == ' ') { ");
            sb.Append("var oldPage_IsValid = Page_IsValid; var oldPage_BlockSubmit = Page_BlockSubmit;");
            sb.Append("if (Page_ClientValidate('" + button.ValidationGroup + "') == false) {");
            sb.Append(" Page_IsValid = oldPage_IsValid; Page_BlockSubmit = oldPage_BlockSubmit; return false; }} ");
            sb.Append("this.value = 'Procesando...';");
            sb.Append("this.disabled = true;");
            sb.Append(clientScript + ";");
            sb.Append("return true;");
            sb.Append("} return false;");
            string submit_Button_onclick_js = sb.ToString();
            button.Attributes.Add("onclick",  submit_Button_onclick_js);
            
        }
         */ 

        private void FillView()
        {
            ViewState["TotalComplementoPago"] = "0";


            var factu = new Prefactura();
            var Empresa = new GAFEmpresa();
            var cliente = new GAFClientes();

            ViewState["PARCIALIDAD"] = new List<DatosParcialidad>();
            ViewState["IDPrefactura"] = ID;
            ViewState["PAGOSXXX"] = new List<DatosPagoComplemento>();
            ViewState["PagoDoctoRelacionado"] = new List<PagoDoctoRelacionado>();
            ViewState["pagosPendientes"] = null;
            BindDocumentosToGridView();
            BindPagosParcialidadToGridView();
             using (factu as IDisposable)
            {

                //var fac=factu.GetPreFacturaID(ID);
                 int idEmpresa = Convert.ToInt16(ddlEmpresa.SelectedValue);
                 var empresa = Empresa.GetById(idEmpresa);//(int)fac.idEmpresa);

                 var prefactu = new Prefactura();
                 using (prefactu as IDisposable)
                 {
                    txtPreFolio.Text = prefactu.GetNextFolio(idEmpresa);
                 }
       
                 if (!string.IsNullOrEmpty(ddlClientes.SelectedValue))
                 {
                     int idCliente = Convert.ToInt16(ddlClientes.SelectedValue);
                     var clien = cliente.GetCliente(idCliente);//(int)fac.idCliente);


                     //var pagos =factu.GetPreFacturaPagosID(ID);
                     ViewState["RFCEmpresa"] = empresa.RFC;
                     ViewState["RFCliente"] = clien.RFC;
                     //  ddlEmpresa.SelectedValue = empresa.RazonSocial;
                     // ddlClientes.SelectedValue = clien.RazonSocial;
                     //  txtPreFolio.Text = fac.PreFolio;
                     txtMonto.Text = "0.0";//Decimal.Round(pagos.Monto,2).ToString();
                     ViewState["MontoTotal"] = txtMonto.Text;

                     var ventas = factu.GetListPagoPorPagarContabilidad(empresa.IdEmpresa, 0, clien.idCliente);


                     List<vfacturasPorPagar> lista;

                     {
                         lista = ventas.ToList();
                     }

                     foreach (var x in lista)
                     {
                         x.SaldoAnteriorPago = Decimal.Round((decimal)x.SaldoAnteriorPago, 2);
                     }

                     //lista = null;

                     ViewState["pagosPendientes"] = lista;

                     this.gvPagos.DataSource = lista;
                     this.gvPagos.DataBind();
                 }
                 else
                 {
                     this.gvPagos.DataSource = null;
                     this.gvPagos.DataBind();
   
                 }

                /*
                if (lista == null||lista.Count==0)
                {
                    RelacionadosDIV.Style["display"] = "block";
                  
                   // DivDoctoRelacionado.Visible = true;
                   // Panel5.Visible = true;
                }
                else
                {
                   
                    RelacionadosDIV.Style["display"] = "none";
           
                   // DivDoctoRelacionado.Visible = false;
                   // Panel5.Visible = false;
                }
                 */ 
            }
        }

        
        protected void ddlOrigenLineaEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            
        }
             

        protected void ddlOrigenEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
      
        }

        //protected void btnConsultarMovimiento_Click(object sender, EventArgs e)
        //{
        //    this.FillView();
        //}

        protected void btnGenerar_Click(object sender, EventArgs e)
        {

           

                Session["Cantidad_de_Procesamientos"] = Convert.ToInt32(Session["Cantidad_de_Procesamientos"].ToString()) + 1;
                if (Session["Cantidad_de_Procesamientos"].ToString() == "1")
                {
                   // txtPreFolio.Text = Session["Cantidad_de_Procesamientos"].ToString();
                    //btnGenerar.Enabled = false;

                   // System.Threading.Thread.Sleep(10000);
                    this.GuardarFactura();
                    

                    //btnGenerar.Enabled = true;
                   
                    //btnGenerar.Visible = false;
                    //Button1.Visible = true;
                }
              // else
              //     txtMonto.Text = Session["Cantidad_de_Procesamientos"].ToString();
            


        }
        public string getRidOfUnprintablesAndUnicode(string inpString)
        {
            string outputs = String.Empty;

            for (int jj = 0; jj < inpString.Length; jj++)
            {
                char ch = inpString[jj];
                if (((int)(byte)ch) >= 32 & ((int)(byte)ch) <= 128)
                {
                    outputs += ch;
                }
            }
            return outputs;
        }   
        protected void btnSubir_Click(object sender, EventArgs e)
        {
            try
            {
                if (archivoPagos.HasFile)//por si hay archivo
                {
                    string nombreArchivo = archivoPagos.FileName;
                    var archivo = this.archivoPagos.FileBytes;
                    //ViewState["NombreArchivo"] = nombreArchivo;
                    //ViewState["BytesArchivo"] = archivo;

                    // string ruta = ConfigurationManager.AppSettings["RutaPago"];
                    string CFDI = Encoding.UTF8.GetString(archivo);
                    CFDI = getRidOfUnprintablesAndUnicode(CFDI);
                    XElement element = XElement.Load((new StringReader(CFDI)));
                    GeneradorCfdi.CFDI32.Comprobante comp = DesSerializar(element);

                    if (comp != null)
                    {

                        if (comp.version != "3.2")
                        {
                            this.lblError.Text = "CFDI no tiene la version 3.2";
                            return;
                        }
                        
                        string RFCEmpresa = ViewState["RFCEmpresa"].ToString();
                        string RFCCliente = ViewState["RFCliente"].ToString();
                        
                        if (comp.Emisor.rfc != RFCEmpresa)
                        {
                            this.lblError.Text = "CFDI no es del emisor";
                            return;
                        }
                        if (comp.Receptor.rfc != RFCCliente)
                        {
                            this.lblError.Text = "CFDI no es del receptor";
                            return;
                        }
                         
                        XNamespace cfdi = @"http://www.sat.gob.mx/cfd/3";
                        XNamespace tfd = @"http://www.sat.gob.mx/TimbreFiscalDigital";
                        var xdoc = XDocument.Parse(CFDI);
                        var elt = xdoc.Element(cfdi + "Comprobante").Element(cfdi + "Complemento").Element(tfd + "TimbreFiscalDigital");
                        var uuid = (string)elt.Attribute("UUID");
                        var date = (DateTime)elt.Attribute("FechaTimbrado");

                        txtFolioD.Text = comp.folio;
                        txtIdDocumento.Text = uuid;
                        ddlMetodoDePagoDR.SelectedValue = "PPD";
                        ddlMonedaDR.SelectedValue = "MXN";
                        txtNumParcialidad.Text = "1";

                  
                    }
                    //if (!Directory.Exists(ruta))
                    //    Directory.CreateDirectory(ruta);

                    //ruta = Path.Combine(ruta, nombreArchivo);
                    //File.WriteAllBytes(ruta, archivo);

                    

                }
            }
            catch (Exception ae)
            {
                this.lblError.Text = "XML no se cargado correctamente";
            }
        }
        private bool ValidarFactura()
        {
            
            if ((ViewState["detalles"] as List<Datosdetalle>).Count == 0)
            {
                this.lblError.Text = "La factura no puede estar vacía";
                return false;
            }
            if (string.IsNullOrEmpty(txtMonto.Text))
            {
                this.lblError.Text = "Favor de relacionar una factura, el monto debe ser cero";
                return false;
            }
            if (Convert.ToDecimal( txtMonto.Text)>0)
            {
                this.lblError.Text = "Favor de relacionar una factura, el monto debe ser cero";
                return false;
            
            }

            /*
            decimal d= Convert.ToDecimal(txtMonto.Text);
            if (d > 0)
            {
                this.lblError.Text = "El monto del pago debe estar en cero";
                return false;
            }
            */

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
        private DatosPrefactura GetFactura(string iniciales, List<Datosdetalle> detalles)
        {
            var sesion = Session["sessionRGV"] as Sesion;
          //  string Id = ViewState["IDPrefactura"] as string;

           // string Id = this.Request.QueryString["id"];
           // ViewState["IDPrefactura"]=Id;            
            var pre = new Prefactura();

            DatosPrefactura fact = new DatosPrefactura();
            //DatosPrefactura fact2 = new DatosPrefactura();
            /*
              using (pre as IDisposable)
            {
                 fact= pre.GetPreFacturaInsert(Convert.ToInt64( Id));

            }
         */

            fact.TipoDocumento = TipoDocumento.ComplementoPagos;
       
            fact.CFDI = "P";
            fact.idcliente =Convert.ToInt16( ddlClientes.SelectedValue);
            fact.IdEmpresa =Convert.ToInt16(  ddlEmpresa.SelectedValue);
            fact.TipoDocumento = TipoDocumento.ComplementoPagos;
            //fact.IdEmpresa = int.Parse(this.ddlEmpresa.SelectedValue);
            fact.Importe = decimal.Parse("0.00", NumberStyles.Currency);
            fact.SubTotal = decimal.Parse("0.00", NumberStyles.Currency);
            fact.Total = decimal.Parse("0.00", NumberStyles.Currency);
            fact.MonedaID = "XXX";//this.ddlMoneda.SelectedValue;
           // fact.idcliente = int.Parse(this.ddlClientes.SelectedValue);
           // fact.Fecha =Convert.ToDateTime( txtFechaPagoP.Text);

            fact.Usuario = sesion.Id;
           // fact.captura = DateTime.Now;
            fact.Cancelado = 0;
            //fact.Usuario = sesion.Id;//Guid.Parse("33760C0C-E45C-4210-8081-81C80827FA73");// System.Guid.NewGuid(); ///cambiar al verdadero
            fact.MonedaS = "XXX"; //this.ddlMoneda.SelectedItem.Text;
            fact.UsoCFDI = "CP01";// se define para complemento de pago
            fact.Estatus = 0;//Convert.ToInt16( ddlStatusFactura.SelectedValue);
            fact.TipoDeComprobante = "Pago";
            var prefactu = new Prefactura();
            using (prefactu as IDisposable)
            {
                fact.Folio = prefactu.GetNextFolio(fact.IdEmpresa);
            }
       

            fact.Fecha = DateTime.Now;
                      

            if (detalles != null)
                if (detalles.Count > 0)
                    fact.Detalles = detalles;

            var Empresa = new GAFEmpresa();
            using (Empresa as IDisposable)
            {
                var emp = Empresa.GetById(Convert.ToInt16(this.ddlEmpresa.SelectedValue));
                fact.LugarExpedicion = emp.CP;
            }

            return fact;

        }
        //------------------------------------------------------
        private facturaComplementos llenarComplementoPagos()
        {
            try
            {
                facturaComplementos comple = new facturaComplementos();
                //List<Pagos> pagos = ViewState["Pagos"] as List<Pagos>;
                List<PagoDoctoRelacionado> documentos =new List<PagoDoctoRelacionado>();
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

                string Id = ViewState["IDPrefactura"] as string;
                string nombreArchivo = ViewState["NombreArchivo"] as string;

                 var factu = new Prefactura();
                 var fac = new Factura();
                     
            // DatosPagos DP = new DatosPagos();
             using (factu as IDisposable)
             {
                /* var pag = factu.GetPreFacturaPagosID(Convert.ToInt64(Id));

                 List<Pagos> pagos = new List<Pagos>();
                 Pagos p = new Pagos();
                 p.fechaPago = pag.FechaPago.ToString();
                 p.formaDePagoP = pag.FormaDePagoP;
                 p.monedaP = pag.MonedaP;
                 p.monto = pag.Monto.ToString("F");
                 p.cadPago = nombreArchivo;
                
                // if (pag.TipoCambioP!=null)
                     p.tipoCambioP = pag.TipoCambioP.ToString();
                 */

                 string MT= ViewState["TotalComplementoPago"] as string;// esto es todo lo que se agrega en los pagos ---revisar


                 List<Pagos> pagos = new List<Pagos>();
                 Pagos p = new Pagos();
                 //DateTime F= DateTime.Now;
                 p.fechaPago = txtFechaPagoP.Text;
                 DateTime s = Convert.ToDateTime(p.fechaPago);
                 TimeSpan ts = new TimeSpan(12, 0, 0);
                 s = s.Date + ts;
                 p.fechaPago = s.ToString();

                 p.formaDePagoP = ddlFormaPagoP.SelectedValue;
                 p.monedaP = ddlMonedaP.SelectedValue;

                 string montoxxx = MT;
                 p.monto = montoxxx;
                 if ( !string.IsNullOrEmpty(txtTipoCambioP.Text))
                     p.tipoCambioP = txtTipoCambioP.Text;
               
                 List<PagoDoctoRelacionado> documentosxxx = ViewState["PagoDoctoRelacionado"] as List<PagoDoctoRelacionado>;
                 if (documentosxxx != null)/////version 3.2
                 {
                     foreach (var x in documentosxxx)
                     {
                          PagoDoctoRelacionado pa = new PagoDoctoRelacionado();
                         pa.IdDocumento = x.IdDocumento;
                         pa.MonedaDR = x.MonedaDR;
                         pa.Serie = x.Serie;
                         pa.Folio = x.Folio;
                         pa.NumParcialidad = x.NumParcialidad;
                         pa.MetodoDePagoDR = x.MetodoDePagoDR;
                         pa.ImpPagado = Convert.ToDecimal(x.ImpPagado.Replace("$","")).ToString("F");
                         pa.ImpSaldoAnt = Convert.ToDecimal(x.ImpSaldoAnt.Replace("$", "")).ToString("F");
                         pa.ImpSaldoInsoluto = Convert.ToDecimal(x.ImpSaldoInsoluto.Replace("$", "")).ToString("F");

                         documentos.Add(pa);
                     }
                     //if (documentos != null)
                     //    if (documentos.Count > 0)
                     //        p.DoctoRelacionado = documentos;
                 }
                
                     var PAGOXX = ViewState["PAGOSXXX"] as List<DatosPagoComplemento>;
                     if (PAGOXX != null)
                         foreach (var x in PAGOXX)
                         {
                             var f = fac.GetListDocumentosPAgosContabilidad(Convert.ToInt64(x.id));
                             PagoDoctoRelacionado pa = new PagoDoctoRelacionado();
                             pa.IdDocumento = f.Uid;
                             pa.MonedaDR = "MXN";//pag.MonedaP;
                             pa.Serie = f.Serie;
                             pa.Folio = f.Folio;
                             pa.NumParcialidad = x.parcialidad;
                             if(!string.IsNullOrEmpty(f.MetodoPago))
                                 pa.MetodoDePagoDR = f.MetodoPago;
                             else
                             pa.MetodoDePagoDR = "PPD";//f.MetodoPago;
                             pa.ImpPagado = Convert.ToDecimal(x.MontoPagado).ToString("F");
                             pa.ImpSaldoAnt = Convert.ToDecimal(x.MontoAnterior).ToString("F");
                             pa.ImpSaldoInsoluto = Convert.ToDecimal(x.SaldoInsoluto).ToString("F");

                             documentos.Add(pa);
                         }
                     if (documentos != null)
                         if (documentos.Count > 0)
                             p.DoctoRelacionado = documentos;
                 
                 pagos.Add(p);

                 comple.pagos = pagos;
             }
                return comple;
            }

            catch (Exception ae)
            {
                return null;
            }
        }

               
        private void GuardarFactura()
        {
            AgregarConceptoUnico();

            bool error = false;
            if (ValidarFactura())
            {
                var detalles = ViewState["detalles"] as List<Datosdetalle>;
                var iniciales = Session["iniciales"] as string;
                DatosPrefactura fact = GetFactura(iniciales, detalles);
                try
                {
                    
                        GAFFactura pre = new GAFFactura();
                        //Prefactura pre = new Prefactura();
                        using (pre as IDisposable)
                        {
                            string s = "OK";
                            
                            //string idPrefacturaString = this.Request.QueryString["id"];
                            string idPrefacturaString = "";
                            ViewState["IDPrefactura"] = idPrefacturaString;

                          
                                if (!pre.GuardarFactura33(fact, 0, true, llenarComplementoPagos()))
                                {
                                    s = "* Error al generar la factura";
                                }
                                else
                                    s = "OK";
                           
                             if (s != "OK")
                            {
                                error = true;
                                this.lblError.Text = s;
                            }
                            else
                            {
                                  var pref = new Prefactura();
                                string uudi = pre.Uuid;
                                long id = 0;
                                   using (pre as IDisposable)
                                    {
                                        var f=pref.GetFacturaUUDI(uudi);
                                        id = f.idVenta;
                                   }
                              
                                var PAGOXX = ViewState["PAGOSXXX"] as List<DatosPagoComplemento>;
                               
                                string cadenaId = "";
                                if(PAGOXX!=null)
                                foreach (var x in PAGOXX)
                                {
                                    if (!string.IsNullOrEmpty(cadenaId))
                                        cadenaId = cadenaId + "|" + x.id;
                                    else
                                        cadenaId = x.id;
                                    using (pre as IDisposable)
                                    {
                                        pref.CambiarMontoPagoContabilidad(Convert.ToInt64(x.id), Convert.ToDecimal(x.monto), Convert.ToInt16(x.parcialidad));

                                    }
                                }
                                 
                                  using (pre as IDisposable)
                                    {
                                       // pref.CambiarestatusPago(Convert.ToInt64(idPrefacturaString),2);//2 ya se pago
                                        pref.GuardarIDRelacionadosFactura(id, cadenaId);
                                    }
                                

                                this.ClearAll();

                            }
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
                    this.lblError.Text = "Comprobante generado correctamente";
                   // ActualizarSaldosMaster();
                }
                // this.lblError.Text = string.Empty;
            }
            Session["Cantidad_de_Procesamientos"] = 0;
        }

     

        private void ClearAll()
        {
           // this.Clear();

         
            var detalles = new List<Datosdetalle>();
            ViewState["detalles"] = detalles;

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
            BindPagosToGridView();
            ViewState["PagoDoctoRelacionado"] = new List<PagoDoctoRelacionado>();
            BindDocumentosToGridView();
            ViewState["PARCIALIDAD"] =new List<DatosParcialidad>();
            BindPagosParcialidadToGridView();
            //  cbImpuestos.Checked = false;
            //cbCfdiRelacionados.Checked = false;
        }
        
        protected void ddlFormaPagoP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       

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

        private void BindPagosParcialidadToGridView()
        {
            List<DatosParcialidad> PARCIALIDAD = ViewState["PARCIALIDAD"] as List<DatosParcialidad>;

            if (PARCIALIDAD != null && PARCIALIDAD.Count > 0)
            {
                int noColumns = this.gvPagosParcialidad.Columns.Count;
                this.gvPagosParcialidad.Columns[noColumns - 1].Visible = this.gvPagosParcialidad.Columns[noColumns - 2].Visible = true;
            }
            else
            {
                int noColumns = this.gvPagosParcialidad.Columns.Count;
                this.gvPagosParcialidad.Columns[noColumns - 1].Visible = this.gvPagosParcialidad.Columns[noColumns - 2].Visible = false;
            }


            this.gvPagosParcialidad.DataSource = PARCIALIDAD;
            this.gvPagosParcialidad.DataBind();
        }

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
                BindPagosToGridView();
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

        protected void gvPagos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPagos.EditIndex = e.NewEditIndex;
            this.gvPagos.DataSource = ViewState["pagosPendientes"];
            //this.gvPagos.PageIndex = e.NewPageIndex;
            this.gvPagos.DataBind();
            //gvbind(); 
        }

        protected void gvPagos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPagos.EditIndex = -1;
            this.gvPagos.DataSource = ViewState["pagosPendientes"];
            //this.gvPagos.PageIndex = e.NewPageIndex;
            this.gvPagos.DataBind();
        }

        protected void gvPagos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lblError.Text = "";
            gvPagos.EditIndex = -1;
            TextBox name = gvPagos.Rows[e.RowIndex].FindControl("txt_Monto") as TextBox;
            var id = this.gvPagos.DataKeys[Convert.ToInt32(e.RowIndex)].Values["idVenta"];
           long n=Convert.ToInt64( id);
            decimal monto =0;
           try
           {
               monto = Convert.ToDecimal(name.Text);
           }
           catch (Exception)
           {
               lblError.Text = "Dato invalido ";
           }
            if(monto>Convert.ToDecimal( txtMonto.Text))
                lblError.Text = "El monto asignado no puede ser mayor al monto total";

            List<vfacturasPorPagar> L = ViewState["pagosPendientes"] as List<vfacturasPorPagar>;
            decimal montoante = (decimal)L.First(x => x.idVenta == n).SaldoAnteriorPago;
            int parcialidad = (int)L.First(x => x.idVenta == n).Parcialidad;
            if( (montoante-monto)<0)
                lblError.Text = "El monto asignado no puede ser mayor al monto de la factura";

            if (string.IsNullOrEmpty(lblError.Text))
            {
                string MT = ViewState["TotalComplementoPago"] as string;
                if (MT == "0")
                    MT = txtMonto.Text;
                ViewState["TotalComplementoPago"] = MT;
                txtMonto.Text = (Convert.ToDecimal(txtMonto.Text) - monto).ToString();

                //  vprefactura l = L.Where(x => x.idPreFactura == n).FirstOrDefault();
                L.First(x => x.idVenta == n).SaldoAnteriorPago = montoante - monto;
                L.First(x => x.idVenta == n).Parcialidad = parcialidad + 1;

                ViewState["pagosPendientes"] = L;

                var PAGOXX=  ViewState["PAGOSXXX"] as List<DatosPagoComplemento>;
                DatosPagoComplemento DP = new DatosPagoComplemento();
                DP.id = id.ToString();
                DP.monto = (montoante - monto).ToString();
                DP.parcialidad = (parcialidad + 1).ToString();
                DP.MontoPagado = monto.ToString();
                DP.MontoAnterior = montoante.ToString();
                DP.SaldoInsoluto = DP.monto;
                PAGOXX.Add(DP);
                ViewState["PAGOSXXX"] = PAGOXX;

                
                List<DatosParcialidad> PARCIALIDAD = ViewState["PARCIALIDAD"] as List<DatosParcialidad>;
                DatosParcialidad P = new DatosParcialidad();
                P.MontoPagado = monto.ToString();
                P.Parcialidad = (parcialidad+ 1).ToString();
                P.PreFolio = this.gvPagos.Rows[Convert.ToInt32(e.RowIndex)].Cells[0].Text;
                P.SaldoAnteriorPago = montoante.ToString();
                P.idPreFactura = n.ToString();
                PARCIALIDAD.Add(P);
                ViewState["PARCIALIDAD"] = PARCIALIDAD;
                BindPagosParcialidadToGridView();

            }
                this.gvPagos.DataSource = ViewState["pagosPendientes"];
                //    this.gvPagos.PageIndex = e.NewPageIndex;
                this.gvPagos.DataBind();
               
            
            

        }

        protected void gvPagos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPagos.DataSource = ViewState["pagosPendientes"];
            this.gvPagos.PageIndex = e.NewPageIndex;
            this.gvPagos.DataBind();
       
        }

        protected void gvPagosParcialidad_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("EliminarPracialidad"))
            {
                List<vfacturasPorPagar> L = ViewState["pagosPendientes"] as List<vfacturasPorPagar>;
          
                List<DatosParcialidad> PARCIALIDAD = ViewState["PARCIALIDAD"] as List<DatosParcialidad>;
                DatosParcialidad dt = PARCIALIDAD.ElementAt(Convert.ToInt32(e.CommandArgument));
                PARCIALIDAD.RemoveAt(Convert.ToInt32(e.CommandArgument));
                foreach (var z in PARCIALIDAD)
                {
                    
                }

                ViewState["PARCIALIDAD"] = PARCIALIDAD;
                List<DatosPagoComplemento> PAGOXX = ViewState["PAGOSXXX"] as List<DatosPagoComplemento>;
                PAGOXX.RemoveAt(Convert.ToInt32(e.CommandArgument));
                DatosPagoComplemento P = new DatosPagoComplemento();
                //P=PAGOXX.Find(p => p.id == dt.idPreFactura);
                foreach (var x in PARCIALIDAD)
                {
                    if (x.idPreFactura == dt.idPreFactura)
                    {
                        if (Convert.ToInt16(x.Parcialidad) > Convert.ToInt16(dt.Parcialidad))
                            //x.MontoAnterior =(Convert.ToDecimal( x.MontoAnterior) + Convert.ToDecimal(dt.MontoPagado)).ToString();
                            x.Parcialidad = (Convert.ToInt16(x.Parcialidad) - 1).ToString();
                    }
                }
                
                foreach (var x in PAGOXX)
                {
                    if (x.id == dt.idPreFactura)
                    {
                        if (Convert.ToInt16(x.parcialidad) > Convert.ToInt16(dt.Parcialidad))
                        //x.MontoAnterior =(Convert.ToDecimal( x.MontoAnterior) + Convert.ToDecimal(dt.MontoPagado)).ToString();
                        x.parcialidad = (Convert.ToInt16( x.parcialidad) - 1).ToString();
                    }
                }
                foreach (var l in L)
                {
                    if (l.idVenta == Convert.ToInt64( dt.idPreFactura))
                    {    int n=1;
                         string a= ((decimal)l.SaldoAnteriorPago + Convert.ToDecimal(dt.MontoPagado)).ToString() ;
                         l.SaldoAnteriorPago = Convert.ToDecimal(a);
                         //if (l.Parcialidad > 2)
                         //    n = 2;
                        string q = (Convert.ToInt16(l.Parcialidad) - n).ToString();
                        l.Parcialidad = Convert.ToInt16(q);
                    }
                }

                txtMonto.Text = (Convert.ToDecimal(dt.MontoPagado) + Convert.ToDecimal(txtMonto.Text)).ToString();
                ViewState["pagosPendientes"] = L;

                 ViewState["PAGOSXXX"] = PAGOXX;

                 this.gvPagos.DataSource = ViewState["pagosPendientes"];
                 this.gvPagos.DataBind();
              
                BindPagosParcialidadToGridView();


            }
        }
        //----------------
        private void llenadoPracialidad(string folio,string montoPagar, string montoAnterior, string parcialidad)
        {
  
        
        }

        protected void gvPagosParcialidad_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvPagosParcialidad.DataSource = ViewState["PARCIALIDAD"];
            this.gvPagosParcialidad.PageIndex = e.NewPageIndex;
            this.gvPagosParcialidad.DataBind(); 
        }

        protected void btnOK1_Click(object sender, EventArgs e)
        {

        }

        protected void btncancel_Click(object sender, EventArgs e)
        {

        }
        
        protected void btnAgregarDocumento_Click(object sender, EventArgs e)
        {
            decimal monto = Convert.ToDecimal(txtImpPagado.Text);
            if (monto > Convert.ToDecimal(txtMonto.Text))
            {
                lblError.Text = "El monto asignado no puede ser mayor al monto total";
                return;
            }
            txtMonto.Text = (Convert.ToDecimal(txtMonto.Text) - monto).ToString();

            List<PagoDoctoRelacionado> documentos = ViewState["PagoDoctoRelacionado"] as List<PagoDoctoRelacionado>;


            PagoDoctoRelacionado Docum = new PagoDoctoRelacionado();
            Docum.Folio = txtFolioD.Text;
            Docum.IdDocumento = txtIdDocumento.Text;
            decimal ImpPagado = Convert.ToDecimal(txtImpPagado.Text);
            Docum.ImpPagado = ImpPagado.ToString("C");
            decimal impSalAnt =Convert.ToDecimal( txtImpSaldoAnt.Text);
            Docum.ImpSaldoAnt = impSalAnt.ToString("C");
            decimal SalInsoluto = Convert.ToDecimal(txtImpSaldoInsoluto.Text);
            Docum.ImpSaldoInsoluto = SalInsoluto.ToString("C");
             Docum.MetodoDePagoDR = ddlMetodoDePagoDR.SelectedValue;

            Docum.MonedaDR = ddlMonedaDR.SelectedValue;
            Docum.NumParcialidad = txtNumParcialidad.Text;
            Docum.ID = "1";
            documentos.Add(Docum);
            //      ViewState["Pagos"] = pagos;
            ViewState["PagoDoctoRelacionado"] = documentos;

            BindDocumentosToGridView();

            txtIdDocumento.Text = "";
            txtFolioD.Text = "";
            txtImpPagado.Text = "";
            txtImpSaldoAnt.Text = "";
            txtImpSaldoInsoluto.Text = "";
            txtNumParcialidad.Text = "";
        }
        
       private void BindDocumentosToGridView()
        {
            List<PagoDoctoRelacionado> documentos = ViewState["PagoDoctoRelacionado"] as List<PagoDoctoRelacionado>;


            if (documentos != null && documentos.Count > 0)
            {
                int noColumns = this.gvDocumento.Columns.Count;
                this.gvDocumento.Columns[noColumns - 1].Visible = this.gvDocumento.Columns[noColumns - 2].Visible = true;
            }
            else
            {
                int noColumns = this.gvDocumento.Columns.Count;
                this.gvDocumento.Columns[noColumns - 1].Visible = this.gvDocumento.Columns[noColumns - 2].Visible = false;
            }


            this.gvDocumento.DataSource = documentos;
            this.gvDocumento.DataBind();
        }
        
        //-------------------------------------------------------------------------------


       
        protected void gvDocumento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("EliminarDocumento"))
            {
                var documentos = ViewState["PagoDoctoRelacionado"] as List<PagoDoctoRelacionado>;
                var doc= documentos.ElementAt(Convert.ToInt32(e.CommandArgument));
                txtMonto.Text = (Convert.ToDecimal(txtMonto.Text) + Convert.ToDecimal(doc.ImpPagado)).ToString();
                documentos.RemoveAt(Convert.ToInt32(e.CommandArgument));
                ViewState["PagoDoctoRelacionado"] = documentos;
                this.BindDocumentosToGridView();

                //this.UpdateTotales();

            }

        }
         
        //protected void Button1_Click(object sender, EventArgs e)
        //{

        //    btnGenerar.Visible = true;
        //    Button1.Visible = false;
        //    btnGenerar_Click(null, null);
           
        //}


        private GeneradorCfdi.CFDI32.Comprobante DesSerializar(XElement element)
        {

            var ser = new XmlSerializer(typeof(GeneradorCfdi.CFDI32.Comprobante));
            string xml = element.ToString();
            var reader = new StringReader(xml);
            var comLXMLComprobante = (GeneradorCfdi.CFDI32.Comprobante)ser.Deserialize(reader);
            return comLXMLComprobante;
        }

        protected void ddlMonedaDR_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cbDoctoRelacionado_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDoctoRelacionado.Checked == true)
            {
            
              gvPagosParcialidad.Visible = false;
              gvPagos.Visible = false;
            //  var ID = ViewState["IDPrefactura"];
              ViewState["PAGOSXXX"] = new List<DatosPagoComplemento>();
              ViewState["PARCIALIDAD"] = new List<DatosParcialidad>();
              FillView();
              BindPagosParcialidadToGridView();

              RelacionadosDIV.Style["display"] = "block";
              txtMonto.Text = ViewState["MontoTotal"].ToString();

               //   DivDoctoRelacionado.Visible = true;
              //  Panel5.Visible = true;
            }
            else
            {
                gvPagosParcialidad.Visible = true;
                gvPagos.Visible = true;
                txtMonto.Text=ViewState["MontoTotal"].ToString();

                RelacionadosDIV.Style["display"] = "none";
               // DivDoctoRelacionado.Visible = false;
               // Panel5.Visible = false;
                
                ViewState["PagoDoctoRelacionado"] = new List<PagoDoctoRelacionado>();
                BindDocumentosToGridView();

            }
        }

        protected void ddlLinea_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            var Empresa = new GAFEmpresa();
            using (Empresa as IDisposable)
            {
                this.ddlEmpresa.Items.Clear();
                this.ddlEmpresa.DataSource = Empresa.GetListForLine(ddlLinea.SelectedValue);
                this.ddlEmpresa.DataBind();

            }
            int idEmpresa = Convert.ToInt16(ddlEmpresa.SelectedValue);

            if (ddlEmpresa.Items.Count > 0)
            {

                var prefactu = new Prefactura();
                using (prefactu as IDisposable)
                {
                    this.txtPreFolio.Text = prefactu.GetNextFolio(idEmpresa);
                }

            }

            FillView();
        }

        protected void ddlLineaCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            var cliente = new GAFClientes();
            using (cliente as IDisposable)
            {
                ddlClientes.Items.Clear();
                // var clientes = cliente.GetList();
                var clientes = cliente.GetListLinea(ddlLineaCliente.SelectedValue);
                //   var clientes = cliente.GetList(idEmpresa, string.Empty, false);
                this.ddlClientes.DataSource = clientes;
                this.ddlClientes.DataBind();
            }

            FillView();
             
        }

        protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idEmpresa = Convert.ToInt16(ddlEmpresa.SelectedValue);

            if (ddlEmpresa.Items.Count > 0)
            {

                var prefactu = new Prefactura();
                using (prefactu as IDisposable)
                {
                    this.txtPreFolio.Text = prefactu.GetNextFolio(idEmpresa);
                }

            }
            FillView();
        }

        protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillView();
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

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.FillView();
        }

    }
}