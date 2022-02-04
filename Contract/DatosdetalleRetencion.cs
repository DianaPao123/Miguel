using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Contract
{
       [Serializable()]
 
   public class DatosdetalleRetencion
    {
        [DataMemberAttribute]
        public decimal Base { get; set; }
        [DataMemberAttribute]
        public string Impuesto { get; set; }
        [DataMemberAttribute]
        public string TipoFactor { get; set; }
        [DataMemberAttribute]
        public string TasaOCuota { get; set; }
        [DataMemberAttribute]
        public decimal Importe { get; set; }
    }
}
