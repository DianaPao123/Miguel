﻿using System;
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
using System.IO;
using System.Configuration;
using DobleClip;

namespace GAFWEB
{
    public partial class wfrPreFactura : Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            string clientScript = ClientScript.GetPostBackEventReference(btnGenerarFactura, null);
            Botones botonRGV = new Botones();
            botonRGV.PrevenirDoubleEnvio(btnGenerarFactura, clientScript, "Confirma que deseas generar la factura");

        }
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!this.IsPostBack)
            {
                try
                {
                    Session["Cantidad_de_Procesamientos"] = 0;

                    DivDPagos.Style["display"] = "none";
                    txtFechaPagoD.Visible = false;
                    txtMontoPago.Visible = false;

                   // DivDPagos.Visible = false;

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

                  llenarUsoCFDI();
                  ddlUsoCFDI.SelectedValue = "P01";
                     
                  

                    //-----------------------------------------
                  List<Catalogo_Giros> giro = new List<Catalogo_Giros>();
                   int idEmpresa ;
                  var Empresa = new GAFEmpresa();
                  using (Empresa as IDisposable)
                  {
                      giro = Empresa.GetListGiro();
                      ddlGiro.DataSource = giro;
                      ddlGiro.DataBind();
                      this.ddlEmpresa.Items.Clear();
                      this.ddlEmpresa.DataSource = Empresa.GetListGiroempresaBy(Convert.ToInt64( ddlGiro.SelectedValue));
                      this.ddlEmpresa.DataBind();
                      if (ddlEmpresa.Items.Count > 0)
                          idEmpresa = Convert.ToInt16(ddlEmpresa.SelectedValue);
                      else
                          idEmpresa = 0;
                     
                      
                   //  if (ddlEmpresa.Items.Count > 0)
                        {
                           var cliente =new GAFClientes();
                           using (cliente as IDisposable)
                           {
                               var sesion = Session["sessionRGV"] as Sesion;
         
                               ddlClientes.Items.Clear();
                               var clientes = cliente.GetListClientePromotor(sesion.Id);
                               this.ddlClientes.DataSource = clientes;
                               this.ddlClientes.DataBind();
                           }

                           if (ddlEmpresa.Items.Count > 0)
                           {

                               var prefactu = new Prefactura();
                               using (prefactu as IDisposable)
                               {
                                   this.txtFolio.Text = prefactu.GetNextFolio(idEmpresa);
                                   this.txtPreFolio.Text = prefactu.GetNextPreFolio("F", 0);
                               }
                           }
                            ddlClientes_SelectedIndexChanged(null, null);
                        }

                         //------------catalogos grandes----------------------
                      OperacionesCatalogos OP=new OperacionesCatalogos();
                      using (OP as IDisposable)
                      {

                          ddlClaveUnidad.DataSource = OP.ConsultarClaveUnidadMasUsadas();
                         // ddlClaveUnidad.DataSource = OP.ConsultarClaveUnidadAll();
                          ddlClaveUnidad.DataTextField = "Nombre";
                          ddlClaveUnidad.DataValueField = "c_ClaveUnidad1";
                          ddlClaveUnidad.DataBind();
                        //  ddlClaveUnidad.SelectedValue = "H87";


                          ddlClaveUnidadE.DataSource = OP.ConsultarClaveUnidadAll();
                          ddlClaveUnidadE.DataTextField = "Nombre";
                          ddlClaveUnidadE.DataValueField = "c_ClaveUnidad1";
                          ddlClaveUnidadE.DataBind();
                          ddlClaveUnidadE.SelectedValue = "H87";
                          //--------------------------------------------
                          ddlMoneda.DataSource = OP.Consultar_MonedaAll();
                          ddlMoneda.DataTextField = "Descripción";
                          ddlMoneda.DataValueField = "c_Moneda1";
                          ddlMoneda.DataBind();
                          ddlMoneda.SelectedValue = "MXN";
                          CatalogosSAT.c_Moneda mone = OP.Consultar_Moneda(ddlMoneda.SelectedValue);
                          ViewState["DecimalMoneda"] = mone.Decimales;

                          //ddlBancoPago.DataSource = OP.ConsultarBancosAll();
                          //ddlBancoPago.DataTextField = "Descripcion";
                          //ddlBancoPago.DataValueField = "Descripcion";
                          //ddlBancoPago.DataBind();

                       
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
                    ViewState["NombreArchivo"] = "";
                    ViewState["BytesArchivo"] = null;


                   this.BindDetallesToGridView();
                    this.UpdateTotales();
                    //--------------------------------para editar---------------------------------
                    string idPrefacturaString = this.Request.QueryString["idPrefactura"];
                    int idPrefactura;
                    if (!string.IsNullOrEmpty(idPrefacturaString) && int.TryParse(idPrefacturaString, out idPrefactura))
                    {
                        llenarUsoCFDIALL();
                        Editar(idPrefactura);
                    }
                    //-------------------------------fin edicion----------------------
                    else
                    { 

                      MostrarDatosSegunEstatus(Convert.ToInt16( ddlStatusFactura.SelectedValue));
                    }
                    //-----------------
                    idEmpresa = Convert.ToInt16(ddlEmpresa.SelectedValue);
                      
               
                    if (!Empresa.TieneConfiguradoCertificado(idEmpresa))
                    {
                        this.lblError.Text = "Tienes que configurar tus certificados antes de poder facturar";
                        this.btnGenerarFactura.Enabled = this.BtnVistaPrevia.Enabled =
                            this.btnAgregar.Enabled = this.ddlMoneda.Enabled = false;
                        return;
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
             Session["Cantidad_de_Procesamientos"] = Convert.ToInt32(Session["Cantidad_de_Procesamientos"].ToString()) + 1;
             if (Session["Cantidad_de_Procesamientos"].ToString() == "1")
             {
                 this.GuardarFactura();
             }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            
            
            int idCliente = int.Parse(this.ddlClientes.SelectedValue);
            if (string.IsNullOrEmpty(this.txtCantidad.Text.Trim()) ||
               string.IsNullOrEmpty(this.txtPrecio.Text.Trim()) ||
               string.IsNullOrEmpty(this.txtDescripcion.Text.Trim()) )
            {
                this.lblError.Text = "* Error de validación en la partida";
                return;
            }
            this.lblError.Text = string.Empty;
          //  ddlConceptos.Items.Add(this.txtCodigo.Text);

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
                ConceptoUnidad = ddlClaveUnidad.SelectedItem.Text,
                ConceptoValorUnitario = Decimal.Round(decimal.Parse(this.txtPrecio.Text), mon),
                ConceptoImporte = Decimal.Round(decimal.Parse(this.txtPrecio.Text) * decimal.Parse(this.txtCantidad.Text), mon),
                Conceptoidproducto = string.IsNullOrEmpty(txtIdProducto.Value) ? 0 : int.Parse(txtIdProducto.Value),
                //ConceptoNoIdentificacion = this.txtNoIdentificacion.Text,
                ConceptoClaveUnidad = this.ddlClaveUnidad.SelectedItem.Value,
                ConceptoClaveUnidadDescripcion=ddlClaveUnidad.SelectedItem.Text
            };
          
            var descuento = ViewState["descuento"].ToString();
            decimal descuento1 = 0M;
            if (!string.IsNullOrEmpty(descuento))
                descuento1 = Convert.ToDecimal(descuento);

            /*
            if (!string.IsNullOrEmpty(txtDescuento.Text))
            {
                detalle.ConceptoDescuento = decimal.Parse(this.txtDescuento.Text);
                descuento1 = descuento1 + decimal.Parse(this.txtDescuento.Text);

            }
             */ 
            ViewState["descuento"] = descuento1;
      
            detalles.Add(detalle);

            ViewState["detalles"] = detalles;
            AgregarIVA(detalle.Partida.ToString());

            if(cbIVARet.Checked==true)
            AgregarIVARetencion(detalle.Partida.ToString());

            this.txtIdProducto.Value = null;
            this.Clear();
         
            this.BindDetallesToGridView();
        
           this.UpdateTotales();
          
        
         
            
        }

        protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Empresa = new GAFEmpresa();
            lblMensaje.Text = "";
            lblMensajeHistorico.Text = "";

            lblError.Text = "";


            this.btnGenerarFactura.Enabled = this.BtnVistaPrevia.Enabled =
                    this.btnAgregar.Enabled = this.ddlMoneda.Enabled = true;
            var prefactu = new Prefactura();
            using (prefactu as IDisposable)
            {
                this.txtFolio.Text = prefactu.GetNextFolio(Convert.ToInt32(ddlEmpresa.SelectedValue));
            }
               
            using (Empresa as IDisposable)
            {
                if (!Empresa.TieneConfiguradoCertificado(Convert.ToInt32(ddlEmpresa.SelectedValue)))
                {
                    this.lblError.Text = "Tienes que configurar tus certificados antes de poder facturar";
                    this.btnGenerarFactura.Enabled = this.BtnVistaPrevia.Enabled =
                        this.btnAgregar.Enabled = this.ddlMoneda.Enabled = false;
                    return;
                }

                else
                {
                    this.lblError.Text = "";
                    this.btnGenerarFactura.Enabled = this.BtnVistaPrevia.Enabled =
                            this.btnAgregar.Enabled = this.ddlMoneda.Enabled = true;
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
        }

        protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cliente =new  GAFClientes();
            if (string.IsNullOrEmpty(this.ddlClientes.SelectedValue))
                return;
            using (cliente as IDisposable)
            {
                int idCliente = int.Parse(this.ddlClientes.SelectedValue);
                Contract.clientes c = cliente.GetCliente(idCliente);
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
                string x = dt.Partida.ToString();
                decimal desc = 0M;
                if (dt.ConceptoDescuento != null)
                    desc = (decimal)dt.ConceptoDescuento;

                detalles.RemoveAt(Convert.ToInt32(e.CommandArgument));
                //ddlConceptos.Items.Remove(x);  se quito porque no iba en la vista
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
                this.ddlClaveUnidadE.SelectedValue = edicion.ConceptoClaveUnidad;
               
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
                        Contract.producto prod = null;//cliente.ObtenerProductoById(idProducto);

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
                var fact = GetFactura(iniciales,detalles);
                try
                {
                    /*
                    var rol = Session["rol"];
                    rol = "Promotores";
                 
                    if (rol == "Operadores")
                    {
                        GAFFactura fac = new GAFFactura();
                        if (!fac.GuardarFactura33(fact, true, null))
                        {
                            this.lblError.Text = "* Error al generar la factura";
                            return;
                        }
                        this.lblError.Text = string.Empty;

                    }
                    else
                    {
                     */

                    GAFFactura pre = new GAFFactura();
                    //    Prefactura pre = new Prefactura();
                        using (pre as IDisposable)
                        {
                            string s = "";
                            string idPrefacturaString = this.Request.QueryString["idPrefactura"];
                            if(!string.IsNullOrEmpty(idPrefacturaString))
                               s= pre.GuardarPrefactura(fact, Convert.ToInt64(idPrefacturaString),null);
                            else
                            s=pre.GuardarPrefactura(fact,0,null);
                            var prefactu = new Prefactura();
                            using (prefactu as IDisposable)
                            {
                                int idEmpresa = int.Parse(this.ddlEmpresa.SelectedValue);
                                this.txtFolio.Text = prefactu.GetNextFolio(idEmpresa);
                               
                            }

                            if (s != "OK")
                            {
                                error = true;
                                this.lblError.Text = s;
                            }
                            else
                                this.ClearAll();
                        }

                    //}
                   
                  
                   
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
                    ActualizarSaldosMaster();
                }
               
               // this.lblError.Text = string.Empty;
            }
            Session["Cantidad_de_Procesamientos"] = 0;
        }

        private DatosPrefactura  GetFactura(string iniciales, List<Datosdetalle> detalles)
        {

            var sesion = Session["sessionRGV"] as Sesion;
          
            DatosPrefactura fact = new DatosPrefactura();
            if (ddlStatusFactura.SelectedValue != "1")
                fact.CFDI = "G";
            else
                fact.CFDI = "P";
                               fact.TipoDocumento = TipoDocumento.FacturaGeneral;
                               fact.IdEmpresa = int.Parse(this.ddlEmpresa.SelectedValue);
                               fact.Importe = decimal.Parse(this.lblTotal.Text, NumberStyles.Currency);
                               fact.SubTotal = decimal.Parse(this.lblSubtotal.Text, NumberStyles.Currency);
                               fact.Total = decimal.Parse(this.lblTotal.Text, NumberStyles.Currency);
                               fact.MonedaID = this.ddlMoneda.SelectedValue;
                               fact.idcliente = int.Parse(this.ddlClientes.SelectedValue);
                               fact.Fecha = DateTime.Now;
                               fact.Folio = this.txtFolio.Text.PadLeft(4, '0');
                               fact.PreFolio = txtPreFolio.Text;
                           //    fact.Serie = string.IsNullOrEmpty(this.txtSerie.Text) ? null : this.txtSerie.Text;
                              // nProducto = detalles.Count;
                               fact.captura = DateTime.Now;
                               fact.Cancelado = 0;
                               fact.Usuario = sesion.Id;// Guid.Parse("33760C0C-E45C-4210-8081-81C80827FA73");// System.Guid.NewGuid(); ///cambiar al verdadero
                               //fact.LugarExpedicion = this.ddlSucursales.SelectedValue;
                               fact.Proyecto = this.txtProyecto.Text;
                               fact.MonedaS = this.ddlMoneda.SelectedItem.Text;

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
                               fact.UsoCFDI=this.ddlUsoCFDI.SelectedValue;
                               fact.Estatus =Convert.ToInt16( ddlStatusFactura.SelectedValue);


                          var Empresa = new GAFEmpresa();
                          using (Empresa as IDisposable)
                          {
                            var emp=   Empresa.GetById( Convert.ToInt16( this.ddlEmpresa.SelectedValue));
                            fact.LugarExpedicion = emp.CP;
                          }

                          fact.TipoDeComprobante = ddlTipoDocumento.SelectedValue;

             if(ddlFormaPago.SelectedValue!="00")
             {    fact.FormaPagoID = this.ddlFormaPago.SelectedValue;
                 fact.FormaPago= this.ddlFormaPago.SelectedItem.Text;
             }
             if (ddlMetodoPago.SelectedValue != "00")
             {
                 fact.MetodoID = this.ddlMetodoPago.SelectedValue;
                 fact.Metodo = this.ddlMetodoPago.SelectedItem.Text;
             }
             if (!string.IsNullOrEmpty(this.txtTipoCambio.Text) )
                 fact.TipoCambio =this.txtTipoCambio.Text;
           //  if (!string.IsNullOrEmpty(this.txtConfirmacion.Text))
           //      fact.Confirmacion = this.txtConfirmacion.Text;
          //   if (!string.IsNullOrEmpty(this.txtCondicionesPago.Text))
          //       fact.CondicionesPago = this.txtCondicionesPago.Text;
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

               List<string> CfdiRelacionado = ViewState["CfdiRelacionado"] as List<string>;

               if (CfdiRelacionado!=null)
                   if (CfdiRelacionado.Count() > 0)
                   {
                       fact.UUID = CfdiRelacionado;
                       fact.TipoRelacion = ddlTipoRelacion.SelectedValue;
                   }
            /*
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
             
            fact.IVA = Convert.ToDecimal(lblTraslados.Text.Replace("$", ""));

            //--------------------
            //---------
            List<DatosPagos> DPagos = new List<DatosPagos>();
                
             if (ddlStatusFactura.SelectedValue == "1") //par los pagos de promotores
             {
                DatosPagos DP = new DatosPagos();
                 DP.Banco = ddlBancoPago.SelectedValue;
                 DP.Fecha =Convert.ToDateTime( txtFechaPagoD.Text);
                 DP.Monto =Convert.ToDecimal( txtMontoPago.Text);
                 DP.Tipo = "";

               string nombreArchivo=  ViewState["NombreArchivo"] as string ;
               var archivo=  ViewState["BytesArchivo"] as byte[];

               if (!string.IsNullOrEmpty(nombreArchivo) && archivo == null)
               { DP.Tipo = nombreArchivo; }
               else
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
                       DP.Tipo = ruta;

                   }
                 
                     DPagos.Add(DP);
                 fact.Datospagos = DPagos;
             }

             /*
             List<DatosPagos> DPagos = ViewState["DPagos"] as List<DatosPagos>;
             DatosPagos DP = new DatosPagos();
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
            

             if (DPagos != null)
                 if (DPagos.Count > 0)
                     fact.Datospagos = DPagos;
              */ 

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
            this.txtCodigo.Text = this.txtDescripcion.Text = this.txtDetalles.Text = this.txtPrecio.Text =
                this.txtCantidad.Text = /*this.txtUnidad.Text = txtNoIdentificacion.Text =txtDescuento.Text=*/ string.Empty;
        }

        private void ClearAll()
        {
            this.Clear();

            this.txtProyecto.Text = this.txtFolioOriginal.Text = this.txtFechaOriginal.Text = this.txtMontoOriginal.Text =
             txtProyecto.Text = /*this.txtSerie.Text = */  string.Empty;
            this.ddlStatusFactura.SelectedIndex = 0;
            this.txtFechaPago.Text = "";
            this.txtFechaPago.Visible = false;
           // this.lblIva.Text = 0M.ToString("C");
            this.lblTraslados.Text = 0M.ToString("C");
            //this.lblRetenciones.Text = 0M.ToString("C");
          //  lblDescuento.Text = 0M.ToString("C");
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
           
               
            this.BindDetallesToGridView();
            this.BindDetallesImpuestosToGridView();
            this.BindCfdiRelacionadoToGridView();
           // this.BindDPagosToGridView();

            var prefactu = new Prefactura();
            using (prefactu as IDisposable)
            {
                int idEmpresa = int.Parse(this.ddlEmpresa.SelectedValue);
                this.txtFolio.Text = prefactu.GetNextFolio(idEmpresa);
                    this.txtPreFolio.Text = prefactu.GetNextPreFolio("F",0);
                    ddlTipoDocumento.SelectedValue = "Ingreso";
         
            }
           
            //cbImpuestos.Checked = false;
            cbCfdiRelacionados.Checked = false;
        }

        private void BindDetallesToGridView()
        {
            
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
        private void BindCfdiRelacionadoToGridView()
        {
            List<string> CfdiRelacionado = ViewState["CfdiRelacionado"] as List<string>;
            if (CfdiRelacionado != null && CfdiRelacionado.Count > 0)
            {
                int noColumns = this.gvCfdiRelacionado.Columns.Count;
                this.gvCfdiRelacionado.Columns[noColumns - 1].Visible =  true;
            }
            else
            {
                int noColumns = this.gvCfdiRelacionado.Columns.Count;
                this.gvCfdiRelacionado.Columns[noColumns - 1].Visible = false;
            }


            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("UUID");
            int t = 0;
            foreach (var array in CfdiRelacionado)
            {
                DataRow row1 = table.NewRow();
                row1["ID"] = t+1;
                row1["UUID"] = array;
                table.Rows.Add(row1);
                t++;
            }
           
            this.gvCfdiRelacionado.DataSource = table; 
            this.gvCfdiRelacionado.DataBind();
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

      
            //this.lblDescuento.Text = descuento1.ToString("C", cul);
            this.lblRetenciones.Text = retenciontotal.ToString("C", cul);
            this.lblTraslados.Text = trasladototal.ToString("C", cul);

                this.lblTotal.Text = total.ToString("C", cul);
                this.lblSubtotal.Text = subtotal.ToString("C", cul);
     
        }

        #endregion

        protected void gvBuscarConceptos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
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

            MostrarDatosSegunEstatus(Convert.ToInt16(ddlStatusFactura.SelectedValue));
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
            AgregarIVAEDICION(edicion.Partida.ToString());
         
            this.BindDetallesToGridView();
            this.UpdateTotales();
              
        }

        protected void ddlMetodoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMetodoPago.SelectedValue == "PPD")
            {
                divParcialidades.Visible = true;
            }

            else
            {
                divParcialidades.Visible = false;

            }

        }

        protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlTipoDocumento.SelectedValue == "Donativo")
            //{
            //    trDonativo.Visible = true;
            //}
            //else trDonativo.Visible = false;

            var prefactu = new Prefactura();
            using (prefactu as IDisposable)
            {
                if (ddlTipoDocumento.SelectedValue == "Ingreso")
                {
                    this.txtPreFolio.Text = prefactu.GetNextPreFolio("F",0);
                }
                if (ddlTipoDocumento.SelectedValue == "Egreso")
                {
                    this.txtPreFolio.Text = prefactu.GetNextPreFolio("NC",0);
                }
            }

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
        {/*
            
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
            this.gvBuscarConceptosHistorico.DataSource = new List<Contract.producto>();
            this.gvBuscarConceptosHistorico.DataBind();
        }

        protected void gvBuscarConceptosHistorico_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
            int idProducto = Convert.ToInt32(this.gvBuscarConceptosHistorico.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);
            var Producto = new GAFProducto();
            using (Producto as IDisposable)
            {

                Contract.producto prod = Producto.GetProduct(idProducto);
                this.txtIdProducto.Value = idProducto.ToString();
                this.txtCantidad.Text = "1";
                this.txtCodigo.Text = prod.Codigo;
                this.txtDescripcion.Text = prod.Descripcion;
               // this.txtUnidad.Text = prod.Unidad;
                this.txtPrecio.Text = prod.PrecioP.HasValue ? prod.PrecioP.Value.ToString() : string.Empty;
            }
            this.mpeBuscarConceptoHistorico.Hide();
    

        }

        protected void btnBuscarConceptoHistorico_Click(object sender, EventArgs e)
        {
            var idEmpresa = int.Parse(ddlEmpresa.SelectedValue);
          
              var Producto = new GAFProducto();
                    using (Producto as IDisposable)
                    {

                        List<Contract.producto> lista = Producto.ProductSearch(this.txtConceptoHistoricoBusqueda.Text, idEmpresa);
                if (lista.Count > 20)
                {
                    this.lblMensajeHistorico.Text = lista.Count + " resultados, mostrando los primeros 20, refina tu busqueda";
                }
                this.gvBuscarConceptosHistorico.DataSource = new List<Contract.producto>(lista.Take(20).ToList());
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
            this.gvBuscarConceptos.DataSource = new List<Contract.producto>();
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

        protected void cbCfdiRelacionados_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCfdiRelacionados.Checked == true)
            {
                DivCfdiRelacionados.Visible = true;
            }
            else
            {
                DivCfdiRelacionados.Visible = false;
            
            }
         
        }

        protected void btnCfdiRelacionado_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUUDI.Text))
            {
                List<string> CfdiRelacionado = ViewState["CfdiRelacionado"] as List<string>;
                CfdiRelacionado.Add(txtUUDI.Text);
                              ViewState["CfdiRelacionado"] = CfdiRelacionado;
                this.BindCfdiRelacionadoToGridView();

                txtUUDI.Text = "";
            }

        }

        protected void gvCfdiRelacionado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("EliminarCfdiRelacionado"))
            {
                var CfdiRelacionado = ViewState["CfdiRelacionado"] as List<string>;
                CfdiRelacionado.RemoveAt(Convert.ToInt32(e.CommandArgument));
                ViewState["CfdiRelacionado"] = CfdiRelacionado;
                this.BindCfdiRelacionadoToGridView();
   
            }
       
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
                    //ddlClientes.Items.Clear();
                    //var clientes = cliente.GetList(idEmpresa, string.Empty, false);
                    //this.ddlClientes.DataSource = clientes;
                    //this.ddlClientes.DataBind();

                    var sesion = Session["sessionRGV"] as Sesion;

                    ddlClientes.Items.Clear();
                    var clientes = cliente.GetListClientePromotor(sesion.Id);
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

      
        protected void btnAgregarDpago_Click(object sender, EventArgs e)
        {
            /*
            List<DatosPagos> DPagos = ViewState["DPagos"] as List<DatosPagos>;
            DatosPagos DP = new DatosPagos();
            DP.Tipo = "F";
            DP.Descripcion = txtDPObservaciones.Text;
            DP.Fecha = Convert.ToDateTime( txtDPFecha.Text);
            DP.MetododePago = ddlDPMetodoPago.SelectedValue;
            DP.Monto =Convert.ToDecimal( txtDPMonto.Text);
            if (!string.IsNullOrEmpty(txtClaveBancaria.Text))
            {
                DP.Banco = ddlBanco.SelectedValue;
                DP.ClaveBancaria = txtClaveBancaria.Text;
            }
            DPagos.Add(DP);
            ViewState["DPagos"] = DPagos;
            BindDPagosToGridView();
            txtDPObservaciones.Text = "";
            txtDPFecha.Text = "";
            txtDPMonto.Text = "";
            txtClaveBancaria.Text = "";
             */ 
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
                        
                        ddlClientes_SelectedIndexChanged(null, null);
                    }
                    /*
                    var Sucursal = new GAFSucursales();

                    using (Sucursal as IDisposable)
                    {
                        ddlSucursales.Items.Clear();
                        this.ddlSucursales.DataSource = Sucursal.GetSucursalLista(fact.IdEmpresa);
                        ddlSucursales.DataValueField = "LugarExpedicion";
                        ddlSucursales.DataTextField = "Nombre";
                        this.ddlSucursales.DataBind();
                        ddlSucursales.SelectedValue = fact.LugarExpedicion;
                    }
                     */ 
                    //txtSerie.Text = fact.Serie;
                    if (fact.CFDI == "P")
                    {

                       ddlStatusFactura.SelectedValue = "1";
                       MostrarDatosSegunEstatus(Convert.ToInt16(ddlStatusFactura.SelectedValue));
                       DivDPagos.Visible = true;
                       var montoPago= pre.GetPagoCFDIPromotor(id);
                       txtFechaPagoD.Text = montoPago.fecha.Value.ToShortDateString();
                       txtMontoPago.Text = montoPago.Monto.ToString();
                       ddlBancoPago.Text = montoPago.Banco;
                       ViewState["NombreArchivo"] = montoPago.RutaImagen;


                    }
                    txtFolio.Text = fact.Folio;
                    txtPreFolio.Text = fact.PreFolio;
                    ddlTipoDocumento.SelectedValue = fact.TipoDeComprobante;
                  //  txtConfirmacion.Text = fact.Confirmacion;
                    ddlMoneda.SelectedValue = fact.MonedaID;
                    if (!string.IsNullOrEmpty(fact.TipoCambio))
                    {
                        txtTipoCambio.Visible = true;
                        txtTipoCambio.Text = fact.TipoCambio;
                    }
                    ddlFormaPago.SelectedValue = fact.FormaPagoID;
                    ddlMetodoPago.SelectedValue = fact.MetodoID;
                   // txtCondicionesPago.Text = fact.CondicionesPago;
                    ddlUsoCFDI.SelectedValue = fact.UsoCFDI;
                    txtProyecto.Text = fact.Proyecto;
                    ViewState["descuento"] = fact.Descuento;
                   

                    //..............cfdi relaionados------------------
                    List<string> CfdiRelacionado = ViewState["CfdiRelacionado"] as List<string>;

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
                               // ddlConceptos.Items.Add(x.ConceptoClaveProdServ);

                                if(x.ConceptoRetenciones!=null)
                                    if (x.ConceptoRetenciones.Count() > 0)
                                    {
                                        cbIVARet.Checked = true;
                                        cbIVARet.Visible = true;
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
                                            DRT.ConceptoClaveProdServ = x.Partida.ToString();
                                            detallesImpuestos.Add(DRT);
                                        }
                                    }

                           }

                            ViewState["detallesImpuestos"] = detallesImpuestos;
                            if(detallesImpuestos!=null)
                                if (detallesImpuestos.Count() > 0)
                                {
                                  //  cbImpuestos.Checked = true;
                                  //  DivComplementos.Visible = true;
                                  //  cbImpuestos_CheckedChanged(null, null);
                                 //   this.BindDetallesImpuestosToGridView();
                                    this.UpdateTotales();
                                }
                        }
                    /*
                    List<DatosPagos> DPagos = ViewState["DPagos"] as List<DatosPagos>;
                    if(fact.Datospagos!=null)
                        if (fact.Datospagos.Count() > 0)
                        {
                            foreach (var dp in fact.Datospagos)
                            {
                                if (dp.Tipo == "F")
                                {
                                    DatosPagos DP = new DatosPagos();

                                    DP.Banco = dp.Banco;
                                    DP.ClaveBancaria = dp.ClaveBancaria;
                                    DP.Descripcion = dp.Descripcion;
                                    DP.Fecha = dp.Fecha;
                                    DP.MetododePago = dp.MetododePago;
                                    DP.Monto = dp.Monto;
                                    DP.Tipo = "F";
                                    DPagos.Add(DP);
                                }
                                else
                                {
                                   
                                  //  DP.Tipo = "I";
                                     txtObservacionesComi.Text=dp.Descripcion ;
                                    txtFechaComi.Text=dp.Fecha.ToShortDateString();
                                     ddlDPMetodoPagoComi.SelectedValue=dp.MetododePago ;
                                    txtMontoComi.Text=dp.Monto.ToString();
                                    if (!string.IsNullOrEmpty(dp.ClaveBancaria))
                                    {
                                       ddlBancoComi.SelectedValue=  dp.Banco ;
                                        txtClabeComi.Text=dp.ClaveBancaria ;
                                    }
                            
                                
                                }
                            }
                            ViewState["DPagos"] = DPagos;
                            BindDPagosToGridView();
            

                        }
                    */
                    return 1;
                }
            }
        
        }

        protected void ddlClaveUnidad_SelectedIndexChanged(object sender, EventArgs e)
        {
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
        }
       
        //------------------------------------------------------------------------------
        private void MostrarDatosSegunEstatus(int status)
        {
            if (status == 1)//pagado
            {
                ddlFormaPago.Items.Clear();
                 //ListItem L0 = new ListItem();
                // L0.Value = "00";
                // L0.Text= "Seleccionar";
                // ddlFormaPago.Items.Insert(0, L0);
                 ListItem L1 = new ListItem();
                 L1.Value = "01";
                 L1.Text = "Efectivo";
                 ddlFormaPago.Items.Insert(0, L1);
                 ListItem L2 = new ListItem();
                 L2.Value = "02";
                 L2.Text = "Cheque nominativo";
                 ddlFormaPago.Items.Insert(1, L2);
                 ListItem L3 = new ListItem();
                 L3.Value = "03";
                 L3.Text = "Transferencia electrónica de fondos";
                 ddlFormaPago.Items.Insert(2, L3);
                //----------------------------------
                 ddlMetodoPago.Items.Clear();
                 //ListItem M0 = new ListItem();
                 //M0.Value = "00";
                 //M0.Text = "Seleccionar";
                 //ddlMetodoPago.Items.Insert(0, M0);
                 ListItem M1 = new ListItem();
                 M1.Value = "PUE";
                 M1.Text = "Pago en una sola exhibición";
                 ddlMetodoPago.Items.Insert(0, M1);
                 // DivDPagos.Visible = true;
        
                 DivDPagos.Style["display"] = "block";
                 txtFechaPagoD.Visible = true;
                 txtMontoPago.Visible = true;
                
            }
            else
            {
                ddlFormaPago.Items.Clear();
                 ListItem L1 = new ListItem();
                L1.Value = "99";
                L1.Text = "Por definir";
                ddlFormaPago.Items.Insert(0, L1);
                //-------------------------
                ddlMetodoPago.Items.Clear();
                ListItem M1 = new ListItem();
                M1.Value = "PPD";
                M1.Text = "Pago en parcialidades o diferido";
                ddlMetodoPago.Items.Insert(0, M1);
               // DivDPagos.Visible = false;
                DivDPagos.Style["display"] = "none";
                txtFechaPagoD.Visible = false;
                txtMontoPago.Visible = false;
            
               
            }
        
        }

       //------------------------------------------------------------------------------------------
        private void AgregarIVA( string partida)
        {
        List<Datosdetalle> detalles = ViewState["detalles"] as List<Datosdetalle>; 
            List<DatosdetalleRT> detallesImpuestos = ViewState["detallesImpuestos"] as List<DatosdetalleRT>;
                 string  m= ViewState["DecimalMoneda"].ToString() ;
                 int mon = 0;
                 if (!string.IsNullOrEmpty(m))
                     mon = Convert.ToInt16(m);

            foreach(var x in detalles)
            {

                if (x.Partida.ToString() == partida)
                {
                    //-------llenamos el objeto de impuestos RT------------------------
                    DatosdetalleRT DRT = new DatosdetalleRT();
                    DRT.Base = Decimal.Round(Convert.ToDecimal(this.txtPrecio.Text) * Convert.ToDecimal(this.txtCantidad.Text), mon);
                    if (cbIva.Checked == true)
                    {
                        DRT.TasaOCuota = "0.160000";
                        DRT.Importe = Decimal.Round(DRT.Base * Convert.ToDecimal(0.160000), mon);
                    }
                    else
                    {
                        DRT.TasaOCuota = "0.000000";
                        DRT.Importe = Decimal.Round(DRT.Base * Convert.ToDecimal(0.000000), mon);
                    }
                    DRT.Impuesto = "002";//IVA

                    DRT.TipoFactor = "Tasa";
                    DRT.TipoImpuesto = "Traslados";
                    DRT.ConceptoClaveProdServ = partida;
                    //-----------------fin objeto impuestos RT------------------------
                    //-----se llenan los impusetos reales----------------------------
                 
                   // if (ddlTipoImpuesto.SelectedValue == "Traslados")
                    {
                        if (x.ConceptoTraslados == null)
                            x.ConceptoTraslados = new List<DatosdetalleTraslado>();
                        DatosdetalleTraslado traslados = new DatosdetalleTraslado();
                        traslados.Base = Decimal.Round(Convert.ToDecimal(txtPrecio.Text) * Convert.ToDecimal(this.txtCantidad.Text), mon);
                        if (cbIva.Checked == true)
                        {
                            traslados.TasaOCuota = "0.160000";
                            traslados.Importe = Decimal.Round(traslados.Base * Convert.ToDecimal(0.160000), mon);
                        }
                        else
                        {
                            traslados.TasaOCuota = "0.000000";
                            traslados.Importe = Decimal.Round(traslados.Base * Convert.ToDecimal(0.000000), mon);
                        }

                        traslados.Impuesto = "002";
                        traslados.TipoFactor = "Tasa";
                        x.ConceptoTraslados.Add(traslados);
                        x.IVA = (decimal)traslados.Importe;//nuevo para iva por concepto
        
                    }
                    //-------------------------------fin impuestos reales----------------
                    detallesImpuestos.Add(DRT);
                }
                

            }
            ViewState["detalles"] = detalles;
             ViewState["detallesImpuestos"] = detallesImpuestos;
             
      
       }
        private void AgregarIVARetencion(string partida)
        {
            List<Datosdetalle> detalles = ViewState["detalles"] as List<Datosdetalle>;
            List<DatosdetalleRT> detallesImpuestos = ViewState["detallesImpuestos"] as List<DatosdetalleRT>;
            string m = ViewState["DecimalMoneda"].ToString();
            int mon = 0;
            if (!string.IsNullOrEmpty(m))
                mon = Convert.ToInt16(m);

            foreach (var x in detalles)
            {

                if (x.Partida.ToString() == partida)
                {
                    //-------llenamos el objeto de impuestos RT------------------------
                    DatosdetalleRT DRT = new DatosdetalleRT();
                    DRT.Base = Decimal.Round(Convert.ToDecimal(this.txtPrecio.Text) * Convert.ToDecimal(this.txtCantidad.Text), mon);
                    DRT.TasaOCuota = "0.060000";
                    DRT.Importe = Decimal.Round(DRT.Base * Convert.ToDecimal(0.060000), mon);
                    DRT.Impuesto = "002";//IVA

                    DRT.TipoFactor = "Tasa";
                    DRT.TipoImpuesto = "Retenciones";
                    DRT.ConceptoClaveProdServ = partida;
                    //-----------------fin objeto impuestos RT------------------------
                    //-----se llenan los impusetos reales----------------------------

                    // if (ddlTipoImpuesto.SelectedValue == "Traslados")
                    {
                        if (x.ConceptoRetenciones == null)
                            x.ConceptoRetenciones = new List<DatosdetalleRetencion>();
                        DatosdetalleRetencion traslados = new DatosdetalleRetencion();
                        traslados.Base = Decimal.Round(Convert.ToDecimal(txtPrecio.Text) * Convert.ToDecimal(this.txtCantidad.Text), mon);
                        traslados.TasaOCuota = "0.060000";
                        traslados.Importe = Decimal.Round(traslados.Base * Convert.ToDecimal(0.060000), mon);
                      
                        traslados.Impuesto = "002";
                        traslados.TipoFactor = "Tasa";
                        x.ConceptoRetenciones.Add(traslados);
                        x.IVA = (decimal)traslados.Importe;//nuevo para iva por concepto

                    }
                    //-------------------------------fin impuestos reales----------------
                    detallesImpuestos.Add(DRT);
                }


            }
            ViewState["detalles"] = detalles;
            ViewState["detallesImpuestos"] = detallesImpuestos;


        }
     
        private void AgregarIVAEDICION(string partida)
        {
            List<Datosdetalle> detalles = ViewState["detalles"] as List<Datosdetalle>;
            List<DatosdetalleRT> detallesImpuestos = ViewState["detallesImpuestos"] as List<DatosdetalleRT>;
            string m = ViewState["DecimalMoneda"].ToString();
            int mon = 0;
            if (!string.IsNullOrEmpty(m))
                mon = Convert.ToInt16(m);

            foreach (var x in detalles)
            {

                if (x.Partida.ToString() == partida)
                {
                    //-------llenamos el objeto de impuestos RT------------------------
                    DatosdetalleRT DRT = new DatosdetalleRT();
                    DRT.Base = Decimal.Round(Convert.ToDecimal(x.ConceptoValorUnitario) * Convert.ToDecimal(x.ConceptoCantidad), mon);
                    if (x.IVA > 0)
                    {
                        DRT.TasaOCuota = "0.160000";
                        DRT.Importe = Decimal.Round(DRT.Base * Convert.ToDecimal(0.160000), mon);
                    }
                    else
                    {
                        DRT.TasaOCuota = "0.000000";
                        DRT.Importe = Decimal.Round(DRT.Base * Convert.ToDecimal(0.000000), mon);
                    }
                    DRT.Impuesto = "002";//IVA

                    DRT.TipoFactor = "Tasa";
                    DRT.TipoImpuesto = "Traslados";
                    DRT.ConceptoClaveProdServ = partida;
                    //-----------------fin objeto impuestos RT------------------------
                    //-----se llenan los impusetos reales----------------------------

                    // if (ddlTipoImpuesto.SelectedValue == "Traslados")
                    {
                        if (x.ConceptoTraslados == null)
                            x.ConceptoTraslados = new List<DatosdetalleTraslado>();
                        DatosdetalleTraslado traslados = new DatosdetalleTraslado();
                        traslados.Base = Decimal.Round(Convert.ToDecimal(x.ConceptoValorUnitario) * Convert.ToDecimal(x.ConceptoCantidad), mon);
                        if (cbIva.Checked == true)
                        {
                            traslados.TasaOCuota = "0.160000";
                            traslados.Importe = Decimal.Round(traslados.Base * Convert.ToDecimal(0.160000), mon);
                        }
                        else
                        {
                            traslados.TasaOCuota = "0.000000";
                            traslados.Importe = Decimal.Round(traslados.Base * Convert.ToDecimal(0.000000), mon);
                        }

                        traslados.Impuesto = "002";
                        traslados.TipoFactor = "Tasa";

                        foreach (var it in x.ConceptoTraslados) //solo un iva
                        {

                                it.Base = DRT.Base;
                                it.Importe = DRT.Importe;
                                it.Impuesto = DRT.Impuesto;
                                it.TasaOCuota = DRT.TasaOCuota;
                                it.TipoFactor = DRT.TipoFactor;
                                 //de1tallesImpuestos.Add(DRT);
                            
                        }
                        //x.ConceptoTraslados.Add(traslados);
                        x.IVA = (decimal)traslados.Importe;//nuevo para iva por concepto

                    }
                    //-------------------------------fin impuestos reales----------------
                    foreach (var xx in detallesImpuestos)
                    {

                        if (xx.ConceptoClaveProdServ == partida)
                        {
                            xx.Base = DRT.Base;
                            xx.ConceptoClaveProdServ = DRT.ConceptoClaveProdServ;
                            xx.Importe = DRT.Importe;
                            xx.Impuesto = DRT.Impuesto;
                            xx.TasaOCuota = DRT.TasaOCuota;
                            xx.TipoFactor = DRT.TipoFactor;
                            xx.TipoImpuesto = DRT.TipoImpuesto;
                            //detallesImpuestos.Add(DRT);
                        }
                    }
                }


            }
            ViewState["detalles"] = detalles;
            ViewState["detallesImpuestos"] = detallesImpuestos;


        }

        private void AgregarIVARetencionEDICION(string partida)
        {
            List<Datosdetalle> detalles = ViewState["detalles"] as List<Datosdetalle>;
            List<DatosdetalleRT> detallesImpuestos = ViewState["detallesImpuestos"] as List<DatosdetalleRT>;
            string m = ViewState["DecimalMoneda"].ToString();
            int mon = 0;
            if (!string.IsNullOrEmpty(m))
                mon = Convert.ToInt16(m);

            foreach (var x in detalles)
            {

                if (x.Partida.ToString() == partida)
                {
                    //-------llenamos el objeto de impuestos RT------------------------
                    DatosdetalleRT DRT = new DatosdetalleRT();
                    DRT.Base = Decimal.Round(Convert.ToDecimal(x.ConceptoValorUnitario) * Convert.ToDecimal(x.ConceptoCantidad), mon);
                    DRT.TasaOCuota = "0.060000";
                    DRT.Importe = Decimal.Round(DRT.Base * Convert.ToDecimal(0.060000), mon);
                    DRT.Impuesto = "002";//IVA

                    DRT.TipoFactor = "Tasa";
                    DRT.TipoImpuesto = "Retenciones";
                    DRT.ConceptoClaveProdServ = partida;
                    //-----------------fin objeto impuestos RT------------------------
                    //-----se llenan los impusetos reales----------------------------

                    // if (ddlTipoImpuesto.SelectedValue == "Traslados")
                    {
                        if (x.ConceptoRetenciones == null)
                            x.ConceptoRetenciones = new List<DatosdetalleRetencion>();
                        DatosdetalleRetencion retencion = new DatosdetalleRetencion();
                        retencion.Base = Decimal.Round(Convert.ToDecimal(x.ConceptoValorUnitario) * Convert.ToDecimal(x.ConceptoCantidad), mon);
                            retencion.TasaOCuota = "0.060000";
                            retencion.Importe = Decimal.Round(retencion.Base * Convert.ToDecimal(0.060000), mon);
                      
                        retencion.Impuesto = "002";
                        retencion.TipoFactor = "Tasa";

                        foreach (var it in x.ConceptoRetenciones) //solo un iva
                        {

                            it.Base = DRT.Base;
                            it.Importe = DRT.Importe;
                            it.Impuesto = DRT.Impuesto;
                            it.TasaOCuota = DRT.TasaOCuota;
                            it.TipoFactor = DRT.TipoFactor;
                            //de1tallesImpuestos.Add(DRT);

                        }
                        //x.ConceptoTraslados.Add(traslados);
                       // x.IVA = (decimal)retencion.Importe;//nuevo para iva por concepto

                    }
                    //-------------------------------fin impuestos reales----------------
                    foreach (var xx in detallesImpuestos)
                    {

                        if (xx.ConceptoClaveProdServ == partida)
                        {
                            xx.Base = DRT.Base;
                            xx.ConceptoClaveProdServ = DRT.ConceptoClaveProdServ;
                            xx.Importe = DRT.Importe;
                            xx.Impuesto = DRT.Impuesto;
                            xx.TasaOCuota = DRT.TasaOCuota;
                            xx.TipoFactor = DRT.TipoFactor;
                            xx.TipoImpuesto = DRT.TipoImpuesto;
                            //detallesImpuestos.Add(DRT);
                        }
                    }
                }


            }
            ViewState["detalles"] = detalles;
            ViewState["detallesImpuestos"] = detallesImpuestos;


        }


        protected void ddlClaveUnidadE_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtUnidadEdita.Text = ddlClaveUnidadE.SelectedItem.Text;
            mpeEdita.Show();
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
        private void llenarUsoCFDIALL()
        {
            ddlUsoCFDI.Items.Clear();

            ListItem cl = new ListItem();
            cl.Text = "Adquisición de mercancias";
            cl.Value = "G01";
            ddlUsoCFDI.Items.Add(cl);
            ListItem cl1 = new ListItem();
            cl1.Text = "Devoluciones, descuentos o bonificaciones";
            cl1.Value = "G02";
            ddlUsoCFDI.Items.Add(cl1);
            ListItem cl2 = new ListItem();
            cl2.Text = "Gastos en general";
            cl2.Value = "G03";
            ddlUsoCFDI.Items.Add(cl2);
            ListItem cl3 = new ListItem();
            cl3.Text = "Construcciones";
            cl3.Value = "I01";
            ddlUsoCFDI.Items.Add(cl3);
            ListItem cl4 = new ListItem();
            
            cl4.Text = "Mobilario y equipo de oficina por inversiones";
            cl4.Value = "I02";
            ddlUsoCFDI.Items.Add(cl4);
            ListItem cl5 = new ListItem();
            
            cl5.Text = "Equipo de transporte";
            cl5.Value = "I03";
            ddlUsoCFDI.Items.Add(cl5);
            ListItem cl6 = new ListItem();
            cl6.Text = "Equipo de computo y accesorios";
            cl6.Value = "I04";
            ddlUsoCFDI.Items.Add(cl6);
            ListItem cl7 = new ListItem();
            cl7.Text = "Dados, troqueles, moldes, matrices y herramental";
            cl7.Value = "I05";
            ddlUsoCFDI.Items.Add(cl7);
            ListItem cl8 = new ListItem();
            cl8.Text = "Comunicaciones telefónicas";
            cl8.Value = "I06";
            ddlUsoCFDI.Items.Add(cl8);
            ListItem cl9 = new ListItem();
            cl9.Text = "Comunicaciones satelitales";
            cl9.Value = "I07";
            ddlUsoCFDI.Items.Add(cl9);
            ListItem cl10 = new ListItem();
            cl10.Text = "Otra maquinaria y equipo";
            cl10.Value = "I08";
            ddlUsoCFDI.Items.Add(cl10);
            ListItem cl11 = new ListItem();
            
            cl11.Text = "Honorarios médicos, dentales y gastos hospitalarios";
            cl11.Value = "D01";
            ddlUsoCFDI.Items.Add(cl11);
            ListItem cl12 = new ListItem();
            cl12.Text = "Gastos médicos por incapacidad o discapacidad";
            cl12.Value = "D02";
            ddlUsoCFDI.Items.Add(cl12);
            ListItem cl13 = new ListItem();
            cl13.Text = "Gastos funerales";
            cl13.Value = "D03";
            ddlUsoCFDI.Items.Add(cl13);
            ListItem cl14 = new ListItem();
            cl14.Text = "Donativos";
            cl14.Value = "D04";
            ddlUsoCFDI.Items.Add(cl14);
            ListItem cl15 = new ListItem();
            cl15.Text = "Intereses reales efectivamente pagados por créditos hipotecarios (casa habitación)";
            cl15.Value = "D05";
            ddlUsoCFDI.Items.Add(cl15);
            ListItem cl16 = new ListItem();
            
            cl16.Text = "Aportaciones voluntarias al SAR";
            cl16.Value = "D06";
            ddlUsoCFDI.Items.Add(cl16);
            ListItem cl17 = new ListItem();
            cl17.Text = "Primas por seguros de gastos médicos";
            cl17.Value = "D07";
            ddlUsoCFDI.Items.Add(cl17);
            ListItem cl18 = new ListItem();
            
            cl18.Text = "Gastos de transportación escolar obligatoria";
            cl18.Value = "D08";
            ddlUsoCFDI.Items.Add(cl18);
            ListItem cl19 = new ListItem();
            cl19.Text = "Depósitos en cuentas para el ahorro, primas que tengan como base planes de pensiones";
            cl19.Value = "D09";
            ddlUsoCFDI.Items.Add(cl19);
            ListItem cl20 = new ListItem();
            cl20.Text = "Pagos por servicios educativos (colegiaturas)";
            cl20.Value = "D10";
            ddlUsoCFDI.Items.Add(cl20);
            ListItem cl21 = new ListItem();
            cl21.Text = "Por definir";
            cl21.Value = "P01";
            ddlUsoCFDI.Items.Add(cl21);
        }
        private void llenarUsoCFDI()
        {
            ddlUsoCFDI.Items.Clear();

            ListItem cl = new ListItem();
            cl.Text = "Por definir";
            cl.Value = "P01";
            ddlUsoCFDI.Items.Add(cl);
            ListItem cl1 = new ListItem();
            cl1.Text = "Otros";
            cl1.Value = "0";
            ddlUsoCFDI.Items.Add(cl1);
        }

        protected void ddlUsoCFDI_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUsoCFDI.SelectedValue == "0")
                llenarUsoCFDIALL();
        }

        protected void ddlGiro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGiro.SelectedValue == "13")
            
            {
                cbIVARet.Visible = true;
            }
            else
                cbIVARet.Visible=false;

               var Empresa = new GAFEmpresa();
               using (Empresa as IDisposable)
               {
                   this.ddlEmpresa.Items.Clear();
                   this.ddlEmpresa.DataSource = Empresa.GetListGiroempresaBy(Convert.ToInt64(ddlGiro.SelectedValue));
                   this.ddlEmpresa.DataBind();
                   var prefactu = new Prefactura();
                   if (ddlEmpresa.Items.Count > 0)
                   {
                     int  idEmpresa = Convert.ToInt16(ddlEmpresa.SelectedValue);
                   
                       using (prefactu as IDisposable)
                       {
                           this.txtFolio.Text = prefactu.GetNextFolio(idEmpresa);
                           this.txtPreFolio.Text = prefactu.GetNextPreFolio("F", 0);
                       }
                   }

               }
               if (!Empresa.TieneConfiguradoCertificado(Convert.ToInt32( ddlEmpresa.SelectedValue)))
               {
                   this.lblError.Text = "Tienes que configurar tus certificados antes de poder facturar";
                   this.btnGenerarFactura.Enabled = this.BtnVistaPrevia.Enabled =
                       this.btnAgregar.Enabled = this.ddlMoneda.Enabled = false;
                   return;
               }

               else
               {
                   this.lblError.Text = "";
                   this.btnGenerarFactura.Enabled = this.BtnVistaPrevia.Enabled =
                           this.btnAgregar.Enabled = this.ddlMoneda.Enabled = true;
                   return;
               }
        }
        //----------------
    }
}