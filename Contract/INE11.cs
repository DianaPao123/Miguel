namespace Contract.Complemento
{

    using System.Xml.Serialization;

    // 
    // Este código fuente fue generado automáticamente por xsd, Versión=4.0.30319.1.
    // 


    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/ine")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.sat.gob.mx/ine", IsNullable = false)]
    public partial class INE
    {

        private INEEntidad[] entidadField;

        private string versionField;

        //private INETipoProceso tipoProcesoField;
        private string tipoProcesoField;

       // private INETipoComite tipoComiteField;
        private string tipoComiteField;

        private bool tipoComiteFieldSpecified;

        private int idContabilidadField;

        private bool idContabilidadFieldSpecified;

        public INE()
        {
            this.versionField = "1.1";
        }

        /// <comentarios/>
        [XmlElement("Entidad")]
        //   [System.Xml.Serialization.XmlArrayItemAttribute("Entidad", IsNullable = false)]

        public INEEntidad[] Entidad
        {
            get
            {
                return this.entidadField;
            }
            set
            {
                this.entidadField = value;
            }
        }

        /// <comentarios/>
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

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
      //  public INETipoProceso TipoProceso
        public string TipoProceso
        {
            get
            {
                return this.tipoProcesoField;
            }
            set
            {
                this.tipoProcesoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
      //  public INETipoComite TipoComite
        public string TipoComite
        {
            get
            {
                return this.tipoComiteField;
            }
            set
            {
                this.tipoComiteField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TipoComiteSpecified
        {
            get
            {
                return this.tipoComiteFieldSpecified;
            }
            set
            {
                this.tipoComiteFieldSpecified = value;
            }
        }

        /// <comentarios/>

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int IdContabilidad
        {
            get
            {
                return this.idContabilidadField;
            }
            set
            {
                this.idContabilidadField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IdContabilidadSpecified
        {
            get
            {
                return this.idContabilidadFieldSpecified;
            }
            set
            {
                this.idContabilidadFieldSpecified = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/ine")]
    public partial class INEEntidad
    {

        private INEEntidadContabilidad[] contabilidadField;

       // private t_ClaveEntidad claveEntidadField;
        private string claveEntidadField;

       // private INEEntidadAmbito ambitoField;
        private string ambitoField;

        private bool ambitoFieldSpecified;

        /// <comentarios/>
        // [System.Xml.Serialization.XmlElementAttribute("Contabilidad")]
        [XmlElement("Contabilidad")]

        public INEEntidadContabilidad[] Contabilidad
        {
            get
            {
                return this.contabilidadField;
            }
            set
            {
                this.contabilidadField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        //public t_ClaveEntidad ClaveEntidad
        public string ClaveEntidad
        {
            get
            {
                return this.claveEntidadField;
            }
            set
            {
                this.claveEntidadField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
       // public INEEntidadAmbito Ambito
        public string Ambito
        {
            get
            {
                return this.ambitoField;
            }
            set
            {
                this.ambitoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AmbitoSpecified
        {
            get
            {
                return this.ambitoFieldSpecified;
            }
            set
            {
                this.ambitoFieldSpecified = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/ine")]
    public partial class INEEntidadContabilidad
    {

        private int idContabilidadField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int IdContabilidad
        {
            get
            {
                return this.idContabilidadField;
            }
            set
            {
                this.idContabilidadField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.sat.gob.mx/ine")]
    public enum t_ClaveEntidad
    {

        /// <comentarios/>
        AGU,

        /// <comentarios/>
        BCN,

        /// <comentarios/>
        BCS,

        /// <comentarios/>
        CAM,

        /// <comentarios/>
        CHP,

        /// <comentarios/>
        CHH,

        /// <comentarios/>
        COA,

        /// <comentarios/>
        COL,

        /// <comentarios/>
        DIF,

        /// <comentarios/>
        DUR,

        /// <comentarios/>
        GUA,

        /// <comentarios/>
        GRO,

        /// <comentarios/>
        HID,

        /// <comentarios/>
        JAL,

        /// <comentarios/>
        MEX,

        /// <comentarios/>
        MIC,

        /// <comentarios/>
        MOR,

        /// <comentarios/>
        NAY,

        /// <comentarios/>
        NLE,

        /// <comentarios/>
        OAX,

        /// <comentarios/>
        PUE,

        /// <comentarios/>
        QTO,

        /// <comentarios/>
        ROO,

        /// <comentarios/>
        SLP,

        /// <comentarios/>
        SIN,

        /// <comentarios/>
        SON,

        /// <comentarios/>
        TAB,

        /// <comentarios/>
        TAM,

        /// <comentarios/>
        TLA,

        /// <comentarios/>
        VER,

        /// <comentarios/>
        YUC,

        /// <comentarios/>
        ZAC,
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.sat.gob.mx/ine")]
    public enum INEEntidadAmbito
    {

        /// <comentarios/>
        Local,

        /// <comentarios/>
        Federal,
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.sat.gob.mx/ine")]
    public enum INETipoProceso
    {

        /// <comentarios/>
        Ordinario,

        /// <comentarios/>
        Precampaña,

        /// <comentarios/>
        Campaña,
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.sat.gob.mx/ine")]
    public enum INETipoComite
    {

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("Ejecutivo Nacional")]
        EjecutivoNacional,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("Ejecutivo Estatal")]
        EjecutivoEstatal,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("Directivo Estatal")]
        DirectivoEstatal,
    }
}