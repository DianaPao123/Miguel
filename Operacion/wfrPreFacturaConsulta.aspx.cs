using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.ServiceModel;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Linq;
using Label = System.Web.UI.WebControls.Label;
using Business;
using Contract;

using System.Configuration;
using CatalogosSAT;

namespace GAFWEB
{
    public partial class wfrPreFacturasConsulta : System.Web.UI.Page
    {

        public string SelText = "Todo";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                var datos = Session["DatosConsultaFactura"] as DatosBusqueda;
                
                 ViewState["rutaPago"] = "";
                //------rol-----------
                lblLinea.Visible = false;
                ddlLinea.Visible = false;
                //-------------------------
                ViewState["select"] = "Sel";

                var Empresa = new GAFEmpresa();
                using (Empresa as IDisposable)
                {
                    //this.ddlEmpresas.Items.Clear();
                    ListItem cl2 = new ListItem();
                    cl2.Text = "--Todos--";
                    cl2.Value = "0";
                    ddlEmpresas.Items.Add(cl2);
                    var empresaa = Empresa.GetListForLine(ddlLinea.SelectedValue);
                    this.ddlEmpresas.DataSource = empresaa;
                    this.ddlEmpresas.DataBind();

                    int idEmpresa = Convert.ToInt16(ddlEmpresas.SelectedValue);
                    if (ddlEmpresas.Items.Count > 0)
                    {
                        var cliente = new GAFClientes();
                        using (cliente as IDisposable)
                        {
                            //var clientes = cliente.GetList(idEmpresa, string.Empty, false);
                            var clientes = cliente.GetList();

                            //myDropDown.DataTextField = "whatever";
                            //myDropDown.DataValueField = "ID";
                            //myDropDown.DataSource = GetStuff();
                            //myDropDown.DataBind();
                            ListItem cl = new ListItem();
                            cl.Text = "--Todos--";
                            cl.Value = "0";
                            ddlClientes.Items.Add(cl);
                            //ddlClientes.Items.Insert(0, "--Todos--");

                            this.ddlClientes.DataSource = clientes;
                            this.ddlClientes.DataBind();
                        }

                    }

                    this.txtFechaInicial.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("d");
                    this.txtFechaFinal.Text = DateTime.Today.ToString("d");
                }
                   //------------catalogos grandes----------------------
                      //OperacionesCatalogos OP=new OperacionesCatalogos();
                      //using (OP as IDisposable)
                      //{


                      //    ddlBanco.DataSource = OP.ConsultarBancosAll();
                      //    ddlBanco.DataTextField = "Descripcion";
                      //    ddlBanco.DataValueField = "Descripcion";
                      //    ddlBanco.DataBind();
                      //}


