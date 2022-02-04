using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Contract;

namespace Business
{
    public class GAFEmpresa : GAFBusiness
    {

        private void CreaRutas(string rfc)
        {
            string path = Path.Combine(ConfigurationManager.AppSettings["Resources"], rfc);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            if (!Directory.Exists(Path.Combine(path, "Certs")))
                Directory.CreateDirectory(Path.Combine(path, "Certs"));
            if (!Directory.Exists(Path.Combine(path, "Facturas")))
                Directory.CreateDirectory(Path.Combine(path, "Facturas"));
        }


        public List<empresa> GetList(string perfil, int idEmpresa, long idSistema)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    if (perfil.Equals("Administrador"))
                    {
                        var res = db.empresa.Where(p => p.idSistema == idSistema).Select(
                                p =>
                                new
                                    {
                                        RFC = p.RFC,
                                        idSistema = p.idSistema,
                                        IdEmpresa = p.IdEmpresa,
                                        RazonSocial = p.RazonSocial,
                                        TimbresConsumidos = p.TimbresConsumidos,
                                        Linea = p.Linea,
                                        Baja = p.Baja,
                                        Bloqueado = p.Bloqueado
                                    }).OrderBy(p => p.RFC).ToList();
                        return
                            res.Select(
                                p =>
                                new empresa()
                                {
                                    RFC = p.RFC,
                                    idSistema = p.idSistema,
                                    IdEmpresa = p.IdEmpresa,
                                    RazonSocial = p.RazonSocial,
                                    TimbresConsumidos = p.TimbresConsumidos,
                                    Linea = p.Linea,
                                    Baja = p.Baja,
                                    Bloqueado = p.Bloqueado
                                }).ToList();
                    }
                    
