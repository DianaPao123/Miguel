
using System.Xml.Serialization;

namespace Contract
{


    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/implocal")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.sat.gob.mx/implocal", IsNullable = false)]
    public partial class ImpuestosLocales
    {

        private ImpuestosLocalesRetencionesLocales[] retencionesLocalesField;

        private ImpuestosLocalesTrasladosLocales[] trasladosLocalesField;

        private string versionField;

        private decimal totaldeRetencionesField;

        private decimal totaldeTrasladosField;

        public ImpuestosLocales()
        {
            this.versionField = "1.0";
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("RetencionesLocales")]
        public ImpuestosLocalesRetencionesLocales[] RetencionesLocales
        {
            get
            {
                return this.retencionesLocalesField;
            }
            set
            {
                this.retencionesLocalesField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("TrasladosLocales")]
        public ImpuestosLocalesTrasladosLocales[] TrasladosLocales
        {
            get
            {
                return this.trasladosLocalesField;
            }
            set
            {
                this.trasladosLocalesField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string version
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

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal TotaldeRetenciones
        {
            get
            {
                return this.totaldeRetencionesField;
            }
            set
            {
                this.totaldeRetencionesField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal TotaldeTraslados
        {
            get
            {
                return this.totaldeTrasladosField;
            }
            set
            {
                this.totaldeTrasladosField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/implocal")]
    public partial class ImpuestosLocalesRetencionesLocales
    {

        private string impLocRetenidoField;

        private decimal tasadeRetencionField;

        private decimal importeField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ImpLocRetenido
        {
            get
            {
                return this.impLocRetenidoField;
            }
            set
            {
                this.impLocRetenidoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal TasadeRetencion
        {
            get
            {
                return this.tasadeRetencionField;
            }
            set
            {
                this.tasadeRetencionField = value;
            }
        }

        /// <comentarios/>
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

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/implocal")]
    public partial class ImpuestosLocalesTrasladosLocales
    {

        private string impLocTrasladadoField;

        private decimal tasadeTrasladoField;

        private decimal importeField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ImpLocTrasladado
        {
            get
            {
                return this.impLocTrasladadoField;
            }
            set
            {
                this.impLocTrasladadoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal TasadeTraslado
        {
            get
            {
                return this.tasadeTrasladoField;
            }
            set
            {
                this.tasadeTrasladoField = value;
            }
        }

        /// <comentarios/>
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
}