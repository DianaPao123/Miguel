using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using ClienteNtLink;
using ClienteNtLink.TimbradoXpress;
using log4net;

namespace ClienteServiciosWEb
{
    public class ClienteTimbradoXpress
    {
        private static ILog Logger = LogManager.GetLogger(typeof(ClienteTimbradoXpress));

        string APIKey = ConfigurationManager.AppSettings["APIKey"];
        public string Codigo { get; set; }
        public string Mensaje { get; set; }
        string XML { get; set; }
        public string ClienteTimbradoXPRESS(string comprobante)
        {

            Logger.Info("Timbrando comprobante");
            ClienteNtLink.TimbradoXpress.ServicioTimbradoWSPortTypeClient cliente = new ClienteNtLink.TimbradoXpress.ServicioTimbradoWSPortTypeClient();
            var respuesta = cliente.timbrarTFD(APIKey, comprobante);
            Codigo = respuesta.code;
            Mensaje = respuesta.message;
            Logger.Info("Codigo:"+respuesta.code +" Mensaje:"+respuesta.message);
            return respuesta.data;

        }
        public string ClienteCancelarXPRESS(string keyCSD, string cerCSD, string passCSD,
               string uuid, string rfcEmisor, string rfcReceptor, double total, string motivo, string folioSustitucion)
        {

            Logger.Info("Cancelando comprobante");
            ClienteNtLink.TimbradoXpress.ServicioTimbradoWSPortTypeClient cliente = new ClienteNtLink.TimbradoXpress.ServicioTimbradoWSPortTypeClient();
            var respuesta = cliente.cancelar2(APIKey, keyCSD, cerCSD, passCSD, uuid, rfcEmisor, rfcReceptor, total, motivo, folioSustitucion);
            Codigo = respuesta.code;
            Mensaje = respuesta.message;
            Logger.Info("Codigo:" + respuesta.code + " Mensaje:" + respuesta.message);

            //eliminar.............
            //Codigo = "201";
            // Mensaje = "Cancelado";
            return respuesta.data;
        }
        public string ConsultaEstatusCFDI(string uuid, string rfcEmisor, string rfcReceptor, string total)
        {
            Logger.Info("consultando uuid en sat:"+uuid);
            ClienteNtLink.TimbradoXpress.ServicioTimbradoWSPortTypeClient cliente = new ClienteNtLink.TimbradoXpress.ServicioTimbradoWSPortTypeClient();
            var respuesta = cliente.consultarEstadoSAT(APIKey, uuid, rfcEmisor, rfcReceptor, total);
            Codigo = respuesta.CodigoEstatus;
            if(respuesta.CodigoEstatus == "S - Comprobante obtenido satisfactoriamente.")
            Mensaje = respuesta.Estado + "|"+respuesta.EsCancelable+"|"+ respuesta.EstatusCancelacion;
            Logger.Info("Codigo:" + Codigo + " Mensaje:" + Mensaje);


            //eliminar----------------------
            //Codigo = "S:Comprobante obtenido satisfactoriamente";
           // respuesta.Estado = "Cancelado";
           // respuesta.EsCancelable = "Cancelable sin Aceptación";
           // respuesta.EstatusCancelacion = "cancelado sin aceptación ";
           // Mensaje = respuesta.Estado + "|" + respuesta.EsCancelable + "|" + respuesta.EstatusCancelacion;
            //------------------------


            return respuesta.Estado;
        }
        public string ValidaTimbraCfdi(string comprobante)
        {
            return "OK";
            /*
            try
            {

              //  string data = "{\"detail\":[{\"detail\":[{\"message\":\"CFDI40123 - El campo Exportacion no contiene un valor del cat\u00e1logo c_Exportacion.\",\"messageDetail\":\"The required attribute 'Exportacion' is missing.\",\"type\":0,\"typeValue\":\"Error\"}],\"section\":\"CFDI40 - Validacion de Estructura\"},{\"detail\":[{\"message\":\"CFDI40102 - El resultado de la digesti\u00f3n debe ser igual al resultado de la desencripci\u00f3n del sello.\",\"messageDetail\":\"CadenaOriginal: || 4.0 | 000016 | 2022 - 06 - 02T19: 50:35 | 99 | 30001000000400002326 | 1.00 | MXN | 1.16 | I || PPD | 86690 | 01 | 3CEBCC92 - 4A33 - 49F7 - B817 - 04CB7B2DA449 | XAMA620210DQ5 | XAMA620210DQ5 | 605 | XAXX010101000 | PD USA ||| P01 | 78101802 | 1 | E48 | Unidad de servicio| 2 | 1.00 | 1.00 || 1 | 002 | Tasa | 0.160000 | 0.16 || 002 | Tasa | 0.16 || \",\"type\":0,\"typeValue\":\"Error\"},{\"message\":\"CFDI40123 - El campo Exportacion no contiene un valor del cat\u00e1logo c_Exportacion.\",\"messageDetail\":\"0  no se encuentra en el catalogo.\",\"type\":0,\"typeValue\":\"Error\"},{\"message\":\"CFDI40138 - El campo Nombre del emisor, no corresponde con el nombre del titular del certificado de sello digital del Emisor.\",\"messageDetail\":null,\"type\":0,\"typeValue\":\"Error\"},{\"message\":\"CFDI40149 - El campo DomicilioFiscalReceptor, no es igual al valor del campo LugarExpedicion.\",\"messageDetail\":null,\"type\":0,\"typeValue\":\"Error\"},{\"message\":\"CFDI40157 - El campo RegimenFiscalR, no contiene un valor del cat\u00e1logo c_RegimenFiscal.\",\"messageDetail\":null,\"type\":0,\"typeValue\":\"Error\"},{\"message\":\"CFDI40158 - La clave del campo RegimenFiscalR debe corresponder con el tipo de persona(f\u00edsica o moral).\",\"messageDetail\":\" ValorEsperado: 605 | 606 | 607 | 608 | 610 | 611 | 614 | 615 | 616 | 621 | 625 | 626 ValorReportado: 0\",\"type\":0,\"typeValue\":\"Error\"},{\"message\":\"CFDI40159 - La clave del campo RegimenFiscalR no corresponde de acuerdo al RFC del receptor.\",\"messageDetail\":\"El campo Comprobante.Receptor.Rfc es XAXX010101000 o XEXX010101000 y el campo Comprobante.Receptor.RegimenFiscalReceptor es diferente a 616 ValorEsperado: 616 ValorReportado: 0\",\"type\":0,\"typeValue\":\"Error\"},{\"message\":\"CFDI40160 - El campo UsoCFDI, no contiene un valor del cat\u00e1logo c_UsoCFDI.\",\"messageDetail\":null,\"type\":0,\"typeValue\":\"Error\"},{\"message\":\"CFDI40172 - El nodo hijo Impuestos del nodo concepto, no debe existir.\",\"messageDetail\":\"Concepto 2 tiene declarado el atributo ObjetoImp con 01 o 03 y nodo Concepto.Impuestos esta declarado.\",\"type\":0,\"typeValue\":\"Error\"},{\"message\":\"CFDI40205 - El valor del campo TotalImpuestosTrasladados no es igual a la suma de los importes registrados en el elemento hijo Traslado.\",\"messageDetail\":\" ValorEsperado: 0.16 ValorReportado: 0\",\"type\":0,\"typeValue\":\"Error\"}],\"section\":\"CFDI40 - Validaciones Proveedor Comprobante(CFDI40 )\"}],\"cadenaOriginalSAT\":\"\",\"cadenaOriginalComprobante\":\" || 4.0 | 000016 | 2022 - 06 - 02T19: 50:35 | 99 | 30001000000400002326 | 1.00 | MXN | 1.16 | I || PPD | 86690 | 01 | 3CEBCC92 - 4A33 - 49F7 - B817 - 04CB7B2DA449 | XAMA620210DQ5 | XAMA620210DQ5 | 605 | XAXX010101000 | PD USA ||| P01 | 78101802 | 1 | E48 | Unidad de servicio| 2 | 1.00 | 1.00 || 1 | 002 | Tasa | 0.160000 | 0.16 || 002 | Tasa | 0.16 || \",\"uuid\":\"\",\"statusSat\":\"No Aplica\",\"statusCodeSat\":\"No Aplica\",\"status\":\"success\"}";
                ClienteNtLink.TimbradoXpress.ServicioTimbradoWSPortTypeClient cliente = new ClienteNtLink.TimbradoXpress.ServicioTimbradoWSPortTypeClient();
                var res = cliente.validar(APIKey, comprobante);
                Codigo = res.code;
                Mensaje = res.message;
                if (Codigo == "200")
                {
                    string men = "";
                       JavaScriptSerializer serializer = new JavaScriptSerializer();
                    var jsonObject = serializer.Deserialize<ValidaXpress>(res.data);
                     for( int contador = jsonObject.detail.Count-1; contador >= 0; contador--)
                    {
                         men = jsonObject.detail[contador].detail[0].message;
                        if (men != "OK")
                            return men;
                                               
                    }
                    return "OK";
                }
                else
                    return "Error en el servicio del PAC:" + Mensaje;
            }
            catch (Exception ex)
            { Logger.Error(ex.Message);
                return "Error en el servicio del PAC:" + ex.Message;

            }
            */
        }

      
    }
}
