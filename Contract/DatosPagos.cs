using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Contract
{
    [Serializable()]
    public class DatosPagos
    {
        [DataMemberAttribute]
        public DateTime Fecha { get; set; }
      
        [DataMemberAttribute]
        public decimal Monto { get; set; }
        [DataMemberAttribute]
        public string MetododePago { get; set; }
        [DataMemberAttribute]
        public string Descripcion { get; set; }
        [DataMemberAttribute]
        public string Banco { get; set; }
        [DataMemberAttribute]
        public string ClaveBancaria { get; set; }
        [DataMemberAttribute]
        public string Tipo { get; set; }
        [DataMemberAttribute]
        public string Para { get; set; }
        [DataMemberAttribute]
        public string beneficiario { get; set; }
        [DataMemberAttribute]
        public string Cliente { get; set; }
       

    }
}
