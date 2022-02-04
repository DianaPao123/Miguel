using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using log4net.Config;
using log4net;
using System.Net;
using ClienteNtLink.NtLinkService;

namespace ClienteTimbrado
{
    public class ClienteTimbradoNtlink
    {
        private static ILog Logger = LogManager.GetLogger(typeof(ClienteTimbradoNtlink));


        /// <summary>
        /// Constructor por default
        /// </summary>
        public ClienteTimbradoNtlink()
        {
            XmlConfigurator.Configure();
        }


        /// <summary>
        /// Método para timbrar CFDi
        /// </summary>
        /// <param name="comprobante">el string en UTF-8 del cfdi</param>
        /// <returns>el string en UTF-8 con el comprobante timbrado</returns>
        public string TimbraCfdi(string comprobante)
        {
            CertificadorClient cliente = new CertificadorClient();
            try
            {
                return cliente.TimbraCfdi(comprobante);
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }

        }

        /// <summary>
        /// Método
        /// </summary>
        /// <param name="uuid">UUID del comprobante que se va a cancelar</param>
        /// <param name="rfc">RFC del emisor</param>
        /// <returns>Regresa el acuse de cancelación</returns>
        public string CancelaCfdi(string uuid, string rfc)
        {
            CertificadorClient cliente = new CertificadorClient();
            try
            {

                return cliente.CancelaCfdi(uuid, rfc);
            }

            catch (FaultException ee)
            {
                Logger.Error(ee.Message);
                return ee.Message;
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }

        }

        public string CancelaRetencion(string uuid, string rfc)
        {
            CertificadorClient cliente = new CertificadorClient();

            try
            {
                ServicePointManager.DefaultConnectionLimit = 200;
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) =>
                {
                    return true;
                };

                return cliente.CancelaRetencion(uuid, rfc);

            }

            catch (FaultException ee)
            {
                Logger.Error(ee.Message);
                return ee.Message;
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }

        }
    }
}
