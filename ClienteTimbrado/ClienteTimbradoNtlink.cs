using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using ClienteNtLink.NtLinkService;
using log4net.Config;
using log4net;
using ClienteNtLink.facturacionmoderna;
using Contract;
using System.Xml;
using System.IO;
using System.Net;
using System.Configuration;
using System.Xml.Linq;

namespace ClienteNtLink
{
    public class ClienteTimbradoNtlink
    {
        private static ILog Logger = LogManager.GetLogger(typeof(ClienteTimbradoNtlink));
        public string errorCode;
        public string errorMessage;
        private string @string;
        private string @salida;
        public string successCode;
        public string successMessage ;
        string xmlB64 = "";
        public string pdfB64;
        string txtB64;
        string cbbB64;
			
        /// <summary>
        /// Constructor por default
        /// </summary>
        public ClienteTimbradoNtlink()
        {
            XmlConfigurator.Configure();
        }

        public static string Base64Encode(string plainText)
        {
    
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public string TimbraCfdiFacturacionModerna(string comprobante, string emisorRfc)
        {
                               
            try
            {
               
                if (timbrarLayout(comprobante, emisorRfc) == false)
                    return errorCode + "-" + errorMessage;
                else
                    return @salida;
                //object comp = Convert.FromBase64String(comprobante);
                //return cliente.requestTimbrarCFDI(datos);
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }

        }
        /******************************************************************************************/
        // ConnectionWSFM.ConnectionFM
        public bool timbrarLayout(string layout, string emisorRfc)
        {
            string urlService = ConfigurationManager.AppSettings["RutaClienteWS"];
            string userConnectionId = ConfigurationManager.AppSettings["UserNameWS"];
            string userConnectionPass = ConfigurationManager.AppSettings["UserPasWS"];
   		 	//string userConnectionId = "UsuarioPruebasWS";
			//string userConnectionPass = "b9ec2afa3361a59af4b4d102d3f704eabdf097d4";
			//string urlService = "https://t1demo.facturacionmoderna.com/timbrado/soap";
            //string emisorRfc = "LAN7008173R5";
			string generarTxt = "false";
			string generarPdf = "true";
			string generarCbb = "false";
			string text2Cfdi = "";
			string userProxy = "";
			string passProxy = "";
			string hostProxy = "";
			int portProxy = 80;
			bool success = true;
			string uuid = "";
             xmlB64 = "";
             pdfB64 = "";
             txtB64 = "";
             cbbB64 = "";
			

            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                string value = "requestTimbrarCFDI";
               
                text2Cfdi = Convert.ToBase64String(Encoding.UTF8.GetBytes(layout));
                string text = createSoapTimbrado(urlService, userConnectionPass, userConnectionId, emisorRfc, text2Cfdi, generarTxt, generarPdf, generarCbb);
                /*
                if (this.debugMode && this.logFilePath != "")
                {
                    using (FileStream fileStream = new FileStream(this.logFilePath, FileMode.Append, FileAccess.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(fileStream))
                        {
                            streamWriter.WriteLine("\nSoap Request Timbrado:\n" + text + "\n");
                        }
                    }
                }
                 */
 
                XmlDocument xmlDocument2 = new XmlDocument();
                xmlDocument2.LoadXml(text);
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(urlService);
                httpWebRequest.Headers.Add("SOAPAction", value);
                httpWebRequest.ContentType = "text/xml;charset=\"utf-8\"";
                httpWebRequest.Accept = "text/xml";
                httpWebRequest.Method = "POST";
                if (hostProxy != "")
                {
                    httpWebRequest.Proxy = new WebProxy(hostProxy, portProxy);
                    httpWebRequest.Proxy.Credentials = new NetworkCredential(userProxy, passProxy);
                }
                using (Stream requestStream = httpWebRequest.GetRequestStream())
                {
                    xmlDocument2.Save(requestStream);
                }
                using (WebResponse response = httpWebRequest.GetResponse())
                {
                    string text2;
                    using (StreamReader streamReader2 = new StreamReader(response.GetResponseStream()))
                    {
                        text2 = streamReader2.ReadToEnd();
                    }
                    /*
                    if (this.debugMode && this.logFilePath != "")
                    {
                        using (FileStream fileStream = new FileStream(this.logFilePath, FileMode.Append, FileAccess.Write))
                        {
                            using (StreamWriter streamWriter = new StreamWriter(fileStream))
                            {
                                streamWriter.WriteLine("\nSoap Response Timbrado:\n" + text2 + "\n");
                            }
                        }
                    }
                     */ 
                    try
                    {
                        XmlDocument xmlDocument3 = new XmlDocument();
                        xmlDocument3.LoadXml(text2);
                        xmlB64 = xmlDocument3.GetElementsByTagName("xml").Item(0).InnerText.ToString();
                        XmlDocument xmlDocument4 = new XmlDocument();
                        byte[] bytes = Convert.FromBase64String(xmlB64);
                        @string = Encoding.UTF8.GetString(bytes);
                        xmlDocument4.LoadXml(@string);
                        XmlNode xmlNode = xmlDocument4.DocumentElement.GetElementsByTagName("tfd:TimbreFiscalDigital").Item(0);
                        @salida = xmlNode.OuterXml.ToString();
                        uuid = xmlNode.Attributes.GetNamedItem("UUID").InnerText.ToString();
                        if (generarPdf=="true")
                        {
                            try
                            {
                                pdfB64 = xmlDocument3.GetElementsByTagName("pdf").Item(0).InnerText.ToString();
                            }
                            catch (Exception ex2)
                            {
                                pdfB64 = "";
                            }
                        }
                        if (generarTxt=="true")
                        {
                            try
                            {
                                txtB64 = xmlDocument3.GetElementsByTagName("txt").Item(0).InnerText.ToString();
                            }
                            catch (Exception ex2)
                            {
                                txtB64 = "";
                            }
                        }
                        if (generarCbb=="true")
                        {
                            try
                            {
                                cbbB64 = xmlDocument3.GetElementsByTagName("png").Item(0).InnerText.ToString();
                            }
                            catch (Exception ex2)
                            {
                                cbbB64 = "";
                            }
                        }
                    }
                    catch (Exception ex2)
                    {
                        errorMessage = text2;
                        errorCode = " exp-unKnow-001-No se logró leer la respuesta";
                        success = false;
                    }
                }
            }
            catch (WebException ex)
            {
                try
                {
                    WebResponse response2 = ex.Response;
                    using (Stream responseStream = response2.GetResponseStream())
                    {
                        StreamReader streamReader3 = new StreamReader(responseStream);
                        string text2 = streamReader3.ReadToEnd();
                        try
                        {
                            xmlDocument.LoadXml(text2);
                            XmlElement xmlElement = (XmlElement)xmlDocument.GetElementsByTagName("faultcode").Item(0);
                            errorCode = xmlElement.InnerText;
                            XmlElement xmlElement2 = (XmlElement)xmlDocument.GetElementsByTagName("faultstring").Item(0);
                            errorMessage = xmlElement2.InnerText;
                        }
                        catch (Exception ex2)
                        {
                            errorMessage = text2;
                            errorCode = " exp-unKnow-001-No se logró leer la respuesta";
                        }
                        success = false;
                    }
                }
                catch (Exception ex2)
                {
                    errorMessage = "Excepcion: " + ex2.Message;
                    errorCode = "exp-unKnow-002";
                    success = false;
                }
            }
            catch (Exception ex3)
            {
                errorMessage = "Excepcion: " + ex3.Message;
                errorCode = "ex-unKnow-003";
                success = false;
            }
            return success;
        }


