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
using GAFBusiness;
using GAFContract;

using System.Configuration;

namespace GAFWEB
{
    public partial class wfrPreFacturasConsultaDevolucion : System.Web.UI.Page
    {

        public string SelText = "Todo";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {


                ViewState["rutaPago"] = "";
                //------rol-----------
                ViewState["select"] = "Sel";


                    this.txtFechaInicial.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("d");
                    this.txtFechaFinal.Text = DateTime.Today.ToString("d");
                


                this.FillView();

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
                var id = this.gvFacturas.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["idPreFactura"];
                string ruta = ViewState["rutaPago"].ToString();
                ruta = ConfigurationManager.AppSettings["RutaPago"] + id;
                ViewState["rutaPago"] = ruta;
                this.lblFolioPago.Text = id.ToString();
                lblIdventa.Text = "";
                lblErrorPago.Text = "";
         
                this.mpePagar.Show();
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

                var id = this.gvFacturas.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["idDevoluciones"];

                Response.Redirect("wfrDevoluciones.aspx?idDevoluciones=" + id);
            }


        }




       

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            this.FillView();
        }

       

        protected void gvFacturas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvFacturas.DataSource = ViewState["facturas"];
            this.gvFacturas.PageIndex = e.NewPageIndex;
            this.gvFacturas.DataBind();
            this.CalculaTotales(ViewState["facturas"] as List<PreFactura>);

        }

        protected void btnEnviarMail_Click(object sender, EventArgs e)
        {

        }

        protected void btnPagar_Click(object sender, EventArgs e)
        {


        }

        #region Helper Methods

        private void FillView()
        {
            var sesion = Session["sessionRGV"] as Sesion;
               
            var factu = new Prefactura();

               using (factu as IDisposable)
                {
                    DateTime fechaInicial = DateTime.Parse(this.txtFechaInicial.Text);
                    DateTime fechaFinal = DateTime.Parse(this.txtFechaFinal.Text).AddDays(1).AddSeconds(-1);
                    if ((fechaFinal - fechaInicial).TotalDays > 93)
                    {
                        lblError.Text = "El rango de fechas no puede ser mayor a 93 dias";
                        return;
                    }
                    var ventas = factu.GetListDecolucionesOperaciones( fechaInicial, fechaFinal);


                    List<Devoluciones> lista;
                   
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

                }

        }

        private void CalculaTotales(List<PreFactura> lista)
        {

            var subt = lista.Where(c => c.Estatus != 3).Sum(p => p.SubTotal);
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
                this.gvFacturas.FooterRow.Cells[3].Text = subt.Value.ToString("C");
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
            
        }

        protected void btnCerrarPagar_Click(object sender, EventArgs e)
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
            if (archivoPagos.HasFile)
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

                this.mpePagar.Show();
            }
        }

        protected string guardarArchivo(byte[] archivo, string nombre)
        {
            try
            {
                string ruta = ViewState["rutaPago"].ToString();
                 if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);
                 ruta = Path.Combine(ruta, nombre);
              
                File.WriteAllBytes(ruta, archivo);
                return "OK";
            }

            catch (Exception ee)
            {
                return ee.Message;
            }
        }
    }
}