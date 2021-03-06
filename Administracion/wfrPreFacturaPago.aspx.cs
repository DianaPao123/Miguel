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
using GAFBusiness;
using GAFContract;
using System.Drawing;

namespace GAFWEB
{
    public partial class wfrPreFacturaPago : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!this.IsPostBack)
            {
                try
                {
                

                   // DivBanco.Visible = false;
                   // DivBancoComi.Visible = false;

                  // var perfil = Session["perfil"] as string;
                   //var sistema = Session["idSistema"] as long?;
                  var idEmp = Session["idEmpresa"] as int?;

                  var rol = Session["rol"];
                  rol = "Promotores";
                    //-------------------configuracion de rol---------------------------
                  if (rol == "Promotores")
                  {
                      ddlLinea.Visible = false;
                      lblLinea.Visible = false;
                      BtnVistaPrevia.Visible = false;
                  }
                  
                  

                    //-----------------------------------------

                  var Empresa = new GAFEmpresa();
                  using (Empresa as IDisposable)
                  {
                      this.ddlEmpresa.Items.Clear();
                      this.ddlEmpresa.DataSource = Empresa.GetListForLine(ddlLinea.SelectedValue);
                      this.ddlEmpresa.DataBind();
                    
                  int idEmpresa =Convert.ToInt16( ddlEmpresa.SelectedValue);
                      /*
                     if (!Empresa.TieneConfiguradoCertificado(idEmpresa))
                        {
                            this.lblError.Text = "Tienes que configurar tus certificados antes de poder facturar";
                            this.btnGenerarFactura.Enabled = this.BtnVistaPrevia.Enabled = this.btnBuscarProducto.Enabled = 
                                this.btnAgregar.Enabled = this.ddlMoneda.Enabled = false;
                            return;
                        }
                      */
                     if (ddlEmpresa.Items.Count > 0)
                        {
                           var cliente =new GAFClientes();
                           using (cliente as IDisposable)
                           {
                               ddlClientes.Items.Clear();
                               var clientes = cliente.GetList(idEmpresa, string.Empty, false);
                               this.ddlClientes.DataSource = clientes;
                               this.ddlClientes.DataBind();
                           }
                           var prefactu = new Prefactura();
                           using (prefactu as IDisposable)
                           {
                               this.txtFolio.Text = prefactu.GetNextFolio(idEmpresa);
                           }
                            ddlClientes_SelectedIndexChanged(null, null);
                        }
                     var Sucursal = new GAFSucursales();
             
                     //using (Sucursal as IDisposable)
                     //{
                     //    ddlSucursales.Items.Clear();
                     //    this.ddlSucursales.DataSource = Sucursal.GetSucursalLista(idEmpresa);
                     //    ddlSucursales.DataValueField = "LugarExpedicion";
                     //    ddlSucursales.DataTextField = "Nombre";
                     //    this.ddlSucursales.DataBind();
                     //}
                      
                     
                        //------------catalogos grandes----------------------
                      OperacionesCatalogos OP=new OperacionesCatalogos();
                      using (OP as IDisposable)
                      {

                        //  ddlClaveUnidad.DataSource = OP.ConsultarClaveUnidadMasUsadas();
                        // // ddlClaveUnidad.DataSource = OP.ConsultarClaveUnidadAll();
                        //  ddlClaveUnidad.DataTextField = "Nombre";
                        //  ddlClaveUnidad.DataValueField = "c_ClaveUnidad1";
                        //  ddlClaveUnidad.DataBind();
                  


                          ddlClaveUnidadE.DataSource = OP.ConsultarClaveUnidadMasUsadas();
                          ddlClaveUnidadE.DataTextField = "Nombre";
                          ddlClaveUnidadE.DataValueField = "c_ClaveUnidad1";
                          ddlClaveUnidadE.DataBind();
                          ddlClaveUnidadE.SelectedValue = "H87";
                          //--------------------------------------------
                         
                          ddlMonedaP.DataSource = OP.Consultar_MonedaAll();
                          ddlMonedaP.DataTextField = "Descripción";
                          ddlMonedaP.DataValueField = "c_Moneda1";
                          ddlMonedaP.DataBind();
                          ddlMonedaP.SelectedValue = "MXN";

                          ddlMonedaDR.DataSource = ddlMonedaP.DataSource;
                          ddlMonedaDR.DataTextField = "Descripción";
                          ddlMonedaDR.DataValueField = "c_Moneda1";
                          ddlMonedaDR.DataBind();
                          ddlMonedaDR.SelectedValue = "MXN";

                          CatalogosSAT.c_Moneda mone = OP.Consultar_Moneda("XXX");
                          ViewState["DecimalMoneda"] = mone.Decimales;
                          //-----------------------------------------
                         
                      }
                      
                    }
                 // ViewState["PrefacuturaDatos"] = new DatosPrefactura();
                    ViewState["detalles"] = new List<Datosdetalle>();
                    ViewState["detallesImpuestos"] = new List<DatosdetalleRT>();//para impuestos
                    ViewState["CfdiRelacionado"] = new List<string>();
                    ViewState["iva"] = 0M;
                    ViewState["total"] = 0M;
                    ViewState["subtotal"] = 0M;
                    ViewState["descuento"] = 0M;
                    ViewState["DPagos"] = new List<DatosPagos>();
                    ViewState["Pagos"] = new List<Pagos>();
                    ViewState["PagoDoctoRelacionado"] = new List<PagoDoctoRelacionado>();
               

                   this.BindDetallesToGridView();
                    this.UpdateTotales();
                    //--------------------------------para editar---------------------------------
                    string idPrefacturaString = this.Request.QueryString["idPrefactura"];
                    int idPrefactura;
                    if (!string.IsNullOrEmpty(idPrefacturaString) && int.TryParse(idPrefacturaString, out idPrefactura))
                    {
                        Editar(idPrefactura);
                    }
                    //-------------------------------fin edicion----------------------
                    else
                    { 

                     // MostrarDatosSegunEstatus(Convert.ToInt16( ddlStatusFactura.SelectedValue));
                    }
                }
                catch (Exception ex)
                {
                    //Logger.Error(ex.Message);
                }
            }
        }

        

        
        protected void btnGenerarFactura_Click(object sender, EventArgs e)
        {
            this.GuardarFactura();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            
            /*
            int idCliente = int.Parse(this.ddlClientes.SelectedValue);
            if (string.IsNullOrEmpty(this.txtCantidad.Text.Trim()) ||
               string.IsNullOrEmpty(this.txtPrecio.Text.Trim()) ||
               string.IsNullOrEmpty(this.txtDescripcion.Text.Trim()) )
            {
                this.lblError.Text = "* Error de validación en la partida";
                return;
            }
            this.lblError.Text = string.Empty;
           // ddlConceptos.Items.Add(this.txtCodigo.Text);

            List<Datosdetalle> detalles = ViewState["detalles"] as List<Datosdetalle>;
            string m = ViewState["DecimalMoneda"].ToString();
            int mon = 0;
            if (!string.IsNullOrEmpty(m))
                mon = Convert.ToInt16(m);


            Datosdetalle detalle = new Datosdetalle()
            {
                Partida = detalles.Count + 1,
                ConceptoCantidad = decimal.Parse(this.txtCantidad.Text),
                ConceptoDescripcion = this.txtDescripcion.Text,
                ConceptoClaveProdServ = this.txtCodigo.Text,
               // ConceptoUnidad = this.txtUnidad.Text,
                ConceptoValorUnitario = Decimal.Round(decimal.Parse(this.txtPrecio.Text), mon),
                ConceptoImporte = Decimal.Round(decimal.Parse(this.txtPrecio.Text) * decimal.Parse(this.txtCantidad.Text), mon),
                Conceptoidproducto = string.IsNullOrEmpty(txtIdProducto.Value) ? 0 : int.Parse(txtIdProducto.Value),
                //ConceptoNoIdentificacion = this.txtNoIdentificacion.Text,
                ConceptoClaveUnidad = this.ddlClaveUnidad.SelectedItem.Value
           
            };
          
            var descuento = ViewState["descuento"].ToString();
            decimal descuento1 = 0M;
            if (!string.IsNullOrEmpty(descuento))
                descuento1 = Convert.ToDecimal(descuento);

            if (!string.IsNullOrEmpty(txtDescuento.Text))
            {
                detalle.ConceptoDescuento = decimal.Parse(this.txtDescuento.Text);
                descuento1 = descuento1 + decimal.Parse(this.txtDescuento.Text);

            }
            ViewState["descuento"] = descuento1;
      
            detalles.Add(detalle);

            ViewState["detalles"] = detalles;
            AgregarIVA();
            this.txtIdProducto.Value = null;
            this.Clear();
            this.BindDetallesToGridView();
           this.UpdateTotales();
           
        */
         
            
        }

        protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Empresa = new GAFEmpresa();

            using (Empresa as IDisposable)
            {
                int idEmpresa = int.Parse(this.ddlEmpresa.SelectedValue);
                if (!Empresa.TieneConfiguradoCertificado(idEmpresa))
                {
                    this.lblError.Text = "Tienes que configurar tus certificados antes de poder facturar";
                    this.btnGenerarFactura.Enabled = this.BtnVistaPrevia.Enabled = /*this.ddlMoneda.Enabled= this.btnAgregar.Enabled=*/
                          false;
                     return;
                }
                    //var cliente = new GAFClientes();
                    //using (cliente as IDisposable)
                    //{
                    //    ddlClientes.Items.Clear();
                    //    var clientes = cliente.GetList(idEmpresa, string.Empty, false);
                    //    this.ddlClientes.DataSource = clientes;
                    //    this.ddlClientes.DataBind();
                    //}
                   
                /*
                var emp = cliente.ObtenerEmpresaById(idEmpresa);
                divViasConcesionadas.Visible = emp.RFC == "VCN940426PJ4";
                */
                
                lblMensaje.Text = "";
                lblMensajeHistorico.Text = "";

                lblError.Text = "";
                //var Sucursal = new GAFSucursales();

                //using (Sucursal as IDisposable)
                //{
                //    ddlSucursales.Items.Clear();
                //    this.ddlSucursales.DataSource = Sucursal.GetSucursalLista(idEmpresa);
                //    ddlSucursales.DataValueField = "LugarExpedicion";
                //    ddlSucursales.DataTextField = "Nombre";
                //    this.ddlSucursales.DataBind();
                //}

                this.btnGenerarFactura.Enabled = this.BtnVistaPrevia.Enabled =/* this.ddlMoneda.Enabled =this.btnAgregar.Enabled = */
                        true;
                var prefactu = new Prefactura();
                using (prefactu as IDisposable)
                {
                    this.txtFolio.Text = prefactu.GetNextFolio(idEmpresa);
                }
               
              
               // ddlClientes_SelectedIndexChanged(null, null);
                /*
                ViewState["detalles"] = new List<Datosdetalle>();
                ViewState["iva"] = 0M;
                ViewState["total"] = 0M;
                ViewState["subtotal"] = 0M;

                this.BindDetallesToGridView();
                this.UpdateTotales();
                */
            }
        }

        protected void btnGenerarPreview_Click(object sender, EventArgs e)
        {
            /*
            if (!ValidarFactura())
                return;
           
            var pdf = Preview();
            if (pdf == null)
            {
                if (string.IsNullOrEmpty(this.lblError.Text))
                this.lblError.Text = "Error al generar vista previa";
                return;
            }
            Response.AddHeader("Content-Disposition", "attachment; filename=preview.pdf");
            this.Response.ContentType = "application/pdf";
            this.lblError.Text = string.Empty;
            this.Response.BinaryWrite(pdf);
            this.Response.End();
             */
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
                this.txtTipoCambio.Visible = false;
                this.lblTipoCambio.Visible = false;
        
            }
             */ 
        }

        protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cliente =new  GAFClientes();
            if (string.IsNullOrEmpty(this.ddlClientes.SelectedValue))
                return;
            using (cliente as IDisposable)
            {
                int idCliente = int.Parse(this.ddlClientes.SelectedValue);
               GAFContract.clientes c = cliente.GetCliente(idCliente);
                var sb = new StringBuilder();
                sb.AppendLine(c.RazonSocial);
                sb.AppendLine(c.RFC);
                  this.txtRazonSocial.Text = sb.ToString();
            }
        }

        protected void gvDetalles_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("EliminarConcepto"))
            {
                
                var detalles = ViewState["detalles"] as List<Datosdetalle>;
                var detallesImpuestos=    ViewState["detallesImpuestos"] as List<DatosdetalleRT>;
                Datosdetalle dt = detalles.ElementAt(Convert.ToInt32(e.CommandArgument));
                string x = dt.ConceptoClaveProdServ;
                decimal desc = 0M;
                if (dt.ConceptoDescuento != null)
                    desc = (decimal)dt.ConceptoDescuento;

                detalles.RemoveAt(Convert.ToInt32(e.CommandArgument));
                //ddlConceptos.Items.Remove(x);
                ViewState["detalles"] = detalles;

                detallesImpuestos.RemoveAll(p => p.ConceptoClaveProdServ == x);
                ViewState["detallesImpuestos"] = detallesImpuestos;
                BindDetallesImpuestosToGridView();
                this.BindDetallesToGridView();
                var descuento = ViewState["descuento"].ToString();
                decimal descuento1 = 0M;
                if (!string.IsNullOrEmpty(descuento))
                    descuento1 = Convert.ToDecimal(descuento);

                descuento1 = descuento1 - desc;
                ViewState["descuento"] = descuento1;
                UpdateTotales();
                
            }
            if (e.CommandName.Equals("Editar"))
            {
                
                var detalles = ViewState["detalles"] as List<Datosdetalle>;
                var edicion = detalles[Convert.ToInt32(e.CommandArgument)];
                ViewState["detalles"] = detalles;
                this.hidNumero.Value = e.CommandArgument.ToString();
                 this.txtUnidadEdita.Text = edicion.ConceptoUnidad;
                this.txtCantidadEdita.Text = edicion.ConceptoCantidad.ToString();
                this.txtNoIdentificacionEdita.Text = edicion.ConceptoNoIdentificacion;
                this.txtDescripcionEdita.Text = edicion.ConceptoDescripcion;
                this.txtObservacionesEdita.Text = edicion.Descripcion;
                this.txtPrecioUnitarioEdita.Text = edicion.ConceptoValorUnitario.ToString();
                this.txtDescuentoE.Text = edicion.ConceptoDescuento.ToString();
                this.txtCodigoE.Text = edicion.ConceptoClaveProdServ;
                this.txtDescuentoE.Text = edicion.ConceptoDescuento.ToString();
                this.ddlClaveUnidadE.SelectedItem.Value = edicion.ConceptoClaveUnidad;
  
               mpeEdita.Show();
            }
        }

        protected void btnBuscarConcepto_Click(object sender, EventArgs e)
        {

            OperacionesCatalogos OP = new OperacionesCatalogos();
            using (OP as IDisposable)
            {

                List<CatalogosSAT.c_ClaveProdServ> lista = OP.ClaveProdServSearch(this.txtConceptoBusqueda.Text);
                if(lista.Count > 20)
                {
                    this.lblMensaje.Text = lista.Count + " resultados, mostrando los primeros 20, refina tu busqueda";
                }
                this.gvBuscarConceptos.DataSource = new List<CatalogosSAT.c_ClaveProdServ>(lista.Take(20).ToList());
                this.gvBuscarConceptos.DataBind();
            }
             
            this.mpeBuscarConceptos.Show();
        }

        protected void btnSeleccionarConcepto_Click(object sender, EventArgs e)
        {
            
            if (this.gvBuscarConceptos.Rows.Count > 0)
            {
                /*
                var detalles = ViewState["detalles"] as List<Datosdetalle>;
                for (int x = 0; x < this.gvBuscarConceptos.Rows.Count; x++)
                {
                    var chkSeleccion = this.gvBuscarConceptos.Rows[x].FindControl("chkSeleccion") as CheckBox;
                    if(chkSeleccion == null || !chkSeleccion.Checked)
                    {
                        continue;
                    }

                    int idProducto = Convert.ToInt32(this.gvBuscarConceptos.DataKeys[x].Value);

                    var cliente = NtLinkClientFactory.Cliente();
                    using (cliente as IDisposable)
                    {
                        GAFContract.producto prod = null;//cliente.ObtenerProductoById(idProducto);

                        decimal precio = prod.PrecioP.HasValue ? prod.PrecioP.Value : 0;

                        var detalle = new facturasdetalle
                                          {
                                              Partida = detalles.Count + 1,
                                              Cantidad = 1,
                                              Descripcion = prod.Descripcion,
                                              Codigo = prod.Codigo,
                                              Unidad = prod.Unidad,
                                              Descripcion2 = prod.Observaciones,
                                              Precio = precio,
                                              Total = precio * 1,
                                              CuentaPredial = prod.CuentaPredial,
                                          };
                        
                        detalles.Add(detalle);
                    }
                }
                ViewState["detalles"] = detalles;
             
                this.BindDetallesToGridView();
                this.UpdateTotales();
                 */ 
            }
              
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.ClearAll();
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
            /*
            List<DatosPagos> DPagos = ViewState["DPagos"] as List<DatosPagos>;
            if (DPagos == null)
            {
                this.lblError.Text = "Escribe los pagos";
                return false;
            }
            if (DPagos.Count < 1)
            {
                this.lblError.Text = "Escribe los pagos";
                return false;
            }
           */
            if ((ViewState["detalles"] as List<Datosdetalle>).Count == 0)
            {
                this.lblError.Text = "La factura no puede estar vacía";
                return false;
            }
              
            if (string.IsNullOrEmpty(this.txtFolio.Text))
            {
                this.lblError.Text = "Escribe el folio de la factura";
                return false;
            }
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
            /*
            if (txtFechaPago.Visible && !string.IsNullOrEmpty(txtFechaPago.Text))
            {
                var fecha = DateTime.ParseExact(txtFechaPago.Text, "dd/MM/yyyy", new CultureInfo("es-MX"));
                if (fecha > DateTime.Now)
                {
                    this.lblError.Text = "La fecha de pago de la factura esta fuera de rango";
                    return false;
                }
                if (fecha.Year != DateTime.Now.Year)
                {
                    this.lblError.Text = "La fecha de pago de la factura esta fuera de rango";
                    return false;
                }
            }
             */ 
            /*
            if (ddlTipoDocumento.SelectedValue == "Donativo")
            {
                if (string.IsNullOrEmpty(txtDonatAutorizacion.Text))
                {
                    this.lblError.Text = "Escribe el número de autorización del donativo";
                    return false;
                }
                if (string.IsNullOrEmpty(txtDonatFechautorizacion.Text))
                {
                    this.lblError.Text = "Escribe la fecha de autorización del donativo";
                    return false;
                }

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
                var fact = GetFactura(iniciales,detalles);
                try
                {
                    GAFFactura fac = new GAFFactura();
                    string idPrefacturaString = this.Request.QueryString["idPrefactura"];
                    if (!string.IsNullOrEmpty(idPrefacturaString))
                    {
                        if (!fac.GuardarFactura33(fact, Convert.ToInt64(idPrefacturaString), true, llenarComplementoPagos()))
                        {
                            this.lblError.Text = "* Error al generar la factura";
                            return;
                        }
                        this.lblError.Text = string.Empty;
                    }
                    else
                    {
                        if (!fac.GuardarFactura33(fact, 0, true, llenarComplementoPagos()))
                        {
                            this.lblError.Text = "* Error al generar la factura";
                            return;
                        }
                        this.lblError.Text = string.Empty;


                    }
                    //else
                    /*
                    {
                        GAFFactura pre = new GAFFactura();
                        //Prefactura pre = new Prefactura();
                        using (pre as IDisposable)
                        {
                            string s = "";
                            string idPrefacturaString = this.Request.QueryString["idPrefactura"];
                            if(!string.IsNullOrEmpty(idPrefacturaString))
                              s=  pre.GuardarPrefactura(fact, Convert.ToInt64(idPrefacturaString), llenarComplementoPagos());
                            else
                              s=  pre.GuardarPrefactura(fact, 0, llenarComplementoPagos());
                            if (s != "OK")
                            {
                                error = true;
                                this.lblError.Text = s;
                            }
                            else
                                this.ClearAll();
                        }
                    }
                   */
                   
            
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
                    ActualizarSaldosMaster();
                }
                else
                {
                    if (lblError.Text != "El folio de la empresa ya está en uso")
                    {
                        FoliodeFallas fol = new FoliodeFallas();
                        FolioFallas Foli = new FolioFallas();
                        Foli.Descripcion = lblError.Text;
                        Foli.Fecha = DateTime.Now;
                        Foli.Folio = fact.Folio;
                        Foli.idCliente = fact.idcliente;
                        Foli.idEmpresa = fact.IdEmpresa;
                        fol.Save(Foli);
                    }
                }
               // this.lblError.Text = string.Empty;
            }
             
        }

        private DatosPrefactura  GetFactura(string iniciales, List<Datosdetalle> detalles)
        {
            var sesion = Session["sessionRGV"] as Sesion;
          
      
            DatosPrefactura fact = new DatosPrefactura();
            fact.CFDI = "P";
      
                               fact.TipoDocumento = TipoDocumento.ComplementoPagos;
                               fact.IdEmpresa = int.Parse(this.ddlEmpresa.SelectedValue);
                               fact.Importe = decimal.Parse(this.lblTotal.Text, NumberStyles.Currency);
                               fact.SubTotal = decimal.Parse(this.lblSubtotal.Text, NumberStyles.Currency);
                               fact.Total = decimal.Parse(this.lblTotal.Text, NumberStyles.Currency);
                               fact.MonedaID = "XXX";//this.ddlMoneda.SelectedValue;
                               fact.idcliente = int.Parse(this.ddlClientes.SelectedValue);
                               fact.Fecha = DateTime.Now;
                               fact.Folio = this.txtFolio.Text.PadLeft(4, '0');
                              // fact.Serie = string.IsNullOrEmpty(this.txtSerie.Text) ? null : this.txtSerie.Text;
                              // nProducto = detalles.Count;
                               fact.captura = DateTime.Now;
                               fact.Cancelado = 0;
                               fact.Usuario = sesion.Id;//Guid.Parse("33760C0C-E45C-4210-8081-81C80827FA73");// System.Guid.NewGuid(); ///cambiar al verdadero
                              // fact.LugarExpedicion = this.ddlSucursales.SelectedValue;
                              // fact.Proyecto = this.txtProyecto.Text;
                               fact.MonedaS = "XXX"; //this.ddlMoneda.SelectedItem.Text;
                              /* fact.VoBoNombre = this.txtVoBoNombre.Text,
                               fact.VoBoPuesto = this.txtVoBoPuesto.Text,
                               fact.VoBoArea = this.txtVoBoArea.Text,
                               fact.RecibiNombre = this.txtRecibiNombre.Text,
                               fact.RecibiPuesto = this.txtRecibiPuesto.Text,
                               fact.RecibiArea = this.txtRecibiArea.Text,
                               fact.AutorizoNombre = this.txtAutorizoNombre.Text,
                               fact.AutorizoPuesto = this.txtAutorizoPuesto.Text,
                               fact.AutorizoArea = this.txtAutorizoArea.Text, 
                               */
                               fact.UsoCFDI = "P01";// se define para complemento de pago
                               fact.Estatus = 0;//Convert.ToInt16( ddlStatusFactura.SelectedValue);


                               var Empresa = new GAFEmpresa();
                               using (Empresa as IDisposable)
                               {
                                   var emp = Empresa.GetById(Convert.ToInt16(this.ddlEmpresa.SelectedValue));
                                   fact.LugarExpedicion = emp.CP;
                               }
           /*
             if(ddlFormaPago.SelectedValue!="00")
             {    fact.FormaPagoID = this.ddlFormaPago.SelectedValue;
                 fact.FormaPago= this.ddlFormaPago.SelectedItem.Text;
             }
             if (ddlMetodoPago.SelectedValue != "00")
             {
                 fact.MetodoID = this.ddlMetodoPago.SelectedValue;
                 fact.Metodo = this.ddlMetodoPago.SelectedItem.Text;
             }
            */ 
            // if (!string.IsNullOrEmpty(this.txtTipoCambio.Text) )
            //     fact.TipoCambio =this.txtTipoCambio.Text;
           //  if (!string.IsNullOrEmpty(this.txtConfirmacion.Text))
           //      fact.Confirmacion = this.txtConfirmacion.Text;
           //  if (!string.IsNullOrEmpty(this.txtCondicionesPago.Text))
           //      fact.CondicionesPago = this.txtCondicionesPago.Text;
             fact.Fecha = DateTime.Now;

             var descuento = ViewState["descuento"];
            if(descuento!=null)
             fact.Descuento =Convert.ToDecimal( descuento);
              
            /*
            if (ddlTipoDocumento.SelectedValue == "Egreso")
                fact.NotaCredito = true;
            if (ddlTipoDocumento.SelectedValue == "Donativo")
            {
                fact.TipoDocumento = TipoDocumento.Donativo;
                fact.DonativoAutorizacion = txtDonatAutorizacion.Text;
                fact.DonativoFechaAutorizacion = DateTime.ParseExact(txtDonatFechautorizacion.Text, "dd/MM/yyyy",
                                                                     new CultureInfo("es-MX"));
            }
            if (!string.IsNullOrEmpty(txtFolioOriginal.Text))
            {
                //var fecha = txtFechaOriginal.Text;
                fact.Fecha = DateTime.ParseExact(txtFechaOriginal.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                fact.FolioFiscalOriginal = txtFolioOriginal.Text;
                fact.SerieFolioFiscalOriginal = txtSerieOriginal.Text;
                fact.MontoFolioFiscalOriginal = Decimal.Parse(txtMontoOriginal.Text);
            }
            */
            /*
               List<string> CfdiRelacionado = ViewState["CfdiRelacionado"] as List<string>;

               if (CfdiRelacionado!=null)
                   if (CfdiRelacionado.Count() > 0)
                   {
                       fact.UUID = CfdiRelacionado;
                       fact.TipoRelacion = ddlTipoRelacion.SelectedValue;
                   }
            
            if(ddlStatusFactura.SelectedValue=="1")
            {
                fact.StatusPago = true;
                fact.FechaPago = Convert.ToDateTime( txtFechaPago.Text);
            }
            else
                fact.StatusPago = false;
        */
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

            return fact;
        }

        private byte[] Preview()
        {
           
            bool error = false;
            /*
            var detalles = ViewState["detalles"] as List<facturasdetalle>;
            var iniciales = Session["iniciales"] as string;
            var fact = GetFactura(iniciales, detalles);
            */
            try
            {
              /*  var clienteServicio = NtLinkClientFactory.Cliente();
                int idCliente = int.Parse(this.ddlClientes.SelectedValue);
               ServicioLocalContract.clientes c = clienteServicio.ObtenerClienteById(idCliente);
                using (clienteServicio as IDisposable)
                {
                    List<facturasdetalle33> fact33 = new List<facturasdetalle33>();
                        foreach(var de in detalles)
                        {    facturasdetalle33 f33=new facturasdetalle33();
                            f33.ConceptoRetenciones = de.ConceptoRetenciones;
                            f33.ConceptoTraslados=de.ConceptoTraslados;
                            f33.ConceptoClaveProdServ = de.Codigo;
                            fact33.Add(f33);
                        }
                         
                    var pdf = clienteServicio.FacturaPreview33(fact, detalles,fact33 ,null);
                    if (pdf == null)
                    {
                        this.lblError.Text = "* Error al generar la factura";
                        return null;
                    }
                    else return pdf;
                }
             */
            return null;
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
                this.lblError.Text = "Error al generar el comprobante: " + ae.Message;

            }
            if (!error)
            {
                this.lblError.Text = "Comprobante generado correctamente";
            }
            //this.lblError.Text = string.Empty;
            return null;
        }

        private void Clear()
        {
           // this.txtCodigo.Text = this.txtDescripcion.Text = this.txtDetalles.Text = this.txtPrecio.Text =
            //    this.txtCantidad.Text = /*this.txtUnidad.Text = txtNoIdentificacion.Text =*/txtDescuento.Text= string.Empty;
        }

        private void ClearAll()
        {
            this.Clear();

            /*this.txtProyecto.Text =*/ this.txtFolioOriginal.Text = this.txtFechaOriginal.Text = this.txtMontoOriginal.Text =
              /*=this.txtSerie.Text */   string.Empty;
            //this.ddlStatusFactura.SelectedIndex = 0;
           // this.txtFechaPago.Text = "";
           // this.txtFechaPago.Visible = false;
           // this.lblIva.Text = 0M.ToString("C");
            this.lblTraslados.Text = 0M.ToString("C");
            this.lblRetenciones.Text = 0M.ToString("C");
            
            this.lblSubtotal.Text = 0M.ToString("C");
            this.lblTotal.Text = 0M.ToString("C");
            
            var detalles = new List<Datosdetalle>();
            ViewState["detalles"] = detalles;
            
            var Impuestos =new  List<DatosdetalleRT>();
            ViewState["detallesImpuestos"] = Impuestos;//para impuestos
            var Relacionado = new List<string>();
            ViewState["CfdiRelacionado"] = Relacionado;
            var DPagos = new List<DatosPagos>();
             ViewState["DPagos"] =DPagos;
             var Pagos = new List<Pagos>();
             ViewState["Pagos"] = Pagos;
             var PagoDocto = new List<PagoDoctoRelacionado>();
             ViewState["PagoDoctoRelacionado"] = PagoDocto;

             BindDocumentosToGridView();
             BindPagosToGridView();
          
            this.BindDetallesToGridView();
            this.BindDetallesImpuestosToGridView();
         //    this.BindCfdiRelacionadoToGridView();
         //   this.BindDPagosToGridView();

            var prefactu = new Prefactura();
            using (prefactu as IDisposable)
            {
                int idEmpresa = int.Parse(this.ddlEmpresa.SelectedValue);
                this.txtFolio.Text = prefactu.GetNextFolio(idEmpresa);
            }
               
           
          //  cbImpuestos.Checked = false;
            //cbCfdiRelacionados.Checked = false;
        }

        private void BindDetallesToGridView()
        {
            /*
            var conceptos = ViewState["detalles"] as List<Datosdetalle>;
            if(conceptos != null && conceptos.Count > 0)
            {
                int noColumns = this.gvDetalles.Columns.Count;
                this.gvDetalles.Columns[noColumns - 1].Visible = this.gvDetalles.Columns[noColumns - 2].Visible = true;
            }
            else
            {
                int noColumns = this.gvDetalles.Columns.Count;
                this.gvDetalles.Columns[noColumns - 1].Visible = this.gvDetalles.Columns[noColumns - 2].Visible = false;
            }
            this.gvDetalles.DataSource = conceptos;
            this.gvDetalles.DataBind();
            */
        }


        private void BindDetallesImpuestosToGridView()
        {
            /*
            List<DatosdetalleRT> detallesImpuestos2 = ViewState["detallesImpuestos"] as List<DatosdetalleRT>;
            List<DatosdetalleRT> detallesImpuestos = ViewState["detallesImpuestos"] as List<DatosdetalleRT>;
            if (detallesImpuestos != null && detallesImpuestos.Count > 0)
            {
                int noColumns = this.gvImpuestos.Columns.Count;
                this.gvImpuestos.Columns[noColumns - 1].Visible = this.gvImpuestos.Columns[noColumns - 2].Visible = true;
            }
            else
            {
                int noColumns = this.gvImpuestos.Columns.Count;
                this.gvImpuestos.Columns[noColumns - 1].Visible = this.gvImpuestos.Columns[noColumns - 2].Visible = false;
            }
            foreach (var de in detallesImpuestos2)
            {
                if (de.Impuesto == "001")
                    de.Impuesto = "ISR";
                if (de.Impuesto == "002")
                    de.Impuesto = "IVA";
                if (de.Impuesto == "003")
                    de.Impuesto = "IEPS";
            }

            this.gvImpuestos.DataSource = detallesImpuestos2;
            this.gvImpuestos.DataBind();
              */
        }
     

      
        private void UpdateTotales()
        {
            
            CultureInfo cul = CultureInfo.CreateSpecificCulture("es-MX");
           // var cliente = NtLinkClientFactory.Cliente();
           var detalles = ViewState["detalles"] as List<Datosdetalle>;
            var descuento= ViewState["descuento"].ToString();
            decimal descuento1 = 0M;
            if (!string.IsNullOrEmpty(descuento))
                descuento1 = Convert.ToDecimal(descuento);

            if (detalles == null)
                return;
            var iva = 0M;
            var total = 0M;
            var subtotal = 0M;
            var retenciontotal = 0M;
            var trasladototal = 0M;

            foreach (Datosdetalle detalle in detalles)
            {
                subtotal += detalle.ConceptoImporte;

                //iva += detalle.ImporteIva.HasValue ? detalle.ImporteIva.Value : 0;
               // total += detalle.TotalPartida + (detalle.ImporteIva.HasValue ? detalle.ImporteIva.Value : 0);
                total += detalle.ConceptoImporte;
                foreach (var det in detalle.ConceptoTraslados)
                {
                    trasladototal =(decimal) (trasladototal + det.Importe);
                }
                foreach (var ret in detalle.ConceptoRetenciones)
                {
                    retenciontotal = (decimal)(retenciontotal + ret.Importe);
                }

            }
            total = total + trasladototal - retenciontotal-descuento1;

      
            this.lblDescuento.Text = descuento1.ToString("C", cul);
            this.lblRetenciones.Text = retenciontotal.ToString("C", cul);
            this.lblTraslados.Text = trasladototal.ToString("C", cul);

                this.lblTotal.Text = total.ToString("C", cul);
                this.lblSubtotal.Text = subtotal.ToString("C", cul);
     
        }

        #endregion

        protected void gvBuscarConceptos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            /*
            int idProducto = Convert.ToInt32(this.gvBuscarConceptos.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);
            OperacionesCatalogos OP=new OperacionesCatalogos();
           using (OP as IDisposable)
            {

                CatalogosSAT.c_ClaveProdServ prod = OP.Consultar_ClaveProdServ(idProducto);
               this.txtCodigo.Text = prod.c_ClaveProdServ1.ToString();
               if (txtCodigo.Text.Length < 8)
                   txtCodigo.Text = "0" + txtCodigo.Text;

     
            }
          
            this.mpeBuscarConceptos.Hide();
             */ 
        }

        protected void ddlStatusFactura_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            if (ddlStatusFactura.SelectedValue == "1")
            {
                this.lblFechaPago.Visible = true;
                this.txtFechaPago.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.txtFechaPago.Visible = true;
            }
            else
            {
                this.lblFechaPago.Visible = false;
                this.txtFechaPago.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.txtFechaPago.Visible = false;
            }
             */

           // MostrarDatosSegunEstatus(Convert.ToInt16(ddlStatusFactura.SelectedValue));
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            
            var detalles = ViewState["detalles"] as List<Datosdetalle>;
            var edicion = detalles[Convert.ToInt32(this.hidNumero.Value)];
           
           // edicion.idproducto = int.Parse(this.hidDetalle.Value);
            edicion.ConceptoUnidad = this.txtUnidadEdita.Text;
            edicion.ConceptoCantidad = decimal.Parse(this.txtCantidadEdita.Text);
            edicion.ConceptoClaveProdServ = this.txtCodigoE.Text;
            edicion.ConceptoDescripcion = this.txtDescripcionEdita.Text; 
            edicion.Descripcion = this.txtObservacionesEdita.Text;
            edicion.ConceptoValorUnitario= decimal.Parse(this.txtPrecioUnitarioEdita.Text);
            if (!string.IsNullOrEmpty(txtDescuentoE.Text))
                edicion.ConceptoDescuento = decimal.Parse(this.txtDescuentoE.Text);

            edicion.ConceptoNoIdentificacion = this.txtNoIdentificacionEdita.Text;
            edicion.ConceptoClaveUnidad = this.ddlClaveUnidadE.SelectedValue;
           // edicion.CuentaPredial = this.txtCuentaPredialEdita.Text;
            //if (edicion.PorcentajeIva != null)
           // edicion.ImporteIva = ((decimal)edicion.PorcentajeIva / 100) * edicion.TotalPartida;
            edicion.ConceptoImporte = edicion.ConceptoValorUnitario * edicion.ConceptoCantidad;
            ViewState["detalles"] = detalles;
            this.BindDetallesToGridView();
            this.UpdateTotales();
              
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

        protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            if (ddlTipoDocumento.SelectedValue == "Donativo")
            {
                trDonativo.Visible = true;
            }
            else trDonativo.Visible = false;
            */
           
        }
        //---------------------------------------------------------------------------
        public void ActualizarSaldosMaster()
        {
            /*
            var sis = Session["idSistema"] as long?;
           
            if (sis != null)//---nuevo---
            {
                var cliente = NtLinkClientFactory.Cliente();
                using (cliente as IDisposable)
                {
                    var sistema = cliente.ObtenerSistemaById((int)sis.Value, true);
                    Session["SaldoEmision"] = sistema.SaldoEmision;
                    Session["SaldoTimbrado"] = sistema.SaldoTimbrado;
                    Session["Contratos"] = sistema.TimbresContratados;
                    var emision = Session["SaldoEmision"];
                    var timbrado = Session["SaldoTimbrado"];
                    var contratos = Session["Contratos"] ?? "0";

                    Master.labelcontratos.Text = contratos.ToString();
                    Master.labelEmision.Text = emision.ToString();
                    Master.labeltimbrado.Text =timbrado.ToString();
                    Master.panel.Update();
                
                }
                    
                 
            }
            */
        }

        //------------------------------------------------------------------------------

       
       
        protected void ckeckImpuestos_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void btnAgregarImpuesto_Click(object sender, EventArgs e)
        {
         /*   
            List<Datosdetalle> detalles = ViewState["detalles"] as List<Datosdetalle>; 
            List<DatosdetalleRT> detallesImpuestos = ViewState["detallesImpuestos"] as List<DatosdetalleRT>;
                 string  m= ViewState["DecimalMoneda"].ToString() ;
                 int mon = 0;
                 if (!string.IsNullOrEmpty(m))
                     mon = Convert.ToInt16(m);

            foreach(var x in detalles)
            {
               
                if(x.ConceptoClaveProdServ==ddlConceptos.SelectedValue)
                {
                    //-------llenamos el objeto de impuestos RT------------------------
                    DatosdetalleRT DRT = new DatosdetalleRT();
                    DRT.Base = Decimal.Round(Convert.ToDecimal(txtBase.Text),mon);
                   // DRT.Importe = Convert.ToDecimal(txtImporte.Text);
                    if (ddlTasaOCuota.Visible == true)
                        DRT.Importe = Decimal.Round(Convert.ToDecimal(txtBase.Text) * Convert.ToDecimal(ddlTasaOCuota.SelectedValue),mon);
                    if (txtTasa.Visible == true)
                        DRT.Importe = Decimal.Round(Convert.ToDecimal(txtBase.Text) * Convert.ToDecimal( txtTasa.Text),mon);
                      
                    DRT.Impuesto = ddlImpuesto.SelectedValue;
                    if (txtTasa.Visible==true)
                        DRT.TasaOCuota = numerodecimales(Convert.ToDecimal(txtTasa.Text),6);
                    if(ddlTasaOCuota.Visible==true)
                        DRT.TasaOCuota = ddlTasaOCuota.SelectedValue;
                   
                    DRT.TipoFactor = ddlTipoFactor.SelectedValue;
                    DRT.TipoImpuesto = ddlTipoImpuesto.SelectedValue;
                    DRT.ConceptoClaveProdServ = ddlConceptos.SelectedValue;
                    //-----------------fin objeto impuestos RT------------------------
                    //-----se llenan los impusetos reales----------------------------
                    if (ddlTipoImpuesto.SelectedValue == "Retenciones")
                    {
                        if (x.ConceptoRetenciones == null)
                            x.ConceptoRetenciones = new List<DatosdetalleRetencion>();
                          DatosdetalleRetencion retencion = new DatosdetalleRetencion();
                          retencion.Base =Decimal.Round(Convert.ToDecimal( txtBase.Text),mon);
                           if (ddlTasaOCuota.Visible == true)
                        retencion.Importe = Decimal.Round(Convert.ToDecimal(txtBase.Text) * Convert.ToDecimal(ddlTasaOCuota.SelectedValue),mon);
                    if (txtTasa.Visible == true)
                        retencion.Importe =Decimal.Round( Convert.ToDecimal(txtBase.Text) * Convert.ToDecimal(txtTasa.Text),mon);
                 
                        //retencion.Importe =Convert.ToDecimal(txtImporte.Text);
                          retencion.Impuesto=ddlImpuesto.SelectedValue;
                        if (txtTasa.Visible == true)
                            retencion.TasaOCuota = numerodecimales(Convert.ToDecimal(txtTasa.Text), 6);
                        if (ddlTasaOCuota.Visible == true)
                            retencion.TasaOCuota = ddlTasaOCuota.SelectedValue;
       
                        retencion.TipoFactor = ddlTipoFactor.SelectedValue;
                        x.ConceptoRetenciones.Add(retencion);
                    }
                    if (ddlTipoImpuesto.SelectedValue == "Traslados")
                    {
                        if (x.ConceptoTraslados == null)
                            x.ConceptoTraslados = new List<DatosdetalleTraslado>();
                        DatosdetalleTraslado traslados = new DatosdetalleTraslado();
                        traslados.Base = Decimal.Round(Convert.ToDecimal(txtBase.Text),mon);
                       // traslados.Importe = Convert.ToDecimal(txtImporte.Text);
                           if (ddlTasaOCuota.Visible == true)
                        traslados.Importe =Decimal.Round( Convert.ToDecimal(txtBase.Text) * Convert.ToDecimal(ddlTasaOCuota.SelectedValue),mon);
                    if (txtTasa.Visible == true)
                        traslados.Importe = Decimal.Round(Convert.ToDecimal(txtBase.Text) * Convert.ToDecimal( txtTasa.Text),mon);
                 
                        traslados.Impuesto = ddlImpuesto.SelectedValue;
                        if (txtTasa.Visible == true)
                            traslados.TasaOCuota = numerodecimales(Convert.ToDecimal(txtTasa.Text), 6);
                        if (ddlTasaOCuota.Visible == true)
                            traslados.TasaOCuota = ddlTasaOCuota.SelectedValue;
       
                        traslados.TipoFactor = ddlTipoFactor.SelectedValue;
                        x.ConceptoTraslados.Add(traslados);
                    }
                    //-------------------------------fin impuestos reales----------------
                    detallesImpuestos.Add(DRT);
                }
                

            }
            ViewState["detalles"] = detalles;
           
             ViewState["detallesImpuestos"] = detallesImpuestos;

             this.gvImpuestos.DataSource = detallesImpuestos;
             this.gvImpuestos.DataBind();
     
             BindDetallesImpuestosToGridView();
             this.UpdateTotales();
        //     txtBase.Text = "";
        //     txtTasa.Text = "";
              */
        }

        protected void gvImpuestos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            if (e.CommandName.Equals("EliminarImpuesto"))
            {  List<Datosdetalle> detalles = ViewState["detalles"] as List<Datosdetalle>;
                var impuestos = ViewState["detallesImpuestos"] as List<DatosdetalleRT>;
                DatosdetalleRT dt = impuestos.ElementAt(Convert.ToInt32(e.CommandArgument));
                string Impuesto = "";
                if (dt.Impuesto == "IVA")
                    Impuesto = "002";
                if (dt.Impuesto == "ISR")
                      Impuesto = "001";
                if (dt.Impuesto == "IEPS")
                    Impuesto = "003";
               // detalles.RemoveAll(p => p. == x);
                 foreach (var x in detalles)
                 {
                     if (dt.TipoImpuesto == "Traslados")
                     {
                         x.ConceptoTraslados.RemoveAll(p =>  p.Base == dt.Base && p.Importe == dt.Importe
                                   && p.Impuesto == Impuesto && Convert.ToDecimal( p.TasaOCuota)==Convert.ToDecimal( dt.TasaOCuota)
                                   && p.TipoFactor==dt.TipoFactor);
                     
                     }
                     if (dt.TipoImpuesto == "Retenciones")
                     {
                         x.ConceptoRetenciones.RemoveAll(p => p.Base == dt.Base && p.Importe == dt.Importe
                                   && p.Impuesto == Impuesto && Convert.ToDecimal(p.TasaOCuota )== Convert.ToDecimal(dt.TasaOCuota) && p.TipoFactor == dt.TipoFactor);

                     }
                 }
               
                ViewState["detalles"] = detalles;
              
                 impuestos.RemoveAt(Convert.ToInt32(e.CommandArgument));
                ViewState["detallesImpuestos"] = impuestos;
                

                this.BindDetallesImpuestosToGridView();
                this.UpdateTotales();
 
            }
            
        }

        protected void cbImpuestos_CheckedChanged(object sender, EventArgs e)
        {
            /*
            if (cbImpuestos.Checked == true)
            { 
            DivComplementos.Visible=true;
                //---------------------------llenado de datos
                OperacionesCatalogos OP=new OperacionesCatalogos();
            
             List<CatalogosSAT.c_TasaOCuota> L;
            bool rango = false;
            using (OP as IDisposable)
            
            {
                L = OP.Consultar_TasaCuota(ddlImpuesto.SelectedValue, ddlTipoFactor.SelectedValue, ddlTipoImpuesto.SelectedValue,ref rango);
            }

            if (rango == true)
            {
                txtTasa.Text = "";
                txtTasa.Enabled = true;
                txtTasa.Visible = true;
                ddlTasaOCuota.Enabled = false;
                ddlTasaOCuota.Visible = false;
                
            }
            else
            {
                this.ddlTasaOCuota.Visible = true;
                this.ddlTasaOCuota.Enabled = true;
                this.ddlTasaOCuota.DataSource = L;
                this.ddlTasaOCuota.DataBind();
                txtTasa.Enabled = false;
                txtTasa.Visible = false;
            }
                //--
            List<Datosdetalle> detalles = ViewState["detalles"] as List<Datosdetalle>;

            foreach (var x in detalles)
            {

                if (x.ConceptoClaveProdServ == ddlConceptos.SelectedValue)
                {
                    txtBase.Text = x.ConceptoImporte.ToString();
                    break;
                }

            }
 
                //-----------------------------------

            }
            else
                DivComplementos.Visible = false;
        */
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

           // ddlConceptos.Items.Add(this.txtCodigo.Text);

        }

        protected void btnBuscarHistorico_Click(object sender, EventArgs e)
        {
            mpeBuscarConceptoHistorico.Show();
            this.gvBuscarConceptosHistorico.DataSource = new List<GAFContract.producto>();
            this.gvBuscarConceptosHistorico.DataBind();
        }

        protected void gvBuscarConceptosHistorico_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           /*
            int idProducto = Convert.ToInt32(this.gvBuscarConceptosHistorico.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);
            var Producto = new GAFProducto();
            using (Producto as IDisposable)
            {

                GAFContract.producto prod = Producto.GetProduct(idProducto);
                this.txtIdProducto.Value = idProducto.ToString();
                this.txtCantidad.Text = "1";
                this.txtCodigo.Text = prod.Codigo;
                this.txtDescripcion.Text = prod.Descripcion;
               // this.txtUnidad.Text = prod.Unidad;
                this.txtPrecio.Text = prod.PrecioP.HasValue ? prod.PrecioP.Value.ToString() : string.Empty;
            }
            this.mpeBuscarConceptoHistorico.Hide();
    
            */
        }

        protected void btnBuscarConceptoHistorico_Click(object sender, EventArgs e)
        {
            var idEmpresa = int.Parse(ddlEmpresa.SelectedValue);
          
              var Producto = new GAFProducto();
                    using (Producto as IDisposable)
                    {

                        List<GAFContract.producto> lista = Producto.ProductSearch(this.txtConceptoHistoricoBusqueda.Text, idEmpresa);
                if (lista.Count > 20)
                {
                    this.lblMensajeHistorico.Text = lista.Count + " resultados, mostrando los primeros 20, refina tu busqueda";
                }
                this.gvBuscarConceptosHistorico.DataSource = new List<GAFContract.producto>(lista.Take(20).ToList());
                this.gvBuscarConceptosHistorico.DataBind();
            }
            this.mpeBuscarConceptoHistorico.Show();
   
        }

        protected void ddlConceptos_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            List<Datosdetalle> detalles = ViewState["detalles"] as List<Datosdetalle>;

            foreach (var x in detalles)
            {

                if (x.ConceptoClaveProdServ == ddlConceptos.SelectedValue)
                {
                    txtBase.Text = x.ConceptoImporte.ToString();
                    break;
                }

            }
             */
         }

        protected void ddlImpuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            OperacionesCatalogos OP = new OperacionesCatalogos();
             List<CatalogosSAT.c_TasaOCuota> L;
             bool rango = false;
             using (OP as IDisposable)
             {
               
               L=   OP.Consultar_TasaCuota(ddlImpuesto.SelectedValue, ddlTipoFactor.SelectedValue, ddlTipoImpuesto.SelectedValue,ref rango);
             }

             if (rango == true)
             {
                 txtTasa.Text = "";
                 txtTasa.Enabled = true;
                 txtTasa.Visible = true;
                 ddlTasaOCuota.Enabled = false;
                 ddlTasaOCuota.Visible = false;
             }
             else
             {
                 this.ddlTasaOCuota.Visible = true;
                 this.ddlTasaOCuota.Enabled = true;
                 this.ddlTasaOCuota.DataSource = L;
                 this.ddlTasaOCuota.DataBind();
                 txtTasa.Enabled = false;
                 txtTasa.Visible = false;
             }
            */
        }

        protected void ddlTipoFactor_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
              OperacionesCatalogos OP=new OperacionesCatalogos();
                     
            List<CatalogosSAT.c_TasaOCuota> L;
            bool rango = false;

            using (OP as IDisposable)
            {
                  
                L = OP.Consultar_TasaCuota(ddlImpuesto.SelectedValue, ddlTipoFactor.SelectedValue, ddlTipoImpuesto.SelectedValue,ref rango);
            }

            if (rango == true)
            {
                txtTasa.Text = "";
                txtTasa.Enabled = true;
                txtTasa.Visible = true;
                ddlTasaOCuota.Enabled = false;
                ddlTasaOCuota.Visible = false;
            }
            else
            {
                this.ddlTasaOCuota.Visible = true;
                this.ddlTasaOCuota.Enabled = true;
                this.ddlTasaOCuota.DataSource = L;
                this.ddlTasaOCuota.DataBind();
                txtTasa.Enabled = false;
                txtTasa.Visible = false;
            }
            */
        }

        protected void ddlTipoImpuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            OperacionesCatalogos OP = new OperacionesCatalogos();
            List<CatalogosSAT.c_TasaOCuota> L;
            bool rango = false;

            using (OP as IDisposable)
            {
                  
                L = OP.Consultar_TasaCuota(ddlImpuesto.SelectedValue, ddlTipoFactor.SelectedValue, ddlTipoImpuesto.SelectedValue,ref rango);
            }

            if (rango == true)
            {
                txtTasa.Text = "";
                txtTasa.Enabled = true;
                txtTasa.Visible = true;
                ddlTasaOCuota.Enabled = false;
                ddlTasaOCuota.Visible = false;
            }
            else
            {
                this.ddlTasaOCuota.Visible = true;
                this.ddlTasaOCuota.Enabled = true;
                this.ddlTasaOCuota.DataSource = L;
                this.ddlTasaOCuota.DataBind();
                txtTasa.Enabled = false;
                txtTasa.Visible = false;
            }
            */
        }

        protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            this.mpeBuscarConceptos.Show();
            this.gvBuscarConceptos.DataSource = new List<GAFContract.producto>();
            this.gvBuscarConceptos.DataBind();
       

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

        

      

        protected void txtFechaPago_TextChanged(object sender, EventArgs e)
        {

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
                var cliente = new GAFClientes();
                using (cliente as IDisposable)
                {
                    ddlClientes.Items.Clear();
                    var clientes = cliente.GetList(idEmpresa, string.Empty, false);
                    this.ddlClientes.DataSource = clientes;
                    this.ddlClientes.DataBind();
                }
                var prefactu = new Prefactura();
                using (prefactu as IDisposable)
                {
                    this.txtFolio.Text = prefactu.GetNextFolio(idEmpresa);
                }
                ddlClientes_SelectedIndexChanged(null, null);
            }
            var Sucursal = new GAFSucursales();

            //using (Sucursal as IDisposable)
            //{
            //    ddlSucursales.Items.Clear();
            //    this.ddlSucursales.DataSource = Sucursal.GetSucursalLista(idEmpresa);
            //    ddlSucursales.DataValueField = "LugarExpedicion";
            //    ddlSucursales.DataTextField = "Nombre";
            //    this.ddlSucursales.DataBind();
            //}
                      
                 
        }

        protected void cbDPagos_CheckedChanged(object sender, EventArgs e)
        {
            /*
            if (cbDPagos.Checked == true)
                DivDPagos.Visible = true;
            else
                DivDPagos.Visible = false;
             * */
        }

        
     //******************************************************************

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
                   var comp=pre.GetComplementoPago(id);

                    ddlEmpresa.SelectedValue = fact.IdEmpresa.ToString();
                    if (ddlEmpresa.Items.Count > 0)
                    {
                        var cliente = new GAFClientes();
                        using (cliente as IDisposable)
                        {
                            ddlClientes.Items.Clear();
                           // var clientes = cliente.GetList(fact.IdEmpresa, string.Empty, false);
                            var clien = cliente.GetCliente(fact.idcliente);
                            List<clientes> clientes = new List<clientes>();
                            clientes.Add(clien);
                            this.ddlClientes.DataSource = clientes;
                            this.ddlClientes.DataBind();
                            ddlClientes.SelectedValue = fact.idcliente.ToString();
                   
                        }
                        
                        ddlClientes_SelectedIndexChanged(null, null);
                    }
                   
                    //txtSerie.Text = fact.Serie;
                    txtFolio.Text = fact.Folio;
                    //ddlTipoDocumento.SelectedValue = fact.TipoDeComprobante;
                  //  txtConfirmacion.Text = fact.Confirmacion;
                  //  ddlMoneda.SelectedValue = fact.MonedaID;
                  //  if (!string.IsNullOrEmpty(fact.TipoCambio))
                  //  {
                  //      txtTipoCambio.Visible = true;
                 //       txtTipoCambio.Text = fact.TipoCambio;
                 //   }
                   // ddlFormaPago.SelectedValue = fact.FormaPagoID;
                   // ddlMetodoPago.SelectedValue = fact.MetodoID;
                  //  txtCondicionesPago.Text = fact.CondicionesPago;
                  //  ddlUsoCFDI.SelectedValue = fact.UsoCFDI;
                    //txtProyecto.Text = fact.Proyecto;
                    //..............cfdi relaionados------------------
                    /*List<string> CfdiRelacionado = ViewState["CfdiRelacionado"] as List<string>;

                    if (fact.UUID != null)
                        if (fact.UUID.Count() > 0)
                        {
                             CfdiRelacionado=fact.UUID ;
                              ddlTipoRelacion.SelectedValue=fact.TipoRelacion ;
                              ViewState["CfdiRelacionado"] = CfdiRelacionado;
                              this.BindCfdiRelacionadoToGridView();
                              cbCfdiRelacionados.Checked = true;
                              DivCfdiRelacionados.Visible = true;
                        }
   */
                    //-------conceptos------------
                    var detalles = ViewState["detalles"] as List<Datosdetalle>;
                    if(fact.Detalles!=null)
                        if (fact.Detalles.Count() > 0)
                        {
                            detalles = fact.Detalles;
                            ViewState["detalles"] = detalles;
                            this.BindDetallesToGridView();
                            this.UpdateTotales();
                      
                        }
                    //----------impuestos----------------------
                    List<DatosdetalleRT> detallesImpuestos = ViewState["detallesImpuestos"] as List<DatosdetalleRT>;
                    if(fact.Detalles!=null)
                        if (fact.Detalles.Count() > 0)
                        {

                            foreach (var x in fact.Detalles)
                            {
                                //ddlConceptos.Items.Add(x.ConceptoClaveProdServ);

                                if(x.ConceptoRetenciones!=null)
                                    if (x.ConceptoRetenciones.Count() > 0)
                                    { 
                                         foreach (var re in x.ConceptoRetenciones)
                                        {
                                         DatosdetalleRT DRT = new DatosdetalleRT();
                                         DRT.Base = re.Base;
                                         DRT.Importe = re.Importe;
                                          DRT.Impuesto = re.Impuesto;
                                          DRT.TasaOCuota = re.TasaOCuota;
                                          DRT.TipoFactor =re.TipoFactor;
                                          DRT.TipoImpuesto = "Retenciones";
                                           DRT.ConceptoClaveProdServ = x.Partida.ToString();
                                           detallesImpuestos.Add(DRT);
                                        }
                                     }
                                if (x.ConceptoTraslados != null)
                                    if (x.ConceptoTraslados.Count() > 0)
                                    {
                                        foreach (var tr in x.ConceptoTraslados)
                                        {
                                            DatosdetalleRT DRT = new DatosdetalleRT();
                                            DRT.Base = tr.Base;
                                            if(tr.Importe!=null)
                                            DRT.Importe = (decimal)tr.Importe;
                                            DRT.Impuesto = tr.Impuesto;
                                            DRT.TasaOCuota = tr.TasaOCuota;
                                            DRT.TipoFactor = tr.TipoFactor;
                                            DRT.TipoImpuesto = "Traslados";
                                            DRT.ConceptoClaveProdServ = x.ConceptoClaveProdServ;
                                            detallesImpuestos.Add(DRT);
                                        }
                                    }
                           }
                            ViewState["detallesImpuestos"] = detallesImpuestos;
                            if(detallesImpuestos!=null)
                                if (detallesImpuestos.Count() > 0)
                                {
                                   // cbImpuestos.Checked = true;
                                   // DivComplementos.Visible = true;
                                   // cbImpuestos_CheckedChanged(null, null);
                                   // this.BindDetallesImpuestosToGridView();
                                    this.UpdateTotales();
                                }
                        }
                    //-----------------------complementos pago---------------
                    List<Pagos> pagos = ViewState["Pagos"] as List<Pagos>;
                  if(comp!=null)
                      if (comp.Count() > 0)
                      {
                          ViewState["Pagos"] = comp;
                          List<PagoDoctoRelacionado> documentos = ViewState["PagoDoctoRelacionado"] as List<PagoDoctoRelacionado>;
                          foreach (var cm in comp)
                          {
                              if(cm.DoctoRelacionado!=null)
                                  if(cm.DoctoRelacionado.Count()>0)
                              foreach (var d in cm.DoctoRelacionado)
                              {
                                   documentos.Add(d);
                              }
                          }
                          ViewState["PagoDoctoRelacionado"] = documentos;
                          BindDocumentosToGridView();
                          BindPagosToGridView();
                          cbDoctoRelacionado.Checked = true;
                          DivDoctoRelacionado.Visible = true;
                      }
                    
                    return 1;
                }
            }
        
        }

        protected void ddlClaveUnidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            if (ddlClaveUnidad.SelectedValue == "0")
            {
                OperacionesCatalogos OP=new OperacionesCatalogos();
                using (OP as IDisposable)
                {

                   // ddlClaveUnidad.DataSource = OP.ConsultarClaveUnidadMasUsadas();
                    ddlClaveUnidad.Items.Clear();
                     ddlClaveUnidad.DataSource = OP.ConsultarClaveUnidadAll();
                    ddlClaveUnidad.DataTextField = "Nombre";
                    ddlClaveUnidad.DataValueField = "c_ClaveUnidad1";
                    ddlClaveUnidad.DataBind();
                }
            }
             */ 
        }
       
        //------------------------------------------------------------------------------
        private void MostrarDatosSegunEstatus(int status)
        {
           
               
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
            p.ctaBeneficiario = txtCtaBeneficiario.Text;
            p.ctaOrdenante = txtCtaOrdenante.Text;
            p.fechaPago = txtFechaPagoP.Text;
            DateTime s = Convert.ToDateTime(p.fechaPago);
            TimeSpan ts = new TimeSpan(12, 0, 0);
            s = s.Date + ts;
            p.fechaPago = s.ToString();

            p.formaDePagoP = ddlFormaPagoP.SelectedValue;
            p.monedaP = ddlMonedaP.SelectedValue;
            p.monto = txtMonto.Text;
            p.nomBancoOrdExt = txtNomBancoOrdExt.Text;
            p.numOperacion = txtNumOperacion.Text;
            p.rfcEmisorCtaBen = txtRfcEmisorCtaBen.Text;
            p.rfcEmisorCtaOrd = txtRfcEmisorCtaOrd.Text;
           // p.selloPago = txtSelloPago.Text;
            p.tipoCadPago = txtTipoCadPago.Text;
            p.tipoCambioP = txtTipoCambioP.Text;

            ddlID.Items.Add(p.id);

            pagos.Add(p);
            ViewState["Pagos"] = pagos;
            BindPagosToGridView();
            
            txtFechaPagoP.Text = "";
            txtMonto.Text = "";
            txtRfcEmisorCtaOrd.Text = "";
            txtCtaOrdenante.Text = "";
            txtCtaBeneficiario.Text = "";
           // txtCertPago.Text = "";
           // txtSelloPago.Text = "";
            txtTipoCambioP.Text = "";
            txtNumOperacion.Text = "";
            txtNomBancoOrdExt.Text = "";
            txtRfcEmisorCtaBen.Text = "";
            txtTipoCadPago.Text = "";
           // txtCadPago.Text = "";
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

        protected void gvPagos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("EliminarPago"))
            {
                var pagos = ViewState["Pagos"] as List<Pagos>;
                var documentos = ViewState["PagoDoctoRelacionado"] as List<PagoDoctoRelacionado>;
                Pagos dt = pagos.ElementAt(Convert.ToInt32(e.CommandArgument));
                string x = dt.id;
                pagos.RemoveAt(Convert.ToInt32(e.CommandArgument));
                ddlID.Items.Remove(x);
                ViewState["Pagos"] = pagos;

                documentos.RemoveAll(p => p.ID == x);
                ViewState["PagoDoctoRelacionado"] = documentos;
                BindPagosToGridView();
                this.BindDocumentosToGridView();
                UpdateTotales();
            }
        }

        protected void gvPagos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cbDoctoRelacionado_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDoctoRelacionado.Checked == true)
            {
                DivDoctoRelacionado.Visible = true;
            }
            else
                DivDoctoRelacionado.Visible = false;
        }

        protected void ddlID_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlMetodoDePagoDR_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlMonedaDR_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnAgregarDocumento_Click(object sender, EventArgs e)
        {
            List<PagoDoctoRelacionado> documentos = ViewState["PagoDoctoRelacionado"] as List<PagoDoctoRelacionado>;

            PagoDoctoRelacionado Docum = new PagoDoctoRelacionado();
            Docum.Folio = txtFolioD.Text;
            Docum.IdDocumento = txtIdDocumento.Text;
            Docum.ImpPagado = txtImpPagado.Text;
            Docum.ImpSaldoAnt = txtImpSaldoAnt.Text;
            Docum.ImpSaldoInsoluto = txtImpSaldoInsoluto.Text;
            Docum.MetodoDePagoDR = ddlMetodoDePagoDR.SelectedValue;
            Docum.MonedaDR = ddlMonedaDR.SelectedValue;
            Docum.NumParcialidad = txtNumParcialidad.Text;
            Docum.Serie = txtSerieD.Text;
            Docum.TipoCambioDR = txtTipoCambioDR.Text;
            Docum.ID = ddlID.SelectedValue;
            //  if (x.DoctoRelacionado == null)
            //   x.DoctoRelacionado = new List<PagoDoctoRelacionado>();
            //   x.DoctoRelacionado.Add(Docum);
            documentos.Add(Docum);
            //      ViewState["Pagos"] = pagos;
            ViewState["PagoDoctoRelacionado"] = documentos;

            BindDocumentosToGridView();

            txtIdDocumento.Text = "";
            txtFolioD.Text = "";
            txtNumParcialidad.Text = "";
            txtImpPagado.Text = "";
            txtSerieD.Text = "";
            txtTipoCambioDR.Text = "";
            txtImpSaldoAnt.Text = "";
            txtImpSaldoInsoluto.Text = "";
        }

        protected void gvDocumento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("EliminarDocumento"))
            {
                var documentos = ViewState["PagoDoctoRelacionado"] as List<PagoDoctoRelacionado>;
                documentos.RemoveAt(Convert.ToInt32(e.CommandArgument));
                ViewState["PagoDoctoRelacionado"] = documentos;
                this.BindDocumentosToGridView();

                //this.UpdateTotales();

            }
        }

        private facturaComplementos llenarComplementoPagos()
        {
            try
            {
                facturaComplementos comple = new facturaComplementos();
                List<Pagos> pagos = ViewState["Pagos"] as List<Pagos>;
                List<PagoDoctoRelacionado> documentos = ViewState["PagoDoctoRelacionado"] as List<PagoDoctoRelacionado>;

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

                comple.pagos = pagos;
                return comple;
            }

            catch (Exception ae)
            {
                return null;
            }
        }

        protected void gvFacturas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("seleccionar"))
            {
                string folio = this.gvFacturas.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
                string uuid = this.gvFacturas.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
                string serie = this.gvFacturas.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
                if (folio == "&nbsp;")
                    folio = "";
                if (serie == "&nbsp;")
                    serie = "";

                txtIdDocumento.Text = uuid;
                txtSerieD.Text = serie;
                txtFolioD.Text = folio;
            }
        }
        protected void gvFacturas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvFacturas.DataSource = ViewState["facturas"];
            this.gvFacturas.PageIndex = e.NewPageIndex;
            this.gvFacturas.DataBind();
           

        }
        protected void gvFacturas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                 if (e.Row.Cells[6].Text == "Cancelado")
                    e.Row.BackColor = Color.FromName("#FEDDB8");
                if (e.Row.Cells[6].Text == "Pendiente")
                    e.Row.BackColor = Color.FromName("#e4e5e7");
                if (e.Row.Cells[6].Text == "Pagado")
                    e.Row.BackColor = Color.FromName("#b3d243");
            }
        }
        protected void BuscarFactura_Click(object sender, EventArgs e)
        {
            FillView();
        }

        private void FillView()
        {
            var sesion = Session["sessionRGV"] as Sesion;
          
      

          var factu = new Factura();

               using (factu as IDisposable)
                {
                   var lista= factu.GetListComplementoPAgos(sesion.Id);
                   
                                    

                    ViewState["facturas"] = lista;

                    this.gvFacturas.DataSource = lista;
                    this.gvFacturas.DataBind();

                   }

        }
        //------------------------
        //------------------------------------------------------------------------------------------
        private void AgregarConceptoUnico()
        {
           ViewState["detalles"] =new List<Datosdetalle>();
            List<Datosdetalle> detalles = ViewState["detalles"] as List<Datosdetalle>;
         
            Datosdetalle detalle = new Datosdetalle()
            {
                Partida = detalles.Count + 1,
                ConceptoCantidad = 1,
                ConceptoDescripcion = "Pago",
                ConceptoClaveProdServ = "84111506",
                ConceptoValorUnitario = 0,
                ConceptoImporte =0,
                Conceptoidproducto =0,
                ConceptoClaveUnidad = "ACT"

            };

            detalles.Add(detalle);
            ViewState["detalles"] = detalles;
        }
        //----------------
    }
}