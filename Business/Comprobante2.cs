﻿
using System;
using System.Collections.Generic;
using System.Xml.Schema;
using System.Xml.Serialization;


namespace GeneradorCfdi.CFDI32
{ // 

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.sat.gob.mx/cfd/3", IsNullable = false)]
    public partial class Comprobante
    {

        
        public override string ToString()
        {
            if (this.Complemento != null && this.Complemento.timbreFiscalDigital != null)
            {
                return this.serie + "|" + 
                    this.folio + "|" + 
                       this.Complemento.timbreFiscalDigital.UUID + "|" +
                       this.noCertificado + "|" +
                       this.Complemento.timbreFiscalDigital.NoCertificadoSAT + "|" +
                       this.fecha.ToString("s") + "|" +
                       this.Complemento.timbreFiscalDigital.FechaTimbrado.ToString("s") + "|" +
                       this.sello +
                       this.Complemento.timbreFiscalDigital.SelloSAT + "|" + "\r\n" +
                       this.CadenaOriginalTimbre;
            }
            return "";
        }
      
        
        [XmlIgnore]
        public decimal Anticipo { get; set; }
        [XmlIgnore]
        public string PorcentajeAnticipo { get; set; }
        [XmlIgnore]
        public string LeyendaAnticipo { get; set; }
        
        
        [XmlIgnore]
        public string venta{ get; set; }
        [XmlIgnore]
        public string cliente{ get; set; }
        [XmlIgnore]
        public string sucursal{ get; set; }
        [XmlIgnore]
        public string vendedor{ get; set; }
        [XmlIgnore]
        public string nota1{ get; set; }
        [XmlIgnore]
        public string nota2{ get; set; }
        [XmlIgnore]
        public string nota3{ get; set; }
        [XmlIgnore]
        public string fpago{ get; set; }



        [XmlIgnore]
        public string VoBoTitulo { get; set; }
        [XmlIgnore]
        public string RecibiTitulo { get; set; }
        [XmlIgnore]
        public string AutorizoTitulo { get; set; }
        [XmlIgnore]
        public string AgregadoTitulo { get; set; }

        [XmlIgnore]
        public string AgregadoNombre { get; set; }
        [XmlIgnore]
        public string AgregadoPuesto { get; set; }
        [XmlIgnore]
        public string AgregadoArea { get; set; }

        [XmlIgnore]
        public string VoBoNombre { get; set; }
        [XmlIgnore]
        public string VoBoPuesto { get; set; }
        [XmlIgnore]
        public string VoBoArea { get; set; }

        [XmlIgnore]
        public string RecibiNombre { get; set; }
        [XmlIgnore]
        public string RecibiPuesto { get; set; }
        [XmlIgnore]
        public string RecibiArea { get; set; }

        [XmlIgnore]
        public string AutorizoNombre { get; set; }
        [XmlIgnore]
        public string AutorizoPuesto { get; set; }
        [XmlIgnore]
        public string AutorizoArea { get; set; }

        [XmlIgnore]
        public string DonatLeyenda { get; set; }
        [XmlIgnore]
        public string DonatAprobacion { get; set; }
        [XmlIgnore]
        public string DonatFecha { get; set; }
        [XmlIgnore]
        public string TituloOtros { get; set; }

        [XmlIgnore]
        public string Titulo { get; set; }

        [XmlIgnore]
        public decimal RetencionIsr { get; set; }

        [XmlIgnore]
        public decimal RetencionIva { get; set; }

        [XmlIgnore]
        public string CURPEmisor { get; set; }

        [XmlIgnore]
        public DateTime? FechaPago { get; set; }

        [XmlIgnore]
        public decimal TrasladoIeps { get; set; }

        [XmlIgnore]
        public decimal TrasladoIva { get; set; }
        [XmlIgnore]
        public decimal TasaTrasladoIeps { get; set; }

        [XmlIgnore]
        public decimal TasaTrasladoIva { get; set; }


        [XmlIgnore]
        public List<ComprobanteConcepto> ConceptosAduana { get; set; }

        [XmlIgnore]
        public string Leyenda { get; set; }

        [XmlIgnore]
        public string Regimen { get; set; }

        [XmlIgnore]
        public string LeyendaSuperior { get; set; }

        [XmlIgnore]
        public string LeyendaInferior { get; set; }

        [XmlIgnore]
        public string XmlString { get; set; }

