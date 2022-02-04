using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.ServiceModel;



namespace Contract
{
    public class Prefactura : GAFContract
    {
        public long idComprobante { get; set; } 

        public void Cambiarestatus(long idPrefactura, int status)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    PreFactura fac = db.PreFactura.Single(l => l.idPreFactura == idPrefactura);
                    fac.Estatus = status;
                    db.PreFactura.ApplyCurrentValues(fac);
                    db.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                throw;
            }
        }

        public void CambiarestatusDevolucionPago(long idpago, bool status)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    PrePagos fac = db.PrePagos.Single(l => l.idPagos == idpago);
                    fac.EsRegistrado = status;
                    db.PrePagos.ApplyCurrentValues(fac);
                    db.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                throw;
            }
        }
        public List< PreComplementoPago> GetPreComplementoPagos(long idPrefactura)
        {

            try
            {
                using (var db = new GAFEntities())
                {
                    var factura = db.PreComplementoPago.Where(c => c.idPreFactura == idPrefactura);
                    return factura.ToList();
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
        public List<PreFCPDoctoRelacionado> GetPreFCPDoctoRelacionado(long IdPreComplementoPago)
        {

            try
            {
                using (var db = new GAFEntities())
                {
                    var factura = db.PreFCPDoctoRelacionado.Where(c => c.IdPreComplementoPago == IdPreComplementoPago);
                    return factura.ToList();
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
        public PreFactura GetCliente(long idPrefactura)
        {

            try
            {
                using (var db = new GAFEntities())
                {
                    var factura = db.PreFactura.Where(c => c.idPreFactura == idPrefactura);
                    return factura.FirstOrDefault();
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
        public List<PreConceptos> GetConceptos(long idPrefactura)
        {

            try
            {
                using (var db = new GAFEntities())
                {
                    var factura = db.PreConceptos.Where(c => c.idPreFactura == idPrefactura);
                    return factura.ToList();
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

        public List<PreImpuestos> GetImpuestos(long idConceptos)
        {

            try
            {
                using (var db = new GAFEntities())
                {
                    var factura = db.PreImpuestos.Where(c => c.IdPreConceptos == idConceptos);
                    return factura.ToList();
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
        public List<PreCfdiRelacionado> GetPreCfdiRelacionado(long idPrefactura)
        {

            try
            {
                using (var db = new GAFEntities())
                {
                    var factura = db.PreCfdiRelacionado.Where(c => c.IdPrefactura == idPrefactura);
                    return factura.ToList();
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
        /*
        public List<PrePagos> GetPrePagos(long idPrefactura)
        {

            try
            {
                using (var db = new GAFEntities())
                {
                    var factura = db.PrePagos.Where(c => c.idPrefactura == idPrefactura);
                    return factura.ToList();
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

        public List<PreComplementoImpLocal> GetPreComplementoImpLocal(long idPrefactura)
        {

            try
            {
                using (var db = new GAFEntities())
                {
                    var factura = db.PreComplementoImpLocal.Where(c => c.idPreFactura == idPrefactura);
                    return factura.ToList();
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

        public PreFactura  GetFolio(string folio)
        {

            try
            {
                using (var db = new GAFEntities())
                {
                    var factura = db.PreFactura.Where(c => c.Folio == folio);
                    return factura.FirstOrDefault();
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

        public string GetNextFolio(int idEmpresa)
        {
            try
            {
                using (var db = new GAFEntities())
                {

                    // var empre = db.empresa.FirstOrDefault(p => p.IdEmpresa == idEmpresa);//nuevo
                    var empre = db.empresa.Where(p => p.IdEmpresa == idEmpresa).FirstOrDefault();//nuevo

                    string folio = "0";//nuevo
                    if (empre != null) //nuevo
                    {  //nuevo
                        if (!string.IsNullOrEmpty(empre.Folio))//nuevo
                            folio = empre.Folio;//nuevo

                    } //nuevo            
                    
                    Int64 i = 0;//nuevo
                    i = Int64.Parse(folio);//nuevo

                    i++;
                    return i.ToString().PadLeft(6, '0');

                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }
        }

        public string GetNextPreFolio(string Letra,int x)
        {
            try
            {
                using (var db = new GAFEntities())
                {

                    int año=DateTime.Now.Year;
                    int mes=DateTime.Now.Month;
                    // var empre = db.empresa.FirstOrDefault(p => p.IdEmpresa == idEmpresa);//nuevo
                    var prefolios = db.PreFolios.Where(p => p.Id == 1).FirstOrDefault();//nuevo

                    string folio = "0";//nuevo
                    if (prefolios != null) //nuevo
                    {  //nuevo
                       // if (!string.IsNullOrEmpty(prefolios.Prefolio))//nuevo
                        if (Letra == "F" || Letra == "NC")
                        folio = prefolios.Prefolio.ToString();//nuevo
                     // if (Letra == "NC")
                     //     folio = prefolios.NCfolio.ToString();//nuevo
                      if (Letra == "P")
                          folio = prefolios.Pfolio.ToString();//nuevo


                    } //nuevo            

                    Int64 i = 0;//nuevo
                    i = Int64.Parse(folio);//nuevo
                    if(x==0)
                    i++;
                    string consecutivo= i.ToString().PadLeft(5, '0');
                    return Letra+"-" + mes + "-" + año + "-" + consecutivo;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                return null;
            }
        }
    
        public long SavePrefactura(PreFactura factura)
        {

            try
            {
               
                    using (var db = new GAFEntities())
                    {
                        if (factura.idCliente == 0)
                        {
                            db.PreFactura.AddObject(factura);
                        }
                        else
                        {
                            var y = db.PreFactura.Where(p => p.idPreFactura == factura.idPreFactura).FirstOrDefault();
                            db.PreFactura.ApplyCurrentValues(factura);
                        }
                        db.SaveChanges();
                        return factura.idPreFactura;
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

        public int GuardarPrefacturaTimbre(long idPreFactura)
        {

            try
            {
                  using (var db = new GAFEntities())
                {


                   var factura= db.PreFactura.FirstOrDefault(p => p.idPreFactura == idPreFactura);
                          factura.Timbrado = true;
                            db.PreFactura.ApplyCurrentValues(factura);
                            db.SaveChanges();
                }
                  return 1;  
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

        public long GuardarPrefactura(DatosPrefactura factura, long idPrefactura, facturaComplementos complementosF)
        {

            try
            {
               

                using (var db = new GAFEntities())
                {

                    
                  //  empresa em = db.empresa.Where(p => p.IdEmpresa == factura.IdEmpresa).FirstOrDefault();
                  //  em.Folio = factura.Folio;
                  //  db.SaveChanges();
                    //-------------
                   
                    //--------------
                    PreFactura P = ConvertirPrefactura(factura);
                   
                    if (idPrefactura == 0)
                    {

                        P.PreFolio = ActualizaPrefolio(factura.PreFolio);
                        db.PreFactura.AddObject(P);
                        
                    }
                    else
                    {
                        
                        PreFactura PF = new PreFactura();
                        P.idPreFactura = idPrefactura;
                        //------
                        PF=db.PreFactura.FirstOrDefault(p => p.idPreFactura == idPrefactura);
                        if (PF.pagoVerificado != null)//...
                        {
                            P.pagoVerificado = PF.pagoVerificado;//---
                            if (P.pagoVerificado != 1)
                                P.pagoVerificado = null;
                        }
                        if(PF.TipoDeComprobante!=P.TipoDeComprobante)
                            P.PreFolio = ActualizaPrefolio(factura.PreFolio);
                        //-------
                        db.PreFactura.FirstOrDefault(p => p.idPreFactura == idPrefactura);
                        db.PreFactura.ApplyCurrentValues(P);
                        List<PreConceptos> con = db.PreConceptos.Where(p => p.idPreFactura == idPrefactura).ToList();
                        foreach (var c in con)
                        {
                            List<PreImpuestos> imp = db.PreImpuestos.Where(p => p.IdPreConceptos == c.idPreConcepto).ToList();
                            foreach (var i in imp)
                            {
                                db.PreImpuestos.DeleteObject(i);
                            }
                            db.PreConceptos.DeleteObject(c);
                        }
                        List<PreCfdiRelacionado> pre = db.PreCfdiRelacionado.Where(p => p.IdPrefactura == idPrefactura).ToList();
                        foreach (var pr in pre)
                        {
                            db.PreCfdiRelacionado.DeleteObject(pr);
                        }
                        
                        List<PagosPromotor> preP = db.PagosPromotor.Where(p => p.IdPrefactura == idPrefactura).ToList();
                        foreach (var pP in preP)
                        {
                            db.PagosPromotor.DeleteObject(pP);
                        }
                         
                        //-----------------------aqui eliminar los complementos--------------------
                        List<PreComplementoPago> com = db.PreComplementoPago.Where(p => p.idPreFactura == idPrefactura).ToList();
                        foreach (var c in com)
                        {
                            List<PreFCPDoctoRelacionado> imp = db.PreFCPDoctoRelacionado.Where(p => p.IdPreComplementoPago == c.IdComplementoPago).ToList();
                            foreach (var i in imp)
                            {
                                db.PreFCPDoctoRelacionado.DeleteObject(i);
                            }
                            db.PreComplementoPago.DeleteObject(c);
                        }

                        List<PreComplementoImpLocal> comIL = db.PreComplementoImpLocal.Where(p => p.idPreFactura == idPrefactura).ToList();
                        foreach (var cil in comIL)
                        {
                            db.PreComplementoImpLocal.DeleteObject(cil);
                        }

                        //------------------------------------------------------------------
                    }
                    
                    db.SaveChanges();
                    idComprobante = P.idPreFactura;
                    
                    foreach (var ui in factura.UUID)
                    {
                        PreCfdiRelacionado cf = new PreCfdiRelacionado();
                        cf.IdPrefactura = P.idPreFactura;
                        cf.TipoRelacion = factura.TipoRelacion;
                        cf.UUDI = ui;
                        db.PreCfdiRelacionado.AddObject(cf);
                      
                        db.SaveChanges();
                    }
                    foreach (var de in factura.Detalles)
                    {
                        PreConceptos PreC = ConvertirPreConcepto(de);
                        PreC.idPreFactura = P.idPreFactura;
                        db.PreConceptos.AddObject(PreC);
                        db.SaveChanges();
                        foreach (var tras in de.ConceptoTraslados)
                        {
                            PreImpuestos PIT = ConvertirreImopuestosTras(tras);
                            PIT.IdPreConceptos = PreC.idPreConcepto;
                            db.PreImpuestos.AddObject(PIT);
                   
                        }
                        foreach (var ret in de.ConceptoRetenciones)
                        {
                            PreImpuestos PIT = ConvertirreImopuestosRet(ret);
                            PIT.IdPreConceptos = PreC.idPreConcepto;
                            db.PreImpuestos.AddObject(PIT);

                        }
                       
                    }

                    if (factura.Datospagos != null)
                        if(factura.Datospagos.Count()>0)
                    {
                        PagosPromotor PP = new PagosPromotor();
                        PP.Banco= factura.Datospagos[0].Banco;
                        PP.fecha = factura.Datospagos[0].Fecha;
                        PP.Monto = factura.Datospagos[0].Monto;
                        PP.RutaImagen = factura.Datospagos[0].Tipo;
                        PP.IdPrefactura = P.idPreFactura;
                        db.PagosPromotor.AddObject(PP);
                    }

                    /*
                    foreach (var dp in factura.Datospagos)
                    {
                        PrePagos PP = ConvertirPrePagos(dp);
                        PP.idPrefactura = P.idPreFactura;
                        PP.idUsuarioRegistro = P.IDUsuario;
                        db.PrePagos.AddObject(PP);
                      
                    }
                    */
                    //-----------------complementos----------------------------
                    if (complementosF != null)
                    {
                        if (complementosF.pagos != null)
                            if (complementosF.pagos.Count() > 0)
                                foreach (var pg in complementosF.pagos)
                                {

                                    PreComplementoPago PG = ConvertirPreComplementoPago(pg);
                                    PG.idPreFactura = P.idPreFactura;
                                    db.PreComplementoPago.AddObject(PG);
                                    db.SaveChanges();
                                    if (pg.DoctoRelacionado != null)
                                        if (pg.DoctoRelacionado.Count() > 0)
                                            foreach (var dr in pg.DoctoRelacionado)
                                            {
                                                PreFCPDoctoRelacionado pd = ConvertirPreFCPDoctoRelacionado(dr);
                                                pd.IdPreComplementoPago = PG.IdComplementoPago;
                                                db.PreFCPDoctoRelacionado.AddObject(pd);
                                            }
                                }
                       
                        
                    }
                    if (factura.ImpuestosLocales != null)
                    {
                        if(factura.ImpuestosLocales.imp!=null)
                            if(factura.ImpuestosLocales.imp.Count()>0)
                        foreach (var il in factura.ImpuestosLocales.imp)
                        {
                            PreComplementoImpLocal pil = new PreComplementoImpLocal();
                            pil.implocal= il.ImpuestosLocales;
                            pil.ImpLoc = il.ImpLoc;
                            pil.Tasade = Convert.ToDecimal( il.Tasa);
                            pil.Importe = Convert.ToDecimal( il.Importe.Replace("$", ""));
                            pil.idPreFactura = P.idPreFactura;
                            pil.TotaldeRetenciones = Convert.ToDecimal(factura.ImpuestosLocales.TotaldeRetenciones.Replace("$", ""));
                            pil.TotaldeTraslados = Convert.ToDecimal(factura.ImpuestosLocales.TotaldeTraslados.Replace("$", ""));
                            db.PreComplementoImpLocal.AddObject(pil);
                        }
                    }
                    //--------------------fin complementos-----------------------------
                    db.SaveChanges();
                }
                return 1;

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
        //-----------------------
        private string ActualizaPrefolio(string PreFolio)
        {
            using (var db = new GAFEntities())
            {

                PreFolios Fo = new PreFolios();
                Fo = db.PreFolios.FirstOrDefault(p => p.Id == 1);

                var prefactu = new Prefactura(); string txtPreFolio = "";
                using (prefactu as IDisposable)
                {
                    if (PreFolio[0] == 'F')
                    {
                        Fo.Prefolio = Fo.Prefolio + 1;
                        db.PreFolios.ApplyCurrentValues(Fo);
                        db.SaveChanges();
                        txtPreFolio = prefactu.GetNextPreFolio("F", 1);

                    }
                    if (PreFolio[0] == 'P')
                    {
                        Fo.Pfolio = Fo.Pfolio + 1;
                        db.PreFolios.ApplyCurrentValues(Fo);
                        db.SaveChanges();
                        txtPreFolio = prefactu.GetNextPreFolio("P", 1);
                    }
                    if (PreFolio[0] == 'N')
                    {
                        Fo.Prefolio = Fo.Prefolio + 1;
                     
                       // Fo.NCfolio = Fo.NCfolio + 1;
                        db.PreFolios.ApplyCurrentValues(Fo);
                        db.SaveChanges();
                        txtPreFolio = prefactu.GetNextPreFolio("NC", 1);
                    }
                }
                return txtPreFolio;
            }
        }


        public long GuardarDevoluiciones(DatosPrefactura factura, decimal total)
        {

            try
            {
                using (var db = new GAFEntities())
                {
                    Devoluciones D = new Devoluciones();
                    
                      decimal subtotal=0;
                    foreach (var dp in factura.Datospagos)
                    {
                        subtotal = subtotal + dp.Monto;
                    }
                    D.Monto = subtotal;
                    D.Pendiente = subtotal;
                    D.fecha = DateTime.Now;
                    D.IdPreFacturas = factura.IdPrefacturas;
                    var usu= db.Usuario.FirstOrDefault(p => p.idUsuario == factura.Usuario);
                    D.Promotor=usu.Nombre+" "+usu.ApellidoP+" "+ usu.ApellidoM;
                    db.Devoluciones.AddObject(D);
                    db.SaveChanges();

                    foreach (var dp in factura.Datospagos)
                    {
                        PrePagos PP = ConvertirPrePagos(dp);
                        PP.idUsuarioRegistro = factura.Usuario;
                        PP.idDevoluciones = D.idDevoluciones;
                        db.PrePagos.AddObject(PP);
                      
                    }
                    db.SaveChanges();
                }
                return 1;

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
        public long GuardarPagosF(PagosF factura)
        {

            try
            {
                using (var db = new GAFEntities())
                {
                    db.PagosF.AddObject(factura);
                    db.SaveChanges();
                }
                return 1;

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
        //-----------------------complemento pagoss-------------------
        private PreComplementoPago ConvertirPreComplementoPago(Pagos f)
        {
            PreComplementoPago P = new PreComplementoPago();
            P.CtaBeneficiario = f.ctaBeneficiario;
            P.CtaOrdenante = f.ctaOrdenante;
            P.FechaPago = Convert.ToDateTime(f.fechaPago);
            P.FormaDePagoP = f.formaDePagoP;
            P.MonedaP = f.monedaP;
            if (!string.IsNullOrEmpty(f.monto))
             P.Monto =Convert.ToDecimal( f.monto);
            P.NomBancoOrdExt = f.nomBancoOrdExt;
            P.NumOperacion = f.numOperacion;
            P.RfcEmisorCtaBen = f.rfcEmisorCtaBen;
            P.RfcEmisorCtaOrd = f.rfcEmisorCtaOrd;
            P.TipoCadPago = f.tipoCadPago;
            if (!string.IsNullOrEmpty(f.tipoCambioP))
            P.TipoCambioP = Convert.ToDecimal(f.tipoCambioP);
            P.RutaImagen=f.rutaImagen;
            return P;
        }
        private PreFCPDoctoRelacionado ConvertirPreFCPDoctoRelacionado(PagoDoctoRelacionado f)
        {
            PreFCPDoctoRelacionado P = new PreFCPDoctoRelacionado();
            P.Folio = f.Folio;
            P.IdDocumento = f.IdDocumento;
            if (!string.IsNullOrEmpty(f.ImpPagado))
            P.ImpPagado =Convert.ToDecimal( f.ImpPagado);
            if (!string.IsNullOrEmpty(f.ImpSaldoAnt))
                P.ImpSaldoAnt = Convert.ToDecimal(f.ImpSaldoAnt);
            if (!string.IsNullOrEmpty(f.ImpSaldoInsoluto))
            P.ImpSaldoInsoluto = Convert.ToDecimal(f.ImpSaldoInsoluto);
            P.MetodoDePagoDR = f.MetodoDePagoDR;
            P.MonedaDR = f.MonedaDR;
            if (!string.IsNullOrEmpty(f.NumParcialidad))
             P.NumParcialidad = Convert.ToInt16(f.NumParcialidad);
            P.Serie = f.Serie;
            if (!string.IsNullOrEmpty(f.TipoCambioDR))
                P.TipoCambioDR = Convert.ToDecimal(f.TipoCambioDR);
                        return P;
        
        }
        //-----------------------
        private PreFactura ConvertirPrefactura(DatosPrefactura f)
        {
            PreFactura P = new PreFactura();
            P.CFDI = f.CFDI;
            P.Certificado = "";//no
            P.CondicionesDePago = f.CondicionesPago;
            P.Confirmacion = f.Confirmacion;
            P.Descuento = f.Descuento;
            P.Estatus = 0;//definir
            P.Fecha=f.Fecha;
            P.FechaUltimaModificacion = DateTime.Now;
            P.Folio = f.Folio;
            P.FormaPago = f.FormaPagoID;
            P.idCliente = f.idcliente;
            P.idEmpresa = f.IdEmpresa;
            P.IDUsuario = f.Usuario;
            P.LugarExpedicion = f.LugarExpedicion;
            P.MetodoPago = f.MetodoID;
            P.Moneda = f.MonedaID;
            P.NoCertificado = "";//no
            P.Sello = "";//no
            P.Serie = f.Serie;
            P.SubTotal = f.SubTotal;
            if(!string.IsNullOrEmpty(f.TipoCambio))
            P.TipoCambio =Convert.ToDecimal( f.TipoCambio);
            P.Total = f.Total;
            P.Version = "3.3";
            P.UsoCFDI = f.UsoCFDI;
            P.EstatusVista = f.Estatus;
            P.TipoDeComprobante = f.TipoDeComprobante;//nuevo
            P.IVA = f.IVA;
            P.PreFolio = f.PreFolio;
            return P;
        }
        private PreConceptos ConvertirPreConcepto(Datosdetalle d)
        {
            PreConceptos P = new PreConceptos();
            P.Cantidad = d.ConceptoCantidad;
            P.ClaveProdServ = d.ConceptoClaveProdServ;
            P.ClaveUnidad = d.ConceptoClaveUnidad;
            P.CuentaPredial = d.ConceptoCuentaPredial;
            P.Descripcion = d.ConceptoDescripcion;
            P.Descuento = d.ConceptoDescuento;
            P.Importe = d.ConceptoImporte;
            P.NoIdentificacion = d.ConceptoNoIdentificacion;
            P.Unidad = d.ConceptoUnidad;
            P.ValorUnitario = d.ConceptoValorUnitario;
            P.IVA = d.IVA;
            return P;
        }
        private PreImpuestos ConvertirreImopuestosTras(DatosdetalleTraslado I)
        {
            PreImpuestos P = new PreImpuestos();
            P.Base = I.Base;
            P.Importe = I.Importe;
            P.Impuesto = I.Impuesto;
            if (!string.IsNullOrEmpty(I.TasaOCuota))
            P.TasaOCuota =Convert.ToDecimal( I.TasaOCuota);
            P.TipoFactor = I.TipoFactor;
            P.TipoImpuesto = "Traslados";
            return P;
        }
        private PreImpuestos ConvertirreImopuestosRet(DatosdetalleRetencion I)
        {
            PreImpuestos P = new PreImpuestos();
            P.Base = I.Base;
            P.Importe = I.Importe;
            P.Impuesto = I.Impuesto;
            if (!string.IsNullOrEmpty(I.TasaOCuota))
                P.TasaOCuota = Convert.ToDecimal(I.TasaOCuota);
            P.TipoFactor = I.TipoFactor;
            P.TipoImpuesto = "Retenciones";
            return P;
        }
        private PrePagos ConvertirPrePagos(DatosPagos d)
        {
            PrePagos P = new PrePagos();
            //P.Fecha = d.Fecha;
            P.Fecha = null;
            P.MetodoPago = d.MetododePago;
            P.Monto = d.Monto;
            P.Observaciones = d.Descripcion;
            P.FechaRegistro = DateTime.Now;
            if (!string.IsNullOrEmpty(d.ClaveBancaria))
            {
                P.Banco = d.Banco;
                P.ClabeInterbancaria = d.ClaveBancaria;
            }
            P.Tipo = d.Tipo;
            P.Para = d.Para;
            P.beneficiario = d.beneficiario;
            if(!string.IsNullOrEmpty(d.Cliente))
            P.Cliente = d.Cliente;
            return P;
        }
        //-----------------------------------------------------------
           public List<Pagos> GetComplementoPago(long idPrefactura)
         {
             List<Pagos> FC = new List<Pagos>();
              List<PreComplementoPago> CP = new List<PreComplementoPago>();
              CP = GetPreComplementoPagos(idPrefactura);
              if (CP != null)
              if(CP.Count>0)
                foreach (var cp in CP)
                {
                    Pagos f = new Pagos();
                    f.ctaBeneficiario = cp.CtaBeneficiario;
                    f.ctaOrdenante = cp.CtaOrdenante;
                    if(cp.FechaPago!=null)
                    f.fechaPago = cp.FechaPago.ToString();
                    f.formaDePagoP = cp.FormaDePagoP;
                    f.id = (FC.Count()+1).ToString();
                    f.monedaP = cp.MonedaP;
                    if(cp.Monto!=null)
                    f.monto = cp.Monto.ToString();
                    f.nomBancoOrdExt = cp.NomBancoOrdExt;
                    f.numOperacion = cp.NumOperacion;
                    f.rfcEmisorCtaBen = cp.RfcEmisorCtaBen;
                    f.rfcEmisorCtaOrd = cp.RfcEmisorCtaOrd;
                    f.tipoCadPago = cp.TipoCadPago;
                    if(cp.TipoCambioP!=null)
                    f.tipoCambioP = cp.TipoCambioP.ToString();
                    //--------------------------------------------------------
                    List<PreFCPDoctoRelacionado> FCP = new List<PreFCPDoctoRelacionado>();
                    List<PagoDoctoRelacionado>PR=new List<PagoDoctoRelacionado>();
                    FCP= GetPreFCPDoctoRelacionado(cp.IdComplementoPago);
                     if (FCP != null)
                         if (FCP.Count > 0)
                         {
                             foreach (var p in FCP)
                             {
                                 PagoDoctoRelacionado pd = new PagoDoctoRelacionado();
                                 pd.Folio = p.Folio;
                                 pd.ID = f.id;
                                 pd.IdDocumento = p.IdDocumento;
                                 if (p.ImpPagado != null)
                                     pd.ImpPagado = p.ImpPagado.ToString();
                                 if (p.ImpSaldoAnt != null)
                                     pd.ImpSaldoAnt = p.ImpSaldoAnt.ToString();
                                 if (p.ImpSaldoInsoluto != null)
                                     pd.ImpSaldoInsoluto = p.ImpSaldoInsoluto.ToString();
                                 pd.MetodoDePagoDR = p.MetodoDePagoDR;
                                 pd.MonedaDR = p.MonedaDR;
                                 if (p.NumParcialidad != null)
                                     pd.NumParcialidad = p.NumParcialidad.ToString();
                                 pd.Serie = p.Serie;
                                 if (p.TipoCambioDR != null)
                                     pd.TipoCambioDR = p.TipoCambioDR.ToString();
                                 PR.Add(pd);
                             }
                             if (PR.Count > 0)
                                 f.DoctoRelacionado = PR;
                         }
                    //-------------------------------------------------------
                    FC.Add(f); 
                }

              return FC;

        }
        //------------------------------------------------------------
        public DatosPrefactura GetPreFacturaInsert(long idPrefactura)
        {
            try
            {
            DatosPrefactura DP = new DatosPrefactura();
            //---------------------------------------
            PreFactura P = new PreFactura();
            P = GetCliente(idPrefactura);
            if (P != null)
            {
                DP.CFDI = P.CFDI;
                DP.CondicionesPago = P.CondicionesDePago;
                DP.Confirmacion = P.Confirmacion;
                if(P.Descuento!=null)
                DP.Descuento = (decimal)P.Descuento;
                DP.Fecha = (DateTime)P.Fecha;
                DP.Folio = P.Folio;
                DP.PreFolio = P.PreFolio;
                DP.FormaPagoID = P.FormaPago;
                DP.LugarExpedicion = P.LugarExpedicion;
                DP.MetodoID= P.MetodoPago;
                DP.MonedaID = P.Moneda;
                DP.Serie = P.Serie;
                DP.SubTotal =(decimal) P.SubTotal;
                if (P.TipoCambio!=null)
                    DP.TipoCambio = P.TipoCambio.ToString();
                DP.Total = P.Total;
                DP.TipoDeComprobante=P.TipoDeComprobante;
                DP.LugarExpedicion=P.LugarExpedicion;
                DP.idcliente = (int)P.idCliente;
                DP.IdEmpresa = (int)P.idEmpresa;
                DP.UsoCFDI = P.UsoCFDI;
                DP.TipoDeComprobante = P.TipoDeComprobante;
                if(P!=null)
                DP.IVA =(decimal) P.IVA;
                if (P.IDUsuario!=null)
                DP.Usuario =(System.Guid) P.IDUsuario;
                //-------------------------------------conceptos------------------------
                List<PreConceptos> C = new List<PreConceptos>();
                C = GetConceptos(idPrefactura);
                if(C!=null)
                    if(C.Count>0)
                     {
                         List<Datosdetalle> D = new List<Datosdetalle>();
                         foreach (var de in C)
                         {
                            Datosdetalle d = new Datosdetalle();
                             d.Partida = D.Count + 1;//
                             if(de.IVA!=null)
                             d.IVA = (decimal)de.IVA;
                             if (de.Cantidad!=null)
                             d.ConceptoCantidad= (decimal)de.Cantidad;
                             d.ConceptoClaveProdServ=de.ClaveProdServ ;
                             d.ConceptoClaveUnidad=de.ClaveUnidad;
                             d.ConceptoCuentaPredial=de.CuentaPredial ;
                             d.ConceptoDescripcion=de.Descripcion;
                             d.ConceptoDescuento=de.Descuento ;
                             d.ConceptoImporte=(decimal)de.Importe ;
                             d.ConceptoNoIdentificacion=de.NoIdentificacion ;
                             d.ConceptoUnidad=de.Unidad ;
                             d.ConceptoValorUnitario=(decimal)de.ValorUnitario ;
                             //-------------
                             List<PreImpuestos> I = new List<PreImpuestos>();
                             I = GetImpuestos(de.idPreConcepto);
                             if (I != null)
                                 if(I.Count>0)
                             {
                                 List<DatosdetalleRetencion> DR = new List<DatosdetalleRetencion>();
                                 List<DatosdetalleTraslado> DT = new List<DatosdetalleTraslado>();
                             foreach (var im in I)
                             {
                                 if (im.TipoImpuesto == "Retenciones")
                                 {
                                     DatosdetalleRetencion dr = new DatosdetalleRetencion();
                                     dr.Base = (decimal)im.Base;
                                     dr.Importe =(decimal) im.Importe;
                                     dr.Importe =(decimal) im.Importe;
                                     if(im.Importe!=null)
                                     dr.Importe =(decimal) im.Importe;
                                     dr.Impuesto = im.Impuesto;
                                     if(im.TasaOCuota!=null)
                                     dr.TasaOCuota = im.TasaOCuota.ToString();
                                     dr.TipoFactor = im.TipoFactor;
                                     DR.Add(dr);
                                 }
                                 if (im.TipoImpuesto == "Traslados")
                                 {
                                     DatosdetalleTraslado dt = new DatosdetalleTraslado();
                                     dt.Base = (decimal)im.Base;
                                     dt.Importe = (decimal)im.Importe;
                                     dt.Importe = (decimal)im.Importe;
                                     if (im.Importe != null)
                                         dt.Importe = (decimal)im.Importe;
                                     dt.Impuesto = im.Impuesto;
                                     if (im.TasaOCuota != null)
                                         dt.TasaOCuota = im.TasaOCuota.ToString();
                                     dt.TipoFactor = im.TipoFactor;
                                     DT.Add(dt);
                                 }
                             }
                             if (DT.Count > 0)
                             {
                                 d.ConceptoTraslados = new List<DatosdetalleTraslado>();
                                 d.ConceptoTraslados = DT;
                             }
                              if(DR.Count>0)
                              {
                                  d.ConceptoRetenciones = new List<DatosdetalleRetencion>();
                                  d.ConceptoRetenciones = DR;
                              }
                                     //-------ciclo-----------
                             }
                             //---------------fin impuestos
                             D.Add(d);
                         }
                         DP.Detalles = new List<Datosdetalle>();
                         DP.Detalles = D;
                    }
                //---------------------------------------------
                List<PreCfdiRelacionado> R = new List<PreCfdiRelacionado>();
                List<string>L=new List<string>();
                  R = GetPreCfdiRelacionado(idPrefactura);
                if(R!=null)
                    if (R.Count > 0)
                    {
                        string r = ""; string TipoRelacion="";
                        foreach (var re in R)
                        {
                            r = "";
                            TipoRelacion = re.TipoRelacion;
                            r = re.UUDI;
                            L.Add(r);
                        }
                      DP.UUID=L;
                      DP.TipoRelacion = TipoRelacion;

                    }
                 /*
                List<PrePagos> PA=new List<PrePagos>();
                List<DatosPagos>DPA=new List<DatosPagos>();
                  PA= GetPrePagos(idPrefactura);
                if(PA!=null)
                if(PA.Count()>0)
                {
                   foreach (var pa in PA)
                       {
                           DatosPagos pag = new DatosPagos();
                           pag.Banco = pa.Banco;
                           pag.ClaveBancaria = pa.ClabeInterbancaria;
                           pag.Descripcion = pa.Observaciones;
                           pag.Fecha =(DateTime) pa.Fecha;
                           pag.MetododePago = pa.MetodoPago;
                           pag.Monto = (decimal)pa.Monto;
                           pag.Tipo = pa.Tipo;
                           DPA.Add(pag);
                       }
                   DP.Datospagos = DPA;

                }
                //----------------------
               */
                ImpLocales IL = new ImpLocales();
                var CIL = GetPreComplementoImpLocal(idPrefactura);
                if (CIL != null)
                {
                    foreach (var cil in CIL)
                    {
                        IL.TotaldeRetenciones = cil.TotaldeRetenciones.ToString();
                        IL.TotaldeTraslados = cil.TotaldeTraslados.ToString();
                        ImpuestosL I=new ImpuestosL();
                        I.Importe=cil.Importe.ToString();
                        I.ImpLoc=cil.ImpLoc;
                        I.ImpuestosLocales=cil.implocal;
                        I.Tasa=cil.Tasade.ToString();
                        IL.imp.Add(I);

                    }
                    DP.ImpuestosLocales = IL;
                }
                return DP;
            }
            else
                return null;
        }
            catch (Exception ee)
            {
                Logger.Error(ee);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                return null;
            }
        
        }

        //-----------------------------
        public List<vprefactura> GetListPrefacturaPromotores(System.Guid id, DateTime fechaInicial, DateTime fechaFinal, int idEmpresa, int status, int idCliente = 0)
        {
            try
            {
                List<vprefactura> lista;
                using (var db = new GAFEntities())
                {
                    if (idEmpresa == 0)
                    {

                        if (idCliente == 0)
                        {
                            lista = db.vprefactura.Where(p => p.Fecha >= fechaInicial && p.TipoDeComprobante!="Pago" &&
       
                                                          p.Fecha <= fechaFinal && p.IDUsuario == id && (p.Timbrado == null)).OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            // order by folio
                            lista = db.vprefactura.Where(p => p.Fecha >= fechaInicial && p.Fecha <= fechaFinal && p.TipoDeComprobante != "Pago" &&
                                                      p.idCliente == idCliente && p.IDUsuario == id && p.Timbrado == null).OrderByDescending(p => p.Fecha).ToList();
                        }



                    }
                    else
                        if (idCliente == 0)
                        {

                            lista = db.vprefactura.Where(p => p.idEmpresa == idEmpresa && p.Fecha >= fechaInicial && p.TipoDeComprobante != "Pago" &&
                                                          p.Fecha <= fechaFinal && p.IDUsuario == id && p.Timbrado == null).OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            lista = db.vprefactura.Where(p => p.idEmpresa == idEmpresa && p.Fecha >= fechaInicial && p.idCliente == idCliente && p.TipoDeComprobante != "Pago" &&
                                                          p.Fecha <= fechaFinal && p.IDUsuario == id && p.Timbrado == null).OrderByDescending(p => p.Fecha).ToList();
                        }



                    if (status == 9)
                    {
                        return lista;
                    }
                    return lista.Where(p => p.Estatus == status).ToList();
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<vprefactura>();
            }

        }

        public List<vprefactura> GetListPrefactura(DateTime fechaInicial, DateTime fechaFinal, int idEmpresa, int status, int idCliente = 0)
        {
            try
            {
                List<vprefactura> lista;
                using (var db = new GAFEntities())
                {
                    if (idEmpresa == 0)
                    {

                        if (idCliente == 0)
                        {
                            lista = db.vprefactura.Where(p => p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal && p.Timbrado == null && p.TipoDeComprobante != "Pago").OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            // order by folio
                            lista = db.vprefactura.Where(p => p.Fecha >= fechaInicial && p.Fecha <= fechaFinal &&
                                                      p.idCliente == idCliente && p.Timbrado == null && p.TipoDeComprobante != "Pago").OrderByDescending(p => p.Fecha).ToList();
                        }



                    }
                    else
                        if (idCliente == 0)
                        {

                            lista = db.vprefactura.Where(p => p.idEmpresa == idEmpresa && p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal && p.Timbrado == null && p.TipoDeComprobante != "Pago").OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            lista = db.vprefactura.Where(p => p.idEmpresa == idEmpresa && p.Fecha >= fechaInicial && p.idCliente == idCliente &&
                                                          p.Fecha <= fechaFinal && p.Timbrado == null && p.TipoDeComprobante != "Pago").OrderByDescending(p => p.Fecha).ToList();
                        }



                    if (status == 9)
                    {
                        return lista;
                    }
                    return lista.Where(p => p.Estatus == status).ToList();
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<vprefactura>();
            }

        }

        public List<vprefactura> GetListPagofactura(DateTime fechaInicial, DateTime fechaFinal, int idEmpresa, int status, int idCliente = 0)
        {
            try
            {
                List<vprefactura> lista;
                using (var db = new GAFEntities())
                {
                    if (idEmpresa == 0)
                    {

                        if (idCliente == 0)
                        {
                            lista = db.vprefactura.Where(p => p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal && p.CFDI == "P").OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            // order by folio
                            lista = db.vprefactura.Where(p => p.Fecha >= fechaInicial && p.Fecha <= fechaFinal &&
                                                      p.idCliente == idCliente && p.CFDI == "P" ).OrderByDescending(p => p.Fecha).ToList();
                        }



                    }
                    else
                        if (idCliente == 0)
                        {

                            lista = db.vprefactura.Where(p => p.idEmpresa == idEmpresa && p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal && p.CFDI == "P").OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            lista = db.vprefactura.Where(p => p.idEmpresa == idEmpresa && p.Fecha >= fechaInicial && p.idCliente == idCliente &&
                                                          p.Fecha <= fechaFinal && p.CFDI == "P").OrderByDescending(p => p.Fecha).ToList();
                        }



                    if (status == 9)
                    {
                        return lista;
                    }
                    return lista.Where(p => p.Estatus == status).ToList();
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<vprefactura>();
            }

        }

        public List<vPrefacturaPagos> GetListPagofacturaValido(System.Guid id,DateTime fechaInicial, DateTime fechaFinal, int idEmpresa, int status, int idCliente = 0)
        {
            try
            {
                List<vPrefacturaPagos> lista;
                using (var db = new GAFEntities())
                {
                    if (idEmpresa == 0)
                    {

                        if (idCliente == 0)
                        {
                            lista = db.vPrefacturaPagos.Where(p => p.Fecha >= fechaInicial && p.IDUsuario == id &&
                                                          p.Fecha <= fechaFinal && p.TipoDeComprobante == "Pago" && (p.pagoVerificado == 1 || p.pagoVerificado == -1)
                                                          ).OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            // order by folio
                            lista = db.vPrefacturaPagos.Where(p => p.Fecha >= fechaInicial && p.Fecha <= fechaFinal && p.IDUsuario == id &&
                                                      p.idCliente == idCliente && p.TipoDeComprobante == "Pago" && (p.pagoVerificado == 1 || p.pagoVerificado == -1)).OrderByDescending(p => p.Fecha).ToList();
                        }



                    }
                    else
                        if (idCliente == 0)
                        {

                            lista = db.vPrefacturaPagos.Where(p => p.idEmpresa == idEmpresa && p.Fecha >= fechaInicial && p.IDUsuario == id &&
                                                          p.Fecha <= fechaFinal && p.TipoDeComprobante == "Pago" && (p.pagoVerificado == 1 || p.pagoVerificado == -1)).OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            lista = db.vPrefacturaPagos.Where(p => p.idEmpresa == idEmpresa && p.Fecha >= fechaInicial && p.idCliente == idCliente && p.IDUsuario == id &&
                                                          p.Fecha <= fechaFinal && p.TipoDeComprobante == "Pago" && (p.pagoVerificado == 1 || p.pagoVerificado == -1)).OrderByDescending(p => p.Fecha).ToList();
                        }



                    if (status == 9)
                    {
                        return lista;
                    }
                    return lista.Where(p => p.Estatus == status).ToList();
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<vPrefacturaPagos>();
            }

        }
        //----------------------------------------------------------
        public List<vPrefacturaPagos> GetListPagoPrefactura( DateTime fechaInicial, DateTime fechaFinal, int idEmpresa, int status, int idCliente = 0)
        {
            try
            {
                List<vPrefacturaPagos> lista;
                using (var db = new GAFEntities())
                {
                    if (idEmpresa == 0)
                    {

                        if (idCliente == 0)
                        {
                            lista = db.vPrefacturaPagos.Where(p => p.Fecha >= fechaInicial  &&
                                                          p.Fecha <= fechaFinal && p.TipoDeComprobante == "Pago" 
                                                          ).OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            // order by folio
                            lista = db.vPrefacturaPagos.Where(p => p.Fecha >= fechaInicial && p.Fecha <= fechaFinal  &&
                                                      p.idCliente == idCliente && p.TipoDeComprobante == "Pago" ).OrderByDescending(p => p.Fecha).ToList();
                        }



                    }
                    else
                        if (idCliente == 0)
                        {

                            lista = db.vPrefacturaPagos.Where(p => p.idEmpresa == idEmpresa && p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal && p.TipoDeComprobante == "Pago" ).OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            lista = db.vPrefacturaPagos.Where(p => p.idEmpresa == idEmpresa && p.Fecha >= fechaInicial && p.idCliente == idCliente  &&
                                                          p.Fecha <= fechaFinal && p.TipoDeComprobante == "Pago" ).OrderByDescending(p => p.Fecha).ToList();
                        }



                    if (status == 9)
                    {
                        return lista;
                    }
                    return lista.Where(p => p.Estatus == status).ToList();
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<vPrefacturaPagos>();
            }

        }
      
        //----------------------------------------------------------
        public List<vPrefacturaPagos> GetListPagofacturaRechazadosValidados(DateTime fechaInicial, DateTime fechaFinal, int idEmpresa, int status, int idCliente = 0)
        {
            try
            {
                List<vPrefacturaPagos> lista;
                using (var db = new GAFEntities())
                {
                    if (idEmpresa == 0)
                    {

                        if (idCliente == 0)
                        {
                            lista = db.vPrefacturaPagos.Where(p => p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal && p.CFDI == "P" && p.pagoVerificado != null).OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            // order by folio
                            lista = db.vPrefacturaPagos.Where(p => p.Fecha >= fechaInicial && p.Fecha <= fechaFinal &&
                                                      p.idCliente == idCliente && p.CFDI == "P" && p.pagoVerificado != null).OrderByDescending(p => p.Fecha).ToList();
                        }



                    }
                    else
                        if (idCliente == 0)
                        {

                            lista = db.vPrefacturaPagos.Where(p => p.idEmpresa == idEmpresa && p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal && p.CFDI == "P" && p.pagoVerificado != null).OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            lista = db.vPrefacturaPagos.Where(p => p.idEmpresa == idEmpresa && p.Fecha >= fechaInicial && p.idCliente == idCliente &&
                                                          p.Fecha <= fechaFinal && p.CFDI == "P" && p.pagoVerificado != null).OrderByDescending(p => p.Fecha).ToList();
                        }

                    foreach (var l in lista)
                    {
                        if (l.Monto == null)
                        {
                            l.Monto = l.MontoCFDI;
                            l.FechaPago = l.FechaPagoCFDI;
                        }
                    }

                    if (status == 9)
                    {
                        return lista;
                    }



                    return lista.Where(p => p.Estatus == status).ToList();
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<vPrefacturaPagos>();
            }

        }

        //----------------------------------------------------------
        public List<vPrefacturaPagos> GetListPagofacturaValidar(DateTime fechaInicial, DateTime fechaFinal, int idEmpresa, int status, int idCliente = 0)
        {
            try
            {
                List<vPrefacturaPagos> lista;
                using (var db = new GAFEntities())



                {
                    if (idEmpresa == 0)
                    {

                        if (idCliente == 0)
                        {
                            lista = db.vPrefacturaPagos.Where(p => p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal && p.CFDI == "P" && p.pagoVerificado == null).OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            // order by folio
                            lista = db.vPrefacturaPagos.Where(p => p.Fecha >= fechaInicial && p.Fecha <= fechaFinal &&
                                                      p.idCliente == idCliente && p.CFDI == "P" && p.pagoVerificado == null).OrderByDescending(p => p.Fecha).ToList();
                        }



                    }
                    else
                        if (idCliente == 0)
                        {

                            lista = db.vPrefacturaPagos.Where(p => p.idEmpresa == idEmpresa && p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal && p.CFDI == "P" && p.pagoVerificado == null).OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            lista = db.vPrefacturaPagos.Where(p => p.idEmpresa == idEmpresa && p.Fecha >= fechaInicial && p.idCliente == idCliente &&
                                                          p.Fecha <= fechaFinal && p.CFDI == "P" && p.pagoVerificado == null).OrderByDescending(p => p.Fecha).ToList();
                        }

                    foreach (var l in lista)
                    {
                        if (l.Monto == null)
                        {
                            l.Monto = l.MontoCFDI;
                            l.FechaPago = l.FechaPagoCFDI;
                        }
                    }

                    if (status == 9)
                    {
                        return lista;
                    }



                    return lista.Where(p => p.Estatus == status).ToList();
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<vPrefacturaPagos>();
            }

        }
        //----------------------------------------------------------
        public string validarNumeroValidacion(long idPrefactura,string usuario)
        {

            using (var db = new GAFEntities())
            {
                PreFactura fac = db.PreFactura.Single(l => l.idPreFactura == idPrefactura);
                if (string.IsNullOrEmpty(fac.Validador1))
                    return "No";
                if (string.IsNullOrEmpty(fac.Validador2) && usuario != fac.Validador1) 
                {
                    return "ok";
                }
                else
                    return "Mismo Usuario";
            }

        }
        public bool ValidarestatusPago(long idPrefactura, string usuario)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    PreFactura fac = db.PreFactura.Single(l => l.idPreFactura == idPrefactura);
                    if (string.IsNullOrEmpty(fac.Validador1))
                    {
                        fac.Validador1 = usuario;
                        db.PreFactura.ApplyCurrentValues(fac);
                        db.SaveChanges();
                        return false;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(fac.Validador2) && usuario != fac.Validador1)
                        {
                            fac.Validador2 = usuario;
                            db.PreFactura.ApplyCurrentValues(fac);
                            db.SaveChanges();
                            return true;
                        }
                        else
                            return false;
                        
                    }

                    return true;
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
             
                return true;
                throw;
            }
        }
        public void CambiarestatusPagoRechazo(long idPrefactura, int status,string motivo)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    PreFactura fac = db.PreFactura.Single(l => l.idPreFactura == idPrefactura);
                    fac.pagoVerificado = status;
                    fac.MotivoRechazo = motivo;
                    db.PreFactura.ApplyCurrentValues(fac);
                    db.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                throw;
            }
        }

        public void CambiarestatusPago(long idPrefactura, int status)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    PreFactura fac = db.PreFactura.Single(l => l.idPreFactura == idPrefactura);
                    fac.pagoVerificado = status;
                    db.PreFactura.ApplyCurrentValues(fac);
                    db.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                throw;
            }
        }
        public void GuardarIDRelacionados(long idPrefactura, string ids)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    PreComplementoPago fac = db.PreComplementoPago.Single(l => l.idPreFactura == idPrefactura);
                    fac.Ids = ids;
                    db.PreComplementoPago.ApplyCurrentValues(fac);
                    db.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                throw;
            }
        }
        public void GuardarIDRelacionadosFactura(long idPrefactura, string ids)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    FacturaPagoRelacionados fac = new FacturaPagoRelacionados();
                    fac.Ids = ids;
                     fac.IdFactura=idPrefactura;
                    db.FacturaPagoRelacionados.AddObject(fac);
                    db.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                throw;
            }
        }

        public void CambiarMontoPago(long idPrefactura,decimal monto, int parcialidad)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    PreFactura fac = db.PreFactura.Single(l => l.idPreFactura == idPrefactura);
                    fac.SaldoAnteriorPago = monto;
                    fac.Parcialidad = parcialidad;

                    db.PreFactura.ApplyCurrentValues(fac);
                    db.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                throw;
            }
        }

        public void CambiarMontoPagoContabilidad(long idPrefactura, decimal monto, int parcialidad)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    facturas fac = db.facturas.Single(l => l.idVenta == idPrefactura);
                    fac.SaldoAnteriorPago = monto;
                    fac.Parcialidad = parcialidad;

                    db.facturas.ApplyCurrentValues(fac);
                    db.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException);
                throw;
            }
        }

        public PreFactura GetPreFacturaID(long id)
        {
            try
            {
               using (var db = new GAFEntities())
                {
                       var  lista = db.PreFactura.Where(p => p.idPreFactura == id).FirstOrDefault();
                   
                    return lista;
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new PreFactura();
            }

        }
        public facturas GetFacturaUUDI(string uudi)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    var lista = db.facturas.Where(p => p.Uid == uudi).FirstOrDefault();

                    return lista;
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new facturas();
            }

        }
        public PreComplementoPago GetPreFacturaPagosID(long id)
        {
            try
            {
                using (var db = new GAFEntities())
                {
                    var lista = db.PreComplementoPago.Where(p => p.idPreFactura == id).FirstOrDefault();

                    return lista;
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new PreComplementoPago();
            }

        }

        public List<PreFactura> GetListPagoPorPagar( int idEmpresa, int status, int idCliente = 0)
        {
            try
            {
                List<PreFactura> lista;
                using (var db = new GAFEntities())
                {
                    if (idEmpresa == 0)
                    {

                        if (idCliente == 0)
                        {
                            lista = db.PreFactura.Where(p => p.CFDI != "P" &&p.Timbrado==true && (p.SaldoAnteriorPago>0||p.SaldoAnteriorPago==null)).OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            // order by folio
                            lista = db.PreFactura.Where(p => p.idCliente == idCliente && p.CFDI != "P" && p.Timbrado == true && (p.SaldoAnteriorPago > 0 || p.SaldoAnteriorPago == null)).OrderByDescending(p => p.Fecha).ToList();
                        }



                    }
                    else
                        if (idCliente == 0)
                        {

                            lista = db.PreFactura.Where(p => p.idEmpresa == idEmpresa && p.CFDI != "P" && p.Timbrado == true && (p.SaldoAnteriorPago > 0 || p.SaldoAnteriorPago == null)).OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            lista = db.PreFactura.Where(p => p.idEmpresa == idEmpresa && p.idCliente == idCliente
                                                          && p.CFDI != "P" && p.Timbrado == true && (p.SaldoAnteriorPago > 0 || p.SaldoAnteriorPago == null)).OrderByDescending(p => p.Fecha).ToList();
                        }

                    if (lista != null)
                    {
                        foreach (var d in lista)
                        {
                            if (d.SaldoAnteriorPago == null)
                                d.SaldoAnteriorPago = d.Total;
                            if (d.Parcialidad == null)
                                d.Parcialidad = 0;
                        }
                    }

                   // if (status == 9)
                    {
                        return lista;
                    }
                   // return lista.Where(p => p.Estatus == status).ToList();
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<PreFactura>();
            }

        }
        public List<vPrefacturaPorPagar> GetListPagoPorPagarPrefactura(int idEmpresa, int status, int idCliente = 0)
        {
            try
            {
                List<vPrefacturaPorPagar> lista;
                using (var db = new GAFEntities())
                {
                    if (idEmpresa == 0)
                    {

                        if (idCliente == 0)
                        {
                            lista = db.vPrefacturaPorPagar.Where(p => p.CFDI != "P"&&p.Cancelado!=1  && p.Timbrado == true && (p.SaldoAnteriorPago > 0 || p.SaldoAnteriorPago == null)).OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            // order by folio
                            lista = db.vPrefacturaPorPagar.Where(p => p.idCliente == idCliente && p.CFDI != "P" && p.Cancelado != 1 && p.Timbrado == true && (p.SaldoAnteriorPago > 0 || p.SaldoAnteriorPago == null)).OrderByDescending(p => p.Fecha).ToList();
                        }



                    }
                    else
                        if (idCliente == 0)
                        {

                            lista = db.vPrefacturaPorPagar.Where(p => p.idEmpresa == idEmpresa && p.CFDI != "P" && p.Cancelado != 1 && p.Timbrado == true && (p.SaldoAnteriorPago > 0 || p.SaldoAnteriorPago == null)).OrderByDescending(p => p.Fecha).ToList();
                        }
                        else
                        {
                            lista = db.vPrefacturaPorPagar.Where(p => p.idEmpresa == idEmpresa && p.idCliente == idCliente
                                                          && p.CFDI != "P" && p.Cancelado != 1 && p.Timbrado == true && (p.SaldoAnteriorPago > 0 || p.SaldoAnteriorPago == null)).OrderByDescending(p => p.Fecha).ToList();
                        }

                    if (lista != null)
                    {
                        foreach (var d in lista)
                        {
                            if (d.SaldoAnteriorPago == null)
                                d.SaldoAnteriorPago = d.Total;
                            if (d.Parcialidad == null)
                                d.Parcialidad = 0;
                        }
                    }

                    // if (status == 9)
                    {
                        return lista;
                    }
                    // return lista.Where(p => p.Estatus == status).ToList();
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<vPrefacturaPorPagar>();
            }

        }
        public List<vfacturasPorPagar> GetListPagoPorPagarContabilidad(int idEmpresa, int status, int idCliente = 0)
        {
            try
            {
                List<vfacturasPorPagar> lista;
                using (var db = new GAFEntities())
                {

                    lista = db.vfacturasPorPagar.Where(p => p.IdEmpresa == idEmpresa && p.FormaPago == "Por definir" &&p.Cancelado==0 && p.idcliente == idCliente
                                                          && (p.SaldoAnteriorPago > 0 || p.SaldoAnteriorPago == null)).OrderByDescending(p => p.Fecha).ToList();
                }

                    if (lista != null)
                    {
                        foreach (var d in lista)
                        {
                            if (d.SaldoAnteriorPago == null)
                                d.SaldoAnteriorPago = d.Total;
                            if (d.Parcialidad == null)
                                d.Parcialidad = 0;
                        }
                    }

                    // if (status == 9)
                    {
                        return lista;
                    }
                    // return lista.Where(p => p.Estatus == status).ToList();
                
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<vfacturasPorPagar>();
            }

        }
      
        
        //---------------------------
        public List<PreFactura> GetList(DateTime fechaInicial, DateTime fechaFinal, int idEmpresa, int status, int idCliente = 0)
        {
            try
            {
                List<PreFactura> lista;
                using (var db = new GAFEntities())
                {
                    if (idEmpresa == 0)
                    {

                        if (idCliente == 0)
                        {
                            lista = db.PreFactura.Where(p => p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal).OrderBy(p => p.Folio).ToList();
                        }
                        else
                        {
                            // order by folio
                            lista = db.PreFactura.Where(p => p.Fecha >= fechaInicial && p.Fecha <= fechaFinal &&
                                                      p.idCliente == idCliente).OrderBy(p => p.Folio).ToList();
                        }



                    }
                    else
                        if (idCliente == 0)
                        {

                            lista = db.PreFactura.Where(p => p.idEmpresa == idEmpresa && p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal).OrderByDescending(p => p.Folio).ToList();
                        }
                        else
                        {
                            lista = db.PreFactura.Where(p => p.idEmpresa == idEmpresa && p.Fecha >= fechaInicial && p.idCliente == idCliente &&
                                                          p.Fecha <= fechaFinal).OrderByDescending(p => p.Folio).ToList();
                        }

              

                    if (status ==9)
                    {
                        return lista;
                    }
                    return lista.Where(p => p.Estatus == status).ToList();
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<PreFactura>();
            }

        }
        //---------------------------
        public List<vDevoluciones> GetListDecoluciones(System.Guid id, DateTime fechaInicial, DateTime fechaFinal, int idEmpresa, int status, int idCliente = 0)
        {
            try
            {
                List<vDevoluciones> lista;
                using (var db = new GAFEntities())
                {
                    if (idEmpresa == 0)
                    {

                        if (idCliente == 0)
                        {
                            lista = db.vDevoluciones.Where(p => p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal && ( p.Estatus==1)).OrderBy(p => p.Folio).ToList();
                        }
                        else
                        {
                            // order by folio
                            lista = db.vDevoluciones.Where(p => p.Fecha >= fechaInicial && p.Fecha <= fechaFinal &&
                                                      p.idCliente == idCliente && ( p.Estatus == 1)).OrderBy(p => p.Folio).ToList();
                        }



                    }
                    else
                        if (idCliente == 0)
                        {

                            lista = db.vDevoluciones.Where(p => p.idEmpresa == idEmpresa && p.Fecha >= fechaInicial &&
                                                          p.Fecha <= fechaFinal && ( p.Estatus == 1)).OrderByDescending(p => p.Folio).ToList();
                        }
                        else
                        {
                            lista = db.vDevoluciones.Where(p => p.idEmpresa == idEmpresa && p.Fecha >= fechaInicial && p.idCliente == idCliente &&
                                                          p.Fecha <= fechaFinal && ( p.Estatus == 1)).OrderByDescending(p => p.Folio).ToList();
                        }



                    if (status == -1)
                    {
                        return lista;
                    }
                    return lista.Where(p => p.Estatus == status).ToList();
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<vDevoluciones>();
            }

        }
        //---------------------------

        public List<Devoluciones> GetListDecolucionesOperaciones(DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                List<Devoluciones> lista;
                using (var db = new GAFEntities())
                {


                    lista = db.Devoluciones.Where(p => p.fecha >= fechaInicial &&
                                                  p.fecha <= fechaFinal && p.Pendiente>0).OrderByDescending(p => p.idDevoluciones).ToList();

                    return lista.ToList();
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<Devoluciones>();
            }

        }
        //---------------------------

        public List<PagosF> GetPAGO(string idPrefacturas)
        {
            try
            {

                String[] substrings = idPrefacturas.Split('|');

                List<PagosF> lista = new List<PagosF>(); 
                foreach (var id in substrings)
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        long x = Convert.ToInt64(id);
                        using (var db = new GAFEntities())
                        {
                            PagosF l = db.PagosF.Where(p => p.IdPrefactura == x).First();
                            if(l!=null)
                            lista.Add(l);
                        }
                    }
                }
                return lista;
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return null;
            }

        }

        public List<PrePagos> GetDecolucionesOperaciones(long idDevolucion)
        {
            try
            {
                List<PrePagos> lista;
                using (var db = new GAFEntities())
                {


                    lista = db.PrePagos.Where(p => p.idDevoluciones == idDevolucion).ToList();

                    return lista;
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return new List<PrePagos>();
            }

        }


        public Devoluciones GetDecoluciones(long idDevolucion)
        {
            try
            {
                Devoluciones lista;
                using (var db = new GAFEntities())
                {


                    lista = db.Devoluciones.Where(p => p.idDevoluciones == idDevolucion).First();

                    return lista;
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return null;
            }

        }
        public PagosPromotor GetPagoCFDIPromotor(long idprefactura)
        {
            try
            {
                PagosPromotor lista;
                using (var db = new GAFEntities())
                {


                    lista = db.PagosPromotor.Where(p => p.IdPrefactura == idprefactura).First();

                    return lista;
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return null;
            }

        }

        public decimal GetDecolucionesOperacionesPendiente(long idDevolucion)
        {
            try
            {
                Devoluciones lista;
                using (var db = new GAFEntities())
                {


                    lista = db.Devoluciones.Where(p => p.idDevoluciones == idDevolucion).First();

                    return (decimal)lista.Pendiente;
                }
            }
            catch (Exception eee)
            {
                Logger.Error(eee.Message);
                return 0;
            }

        }

        public int SetDecolucionesOperacionesPendiente(long idDevolucion,decimal monto)
        {
            try
            {
                Devoluciones d;
                using (var db = new GAFEntities())
                {


                    d = db.Devoluciones.Where(p => p.idDevoluciones == idDevolucion).First();
                    d.Pendiente = monto;
                    db.Devoluciones.ApplyCurrentValues(d);
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
