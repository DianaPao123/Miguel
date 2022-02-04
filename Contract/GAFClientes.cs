using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;



namespace Contract
{
    public class GAFClientes : GAFContract
    {




        public clientes GetClienteRazonSocial(string Cliente)
        {

            try
            {
                using (var db = new GAFEntities())
                {
                    var cliente = db.clientes.Where(c => c.RazonSocial == Cliente);
                    return cliente.FirstOrDefault();
                }
            }
            catch (Exception ee)
            {
                   return null;
            }

        }

        public clientes GetCliente(int idCliente)
        {

            try
            {
                using (var db = new GAFEntities())
                {
                    var cliente = db.clientes.Where(c => c.idCliente == idCliente);
                    return cliente.FirstOrDefault();
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return null;
            }

        }
        public Usuario GetUsuario(int idUsuario)
        {

            try
            {
                using (var db = new GAFEntities())
                {
                    var cliente = db.Usuario.Where(c => c.id == idUsuario);
                    return cliente.FirstOrDefault();
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return null;
            }

        }

      
        public clientes  GetCliente(string rfc)
        {

            try
            {
                using (var db = new GAFEntities())
                {
                    var cliente = db.clientes.Where(c => c.RFC == rfc);
                    return cliente.FirstOrDefault();
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return null;
            }
            
        }


        public List<clientes> GetList(string linea)
        {

            try
            {
                using (var db = new GAFEntities())
                {
                    string query = "select Value c from NtLinkLocalServiceEntities.clientes as c inner join NtLinkLocalServiceEntities.empresa as b " +
                        " ON (c.idempresa = b.IdEmpresa) where b.Linea = @linea";
                    ObjectParameter op = new ObjectParameter("linea", linea);
                    var q = db.CreateQuery<clientes>(query, new [] {op}).OrderBy(p=>p.RazonSocial);
                    var result = q.ToList();
                    return result;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return null;
            }
        }
        public List<clientes> GetListLinea(string linea)
        {

            try
            {
                using (var db = new GAFEntities())
                {
                    var cliente = db.clientes.Where(c => c.Linea == linea).ToList();

                    return cliente;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return null;
            }
        }


        public List<clientes> GetList()
        {
            
            try
            {
                using (var db = new GAFEntities())
                {

                    var result = db.clientes.OrderBy(l=>l.RazonSocial).ToList();
                    return result;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return null;
            }
        }
        public List<Usuario> GetListPromotor()
        {

            try
            {
                using (var db = new GAFEntities())
                {

                    var result = db.Usuario.Where(p => p.Rol == "Promotor").ToList();
                    if (result != null)
                    {

                        foreach (var x in result)
                        {
                            x.Nombre = x.Nombre + ' ' + x.ApellidoP + ' ' + x.ApellidoM;
                        }
                    }
                    return result;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return null;
            }
        }

        public List<Usuario> GetListPromotorOperador(string usuario )
        {

            try
            {
                var result=new List<Usuario>();  
                using (var db = new GAFEntities())
                {
                    if(!string.IsNullOrEmpty(usuario))
                        result = db.Usuario.Where(p => p.Usuario1.Contains(usuario) && (p.Rol == "Promotor" || p.Rol == "Operador" || p.Rol == "Contabilidad")).ToList();
                    else
                      result = db.Usuario.Where(p => p.Rol == "Promotor" || p.Rol == "Operador").ToList();
                    if (result != null)
                        if(result.Count>0)
                    {

                        foreach (var x in result)
                        {
                            x.Nombre = x.Nombre + ' ' + x.ApellidoP + ' ' + x.ApellidoM;
                        }
                    }
                    return result;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return null;
            }
        }



        public List<clientes> GetList(int idEmpresa, string filtro = "", bool lista = false)
        {
            List<clientes> result = new List<clientes>();
            try
            {
                using (var db = new GAFEntities())
                {
                    if (string.IsNullOrEmpty(filtro))
                    {
                        result =
                            db.clientes.Where(p => p.idempresa == idEmpresa).OrderByDescending(p => p.RazonSocial).
                                ToList();
                    }
                    else
                    {
                        result =
                            db.clientes.Where(
                                p =>
                                p.idempresa == idEmpresa && (p.RazonSocial.Contains(filtro) || p.RFC.Contains(filtro))).
                                OrderByDescending(p => p.RazonSocial).ToList();
                    }
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
            }
            if (lista)
                result.Add(new clientes{RazonSocial = "Todos",idCliente = 0});
            result.Reverse();
            return result;
        }


        public List<vClientePromotor> GetClientePromotor(System.Guid usurio,string RFC)
        {

            try
            {
                List<vClientePromotor> cliente;
                using (var db = new GAFEntities())
                {
                    if(string.IsNullOrEmpty(RFC))
                     cliente = db.vClientePromotor.Where(c => c.IdPromotor == usurio).ToList();
                    else
                        cliente = db.vClientePromotor.Where(c => c.IdPromotor == usurio && c.RFC==RFC).ToList();
                               
                    return cliente;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return null;
            }

        }
        public List<vClientePromotor> GetClientePromotorAll( string RFC)
        {

            try
            {
                List<vClientePromotor> cliente;
                using (var db = new GAFEntities())
                {
                    if (string.IsNullOrEmpty(RFC))
                        cliente = db.vClientePromotor.ToList();
                    else
                        cliente = db.vClientePromotor.Where(c => c.RFC == RFC).ToList();

                    return cliente;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return null;
            }

        }
        public List<clientes> GetClientePromotorAllX(string RFC)
        {

            try
            {
                List<clientes> cliente;
                using (var db = new GAFEntities())
                {
                    if (string.IsNullOrEmpty(RFC))
                        cliente = db.clientes.ToList();
                    else
                        cliente = db.clientes.Where(c => c.RFC == RFC ).ToList();

                    return cliente;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return null;
            }

        }
      
        public List<clientes> GetClientePromotorAllBC(string RFC)
        {

            try
            {
                List<clientes> cliente;
                using (var db = new GAFEntities())
                {
                    if (string.IsNullOrEmpty(RFC))
                        cliente = db.clientes.Where(c=>c.Linea!="A").ToList();
                    else
                        cliente = db.clientes.Where(c => c.RFC == RFC && c.Linea != "A").ToList();

                    return cliente;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return null;
            }

        }
        public bool EliminarCliente(clientes cliente)
        {
            try
            {

                Logger.Info(cliente);

                using (var db = new GAFEntities())
                {
                    var existe = db.facturas.Any(p => p.idcliente == cliente.idCliente);
                    if (existe)
                        throw new FaultException("El cliente tiene facturas capturadas, no es posible eliminar");
                    var cli = db.clientes.FirstOrDefault(c => c.idCliente == cliente.idCliente);
                    db.clientes.DeleteObject(cli);
                    db.SaveChanges();
                    return true;
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
                return false;
            }
        }

        public int SaveCliente(clientes cliente)
        {

            try
            {
                if (Validar(cliente))
                {
                    using (var db = new GAFEntities())
                    {
                        if (cliente.idCliente == 0)
                        {
                            db.clientes.AddObject(cliente);
                        }
                        else
                        {
                            var y = db.clientes.Where(p => p.idCliente == cliente.idCliente).FirstOrDefault();
                            db.clientes.ApplyCurrentValues(cliente);
                        }
                        db.SaveChanges();
                        return cliente.idCliente;
                    }
                }
                return 0;
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


        /*
        public DatosNomina GetDatosByCliente(int idCliente)
        {
            try
            {

                using (var db = new GAFEntities())
                {
                    var datos = db.DatosNomina.FirstOrDefault(p => p.IdCliente == idCliente);
                    return datos;
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
                return null;
            }
        }
        
        public bool SaveDatosNomina(DatosNomina datos)
        {
            try
            {

                using (var db = new GAFEntities())
                {
                    if (datos.IdDatoNomina == 0)
                    {
                        db.DatosNomina.AddObject(datos);
                    }
                    else
                    {
                        var y = db.DatosNomina.FirstOrDefault(p => p.IdDatoNomina == datos.IdDatoNomina);
                        db.DatosNomina.ApplyCurrentValues(datos);
                    }
                    db.SaveChanges();
                    return true;
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
                return false;
            }
        }
        */

        private bool Validar(clientes e)
        {
            //TODO: Validar los campos requeridos y generar excepcion
            {
                if (string.IsNullOrEmpty(e.RazonSocial))
                {
                    throw new FaultException<ApplicationException>(new ApplicationException("La Razón Social no puede ir vacía"), "La Razón Social no puede ir vacía");
                }
                if (string.IsNullOrEmpty(e.RFC))
                {
                    throw new FaultException<ApplicationException>(new ApplicationException("El RFC no puede ir vacío"), "El RFC no puede ir vacío");
                }
      
                if (!string.IsNullOrEmpty(e.CP))
                    if (e.CP.Length < 5)// se agrego 
                    {
                        throw new FaultException<ApplicationException>(new ApplicationException("El campo CP no puede ser menor a 5"), "El campo CP no puede ser menor a 5");
                  
                    }
                Regex reg = new Regex("^[A-Z,Ñ,&amp;]{3,4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]{2}[0-9,A]$");
                if (!reg.IsMatch(e.RFC))
                {
                    throw new FaultException<ApplicationException>(new ApplicationException("El RFC es inválido"),"El RFC es inválido");
                }
            }
            return true;
        }
        /*
        public List<vClientesPromotores> ListaPromotoresClientes(int idCliente)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    var x = db.vClientesPromotores.Where(p => p.idCliente == idCliente).ToList();
                    Logger.Debug(x.Count);
                    return x;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return null;
            }
        }
         */ 


        //----------------------------------------------clientes promotores-------------------------------------------------------
       
        public int SaveClientePromotores(ClientePromotor cliente)
        {

            try
            {
                
                    using (var db = new GAFEntities())
                    {
                        if (cliente.idClientePromotor == 0)
                        {
                            db.ClientePromotor.AddObject(cliente);
                        }
                        else
                        {
                            var y = db.ClientePromotor.Where(p => p.idCliente == cliente.idCliente).FirstOrDefault();
                            db.ClientePromotor.ApplyCurrentValues(cliente);
                        }
                        db.SaveChanges();
                        return 1;
                    }
                
                return 0;
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
      //-----------------------------------
        public List<ClientePromotor> GetListClientePromotor(System.Guid idPromotor)
        {
            List<ClientePromotor> result = new List<ClientePromotor>();
            try
            {
                using (var db = new GAFEntities())
                {
                  
                        result =
                            db.ClientePromotor.Where(p => p.IdPromotor == idPromotor).OrderByDescending(p => p.RazonSocial).
                                ToList();
                    
                   
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
            }
            
            return result;
        }
    //----------------------------------
        public ClientePromotor GetClientePromotores(int idClientePromotor)
        {

            try
            {
                using (var db = new GAFEntities())
                {
                    var cliente = db.ClientePromotor.Where(c => c.idClientePromotor == idClientePromotor);
                    return cliente.FirstOrDefault();
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return null;
            }

        }
        public ClientePromotor GetClientePromotorRelacion(int idCliente,System.Guid idPromotor)
        {

            try
            {
                using (var db = new GAFEntities())
                {
                    var cliente = db.ClientePromotor.Where(c => c.idCliente == idCliente && c.IdPromotor==idPromotor);
                    return cliente.FirstOrDefault();
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return null;
            }

        }
        //---------------------------------------

    }
}
