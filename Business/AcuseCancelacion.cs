using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Business
{
    public class AcuseCancelacion : GAFBusiness
    {
        public string SelloSat { get; set; }
        public string FechaCancelacion { get; set; }
        public string Status { get; set; }


        public static AcuseCancelacion Parse(string acuse)
        {
            AcuseCancelacion acuseCancelacion = new AcuseCancelacion();
            try
            {
                if (acuse == string.Empty)
                {
                    return null;
                }
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(acuse);
                acuseCancelacion.FechaCancelacion = xmlDoc.DocumentElement.Attributes["Fecha"].Value;
                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                {
                    if (node.LocalName == "Folios")
                    {
                        foreach (XmlNode node2 in node.ChildNodes)
                        {
                            if (node2.LocalName == "EstatusUUID")
                                acuseCancelacion.Status = node2.InnerText;
                        }
                    }

                    if (node.LocalName == "Signature")
                    {
                        foreach (XmlNode node2 in node.ChildNodes)
                        {
                            if (node2.LocalName == "SignatureValue")
                            {
                                acuseCancelacion.SelloSat = node2.InnerText;
                            }
                        }
                    }
                }
                return acuseCancelacion;
            }
            catch (Exception err)
            {
                Logger.Error("(TraerEstatusSAT) Error al intentar parsear el XML de respuesta SAT, Err:" + err);
                return null;
            }
        

        }
    }
}
