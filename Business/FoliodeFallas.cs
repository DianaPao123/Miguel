using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contract;
using System.ServiceModel;

namespace Business
{
    public class FoliodeFallas : GAFBusiness
    {
        //------------------------------------------------------------------------
        public bool Save(FolioFallas u)
        {
            int m = 1;
            try
            {
                using (var db = new GAFEntities())
                {


                    db.FolioFallas.AddObject(u);
                    db.SaveChanges();

                    return true;

                }
            }
            catch (ApplicationException ae)
            {
                throw new FaultException(ae.Message);
            }
            catch (FaultException fe)
            {
                throw;
            }
            catch (Exception ee)
            {
                Logger.Error(ee.Message);
                if (ee.InnerException != null)
                    Logger.Error(ee.InnerException.Message);
                return false;
            }
        }
        //---------------------------------------------
    
    }
}
