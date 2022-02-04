#region

using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto.Parameters;


#endregion

namespace Business
{
  
    

    public class OpensslKey
    {

        public static RSACryptoServiceProvider DecodePrivateKey(byte[] archivo, string pass, string format)
        {
            try
            {
                if (format == ".pem")
                {
                    var datos = Encoding.ASCII.GetString(archivo);
                    datos = datos.Replace("-----BEGIN ENCRYPTED PRIVATE KEY-----", "");
                    datos = datos.Replace("-----END ENCRYPTED PRIVATE KEY-----", "");
                    archivo = Convert.FromBase64String(datos);
                }
                var key = PrivateKeyFactory.DecryptKey(pass.ToCharArray(),new MemoryStream(archivo));
                RsaPrivateCrtKeyParameters pars = (RsaPrivateCrtKeyParameters)key;
                RSAParameters parms = DotNetUtils.ToRSAParameters(pars);
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.ImportParameters(parms);
                return rsa;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

    }
}
