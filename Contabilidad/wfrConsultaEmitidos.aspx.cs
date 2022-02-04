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
using System.Data;
using ClosedXML.Excel;


namespace GAFWEB
{
    public partial class wfrConsultaEmitidos : System.Web.UI.Page
    {

        public string SelText = "Todo";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                //------rol-----------
                //lblLinea.Visible = false;
                //ddlLinea.Visible = false;
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
                string uuid = this.gvFacturas.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
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
                
                string uuid = this.gvFacturas.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
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

                var idCliente = (int)this.gvFacturas.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["IdCliente"];
                var cliente = new GAFClientes();
                using (cliente as IDisposable)
                {
                    clientes c = cliente.GetCliente(idCliente);
                    this.lblEmailCliente.Text = c.Email;
                }
                this.lblGuid.Text = this.gvFacturas.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
                this.mpeEmail.Show();
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
        private void IngesoEgresoExportar()
        {

            DataTable dt = new DataTable("GridView_Data");
            GridView gvOrders = (GridView)gvFacturas.Rows[0].FindControl("gvOrders");
            dt.Columns.Add("PreFolio");//no cacho este 
            dt.Columns.Add("Folio");//no cacho este 
            dt.Columns.Add("Folio Fiscal");//no cacho este 
            dt.Columns.Add("Fecha Emision");//no cacho este 
            dt.Columns.Add("Empresa");//no cacho este 
            dt.Columns.Add("EmpresaRFC");//no cacho este 
            dt.Columns.Add("Cliente");//no cacho este 
            dt.Columns.Add("ClienteRFC");//no cacho este 
            dt.Columns.Add("Tipo Comprobante");//no cacho este 
            dt.Columns.Add("SubTotal");//no cacho este 
            dt.Columns.Add("IVA");//no cacho este 
            dt.Columns.Add("Total");//no cacho este 
            dt.Columns.Add("FormaPago");//no cacho este 
            dt.Columns.Add("Moneda");//no cacho este 
            dt.Columns.Add("Estatus");//no cacho este 
            dt.Columns.Add("FechaCancelacion");//no cacho este 

            dt.Columns.Add("Cantidad");//no cacho este 
            dt.Columns.Add("ClaveUnidad");//no cacho este 
            dt.Columns.Add("Unidad");//no cacho este 
            dt.Columns.Add("ClaveProdServ");//no cacho este 
            dt.Columns.Add("Descripcion");//no cacho este 
            dt.Columns.Add("ValorUnitario");//no cacho este 
            dt.Columns.Add("Importe");//no cacho este 
            dt.Columns.Add("IVAConcepto");//no cacho este 


            /*
            foreach (TableCell cell in gvFacturas.HeaderRow.Cells)
            {
                if(cell.Text!= "&nbsp;")
                dt.Columns.Add(cell.Text);
            }
            foreach (TableCell cell in gvOrders.HeaderRow.Cells)
            {
                if (cell.Text != "&nbsp")
                dt.Columns.Add(cell.Text);
            }*/
            //dt.Columns.RemoveAt(0);
            foreach (GridViewRow row in gvFacturaCustumer.Rows)
            {
                GridView gvOrderscell = (row.FindControl("gvOrders2") as GridView);
                if (gvOrderscell.Rows.Count > 0)
                {
                    for (int j = 0; j < gvOrderscell.Rows.Count; j++)
                    {
                        dt.Rows.Add(row.Cells[1].Text.Replace("&nbsp;", ""), row.Cells[2].Text.Replace("&nbsp;", ""), row.Cells[3].Text.Replace("&nbsp;", ""),
                        row.Cells[4].Text.Replace("&nbsp;", ""), row.Cells[5].Text.Replace("&nbsp;", ""),
                         row.Cells[6].Text.Replace("&nbsp;", ""), row.Cells[7].Text.Replace("&nbsp;", ""),
                          row.Cells[8].Text.Replace("&nbsp;", ""), row.Cells[9].Text.Replace("&nbsp;", ""),
                           row.Cells[10].Text.Replace("&nbsp;", ""), row.Cells[11].Text.Replace("&nbsp;", ""),
                           row.Cells[12].Text.Replace("&nbsp;", ""), row.Cells[13].Text.Replace("&nbsp;", ""), row.Cells[14].Text.Replace("&nbsp;", "")
                           , row.Cells[15].Text.Replace("&nbsp;", ""),row.Cells[16].Text.Replace("&nbsp;", ""),
                           gvOrderscell.Rows[j].Cells[0].Text.Replace("&nbsp;", ""), gvOrderscell.Rows[j].Cells[1].Text.Replace("&nbsp;", ""),
                           gvOrderscell.Rows[j].Cells[2].Text.Replace("&nbsp;", ""), gvOrderscell.Rows[j].Cells[3].Text.Replace("&nbsp;", ""),
                            gvOrderscell.Rows[j].Cells[4].Text.Replace("&nbsp;", ""), gvOrderscell.Rows[j].Cells[5].Text.Replace("&nbsp;", ""),
                            gvOrderscell.Rows[j].Cells[6].Text.Replace("&nbsp;", ""), gvOrderscell.Rows[j].Cells[7].Text.Replace("&nbsp;", "")
                        );
                    }
                }
                else
                {

                    dt.Rows.Add(row.Cells[1].Text.Replace("&nbsp;", ""), row.Cells[2].Text.Replace("&nbsp;", ""), row.Cells[3].Text.Replace("&nbsp;", ""),
                           row.Cells[4].Text.Replace("&nbsp;", ""), row.Cells[5].Text.Replace("&nbsp;", ""),
                            row.Cells[6].Text.Replace("&nbsp;", ""), row.Cells[7].Text.Replace("&nbsp;", ""),
                             row.Cells[8].Text.Replace("&nbsp;", ""), row.Cells[9].Text.Replace("&nbsp;", ""),
                              row.Cells[10].Text.Replace("&nbsp;", ""), row.Cells[11].Text.Replace("&nbsp;", ""),
                               row.Cells[12].Text.Replace("&nbsp;", ""), row.Cells[13].Text.Replace("&nbsp;", ""), row.Cells[14].Text.Replace("&nbsp;", ""),
                               row.Cells[15].Text.Replace("&nbsp;", ""), row.Cells[16].Text.Replace("&nbsp;", ""));

                }
            }
     
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=GridView.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
     
        
        }
        private void PagoExportar()
        {

            DataTable dt = new DataTable("GridView_Data");
            GridView gvOrders = (GridView)gvFacturasPagos.Rows[0].FindControl("gvOrders3");
            dt.Columns.Add("PreFolio");//no cacho este 
            dt.Columns.Add("Folio");//no cacho este 
            dt.Columns.Add("Folio Fiscal");//no cacho este 
            dt.Columns.Add("Fecha Emision");//no cacho este 
            dt.Columns.Add("Empresa");//no cacho este 
            dt.Columns.Add("EmpresaRFC");//no cacho este 
            dt.Columns.Add("Cliente");//no cacho este 
            dt.Columns.Add("ClienteRFC");//no cacho este 
            dt.Columns.Add("Tipo Comprobante");//no cacho este 
            dt.Columns.Add("MontoPago");//no cacho este 
            dt.Columns.Add("SubTotalPago");//no cacho este 
            dt.Columns.Add("IVAPago");//no cacho este 
            dt.Columns.Add("FechaCancelacion");//no cacho este 

            dt.Columns.Add("UUDI");//no cacho este 
            dt.Columns.Add("PreFolio1");//no cacho este 
            dt.Columns.Add("Folio1");//no cacho este 
            dt.Columns.Add("Total");//no cacho este 
            dt.Columns.Add("SubTotal");//no cacho este 
            dt.Columns.Add("IVA");//no cacho este 
            dt.Columns.Add("Moneda1");//no cacho este 
            dt.Columns.Add("SaldoPago");//no cacho este 
            dt.Columns.Add("Parcialidad");//no cacho este 

            /*
            foreach (TableCell cell in gvFacturas.HeaderRow.Cells)
            {
                if(cell.Text!= "&nbsp;")
                dt.Columns.Add(cell.Text);
            }
            foreach (TableCell cell in gvOrders.HeaderRow.Cells)
            {
                if (cell.Text != "&nbsp")
                dt.Columns.Add(cell.Text);
            }*/
            //dt.Columns.RemoveAt(0);
         
            foreach (GridViewRow row in gvFacturaPagoCustumer.Rows)
            {
                GridView gvOrderscell = (row.FindControl("gvOrders4") as GridView);
                if (gvOrderscell.Rows.Count > 0)
                {
                    for (int j = 0; j < gvOrderscell.Rows.Count; j++)
                    {
                        dt.Rows.Add(row.Cells[1].Text.Replace("&nbsp;", ""), row.Cells[2].Text.Replace("&nbsp;", ""), row.Cells[3].Text.Replace("&nbsp;", ""),
                        row.Cells[4].Text.Replace("&nbsp;", ""), row.Cells[5].Text.Replace("&nbsp;", ""),
                         row.Cells[6].Text.Replace("&nbsp;", ""), row.Cells[7].Text.Replace("&nbsp;", ""),
                          row.Cells[8].Text.Replace("&nbsp;", ""), row.Cells[9].Text.Replace("&nbsp;", ""),
                           row.Cells[10].Text.Replace("&nbsp;", ""), row.Cells[11].Text.Replace("&nbsp;", ""),
                           row.Cells[12].Text.Replace("&nbsp;", ""), row.Cells[13].Text.Replace("&nbsp;", ""), 
                           
                           gvOrderscell.Rows[j].Cells[0].Text.Replace("&nbsp;", ""), gvOrderscell.Rows[j].Cells[1].Text.Replace("&nbsp;", ""),
                           gvOrderscell.Rows[j].Cells[2].Text.Replace("&nbsp;", ""), gvOrderscell.Rows[j].Cells[3].Text.Replace("&nbsp;", ""),
                            gvOrderscell.Rows[j].Cells[4].Text.Replace("&nbsp;", ""), gvOrderscell.Rows[j].Cells[5].Text.Replace("&nbsp;", ""),
                            gvOrderscell.Rows[j].Cells[6].Text.Replace("&nbsp;", ""), gvOrderscell.Rows[j].Cells[7].Text.Replace("&nbsp;", ""),
                            gvOrderscell.Rows[j].Cells[8].Text.Replace("&nbsp;", "")
                        );
                    }
                }
                else
                {

                    dt.Rows.Add(row.Cells[1].Text.Replace("&nbsp;", ""), row.Cells[2].Text.Replace("&nbsp;", ""), row.Cells[3].Text.Replace("&nbsp;", ""),
                           row.Cells[4].Text.Replace("&nbsp;", ""), row.Cells[5].Text.Replace("&nbsp;", ""),
                            row.Cells[6].Text.Replace("&nbsp;", ""), row.Cells[7].Text.Replace("&nbsp;", ""),
                             row.Cells[8].Text.Replace("&nbsp;", ""), row.Cells[9].Text.Replace("&nbsp;", ""),
                              row.Cells[10].Text.Replace("&nbsp;", ""), row.Cells[11].Text.Replace("&nbsp;", ""),
                               row.Cells[12].Text.Replace("&nbsp;", ""), row.Cells[13].Text.Replace("&nbsp;", ""));
                    
                }
            }
           
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=GridView.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
           

        }
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            /*    
            var ex = new Export();
            this.Response.AddHeader("Content-Disposition", "attachment; filename=Reporte.xlsx");
            this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            this.Response.BinaryWrite(ex.GridToExcel(this.gvFacturaCustumer, "Facturas"));
            this.Response.End();
              */
            var filtro = rbStatus.SelectedValue;
            if (filtro != "Pago")
            IngesoEgresoExportar();
            else
            PagoExportar();
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
            this.CalculaTotales(ViewState["facturas"] as List<vfacturasEmitidos>);
            
        }

