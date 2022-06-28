namespace Contract.Complemento40
{
    using System.Xml.Serialization;
    using System.Xml.Schema;

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/Pagos20")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.sat.gob.mx/Pagos20", IsNullable = false)]
    public partial class Pagos
    {
         [XmlAttribute("schemaLocation", Namespace = XmlSchema.InstanceNamespace)]
        public string xsiSchemaLocation ="http://www.sat.gob.mx/Pagos20 http://www.sat.gob.mx/sitio_internet/cfd/Pagos/Pagos20.xsd";
               
        private PagosTotales totalesField;

        private PagosPago[] pagoField;

        private string versionField;

        public Pagos()
        {
            this.versionField = "2.0";
        }

        /// <remarks/>
        public PagosTotales Totales
        {
            get
            {
                return this.totalesField;
            }
            set
            {
                this.totalesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Pago")]
        public PagosPago[] Pago
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

        /// <remarks/>
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

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/Pagos20")]
    public partial class PagosTotales
    {

        private string totalRetencionesIVAField;

        private bool totalRetencionesIVAFieldSpecified;

        private string totalRetencionesISRField;

        private bool totalRetencionesISRFieldSpecified;

        private string totalRetencionesIEPSField;

        private bool totalRetencionesIEPSFieldSpecified;

        private string totalTrasladosBaseIVA16Field;

        private bool totalTrasladosBaseIVA16FieldSpecified;

        private string totalTrasladosImpuestoIVA16Field;

        private bool totalTrasladosImpuestoIVA16FieldSpecified;

        private string totalTrasladosBaseIVA8Field;

        private bool totalTrasladosBaseIVA8FieldSpecified;

        private string totalTrasladosImpuestoIVA8Field;

        private bool totalTrasladosImpuestoIVA8FieldSpecified;

        private string totalTrasladosBaseIVA0Field;

        private bool totalTrasladosBaseIVA0FieldSpecified;

        private string totalTrasladosImpuestoIVA0Field;

        private bool totalTrasladosImpuestoIVA0FieldSpecified;

        private string totalTrasladosBaseIVAExentoField;

        private bool totalTrasladosBaseIVAExentoFieldSpecified;

        private string montoTotalPagosField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TotalRetencionesIVA
        {
            get
            {
                return this.totalRetencionesIVAField;
            }
            set
            {
                this.totalRetencionesIVAField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TotalRetencionesIVASpecified
        {
            get
            {
                return this.totalRetencionesIVAFieldSpecified;
            }
            set
            {
                this.totalRetencionesIVAFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TotalRetencionesISR
        {
            get
            {
                return this.totalRetencionesISRField;
            }
            set
            {
                this.totalRetencionesISRField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TotalRetencionesISRSpecified
        {
            get
            {
                return this.totalRetencionesISRFieldSpecified;
            }
            set
            {
                this.totalRetencionesISRFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TotalRetencionesIEPS
        {
            get
            {
                return this.totalRetencionesIEPSField;
            }
            set
            {
                this.totalRetencionesIEPSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TotalRetencionesIEPSSpecified
        {
            get
            {
                return this.totalRetencionesIEPSFieldSpecified;
            }
            set
            {
                this.totalRetencionesIEPSFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TotalTrasladosBaseIVA16
        {
            get
            {
                return this.totalTrasladosBaseIVA16Field;
            }
            set
            {
                this.totalTrasladosBaseIVA16Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TotalTrasladosBaseIVA16Specified
        {
            get
            {
                return this.totalTrasladosBaseIVA16FieldSpecified;
            }
            set
            {
                this.totalTrasladosBaseIVA16FieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TotalTrasladosImpuestoIVA16
        {
            get
            {
                return this.totalTrasladosImpuestoIVA16Field;
            }
            set
            {
                this.totalTrasladosImpuestoIVA16Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TotalTrasladosImpuestoIVA16Specified
        {
            get
            {
                return this.totalTrasladosImpuestoIVA16FieldSpecified;
            }
            set
            {
                this.totalTrasladosImpuestoIVA16FieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TotalTrasladosBaseIVA8
        {
            get
            {
                return this.totalTrasladosBaseIVA8Field;
            }
            set
            {
                this.totalTrasladosBaseIVA8Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TotalTrasladosBaseIVA8Specified
        {
            get
            {
                return this.totalTrasladosBaseIVA8FieldSpecified;
            }
            set
            {
                this.totalTrasladosBaseIVA8FieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TotalTrasladosImpuestoIVA8
        {
            get
            {
                return this.totalTrasladosImpuestoIVA8Field;
            }
            set
            {
                this.totalTrasladosImpuestoIVA8Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TotalTrasladosImpuestoIVA8Specified
        {
            get
            {
                return this.totalTrasladosImpuestoIVA8FieldSpecified;
            }
            set
            {
                this.totalTrasladosImpuestoIVA8FieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TotalTrasladosBaseIVA0
        {
            get
            {
                return this.totalTrasladosBaseIVA0Field;
            }
            set
            {
                this.totalTrasladosBaseIVA0Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TotalTrasladosBaseIVA0Specified
        {
            get
            {
                return this.totalTrasladosBaseIVA0FieldSpecified;
            }
            set
            {
                this.totalTrasladosBaseIVA0FieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TotalTrasladosImpuestoIVA0
        {
            get
            {
                return this.totalTrasladosImpuestoIVA0Field;
            }
            set
            {
                this.totalTrasladosImpuestoIVA0Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TotalTrasladosImpuestoIVA0Specified
        {
            get
            {
                return this.totalTrasladosImpuestoIVA0FieldSpecified;
            }
            set
            {
                this.totalTrasladosImpuestoIVA0FieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TotalTrasladosBaseIVAExento
        {
            get
            {
                return this.totalTrasladosBaseIVAExentoField;
            }
            set
            {
                this.totalTrasladosBaseIVAExentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TotalTrasladosBaseIVAExentoSpecified
        {
            get
            {
                return this.totalTrasladosBaseIVAExentoFieldSpecified;
            }
            set
            {
                this.totalTrasladosBaseIVAExentoFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MontoTotalPagos
        {
            get
            {
                return this.montoTotalPagosField;
            }
            set
            {
                this.montoTotalPagosField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/Pagos20")]
    public partial class PagosPago
    {

        private PagosPagoDoctoRelacionado[] doctoRelacionadoField;

        private PagosPagoImpuestosP impuestosPField;

        private string fechaPagoField;

        private string formaDePagoPField;

        private string monedaPField;

        private string tipoCambioPField;

        private bool tipoCambioPFieldSpecified;

        private string montoField;

        private string numOperacionField;

        private string rfcEmisorCtaOrdField;

        private string nomBancoOrdExtField;

        private string ctaOrdenanteField;

        private string rfcEmisorCtaBenField;

        private string ctaBeneficiarioField;

        private string tipoCadPagoField;

        private bool tipoCadPagoFieldSpecified;

       // private byte[] certPagoField;
        private string certPagoField;

        private string cadPagoField;

       // private byte[] selloPagoField;
        private string selloPagoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DoctoRelacionado")]
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

        /// <remarks/>
        public PagosPagoImpuestosP ImpuestosP
        {
            get
            {
                return this.impuestosPField;
            }
            set
            {
                this.impuestosPField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TipoCambioP
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

        /// <remarks/>
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

        /// <remarks/>
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

        /// <remarks/>
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

        /// <remarks/>
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

        /// <remarks/>
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

        /// <remarks/>
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

        /// <remarks/>
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

        /// <remarks/>
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
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

        /// <remarks/>
       // [System.Xml.Serialization.XmlAttributeAttribute(DataType = "base64Binary")]
        public string CertPago
         //public byte[] CertPago
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

        /// <remarks/>
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

        /// <remarks/>
       // [System.Xml.Serialization.XmlAttributeAttribute(DataType = "base64Binary")]
        //public byte[] SelloPago
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

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/Pagos20")]
    public partial class PagosPagoDoctoRelacionado
    {

        private PagosPagoDoctoRelacionadoImpuestosDR impuestosDRField;

        private string idDocumentoField;

        private string serieField;

        private string folioField;

        private string monedaDRField;

        private decimal equivalenciaDRField;

        private bool equivalenciaDRFieldSpecified;

        private string numParcialidadField;

        private decimal impSaldoAntField;

        private decimal impPagadoField;

        private decimal impSaldoInsolutoField;

        private string objetoImpDRField;

        /// <remarks/>
        public PagosPagoDoctoRelacionadoImpuestosDR ImpuestosDR
        {
            get
            {
                return this.impuestosDRField;
            }
            set
            {
                this.impuestosDRField = value;
            }
        }

        /// <remarks/>
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

        /// <remarks/>
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

        /// <remarks/>
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal EquivalenciaDR
        {
            get
            {
                return this.equivalenciaDRField;
            }
            set
            {
                this.equivalenciaDRField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool EquivalenciaDRSpecified
        {
            get
            {
                return this.equivalenciaDRFieldSpecified;
            }
            set
            {
                this.equivalenciaDRFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "integer")]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal ImpSaldoAnt
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal ImpPagado
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal ImpSaldoInsoluto
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ObjetoImpDR
        {
            get
            {
                return this.objetoImpDRField;
            }
            set
            {
                this.objetoImpDRField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/Pagos20")]
    public partial class PagosPagoDoctoRelacionadoImpuestosDR
    {

        private PagosPagoDoctoRelacionadoImpuestosDRRetencionDR[] retencionesDRField;

        private PagosPagoDoctoRelacionadoImpuestosDRTrasladoDR[] trasladosDRField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("RetencionDR", IsNullable = false)]
        public PagosPagoDoctoRelacionadoImpuestosDRRetencionDR[] RetencionesDR
        {
            get
            {
                return this.retencionesDRField;
            }
            set
            {
                this.retencionesDRField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("TrasladoDR", IsNullable = false)]
        public PagosPagoDoctoRelacionadoImpuestosDRTrasladoDR[] TrasladosDR
        {
            get
            {
                return this.trasladosDRField;
            }
            set
            {
                this.trasladosDRField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/Pagos20")]
    public partial class PagosPagoDoctoRelacionadoImpuestosDRRetencionDR
    {

        private string baseDRField;

        private string impuestoDRField;

        private string tipoFactorDRField;

        private string tasaOCuotaDRField;

        private string importeDRField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string BaseDR
        {
            get
            {
                return this.baseDRField;
            }
            set
            {
                this.baseDRField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ImpuestoDR
        {
            get
            {
                return this.impuestoDRField;
            }
            set
            {
                this.impuestoDRField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TipoFactorDR
        {
            get
            {
                return this.tipoFactorDRField;
            }
            set
            {
                this.tipoFactorDRField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TasaOCuotaDR
        {
            get
            {
                return this.tasaOCuotaDRField;
            }
            set
            {
                this.tasaOCuotaDRField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ImporteDR
        {
            get
            {
                return this.importeDRField;
            }
            set
            {
                this.importeDRField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.sat.gob.mx/sitio_internet/cfd/catalogos")]
    public enum c_Impuesto
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("001")]
        Item001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("002")]
        Item002,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("003")]
        Item003,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.sat.gob.mx/sitio_internet/cfd/catalogos")]
    public enum c_TipoFactor
    {

        /// <remarks/>
        Tasa,

        /// <remarks/>
        Cuota,

        /// <remarks/>
        Exento,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/Pagos20")]
    public partial class PagosPagoDoctoRelacionadoImpuestosDRTrasladoDR
    {

        private string baseDRField;

        private string impuestoDRField;

        private string tipoFactorDRField;

        private string tasaOCuotaDRField;

        private bool tasaOCuotaDRFieldSpecified;

        private string importeDRField;

        private bool importeDRFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string BaseDR
        {
            get
            {
                return this.baseDRField;
            }
            set
            {
                this.baseDRField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ImpuestoDR
        {
            get
            {
                return this.impuestoDRField;
            }
            set
            {
                this.impuestoDRField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TipoFactorDR
        {
            get
            {
                return this.tipoFactorDRField;
            }
            set
            {
                this.tipoFactorDRField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TasaOCuotaDR
        {
            get
            {
                return this.tasaOCuotaDRField;
            }
            set
            {
                this.tasaOCuotaDRField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TasaOCuotaDRSpecified
        {
            get
            {
                return this.tasaOCuotaDRFieldSpecified;
            }
            set
            {
                this.tasaOCuotaDRFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ImporteDR
        {
            get
            {
                return this.importeDRField;
            }
            set
            {
                this.importeDRField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ImporteDRSpecified
        {
            get
            {
                return this.importeDRFieldSpecified;
            }
            set
            {
                this.importeDRFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.sat.gob.mx/sitio_internet/cfd/catalogos")]
    public enum c_Moneda
    {

        /// <remarks/>
        AED,

        /// <remarks/>
        AFN,

        /// <remarks/>
        ALL,

        /// <remarks/>
        AMD,

        /// <remarks/>
        ANG,

        /// <remarks/>
        AOA,

        /// <remarks/>
        ARS,

        /// <remarks/>
        AUD,

        /// <remarks/>
        AWG,

        /// <remarks/>
        AZN,

        /// <remarks/>
        BAM,

        /// <remarks/>
        BBD,

        /// <remarks/>
        BDT,

        /// <remarks/>
        BGN,

        /// <remarks/>
        BHD,

        /// <remarks/>
        BIF,

        /// <remarks/>
        BMD,

        /// <remarks/>
        BND,

        /// <remarks/>
        BOB,

        /// <remarks/>
        BOV,

        /// <remarks/>
        BRL,

        /// <remarks/>
        BSD,

        /// <remarks/>
        BTN,

        /// <remarks/>
        BWP,

        /// <remarks/>
        BYR,

        /// <remarks/>
        BZD,

        /// <remarks/>
        CAD,

        /// <remarks/>
        CDF,

        /// <remarks/>
        CHE,

        /// <remarks/>
        CHF,

        /// <remarks/>
        CHW,

        /// <remarks/>
        CLF,

        /// <remarks/>
        CLP,

        /// <remarks/>
        CNY,

        /// <remarks/>
        COP,

        /// <remarks/>
        COU,

        /// <remarks/>
        CRC,

        /// <remarks/>
        CUC,

        /// <remarks/>
        CUP,

        /// <remarks/>
        CVE,

        /// <remarks/>
        CZK,

        /// <remarks/>
        DJF,

        /// <remarks/>
        DKK,

        /// <remarks/>
        DOP,

        /// <remarks/>
        DZD,

        /// <remarks/>
        EGP,

        /// <remarks/>
        ERN,

        /// <remarks/>
        ETB,

        /// <remarks/>
        EUR,

        /// <remarks/>
        FJD,

        /// <remarks/>
        FKP,

        /// <remarks/>
        GBP,

        /// <remarks/>
        GEL,

        /// <remarks/>
        GHS,

        /// <remarks/>
        GIP,

        /// <remarks/>
        GMD,

        /// <remarks/>
        GNF,

        /// <remarks/>
        GTQ,

        /// <remarks/>
        GYD,

        /// <remarks/>
        HKD,

        /// <remarks/>
        HNL,

        /// <remarks/>
        HRK,

        /// <remarks/>
        HTG,

        /// <remarks/>
        HUF,

        /// <remarks/>
        IDR,

        /// <remarks/>
        ILS,

        /// <remarks/>
        INR,

        /// <remarks/>
        IQD,

        /// <remarks/>
        IRR,

        /// <remarks/>
        ISK,

        /// <remarks/>
        JMD,

        /// <remarks/>
        JOD,

        /// <remarks/>
        JPY,

        /// <remarks/>
        KES,

        /// <remarks/>
        KGS,

        /// <remarks/>
        KHR,

        /// <remarks/>
        KMF,

        /// <remarks/>
        KPW,

        /// <remarks/>
        KRW,

        /// <remarks/>
        KWD,

        /// <remarks/>
        KYD,

        /// <remarks/>
        KZT,

        /// <remarks/>
        LAK,

        /// <remarks/>
        LBP,

        /// <remarks/>
        LKR,

        /// <remarks/>
        LRD,

        /// <remarks/>
        LSL,

        /// <remarks/>
        LYD,

        /// <remarks/>
        MAD,

        /// <remarks/>
        MDL,

        /// <remarks/>
        MGA,

        /// <remarks/>
        MKD,

        /// <remarks/>
        MMK,

        /// <remarks/>
        MNT,

        /// <remarks/>
        MOP,

        /// <remarks/>
        MRO,

        /// <remarks/>
        MUR,

        /// <remarks/>
        MVR,

        /// <remarks/>
        MWK,

        /// <remarks/>
        MXN,

        /// <remarks/>
        MXV,

        /// <remarks/>
        MYR,

        /// <remarks/>
        MZN,

        /// <remarks/>
        NAD,

        /// <remarks/>
        NGN,

        /// <remarks/>
        NIO,

        /// <remarks/>
        NOK,

        /// <remarks/>
        NPR,

        /// <remarks/>
        NZD,

        /// <remarks/>
        OMR,

        /// <remarks/>
        PAB,

        /// <remarks/>
        PEN,

        /// <remarks/>
        PGK,

        /// <remarks/>
        PHP,

        /// <remarks/>
        PKR,

        /// <remarks/>
        PLN,

        /// <remarks/>
        PYG,

        /// <remarks/>
        QAR,

        /// <remarks/>
        RON,

        /// <remarks/>
        RSD,

        /// <remarks/>
        RUB,

        /// <remarks/>
        RWF,

        /// <remarks/>
        SAR,

        /// <remarks/>
        SBD,

        /// <remarks/>
        SCR,

        /// <remarks/>
        SDG,

        /// <remarks/>
        SEK,

        /// <remarks/>
        SGD,

        /// <remarks/>
        SHP,

        /// <remarks/>
        SLL,

        /// <remarks/>
        SOS,

        /// <remarks/>
        SRD,

        /// <remarks/>
        SSP,

        /// <remarks/>
        STD,

        /// <remarks/>
        SVC,

        /// <remarks/>
        SYP,

        /// <remarks/>
        SZL,

        /// <remarks/>
        THB,

        /// <remarks/>
        TJS,

        /// <remarks/>
        TMT,

        /// <remarks/>
        TND,

        /// <remarks/>
        TOP,

        /// <remarks/>
        TRY,

        /// <remarks/>
        TTD,

        /// <remarks/>
        TWD,

        /// <remarks/>
        TZS,

        /// <remarks/>
        UAH,

        /// <remarks/>
        UGX,

        /// <remarks/>
        USD,

        /// <remarks/>
        USN,

        /// <remarks/>
        UYI,

        /// <remarks/>
        UYU,

        /// <remarks/>
        UZS,

        /// <remarks/>
        VEF,

        /// <remarks/>
        VND,

        /// <remarks/>
        VUV,

        /// <remarks/>
        WST,

        /// <remarks/>
        XAF,

        /// <remarks/>
        XAG,

        /// <remarks/>
        XAU,

        /// <remarks/>
        XBA,

        /// <remarks/>
        XBB,

        /// <remarks/>
        XBC,

        /// <remarks/>
        XBD,

        /// <remarks/>
        XCD,

        /// <remarks/>
        XDR,

        /// <remarks/>
        XOF,

        /// <remarks/>
        XPD,

        /// <remarks/>
        XPF,

        /// <remarks/>
        XPT,

        /// <remarks/>
        XSU,

        /// <remarks/>
        XTS,

        /// <remarks/>
        XUA,

        /// <remarks/>
        XXX,

        /// <remarks/>
        YER,

        /// <remarks/>
        ZAR,

        /// <remarks/>
        ZMW,

        /// <remarks/>
        ZWL,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.sat.gob.mx/sitio_internet/cfd/catalogos")]
    public enum c_ObjetoImp
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01")]
        Item01,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("02")]
        Item02,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("03")]
        Item03,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/Pagos20")]
    public partial class PagosPagoImpuestosP
    {

        private PagosPagoImpuestosPRetencionP[] retencionesPField;

        private PagosPagoImpuestosPTrasladoP[] trasladosPField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("RetencionP", IsNullable = false)]
        public PagosPagoImpuestosPRetencionP[] RetencionesP
        {
            get
            {
                return this.retencionesPField;
            }
            set
            {
                this.retencionesPField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("TrasladoP", IsNullable = false)]
        public PagosPagoImpuestosPTrasladoP[] TrasladosP
        {
            get
            {
                return this.trasladosPField;
            }
            set
            {
                this.trasladosPField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/Pagos20")]
    public partial class PagosPagoImpuestosPRetencionP
    {

        private string impuestoPField;

        private decimal importePField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ImpuestoP
        {
            get
            {
                return this.impuestoPField;
            }
            set
            {
                this.impuestoPField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal ImporteP
        {
            get
            {
                return this.importePField;
            }
            set
            {
                this.importePField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/Pagos20")]
    public partial class PagosPagoImpuestosPTrasladoP
    {

        private string basePField;

        private string impuestoPField;

        private string tipoFactorPField;

        private string tasaOCuotaPField;

        private bool tasaOCuotaPFieldSpecified;

        private string importePField;

        private bool importePFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string BaseP
        {
            get
            {
                return this.basePField;
            }
            set
            {
                this.basePField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ImpuestoP
        {
            get
            {
                return this.impuestoPField;
            }
            set
            {
                this.impuestoPField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TipoFactorP
        {
            get
            {
                return this.tipoFactorPField;
            }
            set
            {
                this.tipoFactorPField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TasaOCuotaP
        {
            get
            {
                return this.tasaOCuotaPField;
            }
            set
            {
                this.tasaOCuotaPField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TasaOCuotaPSpecified
        {
            get
            {
                return this.tasaOCuotaPFieldSpecified;
            }
            set
            {
                this.tasaOCuotaPFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ImporteP
        {
            get
            {
                return this.importePField;
            }
            set
            {
                this.importePField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ImportePSpecified
        {
            get
            {
                return this.importePFieldSpecified;
            }
            set
            {
                this.importePFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.sat.gob.mx/sitio_internet/cfd/catalogos")]
    public enum c_FormaPago
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01")]
        Item01,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("02")]
        Item02,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("03")]
        Item03,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04")]
        Item04,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("05")]
        Item05,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("06")]
        Item06,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("08")]
        Item08,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12")]
        Item12,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("13")]
        Item13,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("14")]
        Item14,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15")]
        Item15,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17")]
        Item17,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("23")]
        Item23,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("24")]
        Item24,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("25")]
        Item25,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("26")]
        Item26,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("27")]
        Item27,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("28")]
        Item28,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("29")]
        Item29,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("30")]
        Item30,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("31")]
        Item31,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("99")]
        Item99,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.sat.gob.mx/sitio_internet/cfd/catalogos/Pagos")]
    public enum c_TipoCadenaPago
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01")]
        Item01,
    }
}