                    else
                    {
                        var res =
                            db.empresa.Where(p => p.IdEmpresa == idEmpresa).Select(
                                p =>
                                new
                                    {
                                        RFC = p.RFC,
                                        idSistema = p.idSistema,
                                        IdEmpresa = p.IdEmpresa,
                                        RazonSocial = p.RazonSocial,
                                        TimbresConsumidos = p.TimbresConsumidos,
                                        Linea = p.Linea,
                                        Baja = p.Baja,
                                        Bloqueado = p.Bloqueado
                                    }).OrderBy(p => p.RFC).ToList();
                        return res.Select(
                                p =>
                                new empresa()
                                {
                                    RFC = p.RFC,
                                    idSistema = p.idSistema,
                                    IdEmpresa = p.IdEmpresa,
                                    RazonSocial = p.RazonSocial,
                                    TimbresConsumidos = p.TimbresConsumidos,
                                    Linea = p.Linea,
                                    Baja = p.Baja,
                                    Bloqueado = p.Bloqueado
                                }).ToList();
                    }
                        
                }

            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }

        }



        public List<Catalogo_Giros> GetListGiro()
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    return db.Catalogo_Giros.ToList();
                }

            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }

        }
        //----------------------------------
        public List<empresa> GetListGiroempresaBy(long idGiro)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    return db.empresa.Where(p => p.Id_Giro == idGiro && p.Linea=="A").ToList();
                }

            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }

        }
        public List<empresa> GetListempresa()
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    return db.empresa.ToList();
                }

            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }

        }
        
        //----------------------------------
        public List<empresa> GetListByPattern(string pattern)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    return db.empresa.Where(p => p.RFC.Contains(pattern) || p.RazonSocial.Contains(pattern)).Take(20).ToList();
                }

            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }

        }

        public List<empresa> GetListForLine(string Linea)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    if (Linea == null)
                    {
                        Linea = "A";
                    }
                    if (Linea != null)
                    {
                        var res =
                            db.empresa.Where(p => p.Linea == Linea ).Select(
                                p =>
                                new
                                    {
                                        p.RFC,
                                        p.idSistema, 
                                        p.IdEmpresa,
                                        p.RazonSocial,
                                        p.TimbresConsumidos,
                                        Linea,
                                        p.Baja,
                                        p.Bloqueado
                                    }).OrderBy(p => p.RFC).ToList();
                        return
                            res.Select(
                                p =>
                                new empresa()
                                    {
                                        RFC = p.RFC,
                                        idSistema = p.idSistema,
                                        IdEmpresa = p.IdEmpresa,
                                        RazonSocial = p.RazonSocial,
                                        TimbresConsumidos = p.TimbresConsumidos,
                                        Linea = p.Linea,
                                        Baja = p.Baja,
                                        Bloqueado = p.Bloqueado
                                    }).ToList();
                    }
                    else
                    {
                        return db.empresa.Select(p => new empresa() { RFC = p.RFC, idSistema = p.idSistema, IdEmpresa = p.IdEmpresa, RazonSocial = p.RazonSocial, TimbresConsumidos = p.TimbresConsumidos, Linea = p.Linea, Baja = p.Baja, Bloqueado = p.Bloqueado }).OrderBy(p => p.RFC).ToList();
                    }
                }

            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }

        }
        public List<vEmpresasCuentasBancos> GetListForLineMovimientos(string Linea)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    var res = db.vEmpresasCuentasBancos.Where(p => p.Linea == Linea).ToList();
                    return res;
                 }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }

        }

        public List<Empresa_CuentasBancos> GetListCuentasMovimientos(Int64 idEmpresa)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    var res = db.Empresa_CuentasBancos.Where(p => p.idEmpresa == idEmpresa).ToList();
                    foreach (var x in res)
                        x.nombreBanco = x.nombreBanco + "-" + x.numeroCuenta;
                    return res;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }

        }

       
        public empresa GetById(int idEmpresa)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    var empr = db.empresa.Where(p => p.IdEmpresa == idEmpresa).FirstOrDefault();
                    if (empr != null)
                    {
                        string path = Path.Combine(ConfigurationManager.AppSettings["Resources"], empr.RFC);
                        string cer = Path.Combine(path, "Certs", "csd.cer");
                        if (File.Exists(cer))
                        {
                            X509Certificate2 cert = new X509Certificate2(cer);
                           // empr.VencimientoCert = cert.GetExpirationDateString();
                        }

                    }
                    return empr;

                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }
        }

        public empresa GetByRazonSocial(string Empresa)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    var empr = db.empresa.Where(p => p.RazonSocial == Empresa).FirstOrDefault();
                    return empr;

                }
            }
            catch (Exception ee)
            {
                return null;
            }
        }

        public empresa GetRfc(string rfc)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    var empr = db.empresa.FirstOrDefault(p => p.RFC == rfc);
                    return empr;
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
        public empresa GetByRfc(string rfc)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    var empr = db.empresa.FirstOrDefault(p => p.RFC == rfc);
                    if (empr != null)
                    {
                        string path = Path.Combine(ConfigurationManager.AppSettings["Resources"], empr.RFC);
                        string cer = Path.Combine(path, "Certs", "csd.cer");
                        if (File.Exists(cer))
                        {
                            X509Certificate2 cert = new X509Certificate2(cer);
                          //  empr.VencimientoCert = cert.GetExpirationDateString();
                        }

                    }
                    return empr;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException !=null)
                    Logger.Error(ee.InnerException);
                return null;
            }
        }

        private bool Validar(empresa e)
        {
            //TODO: Validar los campos requeridos y generar excepcion
            {
                if (string.IsNullOrEmpty(e.RazonSocial))
                {

                    throw new FaultException("La Razón Social no puede ir vacía");
                }
                if (string.IsNullOrEmpty(e.RFC))
                {
                    throw new FaultException("El RFC no puede ir vacío");

                }
                if (string.IsNullOrEmpty(e.Email))
                {
                    throw new FaultException("El campo Email es Obligatorio");
                }
              if (string.IsNullOrEmpty(e.RegimenFiscal))
                {
                    throw new FaultException("El campo Regimen Fiscal es Obligatorio");
                }
                Regex regex = new Regex(@"^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$");
     
                if (!regex.IsMatch(e.Email))
                {
                    throw new FaultException("El campo Email esta mal formado");
                }
                Regex reg = new Regex("^[A-Z,Ñ,&amp;]{3,4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]{2}[0-9,A]$");
                if (!reg.IsMatch(e.RFC))
                {
                    throw new FaultException("El RFC es inválido");
                }

                Regex curpx = new Regex("[A-Z]{4}[0-9]{6}[H,M][A-Z]{5}[0-9]{2}");
                if(!string.IsNullOrEmpty(e.CURP))
                if (!curpx.IsMatch(e.CURP))
                {
                    throw new FaultException("Curp incorrecto");

                }

            }
            return true;
        }

        public bool Save(empresa e, byte[] cert, byte[] llave, string passwordLlave, byte[] logo, string formatoLlave)
        {

            try
            {
                using (var db = new GAFEntities())
                {
                    if (Validar(e))
                    {
                        CreaRutas(e.RFC);
                        string path = Path.Combine(ConfigurationManager.AppSettings["Resources"], e.RFC);
                        if (logo != null)
                        {
                            File.WriteAllBytes(Path.Combine(path, "Logo.png"), logo);
                        }
                        if (!ValidaRfcEmisor(e.RFC, cert))
                        {
                            throw new FaultException("El rfc del emisor no corresponde con el certificado");
                        }
                        string pathCer = Path.Combine(path, "Certs", "csd.cer");
                        string pathKey = Path.Combine(path, "Certs", "csd.key" + formatoLlave);
                        File.WriteAllBytes(pathCer, cert);
                        File.WriteAllBytes(pathKey, llave);
                        var key = OpensslKey.DecodePrivateKey(llave, e.PassKey, formatoLlave);
                        if(key == null)
                        {
                            throw new FaultException("El password de la llave es incorrecto");
                        }
                        if (e.IdEmpresa == 0)
                        {
                            if (db.empresa.Any(l => l.RFC.Equals(e.RFC) && l.idSistema == e.idSistema))
                            {
                                throw new FaultException("El RFC ya ha sido dato de alta");
                            }
                            db.empresa.AddObject(e);
                        }
                        else
                        {
                            db.empresa.Where(p => p.IdEmpresa == e.IdEmpresa).FirstOrDefault();
                            db.empresa.ApplyCurrentValues(e);
                        }
                        db.SaveChanges();
                        return true;
                    }
                    Logger.Error("Fallo de validación");
                    return false;
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
                return false;
            }
        }


        public bool ValidaRfcEmisor(string rfc, byte[] certificado)
        {
            try
            {
                X509Certificate2 cer = new X509Certificate2(certificado);
                if (certificado == null)
                    return false;
                var name = cer.SubjectName.Name;
                name = name.Replace("\"", "");
                string strLRfc =
                    name.Substring(name.LastIndexOf("2.5.4.45=") + 9, 13).Trim();
                return strLRfc == rfc;
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                return false;
            }
        }

        public int ObtenerNumeroTimbres(int idEmpresa)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                /*    var res = db.vTimbresEmpresa.FirstOrDefault(p => p.IdEmpresa == idEmpresa);
                    if (res != null)
                        return res.Timbres;*/
                    return 0;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return 0;
            }


        }
        //----------------------------------------------------------------
        public bool Save_RFC_Multiple(empresa e, byte[] logo, bool m) //para al dar de alta el sistema-empresa pueda agregar con mismo rfC 
        {
            
            try
            {
                using (var db = new GAFEntities())
                {
                    if (Validar(e))
                    {
                        if (e.IdEmpresa == 0)
                        {
                            if (m == false)//para empresas con el mismo RFC
                            {
                                if (db.empresa.Any(l => l.RFC.Equals(e.RFC) && l.idSistema == e.idSistema))//quitar solo es para pruebas
                                {
                                    throw new FaultException("El RFC ya ha sido dato de alta");
                                }
                            }
                            db.empresa.AddObject(e);
                        }
                        else
                        {
                            db.empresa.FirstOrDefault(p => p.IdEmpresa == e.IdEmpresa);
                            db.empresa.ApplyCurrentValues(e);
                        }
                        db.SaveChanges();
                        CreaRutas(e.RFC);
                        string path = Path.Combine(ConfigurationManager.AppSettings["Resources"], e.RFC);
                        if (logo != null)
                        {
                            File.WriteAllBytes(Path.Combine(path, "Logo.png"), logo);
                        }
                        return true;
                    }
                    return false;
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

        //------------------------------------------------------------------------
        public bool Save(empresa e, byte[] logo)
        {
            int m = 1;
            try
            {
                using (var db = new GAFEntities())
                {
                    if (Validar(e))
                    {
                        if (e.IdEmpresa == 0)
                        {
                                  if (db.empresa.Any(l => l.RFC.Equals(e.RFC) && l.idSistema == e.idSistema))//quitar solo es para pruebas
                                    {
                                        throw new FaultException("El RFC ya ha sido dato de alta");
                                    }
                            
                            db.empresa.AddObject(e);
                        }
                        else
                        {
                            db.empresa.FirstOrDefault(p => p.IdEmpresa == e.IdEmpresa);
                            db.empresa.ApplyCurrentValues(e);
                        }
                        db.SaveChanges();
                        CreaRutas(e.RFC);
                        string path = Path.Combine(ConfigurationManager.AppSettings["Resources"], e.RFC);
                        if (logo != null)
                        {
                            File.WriteAllBytes(Path.Combine(path, "Logo.png"), logo);
                        }
                        return true;
                    }
                    return false;
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

        public bool TieneConfiguradoCertificado(int idEmpresa)
        {
            using (var db = new GAFEntities())
            {
                empresa emp = db.empresa.Single(l => l.IdEmpresa == idEmpresa);

                string path = Path.Combine(ConfigurationManager.AppSettings["Resources"], emp.RFC);
                string pathCer = Path.Combine(path, "Certs", "csd.cer");
                string pathKey = Path.Combine(path, "Certs", "csd.key");

                if (File.Exists(pathCer) && File.Exists(pathKey))
                {
                    return true;
                }
                return false;
            }
        }
                    

        public string ValidaCSD(empresa e, byte[] cert, byte[] llave, string passwordLlave, string formatoLlave)
        {
            try
            {
                CreaRutas(e.RFC);
                string path = Path.Combine(ConfigurationManager.AppSettings["Resources"], e.RFC);
                if (!ValidaRfcEmisor(e.RFC, cert))
                {
                    return "El RFC del emisor no corresponde con el certificado";
                }
                if (e.RFC.Length == 12 && !ValidaCSDEmisor(cert))
                {
                    return "El Certificado no es de tipo CSD";
                }
                    
                string pathCer = Path.Combine(path, "Certs", "csd.cer");
                string pathKey = Path.Combine(path, "Certs", "csd.key");
                File.WriteAllBytes(pathCer, cert);
                File.WriteAllBytes(pathKey, llave);
                var result = OpensslKey.DecodePrivateKey(llave, passwordLlave, formatoLlave);
                if (result !=  null)
                return "El Certificado CSD  es correcto";
                
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                return "";
            }
            return "El Password de la llave no es correcta";
        }

        public bool ValidaCSDEmisor(byte[] certificado)
        {
            try
            {
                X509Certificate2 cer = new X509Certificate2(certificado);
                if (certificado != null)
                {
                    if (cer.SerialNumber != null)
                    {
                        string serialNumber =
                            cer.SerialNumber.Trim();
                        string a = serialNumber;
                        for (int i = 0; i < a.Length; i++)
                        {
                            if (i < serialNumber.Length)
                            {
                                serialNumber = serialNumber.Remove(i, 1);
                            }
                        }
                        //RfcLco valor = CSDValidar(serialNumber);
                        //if (valor != null)
                        return true;
                    }
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                return false;
            }
            return false;
        }
        /*
        public RfcLco CSDValidar(string cadena)
        {
            try
            {
                using (var db = new NtLinkLocalServiceEntities())
                {
                    return db.RfcLco.FirstOrDefault(p => p.noCertificado == cadena);
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return null;
            }

        }
         */

        public bool GuardarEmpresa(empresa empresa, byte[] cert, byte[] llave, string passwordLlave, byte[] logo, string formatoLlave)
        {
            bool result;
             if (cert == null || llave == null)
            {
                result = Save(empresa, logo);
            }
            else
            {
                result = Save(empresa, cert, llave, passwordLlave, logo, formatoLlave);
            }
            Logger.Debug(result);
            return result;
        }
    }
}
