using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using System.IO;
using Excel;
using Contract;
using System.Globalization;


namespace Business
{
    public class PrefacturaExcel : GAFBusiness
    {
        //---------------------------------
        public DataTable ExtraerExcel(string filePath)
        {
            try
            {

                IExcelDataReader excelReader;

                FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
                //1. Reading from a binary Excel file ('97-2003 format; *.xls)
                if (Path.GetExtension(filePath).ToUpper() == ".XLS")
                {
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else if (Path.GetExtension(filePath).ToUpper() == ".XLSX")
                {
                    //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                else
                    return null;
                //3. DataSet - The result of each spreadsheet will be created in the result.Tables
                DataSet result = excelReader.AsDataSet();

                //3. DataSet - Create column names from first row
                excelReader.IsFirstRowAsColumnNames = false;
                //DataSet result = excelReader.AsDataSet();
                //5. Data Reader methods
                // while (excelReader.Read())
                //{
                //excelReader.GetInt32(0);
                //}
                DataTable dt = result.Tables[0];
                //6. Free resources (IExcelDataReader is IDisposable)
                
               
                 

                excelReader.Close();
                return dt;
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }

        }
        //---------------------------------------------
        public string GenerarPrefacturasMasivas(DataTable dt)
        {
            int i1 = 0; 
            string[][] userArr = new string[dt.Rows.Count][];

            foreach (DataRow row in dt.Rows)
            {
                  userArr[i1] = new string[row.ItemArray.Count()];
                 // var obj = row.ItemArray.ToArray();
                 //  userArr[i1] = ((System.Collections.IEnumerable)obj).Cast<object>().Select(x => x.ToString()).ToArray();
                 userArr[i1] = row.ItemArray.Cast<object>().Select(x => x.ToString()).ToArray();
        
                if (userArr[i1][0] == "FIN")
                {
                    //empesar a guardar
                    DatosPrefactura prefactura = obtenerDatos(userArr);
                   
                    userArr = new string[dt.Rows.Count][];
                    i1 = 0; 
                }
                else
                i1++;

            }
            return "OK";
        }
        //-----------------------------------------------------------------
        public string GenerarTransferenciasExcelMasivas(DataTable dt, System.Guid usuario)
        {
           string Error = "";
           int i=0;
            List<ConceptosCargados> concep = null;
         String Nada = "";
            var contains = dt.AsEnumerable().Where(row => Nada != row[0].ToString()).ToList();

            foreach (DataRow row in contains)
            {
                    string ID = row[0].ToString();
                    if (!string.IsNullOrEmpty(ID)&& ID!="ID")
                    {
                        string Empresa = row[6].ToString();
                        string Total = row[4].ToString();
                        string Cliente = row[2].ToString();

                        concep = null;
                        var conceptos = dt.AsEnumerable().Where(r =>ID == r[8].ToString()).ToList();
                      if (conceptos.Any())
                      {
                        concep= new List<ConceptosCargados>();
                        ConceptosCargados c = new ConceptosCargados();
                        foreach (DataRow row2 in conceptos)
                        {
                            c.Cantidad = row2[9].ToString();
                            c.Clave = row2[10].ToString();
                            c.Unidad = row2[11].ToString();
                            c.Precio = row2[12].ToString();
                            c.Descripcion = row2[13].ToString();
                            concep.Add(c);
                        }
                      }

                        if (!string.IsNullOrEmpty(Cliente) && !string.IsNullOrEmpty(Empresa))
                        {
                            string s = GuardarExcelTrasferencia(Empresa, Cliente, Total, usuario,concep);
                            if (s != "OK")
                                Error = Error + "<br/>" + s;
                        }
                    }
                   
              

            }
            if (Error != "")
                return Error;
            else
            return "OK";
        }

        private string GuardarExcelTrasferencia(string empresa,string cliente, string total, System.Guid usuario, List<ConceptosCargados> C)
        {
            GAFClientes clientes = new GAFClientes();
            GAFEmpresa empresas = new GAFEmpresa();
           // var cli= clientes.GetClienteRazonSocial(cliente);
            var cli = clientes.GetCliente(cliente);
            
            if (cli == null)
                return "El cliente: " + cliente + " no se encuentra en la base";
            //var emp= empresas.GetByRazonSocial(empresa); 
            var emp = empresas.GetRfc(empresa); 

            if(emp==null)
                return "La empresa: " + empresa + " no se encuentra en la base";

            try
            {
                TransferenciasExcel Tras = new TransferenciasExcel();
                Tras.IdCliente = cli.idCliente;
                Tras.IdEmpres = emp.IdEmpresa;
                Tras.RazonSocialCliente = cli.RazonSocial;
                Tras.RazonSocialEmpresa = emp.RazonSocial;
                Tras.Total = Convert.ToDecimal(total);
                Tras.usuario = usuario;
                using (var db = new GAFEntities())
                {
                    db.TransferenciasExcel.AddObject(Tras);
                    db.SaveChanges();

                    if(C!=null)
                    foreach (var c in C)
                    {
                        TransferenciasExcelConceptos Tr = new TransferenciasExcelConceptos();
                        Tr.IDTrasferencias = (int)Tras.ID;
                        Tr.ValorUnitario =Convert.ToDecimal(c.Precio);
                        Tr.Descripcion = c.Descripcion;
                        Tr.ClaveUnidad = c.Unidad;
                        Tr.ClaveProdServ = c.Clave;
                        Tr.Cantidad =(decimal) Convert.ToDecimal(c.Cantidad);
                         db.TransferenciasExcelConceptos.AddObject(Tr);
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                return "Error al guardar en base:" + ee.Message;
            }

            return "OK";
        }
        //---------------------------------------------------------

        public DatosPrefactura obtenerDatos(string[][] datos)
        {
            
            #region Obtención de datos
            var comprob = datos.FirstOrDefault(p => p[0] == "COMP");
            var CfdiRelacionados = datos.FirstOrDefault(p => p[0] == "CRT");//nuevo
            var UUID = datos.Where(p => p[0] == "CRU");//nuevo
            var emisor = datos.FirstOrDefault(p => p[0] == "E");
            var receptor = datos.FirstOrDefault(p => p[0] == "R");
            var concepts = datos.Where(p => p[0] == "C");
            var InformAdua = datos.Where(p => p[0] == "IAC");//nuevo
            var Traslados = datos.Where(p => p[0] == "ITC");//nuevo
            var Retenciones = datos.Where(p => p[0] == "IRC");//nuevo
            var Parte = datos.Where(p => p[0] == "PC");//nuevo
            var IAParte = datos.Where(p => p[0] == "IAPC");//nuevo
            var ITraslados = datos.Where(p => p[0] == "IT");//nuevo
            var IRetenciones = datos.Where(p => p[0] == "IR");//nuevo
            var ITotales = datos.FirstOrDefault(p => p[0] == "TIMP");//nuevo
                    
               
            var impuestosTraslado = datos.Where(p => p[0] == "IT");
            var impuestosRetencion = datos.Where(p => p[0] == "IR");
            //---------nuevos
            var impuestosLocales = datos.FirstOrDefault(p => p[0] == "IL");
            var impuestosLocalesretenciones = datos.Where(p => p[0] == "ILR");
            var impuestosLocalestraslados = datos.Where(p => p[0] == "ILTL");

            //---------------------------------------------
            var cabeceraLeyendasFiscales = datos.FirstOrDefault(j => j[0] == "LeyendasFiscales");
            var disposicionFiscal = datos.Where(j => j[0] == "LeyendasFiscLeyenda");

            //-----------complementos--------------------------------
            var pagos = datos.Where(p => p[0] == "PAG");
            var documentos = datos.Where(p => p[0] == "DRPAG");
            var impPagosDocu = datos.Where(p => p[0] == "TIMPPAG");
            var impPagosRetencionDocu = datos.Where(p => p[0] == "IRPAG");
            var impPagosTrasladoDocu = datos.Where(p => p[0] == "ITPAG");
        

            //----------------------------------------------
            var datosAdicionales = datos.FirstOrDefault(j => j[0] == "AD");
            var fin = datos.FirstOrDefault(j => j[0] == "FIN");
            if (fin == null)
            {
              //  throw new IncompleteException();
            }
            DatosPrefactura comprobante = new DatosPrefactura();
           // Comprobante comprobante = new Comprobante();
            #endregion
            #region Emisor
            
            comprobante.IdEmpresa=Convert.ToInt32 (emisor[1]);
          
            #endregion
           
            #region Receptor
           comprobante.idcliente =Convert.ToInt32 ( receptor[1]);
             
                    
            #endregion
            #region Total, subtotal, moneda, forma de pago, serie, leyendas, notas, descuentos
            if(!string.IsNullOrEmpty(comprob[1]))
            comprobante.Serie = comprob[1];

            if (!string.IsNullOrEmpty(comprob[2]))
            comprobante.Folio = comprob[2];
            comprobante.Fecha = Convert.ToDateTime(comprob[3]);
            comprobante.LugarExpedicion = comprob[4];
            comprobante.TipoDeComprobante = comprob[5];
            if (!string.IsNullOrEmpty(comprob[6]))
             comprobante.FormaPago = comprob[6];
                 
            if (!string.IsNullOrEmpty(comprob[7]))
                   comprobante.MetodoID = comprob[7];
            
            if (!string.IsNullOrEmpty(comprob[8]))
                comprobante.CondicionesPago = comprob[8];
            comprobante.SubTotal = decimal.Parse(comprob[9]);// factura.Factura.Total.Value - factura.Factura.IVA.Value + factura.Factura.RetenciónIva;
            if (!string.IsNullOrEmpty(comprob[10]))
             comprobante.Descuento =decimal.Parse( comprob[10]);
          
            comprobante.Total = decimal.Parse(comprob[11]);
            comprobante.MonedaID = comprob[12];
            if (!string.IsNullOrEmpty(comprob[13]))
                comprobante.TipoCambio = comprob[13];
            
            if (!string.IsNullOrEmpty(comprob[14]))
                comprobante.Confirmacion = comprob[14];
                                
           
            #endregion
            /*
            #region CfdiRelacionados
            if(CfdiRelacionados!=null)
            if (CfdiRelacionados.Any())
            { 
                if (comprobante.CfdiRelacionados == null)
                    comprobante.CfdiRelacionados = new ComprobanteCfdiRelacionados();
                comprobante.CfdiRelacionados.TipoRelacion = CfdiRelacionados[1];
            }
            #endregion
            #region CfdiRelacionadosUUDI
            if (UUID.Any())
            {
                if (comprobante.CfdiRelacionados == null)
                    comprobante.CfdiRelacionados = new ComprobanteCfdiRelacionados();
                if (comprobante.CfdiRelacionados.CfdiRelacionado == null)
                    comprobante.CfdiRelacionados.CfdiRelacionado = new List<ComprobanteCfdiRelacionadosCfdiRelacionado>();
                foreach (var uudi in UUID)
                {
                    ComprobanteCfdiRelacionadosCfdiRelacionado u=new ComprobanteCfdiRelacionadosCfdiRelacionado();
                    u.UUID=uudi[1];
                    comprobante.CfdiRelacionados.CfdiRelacionado.Add(u);
                }
            }
            #endregion
            #region Datos adicionales
            if (datosAdicionales != null)
            {
                DatosAdicionales da = new DatosAdicionales();
                da.CalleEmisor = datosAdicionales[1];
                da.NumExterior = datosAdicionales[2];
                da.NumInterior = datosAdicionales[3];
                da.Colonia = datosAdicionales[4];
                da.Municipio = datosAdicionales[5];
                da.Estado = datosAdicionales[6];
                da.Pais = datosAdicionales[7];
                da.CodigoPostal = datosAdicionales[8];
                da.Localidad = datosAdicionales[9];
                da.CondicionesPago = datosAdicionales[10];
                da.NumOportunidad = datosAdicionales[11];
                da.OrdenCompra = datosAdicionales[12];
                da.NombreContacto = datosAdicionales[13];
                da.Vendedor = datosAdicionales[14];
                comprobante.DatosAdicionales = da;

            }
          
            #endregion
            #region Conceptos
            List<GeneradorCfdi.ComprobanteConcepto> conceptos = new List<GeneradorCfdi.ComprobanteConcepto>();
            // List<GeneradorCfdi.t_InformacionAduanera> informacionAduanera = new List<GeneradorCfdi.t_InformacionAduanera>();
            foreach (var detalle in concepts)
            {
                GeneradorCfdi.ComprobanteConcepto con = new GeneradorCfdi.ComprobanteConcepto();
                con.ClaveProdServ = GetValue2(detalle[2]);
                if (!string.IsNullOrEmpty(detalle[3]))
                 con.NoIdentificacion = detalle[3];
                con.Cantidad = decimal.Parse(detalle[4]);
                con.ClaveUnidad = detalle[5];
                if (!string.IsNullOrEmpty(detalle[6]))
                    con.Unidad = detalle[6];
                con.Descripcion = GetValue2(detalle[7]);
                con.ValorUnitario =detalle[8];
                con.Importe = detalle[9];
                if (!string.IsNullOrEmpty(detalle[10]))
                {
                    con.DescuentoSpecified = true;
                    con.Descuento = detalle[10];
                }
                else
                    con.DescuentoSpecified = false;
                if (!string.IsNullOrEmpty(detalle[11]))
                {
                    if (con.CuentaPredial == null)
                        con.CuentaPredial = new GeneradorCfdi.ComprobanteConceptoCuentaPredial();
                    con.CuentaPredial.Numero = detalle[11];
                }
                if (!string.IsNullOrEmpty(detalle[12]))
                    con.Detalles = detalle[12];

                #region informacionAduanera

                //-------------------------informacion aduanera------------------
                var IA = InformAdua.Where(j => (j[1] == detalle[1]));
                if (IA.Any())
                {
                    List<ComprobanteConceptoInformacionAduanera> infAdu = new List<ComprobanteConceptoInformacionAduanera>();
                    foreach (var ia in IA)
                    {
                        ComprobanteConceptoInformacionAduanera i = new ComprobanteConceptoInformacionAduanera();
                        i.NumeroPedimento = ia[2];
                        infAdu.Add(i);
                    }
                    con.InformacionAduanera = infAdu.ToArray();

                }
                #endregion
                #region traslados

                //-----------------------------traslados-------------------------
                var Tras = Traslados.Where(j => (j[1] == detalle[1]));
                if (Tras.Any())
                {
                    if(con.Impuestos==null)
                    con.Impuestos = new ComprobanteConceptoImpuestos();
                    List<ComprobanteConceptoImpuestosTraslado> LT = new List<ComprobanteConceptoImpuestosTraslado>();
                    foreach (var t in Tras)
                    {
                        ComprobanteConceptoImpuestosTraslado lt = new ComprobanteConceptoImpuestosTraslado();
                        lt.Base =Convert.ToDecimal( t[2]);
                        lt.Impuesto = t[3];
                        lt.TipoFactor = t[4];
                        if (!string.IsNullOrEmpty(t[6]))
                        {
                            lt.TasaOCuota = t[5];
                            lt.TasaOCuotaSpecified = true;
                        }
                        else
                            lt.TasaOCuotaSpecified = false;
                        if (!string.IsNullOrEmpty(t[6]))
                        {
                            lt.ImporteSpecified = true;
                            lt.Importe = t[6];
                        }
                        else
                            lt.ImporteSpecified = false;
                        LT.Add(lt);

                    }
                    con.Impuestos.Traslados = LT.ToArray();

                }
                #endregion
                #region Retenciones

                //----------------------------Retenciones-----------------------------
                var Ret = Retenciones.Where(j => (j[1] == detalle[1]));
                if (Ret.Any())
                {
                    if (con.Impuestos == null)
                        con.Impuestos = new ComprobanteConceptoImpuestos();
                    List<ComprobanteConceptoImpuestosRetencion> LR = new List<ComprobanteConceptoImpuestosRetencion>();
                    foreach (var r in Ret)
                    {
                        ComprobanteConceptoImpuestosRetencion lr = new ComprobanteConceptoImpuestosRetencion();
                        lr.Base = Convert.ToDecimal(r[2]);
                        lr.Impuesto = r[3];
                        lr.TipoFactor = r[4];
                        lr.TasaOCuota = r[5];
                        lr.Importe = r[6];
                        LR.Add(lr);

                    }
                    con.Impuestos.Retenciones = LR.ToArray();


                }
                #endregion
                #region Parte

                //------------------------------Parte---------------------------
                var P = Parte.Where(j => (j[2] == detalle[1]));
                if (P.Any())
                {
                    List<GeneradorCfdi.ComprobanteConceptoParte> LP = new List<GeneradorCfdi.ComprobanteConceptoParte>();
                    foreach (var p in P)
                    {
                        GeneradorCfdi.ComprobanteConceptoParte lp = new GeneradorCfdi.ComprobanteConceptoParte();
                        lp.ClaveProdServ = p[3];
                        if (!string.IsNullOrEmpty(p[4]))
                          lp.NoIdentificacion = p[4];
                        lp.Cantidad =Convert.ToDecimal( p[5]);
                        lp.Unidad = p[6];
                        lp.Descripcion = p[7];
                        if (!string.IsNullOrEmpty(p[8]))
                        {
                            lp.ValorUnitario =Convert.ToDecimal( p[8]);
                            lp.ValorUnitarioSpecified = true;
                        }
                        else
                            lp.ValorUnitarioSpecified = false;
                        if (!string.IsNullOrEmpty(p[9]))
                        {

                            lp.Importe =Convert.ToDecimal( p[9]);
                            lp.ImporteSpecified = true;
                        }
                        else
                            lp.ImporteSpecified = false;
                        //-------------------------------------------
                        var IAP= IAParte.Where(j => (j[1] == p[1]));
                        if (IAP.Any())
                        {
                            if (lp.InformacionAduanera==null)
                            lp.InformacionAduanera = new List<ComprobanteConceptoParteInformacionAduanera>();
                            List<ComprobanteConceptoParteInformacionAduanera> infAduP = new List<ComprobanteConceptoParteInformacionAduanera>();
                            foreach (var iap in IAP)
                            {
                                ComprobanteConceptoParteInformacionAduanera i = new ComprobanteConceptoParteInformacionAduanera();
                                i.NumeroPedimento = iap[2];
                                infAduP.Add(i);
                            }
                           lp.InformacionAduanera = infAduP;
                  
                        }
              
                        //-----------------------------
                        LP.Add(lp);
                       
                    }
                    con.Parte = LP.ToArray();

                }
                #endregion

                //--------------------------------------------------------------------
              
                conceptos.Add(con);

            }
            comprobante.Conceptos = conceptos.ToArray();
            #endregion
            #region Retencion 
            if (IRetenciones.Any())
            {
                if (comprobante.Impuestos == null)
                    comprobante.Impuestos = new GeneradorCfdi.ComprobanteImpuestos();

                List<GeneradorCfdi.ComprobanteImpuestosRetencion> LT = new List<GeneradorCfdi.ComprobanteImpuestosRetencion>();
                foreach (var t in IRetenciones)
                {
                    GeneradorCfdi.ComprobanteImpuestosRetencion  lt = new GeneradorCfdi.ComprobanteImpuestosRetencion();
                    lt.Impuesto = t[1];
                   lt.Importe = t[2];
                    LT.Add(lt);

                }
                comprobante.Impuestos.Retenciones = LT.ToArray();
            }

            #endregion
            #region Traslados

            if (ITraslados.Any())
            {
                if (comprobante.Impuestos == null)
                    comprobante.Impuestos = new GeneradorCfdi.ComprobanteImpuestos();

                List<GeneradorCfdi.ComprobanteImpuestosTraslado> LT = new List<GeneradorCfdi.ComprobanteImpuestosTraslado>();
                foreach (var t in ITraslados)
                {
                    GeneradorCfdi.ComprobanteImpuestosTraslado lt = new GeneradorCfdi.ComprobanteImpuestosTraslado();
                    lt.Impuesto = t[1];
                    lt.TipoFactor = t[2];
                    lt.TasaOCuota = t[3];
                    lt.Importe = t[4];
                    LT.Add(lt);

                }
                comprobante.Impuestos.Traslados = LT.ToArray();
            }
            #endregion
            #region Totales Retencion Traslados
            if (ITotales != null)
            {
                if (comprobante.Impuestos == null)
                    comprobante.Impuestos = new GeneradorCfdi.ComprobanteImpuestos();
                if (!string.IsNullOrEmpty(ITotales[2]))
                {
                    comprobante.Impuestos.TotalImpuestosRetenidosSpecified = true;
                    comprobante.Impuestos.TotalImpuestosRetenidos = ITotales[2];
                }
                else
                    comprobante.Impuestos.TotalImpuestosRetenidosSpecified = false;
                if (!string.IsNullOrEmpty(ITotales[1]))
                {
                    comprobante.Impuestos.TotalImpuestosTrasladadosSpecified = true;
                    comprobante.Impuestos.TotalImpuestosTrasladados = ITotales[1];
                }
                else
                    comprobante.Impuestos.TotalImpuestosTrasladadosSpecified = false;
            }

            #endregion
            #region Impuestos, cantidad en letra
          
            comprobante.CantidadLetra = CantidadLetra.Enletras(comprobante.Total.ToString(), comprobante.Moneda, comprobante.Emisor.Rfc);

            
            //comprobante.Impuestos = impuestos;
            #endregion

            #region Obtención de leyendas fiscales

            //----------------------esto es nuevo para los leyendasFisc
            if (cabeceraLeyendasFiscales != null)
            {
                LeyendasFiscales H = new LeyendasFiscales();
                H.version = cabeceraLeyendasFiscales[1];
                List<LeyendasFiscalesLeyenda> LFL = new List<LeyendasFiscalesLeyenda>();


                foreach (var can in disposicionFiscal)
                {
                    LeyendasFiscalesLeyenda L = new LeyendasFiscalesLeyenda();

                    L.disposicionFiscal = can[1];
                    L.norma = can[2];
                    L.textoLeyenda = can[3];
                    LFL.Add(L);

                }
                H.Leyenda = LFL.ToArray();
                if (comprobante.Complemento == null)
                    comprobante.Complemento = new GeneradorCfdi.ComprobanteComplemento();
                comprobante.Complemento.leyendasFicales = new LeyendasFiscales();
                comprobante.Complemento.leyendasFicales = H;
            }
         
            //----------------------esto es nuevo para los impuestos locales
            if (impuestosLocales != null)

            {
                comprobante.Complemento = new GeneradorCfdi.ComprobanteComplemento();
                comprobante.Complemento.implocal = new ImpuestosLocales();

                comprobante.Complemento.implocal.TotaldeRetenciones = Convert.ToDecimal(impuestosLocales[1]);
                comprobante.Complemento.implocal.TotaldeTraslados = Convert.ToDecimal(impuestosLocales[2]);
                comprobante.Complemento.implocal.version = impuestosLocales[3];
              //  comprobante.Complemento.imlocal.TrasladosLocales = new ImpuestosLocalesTrasladosLocales();
                //--------------------------------------------------------
                if (impuestosLocalestraslados.Any())
                {
                    List<ImpuestosLocalesTrasladosLocales> LITL = new List<ImpuestosLocalesTrasladosLocales>();
                    foreach (var imt in impuestosLocalestraslados)
                    {
                        ImpuestosLocalesTrasladosLocales litl = new ImpuestosLocalesTrasladosLocales();
                        litl.ImpLocTrasladado = imt[1];
                        litl.Importe = Convert.ToDecimal(imt[2]);
                        litl.TasadeTraslado = Convert.ToDecimal(imt[3]);

                        LITL.Add(litl);
                    }
                    comprobante.Complemento.implocal.TrasladosLocales = LITL.ToArray();

                }
                //-------------------------------------------------------------
                if (impuestosLocalesretenciones.Any())
                {
                    //comprobante.Complemento.imlocal.RetencionesLocales = new ImpuestosLocalesRetencionesLocales();
                    List<ImpuestosLocalesRetencionesLocales> LIL = new List<ImpuestosLocalesRetencionesLocales>();
                    foreach (var im in impuestosLocalesretenciones)
                    {
                        ImpuestosLocalesRetencionesLocales lil = new ImpuestosLocalesRetencionesLocales();
                        lil.ImpLocRetenido = im[1];
                        lil.Importe = Convert.ToDecimal(im[2]);
                        lil.TasadeRetencion = Convert.ToDecimal(im[3]);
                        LIL.Add(lil);
                    }
                    comprobante.Complemento.implocal.RetencionesLocales = LIL.ToArray();
                }
            }
            //---------------fin de impuestos locales (nuevo)
            #endregion
            */
            //---------------------------complementos-----------------------------------
         /*   
          #region complementoPagos

            if (pagos.Any())
            {
                Pagos P= new Pagos();
                P.Version = "1.0";
                PagosPago PP = new PagosPago();
                foreach(var pa in pagos)
                {
                  PP.FechaPago = pa[2];  
                  PP.FormaDePagoP=pa[3];
                  PP.MonedaP=pa[4];
                  if (!string.IsNullOrEmpty(pa[5]))
                  {
                      PP.TipoCambioP=Convert.ToDecimal( pa[5]);
                      PP.TipoCambioPSpecified=true;
                  }
                  else
                      PP.TipoCambioPSpecified=false;
                    PP.Monto=Convert.ToDecimal(pa[6]);
                     if (!string.IsNullOrEmpty(pa[7]))
                     PP.NumOperacion=pa[7];
                     if (!string.IsNullOrEmpty(pa[8]))
                   PP.RfcEmisorCtaOrd=pa[8];
                     if (!string.IsNullOrEmpty(pa[9]))
                   PP.NomBancoOrdExt=pa[9];
                  if (!string.IsNullOrEmpty(pa[10]))
                   PP.CtaOrdenante=pa[10];
                   if (!string.IsNullOrEmpty(pa[11]))
                   PP.RfcEmisorCtaBen=pa[11];
                   if (!string.IsNullOrEmpty(pa[12]))
                   PP.CtaBeneficiario=pa[12];
                   if (!string.IsNullOrEmpty(pa[13]))
                   { PP.TipoCadPagoSpecified=true;
                   PP.TipoCadPago=pa[13];
                   }
                   else
                    PP.TipoCadPagoSpecified=false;
                     if (!string.IsNullOrEmpty(pa[14]))
                   PP.CertPago=pa[14];
                     if (!string.IsNullOrEmpty(pa[15]))
                   PP.CadPago=pa[15];
                  if (!string.IsNullOrEmpty(pa[16]))
                   PP.SelloPago=pa[16];
                    
                  //------------------------------------
                  #region documentos

                  var doc = documentos.Where(j => (j[1] == pa[1]));
                    if (doc.Any())
                    {
                       List< PagosPagoDoctoRelacionado> D = new  List<PagosPagoDoctoRelacionado>();
                        foreach (var d in doc)
                        {
                           PagosPagoDoctoRelacionado docu = new PagosPagoDoctoRelacionado();
                            docu.IdDocumento = d[2];
                            if (!string.IsNullOrEmpty(d[3]))
                                docu.Serie = d[3];
                            if (!string.IsNullOrEmpty(d[4]))
                                docu.Folio = d[4];
                            docu.MonedaDR = d[5];
                            if (!string.IsNullOrEmpty(d[6]))
                            {
                                docu.TipoCambioDRSpecified = true;
                                docu.TipoCambioDR =Convert.ToDecimal( d[6]);
                            }
                            else
                                docu.TipoCambioDRSpecified = false;
                            docu.MetodoDePagoDR = d[7];
                            if (!string.IsNullOrEmpty(d[8]))
                                docu.NumParcialidad = d[8];
                            if (!string.IsNullOrEmpty(d[9]))
                            {
                                docu.ImpSaldoAntSpecified = true;
                                docu.ImpSaldoAnt = Convert.ToDecimal(d[9]);
                            }
                            else
                                docu.ImpSaldoAntSpecified = false;
                            if (!string.IsNullOrEmpty(d[10]))
                            {
                                docu.ImpPagadoSpecified = true;
                                docu.ImpPagado = Convert.ToDecimal(d[10]);
                            }
                            else
                            docu.ImpPagadoSpecified = false;
                            if (!string.IsNullOrEmpty(d[11]))
                            {
                                docu.ImpSaldoInsolutoSpecified = true;
                                docu.ImpSaldoInsoluto =  Convert.ToDecimal(d[11]);
                            }
                            else
                                docu.ImpSaldoInsolutoSpecified = false;

                            D.Add(docu);
                        }
                        if (D != null)
                            if (D.Count() > 0)
                                PP.DoctoRelacionado = D.ToArray();
                    }
                  #endregion
                    //----------------------------------
                       #region TotalImpuestosPagos

                    var IPD = impPagosDocu.Where(j => (j[1] == pa[1]));
                    if (IPD.Any())
                  {
                      List<PagosPagoImpuestos> PI = new List<PagosPagoImpuestos>();
                      
                        foreach (var ip in IPD)
                      {
                          PagosPagoImpuestos pi = new PagosPagoImpuestos();
                          if (!string.IsNullOrEmpty(ip[3]))
                          {
                              pi.TotalImpuestosTrasladadosSpecified = true;
                              pi.TotalImpuestosTrasladados = Convert.ToDecimal(ip[3]);
                          }
                          else
                              pi.TotalImpuestosTrasladadosSpecified = false;
                          if (!string.IsNullOrEmpty(ip[4]))
                          {
                              pi.TotalImpuestosRetenidosSpecified = true;
                              pi.TotalImpuestosRetenidos = Convert.ToDecimal(ip[4]);
                          }
                          else
                              pi.TotalImpuestosRetenidosSpecified = false;
                          //------------------------------------------
                          #region ImpuestosRetenidosPagos
                          var RP = impPagosRetencionDocu.Where(j => (j[1] == ip[2]));
                                if (RP.Any())
                                {
                                    List<PagosPagoImpuestosRetencion> R = new List<PagosPagoImpuestosRetencion>();
                                    foreach (var rp in RP)
                                    {
                                        PagosPagoImpuestosRetencion r = new PagosPagoImpuestosRetencion();
                                      
                                        r.Impuesto = rp[2];
                                        r.Importe = Convert.ToDecimal(rp[3]);
                                        R.Add(r);
                                    }
                                    if (R != null)
                                        if (R.Count() > 0)
                                            pi.Retenciones = R;
                                }
                          #endregion
                          //---------------------------------------

                                #region ImpuestosTrasladosPagos
                                var TP = impPagosTrasladoDocu.Where(j => (j[1] == ip[2]));
                                if (TP.Any())
                                {
                                    List<PagosPagoImpuestosTraslado> T = new List<PagosPagoImpuestosTraslado>();
                                    foreach (var tp in TP)
                                    {
                                        PagosPagoImpuestosTraslado t = new PagosPagoImpuestosTraslado();

                                        t.Impuesto = tp[2];
                                        t.TipoFactor = tp[3];
                                        t.TasaOCuota = Convert.ToDecimal( tp[4]);
                                        t.Importe = Convert.ToDecimal(tp[5]);
                                        T.Add(t);
                                    }
                                    if (T != null)
                                        if (T.Count() > 0)
                                            pi.Traslados = T;
                                }
                                #endregion
                            //------------------------------------
                          PI.Add(pi);
                      }
                        if (PI != null)
                            if (PI.Count() > 0)
                                PP.Impuestos = PI.ToArray();
                  }

                       #endregion
                           if(P.Pago==null)
                            P.Pago = new List<PagosPago>();
                            P.Pago.Add(PP);
                        
                }
                if (comprobante.Complemento == null)
                    comprobante.Complemento = new GeneradorCfdi.ComprobanteComplemento();
                if (comprobante.Complemento.Pag == null)
                    comprobante.Complemento.Pag = new Pagos();
                comprobante.Complemento.Pag = P;
            }
           
                #endregion
          */ 
            //-------------------------fin complementos---------------------------------
            return comprobante;

        

        }
        //--------------------------------------------
    }

}