        // ConnectionWSFM.ConnectionFM
        private string createSoapTimbrado(string urlService, string userConnectionPass, string userConnectionId, string emisorRfc, string text2Cfdi, string generarTxt, string generarPdf, string generarCbb)
        {
            return string.Concat(new object[]
	{
		"<?xml version=\"1.0\" encoding=\"UTF-8\"?><SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:ns1=\"",
		urlService,
		"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\"><SOAP-ENV:Body><ns1:requestTimbrarCFDI><request xsi:type=\"SOAP-ENC:Struct\"><UserPass xsi:type=\"xsd:string\">",
		userConnectionPass,
		"</UserPass><UserID xsi:type=\"xsd:string\">",
		userConnectionId,
		"</UserID><emisorRFC xsi:type=\"xsd:string\">",
		emisorRfc,
		"</emisorRFC><text2CFDI xsi:type=\"xsd:string\">",
		text2Cfdi,
		"</text2CFDI><generarTXT xsi:type=\"xsd:boolean\">",
		generarTxt,
		"</generarTXT><generarPDF xsi:type=\"xsd:boolean\">",
		generarPdf,
		"</generarPDF><generarCBB xsi:type=\"xsd:boolean\">",
		generarCbb,
		"</generarCBB></request></ns1:requestTimbrarCFDI></SOAP-ENV:Body></SOAP-ENV:Envelope>"
	});
        }

