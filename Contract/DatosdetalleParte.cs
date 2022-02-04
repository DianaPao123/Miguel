using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Contract
{
    [Serializable()]
   public class DatosdetalleParte
    {

      public List<string> NumeroPedimento { get; set; }
       [DataMemberAttribute]
       public string ClaveProdServ { get; set; }
       [DataMemberAttribute]
       public string NoIdentificacion { get; set; }
       [DataMemberAttribute]
       public decimal Cantidad { get; set; }
       [DataMemberAttribute]
       public string Unidad { get; set; }
       [DataMemberAttribute]
       public string Descripcion { get; set; }
       [DataMemberAttribute]
       public decimal? ValorUnitario { get; set; }
       [DataMemberAttribute]
       public decimal? Importe { get; set; }

       
    }
}