                if (datos != null)
                {
                    ddlEmpresas.SelectedValue = datos.Empresa;
                    ddlClientes.SelectedValue = datos.Cliente;
                    txtFechaInicial.Text = datos.FechaInicial;
                    txtFechaFinal.Text = datos.FechaFinal;
                  //  ddlPromotor.SelectedValue = datos.Promotor;
                    rbStatus.SelectedValue = datos.Status;

                    Session["DatosConsultaFactura"] = null;

                }
                this.FillView();
                if (datos != null)
                    gvFacturas_PageIndexChanging(null, null);

            }
        }

        protected void gvFacturas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.Equals("GenerarCFDI"))
            {
                bool error = false;

                try
                {

                    var id = this.gvFacturas.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["idPreFactura"];
                    var cliente = new GAFFactura();
                    using (cliente as IDisposable)
                    {
                        if (!cliente.GuardarPreFactura33(Convert.ToInt64(id)))
                        {
                            this.lblError.Text = "* Error al generar la factura";
                            return;
                        }
                        this.lblError.Text = string.Empty;
                        this.FillView();
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
                    if (ae.InnerException != null)
                    { } this.lblError.Text = ae.Message;
                }
                catch (Exception ae)
                {
                    error = true;
                    if (ae.InnerException != null)
                    { } this.lblError.Text = "Error al generar el comprobante:" + ae.Message;
                }
                if (!error)
                { this.lblError.Text = "Comprobante generado correctamente"; }
            }
            else if (e.CommandName.Equals("Pagar"))
            {
                string folio = this.gvFacturas.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
             
                var id = this.gvFacturas.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["idPreFactura"];
                string ruta = ViewState["rutaPago"].ToString();
                ruta = ConfigurationManager.AppSettings["RutaPago"] + id;
                ViewState["rutaPago"] = ruta;
                this.lblFolioPagoPrefactura.Text = folio.ToString();
                lblIdPrefactura.Text = id.ToString();
                lblFolioPago.Text = folio.ToString();
               // this.mpePagarPreFactura.Show();
                mpePagar.Show();
            }
            if (e.CommandName.Equals("DescargarPdf"))
            {

                var id = this.gvFacturas.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["idPreFactura"];
                var cliente = new GAFFactura();
                using (cliente as IDisposable)
                {
                    byte[] pdf;
                    pdf = cliente.PreFacturaPreview33(Convert.ToInt64(id));
                    //-----------
                    //---------------
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + "PreFactura" + "_" + id  + ".pdf");
                    this.Response.ContentType = "application/pdf";
                                        
                    if (pdf == null)
                    {
                        this.lblError.Text = "Archivo no encontrado";
                        return;
                    }
                    this.Response.BinaryWrite(pdf);
                    this.Response.End();
                }

            }
            if (e.CommandName.Equals("Editar"))
            {

                
                      DatosBusqueda da = new DatosBusqueda();
                      da.Empresa=ddlEmpresas.SelectedValue ;
                      da.Cliente=ddlClientes.SelectedValue;
                      da.FechaInicial=txtFechaInicial.Text ;
                      da.FechaFinal=txtFechaFinal.Text ;
                      da.Status = rbStatus.SelectedValue;
                     // da.Promotor=ddlPromotor.SelectedValue ;
                      Session["DatosConsultaFactura"] = da;

                  
                var id = this.gvFacturas.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["idPreFactura"];
                var CFDI = this.gvFacturas.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["CFDI"];
                if (CFDI.ToString() == "I")
                    Response.Redirect("wfrPreImpuestosLocales.aspx?idPrefactura=" + id);
                else    
            //if (CFDI.ToString() == "G")
                {
                    Response.Redirect("wfrPreFactura.aspx?idPrefactura=" + id);
                }
                //if (CFDI.ToString() == "P")
                 //   Response.Redirect("wfrPreFacturaPago.aspx?idPrefactura=" + id);
             
            }

            if (e.CommandName.Equals("Rechazado"))
            {
                var id = this.gvFacturas.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["idPreFactura"];
            
               // var id = Convert.ToInt32(e.CommandArgument);
                using (var db = new GAFEntities())
                {
                    long ID = Convert.ToInt64(id);
                    var R = db.PreFactura.Where(p => p.idPreFactura == ID).FirstOrDefault();
                    if (R != null)

                        txtMotivoCancela.Text = R.MotivoRechazo;
                }
                
                gvFacturas.DataSource = ViewState["facturas"];
                gvFacturas.DataBind();
                mpeCancelar.Show();
                UpdatePanel10.Update();
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            this.FillView();
            mpeCancelar.Hide();
        }


        protected void btnExportar_Click(object sender, EventArgs e)
        {

            var ex = new Export();
            this.Response.AddHeader("Content-Disposition", "attachment; filename=Reporte.xlsx");
            this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            this.Response.BinaryWrite(ex.GridToExcel(this.gvFacturaCustumer, "Facturas"));
            this.Response.End();

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            this.FillView();
            Session["idPagina"] = null;
            gvFacturas_PageIndexChanging(null, null);
        }

        protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            var cliente = new GAFClientes();
            using (cliente as IDisposable)
            {
                var idEmpresa = int.Parse(this.ddlEmpresas.SelectedValue);

                var clientes = cliente.GetList(idEmpresa, string.Empty, false);
                ddlClientes.Items.Clear();
                ListItem cl = new ListItem();
                cl.Text = "--Todos--";
                cl.Value = "0";
                ddlClientes.Items.Add(cl);

                this.ddlClientes.DataSource = clientes;
                this.ddlClientes.DataBind();
                DateTime fechaInicial = DateTime.Parse(this.txtFechaInicial.Text);
                DateTime fechaFinal = DateTime.Parse(this.txtFechaFinal.Text).AddDays(1).AddSeconds(-1);
            }
            */

        }

        protected void gvFacturas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvFacturas.DataSource = ViewState["facturas"];
            if (e != null)
            {
                this.gvFacturas.PageIndex = e.NewPageIndex;
                Session["idPagina"] = e.NewPageIndex;
            }
            else
            {
                var xxx = Session["idPagina"]; int x = 0;
                if (xxx != null)
                    x = Convert.ToInt16(xxx);
                this.gvFacturas.PageIndex = x;
            }
            this.gvFacturas.DataBind();
            this.CalculaTotales(ViewState["facturas"] as List<vprefactura>);

        }

        protected void btnEnviarMail_Click(object sender, EventArgs e)
        {

        }

        protected void btnPagar_Click(object sender, EventArgs e)
        {
            var cliente = new Prefactura();
            using (cliente as IDisposable)
            {
                cliente.Cambiarestatus(Convert.ToInt64(lblIdPrefactura.Text),1);
                LlenarTotalDevolucionesActores(Convert.ToInt64(lblIdPrefactura.Text));

            }
            FillView();
           
        }

        #region Helper Methods

        private void FillView()
        {


            // var perfil = Session["perfil"] as string;
            // var iniciales = Session["iniciales"] as string;
            //  var cliente = NtLinkClientFactory.Cliente();
            var factu = new Prefactura();

            var filtro = Convert.ToInt16(rbStatus.SelectedValue);
            if (!string.IsNullOrEmpty(this.ddlClientes.SelectedValue))
                using (factu as IDisposable)
                {
                    DateTime fechaInicial = DateTime.Parse(this.txtFechaInicial.Text);
                    DateTime fechaFinal = DateTime.Parse(this.txtFechaFinal.Text).AddDays(1).AddSeconds(-1);
                    if ((fechaFinal - fechaInicial).TotalDays > 93)
                    {
                        lblError.Text = "El rango de fechas no puede ser mayor a 93 dias";
                        return;
                    }
                    var ventas = factu.GetListPrefactura(fechaInicial, fechaFinal, int.Parse(this.ddlEmpresas.SelectedValue), filtro,
                        int.Parse(this.ddlClientes.SelectedValue));


                    List<vprefactura> lista;
                    /* if(!string.IsNullOrEmpty(this.txtTexto.Text))
                     {
                         lista = ventas.Where(l => (l.Cliente != null && l.Cliente.Contains(this.txtTexto.Text))
                             || (l.Observaciones != null && l.Observaciones.Contains(this.txtTexto.Text))).ToList();
                     }
                     else */
                    {
                        lista = ventas.ToList();
                    }
                    var gridFatura = new GridView();
                    foreach (DataControlField colum in gvFacturas.Columns)
                    {
                        gridFatura.Columns.Add(colum);
                    }



                    ViewState["facturas"] = lista;

                    this.gvFacturas.DataSource = lista;
                    this.gvFacturas.DataBind();

                    CalculaTotales(lista);
                    this.gvFacturaCustumer.DataSource = lista;
                    this.gvFacturaCustumer.DataBind();
                    UpdatePanel2.Update();
                }

        }

        private void CalculaTotales(List<vprefactura> lista)
        {

          //  var subt = lista.Where(c => c.Estatus != 3).Sum(p => p.SubTotal);
            var total = lista.Where(c => c.Estatus != 3).Sum(p => p.Total);
            // var iva = lista.Where(c => c.Cancelado == 0).Sum(p => p.IVA);
            //  var retiva = lista.Where(c => c.Cancelado == 0 && c.RetIva.HasValue).Sum(p => p.RetIva);
            //  var retisr = lista.Where(c => c.Cancelado == 0 && c.RetIsr.HasValue).Sum(p => p.RetIsr);
            //  var ieps = lista.Where(c => c.Cancelado == 0 && c.Ieps.HasValue).Sum(p => p.Ieps);
            var iva = 0;
            var retiva = 0;
            var retisr = 0;
            var ieps = 0;

            if (this.gvFacturas.FooterRow != null)
            {
                this.gvFacturas.FooterRow.Cells[0].Text = "TOTAL";
               // this.gvFacturas.FooterRow.Cells[3].Text = subt.Value.ToString("C");
                //this.gvFacturas.FooterRow.Cells[6].Text = subt.Value.ToString("C");
                //this.gvFacturas.FooterRow.Cells[7].Text = iva.ToString("C");
                //this.gvFacturas.FooterRow.Cells[8].Text = retiva.ToString("C");
                //this.gvFacturas.FooterRow.Cells[9].Text = retisr.ToString("C");
                //this.gvFacturas.FooterRow.Cells[10].Text = ieps.ToString("C");
                //this.gvFacturas.FooterRow.Cells[11].Text = total.Value.ToString("C");
                this.gvFacturas.FooterRow.Cells[4].Text = total.ToString("C");

            }

        }

        #endregion

        protected void gvFacturas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                /* si estaba timbrada
                if (e.Row.Cells[6].Text == "True")

                {
                    e.Row.Cells[6].Text = "Si";
                    //e.Row.Cells[7].Text = "";
                    e.Row.Cells[8].Text = "";
                    //e.Row.Cells[9].Text = "";
                  
                    //e.Row.Cells[6].Visible = false;
                    //e.Row.Cells[8].Visible = false;

                }
                 */ 
                if (e.Row.Cells[5].Text == "0")
                    e.Row.Cells[5].Text = "Pendiente por Revisar";
                if (e.Row.Cells[5].Text == "2")
                    e.Row.Cells[5].Text = "Parcialmente Pagada";
                if (e.Row.Cells[5].Text == "1")
                    e.Row.Cells[5].Text = "Pagada";
                if (e.Row.Cells[5].Text == "3")
                {
                    e.Row.Cells[5].Text = "Rechazada";
                    e.Row.Cells[8].Text = "X";
                    e.Row.Cells[9].Text = "X";
                   
                }

                     e.Row.Cells[6].Text=  TipoDEPago(e.Row.Cells[6].Text);

                 //  Button sav = (Button)e.Row.Cells[10].FindControl("Button1");
                     LinkButton btnSave = (LinkButton)e.Row.Cells[10].Controls[0];

                     if (btnSave.Text == "1")
                     {
                         e.Row.BackColor = Color.FromName("#A8CF38");
                         e.Row.Cells[10].Text = "Si";
                     }
                     else
                     {
                         if (btnSave.Text == "-1")
                         {
                             e.Row.BackColor = Color.FromName("#F6DDCC");
                             btnSave.Text = "Rechazado";
                            // e.Row.Cells[10].Text = "Rechazado";
                         }
                         else
                             e.Row.Cells[10].Text = "No";
                     }
                
                /*
                //antes 13 ahora 6
                if (e.Row.Cells[6].Text == "Cancelado")
                    e.Row.BackColor = Color.FromName("#FEDDB8");
                if (e.Row.Cells[6].Text == "Pendiente")
                    e.Row.BackColor = Color.FromName("#e4e5e7");
                if (e.Row.Cells[6].Text == "Pagado")
                    e.Row.BackColor = Color.FromName("#b3d243");
                  */
            }
        }

        protected void btnCerrarPagar_Click(object sender, EventArgs e)
        {
            FillView();

        }

        protected void btnCerrarPagarPrefactura_Click(object sender, EventArgs e)
        {
            FillView();

        }

        protected void ddlLinea_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSubir_Click(object sender, EventArgs e)
        {
            lblIdventa.Text = "";
            lblErrorPago.Text = "";

            //--------------------------
            var cliente = new Prefactura();
            using (cliente as IDisposable)
            {
                cliente.Cambiarestatus(Convert.ToInt64(lblIdPrefactura.Text), 1);
                LlenarTotalDevolucionesActores(Convert.ToInt64(lblIdPrefactura.Text));

            }
            //----------------

            if (archivoPagos.HasFile)//por si hay archivo
            {
                string nombreArchivo = archivoPagos.FileName;
                var archivo = this.archivoPagos.FileBytes;
                string x = guardarArchivo(archivo, nombreArchivo);
                if (x == "OK")
                {

                    lblIdventa.Text = "Se guardó el pago.";
                    lblIdventa.Visible = true;
                }
                else
                {
                    lblErrorPago.Text = x;

                }

                // this.mpePagar.Show();
            }
            else  //no hay archivo
            {
               string x= guardarArchivo();
                if (x == "OK")
                {

                    lblIdventa.Text = "Se guardó el pago.";
                    lblIdventa.Visible = true;
                }
                else
                {
                    lblErrorPago.Text = x;

                }
            }
            FillView();

        }

        protected string guardarArchivo(byte[] archivo, string nombre)
        {
            try
            {
                string ruta = ViewState["rutaPago"].ToString();
                 if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);
                 ruta = Path.Combine(ruta, nombre);
                 var factu = new Prefactura();
                 using (factu as IDisposable)
                 {
                     File.WriteAllBytes(ruta, archivo);
                     PagosF P = new PagosF();
                     P.Banco = ddlBanco.SelectedValue;
                     P.Monto = Convert.ToDecimal(txtMonto.Text);
                     P.IdPrefactura = Convert.ToInt64(lblIdPrefactura.Text);
                     P.RutaImagen = ruta;
                     P.fecha = DateTime.Now;
                    var x= factu.GuardarPagosF(P);
                    if (x != 1)
                        return "Error al guardar en la base";
                 }
              
                return "OK";
            }

            catch (Exception ee)
            {
                return ee.Message;
            }
        }
        protected string guardarArchivo()
        {
            try
            {
                //string ruta = ViewState["rutaPago"].ToString();
                //if (!Directory.Exists(ruta))
                //    Directory.CreateDirectory(ruta);
                //ruta = Path.Combine(ruta, nombre);
                var factu = new Prefactura();
                using (factu as IDisposable)
                {
                   // File.WriteAllBytes(ruta, archivo);
                    PagosF P = new PagosF();
                    P.Banco = ddlBanco.SelectedValue;
                    P.Monto = Convert.ToDecimal(txtMonto.Text);
                    P.IdPrefactura = Convert.ToInt64(lblIdPrefactura.Text);
                    P.RutaImagen = "";
                    P.fecha = DateTime.Now;
                    var x = factu.GuardarPagosF(P);
                    if (x != 1)
                        return "Error al guardar en la base";
                }

                return "OK";
            }

            catch (Exception ee)
            {
                return ee.Message;
            }
        }
        //-----------------------------------------------------
        private void LlenarTotalDevolucionesActores(long idPrefactura)
        {
            var client = new GAFClientes();
                               
            var factu = new Prefactura();
            using (factu as IDisposable)
            {
              PreFactura f =factu.GetCliente(idPrefactura);
              int idCliente =(int) f.idCliente;
              System.Guid IdPromotor = (System.Guid)f.IDUsuario;
              ClientePromotor c = client.GetClientePromotorRelacion(idCliente, IdPromotor);
              if (c != null)
              {
                  if (c.PorcentajeCliente != null)
                      f.DTCliente = f.SubTotal + (((decimal)c.PorcentajeCliente / 100) * f.SubTotal);
                  else
                      f.DTCliente = 0;
                  if (c.PorcentajeContacto != null)
                      f.DTContacto = ((decimal)c.PorcentajeContacto / 100) * f.SubTotal;
                  else
                      f.DTContacto = 0;
                  if (c.PorcentajeEmpresa != null)
                      f.DTEmpresa = ((decimal)c.PorcentajeEmpresa / 100) * f.SubTotal;
                  else
                      f.DTEmpresa = 0;
                  if (c.PorcentajePromotor != null)
                      f.DTPromotor = ((decimal)c.PorcentajePromotor / 100) * f.SubTotal;
                  else
                      f.DTPromotor = 0;
                  factu.SavePrefactura(f);
              }
            }

        }
        //--------------------------

        //--------------------------------------------------
        private string TipoDEPago(string tipo)
        {
            string a = tipo;
            switch (tipo)
            {
                case "01": { a = "Efectivo"; break; }
                case "02": { a = "Cheque nominativo"; break; }
                case "03": { a = "Transferencia electrónica de fondos"; break; }
                case "04": { a = "Tarjetas de crédito"; break; }
                case "05": { a = "Monederos electrónicos"; break; }
                case "06": { a = "Dinero electrónico"; break; }
                //case "07": { a = "Tarjetas digitales"; break; }
                case "08": { a = "Vales de despensa"; break; }
                //case "09": { a = "Bienes"; break; }
                //case "10": { a = "Servicio"; break; }
                //case "11": { a = "Por cuenta de tercero"; break; }
                case "12": { a = "Dación en pago"; break; }
                case "13": { a = "Pago por subrogación"; break; }
                case "14": { a = "Pago por consignación"; break; }
                case "15": { a = "Condonación"; break; }
                //case "16": { a = "Cancelación"; break; }
                case "17": { a = "Compensación"; break; }
                case "23": { a = "Novación"; break; }
  case "24": { a = "Confusión"; break; }
  case "25": { a = "Remisión de deuda"; break; }
  case "26": { a = "Prescripción o caducidad"; break; }
  case "27": { a = "A satisfacción del acreedor"; break; }
  case "28": { a = "Tarjeta de débito"; break; }
  case "29": { a = "Tarjeta de servicios"; break; }
                    case "30": { a = "Aplicación de anticipos"; break; }
                //case "98": { a = "NA"; break; }
                    case "99": { a = "Por definir"; break; }
            }
            return a;
        }
        //----------------------------------------------



    }
}