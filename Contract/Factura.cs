using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Contract
{
    public class Factura : GAFContract
    {
        /*
        public List<vventas> GetListCuentas(DateTime fechaInicial, DateTime fechaFinal, int idEmpresa, string status, int idCliente = 0, string linea = null)
        {
            try
            {
                List<vventas> lista;
                using (var db = new NtLinkLocalServiceEntities())
                {
                    if (idEmpresa == 0)
                    {
                        if (linea == null)
                        {
                            lista = db.vventas.Where(p => p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal).ToList();
                        }
                        else
                        {
                            if(idCliente==0)
                            {
                                lista = db.vventas.Where(p => p.Fecha >= fechaInicial &&
                                                              p.Fecha <= fechaFinal && p.Linea == linea).ToList();    
                            }
                            else
                            {
                                lista = db.vventas.Where(p => p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal && p.Linea == linea &&
                                                          p.idcliente == idCliente).ToList();
                            }
                            
                        }


                    }
                    else if (idCliente == 0)
                    {
                        if (linea == null)
                        {
                            lista = db.vventas.Where(p => p.IdEmpresa == idEmpresa && p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal).ToList();
                        }
                        else
                        {
                            lista = db.vventas.Where(p => p.IdEmpresa == idEmpresa && p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal && p.Linea == linea).ToList();
                        }
                    }
                    else
                    {
                        if (linea == null)
                        {
                            lista =
                           db.vventas.Where(
                               p => p.IdEmpresa == idEmpresa && p.idcliente == idCliente && p.Fecha >= fechaInicial &&
                                    p.Fecha <= fechaFinal).ToList();
                        }
                        else
                        {
                            lista =
                              db.vventas.Where(
                                  p => p.IdEmpresa == idEmpresa && p.idcliente == idCliente && p.Fecha >= fechaInicial &&
                                       p.Fecha <= fechaFinal && p.Linea == linea).ToList();
                        }


                    }
                    if (status == "Todos")
                    {
                        return lista;
                    }
                    return lista.Where(p => p.StatusFactura == status || p.StatusFactura == "Pagado").ToList();
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<vventas>();
            }

        }
        */
        public List<vfacturasEmitidos> GetListEmitidos(string Linea, DateTime fechaInicial, DateTime fechaFinal, int idEmpresa, string status, int idCliente = 0)
        {
            try
            {
                List<vfacturasEmitidos> lista;
                using (var db = new GAFEntities())
                {
                    if (idEmpresa == 0)
                    {

                        if (idCliente == 0)
                        {
                            lista = db.vfacturasEmitidos.Where(p => p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal && p.Linea == Linea).OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            // order by folio
                            lista = db.vfacturasEmitidos.Where(p => p.Fecha >= fechaInicial && p.Fecha <= fechaFinal &&
                                                      p.idCliente == idCliente && p.Linea == Linea).OrderByDescending(p => p.Fecha).ToList();
                        }



                    }
                    else
                        if (idCliente == 0)
                        {

                            lista = db.vfacturasEmitidos.Where(p => p.IdEmpresa == idEmpresa && p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal && p.Linea == Linea).OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            lista = db.vfacturasEmitidos.Where(p => p.IdEmpresa == idEmpresa && p.Fecha >= fechaInicial && p.idCliente == idCliente &&
                                                          p.Fecha <= fechaFinal && p.Linea == Linea).OrderByDescending(p => p.Fecha).ToList();
                        }

                    // lista = db.facturas.Where(p => p.IdEmpresa == idEmpresa ).OrderByDescending(p => p.Folio).ToList();


                    if (status == "Todos")
                    {
                        return lista;
                    }
                    return lista.Where(p => p.TipoDocumentoStr == status).ToList();
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<vfacturasEmitidos>();
            }

        }

        public List<vfacturas> GetListPromotorOperador(string Linea, DateTime fechaInicial, DateTime fechaFinal, int idEmpresa, string status, string promotor, int idCliente = 0)
        {
            try
            {
                List<vfacturas> lista;
                using (var db = new GAFEntities())
                {
                    if (idEmpresa == 0)
                    {

                        if (idCliente == 0)
                        {
                            lista = db.vfacturas.Where(p => p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal && (p.Linea == Linea)).OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            // order by folio
                            lista = db.vfacturas.Where(p => p.Fecha >= fechaInicial && p.Fecha <= fechaFinal &&
                                                      p.idCliente == idCliente && (p.Linea == Linea)).OrderByDescending(p => p.Fecha).ToList();
                        }



                    }
                    else
                        if (idCliente == 0)
                        {

                            lista = db.vfacturas.Where(p => p.IdEmpresa == idEmpresa && p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal && (p.Linea == Linea)).OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            lista = db.vfacturas.Where(p => p.IdEmpresa == idEmpresa && p.Fecha >= fechaInicial && p.idCliente == idCliente &&
                                                          p.Fecha <= fechaFinal && (p.Linea == Linea)).OrderByDescending(p => p.Fecha).ToList();
                        }

                    // lista = db.facturas.Where(p => p.IdEmpresa == idEmpresa ).OrderByDescending(p => p.Folio).ToList();

                    if (promotor != "0")
                        lista = lista.Where(p => p.Usuario == new Guid(promotor)).ToList();

                    if (status == "Todos")
                    {
                        return lista;
                    }
                    return lista.Where(p => p.TipoDocumentoStr == status).ToList();
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<vfacturas>();
            }

        }
      

        public List<vfacturas> GetListOperador(string Linea, DateTime fechaInicial, DateTime fechaFinal, int idEmpresa, string status, string promotor, int idCliente = 0)
        {
            try
            {
                List<vfacturas> lista;
                using (var db = new GAFEntities())
                {
                    if (idEmpresa == 0)
                    {
                        
                            if (idCliente == 0)
                            {
                                lista = db.vfacturas.Where(p => (p.Fecha >= fechaInicial &&
                                                              p.Fecha <= fechaFinal) && (p.Linea == Linea)).OrderByDescending(p => p.Fecha).ToList();
                            }
                            else
                            {
                                // order by folio
                                lista = db.vfacturas.Where(p => p.idCliente == idCliente && (p.Fecha >= fechaInicial && p.Fecha <= fechaFinal) &&
                                                           (p.Linea == Linea)).OrderByDescending(p => p.Fecha).ToList();
                            }

                        
                        
                    }
                    else 
                        if (idCliente == 0)
                        {
                        
                            lista = db.vfacturas.Where(p => p.IdEmpresa == idEmpresa && (p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal )&& (p.Linea == Linea)).OrderByDescending(p => p.Fecha).ToList();
                        }
                        else 
                        {
                            lista = db.vfacturas.Where(p => p.IdEmpresa == idEmpresa && p.idCliente == idCliente &&( p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal )&& (p.Linea == Linea)).OrderByDescending(p => p.Fecha).ToList();
                        }

                   // lista = db.facturas.Where(p => p.IdEmpresa == idEmpresa ).OrderByDescending(p => p.Folio).ToList();
                        
                   if( promotor!="0")
                       lista = lista.Where(p => p.idUsuario == new Guid(promotor)).ToList();

                    if (status == "Todos")
                    {
                        return lista;
                    }
                    return lista.Where(p => p.TipoDocumentoStr== status).ToList();
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<vfacturas>();
            }
           
        }
        public List<vfacturas> GetList(DateTime fechaInicial, DateTime fechaFinal, int idEmpresa, string status,  int idCliente = 0)
        {
            try
            {
                List<vfacturas> lista;
                using (var db = new GAFEntities())
                {
                    if (idEmpresa == 0)
                    {

                        if (idCliente == 0)
                        {
                            lista = db.vfacturas.Where(p => p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal).OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            // order by folio
                            lista = db.vfacturas.Where(p => p.Fecha >= fechaInicial && p.Fecha <= fechaFinal &&
                                                      p.idCliente == idCliente).OrderByDescending(p => p.Fecha).ToList();
                        }



                    }
                    else
                        if (idCliente == 0)
                        {

                            lista = db.vfacturas.Where(p => p.IdEmpresa == idEmpresa && p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal).OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            lista = db.vfacturas.Where(p => p.IdEmpresa == idEmpresa && p.Fecha >= fechaInicial && p.idCliente == idCliente &&
                                                          p.Fecha <= fechaFinal).OrderByDescending(p => p.Fecha).ToList();
                        }

                    // lista = db.facturas.Where(p => p.IdEmpresa == idEmpresa ).OrderByDescending(p => p.Folio).ToList();

               
                    if (status == "Todos")
                    {
                       
                        return lista;
                    }
                    return lista.Where(p => p.StatusFactura == status).ToList();
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<vfacturas>();
            }

        }
        public List<vfacturasContabilidad> GetListContabilidad(string Linea, DateTime fechaInicial, DateTime fechaFinal, int idEmpresa, string status, int idCliente = 0)
        {
            try
            {
                List<vfacturasContabilidad> lista;
                using (var db = new GAFEntities())
                {
                    if (idEmpresa == 0)
                    {

                        if (idCliente == 0)
                        {
                            lista = db.vfacturasContabilidad.Where(p => p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal && p.Linea==Linea).OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            // order by folio
                            lista = db.vfacturasContabilidad.Where(p => p.Fecha >= fechaInicial && p.Fecha <= fechaFinal &&
                                                      p.idCliente == idCliente && p.Linea == Linea).OrderByDescending(p => p.Fecha).ToList();
                        }



                    }
                    else
                        if (idCliente == 0)
                        {

                            lista = db.vfacturasContabilidad.Where(p => p.IdEmpresa == idEmpresa && p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal && p.Linea == Linea).OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            lista = db.vfacturasContabilidad.Where(p => p.IdEmpresa == idEmpresa && p.Fecha >= fechaInicial && p.idCliente == idCliente &&
                                                          p.Fecha <= fechaFinal && p.Linea == Linea).OrderByDescending(p => p.Fecha).ToList();
                        }

                    // lista = db.facturas.Where(p => p.IdEmpresa == idEmpresa ).OrderByDescending(p => p.Folio).ToList();


                    if (status == "Todos")
                    {
                        return lista;
                    }
                    return lista.Where(p => p.TipoDocumentoStr == status).ToList();
                    
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<vfacturasContabilidad>();
            }

        }
        public List<vfacturasPromotores> GetListPromotores(System.Guid usuario, DateTime fechaInicial, DateTime fechaFinal, int idEmpresa, string status, int idCliente = 0)
        {
            try
            {
                List<vfacturasPromotores> lista;
                using (var db = new GAFEntities())
                {
                    if (idEmpresa == 0)
                    {

                        if (idCliente == 0)
                        {
                            lista = db.vfacturasPromotores.Where(p => p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal && p.IDUsuario == usuario).OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            // order by folio
                            lista = db.vfacturasPromotores.Where(p => p.Fecha >= fechaInicial && p.Fecha <= fechaFinal &&
                                                      p.idCliente == idCliente && p.IDUsuario == usuario).OrderByDescending(p => p.Fecha).ToList();
                        }



                    }
                    else
                        if (idCliente == 0)
                        {

                            lista = db.vfacturasPromotores.Where(p => p.IdEmpresa == idEmpresa && p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal && p.IDUsuario == usuario).OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            lista = db.vfacturasPromotores.Where(p => p.IdEmpresa == idEmpresa && p.Fecha >= fechaInicial && p.idCliente == idCliente &&
                                                          p.Fecha <= fechaFinal && p.IDUsuario == usuario).OrderByDescending(p => p.Fecha).ToList();
                        }

                    // lista = db.facturas.Where(p => p.IdEmpresa == idEmpresa ).OrderByDescending(p => p.Folio).ToList();


                    if (status == "Todos")
                    {
                        return lista;
                    }
                    return lista.Where(p => p.TipoDocumentoStr == status).ToList();
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<vfacturasPromotores>();
            }

        }

        public List<vventas> GetListComplementoPAgos(System.Guid guid)
        {
            try
            {
                List<vventas> lista;
                using (var db = new GAFEntities())
                {

                    return db.vventas.Where(p => p.StatusFactura != "Cancelado" && p.IDUsuario==guid).ToList();
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<vventas>();
            }

        }
        public vventas GetListDocumentosPAgos(long idprefactura)
        {
            try
            {
                vventas lista;
                using (var db = new GAFEntities())
                {

                    return db.vventas.Where(p => p.idPreFactura==idprefactura).FirstOrDefault();
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new vventas();
            }

        }
        public facturas GetListDocumentosPAgosContabilidad(long idfactura)
        {
            try
            {
                facturas lista;
                using (var db = new GAFEntities())
                {

                    return db.facturas.Where(p => p.idVenta == idfactura).FirstOrDefault();
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new facturas();
            }

        }

       /*
        //--------------------------------------
        public List<vventaRetenciones> GetListRetenciones(DateTime fechaInicial, DateTime fechaFinal, int idEmpresa, string status, int idCliente = 0, string linea = null )
        {
            try
            {
                string iniciales = null;
                List<vventaRetenciones> lista;
                using (var db = new NtLinkLocalServiceEntities())
                {
                    if (idEmpresa == 0)
                    {
                        if (linea == null)
                        {
                            lista = db.vventaRetenciones.Where(p => p.FechaFactura >= fechaInicial &&
                                                          p.FechaFactura <= fechaFinal).OrderByDescending(p => p.Id).ToList();
                        }
                        else
                        {
                            if (idCliente == 0)
                            {
                                lista = db.vventaRetenciones.Where(p => p.FechaFactura >= fechaInicial &&
                                                              p.FechaFactura <= fechaFinal && p.Linea == linea).OrderByDescending(p => p.Id).ToList();
                            }
                            else
                            {
                                // order by folio
                                lista = db.vventaRetenciones.Where(p => p.FechaFactura >= fechaInicial && p.FechaFactura <= fechaFinal && p.Linea == linea &&
                                                          p.idCliente == idCliente).OrderByDescending(p => p.Id).ToList();
                            }

                        }

                        //Lista de REportes
                    }
                    else if (idCliente == 0)
                    {
                         if (linea == null)
                        {
                            lista = db.vventaRetenciones.Where(p => p.IdEmpresa == idEmpresa && p.FechaFactura >= fechaInicial &&
                                                          p.FechaFactura <= fechaFinal).OrderByDescending(p => p.Id).ToList();
                        }
                        else
                        {
                            lista = db.vventaRetenciones.Where(p => p.IdEmpresa == idEmpresa && p.FechaFactura >= fechaInicial &&
                                                          p.FechaFactura <= fechaFinal && p.Linea == linea).OrderByDescending(p => p.Id).ToList();
                        }
                    }
                    else
                    {
                        if (linea == null)
                        {
                            lista =
                           db.vventaRetenciones.Where(
                               p => p.IdEmpresa == idEmpresa && p.idCliente == idCliente && p.FechaFactura >= fechaInicial &&
                                    p.FechaFactura <= fechaFinal).OrderByDescending(p => p.Id).ToList();
                        }
                        else
                        {
                            lista =
                              db.vventaRetenciones.Where(
                                  p => p.IdEmpresa == idEmpresa && p.idCliente == idCliente && p.FechaFactura >= fechaInicial &&
                                       p.FechaFactura <= fechaFinal && p.Linea == linea).OrderByDescending(p => p.Id).ToList();
                        }


                    }
                    if (status == "Todos")//manda todos
                    {
                        return lista;
                    } 
                    return lista.Where(p => p.Status == status).ToList(); //si no busca por el estatus
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<vventaRetenciones>();
            }

        }
        //----------------------------------------------------------
       public string Confirmacion(string Confirmar, Int64 IdTimbre)
        {
            try
            {
           
            using (var db = new NtLinkLocalServiceEntities())
            {
                {
                    ConfirmacionTimbreWs33 C=db.ConfirmacionTimbreWs33.FirstOrDefault(p => p.IdTimbre == IdTimbre);
                    C.Confirmacion = Confirmar;
                    C.procesado = false;
                    db.ConfirmacionTimbreWs33.ApplyCurrentValues(C);
                }
                db.SaveChanges();
                return "OK";
            }
           }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return ("Error al Confirmar ");
            }

        }
        //--------------------------------------
        public List<ConfirmacionTimbreWs33> GetListConfirmacion( string idEmpresa, string idCliente)
        {
            try
            {
                int idC = 0;
                int idE = 0;

                if (!string.IsNullOrEmpty(idCliente))
                    idC = int.Parse(idCliente);
                if (!string.IsNullOrEmpty(idEmpresa))
                    idE = int.Parse(idEmpresa);

                List<ConfirmacionTimbreWs33> lista;
                using (var db = new NtLinkLocalServiceEntities())
                {
                    if (string.IsNullOrEmpty( idEmpresa))
                    {
                            lista = db.ConfirmacionTimbreWs33.OrderByDescending(p => p.IdTimbre).ToList();
                        //Lista de REportes
                    }
                    else if (string.IsNullOrEmpty(idCliente) || idCliente=="0")
                    {
                          empresa empr = db.empresa.Where(x=>x.IdEmpresa==idE).FirstOrDefault();
                          if (empr != null)
                              idEmpresa = empr.RFC;
                          
                        lista = db.ConfirmacionTimbreWs33.Where(p => p.RfcEmisor == idEmpresa ).OrderByDescending(p => p.IdTimbre).ToList();
                    }
                    else
                    {
                        empresa empr = db.empresa.Where(x => x.IdEmpresa == idE).FirstOrDefault();
                        if (empr != null)
                            idEmpresa = empr.RFC;
                        clientes clien = db.clientes.Where(x => x.idCliente == idC).FirstOrDefault();
                        if (clien != null)
                            idCliente = clien.RFC;
                     
                            lista =
                           db.ConfirmacionTimbreWs33.Where(
                               p => p.RfcEmisor == idEmpresa && p.RfcReceptor == idCliente ).OrderByDescending(p => p.IdTimbre).ToList();
                  
                    }
                  //  if (status == "Todos")//manda todos
                   // {
                        return lista;
                  //  }
                   // return lista.Where(p => p.Status == status).ToList(); //si no busca por el estatus
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<ConfirmacionTimbreWs33>();
            }

        }

      
        //-----------------------------------------------------
        public List<vventas> GetListNomina(DateTime fechaInicial, DateTime fechaFinal, int idEmpresa, string status, int idCliente = 0, string linea = null, string iniciales = null)
        {
            try
            {
                List<vventas> lista;
                using (var db = new NtLinkLocalServiceEntities())
                {
                    if (idEmpresa == 0)
                    {
                        if (!string.IsNullOrEmpty(iniciales))
                        {
                            lista = db.vventas.Where(p => p.Tipo == 1 &&  p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal && p.Usuario == iniciales).OrderBy(p => p.Folio).ToList();
                        }
                        else if (linea == null)
                        {
                            lista = db.vventas.Where(p => p.Tipo == 1 && p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal).OrderBy(p => p.Folio).ToList();
                        }
                        else
                        {
                            if (idCliente == 0)
                            {
                                lista = db.vventas.Where(p => p.Tipo == 1 && p.Fecha >= fechaInicial &&
                                                              p.Fecha <= fechaFinal && p.Linea == linea).OrderBy(p => p.Folio).ToList();
                            }
                            else
                            {
                                // order by folio
                                lista = db.vventas.Where(p => p.Tipo == 1 && p.Fecha >= fechaInicial && p.Fecha <= fechaFinal && p.Linea == linea &&
                                                          p.idcliente == idCliente).OrderBy(p => p.Folio).ToList();
                            }

                        }

                        //Lista de REportes
                    }
                    else if (idCliente == 0)
                    {
                        if (!string.IsNullOrEmpty(iniciales))
                        {
                            lista = db.vventas.Where(p => p.Tipo == 1 && p.IdEmpresa == idEmpresa && p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal && p.Usuario == iniciales).OrderByDescending(p => p.Folio).ToList();
                        }
                        else if (linea == null)
                        {
                            lista = db.vventas.Where(p => p.Tipo == 1 && p.IdEmpresa == idEmpresa && p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal).OrderByDescending(p => p.Folio).ToList();
                        }
                        else
                        {
                            lista = db.vventas.Where(p => p.Tipo == 1 && p.IdEmpresa == idEmpresa && p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal && p.Linea == linea).OrderByDescending(p => p.Folio).ToList();
                        }
                    }
                    else
                    {
                        if (linea == null)
                        {
                            lista =
                           db.vventas.Where(
                               p => p.Tipo == 1 && p.IdEmpresa == idEmpresa && p.idcliente == idCliente && p.Fecha >= fechaInicial &&
                                    p.Fecha <= fechaFinal).ToList();
                        }
                        else
                        {
                            lista =
                              db.vventas.Where(
                                  p => p.Tipo == 1 && p.IdEmpresa == idEmpresa && p.idcliente == idCliente && p.Fecha >= fechaInicial &&
                                       p.Fecha <= fechaFinal && p.Linea == linea).ToList();
                        }


                    }
                    if (status == "Todos")
                    {
                        return lista;
                    }
                    return lista.Where(p => p.StatusFactura == status).ToList();
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<vventas>();
            }

        }
        //----nuevo
        public vventaRetenciones GetRetencionById(Int64 ID)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    var x = db.vventaRetenciones.FirstOrDefault(p => p.Id == ID);
                    return x;
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return null;
            }
        }
        
        */
        public facturas GetByComprobante(string UUDI)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    var x = db.facturas.FirstOrDefault(p => p.Uid == UUDI);
                    return x;
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return null;
            }
        }
        public ComprobantePdf GetByComprobantePDF(string UUDI)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    var x = db.ComprobantePdf.FirstOrDefault(p => p.timbre_UUID == UUDI);
                    return x;
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return null;
            }
        }
        public facturas GetById(int idVenta)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    var x = db.facturas.FirstOrDefault(p => p.idVenta == idVenta);
                    return x;
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return null;
            }
        }
        /*
        public bool SaveList (List<vventas> lista)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    foreach (vventas ventas in lista)
                    {
                        vventas ventas1 = ventas;
                        facturas f = db.facturas.Where(p => p.idVenta == ventas1.idVenta).First();
                        f.Cancelado = ventas.Cancelado;
                        f.FechaPago = ventas.FechaPago;
                        f.ReferenciaPago = ventas.ReferenciaPago;
                        f.Vencimiento = ventas.Vencimiento;
                        f.Proyecto = ventas.Proyecto;
                        //_entities.facturas.ApplyCurrentValues(f);
                    }
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return false;
            }
            
        }
        */
        


    }
}
