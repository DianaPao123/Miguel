using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contract
{
   public class GAFTransferenciasExcel : GAFContract
    {
       public List<TransferenciasExcel> GetTransferenciaAll()
       {

           try
           {
               List<TransferenciasExcel> cliente;
               using (var db = new GAFEntities())
               {
                       cliente = db.TransferenciasExcel.Where(p=>p.Activo==null).ToList();
                 
                   return cliente;
               }
           }
           catch (Exception ee)
           {
                return null;
           }

       }

       public int EliminarTrasferencia()
       {

           try
           {
               List<TransferenciasExcel> cliente;
               using (var db = new GAFEntities())
               {
                   cliente = db.TransferenciasExcel.Where(p=>p.Activo==null).ToList();
                   foreach (var row in cliente)
                   {
                       db.TransferenciasExcel.DeleteObject(row);
                   }
                   db.SaveChanges();
                   return 1;
               }
           }
           catch (Exception ee)
           {
               return 0;
           }

       }
       public TransferenciasExcel GetTransferenciaId(Int64 id)
       {

           try
           {
               TransferenciasExcel cliente;
               using (var db = new GAFEntities())
               {
                   cliente = db.TransferenciasExcel.Where(c => c.ID == id).FirstOrDefault(); 

                   return cliente;
               }
           }
           catch (Exception ee)
           {
               return null;
           }

       }

        public List<TransferenciasExcelConceptos> GetTransferenciaConceptosId(Int64 id)
        {

            try
            {
                using (var db = new GAFEntities())
                {
                  var  cliente = db.TransferenciasExcelConceptos.Where(c => c.IDTrasferencias == id).ToList();

                    return cliente;
                }
            }
            catch (Exception ee)
            {
                return null;
            }

        }

        public string Confirmacion(Int64 IdTimbre)
       {
           try
           {

               using (var db = new GAFEntities())
               {
                   {
                       TransferenciasExcel C = db.TransferenciasExcel.FirstOrDefault(p => p.ID == IdTimbre);
                       C.Activo = true;
                       db.TransferenciasExcel.ApplyCurrentValues(C);
                   }
                   db.SaveChanges();
                   return "OK";
               }
           }
           catch (Exception eee)
           {
               Logger.Error(eee.Message);
               return ("Error al Confirmar ");
           }

       }
        //--------------------------------------
      
    }
}
