using System;
using System.Xml;
using System.Xml.Xsl;
using System.IO;

namespace Business
{
    class GeneradorCadenasTimbre
    {
        private XmlTextReader xsltReader;
        private string xsl;
        private StringReader xsltInput;
        private XslCompiledTransform xsltTransform = new XslCompiledTransform();


        public GeneradorCadenasTimbre(bool timbre)
        {
            if (xsl == null)
            {
                var cwd = Environment.CurrentDirectory;
                try
                {
                    xsl = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Xslt\cadenaoriginal_TFD_1_1.xslt"));
                    Environment.CurrentDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Xslt\");
                    xsltInput = new StringReader(xsl);
                    xsltReader = new XmlTextReader(xsltInput);
                    xsltTransform.Load(xsltReader);
                }
                catch (Exception exception)
                {
                    throw;
                }
                finally
                {
                    Environment.CurrentDirectory = cwd;
                }
            }
        }


        /// <summary>
        /// Generador de cadenas originales de timbre
        /// </summary>
        public GeneradorCadenasTimbre()
        {
            if (xsl == null)
            {
                var cwd = Environment.CurrentDirectory;
                try
                {
                    xsl = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\cadenaoriginal_TFD_1_1.xslt"));
                    Environment.CurrentDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory , @"Resources\");
                    xsltInput = new StringReader(xsl);
                    xsltReader = new XmlTextReader(xsltInput);
                    xsltTransform.Load(xsltReader);
                }
                catch (Exception exception)
                {
                    throw;
                }
                finally
                {
                    Environment.CurrentDirectory = cwd;
                }
            }
        }

        public string CadenaOriginal(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new ArgumentException("Error", "xml");
            }
            StringReader xmlInput = new StringReader(xml);
            XmlTextReader xmlReader = new XmlTextReader(xmlInput);
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter transformedXml = new XmlTextWriter(stringWriter);
            try
            {
                xsltTransform.Transform(xmlReader, transformedXml);
            }
            catch (Exception ex)
            {
                throw;
            }
            return stringWriter.ToString();
        }
    }
}
