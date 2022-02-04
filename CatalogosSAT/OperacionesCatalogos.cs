using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatalogosSAT
{
    public class OperacionesCatalogos
    {

      
      //---------------------------------------------------------------
        public c_FormaPago Consultar_FormaPago(string formaPago)
        {
            try
            {
                c_FormaPago C = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                    C = db.c_FormaPago.FirstOrDefault(p => p.c_FormaPago1 == formaPago);

                    return C;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }
        //-----------------------------------------------------------


        public Divisas Consultar_TipoDivisa(string ClavePais)
        {
            try
            {
                Divisas C = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                    C = db.Divisas.FirstOrDefault(p => p.Clave == ClavePais);

                    return C;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }
        //_-----------------------------------------------------------

        public List< c_TasaOCuota> Consultar_TasaCuotaTraslado(string Impu, string tipoFactor)
        {
            try
            {
                if (Impu == "001")
                    Impu = "ISR";
                if (Impu == "002")
                    Impu = "IVA";
                if (Impu == "003")
                    Impu = "IEPS";


               List< c_TasaOCuota> C = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                    C = db.c_TasaOCuota.Where(p => p.Impuesto == Impu && p.Traslado == "Sí" && p.Factor == tipoFactor).ToList();

                    return C;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }
        //_-----------------------------------------------------------

        public List<c_TasaOCuota> Consultar_TasaCuotaRetencion(string Impu, string tipoFactor)
        {
            try
            {
                if (Impu == "001")
                    Impu = "ISR";
                if (Impu == "002")
                    Impu = "IVA";
                if (Impu == "003")
                    Impu = "IEPS";


                List<c_TasaOCuota> C = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                    C = db.c_TasaOCuota.Where(p => p.Impuesto == Impu && p.Retencion=="Sí" && p.Factor==tipoFactor).ToList();

                    return C;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }//_-----------------------------------------------------------

        public List<c_TasaOCuota> Consultar_TasaCuota(string Impu, string tipoFactor, string tipoReteTras, ref bool rango)
        {
            try
            {
                if (Impu == "001")
                    Impu = "ISR";
                if (Impu == "002")
                    Impu = "IVA";
                if (Impu == "003")
                    Impu = "IEPS";


                List<c_TasaOCuota> C = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                    if (tipoReteTras == "Retenciones")
                        C = db.c_TasaOCuota.Where(p => p.Impuesto == Impu && p.Retencion == "Sí" && p.Factor == tipoFactor).OrderByDescending(p => p.Maximo).ToList();
                    if (tipoReteTras == "Traslados")
                        C = db.c_TasaOCuota.Where(p => p.Impuesto == Impu && p.Traslado == "Sí" && p.Factor == tipoFactor).OrderByDescending(p => p.Maximo).ToList();

                    if (C != null)
                        if (C[0].RangoOFijo == "Rango")
                            rango = true;
                    return C;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }
        //_-----------------------------------------------------------

        public c_TipoDeComprobante Consultar_TipoDeComprobante(string TC)
        {
            try
            {
                c_TipoDeComprobante C = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                    C = db.c_TipoDeComprobante.FirstOrDefault(p => p.c_TipoDeComprobante1 == TC);

                    return C;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }
        //------------------------------------------------------
        public List<c_ClaveUnidad> ConsultarClaveUnidadAll()
        {
            try
            {
                List<c_ClaveUnidad> C = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {

                    C = db.c_ClaveUnidad.OrderBy(p => p.Nombre).ToList();
                   

                    return C;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }
        

        //_-----------------------------------------------------------
        public List<c_ClaveUnidad> ConsultarClaveUnidadMasUsadas()
        {
            try
            {
                List<c_ClaveUnidad> C = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {

                    C = db.c_ClaveUnidad.Where(p => p.Nombre == "Pieza" || p.Nombre == "Kilogramo"
                        || p.Nombre == "Litro" || p.Nombre == "Lote" || p.Nombre == "Gramo" || p.Nombre == "Unidad de servicio")
                        .ToList();

                    c_ClaveUnidad c = new c_ClaveUnidad();
                    c.Nombre = "Otros";
                    c.c_ClaveUnidad1 = "0";
                    C.Add(c);
                    return C;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }


        //_-----------------------------------------------------------
        //------------------------------------------------------
        public List<Bancos> ConsultarBancosAll()
        {
            try
            {
                List<Bancos> C = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {

                    C = db.Bancos.ToList();


                    return C;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }


        //_-----------------------------------------------------------
        public c_ClaveProdServ Consultar_ClaveProdServ(Int64 Clave)
        {
            try
            {
                c_ClaveProdServ C = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                    C = db.c_ClaveProdServ.FirstOrDefault(p => p.c_ClaveProdServ1 == Clave );

                    return C;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }
        public List<c_ClaveProdServ> Consultar_ClaveProdServAll()
        {
            try
            {
               List< c_ClaveProdServ> C = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {

                    C = db.c_ClaveProdServ.Where(p => p.c_ClaveProdServ1 > 12161801 && p.c_ClaveProdServ1 < 39112305).ToList();

                    return C;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }
        public List<c_ClaveProdServ> ClaveProdServSearch(string query)
        {
            try
            {
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                        var x = db.c_ClaveProdServ.Where(p => p.Descripcion.ToLower().Contains(query)).ToList();
                        return x;
                }
            }
            catch (Exception ee)
            {
                return null;
            }

        }


        //------------------------------------------------------------------------
      
        public c_UsoCFDI Consultar_USOCFDI(string uso_CFDI)
        {
            try
            {
                c_UsoCFDI pais = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                    pais = db.c_UsoCFDI.FirstOrDefault(p => p.c_UsoCFDI1 == uso_CFDI);

                    return pais;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }

        public c_NumPedimentoAduana Consultar_NumPedimentoAduana(int c_Aduana)
        {
            try
            {
                c_NumPedimentoAduana N = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                    N = db.c_NumPedimentoAduana.FirstOrDefault(p => p.c_Aduana == c_Aduana);

                    return N;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }
       
       

        public c_Pais Consultar_Pais( string pais1)
        {
            try
            {
                if (!string.IsNullOrEmpty(pais1))
                
                pais1 = pais1.Replace("Item", "");
             
                c_Pais pais = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                    pais = db.c_Pais.FirstOrDefault(p => p.c_Pais1 == pais1);

                    return pais;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }
        public c_Estado Consultar_EstadosPais(string E, string pais)
        {
            try
            {
                if (!string.IsNullOrEmpty(E))
                
                E = E.Replace("Item", "");
                if (!string.IsNullOrEmpty(pais))
                    pais = pais.Replace("Item", "");
              
                c_Estado Estado = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                    Estado = db.c_Estado.FirstOrDefault(p => p.c_Estado1 == E && p.c_Pais == pais);

                    return Estado;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }

        public c_Estado Consultar_Estados(string E)
        {
            try
            {
                if (!string.IsNullOrEmpty(E))
                
               E= E.Replace("Item", "");
                c_Estado Estado=null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                     Estado = db.c_Estado.FirstOrDefault(p => p.c_Estado1 == E && p.c_Pais=="MEX");

                     return Estado;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }

        public List<c_Estado> Consultar_EstadosALL() //solo para 
        {
            try
            {
               List< c_Estado> Estado = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                    Estado = db.c_Estado.Where(p=> p.c_Pais == "MEX" ).ToList();

                    return Estado;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }

        public List<c_Municipio> Consultar_MunicipioALL(string estado) //solo para 
        {
            try
            {
                List<c_Municipio> Estado = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                    Estado = db.c_Municipio.Where(p => p.c_Estado == estado ).ToList();

                    return Estado;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }
        public List<c_CP> Consultar_CPALL(string estado, string municipio)
        {
            try
            {
               List <c_CP> CP = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                    CP = db.c_CP.Where(p => p.c_Estado == estado && p.c_Municipio==municipio ).ToList();

                    return CP;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }
        

        public c_Municipio Consultar_Municipio(string E,string M)
        {
            try
            {
                if (!string.IsNullOrEmpty(E))
                 E = E.Replace("Item", "");
                if (!string.IsNullOrEmpty(M))
                    M = M.Replace("Item", "");
              

                c_Municipio Municipio=null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                 
                    if(E!="")
                    Municipio = db.c_Municipio.FirstOrDefault(p => p.c_Estado == E && p.c_Municipio1 == M);
                    else
                        Municipio = db.c_Municipio.FirstOrDefault(p => p.c_Municipio1 == M);
                    
                    return Municipio;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }
        public c_Municipio Consultar_MunicipioMEX(string E, string M)
        {
            try
            {
                if (!string.IsNullOrEmpty(E))
                    E = E.Replace("Item", "");
                if (!string.IsNullOrEmpty(M))
                    M = M.Replace("Item", "");
              
                //c_Municipio Municipio = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                    var results = (from p in db.c_Municipio
                                   join d in db.c_Estado on  p.c_Estado equals  d.c_Estado1 
                                   where (p.c_Estado == E && p.c_Municipio1==M && d.c_Pais=="MEX")
                                    select p).First();

                    return results;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }
        public c_Localidad Consultar_LocalidadMEX(string E, string L)
        {
            try
            {
                if (!string.IsNullOrEmpty(E))
                    E = E.Replace("Item", "");
                if (!string.IsNullOrEmpty(L))
                    L = L.Replace("Item", "");
              
              //  c_Localidad Localidad = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                    var results = (from p in db.c_Localidad
                                   join d in db.c_Estado on p.c_Estado equals d.c_Estado1
                                   where (p.c_Estado == E && p.c_Localidad1 == L && d.c_Pais == "MEX")
                                   select p).First();

                    return results;

                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }

        public c_Localidad Consultar_Localidad(string E, string L)
        {
            try
            {
                if (!string.IsNullOrEmpty(E))
                
                E = E.Replace("Item", "");
                if (!string.IsNullOrEmpty(L))
                    L = L.Replace("Item", "");
              
                c_Localidad Localidad=null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                    if(E!="")
                    Localidad = db.c_Localidad.FirstOrDefault(p => p.c_Estado == E && p.c_Localidad1 == L);
                    else
                        Localidad = db.c_Localidad.FirstOrDefault(p => p.c_Localidad1 == L);
                    
                    return Localidad;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }
        public c_Colonia Consultar_ColoniaMEX(string CP, string C)
        {
            try
            {
                if (!string.IsNullOrEmpty(CP))
                
                CP =CP.Replace("Item", "");
                if (!string.IsNullOrEmpty(C))
                    C = C.Replace("Item", "");
              
                //c_Colonia Localidad=null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                    var results = (from c in db.c_Colonia
                                   join d in db.c_CP on c.c_CodigoPostal equals d.c_CP1
                                   join p in db.c_Estado on d.c_Estado equals p.c_Estado1

                                   where (c.c_Colonia1 == C && c.c_CodigoPostal==CP && p.c_Pais == "MEX")
                                   select c).First();

                    return results;

                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }

        public c_Colonia Consultar_Colonia(string CP, string C)
        {
            try
            {
                if (!string.IsNullOrEmpty(CP))
                
                CP = CP.Replace("Item", "");
                if (!string.IsNullOrEmpty(C))
                    C = C.Replace("Item", "");
              
             
                c_Colonia Localidad = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                    if (CP != "")
                        Localidad = db.c_Colonia.FirstOrDefault(p => p.c_CodigoPostal == CP && p.c_Colonia1 == C);
                    else
                        Localidad = db.c_Colonia.FirstOrDefault(p => p.c_Colonia1 == C);

                    return Localidad;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }
        public c_CP Consultar_CP(string E, string M,string L, string cp)
        {
            try
            {
                if (!string.IsNullOrEmpty(cp))
                cp = cp.Replace("Item", "");
                if(!string.IsNullOrEmpty( L))
                L = L.Replace("Item", "");
                if (!string.IsNullOrEmpty(E))
                 E =E.Replace("Item", "");
                if (!string.IsNullOrEmpty(M))
                M = M.Replace("Item", "");
              
             
                c_CP CP=null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                    if (!string.IsNullOrEmpty(E) && !string.IsNullOrEmpty(M) && !string.IsNullOrEmpty(L))
                    {
                        CP = db.c_CP.FirstOrDefault(p => p.c_Estado == E && p.c_Municipio == M && p.c_Localidad == L && p.c_CP1 == cp);
                        return CP;
                    }
                    if (!string.IsNullOrEmpty(E) && !string.IsNullOrEmpty(M))
                    {
                        CP = db.c_CP.FirstOrDefault(p => p.c_Estado == E && p.c_Municipio == M && p.c_CP1 == cp);
                        return CP;
                    }
                    if (!string.IsNullOrEmpty(E) && !string.IsNullOrEmpty(L))
                    {
                        CP = db.c_CP.FirstOrDefault(p => p.c_Estado == E && p.c_Localidad == L && p.c_CP1 == cp);
                        return CP;
                    }
                    if (!string.IsNullOrEmpty(M) && !string.IsNullOrEmpty(L))
                    {
                        CP = db.c_CP.FirstOrDefault(p => p.c_Municipio == M && p.c_Localidad == L && p.c_CP1 == cp);
                        return CP;
                    }
                    if (!string.IsNullOrEmpty(E))
                    {
                        CP = db.c_CP.FirstOrDefault(p => p.c_Estado == E && p.c_CP1 == cp);
                        return CP;
                    }
                    if (!string.IsNullOrEmpty(M))
                    {
                        CP = db.c_CP.FirstOrDefault(p => p.c_Municipio == M && p.c_CP1 == cp);
                        return CP;
                    }
                    if (!string.IsNullOrEmpty(L))
                    {
                        CP = db.c_CP.FirstOrDefault(p => p.c_Localidad == L && p.c_CP1 == cp);
                        return CP;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }
        public c_CP Consultar_CPMEX(string E, string M, string L, string cp)
        {
            try
            {
                if (!string.IsNullOrEmpty(cp))
                cp = cp.Replace("Item", "");
                if (!string.IsNullOrEmpty(L))
                L = L.Replace("Item", "");
                if (!string.IsNullOrEmpty(E))
                 E = E.Replace("Item", "");
                if (!string.IsNullOrEmpty(M))
                 M = M.Replace("Item", "");
              
             
                c_CP CP = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                    var results = (from c in db.c_CP
                                   join p in db.c_Estado on c.c_Estado equals p.c_Estado1

                                   where (c.c_Estado == E && c.c_Municipio ==M && c.c_Localidad==L &&c.c_CP1==cp && p.c_Pais == "MEX")
                                   select c).First();

                    return results;

                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }
        


        public c_RegimenFiscal Consultar_RegimenFiscal(string clave)
        {
            try
            {
                c_RegimenFiscal regimen = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                    regimen = db.c_RegimenFiscal.FirstOrDefault(p => p.c_RegimenFiscal1 == clave);

                    return regimen;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }
        public c_Pais Consultar_PaisVerificacionLinea(string clave)
        {
            try
            {
                c_Pais pais = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                    pais = db.c_Pais.FirstOrDefault(p => p.c_Pais1 == clave);

                    return pais;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }
        public c_CP Consultar_CP( string cp)
        {
            try
            {
                c_CP CP = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                      CP = db.c_CP.FirstOrDefault(p => p.c_CP1 == cp);

                    return CP;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }
        public c_FraccionArancelaria Consultar_FraccionArancelaria(string f)
        {
            try
            {
                c_FraccionArancelaria CP = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                    CP = db.c_FraccionArancelaria.FirstOrDefault(p => p.c_FraccionArancelaria1 == f);

                    return CP;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }
        public UnidadMedida Consultar_UnidadMedida(string f)
        {
            try
            {
                int x = Convert.ToInt16(f);
                string x1 = x.ToString();
                UnidadMedida UM = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                    UM = db.UnidadMedida.FirstOrDefault(p => p.C_UnidadMedida == x1);

                    return UM;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }

        public c_Moneda Consultar_Moneda(string moneda)
        {
            try
            {
                c_Moneda CP = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                    CP = db.c_Moneda.FirstOrDefault(p => p.c_Moneda1 ==moneda);

                    return CP;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }

        public List< c_Moneda> Consultar_MonedaAll()
        {
            try
            {
               List< c_Moneda> CP = null;
                using (var db = new CatalogosSAT.CatalogosEntities1())
                {
                    CP = db.c_Moneda.ToList();
                    
                    return CP;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

        }
    }
}
