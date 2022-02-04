using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contract
{
       [Serializable()]
 
  public  class facturaComplementos
    {
        public List<Pagos> pagos;
        public INE ine;
    }

}
