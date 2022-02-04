using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Contract
{
    

    [Serializable()]
    public class DatosClientesEspecial
    {
        [DataMemberAttribute]
        public string nombre { get; set; }
        [DataMemberAttribute]
        public string rfc { get; set; }
  
    }
}
