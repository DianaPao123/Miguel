using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;

namespace Contract
{
    public class GAFContract
    {

        protected static ILog Logger = LogManager.GetLogger(typeof(GAFContract));
        protected GAFContract()
        {
            XmlConfigurator.Configure();
        }

    }
}
