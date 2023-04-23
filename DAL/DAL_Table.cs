using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Table
    {
        private List<DTO_Table> tables = new List<DTO_Table>();
        public static int TableWidth = 100;
        public static int TableHeight = 100;
        
        public DAL_Table() { }
        Connection con = new Connection();
        
        public List<DTO_Table> GetTable()
        {

            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "exec [dbo].[get_table] ";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    
                    int id = (int)reader["table_id"];
                    string name = reader["table_name"].ToString();
                    string status = reader["status"].ToString();

                    DTO_Table table = new DTO_Table(id,name,status);
                    tables.Add(table);
                }

                reader.Close();
            }

            return tables;
        }
        public void AutoTable(int count)
        {
            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "create_table @count";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@count", count);

                connection.Open();

                command.ExecuteNonQuery();

            }
        }
        public bool InsertTable()
        {
            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "exec insert_table";
                SqlCommand command = new SqlCommand(query, connection);


                connection.Open();

                int res = command.ExecuteNonQuery();
                return res > 0;
            }
        }
        public bool DeleteTable(int id)
        {
            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "delete from DrinkTable where table_id = @id";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", id);
                connection.Open();

                int res = command.ExecuteNonQuery();
                return res > 0;
            }
        }
        
    }
}
