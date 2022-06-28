using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Contract
{
    [Serializable()]
   public class PagoDoctoRelacionado
    {
        public List<facturasdetalleRT> Impuestos;

        [DataMemberAttribute]
        public string ID { get; set; }
          
           [DataMemberAttribute]
           public string IdDocumento { get; set; }
           [DataMemberAttribute]
           public string Serie { get; set; }
           [DataMemberAttribute]
           public string Folio { get; set; }
           [DataMemberAttribute]
           public string MonedaDR { get; set; }
           [DataMemberAttribute]
           public string TipoCambioDR { get; set; }
           [DataMemberAttribute]
           public string MetodoDePagoDR { get; set; }
           [DataMemberAttribute]
           public string NumParcialidad { get; set; }
           [DataMemberAttribute]
           public string ImpSaldoAnt { get; set; }
           [DataMemberAttribute]
           public string ImpPagado { get; set; }
           [DataMemberAttribute]
           public string ImpSaldoInsoluto { get; set; }
        // nuevos
        [DataMemberAttribute]
        public string EquivalenciaDR { get; set; }
        [DataMemberAttribute]
        public string ObjetoImpDR { get; set; }
    }

}
