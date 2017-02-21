using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDev.Util
{
    public class Log
    {
        public Log()
        {
           
        }

        public static void AddMessage(string message)
        {
            string log_path = string.Format("{0}\\{1}.txt", AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["LogFileName"].ToString());
            System.IO.File.AppendAllText(log_path, message);
        }
    }
}
