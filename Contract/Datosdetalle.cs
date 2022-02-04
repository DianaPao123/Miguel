using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Contract
{
    [Serializable()]
    public partial class Datosdetalle
    {
               
        //-------------------------Buenos para CDFI3.3------------------------------------
        [DataMemberAttribute]
        public string ConceptoClaveProdServ { get; set; }
        [DataMemberAttribute]
        public string ConceptoNoIdentificacion { get; set; }
        [DataMemberAttribute]
        public decimal ConceptoCantidad { get; set; }
        [DataMemberAttribute]
        public string ConceptoClaveUnidad { get; set; }
        [DataMemberAttribute]
        public string ConceptoUnidad { get; set; }
        [DataMemberAttribute]
        public string ConceptoDescripcion { get; set; }
        [DataMemberAttribute]
        public decimal ConceptoValorUnitario { get; set; }
        [DataMemberAttribute]
        public decimal ConceptoImporte { get; set; }
        [DataMemberAttribute]
        public decimal? ConceptoDescuento { get; set; }
        [DataMemberAttribute]
        public int? Conceptoidproducto { get; set; }
        [DataMemberAttribute]
        public string Descripcion { get; set; }

        [DataMemberAttribute]
        public string ConceptoClaveUnidadDescripcion { get; set; }//para vista
        [DataMemberAttribute]
        public decimal IVA { get; set; }

        public List<string> NumeroPedimento { get; set; }//de informacion aduanera

        public List< DatosdetalleTraslado> ConceptoTraslados { get; set; }
        public List<DatosdetalleRetencion> ConceptoRetenciones { get; set; }
        
        [DataMemberAttribute]
        public string ConceptoCuentaPredial { get; set; }
        [DataMemberAttribute]
        
        public DatosdetalleParte ConceptoParte { get; set; }


        public Datosdetalle()
        {
        //    NumeroPedimento = null;
        //    ConceptoTraslados = null;
        //    Retenciones = null;
            ConceptoTraslados = new List<DatosdetalleTraslado>();
            ConceptoRetenciones = new List<DatosdetalleRetencion>();
        }
        //--------------------------------------------------------------
        

        [DataMemberAttribute]
        public int Partida { get; set; }
        
    }
}
