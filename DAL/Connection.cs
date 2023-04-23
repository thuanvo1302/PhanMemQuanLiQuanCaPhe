using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Connection
    {
        public string GetConnectionString()
        {
            SqlConnectionStringBuilder builder;

            builder = new System.Data.SqlClient.SqlConnectionStringBuilder();
            builder["Data Source"] = "LAPTOP-IVVBA0SM\\SQLEXPRESS";
            builder["integrated Security"] = true;
            builder["Initial Catalog"] = "QuanLiQuanCaPhe";
            builder.UserID = "LAPTOP-IVVBA0SM\\VIVOBOOK";
            builder["Password"] = "";

            return builder.ConnectionString;
        }
    }
}
