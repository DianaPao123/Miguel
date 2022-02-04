using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contract;
using System.ServiceModel;

namespace Business
{
    public class Usuarios : GAFBusiness
    {

        //------------------------------------------------------------------------
        public bool Save(Usuario u)
        {
            int m = 1;
            try
            {
                using (var db = new GAFEntities())
                {
                    

                            db.Usuario.AddObject(u);
                            db.SaveChanges();
                    
                        return true;
                   
                }
            }
            catch (ApplicationException ae)
            {
                throw new FaultException(ae.Message);
            }
            catch (FaultException fe)
            {
                throw;
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException.Message);
                return false;
            }
        }
        //---------------------------------------------
        public Usuario GetUsuario(System.Guid ID)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    var x = db.Usuario.Where(p => p.idUsuario == ID).FirstOrDefault();
                    return x;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }
        }
        //--------------------------------------------
        //------------------------------------------------------------------------
        public bool Actualizar(Usuario usu)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    Usuario u = new Usuario();
                    var x = db.Usuario.FirstOrDefault(p => p.idUsuario == usu.idUsuario);
                    u.Nombre = usu.Nombre;
                    u.ApellidoP = usu.ApellidoP;
                    u.ApellidoM = usu.ApellidoM;
                    u.Activo = true;
                    u.Email = usu.Email;
                    u.Usuario1 = usu.Usuario1;
                    u.Contraseña = usu.Contraseña;
                    u.idUsuario = u.idUsuario;
                    u.id = x.id;
                    u.idUsuario = usu.idUsuario;
                    u.Rol = x.Rol;
                    db.Usuario.ApplyCurrentValues(u);
                    db.SaveChanges();

                    return true;

                }
            }
            catch (ApplicationException ae)
            {
                throw new FaultException(ae.Message);
            }
            catch (FaultException fe)
            {
                throw;
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException.Message);
                return false;
            }
        }
        //-------------------------------------------

    }
}
