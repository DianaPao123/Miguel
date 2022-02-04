using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Contract
{
      [Serializable()]
   public class DatosDevoluciones
    {
      [DataMemberAttribute]
       public decimal totalEmpresa { get; set; }
      // [DataMemberAttribute]
      // public decimal totalCliente { get; set; }
       [DataMemberAttribute]
       public decimal totalPromotor { get; set; }
       [DataMemberAttribute]
       public decimal totalContacto { get; set; }
       public List<DatosDevolucionesClientes> cliente { get; set; }
       public List<string> idPrefacturas { get; set; }
    }
      [Serializable()]
      public class DatosDevolucionesClientes
      {
          [DataMemberAttribute]
          public decimal totalCliente { get; set; }
          [DataMemberAttribute]
          public string nombre { get; set; }
          [DataMemberAttribute]
          public string idCliente { get; set; }
      
      }

}
