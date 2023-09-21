using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public abstract class connection
    {
        private readonly string connectionString;
        public connection()
        {
            connectionString = ConfigurationManager.ConnectionStrings["myconection"].ConnectionString;
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
