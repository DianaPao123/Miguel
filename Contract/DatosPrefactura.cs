using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Contract;


namespace Contract
{

    public enum TipoDocumento
    {
        FacturaGeneral,
        FacturaTransportista,
        FacturaAduanera,
        Referencia,
        ReciboHonorarios,
        Constructor,
        Donativo,
        NotaCredito,
        Arrendamiento,
        FacturaGeneralFirmas,
        ConstructorFirmas,
        ConstructorFirmasCustom,
        FacturaLiverpool,
        FacturaMabe,
        FacturaDeloitte,
        FacturaSorianaCEDIS,
        FacturaSorianaTienda,
        FacturaAdo,
        CorporativoAduanal,
        FacturaPemex,
        FacturaLucent, Nomina, Amc71, CartaPorte, Coppel, HomeDepot, Pilgrims,
        Retenciones, Honda, Amazon, Complementos, ASONIOSCOC,ComplementoPagos
    }
     [Serializable()]
    public class DatosPrefactura
    {
        
        [DataMemberAttribute()]
        public string Regimen { get; set; }
        [DataMemberAttribute()]
        public TipoDocumento TipoDocumento { get; set; }
        [DataMemberAttribute()]
        public int IdEmpresa  { get; set; }
        [DataMemberAttribute()]
        public decimal Importe { get; set; }
        [DataMemberAttribute()]
        public decimal SubTotal { get; set; }
        [DataMemberAttribute()]
        public decimal Total { get; set; }
        [DataMemberAttribute()]
        public string MonedaID { get; set; }
        [DataMemberAttribute()]
        public int idcliente { get; set; }
        [DataMemberAttribute()]
        public DateTime Fecha { get; set; }
        [DataMemberAttribute()]
        public string Folio { get; set; }
        [DataMemberAttribute()]
        public string PreFolio { get; set; }
     
        [DataMemberAttribute()]
        public string Serie { get; set; }
        [DataMemberAttribute()]
        public int nProducto { get; set; }
        [DataMemberAttribute()]
        public DateTime captura { get; set; }
        [DataMemberAttribute()]
        public int Cancelado { get; set; }
        [DataMemberAttribute()]
        public Guid Usuario { get; set; }
        [DataMemberAttribute()]
        public string LugarExpedicion { get; set; }
        [DataMemberAttribute()]
        public string Proyecto { get; set; }
        [DataMemberAttribute()]
        public string MonedaS { get; set; }
        [DataMemberAttribute()]
        public string UsoCFDI { get; set; }
         [DataMemberAttribute()]
        public string FormaPagoID{ get; set; }
         [DataMemberAttribute()]
        public string FormaPago { get; set; }
         [DataMemberAttribute()]
        public string MetodoID { get; set; }
         [DataMemberAttribute()]
         public string Metodo { get; set; }
         [DataMemberAttribute()]
         public string TipoCambio { get; set; }
         [DataMemberAttribute()]
         public string Confirmacion { get; set; }
         [DataMemberAttribute()]
         public string CondicionesPago { get; set; }
         [DataMemberAttribute()]
         public decimal Descuento { get; set; }
         [DataMemberAttribute()]
         public string TipoDeComprobante { get; set; }
         [DataMemberAttribute()]
         public string uudi { get; set; } //para el real
         [DataMemberAttribute()]
         public int Estatus { get; set; }//0-pendiente,1-Pagado,2-parcialmente pagada(para prefactura) 
         [DataMemberAttribute()]
         public string CFDI { get; set; }//atributo para saber si es General(G),Complemento PAgo(P)
         [DataMemberAttribute()]
         public decimal IVA { get; set; }
         [DataMemberAttribute()]
         public decimal MontoTotalComplementoPago { get; set; }
         
         public List<Datosdetalle> Detalles { get; set; }
         public List<DatosPagos> Datospagos { get; set; }//para los pagos
         [DataMemberAttribute()]
         public string IdPrefacturas { get; set; }
         [DataMemberAttribute]
         public ImpLocales ImpuestosLocales { get; set; }
        [DataMemberAttribute]
         public string TipoRelacion { get; set; }// para CfdiRelacionados
        public List<string> UUID { get; set; } // para CfdiRelacionados
        [DataMemberAttribute()]
        public System.Guid promotor { get; set; }
      
         public DatosPrefactura()
        {
            Detalles = new List<Datosdetalle>();
            Datospagos = new List<DatosPagos>();
            UUID = new List<string>();
        }

    }
}
