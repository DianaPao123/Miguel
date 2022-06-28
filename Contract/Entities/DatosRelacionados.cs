using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contract
{   [Serializable()]
   public class DatosRelacionados
    {
       // public List<string> uuid { get; set; }
        public string tipoRelacion { get; set; }
        public string uuid { get; set; }

        //public DatosRelacionados()
        //{
        //    uuid = new List<string>();
        //}
    }
[Serializable()]
public class DatosListaRelacionados
{
    public List<string> uuid { get; set; }
    public string tipoRelacion { get; set; }

    public DatosListaRelacionados()
    {
        uuid = new List<string>();
    }
}
}
     