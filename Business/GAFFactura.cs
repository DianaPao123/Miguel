using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using Contract;
using CatalogosSAT;
using ClienteNtLink;
using Business.ReportExecution;
using Business.CFDI40;
using ClienteServiciosWEb;
using Contract.Complemento40;

namespace Business
{
    public class GAFFactura : GAFBusiness
    {
        public long idComprobante { get; set; }
        
        public long idComprobantePDF { get; set; }
        public string Uuid { get; set; }
        public static CatalogosSAT.c_Moneda mone;
        //private facturas _factura;
        private DatosPrefactura _factura;
        private object factura;

        public Comprobante Cfdi { get; set; }

        public List<Datosdetalle> Detalles { get; set; }


        public DatosPrefactura Factura
        {
            get { return _factura; }
            set { _factura = value; }
        }

        public clientes Receptor { get; set; }
      //  public ClientePromotor Receptor { get; set; }

        public empresa Emisor { get; set; }


        /* 
        public GAFFactura(int idFactura)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    if (idFactura == 0)
                    {
                        this.Factura = new DatosPrefactura();
                        //   this.Detalles = new List<Datosdetalle>();
                    }
                    else
                    {
                        this.Factura = db.facturas.Where(p => p.idVenta == idFactura).FirstOrDefault();
                        if (Factura == null)
                        {
                            throw new ApplicationException("La factura " + idFactura.ToString() + " No se encontró");
                        }
                        //this.Detalles = db.facturasdetalle.Where(p => p.idVenta == idFactura).ToList();
                    }
                }
            }
            catch (Exception ee)
            {


                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
            }

        }
        */
        //----------------------------------------------------------------------------
        private facturas LLenadoFactura(DatosPrefactura f)
        {
            facturas fac = new facturas();
             fac.captura=DateTime.Now;
             fac.Descuento=f.Descuento.ToString();
             fac.Fecha = f.Fecha;
             fac.Folio = f.Folio;
             fac.PreFolio = f.PreFolio;
             fac.idcliente = f.idcliente;
             fac.IdEmpresa = f.IdEmpresa;
             fac.Importe = f.Importe;
      
             if (f.Detalles != null)
                 fac.nProducto = f.Detalles.Count();
             else
                 fac.nProducto = 0;
             fac.Serie = f.Serie;
             fac.SubTotal = f.SubTotal;
            if(!string.IsNullOrEmpty( f.TipoCambio))
             fac.TipoCambio =Convert.ToDecimal( f.TipoCambio);
            fac.Total = f.Total;
             fac.Usuario = f.Usuario;
             fac.StatusFactura = "Pendiente";
             fac.TipoDocumentoStr = f.TipoDeComprobante;
             fac.Uid = f.uudi;
             fac.IdPdf = idComprobantePDF;
             fac.Cancelado = 0;
             fac.TipoDocumentoStr = f.TipoDeComprobante;
             fac.IVA = f.IVA;
             fac.FormaPago = f.FormaPago;
             //if (f.TipoRelacion!=null)
             //{
             //    fac.TipoRelacion = f.TipoRelacion[0].tipoRelacion; //
             //    if(fac.UUDI!=null)
             //    fac.UUDI = f.UUID[0];//
             //}
             fac.Moneda = f.MonedaS;
            if(f.promotor!=null)
             fac.Promotor = f.promotor;

            fac.MetodoPago = f.MetodoID;
            if(f.MontoTotalComplementoPago!=null && f.MontoTotalComplementoPago!=0)
            fac.Pagado = f.MontoTotalComplementoPago;

          return fac;
        }
        private List<Conceptos> LLenadoConcepto(DatosPrefactura f)
        {
           List< Conceptos> C = new List<Conceptos>();

            foreach (var d in f.Detalles)
            { Conceptos P=new Conceptos();
            P.Cantidad = d.ConceptoCantidad;
            P.ClaveProdServ = d.ConceptoClaveProdServ;
            P.ClaveUnidad = d.ConceptoClaveUnidad;
            P.CuentaPredial = d.ConceptoCuentaPredial;
            P.Descripcion = d.ConceptoDescripcion;
            P.Descuento = d.ConceptoDescuento;
            P.Importe = d.ConceptoImporte;
            P.NoIdentificacion = d.ConceptoNoIdentificacion;
            P.Unidad = d.ConceptoUnidad;
            P.ValorUnitario = d.ConceptoValorUnitario;
            P.IVA = d.IVA;
            C.Add(P);
            }

            return C;
        }
        //--------------------------
          public byte[] PreFacturaPreview33(long idPreFactura)
        {
              var pre = new Prefactura();

            using (pre as IDisposable)
            {
                var fact = pre.GetPreFacturaInsert(idPreFactura);

                if (fact == null)
                    return null;

                else
                {
                   return FacturaPreview33(fact, null);
                }
            }
        
        }
        //--------------
        public bool GuardarPreFactura33(long idPreFactura)
        {
            var pre = new Prefactura();

            using (pre as IDisposable)
            {
                var fact = pre.GetPreFacturaInsert(idPreFactura);

                if (fact == null)
                    return false;

                else
                {
                  //  pre.GuardarPrefacturaTimbre(idPreFactura);

                    bool salida = GuardarFactura33(fact, idPreFactura, true, null);
                    if(salida)
                    {
                        salida = false;
                        //using (var db = new GAFEntities())
                        //{
                        //    PreFactura em = db.PreFactura.Where(p => p.idPreFactura == idPreFactura).FirstOrDefault();
                        //  //  em.Estatus = 4;
                        //    em.Timbrado = true;
                        //    db.SaveChanges();
                        //    //0 -pendiente
                        //    //1- pagado
                        //    //2.-pendiente por pagar
                        //    //3- cancelado
                        //    //4-timbrado
                        //    salida = true;
                        //}
                    }
                    return salida;
                }   
            }
        }
        public string GuardarPrefactura(DatosPrefactura factura, long idPrefactura, facturaComplementos complementosF)
        {

            GAFFactura fac = new GAFFactura();
            string resul= fac.SinGuardarFactura33(factura, complementosF);
         //   string resul = "OK";
            if (resul == "OK")
            {
                //-------------------------------------------------------
                Prefactura P = new Prefactura();

                long x = P.GuardarPrefactura(factura, idPrefactura, complementosF);
                if(x!=1)
                    return "Error en al guardar en la base";
                idComprobante= P.idComprobante;
            }
            return resul;
        }
        //--------------------------------------------------------------------------------------
        public string SinGuardarFactura33(DatosPrefactura fact, facturaComplementos complementosF)
        {
            try
            {
                GAFClientes nlc = new GAFClientes();
                clientes cliente = nlc.GetCliente(fact.idcliente);
                GAFEmpresa emp = new GAFEmpresa();
                empresa empresa = emp.GetById(fact.IdEmpresa);
                // GAFFactura fac = new GAFFactura(0);
                GAFFactura fac = new GAFFactura();

                fact.Regimen = empresa.RegimenFiscal;
                fac.Emisor = empresa;
                fac.Receptor = cliente;
                fac.Detalles = fact.Detalles;
                fac.Factura = fact;//LLenadoFactura( fact);

                var comprobante = GetComprobante(fac, cliente, empresa, complementosF);
                string path = Path.Combine(ConfigurationManager.AppSettings["Resources"], empresa.RFC, "Certs");
                X509Certificate2 cert = new X509Certificate2(Path.Combine(path, "csd.cer"));
                string rutaLlave = Path.Combine(path, "csd.key");
                if (File.Exists(rutaLlave + ".pem"))
                    rutaLlave = rutaLlave + ".pem";
                GeneradorCfdi gen = new GeneradorCfdi();

                string XML = gen.GenerarCfdSinTimbrar(comprobante, cert, rutaLlave, empresa.PassKey);

                if (string.IsNullOrEmpty(XML))
                    return "Error al generar el xml para validar";
                //------------------------
                  //  ClienteTimbradoNtlink clienteSW = new ClienteTimbradoNtlink();
                 
                ServicePointManager.DefaultConnectionLimit = 200;
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) =>
                {
                    return true;
                };

                ClienteTimbradoXpress clienteWS = new ClienteTimbradoXpress();
                string timbreString = clienteWS.ValidaTimbraCfdi(XML);
              // string timbreString = clienteSW.ValidaTimbraCfdi(XML); //facturacion moderna

           
                //-------------------------
                return timbreString;
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
               // return ee.Message;
                return "Error al generar el xml para validar";
            }

        }
        //-----------------------------------------------------------------------------------------
        public bool GuardarFactura33(DatosPrefactura fact,long idPrefactura , bool enviar, facturaComplementos complementosF)
        {

            Logger.Debug(fact.Folio);
            GAFClientes nlc = new GAFClientes();
            clientes cliente = nlc.GetCliente(fact.idcliente);
            GAFEmpresa emp = new GAFEmpresa();
                       
            empresa empresa = emp.GetById(fact.IdEmpresa);
            ///...............aqui para guardar amntes-------------------------------
            
            if (fact.Folio == null)
            {
                fact.Folio = GetNextFolio(empresa.IdEmpresa);
            }
            Int64 Foli = Int64.Parse(fact.Folio);
            Int64 Foli2 = 0;

            if (!string.IsNullOrEmpty(empresa.Folio))
                 Foli2 = Int64.Parse(empresa.Folio);

            if (Foli < Foli2)
            {
                throw new FaultException("El folio de la empresa ya está en uso");

            }
            else
            {
                using (var db = new GAFEntities())
                {
                    empresa em = db.empresa.Where(p => p.IdEmpresa == empresa.IdEmpresa).FirstOrDefault();
                    em.Folio = fact.Folio;
                    db.SaveChanges();
                }
                //--------------------------------------------------------------------------
                // GAFFactura fac = new GAFFactura(0);
                GAFFactura fac = new GAFFactura();

                if (string.IsNullOrEmpty(empresa.RegimenFiscal))
                {
                    throw new FaultException("Debes capturar el regimen fiscal de la empresa");
                }
                fact.Regimen = empresa.RegimenFiscal;
                fac.Emisor = empresa;
                fac.Receptor = cliente;
                fac.Detalles = fact.Detalles;
                fac.Factura = fact;//LLenadoFactura( fact);
                // fact.ConceptosAduanera = conceptosAduana;
                if (complementosF != null)
                    if (complementosF.pagos != null && complementosF.pagos.Count > 0)
                        fac.Factura.MontoTotalComplementoPago = Convert.ToDecimal(complementosF.pagos[0].monto);


                Comprobante cfd = GAFFactura.GeneraCfd(fac, enviar, complementosF);
                if (cfd != null)
                {
                    fac.Factura.uudi = cfd.Complemento.timbreFiscalDigital.UUID;
                    fac.idComprobantePDF = cfd.idComprobantePDF;
                    Uuid = fac.Factura.uudi;
                    fac.Save(idPrefactura);
                    return true;
                }
                return false;
            }
        }