        protected void btnEnviarMail_Click(object sender, EventArgs e)
        {
            /*
            var cliente = NtLinkClientFactory.Cliente();
            using (cliente as IDisposable)
            {
                string uuid = this.lblGuid.Text;
                
                byte[] xml = cliente.FacturaXml(uuid);
                byte[] pdf = cliente.FacturaPdf(uuid);
                
                var atts = new List<EmailAttachment>();
                atts.Add(new EmailAttachment { Attachment = xml,Name = uuid + ".xml"});
                atts.Add(new EmailAttachment {Attachment = pdf, Name = uuid + ".pdf"});
                var idEmp = Session["idEmpresa"] as int?;
                var empresa = cliente.ObtenerEmpresaById(idEmp.Value);
                var emails = new List<string>();

                if (!string.IsNullOrEmpty(this.lblEmailCliente.Text))
                {
                    emails.Add(this.lblEmailCliente.Text);
                }
                emails.AddRange(this.txtEmails.Text.Split(','));
                try
                {
                    cliente.SendMailByteArray(emails, atts, "Se envia la factura con folio " + uuid + " su la representación visual.",
                          "Envio de Facturas", empresa.Email, empresa.RazonSocial);
                }
                catch (FaultException fe)
                {
                    lblError.Text = fe.Message;
                }
                
                this.mpeEmail.Hide();
            }
             */
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
            
            
           // var perfil = Session["perfil"] as string;
           // var iniciales = Session["iniciales"] as string;
          //  var cliente = NtLinkClientFactory.Cliente();
            var factu =new  Factura();
            
            var filtro = rbStatus.SelectedValue;
            if (filtro == "Pago")
            {
                gvFacturas.Visible = false;
                gvFacturasPagos.Visible = true;
            }
            else
            {
                gvFacturas.Visible = true;
                gvFacturasPagos.Visible = false;
            }
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
                var ventas = factu.GetListEmitidos(ddlLinea.SelectedValue,fechaInicial, fechaFinal, int.Parse(this.ddlEmpresas.SelectedValue), filtro,
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
                            if (ddlLinea.SelectedValue == "A")
                            {
                                if (v.SaldoAnteriorPago == 0)
                                    v.EstusCFDI = "Liquidado";
                                else
                                    v.EstusCFDI = "Pendiente";
                            }
                            else  // las que no traen pre
                            {
                                if (v.SaldoAnteriorFactura == 0)
                                    v.EstusCFDI = "Liquidado";
                                else
                                    v.EstusCFDI = "Pendiente";

                            }
                        }
                    }
                    if(v.Monto!=null)
                    {
                        CultureInfo cul = CultureInfo.CreateSpecificCulture("es-MX");
                        decimal.Parse("0.00", NumberStyles.Currency);
                       v.SubTotalPago= ((decimal)v.Monto / (decimal)1.16);
                       v.IVAPago = ((decimal)v.Monto - Convert.ToDecimal(v.SubTotalPago));
                    }

