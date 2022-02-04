using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Contract
{
    [Serializable()]
    public class DatosParcialidad
    {
        [DataMemberAttribute]
        public string idPreFactura { get; set; }
        [DataMemberAttribute]
        public string PreFolio { get; set; }
        [DataMemberAttribute]
        public string SaldoAnteriorPago { get; set; }
        [DataMemberAttribute]
        public string MontoPagado { get; set; }
        [DataMemberAttribute]
        public string Parcialidad { get; set; }
      


    }
}