        public static Comprobante GeneraCfd(GAFFactura factura, bool enviar, facturaComplementos complementos)
        {
            try
            {

                Logger.Debug(factura.Emisor.RFC);
                empresa emp = factura.Emisor;
                /*
                var sistemaBo = new NtLinkSistema();
                var sistema = sistemaBo.GetSistema((int)emp.idSistema.Value);
                if (sistema.SaldoEmision <= 0)
                {
                    Logger.Info("Saldo: " + sistema.SaldoEmision);
                    throw new FaultException(
                        "No cuentas con saldo suficiente para emitir facturas, contacta a tu ejecutivo de ventas");
                }
                */
                clientes cliente = factura.Receptor;
                if (string.IsNullOrEmpty(cliente.RFC))
                {
                    Logger.Error("El rfc es erróneo " + cliente.RFC);
                    throw new ApplicationException("El rfc es erróneo");
                }
                string path = Path.Combine(ConfigurationManager.AppSettings["Resources"], emp.RFC, "Certs");
                X509Certificate2 cert = new X509Certificate2(Path.Combine(path, "csd.cer"));
                string rutaLlave = Path.Combine(path, "csd.key");
                if (File.Exists(rutaLlave + ".pem"))
                    rutaLlave = rutaLlave + ".pem";
                Logger.Debug("Ruta Llave " + rutaLlave);
                var comprobante = GetComprobante(factura, cliente, emp, complementos);
                GeneradorCfdi gen = new GeneradorCfdi();
                
                //comprobante.articulos = factura.Detalles;


                gen.GenerarCfd(comprobante, cert, rutaLlave, emp.PassKey);
                string ruta = Path.Combine(ConfigurationManager.AppSettings["Salida"], emp.RFC);
                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);
                string xmlFile = Path.Combine(ruta, comprobante.Complemento.timbreFiscalDigital.UUID + ".xml");
                Logger.Debug(comprobante.XmlString);
                StreamWriter sw = new StreamWriter(xmlFile, false, Encoding.UTF8);

                sw.Write(comprobante.XmlString);

                sw.Close();
                 
                byte[] pdf = new byte[0];

                try
                {
                    long id = 0;
                    
                    pdf = gen.GetPdfFromComprobante(comprobante, emp.Orientacion, factura.Factura.TipoDocumento, ref id, factura.Factura.Metodo);
                    comprobante.idComprobantePDF = id;
                    string pdfFile = Path.Combine(ruta, comprobante.Complemento.timbreFiscalDigital.UUID + ".pdf");
                    File.WriteAllBytes(pdfFile, pdf);
                    
                }
                catch (Exception ee)
                {
                    Logger.Error(ee);
                    if (ee.InnerException != null)
                        Logger.Error(ee.InnerException);
                }
                if (enviar)
                {
                    try
                    {

                        Logger.Debug("Enviar Correo");
                        byte[] xmlBytes = Encoding.UTF8.GetBytes(comprobante.XmlString);
                        var atts = new List<EmailAttachment>();
                        atts.Add(new EmailAttachment
                        {
                            Attachment = xmlBytes,
                            Name = comprobante.Complemento.timbreFiscalDigital.UUID + ".xml"
                        });
                        atts.Add(new EmailAttachment
                        {
                            Attachment = pdf,
                            Name = comprobante.Complemento.timbreFiscalDigital.UUID + ".pdf"
                        });
                        Mailer m = new Mailer();
                        if (factura.Receptor.Bcc != null)
                            m.Bcc = factura.Receptor.Bcc;
                        List<string> emails = new List<string>();
                        emails.Add(cliente.Email);
                        if (comprobante.TipoDeComprobante == "I")
                        {
                            m.Send(emails, atts,
                                "Se envia la factura con folio " + comprobante.Complemento.timbreFiscalDigital.UUID +
                                " en formato XML y PDF.",
                                "Envío de Factura", emp.Email, emp.RazonSocial);
                        }
                        if (comprobante.TipoDeComprobante == "E")
                        {
                            m.Send(emails, atts,
                                "Se envia la nota de crédito con folio " + comprobante.Complemento.timbreFiscalDigital.UUID +
                                " en formato XML y PDF.",
                                "Envío de la nota de crédito", emp.Email, emp.RazonSocial);
                        }
                        if (comprobante.TipoDeComprobante == "P")
                        {
                            m.Send(emails, atts,
                                "Se envia el comprobante de pago con folio " + comprobante.Complemento.timbreFiscalDigital.UUID +
                                " en formato XML y PDF.",
                                "Envío el comprobante de pago", emp.Email, emp.RazonSocial);
                        }
                    }
                    catch (Exception ee)
                    {
                        Logger.Error(ee.Message);
                        if (ee.InnerException != null)
                            Logger.Error(ee.InnerException);
                    }
                   
                }
                return comprobante;
            }
            catch (FaultException fe)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                if (ex.InnerException != null)
                    Logger.Error(ex.InnerException);
                return null;
            }
                   
        }

       
        
        public  byte[] GetXmlData(string uuid)
        {
            return GetData(uuid, "xml");
        }

        public  byte[] GetPdfData(string uuid)
        {
            return GetData(uuid, "pdf");
        }
        /*
        public static byte[] GetPdfDataRetenciones(string uuid,string rfc,string tipo)
        {
            string ruta = Path.Combine(ConfigurationManager.AppSettings["Salida"], rfc);
            if (File.Exists(Path.Combine(ruta, uuid + "."+tipo)))
            {
                var bytesRes = File.ReadAllBytes(Path.Combine(ruta, uuid +"."+ tipo));
                return bytesRes;
            }
            else
                return null;
        }
        */
        public facturas GetFacturaUUID(string uuid)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    var venta = db.facturas.Where(p => p.Uid == uuid).FirstOrDefault();
                    return venta;
                }
            }
            catch (Exception ex)
            { return null; }
        }
        public static byte[] GetData(string uuid, string tipo)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    var venta = db.facturas.Where(p => p.Uid == uuid).FirstOrDefault();
                    if (venta == null)
                    {
                        Logger.Error("No se encontró la factura: " + uuid);
                        return null;
                    }
                    
            
                    var empresa = db.empresa.Where(p => p.IdEmpresa == venta.IdEmpresa).FirstOrDefault();
                    if (empresa == null)
                    {
                        Logger.Error("No se encontró la factura: " + uuid);
                        return null;
                    }
                    string ruta = Path.Combine(ConfigurationManager.AppSettings["Salida"], empresa.RFC);
                    if (File.Exists(Path.Combine(ruta, uuid + ".xml")))
                    {
                        var bytes = File.ReadAllBytes(Path.Combine(ruta, uuid + "." + tipo));
                        return bytes;
                    }
                    else
                    {
                        /*
                        Logger.Error("No se encontró la factura: " + uuid);
                        var gen = new GeneradorCfdi();
                        var tipoDoc = (TipoDocumento)Enum.Parse(typeof (TipoDocumento), venta.TipoDocumentoStr, true);
                        if (venta.IdPdf.HasValue)
                        {
                        
                            Logger.Info("Generando nuevo PDF");
                            var res = gen.GetPdfFromComprobante(empresa.RFC, tipoDoc, empresa.IdEmpresa, venta.idcliente,
                                venta.IdPdf.Value);
                            return res;

                        }*/
                        return null;
                    }

                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }
        }
        
        /*
        public void EnviarFactura(string rfc, string folioFiscal, List<string> rec, List<string> bcc)
        {
            string ruta = Path.Combine(ConfigurationManager.AppSettings["Salida"], rfc);
            string pdfFile = Path.Combine(ruta, folioFiscal + ".pdf");
            string xmlFile = Path.Combine(ruta, folioFiscal + ".xml");

            if (File.Exists(pdfFile) && File.Exists(xmlFile))
            {
                try
                {
                    using (var db = new NtLinkLocalServiceEntities())
                    {
                        
                        var venta = db.facturas.FirstOrDefault(p => p.Uid == folioFiscal);
                        if (venta == null)
                        {
                            throw new FaultException("No se encontró la factura");
                        }
                        var emp = db.empresa.FirstOrDefault(e => e.IdEmpresa == venta.IdEmpresa);
                        Logger.Debug("Enviar Correo");
                        byte[] xmlBytes = File.ReadAllBytes(xmlFile);
                        var atts = new List<EmailAttachment>();
                        atts.Add(new EmailAttachment
                        {
                            Attachment = xmlBytes,
                            Name = Path.GetFileName(xmlFile)
                        });
                        atts.Add(new EmailAttachment
                        {
                            Attachment = File.ReadAllBytes(pdfFile),
                            Name = Path.GetFileName(pdfFile)
                        });
                        Mailer m = new Mailer();
                        if (bcc != null && bcc.Count > 0)
                        {
                            m.Bcc = bcc[0];
                        }

                        m.Send(rec, atts,
                            "Se envia la factura con folio " + folioFiscal +
                            " en formato XML y PDF.",
                            "Envío de Factura", emp.Email, emp.RazonSocial);
                         
                    }

                }
                catch (FaultException ee)
                {
                    Logger.Error(ee + folioFiscal + " " + rfc);
                    throw;

                }
                catch (Exception ee)
                {
                    Logger.Error(ee.Message);
                    if (ee.InnerException != null)
                        Logger.Error(ee.InnerException);
                }
            }
            else
            {
                throw new FaultException("No se encontró la factura");
            }

        }

        private static bool ValidaSaldo(long idSistema)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    var sis = db.Sistemas.FirstOrDefault(p=>p.IdSistema == idSistema);
                    if (sis.SaldoEmision <= 0)
                    {
                        throw new FaultException("No cuentas con saldo suficiente para emitir facturas, contacta a tu ejecutivo de ventas");
                    }
                  

                }
                return true;
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                throw;
            }
        }
        */

        public  byte[] GetZipFacturas(List<int> lista, string rfc)
        {
            var zip = Path.Combine(ConfigurationManager.AppSettings["TmpFiles"], Guid.NewGuid().ToString() + ".zip");
            string ruta = Path.Combine(ConfigurationManager.AppSettings["Salida"], rfc);
            var list = new List<string>();
            foreach (var i in lista)
            {
                using (var db = new GAFEntities())
                {
                    var venta = db.facturas.FirstOrDefault(p => p.idVenta == i);
                    string pdfFile = Path.Combine(ruta, venta.Uid + ".pdf");
                    string xmlFile = Path.Combine(ruta, venta.Uid + ".xml");
                   
                    if (File.Exists(pdfFile))
                        list.Add(pdfFile);
                    if (File.Exists(xmlFile))
                        list.Add(xmlFile);
                }
            }
            ZipUtility.CompressOrAppendZip(list, zip);
            return File.ReadAllBytes(zip);

        }
        
        private static Comprobante GetComprobante(GAFFactura factura, clientes cliente, empresa emp, facturaComplementos complementos)
        {

            Logger.Debug(factura.Factura.Folio);
           // if (!ValidaSaldo(factura.Emisor.idSistema.Value))
           //     return null;
            Comprobante comprobante = new Comprobante();
            comprobante.Emisor = new ComprobanteEmisor();
            comprobante.Emisor.Nombre = emp.RazonSocial;

              CatalogosSAT.OperacionesCatalogos o9 = new CatalogosSAT.OperacionesCatalogos();
            
              /*CatalogosSAT.c_Moneda*/ mone = o9.Consultar_Moneda(factura.Factura.MonedaID);
          
           
            comprobante.Emisor.RegimenFiscal = factura.Factura.Regimen;
            comprobante.Emisor.Rfc = emp.RFC;
        
            comprobante.Titulo = "Factura";
            if (factura.Factura.TipoDocumento == TipoDocumento.ComplementoPagos)
                comprobante.TipoDeComprobante = "P";
            else
            {
                if (factura._factura.TipoDeComprobante == "Ingreso")
                comprobante.TipoDeComprobante = "I";//cambio obligado
                if (factura._factura.TipoDeComprobante == "Egreso")
                    comprobante.TipoDeComprobante = "E";//cambio obligado
                

            }
   

            if (factura.Factura.TipoRelacion != null)
            {

                List<ComprobanteCfdiRelacionados> LREL = new List<ComprobanteCfdiRelacionados>();
                foreach (var rela in factura.Factura.TipoRelacion)
                {
                    ComprobanteCfdiRelacionados rel = new ComprobanteCfdiRelacionados();
                    rel.TipoRelacion = rela.tipoRelacion;

                    if (rela.uuid != null)
                    {

                        List<ComprobanteCfdiRelacionadosCfdiRelacionado> LisUUID = new List<ComprobanteCfdiRelacionadosCfdiRelacionado>();
                        foreach (var uudi in rela.uuid)
                        {
                            ComprobanteCfdiRelacionadosCfdiRelacionado uui = new ComprobanteCfdiRelacionadosCfdiRelacionado();

                            uui.UUID = uudi;
                            LisUUID.Add(uui);
                        }
                        rel.CfdiRelacionado = LisUUID;



                    }
                    LREL.Add(rel);
                }
                comprobante.CfdiRelacionados = LREL;

            }

            /*
            if (factura.Factura.NotaCredito)
            {
                comprobante.TipoDeComprobante = "E";//cambio obligado
                comprobante.Titulo = "Nota de Crédito";
            }
                */
            if (factura.Factura.TipoDocumento == TipoDocumento.ReciboHonorarios)
            {
                comprobante.Titulo = "Recibo de Honorarios";
            }
            if (factura.Factura.TipoDocumento == TipoDocumento.Arrendamiento)
            {
                comprobante.Titulo = "Recibo de Arrendamiento";
            }
            if (factura.Factura.TipoDocumento == TipoDocumento.CartaPorte)
            {
                comprobante.Titulo = "Carta Porte";
            }
            /*
            if (factura.Factura.TipoDocumento == TipoDocumento.Donativo)
            {
                    comprobante.TipoDeComprobante = "I";//cambio obligado

                comprobante.DonatAprobacion = factura.Factura.DonativoAutorizacion;
                comprobante.DonatFecha = factura.Factura.DonativoFechaAutorizacion.ToString("dd/MM/yyyy");
                comprobante.DonatLeyenda =
                    "Este comprobante ampara un donativo, el cual será destinado por la donataria a los fines propios de su objeto social. En el caso de que los bienes donados hayan sido deducidos previamente para los efectos del impuesto sobre la renta, este donativo no es deducible. La reproducción no autorizada de este comprobante constituye un delito en los términos de las disposiciones fiscales.";

                comprobante.Titulo = "Recibo de Donativo";
            }
            */
            else if (factura.Factura.TipoDocumento == TipoDocumento.Nomina)
            {
                  comprobante.TipoDeComprobante = "E";//cambio obligado
            }
            // comprobante.Exportacion = factura.Factura.Exportacion;
            comprobante.Exportacion = "01";

            comprobante.Receptor = new ComprobanteReceptor();
            comprobante.Receptor.Nombre = cliente.RazonSocial;
            comprobante.Receptor.Rfc = cliente.RFC;
            comprobante.Receptor.DomicilioFiscalReceptor = cliente.CP;
            comprobante.Receptor.RegimenFiscalReceptor = cliente.RegimenFiscal;
            comprobante.Receptor.UsoCFDI = factura.Factura.UsoCFDI;

           
            comprobante.LugarExpedicion = factura.Factura.LugarExpedicion;
            comprobante.Fecha =factura.Factura.Fecha.ToString("s");
            comprobante.Total = Decimal.Round(factura.Factura.Total, (int)mone.Decimales);
            //if (factura.Factura.Folio == null)
            //{
            //    factura.Factura.Folio = GetNextFolio(factura.Factura.IdEmpresa);
            //}
           // comprobante.Leyenda = factura.Factura.Leyenda;
            comprobante.LeyendaInferior = emp.LeyendaInferior;
            comprobante.LeyendaSuperior = emp.LeyendaSuperior;
            comprobante.Folio = factura.Factura.Folio;
            comprobante.LugarExpedicion = factura.Factura.LugarExpedicion;
            if (!string.IsNullOrEmpty(factura._factura.MetodoID))
            {

                comprobante.MetodoPagoSpecified = true; 
                comprobante.MetodoPago = factura._factura.MetodoID;
            }
            else

                comprobante.MetodoPagoSpecified = false;
            if (!string.IsNullOrEmpty(factura._factura.FormaPagoID))
            {
                comprobante.FormaPagoSpecified = true;
               comprobante.FormaPago = factura.Factura.FormaPagoID;
            }
            else
                comprobante.FormaPagoSpecified = false;
            if(!string.IsNullOrEmpty(factura.Factura.TipoCambio))
            {comprobante.TipoCambioSpecified=true;
            comprobante.TipoCambio =Convert.ToDecimal(factura.Factura.TipoCambio);
            }
            else
                comprobante.TipoCambioSpecified=false;
            if (!string.IsNullOrEmpty(factura.Factura.Confirmacion))
                comprobante.Confirmacion = factura.Factura.Confirmacion;


            comprobante.Moneda = factura.Factura.MonedaID;
            //comprobante.Regimen = comprobante.Emisor.RegimenFiscal[0].Regimen;
            comprobante.SubTotal = Decimal.Round(factura.Factura.SubTotal, (int)mone.Decimales);// factura.Factura.Total.Value - factura.Factura.IVA.Value + factura.Factura.RetencionIva;
            comprobante.Serie = factura.Factura.Serie;
            /*
            comprobante.VoBoNombre = factura.Factura.VoBoNombre;
            comprobante.VoBoPuesto = factura.Factura.VoBoPuesto;
            comprobante.VoBoArea = factura.Factura.VoBoArea;
            comprobante.AutorizoNombre = factura.Factura.AutorizoNombre;
            comprobante.AutorizoPuesto = factura.Factura.AutorizoPuesto;
            comprobante.AutorizoArea = factura.Factura.AutorizoArea;
            comprobante.RecibiNombre = factura.Factura.RecibiNombre;
            comprobante.RecibiPuesto = factura.Factura.RecibiPuesto;
            comprobante.RecibiArea = factura.Factura.RecibiArea;
            comprobante.VoBoTitulo = factura.Factura.VoBoTitulo;
            comprobante.RecibiTitulo = factura.Factura.RecibiTitulo;
            comprobante.AutorizoTitulo = factura.Factura.AutorizoTitulo;
            comprobante.AgregadoArea = factura.Factura.AgregadoArea;
            comprobante.AgregadoNombre = factura.Factura.AgregadoNombre;
            comprobante.AgregadoPuesto = factura.Factura.AgregadoPuesto;
            comprobante.AgregadoTitulo = factura.Factura.AgregadoTitulo;
            */
            if (!string.IsNullOrEmpty(factura.Factura.CondicionesPago))
                comprobante.CondicionesDePago = factura.Factura.CondicionesPago;
            //---------------------------------------------------------
            
           // comprobante.FechaPago = factura.Factura.FechaPago;
            comprobante.Proyecto = factura.Factura.Proyecto;//campo nuevo
            comprobante.CURPEmisor = emp.CURP;
           // comprobante.TituloOtros = factura.Factura.TituloOtros;
           
            List<ComprobanteConcepto> conceptos = new List<ComprobanteConcepto>();
           
            decimal DescuentosTotales = 0M;
            foreach (Datosdetalle detalle in factura.Detalles)
            {
                ComprobanteConcepto con = new ComprobanteConcepto();
                con.Descripcion = detalle.Descripcion;
                if (!string.IsNullOrEmpty(detalle.ConceptoNoIdentificacion))
                    con.NoIdentificacion = detalle.ConceptoNoIdentificacion;
                    con.Cantidad = detalle.ConceptoCantidad;
                    con.ValorUnitario = numerodecimales(detalle.ConceptoValorUnitario, (int)mone.Decimales);
                    con.Importe = numerodecimales(detalle.ConceptoImporte, (int)mone.Decimales);
                    con.Unidad = detalle.ConceptoUnidad;
                    con.ClaveProdServ = detalle.ConceptoClaveProdServ;
                    con.ClaveUnidad = detalle.ConceptoClaveUnidad;
                    con.Descripcion = detalle.ConceptoDescripcion;
                //con.ObjetoImp = detalle.ObjetoImp;
               if( comprobante.TipoDeComprobante == "P")  //para 40
                    con.ObjetoImp = "01";
                 else 
                con.ObjetoImp = "02";

                if (detalle.ConceptoDescuento != null)
                    {
                      
                        con.DescuentoSpecified = true;
                        con.Descuento = numerodecimales((decimal)detalle.ConceptoDescuento, (int)mone.Decimales); 
                        DescuentosTotales = DescuentosTotales + Convert.ToDecimal( con.Descuento);

                    }
                    else
                        con.DescuentoSpecified = false;
                   //------------------------------------------------concepto.traslado
                    if (detalle.ConceptoTraslados != null)
                    {
                        if(detalle.ConceptoTraslados.Count>0)
                        {
                        if(con.Impuestos==null)
                        con.Impuestos = new ComprobanteConceptoImpuestos();

                        List<ComprobanteConceptoImpuestosTraslado> LC = new List<ComprobanteConceptoImpuestosTraslado>();
                        foreach (var tras in detalle.ConceptoTraslados)
                        {
                            ComprobanteConceptoImpuestosTraslado C = new ComprobanteConceptoImpuestosTraslado();
                            C.Base=tras.Base;
                            if(tras.Importe!=null)
                            {
                            
                            C.ImporteSpecified=true;
                            C.Importe=numerodecimales((decimal)tras.Importe ,(int)mone.Decimales);
                            }
                            else
                                C.ImporteSpecified=false;

                            C.Impuesto = tras.Impuesto;
  
                            if(tras.TasaOCuota!=null)
                            {
                            C.TasaOCuotaSpecified=true;
                            C.TasaOCuota=tras.TasaOCuota;
                            }
                            else
                            C.TasaOCuotaSpecified=false;
                            C.TipoFactor=tras.TipoFactor;
                            LC.Add(C);
                            
                        }
                        con.Impuestos.Traslados = LC.ToArray();
                    }
                        

                    }
                //_----------------------------------------------------------
                    if (detalle.ConceptoRetenciones != null)// concepto.retencion
                    {
                        if (detalle.ConceptoRetenciones.Count > 0)
                        {
                            if (con.Impuestos == null)
                                con.Impuestos = new ComprobanteConceptoImpuestos();

                            int i = 0;
                            List<ComprobanteConceptoImpuestosRetencion> LR = new List<ComprobanteConceptoImpuestosRetencion>();
                            foreach (var tras in detalle.ConceptoRetenciones)
                            {
                                ComprobanteConceptoImpuestosRetencion R = new ComprobanteConceptoImpuestosRetencion();
                                R.Base = tras.Base;
                                R.Importe = numerodecimales(Convert.ToDecimal(tras.Importe), (int)mone.Decimales);
                                R.Impuesto = tras.Impuesto;
                                R.TasaOCuota = tras.TasaOCuota;
                                R.TipoFactor = tras.TipoFactor;
                                LR.Add(R);
                                i++;
                            }
                            con.Impuestos.Retenciones = LR.ToArray();
                        }
                    }
                //------------------------------------------------------------------------

                conceptos.Add(con);
            }

            if (DescuentosTotales > 0)
            {
                comprobante.DescuentoSpecified = true;
                comprobante.Descuento =  numerodecimales(Convert.ToDecimal(DescuentosTotales), (int)mone.Decimales);
            }
            else
                comprobante.DescuentoSpecified = false;
        
            comprobante.Conceptos = conceptos.ToArray(); 
            ///--------------------------------------------------------------------fin conceptos
            /////---------------------------------------------Impuestos externos-------------------------------------------
                     List<DatosdetalleTraslado> DT = new List<DatosdetalleTraslado>();
                     List<DatosdetalleRetencion> DR = new List<DatosdetalleRetencion>();
                     
                     foreach (Datosdetalle detalle in factura.Detalles)//recolectar los impuestos totales
                     {
                         if(detalle.ConceptoTraslados!=null)
                         foreach (var tras in detalle.ConceptoTraslados)
                         {
                             bool x = false;
                        string Tasa = "";
                        if (!string.IsNullOrEmpty(tras.TasaOCuota))
                            Tasa = tras.TasaOCuota.ToString();
                            foreach (var dt in DT)
                             {
                               if (dt.Impuesto == tras.Impuesto && dt.TasaOCuota == Tasa && dt.TipoFactor == tras.TipoFactor)
                               {
                                   if (tras.Importe != null)
                                   {
                                    dt.Importe = ((decimal)dt.Importe + (decimal)tras.Importe);
                                    dt.Base = (dt.Base + tras.Base);//
                                    }
                                 else
                                {
                                    if (dt.Importe!=null)
                                        dt.Importe = dt.Importe;
                                    if (dt.Base!=null)//
                                        dt.Base = dt.Base;//
                                }
                                x = true;

                               }
                             }

                             if(x==false)
                             if (!string.IsNullOrEmpty(tras.TipoFactor) && !string.IsNullOrEmpty(tras.Impuesto) )
                             {
                                 DatosdetalleTraslado Dtras = new DatosdetalleTraslado();
                                 Dtras.TipoFactor = tras.TipoFactor;
                                 Dtras.Impuesto = tras.Impuesto;
                                 Dtras.TasaOCuota = Tasa;
                                if (tras.Importe != null)
                                    Dtras.Importe = tras.Importe;
                                Dtras.Base = tras.Base;//
                                DT.Add(Dtras);
                             }
                         
                         }
                         //-------------
                         if(detalle.ConceptoRetenciones!=null)
                         foreach (var tras in detalle.ConceptoRetenciones)
                         {
                             bool x = false;

                             string Tasa = tras.TasaOCuota.ToString();
                             foreach (var dt in DR)
                             {
                                 if (dt.Impuesto == tras.Impuesto)
                                 {
                                     x = true;
                                     dt.Importe = dt.Importe + tras.Importe;
                                 }
                             }

                             if (x == false)
                               if (!string.IsNullOrEmpty(tras.Impuesto))
                             {
                                 DatosdetalleRetencion Dtras = new DatosdetalleRetencion();
                                 Dtras.Impuesto = tras.Impuesto;
                                 Dtras.Importe = tras.Importe;
                                 DR.Add(Dtras);
                             }
                             
                         }

                     }
            //--------------------------------------------------ordenar los traslado y las retenciones
                     List<ComprobanteImpuestosTraslado> listaTraslados = new List<ComprobanteImpuestosTraslado>();
                     List<ComprobanteImpuestosRetencion> listaRetenciones = new List<ComprobanteImpuestosRetencion>();

            decimal totalTraslado = 0;
            if (DT.Count > 0)
            {

                foreach (var tras in DT)
                {
                    if (tras.TipoFactor != "Exento")
                    {
                        ComprobanteImpuestosTraslado tr = new ComprobanteImpuestosTraslado();
                        if (tras.Importe != null)
                        {
                            tr.ImporteSpecified = true;
                            tr.Importe = numerodecimales(Convert.ToDecimal(tras.Importe), (int)mone.Decimales);
                        }
                        else
                            tr.ImporteSpecified = false;
                        tr.Impuesto = tras.Impuesto;
                        if (!string.IsNullOrEmpty(tras.TasaOCuota))
                        {
                            tr.TasaOCuotaSpecified = true;
                            tr.TasaOCuota = tras.TasaOCuota;
                        }
                        else
                            tr.TasaOCuotaSpecified = false;
                        tr.TipoFactor = tras.TipoFactor;
                        tr.Base = numerodecimales(Convert.ToDecimal(tras.Base), (int)mone.Decimales);//

                        listaTraslados.Add(tr);
                        totalTraslado = totalTraslado + Convert.ToDecimal(tr.Importe);
                    }
                    else
                    {
                        ComprobanteImpuestosTraslado tr = new ComprobanteImpuestosTraslado();

                        tr.Impuesto = tras.Impuesto;
                        tr.TipoFactor = tras.TipoFactor;
                        tr.Base = numerodecimales(Convert.ToDecimal(tras.Base), (int)mone.Decimales);//
                        listaTraslados.Add(tr);
                    }
                }
            }
                    
                     decimal totalRetecion = 0;
                     if (DR.Count > 0)
                     {
         
                         foreach (var tras in DR)
                         {
                             ComprobanteImpuestosRetencion tr = new ComprobanteImpuestosRetencion();
                             tr.Importe = numerodecimales(Convert.ToDecimal(tras.Importe), (int)mone.Decimales);
                             tr.Impuesto =tras.Impuesto;
                             listaRetenciones.Add(tr);
                             totalRetecion = totalRetecion +Convert.ToDecimal( tr.Importe);
                         }
                     }
            //----------------------------

             ComprobanteImpuestos impuestos = new ComprobanteImpuestos();
            if(listaTraslados.Count>0)
            {
                impuestos.Traslados = listaTraslados.ToArray();
                impuestos.TotalImpuestosTrasladadosSpecified = true;
                impuestos.TotalImpuestosTrasladados = numerodecimales(totalTraslado, (int)mone.Decimales);
            }

            if (listaRetenciones.Count() > 0)
            {
               
                impuestos.Retenciones = listaRetenciones.ToArray();//--
                impuestos.TotalImpuestosRetenidos = numerodecimales(totalRetecion, (int)mone.Decimales);//--
                impuestos.TotalImpuestosRetenidosSpecified = true;//--
            }
           if(impuestos!=null)
               if(impuestos.Retenciones!=null ||impuestos.Traslados!=null)
            comprobante.Impuestos = impuestos;


           
            comprobante.CantidadLetra = CantidadLetra.Enletras(comprobante.Total.ToString(), comprobante.Moneda);
            /*
            if (factura.Factura.TipoDocumento == TipoDocumento.FacturaAduanera ||
                factura.Factura.TipoDocumento == TipoDocumento.FacturaLiverpool ||
                 factura.Factura.TipoDocumento == TipoDocumento.FacturaMabe ||
                  factura.Factura.TipoDocumento == TipoDocumento.FacturaSorianaCEDIS ||
                   factura.Factura.TipoDocumento == TipoDocumento.FacturaSorianaTienda ||
                   factura.Factura.TipoDocumento == TipoDocumento.FacturaAdo ||
                  factura.Factura.TipoDocumento == TipoDocumento.FacturaDeloitte ||
                factura.Factura.TipoDocumento == TipoDocumento.Constructor ||
                factura.Factura.TipoDocumento == TipoDocumento.ConstructorFirmas ||
                  factura.Factura.TipoDocumento == TipoDocumento.CorporativoAduanal ||
                factura.Factura.TipoDocumento == TipoDocumento.ConstructorFirmasCustom)
            {
                comprobante.DatosAduana = factura.Factura.DatosAduanera;
                if (!string.IsNullOrEmpty(comprobante.DatosAduana.Saldo))
                {
                    var saldo = decimal.Parse(comprobante.DatosAduana.Saldo, NumberStyles.Currency, new CultureInfo("es-MX"));
                    comprobante.CantidadLetra = CantidadLetra.Enletras(saldo.ToString(), comprobante.Moneda);
                }
                if (factura.Factura.TipoDocumento == TipoDocumento.ConstructorFirmasCustom)
                {
                    var saldo = factura.Factura.Total;
                    comprobante.CantidadLetra = CantidadLetra.Enletras(saldo.ToString(),comprobante.Moneda);
                
                }
                comprobante.ConceptosAduana = factura.Factura.ConceptosAduanera
                    .Select(p => new ComprobanteConcepto
                        {
                            Descripcion = p.Descripcion,
                            ValorUnitario =Convert.ToString( p.Total),
                            Importe = p.Total.ToString(),
                            Detalles = p.Descripcion2,NoIdentificacion = p.Codigo
                        }).ToList();
            }

          */
            
            //----------------------nuevo para impuestos locales------------------------------
            //----------------------esto es nuevo para los impuestos locales
            if (factura.Factura.ImpuestosLocales!=null)
           comprobante = ComplementoImpuestosLocales(comprobante, factura.Factura.ImpuestosLocales);
            //---------------fin de impuestos locales (nuevo)
            if(complementos!=null)
           comprobante = LLenaComplemento(comprobante, complementos);

            return comprobante;
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

        public static byte[] GeneraPreviewRs(GAFFactura factura, facturaComplementos complementosF)
        {
            try
            {
                empresa emp = factura.Emisor;
                clientes cliente = factura.Receptor;
                if (string.IsNullOrEmpty(cliente.RFC))
                {
                    throw new ApplicationException("El rfc es erróneo");
                }
                string path = Path.Combine(ConfigurationManager.AppSettings["Resources"], emp.RFC, "Certs");
                X509Certificate2 cert = new X509Certificate2(Path.Combine(path, "csd.cer"));
                string rutaLlave = Path.Combine(path, "csd.key");

                var comprobante = GetComprobante(factura, cliente, emp,complementosF);
                GeneradorCfdi gen = new GeneradorCfdi();
                gen.GenerarCfdPreview(comprobante, cert, rutaLlave, emp.PassKey);
                string ruta = Path.Combine(ConfigurationManager.AppSettings["Salida"], emp.RFC);
                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);
                //comprobante.CantidadLetra = CantidadLetra.Enletras(comprobante.total.ToString(), comprobante.Moneda);
                long id = 0;
                byte[] pdf = gen.GetPdfFromComprobante(comprobante, emp.Orientacion, factura.Factura.TipoDocumento, ref id,factura.Factura.Metodo);
                return pdf;
            }
            catch (FaultException fe)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                if (ex.InnerException != null)
                    Logger.Error(ex.InnerException);
                return null;
            }

        }
          

        /*

        public static byte[] GeneraPreview(NtLinkFactura factura)
        {
            try
            {
                empresa emp = factura.Emisor;
                clientes cliente = factura.Receptor;
                if (string.IsNullOrEmpty(cliente.RFC))
                {
                    throw new ApplicationException("El rfc es erróneo");
                }
                string path = Path.Combine(ConfigurationManager.AppSettings["Resources"], emp.RFC, "Certs");
                X509Certificate2 cert = new X509Certificate2(Path.Combine(path, "csd.cer"));
                string rutaLlave = Path.Combine(path, "csd.key");

                var comprobante = GetComprobante(factura, cliente, emp,null);
                GeneradorCfdi gen = new GeneradorCfdi();
                gen.GenerarCfdPreview(comprobante, cert, rutaLlave, emp.PassKey);
                string ruta = Path.Combine(ConfigurationManager.AppSettings["Salida"], emp.RFC);
                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                long id = 0;

                //comprobante.CantidadLetra = CantidadLetra.Enletras(comprobante.total.ToString(), comprobante.Moneda);
                byte[] pdf = gen.GetPdfFromComprobante(comprobante, emp.Orientacion, factura.Factura.TipoDocumento, ref id,factura.Factura.Metodo);
                return pdf;
            }
            catch (FaultException fe)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                if (ex.InnerException != null)
                    Logger.Error(ex.InnerException);
                return null;
            }

        }
        */
        //---------------------------------------------------------------------------------



        private bool ValidaFolio(string folio, int idEmpresa)
        {
            if (string.IsNullOrEmpty(folio))
                throw new ApplicationException("El folio de la factura no puede ir vacío");
            return true;

        }


         public static byte[] GetAcuseCancelacion(string report, Int64 idVenta, string estatus, string estatusCancela, string EstatusCancelacion)
       //  public static byte[] GetAcuseCancelacion(string report, Int64 idVenta)
        {
            try
            {
                Logger.Debug(report + "->" + idVenta);
                ReportExecutionService rs = new ReportExecutionService();
                string userName = ConfigurationManager.AppSettings["RSUserName"];
                string password = ConfigurationManager.AppSettings["RSPass"];
                string url = ConfigurationManager.AppSettings["RSUrlExec"];
                rs.Credentials = new NetworkCredential(userName, password);
                rs.Url = url;
                string reportPath = report;//"/ReportesNtLink/Pdf";
                string format = "Pdf";
                string devInfo = @"<DeviceInfo> <OutputFormat>PDF</OutputFormat> </DeviceInfo>";
                 ParameterValue[] parameters = new ParameterValue[4];

                parameters[0] = new ParameterValue();
                parameters[0].Name = "idVenta";
                parameters[0].Value = idVenta.ToString();
                parameters[1] = new ParameterValue();
                parameters[1].Name = "Estatus";
                parameters[1].Value = estatus;
                parameters[2] = new ParameterValue();
                parameters[2].Name = "EstatusCancela";
                parameters[2].Value = estatusCancela;
                parameters[3] = new ParameterValue();

                parameters[3].Name = "EstatusCancelacion";
                parameters[3].Value = EstatusCancelacion;

                ExecutionHeader execHeader = new ExecutionHeader();
                rs.Timeout = 300000;
                rs.ExecutionHeaderValue = execHeader;
                rs.LoadReport(reportPath, null);
                rs.SetExecutionParameters(parameters, "en-US");


                string mimeType;
                string encoding;
                string fileNameExtension;
                Warning[] warnings;
                string[] streamIDs;
                var res = rs.Render(format, devInfo, out fileNameExtension, out mimeType, out encoding, out warnings, out streamIDs);
                Logger.Debug(res.Length);
                return res;
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return null;
            }
        }
          
        /*
        public void CancelarRetencion(string uuid, string acuse)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    var fact = db.Retencion.FirstOrDefault(p => p.Uuid == uuid);
                    if (fact != null)
                    {
                         AcuseCancelacion ac = AcuseCancelacion.Parse(acuse);
                         if (!string.IsNullOrEmpty(ac.FechaCancelacion))
                         fact.FechaCancelacion =ac.FechaCancelacion;
                         fact.EstatusCancelacion = ac.Status;
                         fact.SelloCancelacion = ac.SelloSat; 
                         db.Retencion.ApplyCurrentValues(fact);
                         db.SaveChanges();

                       
                    }
                }
            }
            catch (FaultException fe)
            {
                throw;
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
            }
        }
        */
        public string CancelarFactura(string rfcEmisor, string folioFiscal, string expresion, string rfcReceptor, string motivo, string FolioSustituto)
        {
            try
            {

                var cliente = new ClienteNtLink.ClienteTimbradoNtlink();

               if( cliente.cancelarCfdi(folioFiscal, rfcEmisor,motivo,FolioSustituto))
              //  string respuesta = cliente.CancelaCfdi(folioFiscal, rfcEmisor, expresion, rfcReceptor);
                //if (respuesta.StartsWith("<?xml version=\"1.0\"?>"))
                {
               //   CancelarFacturaGuardar(folioFiscal, cliente.successMessage);
                    CancelarFacturaGuardarNuevo(folioFiscal, cliente.successMessage);

                    return "Comprobante Cancelado correctamente";
                }
                throw new FaultException("Error al cancelar el comprobante, " +cliente.errorMessage );


            }
            catch (FaultException fe)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return "Error al cancelar el comprobante";
            }
        }
        public string CancelarFacturaX(string RFCEmisor,string RFCReceptor,string PassKey ,string uuid, double total, string motivo, string folioSustitucion)
        {
            try
            {
                var cliente = new GAFFactura();
                  using (cliente as IDisposable)
                {
                    string path = Path.Combine(ConfigurationManager.AppSettings["Resources"], RFCEmisor, "Certs");
                    string rutaCert=  Path.Combine(path, "csd.cer");
                    string rutaLlave = Path.Combine(path, "csd.key");
                    String keyB64 = System.Convert.ToBase64String(System.IO.File.ReadAllBytes(rutaLlave));
                    String cerB64 = System.Convert.ToBase64String(System.IO.File.ReadAllBytes(rutaCert));

                    var clienteWS = new ClienteServiciosWEb.ClienteTimbradoXpress();
                    var respuesta= clienteWS.ClienteCancelarXPRESS(keyB64,cerB64,PassKey,uuid, RFCEmisor, RFCReceptor,total,motivo,folioSustitucion );
                    if (clienteWS.Codigo == "201"|| clienteWS.Codigo == "202")
                    {
                        CancelarFacturaGuardarNuevo(uuid, respuesta);
                        return "Comprobante Cancelado correctamente";
                    }
                    else
                        return clienteWS.Codigo + " - " + clienteWS.Mensaje;
                }
            }
            catch (FaultException fe)
            { throw;  }
            catch (Exception ex)
            {  Logger.Error(ex);
                return "Error al cancelar el comprobante: "+ex.Message;
            }
        }

        public bool GetConsultaEstatusCFDI(string uuid, string rfcEmisor, string rfcReceptor, string total, ref string Salida)
        {
            var clienteWS = new ClienteServiciosWEb.ClienteTimbradoXpress();
           var respuesta = clienteWS.ConsultaEstatusCFDI(uuid, rfcEmisor, rfcReceptor, total);
            if (clienteWS.Codigo == "S - Comprobante obtenido satisfactoriamente.")
            {
                Salida = clienteWS.Mensaje;
                return true;
            }
            else
            {
                Salida ="Error en el servicio de cancelacion:"+ respuesta;
                return false;
            }
        }

        public int CancelarActaulizaMontosFactura(int id,string uuid,decimal monto,int parcialidad)
        {

            using (var db = new GAFEntities())
            {
                var fact = db.facturas.FirstOrDefault(p => p.Uid == uuid);
                if (fact != null)
                {
                    if (fact.Parcialidad != null)
                    {
                        fact.Parcialidad = fact.Parcialidad - parcialidad;
                        fact.SaldoAnteriorPago = fact.SaldoAnteriorPago + monto;
                        db.facturas.ApplyCurrentValues(fact);
                        db.SaveChanges();

                        var relacionado = db.FacturaPagoRelacionados.FirstOrDefault(p => p.IdFactura == id);
                        if(relacionado!=null)
                        if (!string.IsNullOrEmpty(relacionado.Ids))
                        {
                            string[] ids = relacionado.Ids.Split('|');
                            if (ids.Count() < 2)
                                db.FacturaPagoRelacionados.DeleteObject(relacionado);
                            else
                            {
                                string cad = "";
                                foreach (var z in ids)
                                {
                                    if (string.IsNullOrEmpty(cad))
                                        cad = z;
                                    else
                                        cad = cad + "|" + z;
                                }
                                relacionado.Ids = cad;
                                db.FacturaPagoRelacionados.ApplyCurrentValues(relacionado);

                            }
                            db.SaveChanges();

                        }
                    }
                    else
                    {
                        var prefactuas = db.PreFactura.FirstOrDefault(p => p.IdFactura == fact.idVenta);
                        if (prefactuas != null)
                        {
                            if (prefactuas.Parcialidad != null)
                            {
                                prefactuas.Parcialidad = prefactuas.Parcialidad - parcialidad;
                                prefactuas.SaldoAnteriorPago = prefactuas.SaldoAnteriorPago + monto;
                                db.facturas.ApplyCurrentValues(fact);
                                db.SaveChanges();

                                var prefactura = db.PreComplementoPago.Where(c => c.idPreFactura == prefactuas.idPreFactura).FirstOrDefault();
                                if (prefactura != null)
                                {
                                    if (!string.IsNullOrEmpty(prefactura.Ids))
                                    {
                                        string[] ids = prefactura.Ids.Split('|');
                                        if (ids.Count() < 2)
                                            db.PreComplementoPago.DeleteObject(prefactura);
                                        else
                                        {                                            
                                                string cad = "";
                                                foreach (var z in ids)
                                                {
                                                    if (string.IsNullOrEmpty(cad))
                                                        cad = z;
                                                    else
                                                        cad = cad + "|" + z;
                                                }
                                                prefactura.Ids = cad;
                                                db.PreComplementoPago.ApplyCurrentValues(prefactura);

                                        }
                                        db.SaveChanges();
                                    }
                                }
                            }

                        }
                    
                    
                    }
                }
            }
            return 1;
        }

        public void CancelarFacturaGuardar(string uuid, string acuse)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    var fact = db.facturas.FirstOrDefault(p => p.Uid == uuid);
                    if (fact != null)
                    {
                        AcuseCancelacion ac = AcuseCancelacion.Parse(acuse);
                        fact.Observaciones = acuse;
                        fact.Cancelado = 1;
                        fact.EstatusCancelacion = ac.Status;
                        fact.FechaCancelacion = ac.FechaCancelacion;
                        fact.SelloCancelacion = ac.SelloSat;
                        db.facturas.ApplyCurrentValues(fact);
                        db.SaveChanges();
                    }
                }
            }
            catch (FaultException fe)
            {
                throw;
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
            }
        }
        public void CancelarFacturaGuardarNuevo(string uuid, string acuse)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    var fact = db.facturas.FirstOrDefault(p => p.Uid == uuid);
                    if (fact != null)
                    {
                        fact.Observaciones = acuse;
                        fact.Cancelado = 2;  //1:cancelado, 0:no cancelado, 2:precancelado
                        fact.EstatusCancelacion = "GT05";  
                        fact.FechaCancelacion = DateTime.Now.ToString();
                        fact.SelloCancelacion = "";
                        db.facturas.ApplyCurrentValues(fact);
                        db.SaveChanges();
                    }
                }
            }
            catch (FaultException fe)
            {
                throw;
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
            }
        }

        public void ConsultaEstatusCFDIServicioSAT(string uuid, string RFCEmisor, string RFCReceptor, string total)
        {
            var clienteWS = new ClienteServiciosWEb.ClienteTimbradoXpress();
            string  resultado = clienteWS.ConsultaEstatusCFDI(uuid, RFCEmisor, RFCReceptor, total);

            if (clienteWS.Codigo == "S - Comprobante obtenido satisfactoriamente.")
            {
                CancelarFacturaEstatus(uuid, resultado, clienteWS.Mensaje);
            }
            else
            {
                CancelarFacturaEstatus(uuid, resultado);
                Logger.Error("Error con el uuid: " + uuid + " :" + resultado);
            }
        }
        private void CancelarFacturaEstatus(string uuid, string cancelado, string acuse)
        {
            try
            {
                string NoCancelable="";
                string EstatusCancelación = "";
                string[] status = acuse.Split('|');
                if (status.Count() > 2)
                {
                    EstatusCancelación = status[2];
                    NoCancelable = status[1];
                }

                using (var db = new GAFEntities())
                {
                    var fact = db.facturas.FirstOrDefault(p => p.Uid == uuid);
                    if (fact != null)
                    {
                        fact.Observaciones = acuse;
                        if (cancelado.ToUpper() == "Cancelado".ToUpper() && (EstatusCancelación.ToUpper() == "Cancelado sin aceptación".ToUpper() ||
                            EstatusCancelación.ToUpper() == "Cancelado con aceptación".ToUpper() ||
                                   EstatusCancelación.ToUpper() == "Plazo vencido".ToUpper()))
                            fact.Cancelado = 1;  //1:cancelado, 0:no cancelado, 2:precancelado,3:no cANCELADO,4:No Cancelable
                        if (EstatusCancelación.ToUpper() == "Solicitud Rechazada".ToUpper())
                            fact.Cancelado = 3;
                        if (NoCancelable.ToUpper() == "No cancelable".ToUpper())
                            fact.Cancelado = 4;

                        db.facturas.ApplyCurrentValues(fact);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
            }
        }
        private void CancelarFacturaEstatus(string uuid, string cancelado)
        {
            try
            {

                using (var db = new GAFEntities())
                {
                    var fact = db.facturas.FirstOrDefault(p => p.Uid == uuid);
                    if (fact != null)
                    {
                        fact.Observaciones = cancelado;
                        db.facturas.ApplyCurrentValues(fact);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
            }
        }

        /*
        public NtLinkFactura(int idFactura)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    if (idFactura == 0)
                    {
                        this.Factura = new facturas();
                        this.Detalles = new List<facturasdetalle>();
                    }
                    else
                    {
                        this.Factura = db.facturas.Where(p => p.idVenta == idFactura).FirstOrDefault();
                        if (Factura == null)
                        {
                            throw new ApplicationException("La factura " + idFactura.ToString() + " No se encontró");
                        }
                        this.Detalles = db.facturasdetalle.Where(p => p.idVenta == idFactura).ToList();
                    }
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
            }

        }
        */
        public bool GuardarFacturaXML(Comprobante c, System.Guid usuario, string uuid)
        {
            GAFClientes nlc = new GAFClientes();
            clientes cliente = nlc.GetCliente(c.Receptor.Rfc);
            GAFEmpresa emp = new GAFEmpresa();
            empresa empresa = emp.GetRfc(c.Emisor.Rfc);
        
            facturas f = new facturas();
            f.Fecha =Convert.ToDateTime( c.Fecha);
            f.idcliente = cliente.idCliente;
            f.Importe = c.Total;
            f.Descuento = "0";
            f.nProducto = 1;
            f.captura = DateTime.Now;
            f.Folio = c.Folio;
            f.Cancelado = 0;
            f.Moneda = "Peso Mexicano";
            f.Uid = uuid;
            f.Total = c.Total;
            f.IdEmpresa = empresa.IdEmpresa;
            f.SubTotal = c.SubTotal;
            f.StatusPago = false;
            f.IdPdf = 0;
            f.TipoDocumentoStr = "Ingreso";
            f.StatusFactura = "Pendiente";
            f.FormaPago = "Por definir";
            f.PreFolio = "FR-" + DateTime.Now.Month + "-" + DateTime.Now.Year +"-"+ c.Folio;
            f.Usuario = usuario;

            var s = f.Total - f.SubTotal;
            if (s != 0)
                f.IVA = s;
            var z= GuardaFactura(f);
            if (z == true)
            {
                  Factura F = new Factura();
                      var fa= F.GetByComprobante(uuid);

                PreFactura p = new PreFactura();
                p.Version = "3.3";
                p.Serie = c.Serie;
                p.Folio = c.Folio;
                p.Fecha = f.Fecha;
                p.FormaPago = c.FormaPago;
                p.SubTotal = c.SubTotal;
                p.Descuento = 0;
                p.Moneda = c.Moneda;
                p.Total = c.Total;
                p.TipoDeComprobante = "Ingreso";
                p.MetodoPago = c.MetodoPago;
                p.LugarExpedicion = c.LugarExpedicion;
                p.idEmpresa = empresa.IdEmpresa;
                p.idCliente = cliente.idCliente;
                p.IDUsuario = usuario;
                p.FechaUltimaModificacion = DateTime.Now;
                p.Estatus = 0;
                p.UsoCFDI = c.Receptor.UsoCFDI;
                p.EstatusVista = 0;
                p.Timbrado = true;
                p.CFDI = "G";
                p.IVA = f.IVA;
                p.PreFolio = f.PreFolio;
                p.IdFactura = fa.idVenta;
                var a=GuardaPreFactura(p);
                return a;
            }
            return false;

        }
        public bool GuardaFactura(facturas F)
        {
            try
            {
                 using (var db = new GAFEntities())
                {
                   db.facturas.AddObject(F);
                    db.SaveChanges();
                    return true;
                 }
            }
        catch (Exception ee)
        {
            Logger.Error(ee);
            if (ee.InnerException != null)
                Logger.Error(ee.InnerException);
            return false;
        }

        }
        public bool GuardaPreFactura(PreFactura F)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    db.PreFactura.AddObject(F);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return false;
            }

        }

        public bool Save(long idPrefactura)
        {
            try
            {

                facturas F = LLenadoFactura(_factura);
                
                List<Conceptos> C = LLenadoConcepto(_factura);

                using (var db = new GAFEntities())
                {
                    //empresa em = db.empresa.Where(p => p.IdEmpresa == _factura.IdEmpresa).FirstOrDefault();
                    //em.Folio = _factura.Folio;
                    //db.SaveChanges();

                    if (F.idVenta == 0 /*&& ValidaFolio(F.Folio, F.IdEmpresa.Value)*/)
                    {
                        db.facturas.AddObject(F);
                       // var ee = db.Sistemas.FirstOrDefault(p => p.IdSistema == Emisor.idSistema);
                       // ee.SaldoEmision = ee.SaldoEmision - 1;
                       // ee.ConsumoEmision = ee.ConsumoEmision + 1;
                        db.SaveChanges();
                        foreach (var c in C)
                        {
                            c.idFactura = F.idVenta;
                            db.Conceptos.AddObject(c);
                        }
                        db.SaveChanges();
                    }
                    else
                    {
                        List<Conceptos> con = db.Conceptos.Where(p => p.idFactura == idPrefactura).ToList();
                        foreach (var c in con)
                        {
                           db.Conceptos.DeleteObject(c);
                        }
                        db.facturas.ApplyCurrentValues(F);
                        db.SaveChanges();

                        foreach (var c in C)
                        {
                            c.idFactura = F.idVenta;
                            db.Conceptos.AddObject(c);
                        }
                        db.SaveChanges();
                    }
                    if (idPrefactura != 0)
                    {
                        PreFactura pre = db.PreFactura.Where(p => p.idPreFactura == idPrefactura).FirstOrDefault();
                        //  em.Estatus = 4;
                        pre.Timbrado = true;
                        pre.IdFactura = F.idVenta;
                        db.SaveChanges();
                        //0 -pendiente
                        //1- pagado
                        //2.-pendiente por pagar
                        //3- cancelado
                        //4-timbrado
                        
                    }
                    return true;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return false;
            }
        }
        
        public  void Pagar(int idVenta, DateTime fechaPago, string referenciaPago)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    facturas fac = db.facturas.Single(l => l.idVenta == idVenta);
                    fac.FechaPago = fechaPago;
                    fac.ReferenciaPago = referenciaPago;
                    fac.StatusFactura = "Pagado";
                    db.facturas.ApplyCurrentValues(fac);
                      db.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                throw;
            }
        }
         
        //***********************************AREA DE COMPLEMENTOS**************************************************
      
        public static Comprobante ComplementoImpuestosLocales(Comprobante comprobante, ImpLocales ImpuestosLocales)
        {
                if (ImpuestosLocales.imp.Count() > 0)
                {

                    if (comprobante.Complemento == null)
                        comprobante.Complemento = new ComprobanteComplemento();
                      comprobante.Complemento.implocal = new ImpuestosLocales();


                    if (ImpuestosLocales.TotaldeRetenciones != null)
                     comprobante.Complemento.implocal.TotaldeRetenciones = decimal.Parse(ImpuestosLocales.TotaldeRetenciones, NumberStyles.Currency);
                    if (ImpuestosLocales.TotaldeTraslados != null)
                        comprobante.Complemento.implocal.TotaldeTraslados = decimal.Parse(ImpuestosLocales.TotaldeTraslados, NumberStyles.Currency);
                    //--------------------------------------------------------
                    if (ImpuestosLocales.imp != null)
                    {
                        List<ImpuestosLocalesTrasladosLocales> LITL = new List<ImpuestosLocalesTrasladosLocales>();
                        foreach (var imp in ImpuestosLocales.imp)
                        {
                            if (imp.ImpuestosLocales == "TrasladosLocales")
                            {
                                ImpuestosLocalesTrasladosLocales litl = new ImpuestosLocalesTrasladosLocales();
                                litl.ImpLocTrasladado = imp.ImpLoc;
                              //  litl.Importe = decimal.Parse(imp.Importe, NumberStyles.Currency);
                                litl.Importe = Decimal.Round(Convert.ToDecimal(imp.Importe.Replace("$", "")), (int)mone.Decimales);
                                //litl.TasadeTraslado = decimal.Parse(imp.Tasa, NumberStyles.Currency);
                                litl.TasadeTraslado = Decimal.Round(Convert.ToDecimal(imp.Tasa), (int)mone.Decimales);
                                
                                LITL.Add(litl);
                            }
                        }
                        if (LITL.Count > 0)
                            comprobante.Complemento.implocal.TrasladosLocales = LITL.ToArray();

                    }
                    //-------------------------------------------------------------
                    if (ImpuestosLocales.imp != null)
                    {
                        //comprobante.Complemento.imlocal.RetencionesLocales = new ImpuestosLocalesRetencionesLocales();
                        List<ImpuestosLocalesRetencionesLocales> LIL = new List<ImpuestosLocalesRetencionesLocales>();
                        foreach (var imp in ImpuestosLocales.imp)
                        {
                            if (imp.ImpuestosLocales == "RetencionesLocales")
                            {
                                ImpuestosLocalesRetencionesLocales lil = new ImpuestosLocalesRetencionesLocales();
                                lil.ImpLocRetenido = imp.ImpLoc;
                                //lil.Importe = decimal.Parse(imp.Importe, NumberStyles.Currency);
                                lil.Importe = Decimal.Round(Convert.ToDecimal(imp.Importe.Replace("$","")), (int)mone.Decimales);
                               // lil.TasadeRetencion = decimal.Parse(imp.Tasa, NumberStyles.Currency);
                                lil.TasadeRetencion = Decimal.Round(Convert.ToDecimal(imp.Tasa), (int)mone.Decimales);
                              
                                LIL.Add(lil);
                            }
                        }
                        if (LIL.Count > 0)
                            comprobante.Complemento.implocal.RetencionesLocales = LIL.ToArray();
                    }
                }
                return comprobante;
        }

        
        public static Comprobante LLenaComplemento(Comprobante comprobante, facturaComplementos complementos)
        {
            if (complementos.pagos != null)
            {


                if (comprobante.Complemento == null)
                    comprobante.Complemento = new ComprobanteComplemento();
                comprobante.Complemento.Pag = new Contract.Complemento40.Pagos();
                comprobante.Complemento.Pag.Version = "2.0";
                //  comprobante.Complemento.Pag.Pago = new List<Complemento40.PagosPago>();
                decimal TotalRetencionesIVA = 0M;
                decimal TotalRetencionesISR = 0M;
                decimal TotalRetencionesIEPS = 0M;
                decimal TotalTrasladosBaseIVA16 = 0M;
                decimal TotalTrasladosImpuestoIVA16 = 0M;
                decimal TotalTrasladosBaseIVA8 = 0M;
                decimal TotalTrasladosImpuestoIVA8 = 0M;
                decimal TotalTrasladosBaseIVA0 = 0M;
                decimal TotalTrasladosImpuestoIVA0 = 0M;
                decimal TotalTrasladosBaseIVAExento = 0M;
                decimal MontoTotalPagos = 0M;


                List<Contract.Complemento40.PagosPago> PA = new List<Contract.Complemento40.PagosPago>();
                foreach (var p in complementos.pagos)
                {
                    Contract.Complemento40.PagosPago pa = new Contract.Complemento40.PagosPago();
                    CatalogosSAT.OperacionesCatalogos o13 = new CatalogosSAT.OperacionesCatalogos();
                    DateTime D = Convert.ToDateTime(p.fechaPago);
                    pa.FechaPago = D.ToString("s");
                    //if (!string.IsNullOrEmpty(p.formaDePagoP))
                    pa.FormaDePagoP = p.formaDePagoP;
                    CatalogosSAT.c_Moneda mone = o13.Consultar_Moneda(p.monedaP);
                    pa.MonedaP = p.monedaP;
                    if (!string.IsNullOrEmpty(p.tipoCambioP))
                    {   if (p.monedaP == "MXN")
                            pa.TipoCambioP = "1";
                       else
                        pa.TipoCambioP = p.tipoCambioP;
                        pa.TipoCambioPSpecified = true;
                    }
                    else
                        pa.TipoCambioPSpecified = false;
                    pa.Monto = numerodecimales(Convert.ToDecimal(p.monto), (int)mone.Decimales);//--
                    if (!string.IsNullOrEmpty(p.numOperacion))
                        pa.NumOperacion = p.numOperacion;
                    if (!string.IsNullOrEmpty(p.rfcEmisorCtaOrd))
                        pa.RfcEmisorCtaOrd = p.rfcEmisorCtaOrd;
                    if (!string.IsNullOrEmpty(p.nomBancoOrdExt))
                        pa.NomBancoOrdExt = p.nomBancoOrdExt;
                    if (!string.IsNullOrEmpty(p.ctaOrdenante))
                        pa.CtaOrdenante = p.ctaOrdenante;
                    if (!string.IsNullOrEmpty(p.rfcEmisorCtaBen))
                        pa.RfcEmisorCtaBen = p.rfcEmisorCtaBen;
                    if (!string.IsNullOrEmpty(p.ctaBeneficiario))
                        pa.CtaBeneficiario = p.ctaBeneficiario;
                    if (!string.IsNullOrEmpty(p.tipoCadPago))
                    {
                        pa.TipoCadPago = p.tipoCadPago;
                        pa.TipoCadPagoSpecified = true;
                    }
                    else
                        pa.TipoCadPagoSpecified = false;

                    if (!string.IsNullOrEmpty(p.certPago))
                        pa.CertPago = p.certPago;///


                    if (!string.IsNullOrEmpty(p.cadPago))
                        pa.CadPago = p.cadPago;
                    // DateTime D22 = D.AddHours(12);
                    // pa.FechaPago = D22.ToString("s");
                    //pa.Impuestos
                    if (!string.IsNullOrEmpty(p.selloPago))
                        pa.SelloPago = p.selloPago;


                    bool impuestos = false;
                    List<Contract.Complemento40.PagosPagoDoctoRelacionado> Doc = new List<Contract.Complemento40.PagosPagoDoctoRelacionado>();
                    if (p.DoctoRelacionado != null)
                    {
                        foreach (var d in p.DoctoRelacionado)
                        {
                          Contract.Complemento40.PagosPagoDoctoRelacionado doc = new Contract.Complemento40.PagosPagoDoctoRelacionado();
                            d.Impuestos=llenarImpuestosPago(d.Impuestos, Convert.ToDecimal(d.ImpPagado));//agregado para 40

                            doc.IdDocumento = d.IdDocumento;
                            if (!string.IsNullOrEmpty(d.Serie))
                                doc.Serie = d.Serie;
                            if (!string.IsNullOrEmpty(d.Folio))
                                doc.Folio = d.Folio;
                            doc.MonedaDR = d.MonedaDR;
                           // if (!string.IsNullOrEmpty(d.EquivalenciaDR))
                            {
                                doc.EquivalenciaDRSpecified = true;
                                doc.EquivalenciaDR = 1;
                            }
                           // else
                           //     doc.EquivalenciaDRSpecified = false;

                            doc.NumParcialidad = d.NumParcialidad;
                            doc.ImpSaldoAnt = Convert.ToDecimal(d.ImpSaldoAnt);
                            doc.ImpPagado = Convert.ToDecimal(d.ImpPagado);
                            doc.ImpSaldoInsoluto = Convert.ToDecimal(d.ImpSaldoInsoluto);
                            doc.ObjetoImpDR = "02";

                            if (d.Impuestos != null && d.Impuestos.Count > 0)
                            {
                                doc.ImpuestosDR = new Contract.Complemento40.PagosPagoDoctoRelacionadoImpuestosDR();
                                doc.ImpuestosDR = llenarPagosDocumentosImpuestos(d.Impuestos, (int)mone.Decimales);
                                impuestos = true;
                            }

                            Doc.Add(doc);
                        }

                        pa.DoctoRelacionado = Doc.ToArray();
                    }

                    decimal tipocambio = 1M;
                    if (pa.TipoCambioPSpecified == true)
                        tipocambio =Convert.ToDecimal(pa.TipoCambioP);

                    if (impuestos == true)
                    {
                        pa.ImpuestosP = llenarPagosImpuestosP(p.DoctoRelacionado,(int)mone.Decimales);

                        if (pa.ImpuestosP != null)
                        {
                            if (pa.ImpuestosP.RetencionesP != null)
                                llenarTotalesPRetenciones(pa.ImpuestosP, ref TotalRetencionesIVA, ref TotalRetencionesISR, ref TotalRetencionesIEPS, tipocambio);
                            if (pa.ImpuestosP.TrasladosP != null)
                                llenarTotalesPRetenciones(pa.ImpuestosP, ref TotalTrasladosBaseIVA16, ref TotalTrasladosImpuestoIVA16,
                                ref TotalTrasladosBaseIVA8, ref TotalTrasladosImpuestoIVA8, ref TotalTrasladosBaseIVA0,
                              ref TotalTrasladosImpuestoIVA0, ref TotalTrasladosBaseIVAExento, tipocambio);
                        }
                    }
                    MontoTotalPagos = MontoTotalPagos + (Convert.ToDecimal(pa.Monto) * tipocambio);

                    PA.Add(pa);
                }
                comprobante.Complemento.Pag.Pago = PA.ToArray();
                comprobante.Complemento.Pag.Totales = new Contract.Complemento40.PagosTotales();
                comprobante.Complemento.Pag.Totales.MontoTotalPagos = numerodecimales(MontoTotalPagos, 2);


                if (TotalRetencionesIVA > 0)
                {
                    comprobante.Complemento.Pag.Totales.TotalRetencionesIVASpecified = true;
                    comprobante.Complemento.Pag.Totales.TotalRetencionesIVA = numerodecimales(TotalRetencionesIVA, 2);
                }
                else
                    comprobante.Complemento.Pag.Totales.TotalRetencionesIVASpecified = false;
                if (TotalRetencionesISR > 0)
                {
                    comprobante.Complemento.Pag.Totales.TotalRetencionesISRSpecified = true;
                    comprobante.Complemento.Pag.Totales.TotalRetencionesISR = numerodecimales(TotalRetencionesISR, 2);
                }
                else
                    comprobante.Complemento.Pag.Totales.TotalRetencionesISRSpecified = false;
                if (TotalRetencionesIEPS > 0)
                {
                    comprobante.Complemento.Pag.Totales.TotalRetencionesIEPSSpecified = true;
                    comprobante.Complemento.Pag.Totales.TotalRetencionesIEPS = numerodecimales(TotalRetencionesIEPS, 2);
                }
                else
                    comprobante.Complemento.Pag.Totales.TotalRetencionesIEPSSpecified = false;
                if (TotalTrasladosBaseIVA16 > 0)
                {
                    comprobante.Complemento.Pag.Totales.TotalTrasladosBaseIVA16Specified = true;
                    comprobante.Complemento.Pag.Totales.TotalTrasladosBaseIVA16 = numerodecimales(TotalTrasladosBaseIVA16, 2);
                }
                else
                    comprobante.Complemento.Pag.Totales.TotalTrasladosBaseIVA16Specified = false;
                if (TotalTrasladosImpuestoIVA16 > 0)
                {
                    comprobante.Complemento.Pag.Totales.TotalTrasladosImpuestoIVA16Specified = true;
                    comprobante.Complemento.Pag.Totales.TotalTrasladosImpuestoIVA16 = numerodecimales(TotalTrasladosImpuestoIVA16, 2);
                }
                else
                    comprobante.Complemento.Pag.Totales.TotalTrasladosImpuestoIVA16Specified = false;
                if (TotalTrasladosBaseIVA8 > 0)
                {
                    comprobante.Complemento.Pag.Totales.TotalTrasladosBaseIVA8Specified = true;
                    comprobante.Complemento.Pag.Totales.TotalTrasladosBaseIVA8 = numerodecimales(TotalTrasladosBaseIVA8, 2);
                }
                else
                    comprobante.Complemento.Pag.Totales.TotalTrasladosBaseIVA8Specified = false;
                if (TotalTrasladosImpuestoIVA8 > 0)
                {
                    comprobante.Complemento.Pag.Totales.TotalTrasladosImpuestoIVA8Specified = true;
                    comprobante.Complemento.Pag.Totales.TotalTrasladosImpuestoIVA8 = numerodecimales(TotalTrasladosImpuestoIVA8, 2);
                }
                else
                    comprobante.Complemento.Pag.Totales.TotalTrasladosImpuestoIVA8Specified = false;

                if (TotalTrasladosBaseIVA0 > 0)
                {
                    comprobante.Complemento.Pag.Totales.TotalTrasladosBaseIVA0Specified = true;
                    comprobante.Complemento.Pag.Totales.TotalTrasladosBaseIVA0 = numerodecimales(TotalTrasladosBaseIVA0, 2);
                }
                else
                    comprobante.Complemento.Pag.Totales.TotalTrasladosBaseIVA0Specified = false;
                if (TotalTrasladosImpuestoIVA0 > 0)
                {
                    comprobante.Complemento.Pag.Totales.TotalTrasladosImpuestoIVA0Specified = true;
                    comprobante.Complemento.Pag.Totales.TotalTrasladosImpuestoIVA0 = numerodecimales(TotalTrasladosImpuestoIVA0, 2);
                }
                else
                    comprobante.Complemento.Pag.Totales.TotalTrasladosImpuestoIVA0Specified = false;
                if (TotalTrasladosBaseIVAExento > 0)
                {
                    comprobante.Complemento.Pag.Totales.TotalTrasladosBaseIVAExentoSpecified = true;
                    comprobante.Complemento.Pag.Totales.TotalTrasladosBaseIVAExento = numerodecimales(TotalTrasladosBaseIVAExento, 2);
                }
                else
                    comprobante.Complemento.Pag.Totales.TotalTrasladosBaseIVAExentoSpecified = false;

            }
            //-------------------------------
            if (complementos.ine != null)
            {
                if (comprobante.Complemento == null)
                    comprobante.Complemento = new ComprobanteComplemento();
                comprobante.Complemento.ine = new Contract.Complemento.INE();
                    if (!string.IsNullOrEmpty(complementos.ine.IdContabilidad))
                    {
                        comprobante.Complemento.ine.IdContabilidadSpecified = true;
                        comprobante.Complemento.ine.IdContabilidad = Convert.ToInt16(complementos.ine.IdContabilidad);
                    }
                    else
                    comprobante.Complemento.ine.IdContabilidadSpecified = false;
                    if (!string.IsNullOrEmpty(complementos.ine.TipoComite))
                    {
                        comprobante.Complemento.ine.TipoComiteSpecified = true;
                        comprobante.Complemento.ine.TipoComite = complementos.ine.TipoComite;
                    }
                    else
                 
                        comprobante.Complemento.ine.TipoComiteSpecified = false;
                      comprobante.Complemento.ine.TipoProceso = complementos.ine.TipoProceso;

                      List<Contract.Complemento.INEEntidad> Entidad = new List<Contract.Complemento.INEEntidad>();
                      foreach (var i in complementos.ine.Entidad)
                    {
                        Contract.Complemento.INEEntidad E = new Contract.Complemento.INEEntidad();
                        if (!string.IsNullOrEmpty(i.Ambito))
                        {
                            E.AmbitoSpecified = true;
                            E.Ambito = i.Ambito;
                        }
                        else
                            E.AmbitoSpecified = false;
                        E.ClaveEntidad = i.ClaveEntidad;
                          if(i.IdContabilidad!=null)
                              if(i.IdContabilidad.Count()>0)
                          {
                              List<Contract.Complemento.INEEntidadContabilidad> Contabilidad = new List<Contract.Complemento.INEEntidadContabilidad>();
                          foreach (var s in i.IdContabilidad)
                          {
                              Contract.Complemento.INEEntidadContabilidad c = new Contract.Complemento.INEEntidadContabilidad();
                           c.IdContabilidad = Convert .ToInt16(s);
                           Contabilidad.Add(c);
                          }
                          E.Contabilidad = Contabilidad.ToArray();
                          }
                          Entidad.Add(E);
                     }
                      if (Entidad != null)
                          comprobante.Complemento.ine.Entidad = Entidad.ToArray();

             }
            return comprobante;
        }
        //***********************************************************************************
        private static List<facturasdetalleRT> llenarImpuestosPago(List<facturasdetalleRT> p,decimal ImpPagado)
        {
            p = new List<facturasdetalleRT>();
            facturasdetalleRT tra=new facturasdetalleRT();
            tra.Base= Convert.ToDecimal(numerodecimales(ImpPagado / 1.16M,6));
            tra.Importe=Convert.ToDecimal(numerodecimales((ImpPagado / 1.16M) * 0.160000M,6));
            tra.TasaOCuota= "0.160000";
            tra.TipoFactor = "Tasa";
            tra.TipoImpuesto = "Traslados";
            tra.Impuesto = "IVA";
            p.Add(tra);
            return p;
        }

        public static string GetNextFolio(int idEmpresa)
        {
            try
            {
                using (var db = new GAFEntities())
                {

                    var empre = db.empresa.Where(p => p.IdEmpresa == idEmpresa).FirstOrDefault();//nuevo

                    string folio = "0";//nuevo
                    if (empre != null) //nuevo
                    {  //nuevo
                        if (!string.IsNullOrEmpty(empre.Folio))//nuevo
                            folio = empre.Folio;//nuevo

                    } //nuevo            
                    
                    Int64 i = 0;//nuevo
                    i = Int64.Parse(folio);//nuevo

                    i++;
                    return i.ToString().PadLeft(6, '0');

                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }
        }

        public byte[] FacturaPreview33(DatosPrefactura fact,  facturaComplementos complementosF)
        
        {
            Logger.Info("FacturaPreview");
            try
            {

            GAFClientes nlc = new GAFClientes();
            clientes cliente = nlc.GetCliente(fact.idcliente);
            GAFEmpresa emp = new GAFEmpresa();
            empresa empresa = emp.GetById(fact.IdEmpresa);
           // GAFFactura fac = new GAFFactura(0);
            GAFFactura fac = new GAFFactura();
           
            fact.Regimen = empresa.RegimenFiscal;
            fac.Emisor = empresa;
            fac.Receptor = cliente;
            fac.Detalles = fact.Detalles;
            fac.Factura = fact;
            // fact.ConceptosAduanera = conceptosAduana;
                   
        
                var cfd = GAFFactura.GeneraPreviewRs(fac, complementosF);
                return cfd;
            }
            catch (FaultException fe)
            {
                throw;
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                return null;
            }
        }
        private static Contract.Complemento40.PagosPagoDoctoRelacionadoImpuestosDR llenarPagosDocumentosImpuestos(List<facturasdetalleRT> Imp,int mon)
        {
            List<Contract.Complemento40.PagosPagoDoctoRelacionadoImpuestosDRRetencionDR> RT = new List<Contract.Complemento40.PagosPagoDoctoRelacionadoImpuestosDRRetencionDR>();
            List<Contract.Complemento40.PagosPagoDoctoRelacionadoImpuestosDRTrasladoDR> TR = new List<Contract.Complemento40.PagosPagoDoctoRelacionadoImpuestosDRTrasladoDR>();

            Contract.Complemento40.PagosPagoDoctoRelacionadoImpuestosDR DR = new Contract.Complemento40.PagosPagoDoctoRelacionadoImpuestosDR();
            foreach (var im in Imp)
            {
                if (im.Impuesto == "ISR")
                    im.Impuesto = "001";
                if (im.Impuesto == "IVA")
                    im.Impuesto = "002";
                if (im.Impuesto == "IEPS")
                    im.Impuesto = "003";
                if (im.TipoImpuesto == "Retenciones")
                {

                    Contract.Complemento40.PagosPagoDoctoRelacionadoImpuestosDRRetencionDR r = new Contract.Complemento40.PagosPagoDoctoRelacionadoImpuestosDRRetencionDR();
                    r.BaseDR = numerodecimales(im.Base, 6);//
                    r.ImporteDR = numerodecimales((decimal)im.Importe, 6);
                    r.ImpuestoDR = im.Impuesto;
                    r.TasaOCuotaDR = im.TasaOCuota;
                    r.TipoFactorDR = im.TipoFactor;
                    RT.Add(r);
                }
                if (im.TipoImpuesto == "Traslados")
                {
                    Contract.Complemento40.PagosPagoDoctoRelacionadoImpuestosDRTrasladoDR r = new Contract.Complemento40.PagosPagoDoctoRelacionadoImpuestosDRTrasladoDR();
                     r.BaseDR = numerodecimales(im.Base, 6);
                    if (im.Importe != null)
                    {
                        r.ImporteDR = numerodecimales((decimal)im.Importe, 6);
                        r.ImporteDRSpecified = true;
                    }
                    else
                        r.ImporteDRSpecified = false;
                    r.ImpuestoDR = im.Impuesto;
                    if (!string.IsNullOrEmpty(im.TasaOCuota))
                    {
                        r.TasaOCuotaDR = im.TasaOCuota;
                        r.TasaOCuotaDRSpecified = true;
                    }
                    else
                        r.TasaOCuotaDRSpecified = false;
                    r.TipoFactorDR = im.TipoFactor;
                    TR.Add(r);

                }
            }
            if (RT.Count > 0)
                DR.RetencionesDR = RT.ToArray();
            if (TR.Count > 0)
                DR.TrasladosDR = TR.ToArray();


            // DR.RetencionesDR
            //  DR.TrasladosDR

            return DR;
        }

        private static Contract.Complemento40.PagosPagoImpuestosP llenarPagosImpuestosP(List<PagoDoctoRelacionado> doC,int mon)
        {
            Contract.Complemento40.PagosPagoImpuestosP Im = new Contract.Complemento40.PagosPagoImpuestosP();
            List<Contract.Complemento40.PagosPagoImpuestosPTrasladoP> T = new List<Contract.Complemento40.PagosPagoImpuestosPTrasladoP>();
            List<Contract.Complemento40.PagosPagoImpuestosPRetencionP> R = new List<Contract.Complemento40.PagosPagoImpuestosPRetencionP>();

            decimal IR001 = 0M;
            decimal IR002 = 0M;
            decimal IR003 = 0M;
            bool x = false;


            foreach (var doc in doC)
            {
                if (doc.Impuestos != null)
                    foreach (var im in doc.Impuestos)
                    {
                        if (im.TipoImpuesto == "Retenciones")
                        {
                            if (im.Impuesto == "001")
                                IR001 = IR001 + (decimal)im.Importe;
                            if (im.Impuesto == "002")
                                IR002 = IR002 + (decimal)im.Importe;
                            if (im.Impuesto == "003")
                                IR003 = IR003 + (decimal)im.Importe;
                        }
                        if (im.TipoImpuesto == "Traslados")
                        {
                            foreach (var dt in T)
                            {
                                if (dt.ImpuestoP == im.Impuesto && dt.TasaOCuotaP == im.TasaOCuota && dt.TipoFactorP == im.TipoFactor)
                                {
                                    if (im.Importe != null)
                                    {                                     
                                        dt.ImporteP = (Convert.ToDecimal(dt.ImporteP) + (Convert.ToDecimal(numerodecimales((decimal)im.Importe,6)))).ToString();
                                        dt.BaseP = (Convert.ToDecimal(dt.BaseP) + (Convert.ToDecimal(numerodecimales(im.Base,6)))).ToString();//
                                    }


                                    x = true;
                                }
                            }
                            if (x == false)
                            {
                                Contract.Complemento40.PagosPagoImpuestosPTrasladoP t = new Contract.Complemento40.PagosPagoImpuestosPTrasladoP();
                               // t.BaseP = im.Base.ToString();
                                t.BaseP = numerodecimales((decimal)im.Base, 6);

                                if (im.Importe != null)
                                {
                                    t.ImportePSpecified = true;
                                    t.ImporteP = numerodecimales((decimal)im.Importe , 6);
                                    //t.ImporteP = im.Importe.ToString();
                                }
                                else
                                    t.ImportePSpecified = false;
                                t.ImpuestoP = im.Impuesto;
                                if (!string.IsNullOrEmpty(im.TasaOCuota))
                                {
                                    t.TasaOCuotaPSpecified = true;
                                    t.TasaOCuotaP = im.TasaOCuota;
                                }
                                else
                                    t.TasaOCuotaPSpecified = false;
                                t.TipoFactorP = im.TipoFactor;
                                T.Add(t);
                            }

                        }
                    }
            }//--------------------------recorrer los datos----------------
            //---retenciones
            if (IR001 != 0)
            {
                Contract.Complemento40.PagosPagoImpuestosPRetencionP r1 = new Contract.Complemento40.PagosPagoImpuestosPRetencionP();
                r1.ImpuestoP = "001";
                r1.ImporteP = IR001;
                R.Add(r1);

            }
            if (IR002 != 0)
            {
                Contract.Complemento40.PagosPagoImpuestosPRetencionP r2 = new Contract.Complemento40.PagosPagoImpuestosPRetencionP();
                r2.ImpuestoP = "002";
                r2.ImporteP = IR002;
                R.Add(r2);
            }
            if (IR003 != 0)
            {
                Contract.Complemento40.PagosPagoImpuestosPRetencionP r3 = new Contract.Complemento40.PagosPagoImpuestosPRetencionP();
                r3.ImpuestoP = "003";
                r3.ImporteP = IR003;
                R.Add(r3);
            }
            if (R.Count > 0)
                Im.RetencionesP = R.ToArray();
            if (T.Count > 0)
                Im.TrasladosP = T.ToArray();
            //-------------------------------------------
            return Im;
        }
        private static void llenarTotalesPRetenciones(Contract.Complemento40.PagosPagoImpuestosP pa, ref decimal TotalTrasladosBaseIVA16,
       ref decimal TotalTrasladosImpuestoIVA16, ref decimal TotalTrasladosBaseIVA8, ref decimal TotalTrasladosImpuestoIVA8,
       ref decimal TotalTrasladosBaseIVA0, ref decimal TotalTrasladosImpuestoIVA0, ref decimal TotalTrasladosBaseIVAExento, decimal TipoCambioP)
        {
            Contract.Complemento40.Pagos P = new Contract.Complemento40.Pagos();
            foreach (var im in pa.TrasladosP)
            {
                if (im.TasaOCuotaPSpecified == true && im.ImportePSpecified == true)
                    if (im.ImpuestoP == "002" && im.TipoFactorP == "Tasa" && Convert.ToDecimal(im.TasaOCuotaP) == Convert.ToDecimal("0.160000"))
                    {
                        TotalTrasladosBaseIVA16 = TotalTrasladosBaseIVA16 + (Convert.ToDecimal(im.BaseP) * TipoCambioP);
                        TotalTrasladosImpuestoIVA16 = TotalTrasladosImpuestoIVA16 + (Convert.ToDecimal(im.ImporteP) * TipoCambioP);
                    }

                if (im.TasaOCuotaPSpecified == true && im.ImportePSpecified == true)
                    if (im.ImpuestoP == "002" && im.TipoFactorP == "Tasa" && Convert.ToDecimal(im.TasaOCuotaP) == Convert.ToDecimal("0.080000"))
                    {
                        TotalTrasladosBaseIVA8 = TotalTrasladosBaseIVA8 + (Convert.ToDecimal(im.BaseP) * TipoCambioP);
                        TotalTrasladosImpuestoIVA8 = TotalTrasladosImpuestoIVA8 + (Convert.ToDecimal(im.ImporteP) * TipoCambioP);
                    }

                if (im.TasaOCuotaPSpecified == true && im.ImportePSpecified == true)
                    if (im.ImpuestoP == "002" && im.TipoFactorP == "Tasa" && Convert.ToDecimal(im.TasaOCuotaP) == Convert.ToDecimal("0.000000"))
                    {
                        TotalTrasladosBaseIVA0 = TotalTrasladosBaseIVA0 + (Convert.ToDecimal(im.BaseP) * TipoCambioP);
                        TotalTrasladosImpuestoIVA0 = TotalTrasladosImpuestoIVA0 + (Convert.ToDecimal(im.ImporteP) * TipoCambioP);
                    }
                if (im.ImpuestoP == "002" && im.TipoFactorP == "Exento")
                {
                    TotalTrasladosBaseIVAExento = TotalTrasladosBaseIVAExento + (Convert.ToDecimal(im.BaseP) * TipoCambioP);
                }


            }


        }

        private static void llenarTotalesPRetenciones(Contract.Complemento40.PagosPagoImpuestosP pa, ref decimal TotalRetencionesIVA, ref decimal TotalRetencionesISR, ref decimal TotalRetencionesIEPS, decimal TipoCambioP)
        {
            Contract.Complemento40.Pagos P = new Contract.Complemento40.Pagos();
            foreach (var im in pa.RetencionesP)
            {

                if (im.ImpuestoP == "002")
                    TotalRetencionesIVA = TotalRetencionesIVA + (im.ImporteP * TipoCambioP);
                if (im.ImpuestoP == "001")
                    TotalRetencionesISR = TotalRetencionesISR + (im.ImporteP * TipoCambioP);
                if (im.ImpuestoP == "003")
                    TotalRetencionesIEPS = TotalRetencionesIEPS + (im.ImporteP * TipoCambioP);

            }


        }

    }


}
