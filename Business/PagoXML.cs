﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Contract.Complemento;
using System.IO;
using System.Xml.Serialization;

namespace Business
{
   public class PagoXML
    {
        //------------------------------------------
        private readonly XNamespace _ns7 = "http://www.sat.gob.mx/Pagos";
        private readonly XNamespace _ns = "http://www.sat.gob.mx/cfd/3";

        public Pagos DesSerializarPagos(string comprobante)
        {
            string _byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            if (comprobante.StartsWith(_byteOrderMarkUtf8))
            {
                comprobante = comprobante.Remove(0, _byteOrderMarkUtf8.Length);
            } 

            XElement element = XElement.Load((new StringReader(comprobante)));

            var ImpL = element.Elements(_ns + "Complemento");
            if (ImpL != null)
            {
                var pag = ImpL.Elements(_ns7 + "Pagos");

                foreach (XElement e in pag)
                {

                    var ser = new XmlSerializer(typeof(Pagos));
                    string xml = e.ToString();
                    var reader = new StringReader(xml);
                    var comLXMLComprobante = (Pagos)ser.Deserialize(reader);
                    return comLXMLComprobante;
                }
                return null;
            }
            else

                return null;

        }
        //------------------------------------------

    }
}
