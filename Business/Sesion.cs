using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
  public  class Sesion
    {
        public string Nombre {get;set;}
        public System.Guid Id { get; set; }
        public string Rol { get; set; }
        public string Usuario { get; set; }
        public string ApellidoM { get; set; }
        public string ApellidoP { get; set; }

    }
}
