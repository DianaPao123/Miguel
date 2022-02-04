using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Contract
{

    public partial class vfacturas
    {

         public decimal SubTotalPago { get; set; }
        public decimal IVAPago { get; set; }
        public string EstusCFDI { get; set; }
    }

}