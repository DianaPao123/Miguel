using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contract;

namespace Business
{
    public static class Autentificacion
    
    {
        public static Usuario Autenticar(string usuario, string password)
      {
          try
          {
              using (var db = new GAFEntities())
              {
                  var U= db.Usuario.Where(p => p.Usuario1==usuario && p.Contraseña==password).First();
                  if (U != null)
                      return U;
                  else
                      return null;
              }

          }
          catch (Exception ee)
          {
             
              return null;
          }

      }
        public static Usuario AutenticarPromotores(string usuario, string password)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    var U = db.Usuario.Where(p => p.Usuario1 == usuario && p.Contraseña == password && (p.Rol == "Promotor" || p.Rol == "PromotorEspecial")).First();
                    if (U != null)
                        return U;
                    else
                        return null;
                }

            }
            catch (Exception ee)
            {

                return null;
            }

        }
        public static Usuario AutenticarOperador(string usuario, string password)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    var U = db.Usuario.Where(p => p.Usuario1 == usuario && p.Contraseña == password && (p.Rol == "Operador" || p.Rol == "SuperAdmin" || p.Rol == "Consulta"/* || p.Rol == "OperadorAdmin"*/)).First();
                    if (U != null)
                        return U;
                    else
                        return null;
                }

            }
            catch (Exception ee)
            {

                return null;
            }

        }
        public static Usuario AutenticarContabilidad(string usuario, string password)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    var U = db.Usuario.Where(p => p.Usuario1 == usuario && p.Contraseña == password && (p.Rol == "Contabilidad" || p.Rol == "ConsultaContabilidad")).First();
                    if (U != null)
                        return U;
                    else
                        return null;
                }

            }
            catch (Exception ee)
            {

                return null;
            }

        }
        public static Usuario AutenticarTesoreria(string usuario, string password)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    var U = db.Usuario.Where(p => p.Usuario1 == usuario && p.Contraseña == password &&( p.Rol == "Tesoreria"||p.Rol == "AdminTesoreria")).First();
                    if (U != null)
                        return U;
                    else
                        return null;
                }

            }
            catch (Exception ee)
            {

                return null;
            }

        }
    }
}
