using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Business
{
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.sat.gob.mx/TimbreFiscalDigital")]
    [XmlRoot(Namespace = "http://www.sat.gob.mx/TimbreFiscalDigital", IsNullable = false)]
    public class TimbreFiscalDigital 
    {

        [XmlAttribute("schemaLocation", Namespace = XmlSchema.InstanceNamespace)]
        public string xsiSchemaLocation = "http://www.sat.gob.mx/TimbreFiscalDigital http://www.sat.gob.mx/sitio_internet/cfd/TimbreFiscalDigital/TimbreFiscalDigitalv11.xsd";

        private string versionField;
        private string uUIDField;
        //---nuevos----
        private string rfcProvCertifField;

        private string leyendaField;
        //-----------------
        private System.DateTime fechaTimbradoField;
        private string selloCFDField;
        private string noCertificadoSATField;
        private string selloSATField;

        [XmlIgnore]
        public string cadenaOriginal { get; set; }
        public TimbreFiscalDigital()
        {
            this.versionField = "1.1";
        }

        [XmlAttribute()]
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

        /// <remarks/>
        [XmlAttribute()]
        public string UUID
        {
            get
            {
                return this.uUIDField;
            }
            set
            {
                this.uUIDField = value;
                
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public System.DateTime FechaTimbrado
        {
            get
            {
                return this.fechaTimbradoField;
            }
            set
            {
                this.fechaTimbradoField = value;
                
            }
        }
        [XmlAttribute()]
       
        public string RfcProvCertif
        {
            get
            {
                return this.rfcProvCertifField;
            }
            set
            {
                this.rfcProvCertifField = value;
            }
        }
         [XmlAttribute()]
       
        public string Leyenda
        {
            get
            {
                return this.leyendaField;
            }
            set
            {
                this.leyendaField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string SelloCFD
        {
            get
            {
                return this.selloCFDField;
            }
            set
            {
                this.selloCFDField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string NoCertificadoSAT
        {
            get
            {
                return this.noCertificadoSATField;
            }
            set
            {
                this.noCertificadoSATField = value;

            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string SelloSAT
        {
            get
            {
                return this.selloSATField;
            }
            set
            {
                this.selloSATField = value;
                
            }
        }

    }
}
