namespace Contract.Complemento
{
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System.Collections;
    using System.Xml.Schema;
    using System.ComponentModel;
    using System.Collections.Generic;

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/Pagos")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.sat.gob.mx/Pagos", IsNullable = false)]
   
    public class Pagos
    {

        [XmlAttribute("schemaLocation", Namespace = XmlSchema.InstanceNamespace)]
        public string xsiSchemaLocation =
            "http://www.sat.gob.mx/Pagos http://www.sat.gob.mx/sitio_internet/cfd/Pagos/Pagos10.xsd";

        private List<PagosPago> pagoField;

        private string versionField;

        public Pagos()
        {
        //    this.pagoField = new List<PagosPago>();
        //    this.versionField = "1.0";
        }
         //  [System.Xml.Serialization.XmlArrayItemAttribute("Pago")]
           [XmlElement("Pago")]
     
        public List<PagosPago> Pago
        {
            get
            {
                return this.pagoField;
            }
            set
            {
                this.pagoField = value;
            }
        }
     
        [System.Xml.Serialization.XmlAttributeAttribute()]
     
        public string Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/Pagos")]
   
    public class PagosPago
    {

        private PagosPagoDoctoRelacionado[] doctoRelacionadoField;

        private PagosPagoImpuestos[] impuestosField;

       // private System.DateTime fechaPagoField;
        private System.String fechaPagoField;

       // private c_FormaPago formaDePagoPField;
        private string formaDePagoPField;

        //private c_Moneda monedaPField;
        private string monedaPField;

        private decimal tipoCambioPField;

        private bool tipoCambioPFieldSpecified;

        private string montoField;

        private string numOperacionField;

        private string rfcEmisorCtaOrdField;

        private string nomBancoOrdExtField;

        private string ctaOrdenanteField;

        private string rfcEmisorCtaBenField;

        private string ctaBeneficiarioField;

        //private c_TipoCadenaPago tipoCadPagoField;
        private string tipoCadPagoField;


        private bool tipoCadPagoFieldSpecified;

        //private byte[] certPagoField;
        private string certPagoField;

        private string cadPagoField;

        private string selloPagoField;

       // private byte[] selloPagoField;

        public PagosPago()
        {
        //    this.impuestosField = new List<PagosPagoImpuestos>();
        //    this.doctoRelacionadoField = new List<PagosPagoDoctoRelacionado>();
        }
        //   [System.Xml.Serialization.XmlArrayItemAttribute("DoctoRelacionado")]
           [XmlElement("DoctoRelacionado")]
     
        public PagosPagoDoctoRelacionado[] DoctoRelacionado
        {
            get
            {
                return this.doctoRelacionadoField;
            }
            set
            {
                this.doctoRelacionadoField = value;
            }
        }
        //   [System.Xml.Serialization.XmlArrayItemAttribute("Impuestos", IsNullable = false)]
           [XmlElement("Impuestos")]
     
        public PagosPagoImpuestos[] Impuestos
        {
            get
            {
                return this.impuestosField;
            }
            set
            {
                this.impuestosField = value;
            }
        }
     
        [System.Xml.Serialization.XmlAttributeAttribute()]
     
      //  public System.DateTime FechaPago
           public System.String FechaPago
           {
            get
            {
                return this.fechaPagoField;
            }
            set
            {
                this.fechaPagoField = value;
            }
        }
     
        [System.Xml.Serialization.XmlAttributeAttribute()]
     /*
        public c_FormaPago FormaDePagoP
        {
            get
            {
                return this.formaDePagoPField;
            }
            set
            {
                this.formaDePagoPField = value;
            }
        }
     */
        public string FormaDePagoP
        {
            get
            {
                return this.formaDePagoPField;
            }
            set
            {
                this.formaDePagoPField = value;
            }
        }
        [System.Xml.Serialization.XmlAttributeAttribute()]
     /*
        public c_Moneda MonedaP
        {
            get
            {
                return this.monedaPField;
            }
            set
            {
                this.monedaPField = value;
            }
        }
     */
        public string MonedaP
        {
            get
            {
                return this.monedaPField;
            }
            set
            {
                this.monedaPField = value;
            }
        }
        [System.Xml.Serialization.XmlAttributeAttribute()]
     
        public decimal TipoCambioP
        {
            get
            {
                return this.tipoCambioPField;
            }
            set
            {
                this.tipoCambioPField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TipoCambioPSpecified
        {
            get
            {
                return this.tipoCambioPFieldSpecified;
            }
            set
            {
                this.tipoCambioPFieldSpecified = value;
            }
        }
           [System.Xml.Serialization.XmlAttributeAttribute()]
     
        public string Monto
        {
            get
            {
                return this.montoField;
            }
            set
            {
                this.montoField = value;
            }
        }
           [System.Xml.Serialization.XmlAttributeAttribute()]
     
        public string NumOperacion
        {
            get
            {
                return this.numOperacionField;
            }
            set
            {
                this.numOperacionField = value;
            }
        }
           [System.Xml.Serialization.XmlAttributeAttribute()]
     
        public string RfcEmisorCtaOrd
        {
            get
            {
                return this.rfcEmisorCtaOrdField;
            }
            set
            {
                this.rfcEmisorCtaOrdField = value;
            }
        }
           [System.Xml.Serialization.XmlAttributeAttribute()]
     
        public string NomBancoOrdExt
        {
            get
            {
                return this.nomBancoOrdExtField;
            }
            set
            {
                this.nomBancoOrdExtField = value;
            }
        }
           [System.Xml.Serialization.XmlAttributeAttribute()]
     
        public string CtaOrdenante
        {
            get
            {
                return this.ctaOrdenanteField;
            }
            set
            {
                this.ctaOrdenanteField = value;
            }
        }
           [System.Xml.Serialization.XmlAttributeAttribute()]
     
        public string RfcEmisorCtaBen
        {
            get
            {
                return this.rfcEmisorCtaBenField;
            }
            set
            {
                this.rfcEmisorCtaBenField = value;
            }
        }
           [System.Xml.Serialization.XmlAttributeAttribute()]
     
        public string CtaBeneficiario
        {
            get
            {
                return this.ctaBeneficiarioField;
            }
            set
            {
                this.ctaBeneficiarioField = value;
            }
        }

          [System.Xml.Serialization.XmlAttributeAttribute()]
     
        //public c_TipoCadenaPago TipoCadPago
           public string TipoCadPago
           {
            get
            {
                return this.tipoCadPagoField;
            }
            set
            {
                this.tipoCadPagoField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TipoCadPagoSpecified
        {
            get
            {
                return this.tipoCadPagoFieldSpecified;
            }
            set
            {
                this.tipoCadPagoFieldSpecified = value;
            }
        }
          [System.Xml.Serialization.XmlAttributeAttribute()]
     
        public string CertPago
        {
            get
            {
                return this.certPagoField;
            }
            set
            {
                this.certPagoField = value;
            }
        }
        
        [System.Xml.Serialization.XmlAttributeAttribute()]
     
        public string CadPago
        {
            get
            {
                return this.cadPagoField;
            }
            set
            {
                this.cadPagoField = value;
            }
        }
          [System.Xml.Serialization.XmlAttributeAttribute()]
     /*
        public byte[] SelloPago
        {
            get
            {
                return this.selloPagoField;
            }
            set
            {
                this.selloPagoField = value;
            }
        }
      */
        public string SelloPago
        {
            get
            {
                return this.selloPagoField;
            }
            set
            {
                this.selloPagoField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/Pagos")]
  
    public partial class PagosPagoDoctoRelacionado
    {

        private string idDocumentoField;

        private string serieField;

        private string folioField;

       // private c_Moneda monedaDRField;
        private string monedaDRField;

        private decimal tipoCambioDRField;

        private bool tipoCambioDRFieldSpecified;

       // private c_MetodoPago metodoDePagoDRField;
        private string metodoDePagoDRField;

        private string numParcialidadField;

        private string impSaldoAntField;

        private bool impSaldoAntFieldSpecified;

        private string impPagadoField;

        private bool impPagadoFieldSpecified;

        private string impSaldoInsolutoField;

        private bool impSaldoInsolutoFieldSpecified;
        
        [System.Xml.Serialization.XmlAttributeAttribute()]
        
        public string IdDocumento
        {
            get
            {
                return this.idDocumentoField;
            }
            set
            {
                this.idDocumentoField = value;
            }
        }
         [System.Xml.Serialization.XmlAttributeAttribute()]
        
        public string Serie
        {
            get
            {
                return this.serieField;
            }
            set
            {
                this.serieField = value;
            }
        }
         [System.Xml.Serialization.XmlAttributeAttribute()]
        
        public string Folio
        {
            get
            {
                return this.folioField;
            }
            set
            {
                this.folioField = value;
            }
        }
        
        [System.Xml.Serialization.XmlAttributeAttribute()]
        /*
        public c_Moneda MonedaDR
        {
            get
            {
                return this.monedaDRField;
            }
            set
            {
                this.monedaDRField = value;
            }
        }
         */
         public string MonedaDR
         {
             get
             {
                 return this.monedaDRField;
             }
             set
             {
                 this.monedaDRField = value;
             }
         }
        

         [System.Xml.Serialization.XmlAttributeAttribute()]
        
        public decimal TipoCambioDR
        {
            get
            {
                return this.tipoCambioDRField;
            }
            set
            {
                this.tipoCambioDRField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TipoCambioDRSpecified
        {
            get
            {
                return this.tipoCambioDRFieldSpecified;
            }
            set
            {
                this.tipoCambioDRFieldSpecified = value;
            }
        }
         [System.Xml.Serialization.XmlAttributeAttribute()]
        /*
        public c_MetodoPago MetodoDePagoDR
        {
            get
            {
                return this.metodoDePagoDRField;
            }
            set
            {
                this.metodoDePagoDRField = value;
            }
        }
         */
        public string MetodoDePagoDR
        {
            get
            {
                return this.metodoDePagoDRField;
            }
            set
            {
                this.metodoDePagoDRField = value;
            }
        }

         [System.Xml.Serialization.XmlAttributeAttribute()]
        
        public string NumParcialidad
        {
            get
            {
                return this.numParcialidadField;
            }
            set
            {
                this.numParcialidadField = value;
            }
        }
         [System.Xml.Serialization.XmlAttributeAttribute()]
        
        public string ImpSaldoAnt
        {
            get
            {
                return this.impSaldoAntField;
            }
            set
            {
                this.impSaldoAntField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ImpSaldoAntSpecified
        {
            get
            {
                return this.impSaldoAntFieldSpecified;
            }
            set
            {
                this.impSaldoAntFieldSpecified = value;
            }
        }
         [System.Xml.Serialization.XmlAttributeAttribute()]
        
        public string ImpPagado
        {
            get
            {
                return this.impPagadoField;
            }
            set
            {
                this.impPagadoField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ImpPagadoSpecified
        {
            get
            {
                return this.impPagadoFieldSpecified;
            }
            set
            {
                this.impPagadoFieldSpecified = value;
            }
        }
         [System.Xml.Serialization.XmlAttributeAttribute()]
        
        public string ImpSaldoInsoluto
        {
            get
            {
                return this.impSaldoInsolutoField;
            }
            set
            {
                this.impSaldoInsolutoField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ImpSaldoInsolutoSpecified
        {
            get
            {
                return this.impSaldoInsolutoFieldSpecified;
            }
            set
            {
                this.impSaldoInsolutoFieldSpecified = value;
            }
        }
    }

    public enum c_Moneda
    {

        /// <comentarios/>
        AED,

        /// <comentarios/>
        AFN,

        /// <comentarios/>
        ALL,

        /// <comentarios/>
        AMD,

        /// <comentarios/>
        ANG,

        /// <comentarios/>
        AOA,

        /// <comentarios/>
        ARS,

        /// <comentarios/>
        AUD,

        /// <comentarios/>
        AWG,

        /// <comentarios/>
        AZN,

        /// <comentarios/>
        BAM,

        /// <comentarios/>
        BBD,

        /// <comentarios/>
        BDT,

        /// <comentarios/>
        BGN,

        /// <comentarios/>
        BHD,

        /// <comentarios/>
        BIF,

        /// <comentarios/>
        BMD,

        /// <comentarios/>
        BND,

        /// <comentarios/>
        BOB,

        /// <comentarios/>
        BOV,

        /// <comentarios/>
        BRL,

        /// <comentarios/>
        BSD,

        /// <comentarios/>
        BTN,

        /// <comentarios/>
        BWP,

        /// <comentarios/>
        BYR,

        /// <comentarios/>
        BZD,

        /// <comentarios/>
        CAD,

        /// <comentarios/>
        CDF,

        /// <comentarios/>
        CHE,

        /// <comentarios/>
        CHF,

        /// <comentarios/>
        CHW,

        /// <comentarios/>
        CLF,

        /// <comentarios/>
        CLP,

        /// <comentarios/>
        CNY,

        /// <comentarios/>
        COP,

        /// <comentarios/>
        COU,

        /// <comentarios/>
        CRC,

        /// <comentarios/>
        CUC,

        /// <comentarios/>
        CUP,

        /// <comentarios/>
        CVE,

        /// <comentarios/>
        CZK,

        /// <comentarios/>
        DJF,

        /// <comentarios/>
        DKK,

        /// <comentarios/>
        DOP,

        /// <comentarios/>
        DZD,

        /// <comentarios/>
        EGP,

        /// <comentarios/>
        ERN,

        /// <comentarios/>
        ETB,

        /// <comentarios/>
        EUR,

        /// <comentarios/>
        FJD,

        /// <comentarios/>
        FKP,

        /// <comentarios/>
        GBP,

        /// <comentarios/>
        GEL,

        /// <comentarios/>
        GHS,

        /// <comentarios/>
        GIP,

        /// <comentarios/>
        GMD,

        /// <comentarios/>
        GNF,

        /// <comentarios/>
        GTQ,

        /// <comentarios/>
        GYD,

        /// <comentarios/>
        HKD,

        /// <comentarios/>
        HNL,

        /// <comentarios/>
        HRK,

        /// <comentarios/>
        HTG,

        /// <comentarios/>
        HUF,

        /// <comentarios/>
        IDR,

        /// <comentarios/>
        ILS,

        /// <comentarios/>
        INR,

        /// <comentarios/>
        IQD,

        /// <comentarios/>
        IRR,

        /// <comentarios/>
        ISK,

        /// <comentarios/>
        JMD,

        /// <comentarios/>
        JOD,

        /// <comentarios/>
        JPY,

        /// <comentarios/>
        KES,

        /// <comentarios/>
        KGS,

        /// <comentarios/>
        KHR,

        /// <comentarios/>
        KMF,

        /// <comentarios/>
        KPW,

        /// <comentarios/>
        KRW,

        /// <comentarios/>
        KWD,

        /// <comentarios/>
        KYD,

        /// <comentarios/>
        KZT,

        /// <comentarios/>
        LAK,

        /// <comentarios/>
        LBP,

        /// <comentarios/>
        LKR,

        /// <comentarios/>
        LRD,

        /// <comentarios/>
        LSL,

        /// <comentarios/>
        LYD,

        /// <comentarios/>
        MAD,

        /// <comentarios/>
        MDL,

        /// <comentarios/>
        MGA,

        /// <comentarios/>
        MKD,

        /// <comentarios/>
        MMK,

        /// <comentarios/>
        MNT,

        /// <comentarios/>
        MOP,

        /// <comentarios/>
        MRO,

        /// <comentarios/>
        MUR,

        /// <comentarios/>
        MVR,

        /// <comentarios/>
        MWK,

        /// <comentarios/>
        MXN,

        /// <comentarios/>
        MXV,

        /// <comentarios/>
        MYR,

        /// <comentarios/>
        MZN,

        /// <comentarios/>
        NAD,

        /// <comentarios/>
        NGN,

        /// <comentarios/>
        NIO,

        /// <comentarios/>
        NOK,

        /// <comentarios/>
        NPR,

        /// <comentarios/>
        NZD,

        /// <comentarios/>
        OMR,

        /// <comentarios/>
        PAB,

        /// <comentarios/>
        PEN,

        /// <comentarios/>
        PGK,

        /// <comentarios/>
        PHP,

        /// <comentarios/>
        PKR,

        /// <comentarios/>
        PLN,

        /// <comentarios/>
        PYG,

        /// <comentarios/>
        QAR,

        /// <comentarios/>
        RON,

        /// <comentarios/>
        RSD,

        /// <comentarios/>
        RUB,

        /// <comentarios/>
        RWF,

        /// <comentarios/>
        SAR,

        /// <comentarios/>
        SBD,

        /// <comentarios/>
        SCR,

        /// <comentarios/>
        SDG,

        /// <comentarios/>
        SEK,

        /// <comentarios/>
        SGD,

        /// <comentarios/>
        SHP,

        /// <comentarios/>
        SLL,

        /// <comentarios/>
        SOS,

        /// <comentarios/>
        SRD,

        /// <comentarios/>
        SSP,

        /// <comentarios/>
        STD,

        /// <comentarios/>
        SVC,

        /// <comentarios/>
        SYP,

        /// <comentarios/>
        SZL,

        /// <comentarios/>
        THB,

        /// <comentarios/>
        TJS,

        /// <comentarios/>
        TMT,

        /// <comentarios/>
        TND,

        /// <comentarios/>
        TOP,

        /// <comentarios/>
        TRY,

        /// <comentarios/>
        TTD,

        /// <comentarios/>
        TWD,

        /// <comentarios/>
        TZS,

        /// <comentarios/>
        UAH,

        /// <comentarios/>
        UGX,

        /// <comentarios/>
        USD,

        /// <comentarios/>
        USN,

        /// <comentarios/>
        UYI,

        /// <comentarios/>
        UYU,

        /// <comentarios/>
        UZS,

        /// <comentarios/>
        VEF,

        /// <comentarios/>
        VND,

        /// <comentarios/>
        VUV,

        /// <comentarios/>
        WST,

        /// <comentarios/>
        XAF,

        /// <comentarios/>
        XAG,

        /// <comentarios/>
        XAU,

        /// <comentarios/>
        XBA,

        /// <comentarios/>
        XBB,

        /// <comentarios/>
        XBC,

        /// <comentarios/>
        XBD,

        /// <comentarios/>
        XCD,

        /// <comentarios/>
        XDR,

        /// <comentarios/>
        XOF,

        /// <comentarios/>
        XPD,

        /// <comentarios/>
        XPF,

        /// <comentarios/>
        XPT,

        /// <comentarios/>
        XSU,

        /// <comentarios/>
        XTS,

        /// <comentarios/>
        XUA,

        /// <comentarios/>
        XXX,

        /// <comentarios/>
        YER,

        /// <comentarios/>
        ZAR,

        /// <comentarios/>
        ZMW,

        /// <comentarios/>
        ZWL,
    }

    public enum c_MetodoPago
    {

        /// <comentarios/>
        PUE,

        /// <comentarios/>
        PIP,

        /// <comentarios/>
        PPD,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/Pagos")]
  
    public partial class PagosPagoImpuestos
    {

        private List<PagosPagoImpuestosRetencion> retencionesField;

        private List<PagosPagoImpuestosTraslado> trasladosField;

        private decimal totalImpuestosRetenidosField;

        private bool totalImpuestosRetenidosFieldSpecified;

        private decimal totalImpuestosTrasladadosField;

        private bool totalImpuestosTrasladadosFieldSpecified;

        public PagosPagoImpuestos()
        {
       //     this.trasladosField = new List<PagosPagoImpuestosTraslado>();
        //    this.retencionesField = new List<PagosPagoImpuestosRetencion>();
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("Retencion", IsNullable = false)]
        public List<PagosPagoImpuestosRetencion> Retenciones
        {
            get
            {
                return this.retencionesField;
            }
            set
            {
                this.retencionesField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("Traslado", IsNullable = false)]
        public List<PagosPagoImpuestosTraslado> Traslados
        {
            get
            {
                return this.trasladosField;
            }
            set
            {
                this.trasladosField = value;
            }
        }
         [System.Xml.Serialization.XmlAttributeAttribute()]
        
        public decimal TotalImpuestosRetenidos
        {
            get
            {
                return this.totalImpuestosRetenidosField;
            }
            set
            {
                this.totalImpuestosRetenidosField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TotalImpuestosRetenidosSpecified
        {
            get
            {
                return this.totalImpuestosRetenidosFieldSpecified;
            }
            set
            {
                this.totalImpuestosRetenidosFieldSpecified = value;
            }
        }
         [System.Xml.Serialization.XmlAttributeAttribute()]
        
        public decimal TotalImpuestosTrasladados
        {
            get
            {
                return this.totalImpuestosTrasladadosField;
            }
            set
            {
                this.totalImpuestosTrasladadosField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TotalImpuestosTrasladadosSpecified
        {
            get
            {
                return this.totalImpuestosTrasladadosFieldSpecified;
            }
            set
            {
                this.totalImpuestosTrasladadosFieldSpecified = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/Pagos")]
  
    public partial class PagosPagoImpuestosRetencion
    {

        private c_Impuesto impuestoField;

        private decimal importeField;
        
    //    [System.Xml.Serialization.XmlAttributeAttribute()]
        
        public c_Impuesto Impuesto
        {
            get
            {
                return this.impuestoField;
            }
            set
            {
                this.impuestoField = value;
            }
        }
         [System.Xml.Serialization.XmlAttributeAttribute()]
        
        public decimal Importe
        {
            get
            {
                return this.importeField;
            }
            set
            {
                this.importeField = value;
            }
        }
    }

    public enum c_Impuesto
    {

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("001")]
        Item001,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("002")]
        Item002,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("003")]
        Item003,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/Pagos")]
  
    public partial class PagosPagoImpuestosTraslado
    {

        private c_Impuesto impuestoField;

        private c_TipoFactor tipoFactorField;

        private decimal tasaOCuotaField;

        private decimal importeField;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        
        public c_Impuesto Impuesto
        {
            get
            {
                return this.impuestoField;
            }
            set
            {
                this.impuestoField = value;
            }
        }
         [System.Xml.Serialization.XmlAttributeAttribute()]
        
        public c_TipoFactor TipoFactor
        {
            get
            {
                return this.tipoFactorField;
            }
            set
            {
                this.tipoFactorField = value;
            }
        }
         [System.Xml.Serialization.XmlAttributeAttribute()]
        
        public decimal TasaOCuota
        {
            get
            {
                return this.tasaOCuotaField;
            }
            set
            {
                this.tasaOCuotaField = value;
            }
        }
         [System.Xml.Serialization.XmlAttributeAttribute()]
        
        public decimal Importe
        {
            get
            {
                return this.importeField;
            }
            set
            {
                this.importeField = value;
            }
        }
    }

    public enum c_TipoFactor
    {

        /// <comentarios/>
        Tasa,

        /// <comentarios/>
        Cuota,

        /// <comentarios/>
        Exento,
    }

    public enum c_FormaPago
    {

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("01")]
        Item01,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("02")]
        Item02,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("03")]
        Item03,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("04")]
        Item04,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("05")]
        Item05,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("06")]
        Item06,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("08")]
        Item08,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("12")]
        Item12,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("13")]
        Item13,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("14")]
        Item14,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("15")]
        Item15,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("17")]
        Item17,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("23")]
        Item23,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("24")]
        Item24,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("25")]
        Item25,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("26")]
        Item26,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("27")]
        Item27,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("28")]
        Item28,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("29")]
        Item29,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("99")]
        Item99,
    }

    public enum c_TipoCadenaPago
    {

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("01")]
        Item01,
    }
}