        private string createSoapValidacion(string urlService, string xml)
        {
            return string.Concat(new object[]
	{
		"<?xml version=\"1.0\" encoding=\"UTF-8\"?><SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:ns1=\"",
		urlService,
		"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\"><SOAP-ENV:Body><ns1:validarCFDI><xml xsi:type=\"xsd:anyType\">",
		xml,
		"</xml></ns1:validarCFDI></SOAP-ENV:Body></SOAP-ENV:Envelope>"
	});
        }

     

        /// <summary>
        /// Método para timbrar CFDi
        /// </summary>
        /// <param name="comprobante">el string en UTF-8 del cfdi</param>
        /// <returns>el string en UTF-8 con el comprobante timbrado</returns>
        public string TimbraCfdi(string comprobante)
        {
            NtLinkService.CertificadorClient cliente = new CertificadorClient();
            try
            {
                return cliente.TimbraCfdi(comprobante);
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }
           
        }

        public string ValidaTimbraCfdi(string comprobante)
        {
       try{
           string userProxy = "";
			string passProxy = "";
			string hostProxy = "";
			int portProxy = 80;
			
           string value = "validarCFDI";
            string urlService = ConfigurationManager.AppSettings["RutaClienteWS"];
          
            string text2Cfdi = Convert.ToBase64String(Encoding.UTF8.GetBytes(comprobante));
            string text = createSoapValidacion(urlService, text2Cfdi);

                 XmlDocument xmlDocument2 = new XmlDocument();
                xmlDocument2.LoadXml(text);
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(urlService);
                httpWebRequest.Headers.Add("SOAPAction", value);
                httpWebRequest.ContentType = "text/xml;charset=\"utf-8\"";
                httpWebRequest.Accept = "text/xml";
                httpWebRequest.Method = "POST";
                if (hostProxy != "")
                {
                    httpWebRequest.Proxy = new WebProxy(hostProxy, portProxy);
                    httpWebRequest.Proxy.Credentials = new NetworkCredential(userProxy, passProxy);
                }
                using (Stream requestStream = httpWebRequest.GetRequestStream())
                {
                    xmlDocument2.Save(requestStream);
                }
                using (WebResponse response = httpWebRequest.GetResponse())
                {
                    string text2;
                    using (StreamReader streamReader2 = new StreamReader(response.GetResponseStream()))
                    {
                        text2 = streamReader2.ReadToEnd();
                    }
                    try
                    {
                        XmlDocument xmlDocument3 = new XmlDocument();
                        xmlDocument3.LoadXml(text2);
                        var xxx = xmlDocument3.InnerXml;
                        int index = xxx.IndexOf("<item><key xsi:type=\"xsd:string\">detalles_validacion</key>");
                        string zx = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><SOAP-ENV:Envelope SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:ns2=\"http://xml.apache.org/xml-soap\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:ns1=\"https://t1demo.facturacionmoderna.com/timbrado/soap\" xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\"><SOAP-ENV:Body><ns1:validarCFDIResponse><return xsi:type=\"ns2:Map\">";

                        string z = xxx.Substring(index, (xxx.Count() - index));
                        zx = zx + z;

                        XmlDocument xmlDocument4 = new XmlDocument();
                        xmlDocument4.LoadXml(zx);
                        XmlNamespaceManager namespaces = new XmlNamespaceManager(xmlDocument4.NameTable);
                        namespaces.AddNamespace("ns1", @"https://t1demo.facturacionmoderna.com/timbrado/soap");
                        namespaces.AddNamespace("ns2", @"http://xml.apache.org/xml-soap");
                        var xnList = xmlDocument4.SelectNodes("//ns1:validarCFDIResponse/return/item/value/item", namespaces);
                        int i = 0;
                        string error = "";
                        foreach (XmlNode xn in xnList)
                        {

                            //var s = xn.SelectNodes("//ns1:validarCFDIResponse/return/item/value/item/value/item", namespaces);
                            var estatus = xn["value"].FirstChild.InnerText;
                            var mensaje = xn["value"].LastChild.InnerText;

                            if (estatus != "typeok" && i<4)
                            {
                                if (error != "")
                                    error = error + " - " + mensaje.Replace("message", "");
                                else
                                    error = error + mensaje.Replace("message", "");

                            }
                            i++;
                        }
                        if (string.IsNullOrEmpty(error))
                            return "OK";
                        else
                            return error;
                    }
                    catch (Exception ex2)
                    {
                        errorMessage = text2;
                        errorCode = " exp-unKnow-001-No se logró leer la respuesta";
                        return text2;
                    }
                }
 
         }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }
        }
       /* public string ValidaTimbraCfdi(string comprobante)
        {
            NtLinkService.CertificadorClient cliente = new CertificadorClient();
            try
            {
                return cliente.ValidaTimbraCfdi(comprobante);
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }

        }
        */ 
        public string ValidaTimbraRFC(string RFC)
        {
            NtLinkService.CertificadorClient cliente = new CertificadorClient();
            try
            {
                return cliente.ValidaRFC(RFC);
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }

        }
        /// <summary>
        /// Método
        /// </summary>
        /// <param name="uuid">UUID del comprobante que se va a cancelar</param>
        /// <param name="rfc">RFC del emisor</param>
        /// <returns>Regresa el acuse de cancelación</returns>
        public string CancelaCfdi(string uuid, string rfcEmisor,string expresion,string rfcReceptor)
        {
            CertificadorClient cliente = new CertificadorClient();
            try
            {

                return cliente.CancelaCfdi(uuid, rfcEmisor, expresion, rfcReceptor);

            }

            catch (FaultException ee)
            {
                Logger.Error(ee.Message);
                return ee.Message;
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }

        }

