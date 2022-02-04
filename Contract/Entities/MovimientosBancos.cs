using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Contract
{

    public partial class MovimientosBancos
    {

   //     public string operacion { get { return ((idNumeroCuentaOrigen.HasValue) ? "cargo" : "abono"); } }
        public string operacion { get; set; }
    
    }

}