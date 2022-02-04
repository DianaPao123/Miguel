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
    public partial class wfrFacturasConsultaB : System.Web.UI.Page
    {

        public string SelText = "Todo";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                 //------rol-----------
               // lblLinea.Visible = false;
               // ddlLinea.Visible = false;
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
                      this.ddlEmpresas.DataSource = Empresa.GetListForLine(ddlLinea.SelectedValue).Where(p => p.RFC == "MLM170606UZA" || p.RFC == "BHU1705197Y5" || p.RFC == "SAS1204131Z5");
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
                              
                              //ddlPromotor.Items.Add(cl);
                              //-----------------------------------
                              if (clientes != null)
                              {
                                  clientes = llenadoClientesEspacial(clientes);
                              }
                              //--------------------------------
                              this.ddlClientes.DataSource = clientes;
                              this.ddlClientes.DataBind();
                              var promo = cliente.GetListPromotor();
                              //this.ddlPromotor.DataSource = promo;
                              //this.ddlPromotor.DataBind();
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
                /*
                    var sesion = Session["sessionRGV"] as Business.Sesion;
            if(sesion!=null)
                if (sesion.Rol == "OperadorAdmin")
                {
                    ddlLinea.Items.Clear();
                   // ddlLinea.Items.Remove(ddlLinea.Items.FindByValue("B"));
                   // ddlLinea.Items.Remove(ddlLinea.Items.FindByValue("C"));
                    ddlLinea.Items.Insert(0, "A");
                    ddlLinea.Items.Insert(1, "D");
                }
                 */
                  
                this.FillView();
                 
      
               
            }
        }

        protected void btnCancelarSAT_Click(object sender, EventArgs e)
        {

            try
            {
                int id = (int)ViewState["IDCancelar"];
                var fact = new Factura();
                var cliente = new GAFFactura();
                var Empresa = new GAFEmpresa();
                var cli = new GAFClientes();
                using (cliente as IDisposable)
                {
                    var venta = fact.GetById(id);
                    var emp = Empresa.GetById((int)venta.IdEmpresa);
                    clientes c = cli.GetCliente(venta.idcliente);
                    //---------------
                    //var factu = fact.GetByComprobantePDF(venta.Uid);
                    //int tam_var = factu.timbre_SelloCFD.Length;
                    //string Var_Sub = factu.timbre_SelloCFD.Substring((tam_var - 8), 8);
                    string Var_Sub = "";
                    string URL = @"https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx";

                    string cadena = URL + "?&id=" + (venta.Uid).ToString().ToUpper() + "&re=" + emp.RFC + "&rr=" + c.RFC + "&tt=" + venta.Total + "&fe=" + Var_Sub;
                    //---------------


                    var cancelacion = cliente.CancelarFactura(emp.RFC, venta.Uid, cadena, c.RFC, ddlMotivo.SelectedValue, txtFolioSustituto.Text);
                    lblError.Text = cancelacion;
                    this.FillView();

                    if (cancelacion == "Comprobante Cancelado correctamente")
                    {
                        string xml = Encoding.UTF8.GetString(GAFFactura.GetData(venta.Uid, "xml"));
                        bool pago10 = xml.Contains("pago10:Pagos");
                        if (pago10)
                        {
                            PagoXML pago = new PagoXML();
                            var P = pago.DesSerializarPagos(xml);
                            foreach (var pagos in P.Pago)
                            {
                                if (pagos.DoctoRelacionado != null)
                                    foreach (var doc in pagos.DoctoRelacionado)
                                    {

                                        cliente.CancelarActaulizaMontosFactura(venta.idVenta, doc.IdDocumento, Convert.ToDecimal(doc.ImpPagado), Convert.ToInt16(doc.NumParcialidad));
                                    }
                            }

                        }

                    }
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
   
                var idCliente = (int)this.gvFacturas.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["IdCliente"];
                var cliente = new GAFClientes();
                using (cliente as IDisposable)
                {
                    clientes c = cliente.GetCliente(idCliente);
                    this.lblEmailCliente.Text = c.Email;
                }
                this.lblGuid.Text = this.gvFacturas.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
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
                int id = Convert.ToInt32(e.CommandArgument);
                ViewState["IDCancelar"] = id;
                this.mpeCancelar.Show();
         
            }

            else if (e.CommandName.Equals("Acuse"))
            {

                try
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    var cliente = new Factura();
                    var fac = new GAFFactura();
                    var Empresa = new GAFEmpresa();
                    var cli = new GAFClientes();

                    using (cliente as IDisposable)
                    {
                        var fact = cliente.GetById(id);
                        var emp = Empresa.GetById((int)fact.IdEmpresa);
                        clientes c = cli.GetCliente(fact.idcliente);

                        //var factu = cliente.GetByComprobantePDF(fact.Uid);
                        //int tam_var = factu.timbre_SelloCFD.Length;
                        //string Var_Sub = factu.timbre_SelloCFD.Substring((tam_var - 8), 8);

                        //string URL = @"https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx";

                        //string cadena = URL + "?&id=" + (fact.Uid).ToString().ToUpper() + "&re=" + emp.RFC + "&rr=" + c.RFC + "&tt=" + fact.Total + "&fe=" + Var_Sub;
                        ////---------------
                        string resultado = "";
                        bool sal = fac.GetConsultaEstatusCFDI(fact.Uid, emp.RFC, c.RFC, fact.Total.ToString(), ref resultado);

                        if (sal)
                        {
                            string[] status = resultado.Split('|');
                            var pdf = GAFFactura.GetAcuseCancelacion("/" + "AcuseCancelacion", id, status[0], status[1], status[2]);
                            if (pdf == null || pdf.Length == 0)
                            {
                                this.lblError.Text = "Archivo no encontrado";
                                return;
                            }
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + fact.idVenta + ".pdf");
                            this.Response.ContentType = "application/pdf";
                            this.Response.BinaryWrite(pdf);
                            this.Response.End();
                        }
                        else
                            lblError.Text = resultado;
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
                if (clientes != null)
                {
                    clientes = llenadoClientesEspacial(clientes);
                }
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
            this.CalculaTotales(ViewState["facturas"] as List<vfacturas>);
            
        }

        protected void btnEnviarMail_Click(object sender, EventArgs e)
        {
            Mailer m = new Mailer();
            var Empresa = new GAFEmpresa();


            var cliente = new GAFFactura();
            using (cliente as IDisposable)
            {
                string uuid = this.lblGuid.Text;

                byte[] xml = cliente.GetXmlData(uuid);
                byte[] pdf = cliente.GetPdfData(uuid);

                var atts = new List<EmailAttachment>();
                atts.Add(new EmailAttachment { Attachment = xml, Name = uuid + ".xml" });
                atts.Add(new EmailAttachment { Attachment = pdf, Name = uuid + ".pdf" });
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
                    m.Send(emails, atts, "Se envia la factura con folio " + uuid + " su la representación visual.",
                          "Envio de Facturas", empresa.Email, empresa.RazonSocial);
                }
                catch (FaultException fe)
                {
                    lblError.Text = fe.Message;
                }

                this.mpeEmail.Hide();
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
            
            
           // var perfil = Session["perfil"] as string;
           // var iniciales = Session["iniciales"] as string;
          //  var cliente = NtLinkClientFactory.Cliente();
            var sesion = Session["sessionRGV"] as Sesion;

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
                var ventas = factu.GetListPromotorOperador(ddlLinea.SelectedValue, fechaInicial, fechaFinal, int.Parse(this.ddlEmpresas.SelectedValue), filtro
                   ,sesion.Id.ToString(), int.Parse(this.ddlClientes.SelectedValue) )  ;

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

                List<vfacturas> lista;
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
              
        }
        
        private void CalculaTotales(List<vfacturas> lista)
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
        protected void ddlMotivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMotivo.SelectedValue != "01")
            {
                txtFolioSustituto.Enabled = false;
                txtFolioSustituto.Text = "";
            }
            else
            {

                txtFolioSustituto.Enabled = true;
            }
        }
        private List<clientes> llenadoClientesEspacial(List<clientes> clientess)
        {
            //-----------------------------------
                List<DatosClientesEspecial> ListaCliente = new List<DatosClientesEspecial>();
                DatosClientesEspecial lc1 = new DatosClientesEspecial(); lc1.nombre = "ARMENTA GONZALEZ JOSE LUIS"; lc1.rfc = "AEGL551115JI2"; ListaCliente.Add(lc1);
                DatosClientesEspecial lc2 = new DatosClientesEspecial(); lc2.nombre = "ARREOLA MEDINA ALFONSO"; lc2.rfc = "AEMA471010FI0"; ListaCliente.Add(lc2);
                DatosClientesEspecial lc3 = new DatosClientesEspecial(); lc3.nombre = "CAMACHO BUCIO PASTOR"; lc3.rfc = "CABP510528PL0"; ListaCliente.Add(lc3);
                DatosClientesEspecial lc4 = new DatosClientesEspecial(); lc4.nombre = "CAMACHO SANCHEZ MARIA APOLONIA"; lc4.rfc = "CASA7512189Q0"; ListaCliente.Add(lc4);
                DatosClientesEspecial lc5 = new DatosClientesEspecial(); lc5.nombre = "CRISPIN ARMENTA RICARDO"; lc5.rfc = "CIAR731018B84"; ListaCliente.Add(lc5);
                DatosClientesEspecial lc6 = new DatosClientesEspecial(); lc6.nombre = "GONZALEZ GALICIA GABINO"; lc6.rfc = "GOGG560525T29"; ListaCliente.Add(lc6);
                DatosClientesEspecial lc7 = new DatosClientesEspecial(); lc7.nombre = "GONZALEZ MORENO ESTHER GUADALUPE"; lc7.rfc = "GOME300206T96"; ListaCliente.Add(lc7);
                DatosClientesEspecial lc8 = new DatosClientesEspecial(); lc8.nombre = "GUTIERREZ ALVAREZ IRMA"; lc8.rfc = "GUAI630816SQ0"; ListaCliente.Add(lc8);
                DatosClientesEspecial lc9 = new DatosClientesEspecial(); lc9.nombre = "GUTIERREZ GONZALEZ GABRIEL"; lc9.rfc = "GUGG480324LBA"; ListaCliente.Add(lc9);
                DatosClientesEspecial lc10 = new DatosClientesEspecial(); lc10.nombre = "HERNANDEZ RESENDIZ VERONICA"; lc10.rfc = "HERV720912FV4"; ListaCliente.Add(lc10);
                DatosClientesEspecial lc11 = new DatosClientesEspecial(); lc11.nombre = "LUNA MUÑOZ BENJAMIN"; lc11.rfc = "LUMB681231U75"; ListaCliente.Add(lc11);
                DatosClientesEspecial lc12 = new DatosClientesEspecial(); lc12.nombre = "MEDINA ESPINO SALOMON"; lc12.rfc = "MEES321018C37"; ListaCliente.Add(lc12);
                //    DatosClientesEspecial lc13 = new DatosClientesEspecial(); lc13.nombre = "MIGUEL GARCIA EVARISTO"; lc13.rfc = ""; ListaCliente.Add(lc13);
                DatosClientesEspecial lc14 = new DatosClientesEspecial(); lc14.nombre = "ORTIZ CAMACHO CARLOS"; lc14.rfc = "OICC620101K29"; ListaCliente.Add(lc14);
                DatosClientesEspecial lc15 = new DatosClientesEspecial(); lc15.nombre = "ORTIZ CAMACHO GUILLERMO"; lc15.rfc = "OICG570605J76"; ListaCliente.Add(lc15);
                DatosClientesEspecial lc16 = new DatosClientesEspecial(); lc16.nombre = "RUIZ CHAVEZ CELSO"; lc16.rfc = "RUCC440406BM7"; ListaCliente.Add(lc16);
                DatosClientesEspecial lc17 = new DatosClientesEspecial(); lc17.nombre = "SANCHEZ ALBARRAN CLAUDIA CECILIA"; lc17.rfc = "SAAC8010305J6"; ListaCliente.Add(lc17);
                DatosClientesEspecial lc18 = new DatosClientesEspecial(); lc18.nombre = "SANCHEZ JACINTO FELICIANO"; lc18.rfc = "SAJF570609M99"; ListaCliente.Add(lc18);
                DatosClientesEspecial lc19 = new DatosClientesEspecial(); lc19.nombre = "SUAREZ CAMACHO CELEDONIO"; lc19.rfc = "SUCC530323KT2"; ListaCliente.Add(lc19);
                DatosClientesEspecial lc20 = new DatosClientesEspecial(); lc20.nombre = "SUAREZ CAMACHO LEANDRO"; lc20.rfc = "SUCL600228914"; ListaCliente.Add(lc20);
                DatosClientesEspecial lc21 = new DatosClientesEspecial(); lc21.nombre = "TAPIA HERNANDEZ JOSE ROBERTO"; lc21.rfc = "TAHR750118TC6"; ListaCliente.Add(lc21);
                DatosClientesEspecial lc22 = new DatosClientesEspecial(); lc22.nombre = "TAPIA LIRA MIGUEL"; lc22.rfc = "TALM610929LG3"; ListaCliente.Add(lc22);
                DatosClientesEspecial lc23 = new DatosClientesEspecial(); lc23.nombre = "CORTES HERNANDEZ JOSE MELITON"; lc23.rfc = "COHM5604018R3"; ListaCliente.Add(lc23);
                DatosClientesEspecial lc24 = new DatosClientesEspecial(); lc24.nombre = "GONZALEZ JIMENEZ FELIPE ELEUTERIO"; lc24.rfc = "GOJF690501GS0"; ListaCliente.Add(lc24);
                DatosClientesEspecial lc25 = new DatosClientesEspecial(); lc25.nombre = "GONZALEZ ROSAS GUADALUPE"; lc25.rfc = "GORG601212C37"; ListaCliente.Add(lc25);
                DatosClientesEspecial lc26 = new DatosClientesEspecial(); lc26.nombre = "TELLEZ AGUILAR MARIA REYNA"; lc26.rfc = "TEAR541030EV2"; ListaCliente.Add(lc26);
                DatosClientesEspecial lc27 = new DatosClientesEspecial(); lc27.nombre = "CABRERA FLORES GERARDO"; lc27.rfc = "CAFG780505MW3"; ListaCliente.Add(lc27);
                DatosClientesEspecial lc28 = new DatosClientesEspecial(); lc28.nombre = "CALZADA MELGAREJO TOMAS"; lc28.rfc = "CAMT560307T92"; ListaCliente.Add(lc28);
                DatosClientesEspecial lc29 = new DatosClientesEspecial(); lc29.nombre = "CANCINO REYES JORGE JESUS"; lc29.rfc = "CARJ5006162A3"; ListaCliente.Add(lc29);
                DatosClientesEspecial lc30 = new DatosClientesEspecial(); lc30.nombre = "ESTRADA PRIETO FELIX"; lc30.rfc = "EAPF541220KI6"; ListaCliente.Add(lc30);
                DatosClientesEspecial lc31 = new DatosClientesEspecial(); lc31.nombre = "GRANADOS VAZQUEZ GERARDO"; lc31.rfc = "GAVG530226R9A"; ListaCliente.Add(lc31);
                DatosClientesEspecial lc32 = new DatosClientesEspecial(); lc32.nombre = "GRANADOS VAZQUEZ JUAN"; lc32.rfc = "GAVJ360330R56"; ListaCliente.Add(lc32);
                DatosClientesEspecial lc33 = new DatosClientesEspecial(); lc33.nombre = "GUTIERREZ CABRERA FRANCISCO"; lc33.rfc = "GUCF6808098NA"; ListaCliente.Add(lc33);
                DatosClientesEspecial lc34 = new DatosClientesEspecial(); lc34.nombre = "HERNANDEZ GONZALEZ ISABEL"; lc34.rfc = "HEGI450831ND7"; ListaCliente.Add(lc34);
                DatosClientesEspecial lc35 = new DatosClientesEspecial(); lc35.nombre = "HERNANDEZ GONZALEZ JOAQUIN"; lc35.rfc = "HEGJ470602B19"; ListaCliente.Add(lc35);
                DatosClientesEspecial lc36 = new DatosClientesEspecial(); lc36.nombre = "JIMENEZ ANDRADE JUAN CARLOS"; lc36.rfc = "JIAJ831216RBA"; ListaCliente.Add(lc36);
                DatosClientesEspecial lc37 = new DatosClientesEspecial(); lc37.nombre = "MARTINEZ CABRERA MARIA ELENA"; lc37.rfc = "MACE770929EN5"; ListaCliente.Add(lc37);
                DatosClientesEspecial lc38 = new DatosClientesEspecial(); lc38.nombre = "MARTINEZ JOSE VICTOR"; lc38.rfc = "MAVI511202R68"; ListaCliente.Add(lc38);
                DatosClientesEspecial lc39 = new DatosClientesEspecial(); lc39.nombre = "MARTINEZ JIMENEZ JOSE LUIS"; lc39.rfc = "MAJL870323QX2"; ListaCliente.Add(lc39);
                DatosClientesEspecial lc40 = new DatosClientesEspecial(); lc40.nombre = "MONDRAGON PRIMERO FELIPE"; lc40.rfc = "MOPF570529SS9"; ListaCliente.Add(lc40);
                DatosClientesEspecial lc41 = new DatosClientesEspecial(); lc41.nombre = "RANGEL ROSAS CELIA"; lc41.rfc = "RARC680701JA2"; ListaCliente.Add(lc41);
                DatosClientesEspecial lc42 = new DatosClientesEspecial(); lc42.nombre = "SEBASTIAN MEDINA ALFREDO"; lc42.rfc = "SEMA660112233"; ListaCliente.Add(lc42);
                DatosClientesEspecial lc43 = new DatosClientesEspecial(); lc43.nombre = "SEPULVEDA MARTINEZ REGINO GUADALUPE"; lc43.rfc = "SEMR6009079Q3"; ListaCliente.Add(lc43);
                DatosClientesEspecial lc44 = new DatosClientesEspecial(); lc44.nombre = "VERGARA ALMARAZ SAMUEL"; lc44.rfc = "VEAS8208207P7"; ListaCliente.Add(lc44);
                DatosClientesEspecial lc45 = new DatosClientesEspecial(); lc45.nombre = "VERGARA GONZALEZ JOSE JUAN"; lc45.rfc = "VEGJ6708299T2"; ListaCliente.Add(lc45);
                DatosClientesEspecial lc46 = new DatosClientesEspecial(); lc46.nombre = "AGUILAR GODINEZ MANUEL"; lc46.rfc = "AUGM640606RJ0"; ListaCliente.Add(lc46);
                DatosClientesEspecial lc47 = new DatosClientesEspecial(); lc47.nombre = "ARRIOLA PEREZ TOMAS"; lc47.rfc = "AIPT540829KH2"; ListaCliente.Add(lc47);
                DatosClientesEspecial lc48 = new DatosClientesEspecial(); lc48.nombre = "FLORES ORTIZ ISMAEL"; lc48.rfc = "FOOI590408166"; ListaCliente.Add(lc48);
                DatosClientesEspecial lc49 = new DatosClientesEspecial(); lc49.nombre = "FUENTES FUENTES MARIA DEL SOCORRO"; lc49.rfc = "FUFS520516J78"; ListaCliente.Add(lc49);
                DatosClientesEspecial lc50 = new DatosClientesEspecial(); lc50.nombre = "GARCIA JUAREZ FRANCISCO JAVIER"; lc50.rfc = "GAJF731215PF9"; ListaCliente.Add(lc50);
                DatosClientesEspecial lc51 = new DatosClientesEspecial(); lc51.nombre = "GARCIA MAYA MIGUEL ANGEL"; lc51.rfc = "GAMM720530AJ8"; ListaCliente.Add(lc51);
                DatosClientesEspecial lc52 = new DatosClientesEspecial(); lc52.nombre = "GARCIA SANCHEZ KARINA"; lc52.rfc = "GASK751219436"; ListaCliente.Add(lc52);
                DatosClientesEspecial lc53 = new DatosClientesEspecial(); lc53.nombre = "HERNANDEZ NOLASCO GASTON"; lc53.rfc = "HENG660110RJ8"; ListaCliente.Add(lc53);
                DatosClientesEspecial lc54 = new DatosClientesEspecial(); lc54.nombre = "LOPEZ MORENO PEDRO"; lc54.rfc = "LOMP460908NR5"; ListaCliente.Add(lc54);
                DatosClientesEspecial lc55 = new DatosClientesEspecial(); lc55.nombre = "MEJIA RANGEL JOSE LUIS"; lc55.rfc = "MERL6604102E7"; ListaCliente.Add(lc55);
                DatosClientesEspecial lc56 = new DatosClientesEspecial(); lc56.nombre = "PAZ CAMACHO JOSE FRANCISCO"; lc56.rfc = "PACF630207KL8"; ListaCliente.Add(lc56);
                DatosClientesEspecial lc57 = new DatosClientesEspecial(); lc57.nombre = "PEREZ ORTEGA JOSE LUIS"; lc57.rfc = "PEOL6212133B5"; ListaCliente.Add(lc57);
                DatosClientesEspecial lc58 = new DatosClientesEspecial(); lc58.nombre = "PEREZ PADILLA BALTAZAR"; lc58.rfc = "PEPB620106GKA"; ListaCliente.Add(lc58);
                DatosClientesEspecial lc59 = new DatosClientesEspecial(); lc59.nombre = "SANCHEZ SANCHEZ VIRGINIA"; lc59.rfc = "SASV700110A11"; ListaCliente.Add(lc59);
                DatosClientesEspecial lc60 = new DatosClientesEspecial(); lc60.nombre = "SANTILLAN SANCHEZ FRANCISCO FELIPE"; lc60.rfc = "SASF7310221LA"; ListaCliente.Add(lc60);
                DatosClientesEspecial lc61 = new DatosClientesEspecial(); lc61.nombre = "SANCHEZ JUAREZ JOSE FILIBERTO"; lc61.rfc = "SAJF510822SS2"; ListaCliente.Add(lc61);
                DatosClientesEspecial lc62 = new DatosClientesEspecial(); lc62.nombre = "ALVARADO DIONICIO RICARDO"; lc62.rfc = "AADR6307298L8"; ListaCliente.Add(lc62);
                DatosClientesEspecial lc63 = new DatosClientesEspecial(); lc63.nombre = "ALVARADO DIONICIO VALENTE"; lc63.rfc = "AADV580521T87"; ListaCliente.Add(lc63);
                DatosClientesEspecial lc64 = new DatosClientesEspecial(); lc64.nombre = "ALVARADO RIVERA JUAN MANUEL"; lc64.rfc = "AARJ781030VAA"; ListaCliente.Add(lc64);
                DatosClientesEspecial lc65 = new DatosClientesEspecial(); lc65.nombre = "DIAZ CAMPOS LETICIA"; lc65.rfc = "DICL690723RD7"; ListaCliente.Add(lc65);
                DatosClientesEspecial lc66 = new DatosClientesEspecial(); lc66.nombre = "DOMINGO GONZALEZ VICTORINO"; lc66.rfc = "DOGV490225KN0"; ListaCliente.Add(lc66);
                DatosClientesEspecial lc67 = new DatosClientesEspecial(); lc67.nombre = "GARCIA VALDEZ ESTEBAN FABIAN"; lc67.rfc = "GAVE460528E80"; ListaCliente.Add(lc67);
                DatosClientesEspecial lc68 = new DatosClientesEspecial(); lc68.nombre = "MARBAN PLIEGO HERMELO"; lc68.rfc = "MAPH560803DQ3"; ListaCliente.Add(lc68);
                DatosClientesEspecial lc69 = new DatosClientesEspecial(); lc69.nombre = "NAVARRETE LOPEZ JULIO ARMANDO"; lc69.rfc = "NALJ7107231A3"; ListaCliente.Add(lc69);
                DatosClientesEspecial lc70 = new DatosClientesEspecial(); lc70.nombre = "NAVARRETE LOPEZ MARIA EUGENIA"; lc70.rfc = "NALE6511211L1"; ListaCliente.Add(lc70);
                DatosClientesEspecial lc71 = new DatosClientesEspecial(); lc71.nombre = "PEREA BERNAL KARLA FABIOLA"; lc71.rfc = "PEBK780406G49"; ListaCliente.Add(lc71);
                DatosClientesEspecial lc72 = new DatosClientesEspecial(); lc72.nombre = "SANABRIA ROA IRVING SEBASTIAN"; lc72.rfc = "SARI000408ET1"; ListaCliente.Add(lc72);
                DatosClientesEspecial lc73 = new DatosClientesEspecial(); lc73.nombre = "TREJO MORENO BULMARO RICARDO"; lc73.rfc = "TEMB610720RR2"; ListaCliente.Add(lc73);
                DatosClientesEspecial lc74 = new DatosClientesEspecial(); lc74.nombre = "GRACIA BEJARANO MARIA ANTONIA"; lc74.rfc = "GABA5507142Q2"; ListaCliente.Add(lc74);
                DatosClientesEspecial lc75 = new DatosClientesEspecial(); lc75.nombre = "TAPIA LICONA LUIS"; lc75.rfc = "TALL490829FW1"; ListaCliente.Add(lc75);
                DatosClientesEspecial lc76 = new DatosClientesEspecial(); lc76.nombre = "TENDILLA PADILLA FRANCISCO"; lc76.rfc = "TEPF621010Q5A"; ListaCliente.Add(lc76);

                clientess = (from p in clientess
                            where (from b in ListaCliente
                                   select b.rfc)
                                     .Contains(p.RFC)
                            select p).Distinct().ToList();

                return clientess;
            
            //--------------------------------
        }
    }
}