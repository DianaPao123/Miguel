using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Contract
{
    [Serializable()]
  public class ImpLocales
    {

        public ImpLocales()
        {
            imp = new List<ImpuestosL>();
        }

       [DataMemberAttribute]
        public  List<ImpuestosL> imp { get; set; }
        
        [DataMemberAttribute]
        public string TotaldeTraslados { get; set; }//decimal
        [DataMemberAttribute]
        public string TotaldeRetenciones { get; set; }//decimal

    
       
    }
    [Serializable()]
  public class ImpuestosL
  {
      [DataMemberAttribute]
      public string ImpuestosLocales { get; set; }
      [DataMemberAttribute]
      public string ImpLoc { get; set; }
      [DataMemberAttribute]
      public string Tasa { get; set; }//decimal
      [DataMemberAttribute]
      public string Importe { get; set; }//decimal
  
  }
}
