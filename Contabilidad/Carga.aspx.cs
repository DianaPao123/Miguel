using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using System.IO;

using System.Data;
using Business;
using System.Configuration;
namespace GAFWEB
{
    public partial class Carga : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubir_Click(object sender, EventArgs e)
        {
            if (archivoPagos.HasFile)//por si hay archivo
            {
                var sesion = Session["sessionRGV"] as Sesion;

                string nombreArchivo = archivoPagos.FileName;
                var archivo = this.archivoPagos.FileBytes;
                string x = guardarArchivo(archivo, nombreArchivo);
                if (x == "OK")
                {

                    PrefacturaExcel excel = new PrefacturaExcel();
                    DataTable T = excel.ExtraerExcel(ViewState["rutaExcel"].ToString());
                    if (T != null)
                    {
                        // excel.GenerarPrefacturasMasivas(T);
                        x = excel.GenerarTransferenciasExcelMasivas(T, sesion.Id);
                        if (x != "OK")
                            lblError.Text = x;
                        else

                            lblError.Text = "El archivo fue Cargado Correctamente.";
                    }
                    else

                        lblError.Text = "Error en el formato excel.";
           
                }
            }

        }
        //------------------------------------------------------------------------
        protected string guardarArchivo(byte[] archivo, string nombre)
        {
            try
            {
                string ruta = ConfigurationManager.AppSettings["rutaExcel"] + "_" + DateTime.Now.ToFileTime();
                ViewState["rutaExcel"] = ruta+"\\"+nombre;
                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);
                ruta = Path.Combine(ruta, nombre);
                File.WriteAllBytes(ruta, archivo);
                return "OK";
            }
            catch (Exception ee)
            {
                return ee.Message;
            }
        }
    }
}