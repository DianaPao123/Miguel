using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Contract
{
       [Serializable()]
 
   public class INE
    {
        [DataMemberAttribute]
        public string TipoProceso { get; set; }
        [DataMemberAttribute]
        public string TipoComite { get; set; }
        [DataMemberAttribute]
        public string IdContabilidad { get; set; }
        public List<INEEntidad> Entidad { get; set; }


    }
          [Serializable()]
 
       public class INEEntidad
       {
              public string ID { get; set; }
      
           [DataMemberAttribute]
              public string ClaveEntidad { get; set; }
           [DataMemberAttribute]
           public string Ambito { get; set; }
           [DataMemberAttribute]
           public List<string> IdContabilidad { get; set; }


       }
          [Serializable()]

          public class INEContabilidad
          {
              public string ID { get; set; }
              public string IdContabilidad { get; set; }

          }

}
