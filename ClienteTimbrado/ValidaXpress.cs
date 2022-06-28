using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteNtLink
{
  

    public class Detail
    {
        public List<Detail> detail { get; set; }
        public string section { get; set; }
        public string message { get; set; }
        public string messageDetail { get; set; }
        public int type { get; set; }
        public string typeValue { get; set; }
    }

    public class ValidaXpress
    {
        public List<Detail> detail { get; set; }
        public string cadenaOriginalSAT { get; set; }
        public string cadenaOriginalComprobante { get; set; }
        public string uuid { get; set; }
        public string statusSat { get; set; }
        public string statusCodeSat { get; set; }
        public string status { get; set; }
    }

}
