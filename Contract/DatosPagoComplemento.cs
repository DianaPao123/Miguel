using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Contract
{
        [Serializable()]
    public class DatosPagoComplemento
    {
            [DataMemberAttribute]
            public string id { get; set; }
            [DataMemberAttribute]
            public string monto { get; set; }
            [DataMemberAttribute]
            public string parcialidad { get; set; }
            [DataMemberAttribute]
            public string MontoPagado { get; set; }
            [DataMemberAttribute]
            public string MontoAnterior { get; set; }
            [DataMemberAttribute]
            public string SaldoInsoluto { get; set; }
    }
}
