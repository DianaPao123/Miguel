using Business;
using Contract;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ActualizadorCancelacion
{

    public class Actualizador
    {
        protected static ILog Log = LogManager.GetLogger(typeof(Actualizador));

        public void LecturaBase()
        {
            try
            {

                //lectura a base de datos
                Log.Info("Obtener registros");
                List<facturas>ListaComp = ObtenerTimbres();

                if (ListaComp.Count() > 0)
                {
                    Log.Info("Se actualizaran " + ListaComp.Count() + " CFD's a Cancelar");
                    var fac = new GAFFactura();
                    var Empresa = new GAFEmpresa();
                    var cli = new GAFClientes();
                    string resultado = "";

                    foreach (var C in ListaComp)
                    {
                        var emp = Empresa.GetId((int)C.IdEmpresa);
                        clientes cliente = cli.GetCliente(C.idcliente);
                        //-------------------servicio----------
                        try
                        {
                            fac.ConsultaEstatusCFDIServicioSAT(C.Uid, emp.RFC, cliente.RFC, C.Total.ToString());
                        }
                        catch (Exception ex)
                        {
                            Log.Error("Error en servicio: " + ex);
                        }
                    }

                }
                else
                    Log.Info("No se encontraron registros");

            }
            catch (Exception ex)
            {
                Log.Error("(Lectura Base) Error de inicio: " + ex);
            }
        }

        private List<facturas> ObtenerTimbres()
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    return db.facturas.Where(p => p.Cancelado ==2).ToList();
                }

            }
            catch (Exception ee)
            {
                Log.Error(ee.Message);
                return null;
            }

        }

    /*
        private void CancelarFacturaGuardarNuevo(string uuid,string cancelado ,string acuse)
        {
            try
            {
                string EstatusCancelación = "";
                string[] status = acuse.Split('|');
                if (status.Count() > 2)
                    EstatusCancelación = status[2];


                using (var db = new GAFEntities())
                {
                    var fact = db.facturas.FirstOrDefault(p => p.Uid == uuid);
                    if (fact != null)
                    {
                        fact.Observaciones = acuse;
                        if(cancelado== "Cancelado" && (EstatusCancelación== "Cancelado sin aceptación"||
                            EstatusCancelación == "Cancelado con aceptación" ||
                                   EstatusCancelación == "Plazo Vencido"))
                        fact.Cancelado = 1;  //1:cancelado, 0:no cancelado, 2:precancelado,3:no cANCELADO
                        if (EstatusCancelación == "Solicitud Rechazada")
                            fact.Cancelado = 3;
                        db.facturas.ApplyCurrentValues(fact);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ee)
            {
                Log.Error(ee.Message);
                if (ee.InnerException != null)
                    Log.Error(ee.InnerException);
            }
        }
        private void CancelarFacturaGuardarNuevo(string uuid, string cancelado)
        {
            try
            {
              
                using (var db = new GAFEntities())
                {
                    var fact = db.facturas.FirstOrDefault(p => p.Uid == uuid);
                    if (fact != null)
                    {
                        fact.Observaciones = cancelado;
                        db.facturas.ApplyCurrentValues(fact);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ee)
            {
                Log.Error(ee.Message);
                if (ee.InnerException != null)
                    Log.Error(ee.InnerException);
            }
        }
        */

    }
}