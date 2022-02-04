

using System.Xml.Serialization;

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public struct request 
{
    
    private string userPassField;
    
    private string userIDField;
    
    private string emisorRFCField;
    
    private string text2CFDIField;
    
    private bool generarTXTField;
    
    private bool generarPDFField;
    
    private bool generarCBBField;
    
    /// <remarks/>
    public string UserPass {
        get {
            return this.userPassField;
        }
        set {
            this.userPassField = value;
        }
    }
    
    /// <remarks/>
    public string UserID {
        get {
            return this.userIDField;
        }
        set {
            this.userIDField = value;
        }
    }
    
    /// <remarks/>
    public string emisorRFC {
        get {
            return this.emisorRFCField;
        }
        set {
            this.emisorRFCField = value;
        }
    }
    
    /// <remarks/>
    public string text2CFDI {
        get {
            return this.text2CFDIField;
        }
        set {
            this.text2CFDIField = value;
        }
    }
    
    /// <remarks/>
    public bool generarTXT {
        get {
            return this.generarTXTField;
        }
        set {
            this.generarTXTField = value;
        }
    }
    
    /// <remarks/>
    public bool generarPDF {
        get {
            return this.generarPDFField;
        }
        set {
            this.generarPDFField = value;
        }
    }
    
    /// <remarks/>
    public bool generarCBB {
        get {
            return this.generarCBBField;
        }
        set {
            this.generarCBBField = value;
        }
    }
}