        [XmlIgnore]
        public string CadenaOriginal { get; set; }
        [XmlIgnore]
        public string CadenaOriginalTimbre { get; set; }

        [XmlIgnore]
        public string CantidadLetra { get; set; }

        [XmlIgnore]
        public string Proyecto { get; set; }

        // Layout Andamios -- Sergio Zavala
        [XmlIgnore]
        public string LeyendaRentaAndamios { get; set; }

        [XmlIgnore]
        public string DomicilioObraAndamios { get; set; }

        [XmlIgnore]
        public string IdClienteAndamios { get; set; }
        
       
        [XmlIgnore]
        public string TelefonoEmisor { get; set; }
        
        [XmlIgnore]
        public string TelefonoReceptor { get; set; }
        // Fin Layout Andamios -- Sergio Zavala
        

        [XmlIgnore]
        public ComprobanteImpuestosTraslado[] Traslados { get; set; }

      
        [XmlIgnore]
        public string DbfNombreVendedor { get; set; }
        // Fin FTPCanadian -- SZ

        // Campo para la cadena con el xml de la adenda
        [XmlIgnore]
        public string XmlAdenda{ get; set; }

        //Campo para el XML con el complemento
        [XmlIgnore]
        public string XmlComplemento { get; set; }


        private ComprobanteEmisor emisorField;

        private ComprobanteReceptor receptorField;

        private ComprobanteConcepto[] conceptosField;

        
        private ComprobanteImpuestos impuestosField;

        private ComprobanteComplemento complementoField;

        private ComprobanteAddenda addendaField;

        private string versionField;

        private string serieField;

        private string folioField;

        private System.DateTime fechaField;

        private string selloField;

        private string formaDePagoField;

        private string noCertificadoField;

        private string certificadoField;

        private string condicionesDePagoField;

        private decimal subTotalField;
        
        private decimal descuentoField;

        private bool descuentoFieldSpecified;

        private string motivoDescuentoField;

        private string tipoCambioField;

        private string monedaField;

        private decimal totalField;

        private ComprobanteTipoDeComprobante tipoDeComprobanteField;

        private string metodoDePagoField;

        private string lugarExpedicionField;

        private string numCtaPagoField;

        private string folioFiscalOrigField;

        private string serieFolioFiscalOrigField;

        private System.DateTime fechaFolioFiscalOrigField;

        private bool fechaFolioFiscalOrigFieldSpecified;

        private decimal montoFolioFiscalOrigField;

        private bool montoFolioFiscalOrigFieldSpecified;


        [XmlAttribute("schemaLocation", Namespace = XmlSchema.InstanceNamespace)] 
        public string xsiSchemaLocation = "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd";


        public Comprobante()
        {
            
        }

        /// <remarks/>
        public ComprobanteEmisor Emisor
        {
            get { return this.emisorField; }
            set { this.emisorField = value; }
        }