        public bool cancelarCfdi(string uuid, string emisorRfc, string motivo,string FolioSustituto)
        {
            bool success = true;
			
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                string urlService = ConfigurationManager.AppSettings["RutaClienteWS"];
                string userConnectionId = ConfigurationManager.AppSettings["UserNameWS"];
                string userConnectionPass = ConfigurationManager.AppSettings["UserPasWS"];
   		
                string userProxy = "";
                string passProxy = "";
                string hostProxy = "";
                int portProxy = 80;
               
		
                string value = "requestCancelarCFDI";
                string text = this.createSoapCancelado(uuid,motivo,FolioSustituto, emisorRfc,urlService,userConnectionPass,userConnectionId);
                  /*
                if (this.debugMode && this.logFilePath != "")
                {
                    using (FileStream fileStream = new FileStream(this.logFilePath, FileMode.Append, FileAccess.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(fileStream))
                        {
                            streamWriter.WriteLine("\nSoap Request Cancelado:\n" + text + "\n");
                        }
                    }
                }
                   */ 
                XmlDocument xmlDocument2 = new XmlDocument();
                xmlDocument2.LoadXml(text);
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(urlService);
                httpWebRequest.Headers.Add("SOAPAction", value);
                httpWebRequest.ContentType = "text/xml;charset=\"utf-8\"";
                httpWebRequest.Accept = "text/xml";
                httpWebRequest.Method = "POST";
                if (hostProxy != "")
                {
                    httpWebRequest.Proxy = new WebProxy(hostProxy, portProxy);
                    httpWebRequest.Proxy.Credentials = new NetworkCredential(userProxy, passProxy);
                }
                using (Stream requestStream = httpWebRequest.GetRequestStream())
                {
                    xmlDocument2.Save(requestStream);
                }
                using (WebResponse response = httpWebRequest.GetResponse())
                {
                    string text2;
                    using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        text2 = streamReader.ReadToEnd();
                    }
                    /*
                    if (this.debugMode && this.logFilePath != "")
                    {
                        using (FileStream fileStream = new FileStream(this.logFilePath, FileMode.Append, FileAccess.Write))
                        {
                            using (StreamWriter streamWriter = new StreamWriter(fileStream))
                            {
                                streamWriter.WriteLine("\nSoap Response Cancelado:\n" + text2 + "\n");
                            }
                        }
                    }
                     */ 
                    try
                    {
                        XmlDocument xmlDocument3 = new XmlDocument();
                        xmlDocument3.LoadXml(text2);
                        successCode = xmlDocument3.GetElementsByTagName("Code").Item(0).InnerText.ToString();
                        successMessage = xmlDocument3.GetElementsByTagName("Message").Item(0).InnerText.ToString();

                    }
                    catch (Exception ex2)
                    {
                        this.errorMessage = text2;
                        this.errorCode = " exp-unKnow-001-No se logró leer la respuesta";
                        success = false;
                    }
                }
            }
            catch (WebException ex)
            {
                try
                {
                    WebResponse response2 = ex.Response;
                    using (Stream responseStream = response2.GetResponseStream())
                    {
                        StreamReader streamReader2 = new StreamReader(responseStream);
                        string text2 = streamReader2.ReadToEnd();
                        try
                        {
                            xmlDocument.LoadXml(text2);
                            XmlElement xmlElement = (XmlElement)xmlDocument.GetElementsByTagName("faultcode").Item(0);
                            this.errorCode = xmlElement.InnerText;
                            XmlElement xmlElement2 = (XmlElement)xmlDocument.GetElementsByTagName("faultstring").Item(0);
                            this.errorMessage = xmlElement2.InnerText;
                        }
                        catch (Exception ex2)
                        {
                            this.errorMessage = text2;
                            this.errorCode = " exp-unKnow-001-No se logró leer la respuesta";
                        }
                        success = false;
                    }
                }
                catch (Exception ex2)
                {
                    this.errorMessage = "Excepcion: " + ex2.Message;
                    this.errorCode = "exp-unKnow-002";
                    success = false;
                }
            }
            catch (Exception ex3)
            {
                this.errorMessage = "Excepcion: " + ex3.Message;
                this.errorCode = "ex-unKnow-003";
                success = false;
            }
            return success;
        }


        private string createSoapCancelado(string uuid, string motivo ,string folioSustituto, string emisorRfc, string urlService, string userConnectionPass, string userConnectionId)
        {
            return string.Concat(new string[]
			{
				"<?xml version=\"1.0\" encoding=\"UTF-8\"?><SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:ns1=\"",
				urlService,
				"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\"><SOAP-ENV:Body><ns1:requestCancelarCFDI><request xsi:type=\"SOAP-ENC:Struct\"><UserPass xsi:type=\"xsd:string\">",
				userConnectionPass,
				"</UserPass><UserID xsi:type=\"xsd:string\">",
				userConnectionId,
				"</UserID><emisorRFC xsi:type=\"xsd:string\">",
				emisorRfc,
				"</emisorRFC><uuid xsi:type=\"xsd:string\">",
				uuid,
                "</uuid><Motivo xsi:type=\"xsd:string\">",
                    motivo,
                 "</Motivo> <FolioSustitucion xsi:type=\"xsd:string\">",
                    folioSustituto,
				"</FolioSustitucion></request></ns1:requestCancelarCFDI></SOAP-ENV:Body></SOAP-ENV:Envelope>"
			});
        }
        private string createSoapConsultaEstausCFDI(string uuid, string emisorRfc, string receptorRfc,string total,string urlService, string userConnectionPass, string userConnectionId)
        {
            return string.Concat(new string[]
            {
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?><SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:ns1=\"",
                urlService,
                "\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\"><SOAP-ENV:Body><ns1:consultarEstatusCFDI><request xsi:type=\"SOAP-ENC:Struct\"><UserPass xsi:type=\"xsd:string\">",
                userConnectionPass,
                "</UserPass><UserID xsi:type=\"xsd:string\">",
                userConnectionId,
                "</UserID><emisorRFC xsi:type=\"xsd:string\">",
                emisorRfc,
                "</emisorRFC><receptorRFC xsi:type=\"xsd:string\">",
                  receptorRfc,
                "</receptorRFC><total xsi:type=\"xsd:string\">",
                total,
                "</total><UUID xsi:type=\"xsd:string\">",
                uuid,
                "</UUID></request></ns1:consultarEstatusCFDI></SOAP-ENV:Body></SOAP-ENV:Envelope>"
            });
        }


        public string ConsultaEstatusCfdi(string cadena)
        {

            CertificadorClient cliente = new CertificadorClient();
            try
            {

                return cliente.ConsultaEstatusCFDI(cadena);

            }

            catch (FaultException ee)
            {
                Logger.Error(ee.Message);
                return ee.Message;
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }

        }


        public bool ConsultaEstatusCFDI(string uuid, string rfcEmisor, string rfcReceptor, string total)
        {
            bool success = true;
            XmlDocument xmlDocument = new XmlDocument();

            try
            {
                 string urlServiceC = ConfigurationManager.AppSettings["RutaClienteWS"];
                string userConnectionIdC = ConfigurationManager.AppSettings["UserNameWS"];
                string userConnectionPassC = ConfigurationManager.AppSettings["UserPasWS"];

                string userProxy = "";
                string passProxy = "";
                string hostProxy = "";
                int portProxy = 80;

                string value = "consultarEstatusCFDI";
                string text = this.createSoapConsultaEstausCFDI(uuid, rfcEmisor, rfcReceptor, total, urlServiceC, userConnectionPassC, userConnectionIdC);
                XmlDocument xmlDocument2 = new XmlDocument();
                xmlDocument2.LoadXml(text);
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(urlServiceC);
                httpWebRequest.Headers.Add("SOAPAction", value);
                httpWebRequest.ContentType = "text/xml;charset=\"utf-8\"";
                httpWebRequest.Accept = "text/xml";
                httpWebRequest.Method = "POST";

                if (hostProxy != "")
                {
                    httpWebRequest.Proxy = new WebProxy(hostProxy, portProxy);
                    httpWebRequest.Proxy.Credentials = new NetworkCredential(userProxy, passProxy);
                }
                using (Stream requestStream = httpWebRequest.GetRequestStream())
                {
                    xmlDocument2.Save(requestStream);
                }
                using (WebResponse response = httpWebRequest.GetResponse())
                {
                    string text2;
                    using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        text2 = streamReader.ReadToEnd();
                    }
                 
                    try
                    {
                        string estado = "";string esCancelable = "";string estatusCancelacion = "";string EstatusFM = "";
                        XmlDocument xmlDocument3 = new XmlDocument();
                        xmlDocument3.LoadXml(text2);
                        successCode = xmlDocument3.GetElementsByTagName("http_code").Item(0).InnerText.ToString();
                        estado = xmlDocument3.GetElementsByTagName("estado").Item(0).InnerText.ToString();
                        esCancelable = xmlDocument3.GetElementsByTagName("esCancelable").Item(0).InnerText.ToString();
                        estatusCancelacion = xmlDocument3.GetElementsByTagName("estatusCancelacion").Item(0).InnerText.ToString();
                        if(string.IsNullOrEmpty(estatusCancelacion))
                        estatusCancelacion = xmlDocument3.GetElementsByTagName("EstatusFM").Item(0).InnerText.ToString();

                        successMessage = estado + "|" + esCancelable + "|" + estatusCancelacion;
                        successCode = ValidarErrores(Convert.ToInt16(successCode));
                    }
                    catch (Exception ex2)
                    {
                        this.errorMessage = text2;
                        this.errorCode = "001-No se logró leer la respuesta";
                        success = false;
                    }
                }
            }
            catch (WebException ex)
            {
                try
                {
                    WebResponse response2 = ex.Response;
                    using (Stream responseStream = response2.GetResponseStream())
                    {
                        StreamReader streamReader2 = new StreamReader(responseStream);
                        string text2 = streamReader2.ReadToEnd();
                        try
                        {
                            xmlDocument.LoadXml(text2);
                            XmlElement xmlElement = (XmlElement)xmlDocument.GetElementsByTagName("faultcode").Item(0);
                            this.errorCode = xmlElement.InnerText;
                            XmlElement xmlElement2 = (XmlElement)xmlDocument.GetElementsByTagName("faultstring").Item(0);
                            this.errorMessage = xmlElement2.InnerText;
                        }
                        catch (Exception ex2)
                        {
                            this.errorMessage = text2;
                            this.errorCode = " exp-unKnow-001-No se logró leer la respuesta";
                        }
                        success = false;
                    }
                }
                catch (Exception ex2)
                {
                    this.errorMessage = "Excepcion: " + ex2.Message;
                    this.errorCode = "exp-unKnow-002";
                    success = false;
                }
            }

            return success;
        }

        public static IDictionary<int, string> Errores {
            get {
                var resultado = new Dictionary<int, string>
                {
                                     {200, "Peticion correcta"},
                                     {400, "Error al consultar, verificar informacion enviada a consulta"},
                                     {404, "No encontrado"},

                };

                return resultado;
            }
        }
        private string ValidarErrores(int x)
        {
            try
            {
                var s = Errores[x];

                return s;
            }
            catch (Exception ex)
            { return "Error en pac, verifique con el administrador"; }
        }


    }
}
