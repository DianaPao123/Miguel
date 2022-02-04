using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;

/// <summary>
/// Descripción breve de CorreoActual
/// </summary>
public class CorreoActual
{
   public  string correoA;////

	public CorreoActual()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
    //-------------------------------------------------------
    public void escribirCorreoActual(string value)
    {
     string texto=actualizarCorreoActual(value );
      string ruta = ConfigurationManager.AppSettings["UserNameRuta"];
     if (!File.Exists(ruta))
     {
         System.IO.Directory.CreateDirectory(ruta);
     }
      
     ruta=Path.Combine(ConfigurationManager.AppSettings["UserNameRuta"], "CorreoActual.txt");
     System.IO.StreamWriter sw = new System.IO.StreamWriter(ruta);
     sw.WriteLine(texto);
     sw.Close();
    }


    //---------------------------------------------------
    public string leerCorreoActual()
    {
        try
        {
           string ruta = Path.Combine(ConfigurationManager.AppSettings["UserNameRuta"], "CorreoActual.txt");
   
            string texto;

            System.IO.StreamReader sr = new System.IO.StreamReader(ruta);
            texto = sr.ReadToEnd();
            sr.Close();
            texto=texto.Replace("\r\n", "");
            correoA=texto;
            return correoActual(texto);
        }
        catch (Exception)
        { correoA = "UserNameGrupo5"; return correoActual("UserNameGrupo5"); }
    
    }
    //--------------------------------------------------------------------------
     private string correoActual(string value)
        {
            switch (value)
            {
                case "UserNameGrupo1":
                    return ConfigurationManager.AppSettings["UserNameGrupo1"];
                case "UserNameGrupo2":
                    return ConfigurationManager.AppSettings["UserNameGrupo2"];
                case "UserNameGrupo3":
                    return ConfigurationManager.AppSettings["UserNameGrupo3"];
                case "UserNameGrupo4":
                    return ConfigurationManager.AppSettings["UserNameGrupo4"];
                case "UserNameGrupo5":
                    return ConfigurationManager.AppSettings["UserNameGrupo5"];
                default:
                     return ConfigurationManager.AppSettings["UserNameGrupo5"];
            }     
        }
        //------------------------------------------------------------------
        //----------------se actualiza el correo actual --------------------------------------------
     private string actualizarCorreoActual(string value)
     {
         switch (value)
         {
             case "UserNameGrupo1":
                 return "UserNameGrupo2";
             case "UserNameGrupo2":
                 return "UserNameGrupo3";
             case "UserNameGrupo3":
                 return "UserNameGrupo4";
             case "UserNameGrupo4":
                 return "UserNameGrupo5";
             case "UserNameGrupo5":
                 return "UserNameGrupo1";
             default:
                 return "UserNameGrupo5";
         }

     }
}