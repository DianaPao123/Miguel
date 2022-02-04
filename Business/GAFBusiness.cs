using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;


namespace Business
{
    public class GAFBusiness
    {
        protected static ILog Logger = LogManager.GetLogger(typeof(GAFBusiness));
        protected GAFBusiness()
        {
            XmlConfigurator.Configure();
        }

    }
}
