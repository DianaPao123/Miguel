using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Contract
{
    [Serializable()]

    public class request
    {
        [DataMemberAttribute]
        public string UserPass { get; set; }
        [DataMemberAttribute]
        public string UserID { get; set; }
        [DataMemberAttribute]
        public string emisorRFC { get; set; }
        [DataMemberAttribute]
        public string text2CFDI { get; set; }
        [DataMemberAttribute]
        public bool generarTXT { get; set; }
        [DataMemberAttribute]
        public bool generarPDF { get; set; }
        [DataMemberAttribute]
        public bool generarCBB { get; set; }


        //public request(string UserPassx, string UserIDx, string emisorRFCx, string text2CFDIx, bool generarTXTx,bool generarPDFx,bool generarCBBx )
        //{
        //    UserPass = UserPassx;
        //    UserID=UserIDx;
        //    emisorRFC = emisorRFCx;
        //    text2CFDI = text2CFDIx;
        //    generarTXT = generarTXTx;
        //    generarPDF = generarPDFx;
        //    generarCBB = generarCBBx;
        //}
        //public request() { }

    }
}
