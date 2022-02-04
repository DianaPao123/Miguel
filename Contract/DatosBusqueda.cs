using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Contract
{
    [Serializable()]
    public  class DatosBusqueda
    {
        [DataMemberAttribute]
        public string Empresa { get; set; }
        [DataMemberAttribute]
        public string FechaInicial { get; set; }
        [DataMemberAttribute]
        public string FechaFinal { get; set; }
        [DataMemberAttribute]
        public string Cliente { get; set; }
        [DataMemberAttribute]
        public string Promotor { get; set; }
        [DataMemberAttribute]
        public string Status { get; set; }
    }
}