        /// <remarks/>
        public ComprobanteReceptor Receptor
        {
            get { return this.receptorField; }
            set { this.receptorField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Concepto", IsNullable = false)]
        public ComprobanteConcepto[] Conceptos
        {
            get { return this.conceptosField; }
            set { this.conceptosField = value; }
        }

        /// <remarks/>
        public ComprobanteImpuestos Impuestos
        {
            get { return this.impuestosField; }
            set { this.impuestosField = value; }
        }

        /// <remarks/>
        public ComprobanteComplemento Complemento
        {
            get { return this.complementoField; }
            set { this.complementoField = value; }
        }

        /// <remarks/>
        public ComprobanteAddenda Addenda
        {
            get { return this.addendaField; }
            set { this.addendaField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string version
        {
            get { return this.versionField; }
            set { this.versionField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string serie
        {
            get { return this.serieField; }
            set { this.serieField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string folio
        {
            get { return this.folioField; }
            set { this.folioField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime fecha
        {
            get { return this.fechaField; }
            set { this.fechaField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string sello
        {
            get { return this.selloField; }
            set { this.selloField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string formaDePago
        {
            get { return this.formaDePagoField; }
            set { this.formaDePagoField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string noCertificado
        {
            get { return this.noCertificadoField; }
            set { this.noCertificadoField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string certificado
        {
            get { return this.certificadoField; }
            set { this.certificadoField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string condicionesDePago
        {
            get { return this.condicionesDePagoField; }
            set { this.condicionesDePagoField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal subTotal
        {
            get { return this.subTotalField; }
            set { this.subTotalField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal descuento
        {
            get { return this.descuentoField; }
            set { this.descuentoField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool descuentoSpecified
        {
            get { return this.descuentoFieldSpecified; }
            set { this.descuentoFieldSpecified = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string motivoDescuento
        {
            get { return this.motivoDescuentoField; }
            set { this.motivoDescuentoField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TipoCambio
        {
            get { return this.tipoCambioField; }
            set { this.tipoCambioField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Moneda
        {
            get { return this.monedaField; }
            set { this.monedaField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal total
        {
            get { return this.totalField; }
            set { this.totalField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ComprobanteTipoDeComprobante tipoDeComprobante
        {
            get { return this.tipoDeComprobanteField; }
            set { this.tipoDeComprobanteField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string metodoDePago
        {
            get { return this.metodoDePagoField; }
            set { this.metodoDePagoField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LugarExpedicion
        {
            get { return this.lugarExpedicionField; }
            set { this.lugarExpedicionField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NumCtaPago
        {
            get { return this.numCtaPagoField; }
            set { this.numCtaPagoField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string FolioFiscalOrig
        {
            get { return this.folioFiscalOrigField; }
            set { this.folioFiscalOrigField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SerieFolioFiscalOrig
        {
            get { return this.serieFolioFiscalOrigField; }
            set { this.serieFolioFiscalOrigField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime FechaFolioFiscalOrig
        {
            get { return this.fechaFolioFiscalOrigField; }
            set { this.fechaFolioFiscalOrigField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool FechaFolioFiscalOrigSpecified
        {
            get { return this.fechaFolioFiscalOrigFieldSpecified; }
            set { this.fechaFolioFiscalOrigFieldSpecified = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal MontoFolioFiscalOrig
        {
            get { return this.montoFolioFiscalOrigField; }
            set { this.montoFolioFiscalOrigField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MontoFolioFiscalOrigSpecified
        {
            get { return this.montoFolioFiscalOrigFieldSpecified; }
            set { this.montoFolioFiscalOrigFieldSpecified = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class ComprobanteEmisor
    {

        
        private t_UbicacionFiscal domicilioFiscalField;

        private t_Ubicacion expedidoEnField;

        private ComprobanteEmisorRegimenFiscal[] regimenFiscalField;

        private string rfcField;

        private string nombreField;
       //  [XmlIgnore]
       // private string CURPField;

        /// <remarks/>
        public t_UbicacionFiscal DomicilioFiscal
        {
            get { return this.domicilioFiscalField; }
            set { this.domicilioFiscalField = value; }
        }

        /// <remarks/>
        public t_Ubicacion ExpedidoEn
        {
            get { return this.expedidoEnField; }
            set { this.expedidoEnField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RegimenFiscal")]
        public ComprobanteEmisorRegimenFiscal[] RegimenFiscal
        {
            get { return this.regimenFiscalField; }
            set { this.regimenFiscalField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string rfc
        {
            get { return this.rfcField; }
            set { this.rfcField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nombre
        {
            get { return this.nombreField; }
            set { this.nombreField = value; }
        }
        
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class t_UbicacionFiscal
    {

        private string calleField;

        private string noExteriorField;

        private string noInteriorField;

        private string coloniaField;

        private string localidadField;

        private string referenciaField;

        private string municipioField;

        private string estadoField;

        private string paisField;

        private string codigoPostalField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string calle
        {
            get { return this.calleField; }
            set { this.calleField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string noExterior
        {
            get { return this.noExteriorField; }
            set { this.noExteriorField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string noInterior
        {
            get { return this.noInteriorField; }
            set { this.noInteriorField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string colonia
        {
            get { return this.coloniaField; }
            set { this.coloniaField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string localidad
        {
            get { return this.localidadField; }
            set { this.localidadField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string referencia
        {
            get { return this.referenciaField; }
            set { this.referenciaField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string municipio
        {
            get { return this.municipioField; }
            set { this.municipioField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string estado
        {
            get { return this.estadoField; }
            set { this.estadoField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string pais
        {
            get { return this.paisField; }
            set { this.paisField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string codigoPostal
        {
            get { return this.codigoPostalField; }
            set { this.codigoPostalField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class t_InformacionAduanera
    {

        private string numeroField;

        private System.DateTime fechaField;

        private string aduanaField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string numero
        {
            get { return this.numeroField; }
            set { this.numeroField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
        public System.DateTime fecha
        {
            get { return this.fechaField; }
            set { this.fechaField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string aduana
        {
            get { return this.aduanaField; }
            set { this.aduanaField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class t_Ubicacion
    {

        private string calleField;

        private string noExteriorField;

        private string noInteriorField;

        private string coloniaField;

        private string localidadField;

        private string referenciaField;

        private string municipioField;

        private string estadoField;

        private string paisField;

        private string codigoPostalField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string calle
        {
            get { return this.calleField; }
            set { this.calleField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string noExterior
        {
            get { return this.noExteriorField; }
            set { this.noExteriorField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string noInterior
        {
            get { return this.noInteriorField; }
            set { this.noInteriorField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string colonia
        {
            get { return this.coloniaField; }
            set { this.coloniaField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string localidad
        {
            get { return this.localidadField; }
            set { this.localidadField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string referencia
        {
            get { return this.referenciaField; }
            set { this.referenciaField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string municipio
        {
            get { return this.municipioField; }
            set { this.municipioField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string estado
        {
            get { return this.estadoField; }
            set { this.estadoField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string pais
        {
            get { return this.paisField; }
            set { this.paisField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string codigoPostal
        {
            get { return this.codigoPostalField; }
            set { this.codigoPostalField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class ComprobanteEmisorRegimenFiscal
    {

        private string regimenField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Regimen
        {
            get { return this.regimenField; }
            set { this.regimenField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class ComprobanteReceptor
    {
        //-
        [XmlIgnore]
        public string Bcc { get; set; }

        [XmlIgnore]
        public string Emails { get; set; }

        private t_Ubicacion domicilioField;

        private string rfcField;

        private string nombreField;

        /// <remarks/>
        public t_Ubicacion Domicilio
        {
            get { return this.domicilioField; }
            set { this.domicilioField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string rfc
        {
            get { return this.rfcField; }
            set { this.rfcField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nombre
        {
            get { return this.nombreField; }
            set { this.nombreField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class ComprobanteConcepto
    {
        [XmlIgnore]
        public string Iva { get; set; }
        [XmlIgnore]
        public string PorcentajeIva { get; set; }
        [XmlIgnore]
        public string On { get; set; }

        [XmlIgnore]
        public string Detalles { get; set; }
        [XmlIgnore]
        public string OrdenCompra { get; set; }
        [XmlIgnore]
        public string CuentaPredial { get; set; }

         [XmlIgnore]
        public string InformacionAduanera { get; set; }

        [XmlIgnore]
        public string FechaPedido { get; set;}
        [XmlIgnore]
        public string CajasPiezas { get; set;}

        private object[] itemsField;

        private decimal cantidadField;

        private string unidadField;

        private string noIdentificacionField;

        private string descripcionField;

        private decimal valorUnitarioField;

        private decimal importeField;

        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ComplementoConcepto",
            typeof (ComprobanteConceptoComplementoConcepto))]
        [System.Xml.Serialization.XmlElementAttribute("CuentaPredial", typeof (ComprobanteConceptoCuentaPredial))]
        [System.Xml.Serialization.XmlElementAttribute("InformacionAduanera", typeof (t_InformacionAduanera))]
        [System.Xml.Serialization.XmlElementAttribute("Parte", typeof (ComprobanteConceptoParte))]
        public object[] Items
        {
            get { return this.itemsField; }
            set { this.itemsField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal cantidad
        {
            get { return this.cantidadField; }
            set { this.cantidadField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string unidad
        {
            get { return this.unidadField; }
            set { this.unidadField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string noIdentificacion
        {
            get { return this.noIdentificacionField; }
            set { this.noIdentificacionField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string descripcion
        {
            get { return this.descripcionField; }
            set { this.descripcionField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal valorUnitario
        {
            get { return this.valorUnitarioField; }
            set { this.valorUnitarioField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal importe
        {
            get { return this.importeField; }
            set { this.importeField = value; }
        }

      
    }

   
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class ComprobanteConceptoComplementoConcepto
    {

        private System.Xml.XmlElement[] anyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlElement[] Any
        {
            get { return this.anyField; }
            set { this.anyField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class ComprobanteConceptoCuentaPredial
    {

        private string numeroField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string numero
        {
            get { return this.numeroField; }
            set { this.numeroField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class ComprobanteConceptoParte
    {

        private t_InformacionAduanera[] informacionAduaneraField;

        private decimal cantidadField;

        private string unidadField;

        private string noIdentificacionField;

        private string descripcionField;

        private decimal valorUnitarioField;

        private bool valorUnitarioFieldSpecified;

        private decimal importeField;

        private bool importeFieldSpecified;


        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("InformacionAduanera")]
        public t_InformacionAduanera[] InformacionAduanera
        {
            get { return this.informacionAduaneraField; }
            set { this.informacionAduaneraField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal cantidad
        {
            get { return this.cantidadField; }
            set { this.cantidadField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string unidad
        {
            get { return this.unidadField; }
            set { this.unidadField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string noIdentificacion
        {
            get { return this.noIdentificacionField; }
            set { this.noIdentificacionField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string descripcion
        {
            get { return this.descripcionField; }
            set { this.descripcionField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal valorUnitario
        {
            get { return this.valorUnitarioField; }
            set { this.valorUnitarioField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool valorUnitarioSpecified
        {
            get { return this.valorUnitarioFieldSpecified; }
            set { this.valorUnitarioFieldSpecified = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal importe
        {
            get { return this.importeField; }
            set { this.importeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool importeSpecified
        {
            get { return this.importeFieldSpecified; }
            set { this.importeFieldSpecified = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class ComprobanteImpuestos
    {

        private ComprobanteImpuestosRetencion[] retencionesField;

        private ComprobanteImpuestosTraslado[] trasladosField;

        private decimal totalImpuestosRetenidosField;

        private bool totalImpuestosRetenidosFieldSpecified;

        private decimal totalImpuestosTrasladadosField;

        private bool totalImpuestosTrasladadosFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Retencion", IsNullable = false)]
        public ComprobanteImpuestosRetencion[] Retenciones
        {
            get { return this.retencionesField; }
            set { this.retencionesField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Traslado", IsNullable = false)]
        public ComprobanteImpuestosTraslado[] Traslados
        {
            get { return this.trasladosField; }
            set { this.trasladosField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal totalImpuestosRetenidos
        {
            get { return this.totalImpuestosRetenidosField; }
            set { this.totalImpuestosRetenidosField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool totalImpuestosRetenidosSpecified
        {
            get { return this.totalImpuestosRetenidosFieldSpecified; }
            set { this.totalImpuestosRetenidosFieldSpecified = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal totalImpuestosTrasladados
        {
            get { return this.totalImpuestosTrasladadosField; }
            set { this.totalImpuestosTrasladadosField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool totalImpuestosTrasladadosSpecified
        {
            get { return this.totalImpuestosTrasladadosFieldSpecified; }
            set { this.totalImpuestosTrasladadosFieldSpecified = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class ComprobanteImpuestosRetencion
    {

        private ComprobanteImpuestosRetencionImpuesto impuestoField;

        private decimal importeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ComprobanteImpuestosRetencionImpuesto impuesto
        {
            get { return this.impuestoField; }
            set { this.impuestoField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal importe
        {
            get { return this.importeField; }
            set { this.importeField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum ComprobanteImpuestosRetencionImpuesto
    {

        /// <remarks/>
        ISR,

        /// <remarks/>
        IVA,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class ComprobanteImpuestosTraslado
    {

        private ComprobanteImpuestosTrasladoImpuesto impuestoField;

        private decimal tasaField;

        private decimal importeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ComprobanteImpuestosTrasladoImpuesto impuesto
        {
            get { return this.impuestoField; }
            set { this.impuestoField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal tasa
        {
            get { return this.tasaField; }
            set { this.tasaField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal importe
        {
            get { return this.importeField; }
            set { this.importeField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum ComprobanteImpuestosTrasladoImpuesto
    {

        /// <remarks/>
        IVA,

        /// <remarks/>
        IEPS,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class ComprobanteComplemento
    {
        [XmlIgnore]
        public Business.TimbreFiscalDigital timbreFiscalDigital { get; set; }

       
        private System.Xml.XmlElement[] anyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlElement[] Any
        {
            get { return this.anyField; }
            set { this.anyField = value; }
        }
       
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class ComprobanteAddenda
    {

        private System.Xml.XmlElement[] anyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlElement[] Any
        {
            get { return this.anyField; }
            set { this.anyField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum ComprobanteTipoDeComprobante
    {

        /// <remarks/>
        ingreso,

        /// <remarks/>
        egreso,

        /// <remarks/>
        traslado,
    }
     
}