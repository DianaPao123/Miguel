using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;


namespace Contract
{
    [Serializable()]
    
    public class Pagos
    {
        public List<PagoDoctoRelacionado> DoctoRelacionado;

       // public PagosPagoImpuestos[] impuestosField;

        [DataMemberAttribute]
        public string id { get; set; }

        [DataMemberAttribute]
        public string fechaPago { get; set; }

        [DataMemberAttribute]
        public string formaDePagoP { get; set; }

        [DataMemberAttribute]
        public string monedaP { get; set; }

        [DataMemberAttribute]
        public string tipoCambioP { get; set; }

        [DataMemberAttribute]
        public string monto { get; set; }

        [DataMemberAttribute]
        public string numOperacion { get; set; }

        [DataMemberAttribute]
        public string rfcEmisorCtaOrd { get; set; }

        [DataMemberAttribute]
        public string nomBancoOrdExt { get; set; }

        [DataMemberAttribute]
        public string ctaOrdenante { get; set; }

        [DataMemberAttribute]
        public string rfcEmisorCtaBen { get; set; }

        [DataMemberAttribute]
        public string ctaBeneficiario { get; set; }

        [DataMemberAttribute]
        public string tipoCadPago { get; set; }

        [DataMemberAttribute]
        public string certPago { get; set; }
        [DataMemberAttribute]
       
        public string cadPago { get; set; }
        [DataMemberAttribute]
       
        public string selloPago { get; set; }
         [XmlIgnore]

        public string rutaImagen { get; set; }

    }
}
