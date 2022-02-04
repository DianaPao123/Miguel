using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contract;
using System.ServiceModel;

namespace Business
{
    public class Movimientos : GAFBusiness
    {

        public long SaveMovimiento(MovimientosBancos movi)
        {

            try
            {

                using (var db = new GAFEntities())
                {
                    if (movi.idMovimiento == 0)
                    {
                        db.MovimientosBancos.AddObject(movi);
                    }
                    else
                    {
                        var y = db.MovimientosBancos.Where(p => p.idMovimiento == movi.idMovimiento).FirstOrDefault();
                        db.MovimientosBancos.ApplyCurrentValues(movi);
                    }
                    db.SaveChanges();
                    return movi.idMovimiento;
                }

            }
            catch (FaultException fe)
            {
                throw;
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return 0;
            }
        }


        public Empresa_CuentasBancos GetIdMovimientos(int idEmpresa, string numerocuenta)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    var res = db.Empresa_CuentasBancos.Where(p => p.idEmpresa == idEmpresa && p.numeroCuenta==numerocuenta ).FirstOrDefault();
                     return res;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }

        }


        public List<MovimientosBancos> GetMovimientos(Int64 idEmpresa)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    System.Web.HttpContext.Current.Session["MOB"] = idEmpresa;
                    var res = db.MovimientosBancos.Where(p => p.idNumeroCuentaOrigen == idEmpresa || p.idNumeroCuentaDestino == idEmpresa).OrderBy(p => p.fechaRegistro).ToList();
                    foreach (var r in res)
                    {
                        if (r.idNumeroCuentaOrigen == idEmpresa)
                            r.operacion = "cargo";
                        else
                            r.operacion = "abono";
                    }

                    return res;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }

        }
               

        public int SetActualizaSaldoMovimiento(Empresa_CuentasBancos M, decimal monto,string operacion)
        {
            try
            {
                Empresa_CuentasBancos d;
                using (var db = new GAFEntities())
                {
                    
                    d = db.Empresa_CuentasBancos.Where(p => p.idEmpresaCuenta == M.idEmpresaCuenta).First();
                    if (d.saldoActual == null)
                        d.saldoActual = 0;
                    if(operacion=="+")
                    d.saldoActual =d.saldoActual+ monto;
                    if (operacion == "-")
                    d.saldoActual = d.saldoActual - monto;
                    db.Empresa_CuentasBancos.ApplyCurrentValues(d);
                    db.SaveChanges();

                    return 1;
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return 0;
            }

        }
    }

  

}