                    else
                        if (ddlLinea.SelectedValue != "A" && filtro == "Pago" && v.Pagado != null)
                        {
                            CultureInfo cul = CultureInfo.CreateSpecificCulture("es-MX");
                            decimal.Parse("0.00", NumberStyles.Currency);
                            v.SubTotalPago = ((decimal)v.Pagado / (decimal)1.16);
                            v.IVAPago = ((decimal)v.Pagado - Convert.ToDecimal(v.SubTotalPago));
                            v.Monto = v.Pagado;
                        }
                }

               
                List<vfacturasEmitidos> lista;
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

                if (filtro != "Pago")
                {
                    this.gvFacturas.DataSource = lista;
                    this.gvFacturas.DataBind();

                    CalculaTotales(lista);
                    this.gvFacturaCustumer.DataSource = lista;
                    this.gvFacturaCustumer.DataBind();
                }
                else
                {
                    this.gvFacturasPagos.DataSource = lista;
                    this.gvFacturasPagos.DataBind();

                    CalculaTotales(lista);
                    this.gvFacturaPagoCustumer.DataSource = lista;
                    this.gvFacturaPagoCustumer.DataBind();
                }
            }
              
        }
        
        private void CalculaTotales(List<vfacturasEmitidos> lista)
        {
            
            var subt = lista.Where(c=>c.Cancelado == 0).Sum(p => p.SubTotal);
            var total = lista.Where(c=>c.Cancelado == 0).Sum(p => p.Total);
            var ivas = lista.Where(c => c.Cancelado == 0).Sum(p => p.IVA);
          //  var retiva = lista.Where(c => c.Cancelado == 0 && c.RetIva.HasValue).Sum(p => p.RetIva);
          //  var retisr = lista.Where(c => c.Cancelado == 0 && c.RetIsr.HasValue).Sum(p => p.RetIsr);
          //  var ieps = lista.Where(c => c.Cancelado == 0 && c.Ieps.HasValue).Sum(p => p.Ieps);
           // var iva =0;
             var retiva =0;
            var retisr =0;
            var ieps =0;

            if (this.gvFacturas.FooterRow != null)
            {
                this.gvFacturas.FooterRow.Cells[0].Text = "TOTAL";
                this.gvFacturas.FooterRow.Cells[10].Text = subt.Value.ToString("C");
                //this.gvFacturas.FooterRow.Cells[6].Text = subt.Value.ToString("C");
                this.gvFacturas.FooterRow.Cells[11].Text = ivas.Value.ToString("C"); 
                //this.gvFacturas.FooterRow.Cells[8].Text = retiva.ToString("C");
                //this.gvFacturas.FooterRow.Cells[9].Text = retisr.ToString("C");
                //this.gvFacturas.FooterRow.Cells[10].Text = ieps.ToString("C");
                //this.gvFacturas.FooterRow.Cells[11].Text = total.Value.ToString("C");
                this.gvFacturas.FooterRow.Cells[12].Text = total.Value.ToString("C");
           
            }
             
        }
        
        #endregion

        protected void gvFacturas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
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
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[15].Text != "Pendiente")
                 e.Row.BackColor = Color.FromName("#b3d243");
                if (!string.IsNullOrEmpty(e.Row.Cells[16].Text.Replace("&nbsp;","")))
                    e.Row.BackColor = Color.FromName("#F6DDCC");
              
                string idVenta = gvFacturas.DataKeys[e.Row.RowIndex].Value.ToString();
                GridView gvOrders = e.Row.FindControl("gvOrders") as GridView;
                gvOrders.DataSource = GetData2(idVenta);
                gvOrders.DataBind();
            }
        }
        protected void OnRowDataBound2(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string idVenta = gvFacturaCustumer.DataKeys[e.Row.RowIndex].Value.ToString();
                GridView gvOrders2 = e.Row.FindControl("gvOrders2") as GridView;
                gvOrders2.DataSource = GetData2(idVenta);
                gvOrders2.DataBind();
            }
        }
        protected void OnRowDataBound3(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!string.IsNullOrEmpty(e.Row.Cells[14].Text.Replace("&nbsp;", "")))
                  //  e.Row.BackColor = Color.FromName("#b3d243");
                e.Row.BackColor = Color.FromName("#F6DDCC");
             try
                {
                 string idVenta="";
                    if(ddlLinea.SelectedValue=="A")
                        idVenta = gvFacturasPagos.DataKeys[e.Row.RowIndex].Values["idPreFactura"].ToString();
                else
                 idVenta = gvFacturasPagos.DataKeys[e.Row.RowIndex].Value.ToString();
                if(!string.IsNullOrEmpty( idVenta))
                {
                GridView gvOrders = e.Row.FindControl("gvOrders3") as GridView;
                gvOrders.DataSource = GetData3(idVenta);
                gvOrders.DataBind();
                }
                }
             catch (Exception ex)
             { }
            }
        }
        protected void OnRowDataBound4(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    string idVenta="";
                    if(ddlLinea.SelectedValue=="A")
                        idVenta = gvFacturaPagoCustumer.DataKeys[e.Row.RowIndex].Values["idPreFactura"].ToString();
                else
                     idVenta = gvFacturaPagoCustumer.DataKeys[e.Row.RowIndex].Value.ToString();
                    if (!string.IsNullOrEmpty(idVenta))
                    {
                        GridView gvOrders2 = e.Row.FindControl("gvOrders4") as GridView;
                        gvOrders2.DataSource = GetData3(idVenta);
                        gvOrders2.DataBind();
                    }
                }
                catch (Exception ex) { }
            }
        }
        private List<Conceptos> GetData2(string venta)
        {
            long x = Convert.ToInt64(venta);
            using (var db = new GAFEntities())
            {
                var factura = db.Conceptos.Where(c => c.idFactura == x);
                return factura.ToList();
            }
        }
        private List<vfacturasEmitidos> GetData3(string venta)
        {
            try{
            List<vfacturasEmitidos> Pre = new List<vfacturasEmitidos>();
            long x = Convert.ToInt64(venta);
            using (var db = new GAFEntities())
            {
                var factura = db.PreComplementoPago.Where(c => c.idPreFactura == x).FirstOrDefault();


                if (factura != null)
                {
                    if (!string.IsNullOrEmpty(factura.Ids))
                    {
                        string[] ids = factura.Ids.Split('|');
                        foreach (var z in ids)
                        {
                            try
                            {

                                long zz = Convert.ToInt64(z);
                                vfacturasEmitidos prefactura = new vfacturasEmitidos();
                                prefactura = db.vfacturasEmitidos.Where(c => c.idPreFactura == zz).FirstOrDefault();

                                if (prefactura != null)
                                    Pre.Add(prefactura);
                            }
                            catch (Exception ex) { }
                        }
                        if (Pre.Count > 0)
                            return Pre.ToList();
                        else
                            return null;
                    }
                }
                else
                {
                   var facturax = db.FacturaPagoRelacionados.Where(c => c.IdFactura == x).FirstOrDefault();
                   if (!string.IsNullOrEmpty(facturax.Ids))
                   {
                       string[] ids = facturax.Ids.Split('|');
                       foreach (var z in ids)
                       {
                           try
                           {

                               long zz = Convert.ToInt64(z);
                               vfacturasEmitidos prefactura = new vfacturasEmitidos();
                               facturas faturass = new facturas();
                               faturass = db.facturas.Where(c => c.idVenta == zz).FirstOrDefault();
                              // prefactura = db.vfacturasEmitidos.Where(c => c.idPreFactura == zz).FirstOrDefault();
                               if (faturass != null)
                               {
                                   prefactura.Uid = faturass.Uid;
                                   prefactura.Folio = faturass.Folio;
                                   prefactura.Total = faturass.Total;
                                   prefactura.SubTotal = faturass.SubTotal;
                                   prefactura.IVA = faturass.IVA;
                                   prefactura.Moneda = faturass.Moneda;
                                   prefactura.SaldoAnteriorPago = faturass.SaldoAnteriorPago;
                                   prefactura.Parcialidad = faturass.Parcialidad;
                               }
                               if (prefactura != null)
                                   Pre.Add(prefactura);
                           }
                           catch (Exception ex) { }
                       }
                       if (Pre.Count > 0)
                           return Pre.ToList();
                       else
                           return null;
                   }

                }

                return null;
            }
            }catch(Exception ex)
            {return null;}

            
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
            this.ddlEmpresas.Items.Clear();
              
            ListItem cl2 = new ListItem();
            cl2.Text = "--Todos--";
            cl2.Value = "0";
            ddlEmpresas.Items.Add(cl2);
                  
            var Empresa = new GAFEmpresa();
            using (Empresa as IDisposable)
            {
                this.ddlEmpresas.DataSource = Empresa.GetListForLine(ddlLinea.SelectedValue);
                this.ddlEmpresas.DataBind();

            }
        }

        protected void gvFacturasPagos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvFacturasPagos.DataSource = ViewState["facturas"];
            this.gvFacturasPagos.PageIndex = e.NewPageIndex;
            this.gvFacturasPagos.DataBind();
            this.CalculaTotales(ViewState["facturas"] as List<vfacturasEmitidos>);
        }

        protected void gvFacturasPagos_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            if (e.CommandName.Equals("DescargarXml"))
            {
                string uuid = this.gvFacturasPagos.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
                var cliente = new Factura();
                using (cliente as IDisposable)
                {
                    var id = (int)this.gvFacturasPagos.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["idventa"];
                    var cfd = cliente.GetById(id);
                    string xml = Encoding.UTF8.GetString(GAFFactura.GetData(uuid, "xml"));
                    //-------------------
                    var clien = new GAFClientes(); string RFC = "";
                    using (clien as IDisposable)
                    {
                        var cli = clien.GetCliente(Convert.ToInt32(cfd.idcliente));
                        RFC = cli.RFC;
                    }
                    //------------------------
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + RFC + "_" + cfd.Folio + "_" + cfd.Fecha.ToString("yyyyMMdd") + "_" + uuid + ".xml");

                    this.Response.ContentType = "text/xml";
                    this.Response.Charset = "UTF-8";
                    this.Response.Write(xml);
                    this.Response.End();
                }
            }
            else if (e.CommandName.Equals("DescargarPdf"))
            {

                string uuid = this.gvFacturasPagos.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
                var cliente = new Factura();
                using (cliente as IDisposable)
                {
                    var id = (int)this.gvFacturasPagos.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["idventa"];
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
                        pdf = fact.GetPdfData(uuid);
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

                var idCliente = (int)this.gvFacturasPagos.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["IdCliente"];
                var cliente = new GAFClientes();
                using (cliente as IDisposable)
                {
                    clientes c = cliente.GetCliente(idCliente);
                    this.lblEmailCliente.Text = c.Email;
                }
                this.lblGuid.Text = this.gvFacturasPagos.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
                this.mpeEmail.Show();
            }
            else if (e.CommandName.Equals("Pagar"))
            {
                var id = (int)this.gvFacturasPagos.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["idventa"];
                var cliente = new Factura();
                using (cliente as IDisposable)
                {
                    var venta = cliente.GetById(id);
                    this.txtFechaPago.Text = venta.FechaPago != null ? Convert.ToDateTime(venta.FechaPago).ToString("dd/MM/yyyy") : string.Empty;
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
                foreach (GridViewRow row in gvFacturasPagos.Rows)
                {
                    var lista = ViewState["facturas"] as List<vfacturas>;
                    if (lista != null)
                    {
                        foreach (vfacturas itemVventas in lista)
                        {
                            itemVventas.Seleccionar = sel;
                        }
                    }
                    gvFacturasPagos.DataSource = lista;
                    gvFacturasPagos.DataBind();

                }
            }


        }
        
    }
}