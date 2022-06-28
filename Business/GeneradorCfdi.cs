
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using MessagingToolkit.QRCode.Codec;

using log4net;
using log4net.Config;
using System.Web;

//using ParameterValue = ServicioLocal.Business.ReportExecution.ParameterValue;
//using Warning = ServicioLocal.Business.ReportExecution.Warning;
using Contract;
using ClienteNtLink;
using Business.ReportService;
using Business.ReportExecution;
using ClienteServiciosWEb;
using Business.CFDI40;
//using ServicioLocal.Business.InternalCertificador;


namespace  Business
{
    public class GeneradorCfdi
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GeneradorCfdi));


        public GeneradorCfdi()
        {
            XmlConfigurator.Configure();
        }

        private string GetXml(Comprobante p, string complemento)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Comprobante));
            using (MemoryStream memStream = new MemoryStream())
            {
                var sw = new StreamWriter(memStream, Encoding.UTF8);
                using (XmlWriter xmlWriter = XmlWriter.Create(sw, new XmlWriterSettings() { Indent = false, Encoding = Encoding.UTF8 }))
                {
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                    namespaces.Add("cfdi", "http://www.sat.gob.mx/cfd/4");

                    ser.Serialize(xmlWriter, p, namespaces);
                    string xml = Encoding.UTF8.GetString(memStream.GetBuffer());
                    xml = xml.Substring(xml.IndexOf(Convert.ToChar(60)));
                    xml = xml.Substring(0, (xml.LastIndexOf(Convert.ToChar(62)) + 1));
                    if (complemento != null)
                    {
                        XElement comprobante = XElement.Parse(xml);
                        var comp = comprobante.Elements(_ns + "Complemento").FirstOrDefault();
                        if (comp == null)
                        {
                            comprobante.Add(new XElement(_ns + "Complemento"));
                            comp = comprobante.Elements(_ns + "Complemento").FirstOrDefault();
                        }
                        comp.Add(XElement.Parse(complemento));
                        SidetecStringWriter swriter = new SidetecStringWriter(Encoding.UTF8);
                        comprobante.Save(swriter,SaveOptions.DisableFormatting);
                        return swriter.ToString();
                    }

                    return xml;
                }
            }
        }

       
        private byte[] GetQrCode(string cadena)
        {
            QrEncoder encoder = new QrEncoder();
            QrCode qrCode;
            if (!encoder.TryEncode(cadena, out qrCode))
            {
                throw new Exception("Error al generar codigo bidimensional: " + cadena);
            }
            GraphicsRenderer gRenderer = new GraphicsRenderer(new FixedModuleSize(2, QuietZoneModules.Two), Brushes.Black, Brushes.White);

            MemoryStream ms = new MemoryStream();
            gRenderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            return ms.GetBuffer();
        }

       
        /*
        private void PrintFields(Type tipo, string prefijo, object emisor, ComprobantePdf comp)
        {
            foreach (PropertyInfo pi in tipo.GetProperties())
            {

                if ((pi != null))
                    PrintFields(pi.PropertyType, prefijo + pi.Name, pi.GetValue(emisor, null), comp);
                else
                {
                    try
                    {
                        if (emisor != null && !pi.PropertyType.IsArray)
                        {
                            var valor = pi.GetValue(emisor, null) == null
                                   ? ""
                                   : pi.GetValue(emisor, null).ToString();
                            var property = comp.GetType().GetProperty(prefijo + "_" + pi.Name);
                            if (property != null)
                            {
                                property.SetValue(comp, valor, null);
                            }
                        }
                    }
                    catch (Exception eee)
                    {
                        Logger.Error(eee);
                    }


                }

            }
        }

       
        public Byte[] GetPdfFromComprobante(string rfcEmisor, TipoDocumento tipo,int idempresa,int idcliente, long idpdf)
        {
            try
            {
                ReportService.ReportingService2005 clt = new ReportingService2005();
                string userName = ConfigurationManager.AppSettings["RSUserName"];
                string password = ConfigurationManager.AppSettings["RSPass"];
                string url = ConfigurationManager.AppSettings["RSUrlService"];
                //clt.Credentials = System.Net.CredentialCache.DefaultCredentials;
                clt.Credentials = new NetworkCredential(userName, password);
                clt.Url = url;
                CatalogItem[] cats = clt.ListChildren("/", true);
                var rep = GetRutaPdf(tipo);
                var reporte = cats.FirstOrDefault(p => p.Name == rfcEmisor + "_" + rep);
                if (reporte == null)
                    reporte = cats.FirstOrDefault(p => p.Name == rep);

                if (reporte == null)
                {
                    throw new FaultException("No esta configurada la plantilla para este comprobante");
                }


                return GetReport(reporte.Path, idempresa, idcliente, idpdf, null);
            }
            catch (Exception ee)
            {
                //System.Diagnostics.Debugger.Launch();
                Logger.Error(ee);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                throw;
            }

        }

        */

        public Byte[] GetPdfFromComprobante(Comprobante comprobante, int orientacion, TipoDocumento tipo, ref long id,string metodoPago)
        {
            

            try
            {
                ReportService.ReportingService2005 clt = new ReportingService2005();
                string userName = ConfigurationManager.AppSettings["RSUserName"];
                string password = ConfigurationManager.AppSettings["RSPass"];
                string url = ConfigurationManager.AppSettings["RSUrlService"];
                //clt.Credentials = System.Net.CredentialCache.DefaultCredentials;
                clt.Credentials = new NetworkCredential(userName, password);
                clt.Url = url;
                CatalogItem[] cats = clt.ListChildren("/", true);
                var rep = GetRutaPdf(tipo);
                var reporte = cats.FirstOrDefault(p => p.Name == comprobante.Emisor.Rfc + "_" + rep);
                if (reporte == null)
                    reporte = cats.FirstOrDefault(p => p.Name == rep);

                if (reporte == null)
                {
                    throw new FaultException("No esta configurada la plantilla para este comprobante");
                }
                if (rep == "PdfHonorarios")
                {
                    comprobante.SubTotal = Convert.ToDecimal(comprobante.Impuestos.TotalImpuestosTrasladados) + comprobante.SubTotal;
                }
                int idEmpre = 0;
                var pdf = GuardaReporte(comprobante,metodoPago,ref idEmpre);
                id = pdf.IdComprobantePdf;
                string xmlData = null;
                xmlData = comprobante.XmlString;

                var db = new GAFEntities();
                    var cliente = db.clientes.FirstOrDefault(p => p.RFC == comprobante.Receptor.Rfc);

                    return GetReport(reporte.Path, idEmpre, cliente.idCliente, pdf.IdComprobantePdf, xmlData);
                    
            }
            catch (Exception ee)
            {
                //System.Diagnostics.Debugger.Launch();
                Logger.Error(ee);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                throw;
            }

        }



        private ComprobantePdf GuardaReporte(Comprobante comprobante, string MetodoPago, ref int idEmpre)
        {
            ComprobantePdf pdf = new ComprobantePdf();
             
            pdf.timbre_CadenaOriginal=comprobante.Complemento.timbreFiscalDigital.cadenaOriginal;
            pdf.timbre_FechaTimbrado=comprobante.Complemento.timbreFiscalDigital.FechaTimbrado.ToString();
            pdf.timbre_Leyenda=comprobante.Complemento.timbreFiscalDigital.Leyenda;
            pdf.timbre_NoCertificadoSAT=comprobante.Complemento.timbreFiscalDigital.NoCertificadoSAT;
            pdf.timbre_RfcProvCertif = comprobante.Complemento.timbreFiscalDigital.RfcProvCertif;
            pdf.timbre_SelloCFD = comprobante.Complemento.timbreFiscalDigital.SelloCFD;
            pdf.timbre_SelloSAT = comprobante.Complemento.timbreFiscalDigital.SelloSAT;
            pdf.timbre_UUID = comprobante.Complemento.timbreFiscalDigital.UUID;
            pdf.timbre_Version = comprobante.Complemento.timbreFiscalDigital.Version;
            pdf.CadenaOriginalTimbre = comprobante.CadenaOriginalTimbre;

            string enteros;
            string decimales;
            string totalLetra = comprobante.Total.ToString();
            if (totalLetra.IndexOf('.') == -1)
            {
                enteros = "0";
                decimales = "0";
            }
            else
            {
                enteros = totalLetra.Substring(0, totalLetra.IndexOf('.'));
                decimales = totalLetra.Substring(totalLetra.IndexOf('.') + 1);
            }

            string total = enteros.PadLeft(10, '0') + "." + decimales.PadRight(6, '0');
            var cantidadletra = CantidadLetra.Enletras(totalLetra, comprobante.Moneda);
            pdf.CantidadLetra = cantidadletra;
           
            int tam_var = comprobante.Sello.Length;
            string Var_Sub = comprobante.Sello.Substring((tam_var - 8), 8);

            //para CFDI
            string URL = @"https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx";
            //para retenciones
            //string URL = @"https://prodretencionverificacion.clouda.sat.gob.mx/";


            string cadenaCodigo = URL + "?" + "&id=" + comprobante.Complemento.timbreFiscalDigital.UUID.ToUpper() +  "&re=" + comprobante.Emisor.Rfc + "&rr=" + comprobante.Receptor.Rfc + "&tt=" + total+"&fe=" + Var_Sub ;

            byte[] bm = GetQrCode(cadenaCodigo);
            pdf.QrCode = bm;
            pdf.XmlString = comprobante.XmlString;// revisar si es conveniente guardar todo el xml
            string logoEmpresa = Path.Combine(ConfigurationManager.AppSettings["Resources"],
                                              comprobante.Emisor.Rfc, "Logo.png");
            if (!File.Exists(logoEmpresa))
            {
                logoEmpresa = Path.Combine(ConfigurationManager.AppSettings["Resources"], "LogoGenerico.png");
            }

            var db = new GAFEntities();
            var empresa = db.empresa.FirstOrDefault(p => p.RFC == comprobante.Emisor.Rfc);
            if (empresa != null)
                idEmpre = empresa.IdEmpresa;
            if (empresa != null && empresa.Logo == null)
            {
                empresa.Logo = File.ReadAllBytes(logoEmpresa);
                db.empresa.ApplyCurrentValues(empresa);

            }
              
            db.ComprobantePdf.AddObject(pdf);
           
            db.SaveChanges();
            return pdf;
        }
        /*

        private byte[] GetReport(string report, string xmlData, string cadenaOriginal, string cantidadLetra,string qrCodeb64, string logoB64 )
        {
            Logger.Debug(report);
            ReportExecutionService rs = new ReportExecutionService();
            string userName = ConfigurationManager.AppSettings["RSUserName"];
            string password = ConfigurationManager.AppSettings["RSPass"];
            string url = ConfigurationManager.AppSettings["RSUrlExec"];
            rs.Credentials = new NetworkCredential(userName, password);
            rs.Url = url;
            //rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            string reportPath = report;//"/ReportesNtLink/Pdf";
            string format = "Pdf";
            string devInfo = @"<DeviceInfo> <OutputFormat>PDF</OutputFormat> </DeviceInfo>";
            int parametros = 5;
            ParameterValue[] parameters = new ParameterValue[parametros];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "CantLetra";
            parameters[0].Value = cantidadLetra;
            parameters[1] = new ParameterValue();
            parameters[1].Name = "CadenaOri";
            parameters[1].Value = cadenaOriginal;
            
                parameters[2] = new ParameterValue();
                parameters[2].Name = "XmlData";
                parameters[2].Value = xmlData;
            parameters[3] = new ParameterValue();
                parameters[3].Name = "QrCode";
                parameters[3].Value = qrCodeb64;
            parameters[4] = new ParameterValue();
                parameters[4].Name = "Logo";
                parameters[4].Value = logoB64;
            //DataSourceCredentials creds = new DataSourceCredentials();

            ////Quitar hardcodeado de base de datos
            //creds.DataSourceName = "DSGAF";
            //creds.UserName = "Admin";
            //creds.Password = "99300055";
            //rs.SetExecutionCredentials(new[] { creds });
            ExecutionHeader execHeader = new ExecutionHeader();
            rs.Timeout = 300000;
            rs.ExecutionHeaderValue = execHeader;
            string historyId = null;
            rs.LoadReport(reportPath, historyId);
            rs.SetExecutionParameters(parameters, "en-US");

            try
            {
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
        */

        private byte[] GetReport(string report, int empresa,int idcliente, long idPdf, string xmlData)
        {
            Logger.Debug(report + "-" + empresa + "-" + idPdf);
            ReportExecutionService rs = new ReportExecutionService();
            string userName = ConfigurationManager.AppSettings["RSUserName"];
            string password = ConfigurationManager.AppSettings["RSPass"];
            string url = ConfigurationManager.AppSettings["RSUrlExec"];
            rs.Credentials = new NetworkCredential(userName, password);
            rs.Url = url;
            //rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            string reportPath = report;//"/ReportesNtLink/Pdf";
            string format = "Pdf";
            string devInfo = @"<DeviceInfo> <OutputFormat>PDF</OutputFormat> </DeviceInfo>";
            int parametros = 0;
            if (xmlData == null)
                parametros = 3;
            else parametros = 4;
            ReportExecution.ParameterValue[] parameters = new ReportExecution.ParameterValue[parametros];
            parameters[0] = new ReportExecution.ParameterValue();
            parameters[0].Name = "Empresa";
            parameters[0].Value = empresa.ToString();
            parameters[1] = new ReportExecution.ParameterValue();
            parameters[1].Name = "IdPdf";
            parameters[1].Value = idPdf.ToString();
            parameters[2] = new ReportExecution.ParameterValue();
            parameters[2].Name = "Cliente";
            parameters[2].Value = idcliente.ToString();

            if (xmlData != null)
            {
                parameters[3] = new ReportExecution.ParameterValue();
                parameters[3].Name = "XmlData";
                parameters[3].Value = xmlData;
                
            }
            //DataSourceCredentials creds = new DataSourceCredentials();

            ////Quitar hardcodeado de base de datos
            //creds.DataSourceName = "DSGAF";
            //creds.UserName = "Admin";
            //creds.Password = "99300055";
            //rs.SetExecutionCredentials(new[] { creds });
            ExecutionHeader execHeader = new ExecutionHeader();
            rs.Timeout = 300000;
            rs.ExecutionHeaderValue = execHeader;
            string historyId = null;
            rs.LoadReport(reportPath, historyId);
            rs.SetExecutionParameters(parameters, "en-US");

            try
            {
                string mimeType;
                string encoding;
                string fileNameExtension;
                ReportExecution.Warning[] warnings;
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

        


        private string GetRutaPdf(TipoDocumento tipo)
        {
            string ruta = null;
            ruta = "PdfGAF33";
            if (tipo == TipoDocumento.FacturaTransportista)
                ruta = "PdfTransportista";
            else if (tipo == TipoDocumento.FacturaAduanera)
                ruta = "Aduanera";
            else if (tipo == TipoDocumento.Referencia)
                ruta = "PdfReferencia";
            else if (tipo == TipoDocumento.ReciboHonorarios || tipo == TipoDocumento.Arrendamiento)
                ruta = "PdfHonorarios";
            else if (tipo == TipoDocumento.FacturaGeneralFirmas)
                ruta = "PdfFirmas";
            else if (tipo == TipoDocumento.ConstructorFirmas)
                ruta = "ConstructorFirmas";
            else if (tipo == TipoDocumento.Constructor)
                ruta = "Constructor";
            else if (tipo == TipoDocumento.ConstructorFirmasCustom)
                ruta = "ConstructorFirmasCustom";
            else if (tipo == TipoDocumento.FacturaLiverpool)
                ruta = "FacturaLiverpool";
            else if (tipo == TipoDocumento.FacturaMabe)
                ruta = "FacturaMabe";
            else if (tipo == TipoDocumento.FacturaDeloitte)
                ruta = "FacturaDeloitte";
            else if (tipo == TipoDocumento.FacturaSorianaCEDIS)
                ruta = "FacturaSorianaCEDIS";
            else if (tipo == TipoDocumento.FacturaSorianaTienda)
                ruta = "FacturaSorianaTienda";
            else if (tipo == TipoDocumento.FacturaAdo)
                ruta = "FacturaAdo";
            else if (tipo == TipoDocumento.CorporativoAduanal)
                ruta = "CorporativoAduanal";
            else if (tipo == TipoDocumento.FacturaLucent)
                ruta = "PdfLucent";
            else if (tipo == TipoDocumento.CartaPorte)
                ruta = "PdfCartaPorte";
            else if (tipo == TipoDocumento.Nomina)
                ruta = "PdfNomina";
            else if (tipo == TipoDocumento.Complementos)
                ruta = "PdfComplementos";
            else if (tipo == TipoDocumento.Retenciones)
            ruta = "Retenciones";
            return ruta;

        }

        private string GetRutaPdfCustomizado(Comprobante comprobante, int orientacion, TipoDocumento tipo)
        {
            string ruta = null;
            ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reportes", comprobante.Emisor.Rfc, orientacion == 0 ?
                "Pdf33.rdlc" : "Horizontal.rdlc");

            if (tipo == TipoDocumento.FacturaTransportista)
                ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reportes", comprobante.Emisor.Rfc, "PdfTransportista.rdlc");
            else if (tipo == TipoDocumento.FacturaAduanera)
                ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reportes", comprobante.Emisor.Rfc, "Aduanera.rdlc");
            else if (tipo == TipoDocumento.Referencia)
                ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reportes", comprobante.Emisor.Rfc, "PdfReferencia.rdlc");
            else if (tipo == TipoDocumento.Constructor)
                ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reportes", comprobante.Emisor.Rfc, "Constructor.rdlc");
            else if (tipo == TipoDocumento.FacturaGeneralFirmas)
                ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reportes", comprobante.Emisor.Rfc, "PdfFirmas.rdlc");

            else if (tipo == TipoDocumento.ConstructorFirmas)
                ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reportes", comprobante.Emisor.Rfc, "ConstructorFirmas.rdlc");


            else if (tipo == TipoDocumento.ReciboHonorarios || tipo == TipoDocumento.Arrendamiento)
            {
                ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reportes", comprobante.Emisor.Rfc, "PdfHonorarios.rdlc");
                comprobante.SubTotal = Convert.ToDecimal( comprobante.Impuestos.TotalImpuestosTrasladados) + comprobante.SubTotal;
            }

            return ruta;
        }

        /*
        public static Comprobante GetComprobanteFromString(string xmlContent)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Comprobante));
            StringReader sr = new StringReader(xmlContent);
            object obj = ser.Deserialize(sr);
            var c = obj as Comprobante;

            if (c != null && c.Complemento != null && c.Complemento.Any.Count > 0)
            {
                var d = c.Complemento.Any.FirstOrDefault(p => p.LocalName == "TimbreFiscalDigital");
                if (d != null)
                {
                    XmlSerializer des = new XmlSerializer(typeof(TimbreFiscalDigital));
                    TimbreFiscalDigital tim = (TimbreFiscalDigital)des.Deserialize(new XmlTextReader(new StringReader(d.OuterXml)));
                    GeneradorCadenasTimbre gcad = new GeneradorCadenasTimbre();
                    var cadenaTimbre = gcad.CadenaOriginal(xmlContent);
                    c.CadenaOriginalTimbre = cadenaTimbre;
                    c.Complemento.timbreFiscalDigital = tim;
                }
                
            }
            return c;
        }
        */
        public static string Firmar(string cadenaOriginal, string rutaLlave, string pass)
        {
            if (!File.Exists(rutaLlave))
            {
                throw new Exception("No se encontró el archivo: rutallave");
            }
         
            byte[] llave = File.ReadAllBytes(rutaLlave);
            if (File.Exists(rutaLlave + ".pem"))
            {
                rutaLlave = rutaLlave + ".pem";
            }
            string ext = Path.GetExtension(rutaLlave);
            //if (string.IsNullOrEmpty(pass))
            //    pass = "12345678a";
            RSACryptoServiceProvider privateKey1 = OpensslKey.DecodePrivateKey(llave, pass, ext);
            UTF8Encoding e = new UTF8Encoding(true);
            byte[] signature = privateKey1.SignData(e.GetBytes(cadenaOriginal), "SHA256");
            string sello256 = Convert.ToBase64String(signature);

            return sello256;

        
        }
        

        private readonly XNamespace _nsRet = "http://www.sat.gob.mx/esquemas/retencionpago/1";
        private readonly XNamespace _ns = "http://www.sat.gob.mx/cfd/4";
        private readonly XNamespace _donat = "http://www.sat.gob.mx/cfd/donat";

        private string ConcatenaTimbreRet(XElement entrada, string xmlTimbre, string xmlDonat, string xmlAddenda,
            bool addendaRepetida, List<XElement> nodosAddenda = null)
        {
            XElement timbre = XElement.Load(new StringReader(xmlTimbre));
          //  XElement timbre = new XElement("timbre", "RecordRGV");//quitar esto solo pruebas
            var complemento = entrada.Elements(_nsRet + "Complemento").FirstOrDefault();
            if (complemento == null)
            {
                entrada.Add(new XElement(_nsRet + "Complemento"));
                complemento = entrada.Elements(_nsRet + "Complemento").FirstOrDefault();
            }
            complemento.Add(timbre);
            if (xmlDonat != null)
            {
                XElement donat = XElement.Load(new StringReader(xmlDonat));
                complemento.Add(donat);
            }
            if (xmlAddenda != null)
            {
                XElement add = XElement.Load(new StringReader(xmlAddenda));
                if (addendaRepetida)
                {
                    entrada.Add(add);
                }
                else
                {
                    entrada.Add(new XElement(_nsRet + "Addenda"));
                    var addenda = entrada.Elements(_nsRet + "Addenda").FirstOrDefault();
                    addenda.Add(add);
                }
            }
            if (nodosAddenda != null && nodosAddenda.Count > 0)
            {
                entrada.Add(new XElement(_nsRet + "Addenda"));
                var addenda = entrada.Elements(_nsRet + "Addenda").FirstOrDefault();
                foreach (XElement nodosAddendum in nodosAddenda)
                {
                    addenda.Add(nodosAddendum);
                }
            }

            MemoryStream mem = new MemoryStream();
            StreamWriter tw = new StreamWriter(mem, Encoding.UTF8);
            //XmlWriter xmlWriter = XmlWriter.Create(tw,
            //                                     new XmlWriterSettings() {Indent = false, Encoding = Encoding.UTF8});
            entrada.Save(tw, SaveOptions.DisableFormatting);
            string xml = Encoding.UTF8.GetString(mem.GetBuffer());
            xml = xml.Substring(xml.IndexOf(Convert.ToChar(60)));
            xml = xml.Substring(0, (xml.LastIndexOf(Convert.ToChar(62)) + 1));
            //xml = xml.Replace("xmlns:donat=\"http://www.sat.gob.mx/donat\"", "");

            return xml;
        }




        private string ConcatenaTimbre(XElement entrada, string xmlTimbre, string xmlDonat, string xmlAddenda, bool addendaRepetida, List<XElement> nodosAddenda = null )
        {
            XElement timbre = XElement.Load(new StringReader(xmlTimbre));
           // XElement timbre = new XElement("timbre","RecordRGV");//quitar esto
            
            var complemento = entrada.Elements(_ns + "Complemento").FirstOrDefault();
            if (complemento == null)
            {
                entrada.Add(new XElement(_ns + "Complemento"));
                complemento = entrada.Elements(_ns + "Complemento").FirstOrDefault();
            }
            complemento.Add(timbre);
            if (xmlDonat != null)
            {
                XElement donat = XElement.Load(new StringReader(xmlDonat));
                complemento.Add(donat);
            }
            if (xmlAddenda != null)
            {
                XElement add = XElement.Load(new StringReader(xmlAddenda));
                if (addendaRepetida)
                {
                    entrada.Add(add);
                }
                else
                {
                    entrada.Add(new XElement(_ns + "Addenda"));
                    var addenda = entrada.Elements(_ns + "Addenda").FirstOrDefault();
                    addenda.Add(add);
                }
            }
            if (nodosAddenda != null && nodosAddenda.Count > 0)
            {
                entrada.Add(new XElement(_ns + "Addenda"));
                var addenda = entrada.Elements(_ns + "Addenda").FirstOrDefault();
                foreach (XElement nodosAddendum in nodosAddenda)
                {
                    addenda.Add(nodosAddendum);
                }
            }

            MemoryStream mem = new MemoryStream();
            StreamWriter tw = new StreamWriter(mem, Encoding.UTF8);
            //XmlWriter xmlWriter = XmlWriter.Create(tw,
            //                                     new XmlWriterSettings() {Indent = false, Encoding = Encoding.UTF8});
            entrada.Save(tw, SaveOptions.DisableFormatting);
            string xml = Encoding.UTF8.GetString(mem.GetBuffer());
            xml = xml.Substring(xml.IndexOf(Convert.ToChar(60)));
            xml = xml.Substring(0, (xml.LastIndexOf(Convert.ToChar(62)) + 1));
            //xml = xml.Replace("xmlns:donat=\"http://www.sat.gob.mx/donat\"", "");


            return xml;
        }



        public string GetXmlAddenda(object addenda, Type tipoAddenda, string prefijo, string ns)
        {
            XmlSerializer ser;
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();

            if (string.IsNullOrEmpty(prefijo))
            {
                ser = new XmlSerializer(tipoAddenda, ns);
            }
            else
            {
                ser = new XmlSerializer(tipoAddenda);
                namespaces.Add(prefijo, ns);
            }

            try
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    var sw = new StreamWriter(memStream, Encoding.UTF8);
                    using (
                        XmlWriter xmlWriter = XmlWriter.Create(sw,
                                                               new XmlWriterSettings() { Indent = false, Encoding = Encoding.UTF8 }))
                    {
                        if (namespaces.Count > 0)
                            ser.Serialize(xmlWriter, addenda, namespaces);
                        else
                        {
                            ser.Serialize(xmlWriter, addenda);
                        }
                        string xml = Encoding.UTF8.GetString(memStream.GetBuffer());
                        xml = xml.Substring(xml.IndexOf(Convert.ToChar(60)));
                        xml = xml.Substring(0, (xml.LastIndexOf(Convert.ToChar(62)) + 1));
                        return xml;
                    }
                }
            }
            catch (Exception ee)
            {

                Logger.Error(ee);
                return null;
            }
        }

        public string GetXmlINE(Contract.Complemento.INE impuestos)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Contract.Complemento.INE));
            try
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    var sw = new StreamWriter(memStream, Encoding.UTF8);
                    using (
                        XmlWriter xmlWriter = XmlWriter.Create(sw,
                                                               new XmlWriterSettings() { Indent = false, Encoding = Encoding.UTF8 }))
                    {
                        XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                        namespaces.Add("ine", "http://www.sat.gob.mx/ine");
                        ser.Serialize(xmlWriter, impuestos, namespaces);
                        string xml = Encoding.UTF8.GetString(memStream.GetBuffer());
                        xml = xml.Substring(xml.IndexOf(Convert.ToChar(60)));
                        xml = xml.Substring(0, (xml.LastIndexOf(Convert.ToChar(62)) + 1));
                        //xml = xml.Replace("xmlns:donat=\"http://www.sat.gob.mx/donat\"", "");
                        xml = xml.Replace("p1:schemaLocation=\"http://www.sat.gob.mx/Pagos20 http://www.sat.gob.mx/sitio_internet/cfd/Pagos/Pagos20.xsd\"", "");
                        xml = xml.Replace("xmlns:p1=\"http://www.w3.org/2001/XMLSchema-instance\"", "");

                        return xml;
                    }
                }
            }
            catch (Exception ee)
            {

                Logger.Error(ee);
                return null;
            }

        }


        public string GetXmlImpuestosLocales(ImpuestosLocales impuestos)
        {
            XmlSerializer ser = new XmlSerializer(typeof(ImpuestosLocales));
            try
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    var sw = new StreamWriter(memStream, Encoding.UTF8);
                    using (
                        XmlWriter xmlWriter = XmlWriter.Create(sw,
                                                               new XmlWriterSettings() { Indent = false, Encoding = Encoding.UTF8 }))
                    {
                        XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                        namespaces.Add("implocal", "http://www.sat.gob.mx/implocal");
                        ser.Serialize(xmlWriter, impuestos, namespaces);
                        string xml = Encoding.UTF8.GetString(memStream.GetBuffer());
                        xml = xml.Substring(xml.IndexOf(Convert.ToChar(60)));
                        xml = xml.Substring(0, (xml.LastIndexOf(Convert.ToChar(62)) + 1));
                        //xml = xml.Replace("xmlns:donat=\"http://www.sat.gob.mx/donat\"", "");
                        return xml;
                    }
                }
            }
            catch (Exception ee)
            {

                Logger.Error(ee);
                return null;
            }

        }

        public string GetXmlPagos(Contract.Complemento40.Pagos impuestos)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Contract.Complemento40.Pagos));
            try
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    var sw = new StreamWriter(memStream, Encoding.UTF8);
                    using (
                        XmlWriter xmlWriter = XmlWriter.Create(sw,
                                                               new XmlWriterSettings() { Indent = false, Encoding = Encoding.UTF8 }))
                    {
                        XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                        namespaces.Add("pago20", "http://www.sat.gob.mx/Pagos20");
                        ser.Serialize(xmlWriter, impuestos, namespaces);
                        string xml = Encoding.UTF8.GetString(memStream.GetBuffer());
                        xml = xml.Substring(xml.IndexOf(Convert.ToChar(60)));
                        xml = xml.Substring(0, (xml.LastIndexOf(Convert.ToChar(62)) + 1));
                        //xml = xml.Replace("xmlns:donat=\"http://www.sat.gob.mx/donat\"", "");
                        xml=xml.Replace("p1:schemaLocation=\"http://www.sat.gob.mx/Pagos20 http://www.sat.gob.mx/sitio_internet/cfd/Pagos/Pagos20.xsd\"","");
                        xml = xml.Replace("xmlns:p1=\"http://www.w3.org/2001/XMLSchema-instance\"", "");

                        return xml;
                    }
                }
            }
            catch (Exception ee)
            {

                Logger.Error(ee);
                return null;
            }

        }


        public void TimbrarComprobanteNtLink(Comprobante comp)
        {
            bool addendaRepetida = false;
            List<XElement> elAddenda = new List<XElement>();
            // ClienteTimbradoNtlink cliente = new ClienteTimbradoNtlink();
            //  CertificadorAppsClient internalCertificadorAppsClient =  new CertificadorAppsClient();
        
            // ClienteTimbradoNtlink cliente = new ClienteTimbradoNtlink();
            ClienteTimbradoXpress cliente = new ClienteTimbradoXpress();
            try
            {
                //string complemento = null;
                Logger.Debug("Timbrando comprobante");

                XmlSerializer ser = new XmlSerializer(typeof(TimbreFiscalDigital));

                //var str = GetXml(comp, complemento);
                ServicePointManager.DefaultConnectionLimit = 200;
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) =>
                {
                    return true;
                };

                //string configUserName = ConfigurationManager.AppSettings["InternalClientUserName"];
                //string configPassword = ConfigurationManager.AppSettings["InternalClientPassword"];
                //cliente.TimbraCfdi(comp.XmlString);

               // string timbreString = cliente.TimbraCfdiFacturacionModerna(comp.XmlString,comp.Emisor.Rfc);
                //------------para Xpress-------------
                string timbreString = cliente.ClienteTimbradoXPRESS(comp.XmlString);
                if (cliente.Codigo != "200")
                {
                    Logger.Error(cliente.Mensaje);
                    throw new FaultException(cliente.Mensaje);

                }
                //----------------
                Logger.Debug(timbreString);
                TimbreFiscalDigital timbre = null;
                try
                {
                    timbre = (TimbreFiscalDigital)ser.Deserialize(new XmlTextReader(new StringReader(timbreString)));
                    
                    //  byte[] pdf;
                    //  pdf = Convert.FromBase64String(cliente.pdfB64);
                    //string ruta = Path.Combine(ConfigurationManager.AppSettings["Salida"], comp.Emisor.Rfc);
                    //string pdfFile = Path.Combine(ruta, timbre.UUID + ".pdf");
                    //File.WriteAllBytes(pdfFile, pdf);
                    
               
                 //   timbre= new TimbreFiscalDigital();//quitar esto
                 //   timbre.UUID = "ESTOESELUUID";     //quitar esto
                }
                catch (Exception ee)
                {
                    Logger.Error(ee);
                    throw new FaultException(timbreString);
                }
                if (timbreString == null)
                {
                    throw new Exception("Ocurrió un error en el timbrado");
                }
                GeneradorCadenasTimbre generadorCadenasTimbre = new GeneradorCadenasTimbre();
                comp.CadenaOriginalTimbre = generadorCadenasTimbre.CadenaOriginal(timbreString);
                timbre.cadenaOriginal = comp.CadenaOriginalTimbre;
               // comp.CadenaOriginalTimbre = "CadenaOriginalTimbre";  //quitar esto
                string addendaXml = null;

                //---------------------------------
               /* if (comp.TipoAddenda == TipoAddenda.ASONIOSCOC)
                {
                    //comp.xsiSchemaLocation = comp.xsiSchemaLocation + " http://www.honda.net.mx/GPC";
                    ASONIOSCOC addendaPemex = comp.AddendaASONIOSCOC;
                    addendaXml = GetXmlAddenda(addendaPemex, typeof(ASONIOSCOC), "cfdi", "http://www.ntlink.com.mx/RGV");

                }
                //rgv agrego la adenda honda
                //---------------------------------
                if (comp.TipoAddenda == TipoAddenda.Honda)
                {
                    //    addendaXml = GetXmlAddendaDeloitte(addenda);
                    comp.xsiSchemaLocation = comp.xsiSchemaLocation + " http://www.honda.net.mx/GPC";
                    Honda addendaPemex = comp.AddendaHonda;
                    addendaXml = GetXmlAddenda(addendaPemex, typeof(Honda), "GPC", "http://www.honda.net.mx/GPC");

                }
                
                

             
                if (comp.AddendaAmece != null)
                {

                    requestForPayment addendaAmece = comp.AddendaAmece;
                    addendaXml = GetXmlAddenda(addendaAmece, typeof(requestForPayment), null, null);
                }
                if (comp.AddendaHomeDepot != null)
                {

                    HomeDepotRequestForPayment addenda = comp.AddendaHomeDepot;
                    addendaXml = GetXmlAddenda(addenda, typeof(HomeDepotRequestForPayment), null, null);
                }
                if (comp.AddendaCoppelObj != null)
                {
                    comp.AddendaCoppelObj.requestForPayment.cadenaOriginal = new AddendaRequestForPaymentCadenaOriginal()
                                                                                 {
                                                                                     Cadena = comp.CadenaOriginal
                                                                                 };
                    addendaXml = GetXmlAddenda(comp.AddendaCoppelObj.requestForPayment,
                                               typeof (AddendaRequestForPayment), null, null);
                }
                */
                string cfdiString = comp.XmlString;
                StringReader sr = new StringReader(cfdiString);
                XElement element = XElement.Load(sr);
                
               

                string xmlFinal = ConcatenaTimbre(element, timbreString, null, addendaXml, addendaRepetida, elAddenda);
               
                comp.Complemento = new ComprobanteComplemento() { timbreFiscalDigital = timbre };

                if (comp.TipoAddenda == TipoAddenda.Amazon)//para quitar p1 por xsd
                {
                    xmlFinal = xmlFinal.Replace("p1:schemaLocation", "xsi:schemaLocation");
                    xmlFinal = xmlFinal.Replace("xmlns:p1=\"http://www.w3.org/2001/XMLSchema-instance\"", "");
         
                }
                if (comp.TipoAddenda == TipoAddenda.ASONIOSCOC)//para quitar p1 por xsd
                {
                    xmlFinal = xmlFinal.Replace("xmlns:cfdi=\"http://www.ntlink.com.mx/RGV\"", "");
               
                }
        
            
                comp.XmlString = xmlFinal;
            }
            catch (FaultException fe)
            {
                Logger.Info(fe);
                throw;
            }
            catch (SoapException exception)
            {
                Logger.Error(exception.Detail.InnerText.Trim());
                throw new ApplicationException("Error al timbrar el comprobante:" + exception.Detail.InnerText.Trim(), exception);
            }
            catch (Exception exception)
            {
                Logger.Error((exception.InnerException == null ? exception.Message : exception.InnerException.Message));
                throw new Exception("Error al timbrar el comprobante", exception);
            }
        }


      

        public void TimbrarComprobantePreview(Comprobante comp)
        {
            //ClienteTimbradoNtlink cliente = new ClienteTimbradoNtlink();
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(TimbreFiscalDigital));
                TimbreFiscalDigital timbre = null;
                try
                {
                    timbre = new TimbreFiscalDigital()
                    {
                        UUID = "No Timbrado",
                        FechaTimbrado = DateTime.Now,
                        NoCertificadoSAT = "000",
                        SelloCFD = comp.Sello,
                        SelloSAT = "Inválido",
                        Version = "1.0"
                    };

                }
                catch (Exception ee)
                {
                    Logger.Error(ee);
                }
                GeneradorCadenasTimbre generadorCadenasTimbre = new GeneradorCadenasTimbre();
                comp.CadenaOriginalTimbre = "Inválido";

                string complementoIL = null;
                //para los impuestos locales
                if (comp.Complemento != null && comp.Complemento.implocal != null)
                {
                    complementoIL = GetXmlImpuestosLocales(comp.Complemento.implocal);
                }

                string cfdiString = GetXml(comp, complementoIL);

                //string cfdiString = GetXml(comp,null);
                StringReader sr = new StringReader(cfdiString);
                var sw = new StringWriter();
                XElement element = XElement.Load(sr);

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = new UnicodeEncoding(false, false); // no BOM in a .NET string
                settings.Indent = false;
                settings.OmitXmlDeclaration = false;
                XmlWriter xmlWriter = XmlWriter.Create(sw, settings);
                ser.Serialize(xmlWriter, timbre);
                string xmlFinal = ConcatenaTimbre(element, sw.ToString(), null, null, false);

                comp.Complemento = new ComprobanteComplemento() { timbreFiscalDigital = timbre };
             
                comp.XmlString = xmlFinal;

            }
            catch (FaultException fe)
            {
                throw;
            }
            catch (SoapException exception)
            {
                Logger.Error(exception.Detail.InnerText.Trim());
                throw new ApplicationException("Error al timbrar el comprobante:" + exception.Detail.InnerText.Trim(), exception);
            }
            catch (Exception exception)
            {
                Logger.Error((exception.InnerException == null ? exception.Message : exception.InnerException.Message));
                throw new Exception("Error al timbrar el comprobante", exception);
            }


        }


        /*
        public void GenerarCfdRetPreview(Retenciones.Retenciones comprobante, X509Certificate2 cert, string rutaLlave, string passLlave)
        {
            try
            {
                comprobante.Cert = Convert.ToBase64String(cert.RawData);
                comprobante.NumCert = NoCert(cert.SerialNumber);
                GeneradorCadenas gen = new GeneradorCadenas();
                string comp = GetXmlRetenciones(comprobante);
                comprobante.XmlString = comp;
                comprobante.CadenaOriginal = gen.CadenaOriginal(comp);
                comprobante.Sello = "Vista Previa";///Firmar(comprobante.CadenaOriginal, rutaLlave, passLlave);
                TimbrarRetPreview(comprobante);
            }
            catch (FaultException fe)
            {
                Logger.Error(fe);
                throw;
            }
            catch (Exception exception)
            {
                Logger.Error((exception.InnerException == null ? exception.Message : exception.InnerException.Message));
                throw;
            }
        }
        */
        
        public void GenerarCfdPreview(Comprobante comprobante, X509Certificate2 cert, string rutaLlave, string passLlave)
        {
            try
            {
                //string complemento = null;
                comprobante.Certificado = Convert.ToBase64String(cert.RawData);
                comprobante.NoCertificado = NoCert(cert.SerialNumber);
                GeneradorCadenas gen = new GeneradorCadenas();  //agregar otra vez

                
                string comp = GetXml(comprobante, null);
                comprobante.XmlString = comp;
                comprobante.CadenaOriginal = gen.CadenaOriginal(comp);
                comprobante.Sello = "Vista Previa";///Firmar(comprobante.CadenaOriginal, rutaLlave, passLlave);
                TimbrarComprobantePreview(comprobante);
            }
            catch (FaultException fe)
            {
                Logger.Error(fe);
                throw;
            }
            catch (Exception exception)
            {
                Logger.Error((exception.InnerException == null ? exception.Message : exception.InnerException.Message));
                throw;
            }
        }
          

        
        public void GenerarCfd(Comprobante comprobante, X509Certificate2 cert, string rutaLlave, string passLlave)
        {
            try
            {
                Logger.Debug("Generando xml");
                comprobante.Certificado = Convert.ToBase64String(cert.RawData);
                comprobante.NoCertificado = NoCert(cert.SerialNumber);
                string complemento = null;
                GeneradorCadenas gen = new GeneradorCadenas();

               
                //if (comprobante.Detallista != null)
                //{
                //    comprobante.xsiSchemaLocation = comprobante.xsiSchemaLocation + " http://www.sat.gob.mx/detallista http://www.sat.gob.mx/sitio_internet/cfd/detallista/detallista.xsd";
                //    complemento = GetXmlAddenda(comprobante.Detallista, typeof(detallista), "detallista", "http://www.sat.gob.mx/detallista");
                //    //comprobante.XmlNomina = complemento;
                    
                //}
                
                if (comprobante.Complemento != null && comprobante.Complemento.Pag != null)
                {
                    comprobante.xsiSchemaLocation = comprobante.xsiSchemaLocation + " http://www.sat.gob.mx/Pagos20 http://www.sat.gob.mx/sitio_internet/cfd/Pagos/Pagos20.xsd";
                    complemento = GetXmlPagos(comprobante.Complemento.Pag);//GetXmlAddenda(comprobante.Complemento.Pag, typeof(GAFContract.Complemento.Pagos), "pago10", " http://www.sat.gob.mx/Pagos");
                
                
                }
                if (comprobante.Complemento != null && comprobante.Complemento.ine != null)
                {
                    comprobante.xsiSchemaLocation = comprobante.xsiSchemaLocation + " http://www.sat.gob.mx/ine http://www.sat.gob.mx/sitio_internet/cfd/ine/ine11.xsd";
                  //  complemento = GetXmlAddenda(comprobante.Complemento.ine, typeof(GAFContract.Complemento.INE), "ine", " http://www.sat.gob.mx/ine");
                    complemento = GetXmlINE(comprobante.Complemento.ine);

                }


                
                if (comprobante.Complemento != null && comprobante.Complemento.implocal != null)
                {
                    complemento = GetXmlImpuestosLocales(comprobante.Complemento.implocal);
                }

                string comp = GetXml(comprobante, complemento);
                if (comprobante.Complemento != null && comprobante.Complemento.Pag != null)
                {
                    comp = comp.Replace("<pago20:Pagos Version=\"2.0\" xmlns:pago20=\"http://www.sat.gob.mx/Pagos20\">", "<pago20:Pagos Version=\"2.0\">");
                    comp = comp.Replace("xmlns:cfdi=\"http://www.sat.gob.mx/cfd/4\"", "xmlns:cfdi=\"http://www.sat.gob.mx/cfd/4\" xmlns:pago20=\"http://www.sat.gob.mx/Pagos20\"");
                  }
                comprobante.CadenaOriginal = gen.CadenaOriginal(comp);
                comprobante.Sello = Firmar(comprobante.CadenaOriginal, rutaLlave, passLlave);
                XElement xeComprobante = XElement.Parse(comp);
                xeComprobante.Add(new XAttribute("Sello", comprobante.Sello));
                SidetecStringWriter sw = new SidetecStringWriter(Encoding.UTF8);
                xeComprobante.Save(sw,SaveOptions.DisableFormatting);
                comprobante.XmlString = sw.ToString();
              //  if (ConfigurationManager.AppSettings["Pac"] == "NtLink")
               // {
                    TimbrarComprobanteNtLink(comprobante);
                //}
               // else throw new Exception("No hay un pac configurado");
            }
            catch (FaultException fe)
            {
                Logger.Error(fe);
                throw;
            }
            catch (Exception exception)
            {
                Logger.Error((exception.InnerException == null ? exception.Message : exception.InnerException.Message));
                throw;
            }
        }
        //------------------------------
        public string GenerarCfdSinTimbrar(Comprobante comprobante, X509Certificate2 cert, string rutaLlave, string passLlave)
        {
            try
            {
                comprobante.Certificado = Convert.ToBase64String(cert.RawData);
                comprobante.NoCertificado = NoCert(cert.SerialNumber);
                string complemento = null;
                GeneradorCadenas gen = new GeneradorCadenas();


                  if (comprobante.Complemento != null && comprobante.Complemento.Pag != null)
                {
                    comprobante.xsiSchemaLocation = comprobante.xsiSchemaLocation + " http://www.sat.gob.mx/Pagos20 http://www.sat.gob.mx/sitio_internet/cfd/Pagos/Pagos20.xsd";
                    complemento = GetXmlPagos(comprobante.Complemento.Pag);//GetXmlAddenda(comprobante.Complemento.Pag, typeof(GAFContract.Complemento.Pagos), "pago10", " http://www.sat.gob.mx/Pagos");
                }
                if (comprobante.Complemento != null && comprobante.Complemento.ine != null)
                {
                    comprobante.xsiSchemaLocation = comprobante.xsiSchemaLocation + " http://www.sat.gob.mx/ine http://www.sat.gob.mx/sitio_internet/cfd/ine/ine11.xsd";
                    complemento = GetXmlAddenda(comprobante.Complemento.ine, typeof(Contract.Complemento.INE), "ine", " http://www.sat.gob.mx/ine");
                }


                if (comprobante.Complemento != null && comprobante.Complemento.implocal != null)
                {
                    complemento = GetXmlImpuestosLocales(comprobante.Complemento.implocal);
                }

                string comp = GetXml(comprobante, complemento);
                if (comprobante.Complemento != null && comprobante.Complemento.Pag != null)
                {
                    comp = comp.Replace("<pago20:Pagos Version=\"2.0\" xmlns:pago20=\"http://www.sat.gob.mx/Pagos20\">", "<pago20:Pagos Version=\"2.0\">");
                    comp = comp.Replace("xmlns:cfdi=\"http://www.sat.gob.mx/cfd/4\"", "xmlns:cfdi=\"http://www.sat.gob.mx/cfd/4\" xmlns:pago20=\"http://www.sat.gob.mx/Pagos20\"");
                }
                comprobante.CadenaOriginal = gen.CadenaOriginal(comp);
                comprobante.Sello = Firmar(comprobante.CadenaOriginal, rutaLlave, passLlave);
                XElement xeComprobante = XElement.Parse(comp);
                xeComprobante.Add(new XAttribute("Sello", comprobante.Sello));
                SidetecStringWriter sw = new SidetecStringWriter(Encoding.UTF8);
                xeComprobante.Save(sw, SaveOptions.DisableFormatting);
                comprobante.XmlString = sw.ToString();

                return comprobante.XmlString;
            }
            catch (FaultException fe)
            {
                Logger.Error(fe);
                throw;
            }
            catch (Exception exception)
            {
                Logger.Error((exception.InnerException == null ? exception.Message : exception.InnerException.Message));
                throw;
            }
        }
        
        private string NoCert(string cert)
        {
            int count = 0;
            StringBuilder sb = new StringBuilder();
            foreach (char c in cert)
            {
                if (count % 2 != 0)
                    sb.Append(c);
                count++;
            }
            return sb.ToString();
        }
        //-------------------------------------------------------------
        //--------------------------------------------------------------
        /*
        public void GenerarCfdRetenciones(Retenciones.Retenciones comprobante, X509Certificate2 cert, string rutaLlave, string passKey)
        {
            try
            {
                Logger.Debug("Generando xml");
                comprobante.NumCert = NoCert(cert.SerialNumber);
                comprobante.Cert = Convert.ToBase64String(cert.RawData);
                var now = DateTime.Now;
                now = now.AddTicks(-(now.Ticks % TimeSpan.TicksPerSecond));
                comprobante.FechaExp = now;
                var gen = new GeneradorCadenasRetenciones();

                string comp = GetXmlRetenciones(comprobante);

                XElement xeComprobantexx = XElement.Parse(comp);
                SidetecStringWriter swxx = new SidetecStringWriter(Encoding.UTF8);
                xeComprobantexx.Save(swxx, SaveOptions.DisableFormatting);

                comprobante.CadenaOriginal = gen.CadenaOriginal(swxx.ToString());
               
                //comprobante.CadenaOriginal = gen.CadenaOriginal(comp);
                comprobante.Sello = Firmar(comprobante.CadenaOriginal, rutaLlave, passKey);
                XElement xeComprobante = XElement.Parse(comp);
                xeComprobante.Add(new XAttribute("Sello", comprobante.Sello));
                SidetecStringWriter sw = new SidetecStringWriter(Encoding.UTF8);
                xeComprobante.Save(sw,SaveOptions.DisableFormatting);
                comprobante.XmlString = sw.ToString();
                if (ConfigurationManager.AppSettings["Pac"] == "NtLink")
                {
                    TimbrarRetencionesNtLink(comprobante);
                }
                else throw new Exception("No hay un pac configurado");
            }
            catch (FaultException fe)
            {
                Logger.Error(fe);
                throw;
            }
            catch (Exception exception)
            {
                Logger.Error((exception.InnerException == null ? exception.Message : exception.InnerException.Message));
                throw;
            }
        }
        
        private void TimbrarRetencionesNtLink(Retenciones.Retenciones comprobante)
        {
            CertificadorAppsClient internalCertificadorAppsClient = new CertificadorAppsClient();
            try
            {
                //string complemento = null;
                Logger.Debug("Timbrando comprobante");

                XmlSerializer ser = new XmlSerializer(typeof(TimbreFiscalDigital));

                ServicePointManager.DefaultConnectionLimit = 200;
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

                // Get the authentication data from config
                string configUserName = ConfigurationManager.AppSettings["InternalClientUserName"];
                string configPassword = ConfigurationManager.AppSettings["InternalClientPassword"];

                string timbreString = internalCertificadorAppsClient.TimbraRetencion(configUserName, configPassword, comprobante.XmlString);
                //string timbreString = "timbreRGV";//quitar solo es para pruebas

                Logger.Debug(timbreString);
                TimbreFiscalDigital timbre = null;
                try
                {
                    timbre = (TimbreFiscalDigital)ser.Deserialize(new XmlTextReader(new StringReader(timbreString)));
                   //   timbre= new TimbreFiscalDigital();//quitar esto es para pruebas
                    //   timbre.UUID = "ESTOESELUUID";     //quitar esto es para pruebas
                }
                catch (Exception ee)
                {
                    Logger.Error(ee);
                    throw new FaultException(timbreString);
                }
                if (timbreString == null)
                {
                    throw new Exception("Ocurrió un error en el timbrado");
                }
                GeneradorCadenasTimbre generadorCadenasTimbre = new GeneradorCadenasTimbre();
                comprobante.CadenaOriginalTimbre = generadorCadenasTimbre.CadenaOriginal(timbreString);
               // comprobante.CadenaOriginalTimbre = "cadenaOriginal"; //quitar esto eso para pruebas

                string cfdiString = comprobante.XmlString;
                StringReader sr = new StringReader(cfdiString);
                XElement element = XElement.Load(sr);

                string xmlFinal = ConcatenaTimbreRet(element, timbreString, null, null, false);
                comprobante.Complemento = new RetencionesComplemento { timbreFiscalDigital = timbre };
                comprobante.XmlString = xmlFinal;
            }
            catch (FaultException fe)
            {
                Logger.Info(fe);
                throw;
            }
            catch (SoapException exception)
            {
                Logger.Error(exception.Detail.InnerText.Trim());
                throw new ApplicationException("Error al timbrar el comprobante:" + exception.Detail.InnerText.Trim(), exception);
            }
            catch (Exception exception)
            {
                Logger.Error((exception.InnerException == null ? exception.Message : exception.InnerException.Message));
                throw new Exception("Error al timbrar el comprobante", exception);
            }


        }
         */ 
        //--------------------------------------------------
        private string TipoDEPago(string tipo)
        {
            string a=tipo;
            switch (tipo)
            {
                case "01": { a = "Efectivo"; break; }
                case "02": { a = "Cheque"; break; }
                case "03": { a = "Transferencia"; break; }
                case "04": { a = "Tarjetas de crédito"; break; }
                case "05": { a = "Monederos electrónicos"; break; }
                case "06": { a = "Dinero electrónico"; break; }
                case "07": { a = "Tarjetas digitales"; break; }
                case "08": { a = "Vales de despensa"; break; }
                case "09": { a = "Bienes"; break; }
                case "10": { a = "Servicio"; break; }
                case "11": { a = "Por cuenta de tercero"; break; }
                case "12": { a = "Dación en pago"; break; }
                case "13": { a = "Pago por subrogación"; break; }
                case "14": { a = "Pago por consignación"; break; }
                case "15": { a = "Condonación"; break; }
                case "16": { a = "Cancelación"; break; }
                case "17": { a = "Compensación"; break; }
                case "98": { a = "NA"; break; }
                case "99": { a = "Otros"; break; }
            }
            return a;
        }
        //----------------------------------------------

    }
}
