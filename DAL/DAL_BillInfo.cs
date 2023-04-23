using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_BillInfo
    {
        Connection con = new Connection();
        List<DTO_BillInfo> bi = new List<DTO_BillInfo>();

        public List<DTO_BillInfo> GetBillInfo(int bill_id)
        {
            
            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "select  bi.billinfo_id, bi.bill_id, bi.drink_id, bi.count from BillInfo bi join Bill b on bi.bill_id = b.bill_id and bi.bill_id = @bill_id ";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@bill_id", bill_id);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DTO_BillInfo bin = new DTO_BillInfo();

                    bin.ID = (int)reader["billinfo_id"];
                    bin.BillID = (int)reader["bill_id"];
                    bin.DrinkID = (int)reader["drink_id"];
                    bin.Count = (int)reader["count"];

                    bi.Add(bin);
                }

                reader.Close();
            }

            return bi;
        }
        public bool DeleteBillInfo(int bill_id)
        {

            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "delete BillInfo where bill_id = @bill_id";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@bill_id", bill_id);
                connection.Open();

                int count = (int)command.ExecuteNonQuery();

                return count > 0;
            }
        }
    }
}
