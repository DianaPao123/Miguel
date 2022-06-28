using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualizadorCancelacion
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            Actualizador A = new Actualizador();
            A.LecturaBase();
        }
    }
}
