using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace PastaFlow_DIAZ_PEREZ.DataAccess
{
    public static class DbConnection
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PastaFlowDB"].ConnectionString;
            return new SqlConnection(connectionString);
        }
    }
}
