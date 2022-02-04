using System.IO;
using System.Text;

namespace Business
{
    public class SidetecStringWriter : StringWriter
    {
        private Encoding _encoding;

        public override Encoding Encoding
        {
            get { return _encoding; }
        }

        public SidetecStringWriter(Encoding encoding)
        {
            this._encoding = encoding;
        }


    }


}

    