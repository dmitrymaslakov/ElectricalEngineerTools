using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalEngineerTools.Framework.DAL.Services
{
    public static class Base
    {
        public static string ConnectionString
        {
            get
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration
                    (System.Reflection.Assembly.GetExecutingAssembly().Location);
                string connectionString = config.ConnectionStrings.ConnectionStrings["DefaultConnection"].ConnectionString;
                return connectionString;
            }
        }
    }
}
