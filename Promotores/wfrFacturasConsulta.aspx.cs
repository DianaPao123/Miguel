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
//using System.Windows.Forms;
//using ServicioLocalContract;
using System.Text;
using System.Linq;
using Label = System.Web.UI.WebControls.Label;
using Business;
using Contract;

namespace GAFWEB
{
    public partial class wfrFacturasConsulta : System.Web.UI.Page
    {

        public string SelText = "Todo";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                //------rol-----------
                lblLinea.Visible = false;
                ddlLinea.Visible = false;
                //-------------------------
                ViewState["select"] = "Sel";

                  var Empresa = new GAFEmpresa();
                  using (Empresa as IDisposable)
                  {
                      this.ddlEmpresas.Items.Clear();
                      ListItem cl2 = new ListItem();
                      cl2.Text = "--Todos--";
                      cl2.Value = "0";
                      ddlEmpresas.Items.Add(cl2);
                 
                      this.ddlEmpresas.DataSource = Empresa.GetListForLine(ddlLinea.SelectedValue);
                      this.ddlEmpresas.DataBind();

                      int idEmpresa = Convert.ToInt16(ddlEmpresas.SelectedValue);
                      if (ddlEmpresas.Items.Count > 0)
                      {
                          var cliente = new GAFClientes();
                          using (cliente as IDisposable)
                          {
                              var sesion = Session["sessionRGV"] as Sesion;
          
                              var clientes = cliente.GetListClientePromotor(sesion.Id);
                          
                             // var clientes = cliente.GetList(idEmpresa, string.Empty, false);
                              
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
                   
                /*
                var perfil = Session["perfil"] as string;
                var sistema = Session["idSistema"] as long?;
                var idEmp = Session["idEmpresa"] as int?;
                var cliente = NtLinkClientFactory.Cliente();
                using (cliente as IDisposable)
                {
                    string guidString = ((Guid)Session["userId"]).ToString();
                    empresa empresa = cliente.ObtenerEmpresaByUserId(guidString);
                    
                    int empresaId = perfil != null && perfil.Equals("Administrador") ? 0 : empresa.IdEmpresa;
                    this.ddlClientes.Items.Clear();
                    this.ddlClientes.DataSource = cliente.ListaClientes(perfil, empresaId, string.Empty, true);
                    this.ddlClientes.DataBind();
                    this.ddlEmpresas.DataSource = cliente.ListaEmpresas(perfil, idEmp.Value, sistema.Value, null);
                    this.ddlEmpresas.Enabled = perfil.Equals("Administrador");
                    this.ddlEmpresas.DataBind();
                    this.txtFechaInicial.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("d");
                    this.txtFechaFinal.Text = DateTime.Today.ToString("d");
                    ddlEmpresas_SelectedIndexChanged(null,null);
                }
                 */ 
                this.FillView();
               
            }
        }

        protected void gvFacturas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
            if(e.CommandName.Equals("DescargarXml"))
            {
                string uuid = this.gvFacturas.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
                var cliente =new Factura();
                using (cliente as IDisposable)
                {
                    var id = (int)this.gvFacturas.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["idventa"];
                    var cfd = cliente.GetById(id);
                    string xml = Encoding.UTF8.GetString(GAFFactura.GetData(uuid, "xml"));
                    //-------------------
                    var clien = new GAFClientes();string RFC="";
                    using (clien as IDisposable)
                    {
                        var cli = clien.GetCliente(Convert.ToInt32(cfd.idcliente));
                        RFC=cli.RFC;
                    }
                    //------------------------
                    Response.AddHeader("Content-Disposition", "attachment; filename=" +RFC + "_" + cfd.Folio + "_" + cfd.Fecha.ToString("yyyyMMdd") + "_" + uuid + ".xml");
                   
                    this.Response.ContentType = "text/xml";
                    this.Response.Charset = "UTF-8";
                    this.Response.Write(xml);
                    this.Response.End();
                }
            }
            else if (e.CommandName.Equals("DescargarPdf"))
            {
                
                string uuid = this.gvFacturas.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
                var cliente = new Factura();
                using (cliente as IDisposable)
                {
                    var id = (int)this.gvFacturas.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["idventa"];
                    var cfd = cliente.GetById(id);
                    //-----------
                    var clien = new GAFClientes(); string RFC = "";
                    using (clien as IDisposable)
                    {
                        var cli = clien.GetCliente(Convert.ToInt32(cfd.idcliente));
                        RFC = cli.RFC;
                    }
                    //---------------
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + RFC + "_" + cfd.Folio + "_" + cfd.Fecha.ToString("yyyyMMdd") + "_" + uuid + ".pdf");
                    //Response.AddHeader("Content-Disposition", "attachment; filename=" +  uuid + ".pdf");
                    this.Response.ContentType = "application/pdf";
                    
                    var fact = new GAFFactura();
                    byte[] pdf;
                    using (fact as IDisposable)
                    {
                      pdf=  fact.GetPdfData(uuid);
                    }
                    if (pdf == null)
                    {
                        this.lblError.Text = "Archivo no encontrado";
                        return;
                    }
                    this.Response.BinaryWrite(pdf);
                    this.Response.End();
                }
                 
            }
            else if (e.CommandName.Equals("EnviarEmail"))
            {
                var idEmpresa = (int)this.gvFacturas.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["IdEmpresa"];
                ViewState["IdEmpresaa"] = idEmpresa;
   
                var idCliente = (int) this.gvFacturas.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["IdCliente"];
                var cliente =new GAFClientes();
                using (cliente as IDisposable)
                {
                    clientes c = cliente.GetCliente(idCliente);
                    this.lblEmailCliente.Text = c.Email;
                }
                this.lblGuid.Text = this.gvFacturas.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
                this.mpeEmail.Show();
                gvFacturas.DataSource = ViewState["facturas"];
                gvFacturas.DataBind();

            }
            else if(e.CommandName.Equals("Pagar"))
            {
                var id = (int)this.gvFacturas.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["idventa"];
                var cliente =new  Factura();
                using (cliente as IDisposable)
                {
                    var venta = cliente.GetById(id);
                    this.txtFechaPago.Text = venta.FechaPago!=null ? Convert.ToDateTime( venta.FechaPago).ToString("dd/MM/yyyy") : string.Empty;
                    this.lblFolioPago.Text = venta.Folio.ToString();
                    this.txtReferenciaPago.Text = venta.ReferenciaPago;
                }
                this.lblIdventa.Text = id.ToString();
                this.mpePagar.Show();
            }
            else if (e.CommandName.Equals("Cancelar"))
            {
                /*
                try
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    var cliente = NtLinkClientFactory.Cliente();
                    using (cliente as IDisposable)
                    {
                        var venta = cliente.GetFactura(id);
                        var cancelacion = cliente.CancelarFactura(venta.RfcEmisor, venta.Guid);
                        lblError.Text = cancelacion;
                        this.FillView();
                    }
                }
                catch (FaultException fe)
                {
                    lblError.Text = fe.Message;
                }
                catch (Exception fe)
                {
                    ;
                }
                */
            }

