using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using System.IO;

using System.Data;
using Business;
using System.Configuration;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Text;
using Contract;
using Business.CFDI40;

namespace GAFWEB
{
    public partial class Carga : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubir_Click(object sender, EventArgs e)
        {
            if (archivoPagos.HasFile)//por si hay archivo
            {
                var sesion = Session["sessionRGV"] as Sesion;

                string nombreArchivo = archivoPagos.FileName;
                var archivo = this.archivoPagos.FileBytes;
                 {
                    string comprobante = Encoding.UTF8.GetString(archivo);
                    comprobante = comprobante.Substring(comprobante.IndexOf(Convert.ToChar(60)));
                    comprobante = comprobante.Substring(0, (comprobante.LastIndexOf(Convert.ToChar(62)) + 1));
                    XElement element = XElement.Load((new StringReader(comprobante)));
                     Comprobante comp = DesSerializar(element);
                     Factura F = new Factura();
                     string UUDI=""; 
                     foreach (var pe in comp.Complemento.Any)
                     {
                         if (pe.Name == "tfd:TimbreFiscalDigital")
                         {
                             var UDI = pe.GetAttribute("UUID").ToString();
                             UUDI = UDI;
                         }
                     }
                     var fa= F.GetByComprobante(UUDI);
                     if (fa == null)
                     {
                         GAFFactura fac = new GAFFactura();
                                              
                        var x= fac.GuardarFacturaXML(comp,sesion.Id,UUDI);
                         if (x==true)
                             lblError.Text = "El archivo guardado correctamente";
                         else
                             lblError.Text = "El archivo no se guardo";
                     }
                     else
                            lblError.Text = "El archivo ya existe";
                  
                }
            }

        }
        //------------------------------------------------------------------------
        private Comprobante DesSerializar(XElement element)
        {

            var ser = new XmlSerializer(typeof(Comprobante));
            string xml = element.ToString();
            var reader = new StringReader(xml);
            var comLXMLComprobante = (Comprobante)ser.Deserialize(reader);
            return comLXMLComprobante;
        }
        //---------------------------------------
        protected string guardarArchivo(byte[] archivo, string nombre)
        {
            try
            {
                string ruta = ConfigurationManager.AppSettings["rutaExcel"] + "_" + DateTime.Now.ToFileTime();
                ViewState["rutaExcel"] = ruta+"\\"+nombre;
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