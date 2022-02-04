using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contract;
using System.ServiceModel;

namespace Business
{
    public class GAFSucursales : GAFBusiness
    {
        public Sucursales GetSucursal(long idSucursal)
        {

            try
            {
                using (var db = new GAFEntities())
                {
                    var com = db.Sucursales.Where(c => c.IdSucursal == idSucursal);
                    return com.FirstOrDefault();
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }

        }


        private bool Validar(Sucursales e)
        {
            //TODO: Validar los campos requeridos y generar excepcion
            {
                if (string.IsNullOrEmpty(e.Nombre))
                {
                    throw new FaultException("El nombre es obligatorio");
                }
                if (string.IsNullOrEmpty(e.LugarExpedicion))
                {
                    throw new FaultException("El lugar de expedición es obligatorio");
                }

            }
            return true;
        }


        public bool SaveSucursal(Sucursales sucursal)
        {

            try
            {
                if (Validar(sucursal))
                {
                    using (var db = new GAFEntities())
                    {
                        if (sucursal.IdSucursal == 0)
                        {
                            db.Sucursales.AddObject(sucursal);
                        }
                        else
                        {
                            var y = db.Sucursales.Where(p => p.IdSucursal == sucursal.IdSucursal).FirstOrDefault();
                            db.Sucursales.ApplyCurrentValues(sucursal);
                        }
                        db.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch (FaultException fe)
            {
                throw;
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return false;
            }
        }

        public List<Sucursales> GetSucursalLista(int idEmpresa)
        {

            try
            {
                using (var db = new GAFEntities())
                {
                    var com = db.Sucursales.Where(c => c.IdEmpresa == idEmpresa);
                    return com.ToList();
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }

        }
        public Sucursales GetSucursal(int idEmpresa)
        {

            try
            {
                using (var db = new GAFEntities())
                {
                    var com = db.Sucursales.Where(c => c.IdEmpresa == idEmpresa);
                    return com.FirstOrDefault();
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }

        }
    }
}