            else if (e.CommandName.Equals("Acuse"))
            {
                /*
                try
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    var cliente = new Factura();
                    using (cliente as IDisposable)
                    {
                        var fact = cliente.GetById(id);
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + fact.idVenta + ".pdf");
                        this.Response.ContentType = "application/pdf";
                        var pdf = cliente.AcuseCancelacion("AcuseCancelacion", id);
                        if (pdf == null || pdf.Length == 0)
                        {
                            this.lblError.Text = "Archivo no encontrado";
                            return;
                        }
                        this.Response.BinaryWrite(pdf);
                        this.Response.End();
                    }
                }
                catch (FaultException fe)
                {
                    lblError.Text = fe.Message;
                }
                catch (Exception fe)
                {
                    ;
                }
                */
            }
            else if (e.CommandName == "SelectAll")
            {
                bool sel;
                if (ViewState["select"].ToString() != "Sel")
                //if (hidSel.Value != "Sel")
                {
                    sel = false;
                    //SelText = "Seleccionar Todos";
                    SelText = "Todos";
                     hidSel.Value = "Sel";
                     ViewState["select"] = "Sel";
                }
                else
                {
                    
                    sel = true;
                    SelText = "Ninguno";
                   // SelText = "Seleccionar Ninguno";
                    hidSel.Value = "Des";
                    ViewState["select"] = "Des";
                }
                foreach (GridViewRow row in gvFacturas.Rows)
                {
                    var lista = ViewState["facturas"] as List<vfacturas>;
                    if (lista != null)
                    {
                        foreach (vfacturas itemVventas in lista)
                        {
                            itemVventas.Seleccionar = sel;
                        }
                    }
                    gvFacturas.DataSource = lista;
                    gvFacturas.DataBind();
                    
                }
            }
           

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
        }

        protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
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

            /*
            var filtro = rbStatus.SelectedValue;
            var cliente = NtLinkClientFactory.Cliente();
            var perfil = Session["perfil"] as string;
            var iniciales = Session["iniciales"] as string;
            //if (string.IsNullOrEmpty(this.ddlClientes.SelectedValue))
            //    return;
            using (cliente as IDisposable)
            {
                var empresaId = int.Parse(this.ddlEmpresas.SelectedValue);
                var sistema = Session["idSistema"] as long?;
                this.ddlClientes.Items.Clear();
                this.ddlClientes.DataSource = cliente.ListaClientes(perfil, empresaId, string.Empty, true);
                this.ddlClientes.DataBind();
                DateTime fechaInicial = DateTime.Parse(this.txtFechaInicial.Text);
                DateTime fechaFinal = DateTime.Parse(this.txtFechaFinal.Text).AddDays(1).AddSeconds(-1);

                List<vventas> ventas = cliente.ListaFacturas(fechaInicial, fechaFinal, int.Parse(this.ddlEmpresas.SelectedValue),
                    int.Parse(this.ddlClientes.SelectedValue), filtro, "A", iniciales).OrderByDescending(p => p.Folio).ToList();

                var lista = new List<vventas>(ventas);
                ViewState["facturas"] = lista;
                this.gvFacturas.DataSource = lista;
                this.gvFacturas.DataBind();
                CalculaTotales(lista);
                this.gvFacturaCustumer.DataSource = lista;
                this.gvFacturaCustumer.DataBind();
            }
             */
        }

        protected void gvFacturas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
            this.gvFacturas.DataSource = ViewState["facturas"];
            this.gvFacturas.PageIndex = e.NewPageIndex;
            this.gvFacturas.DataBind();
            this.CalculaTotales(ViewState["facturas"] as List<vfacturasPromotores>);
            
        }

        protected void btnEnviarMail_Click(object sender, EventArgs e)
        {
            Mailer m = new Mailer();
            var Empresa = new GAFEmpresa();
                
           
            var cliente = new GAFFactura() ;
            using (cliente as IDisposable)
            {
                string uuid = this.lblGuid.Text;

                byte[] xml = cliente.GetXmlData(uuid);
                byte[] pdf = cliente.GetPdfData(uuid);
                var f = cliente.GetFacturaUUID(uuid);

                var atts = new List<EmailAttachment>();
                atts.Add(new EmailAttachment { Attachment = xml,Name = uuid + ".xml"});
                atts.Add(new EmailAttachment {Attachment = pdf, Name = uuid + ".pdf"});
                var idEmp = ViewState["IdEmpresaa"] as int?;

                var empresa = Empresa.GetById(idEmp.Value);
                var emails = new List<string>();

                if (!string.IsNullOrEmpty(this.lblEmailCliente.Text))
                {
                    emails.Add(this.lblEmailCliente.Text);
                }
                emails.AddRange(this.txtEmails.Text.Split(','));
                try
                {
                    if (f == null)
                    {
                        m.Send(emails, atts, "Se envia la factura con folio " + uuid + " en formato XML y PDF.",
                              "Envio de Factura", empresa.Email, empresa.RazonSocial);
                    }
                    if (f.TipoDocumentoStr == "Ingreso")
                    {
                        m.Send(emails, atts,
                            "Se envia la factura con folio " + uuid + " en formato XML y PDF.",
                            "Envío de Factura", empresa.Email, empresa.RazonSocial);
                    }
                    if (f.TipoDocumentoStr == "Egreso")
                    {
                        m.Send(emails, atts,
                            "Se envia la nota de crédito con folio " + uuid + " en formato XML y PDF.",
                            "Envío de la nota de crédito", empresa.Email, empresa.RazonSocial);
                    }
                    if (f.TipoDocumentoStr == "Pago")
                    {
                        m.Send(emails, atts,
                            "Se envia el comprobante de pago con folio " + uuid + " en formato XML y PDF.",
                            "Envío el comprobante de pago", empresa.Email, empresa.RazonSocial);
                    }

                }
                catch (FaultException fe)
                {
                    lblError.Text = fe.Message;
                }
                
                this.mpeEmail.Hide();
                gvFacturas.DataSource = ViewState["facturas"];
                gvFacturas.DataBind();
                up1.Update();
            }
             
        }

        protected void btnPagar_Click(object sender, EventArgs e)
        {
            
            var cliente =new GAFFactura();
            using (cliente as IDisposable)
            {
                try
                {
                    cliente.Pagar(int.Parse(this.lblIdventa.Text), DateTime.Parse(this.txtFechaPago.Text), this.txtReferenciaPago.Text);
                }
                catch(FaultException fe)
                {
                    this.lblErrorPago.Text = fe.Message;
                }
            }
            FillView();
            this.mpePagar.Hide();
            
        }

        #region Helper Methods

        private void FillView()
        {

            var sesion = Session["sessionRGV"] as Sesion;

           // var perfil = Session["perfil"] as string;
           // var iniciales = Session["iniciales"] as string;
          //  var cliente = NtLinkClientFactory.Cliente();
            var factu =new  Factura();
            
            var filtro = rbStatus.SelectedValue;
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
                var ventas = factu.GetListPromotores(sesion.Id,fechaInicial, fechaFinal, int.Parse(this.ddlEmpresas.SelectedValue), filtro,
                    int.Parse(this.ddlClientes.SelectedValue) )  ;

                foreach (var v in ventas)
                {
                    if (filtro == "Pago")
                        v.EstusCFDI = "Liquidado";
                    else
                    {
                        if (v.FormaPago != "Por definir")
                        {
                            v.EstusCFDI = "Liquidado";
                        }
                        else
                        {
                            if (v.SaldoAnteriorPago == 0 )
                                v.EstusCFDI = "Liquidado";
                            else
                                v.EstusCFDI = "Pendiente";
                        }
                    }
                    if (v.Monto != null)
                    {
                        CultureInfo cul = CultureInfo.CreateSpecificCulture("es-MX");
                        decimal.Parse("0.00", NumberStyles.Currency);
                        v.SubTotalPago = ((decimal)v.Monto / (decimal)1.16);
                        v.IVAPago = ((decimal)v.Monto - Convert.ToDecimal(v.SubTotalPago));
                    }
                }

                List<vfacturasPromotores> lista;
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

           

                ViewState["facturas"]   = lista;

                this.gvFacturas.DataSource = lista;
                this.gvFacturas.DataBind();
                                
               CalculaTotales(lista);
                this.gvFacturaCustumer.DataSource = lista;
                this.gvFacturaCustumer.DataBind();
            }
            UpdatePanel2.Update();
        }
        
        private void CalculaTotales(List<vfacturasPromotores> lista)
        {
            
            var subt = lista.Where(c=>c.Cancelado == 0).Sum(p => p.SubTotal);
            var total = lista.Where(c=>c.Cancelado == 0).Sum(p => p.Total);
           // var iva = lista.Where(c => c.Cancelado == 0).Sum(p => p.IVA);
          //  var retiva = lista.Where(c => c.Cancelado == 0 && c.RetIva.HasValue).Sum(p => p.RetIva);
          //  var retisr = lista.Where(c => c.Cancelado == 0 && c.RetIsr.HasValue).Sum(p => p.RetIsr);
          //  var ieps = lista.Where(c => c.Cancelado == 0 && c.Ieps.HasValue).Sum(p => p.Ieps);
            var iva =0;
             var retiva =0;
            var retisr =0;
            var ieps =0;

            if (this.gvFacturas.FooterRow != null)
            {
                this.gvFacturas.FooterRow.Cells[0].Text = "TOTAL";
                this.gvFacturas.FooterRow.Cells[6].Text = subt.Value.ToString("C");
                //this.gvFacturas.FooterRow.Cells[6].Text = subt.Value.ToString("C");
                //this.gvFacturas.FooterRow.Cells[7].Text = iva.ToString("C");
                //this.gvFacturas.FooterRow.Cells[8].Text = retiva.ToString("C");
                //this.gvFacturas.FooterRow.Cells[9].Text = retisr.ToString("C");
                //this.gvFacturas.FooterRow.Cells[10].Text = ieps.ToString("C");
                //this.gvFacturas.FooterRow.Cells[11].Text = total.Value.ToString("C");
                this.gvFacturas.FooterRow.Cells[7].Text = total.Value.ToString("C");
           
            }
             
        }
        
        #endregion

        protected void gvFacturas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[11].Text != "Pendiente")
                    e.Row.BackColor = Color.FromName("#b3d243");
                if (!string.IsNullOrEmpty(e.Row.Cells[12].Text.Replace("&nbsp;", "")))
                    e.Row.BackColor = Color.FromName("#F6DDCC");
            }
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    //antes 13 ahora 6
            //    if (e.Row.Cells[6].Text == "Cancelado")
            //        e.Row.BackColor = Color.FromName("#FEDDB8");
            //    if (e.Row.Cells[6].Text == "Pendiente")
            //        e.Row.BackColor = Color.FromName("#e4e5e7");
            //    if (e.Row.Cells[6].Text == "Pagado")
            //        e.Row.BackColor = Color.FromName("#b3d243");
            //}
        }

        protected void btnCerrarPagar_Click(object sender, EventArgs e)
        {
            FillView();
            mpePagar.Show();
        }

        protected void btnDescargarTodo_OnClick(object sender, EventArgs e)
        {

            try
            {
                List<int> lista = new List<int>();
                foreach (GridViewRow row in gvFacturas.Rows)
                {
                    var control = row.FindControl("cbChecked") as System.Web.UI.WebControls.CheckBox;
                    if (control.Checked)
                    {
                        var id = (int)this.gvFacturas.DataKeys[Convert.ToInt32(row.RowIndex)].Values["idventa"];
                        lista.Add(id);
                    }

                }
                //Crear zip
                var cte = new GAFFactura();
                var emp = new GAFEmpresa();
                byte[] bytes; string RFC = "";
                using (cte as IDisposable)
                {
                    var idEmp = ddlEmpresas.SelectedValue;
                    //var idEmp = Session["idEmpresa"] as int?;
                    if (idEmp == null)
                        return;

                    using (emp as IDisposable)
                    {
                        var empre = emp.GetById(Convert.ToInt32(idEmp));
                        RFC = empre.RFC;
                    }

                    bytes = cte.GetZipFacturas(lista, RFC);

                    /*
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + RFC + "_Comprobantes" + ".zip");
                    this.Response.ContentType = "application/zip, application/octet-stream";
                
                    this.Response.BinaryWrite(bytes);
                    this.Response.End();
                    */
                    HttpContext.Current.Response.ClearContent();
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + RFC + "_Comprobantes" + ".zip");
                    HttpContext.Current.Response.ContentType = "application/zip, application/octet-stream";
                    HttpContext.Current.Response.BinaryWrite(bytes);
                   // HttpContext.Current.Response.Flush();
                    // HttpContext.Current.Response.Close();
                    HttpContext.Current.Response.End();
                }
            }
            catch (FaultException fe)
            {
                lblError.Text = "Ocurrió un error al obtener el archivo";
            }
            catch (Exception ee)
            {
                ;
                //lblError.Text = "Ocurrió un error al obtener el archivo";
            }
          
        }

        protected void ddlLinea_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}