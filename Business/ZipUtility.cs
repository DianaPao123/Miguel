using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;

namespace Business
{
    public static class ZipUtility
    {
        private const String _zipExtension = ".zip";

        public static string CompressOrAppendZip(List<string> files, string zipFileName)
        {

            using (ZipFile file = ZipFile.Create(zipFileName))
            {
                file.BeginUpdate();
                foreach (var f in files)
                {
                    file.Add(f);
                }
                
                file.CommitUpdate();
            }
            return zipFileName;
        }
    }
}
