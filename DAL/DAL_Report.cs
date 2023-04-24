using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Report
    {
        Connection con = new Connection();
        List<DTO_Report> list = new List<DTO_Report>();

        public List<DTO_Report> Report(int bill_id)
        {

            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "select Row_number() over(order by bf.billinfo_id) STT, d.drink_name, d.price, bf.count, d.price*bf.count as tt " +
                    "from BillInfo bf join Bill b on bf.bill_id = b.bill_id  join Drink d on d.drink_id = bf.drink_id " +
                    "where b.bill_id = @bill_id and b.total != 0";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@bill_id", bill_id);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int stt = int.Parse(reader["STT"].ToString());
                    string name = reader["drink_name"].ToString();
                    int price = int.Parse(reader["price"].ToString());
                    int count = (int)reader["count"];
                    int tt = int.Parse(reader["tt"].ToString());

                    DTO_Report report = new DTO_Report(stt,name,price, count, tt);

                    list.Add(report);
                }

                reader.Close();
            }
            return list;
        }

        public int GetTotal(int bill_id)
        {

            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "select total from Bill where bill_id = @bill_id";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@bill_id", bill_id);
                connection.Open();

                int count = (int)command.ExecuteScalar();

                return count ;
            }
        }
        
        public int GetDiscount(int bill_id)
        {

            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "select discount from Bill where bill_id = @bill_id";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@bill_id", bill_id);
                connection.Open();

                int count = (int)command.ExecuteScalar();

                return count ;
            }
        }

        public string GetTableName(int bill_id)
        {

            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "select dt.table_name from Bill b join DrinkTable dt on b.table_id = dt.table_id where bill_id = @bill_id ";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@bill_id", bill_id);
                connection.Open();

                string TableName = command.ExecuteScalar().ToString();

                return TableName;
            }
        }
    }
}
