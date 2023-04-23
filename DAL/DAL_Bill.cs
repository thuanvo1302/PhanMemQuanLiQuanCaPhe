using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Bill
    {
        Connection con = new Connection();
        List<DTO_Menu> billinf = new List<DTO_Menu>();
        List<DTO_Statistic> bills= new List<DTO_Statistic>();
        public DTO_Bill GetBill(int table_id)
        {
            DTO_Bill bill = new DTO_Bill();
            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "select * from Bill where table_id = @val1 and status = N'Chưa thanh toán'";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@val1", table_id);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    
                    bill.Id= (int)reader["bill_id"];
                    bill.Checkin = (DateTime)reader["date_checkin"];
                    bill.Checkout = (DateTime)reader["date_checkout"];
                    bill.Table_ID = (int)reader["table_id"];
                    bill.Status = reader["status"].ToString();
                    bill.Discount = (int)reader["discount"];
                    bill.Total = (int)reader["total"];

                }

                reader.Close();
            }

            return bill;
        }
        public List<DTO_Menu> GetBillIn(int table_id)
        {

            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "exec get_billinfo  @table_id";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@table_id", table_id);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    string name = reader["drink_name"].ToString();
                    int count = (int)reader["count"];
                    float price = float.Parse(reader["price"].ToString());
                    float totalprice = float.Parse(reader["TotalPrice"].ToString());

                    DTO_Menu bill = new DTO_Menu(name, count, price, totalprice);

                    billinf.Add(bill);
                }

                reader.Close();
            }

            return billinf;
        }
        public void InsertBill(int table_id)
        {

            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "insert into Bill values(getdate(),getdate(),@table_id,N'Chưa thanh toán',0,0)";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@table_id", table_id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void UpdateStatusTable(int table_id, string status)
        {

            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "exec Update_Status_Table @table_id, @status";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@table_id", table_id);
                command.Parameters.AddWithValue("@status", status);
                connection.Open();

                command.ExecuteNonQuery();
            }
        }
        public void InsertBillInfo(int bill_id, int drink_id, int count)
        {

            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "insert into BillInfo values(@bill_id, @drink_id, @count)";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@bill_id", bill_id);
                command.Parameters.AddWithValue("@drink_id", drink_id);
                command.Parameters.AddWithValue("@count", count);
                connection.Open();

                command.ExecuteNonQuery();
            }
        }
        public bool GetDrinkIdFromBillIn(int drink_id, int bill_id)
        {

            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "select count(*) from BillInfo where drink_id=@drink_id and bill_id = @bill_id";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@drink_id", drink_id);
                command.Parameters.AddWithValue("@bill_id", bill_id);
                connection.Open();

                int count = (int)command.ExecuteScalar();

                return count>0;
            }
        }
        
        public void UpdateBillInfo(int bill_id, int drink_id, int count)
        {
            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "exec Update_BillInfo @bill_id, @drink_id , @count ";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@bill_id", bill_id);
                command.Parameters.AddWithValue("@drink_id", drink_id);
                command.Parameters.AddWithValue("@count", count);
                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        public void UpdateBill(int bill_id, float discount, float total)
        {

            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "update Bill set status = N'Đã thanh toán',discount = @discount, total = @total where bill_id = @bill_id";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@bill_id", bill_id);
                command.Parameters.AddWithValue("@discount", discount);
                command.Parameters.AddWithValue("@total", total);
                connection.Open();

                command.ExecuteNonQuery();
            }
        }
        public List<DTO_Statistic> GetAllBill(string from, string to)
        {

            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "select * from Bill where status = N'Đã thanh toán' and cast((date_checkin) as date) >= @from and cast((date_checkout) as date) <= @to and total != 0";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@from", from);
                command.Parameters.AddWithValue("@to", to);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DateTime checkin = (DateTime)reader["date_checkin"];
                    DateTime checkout = (DateTime)reader["date_checkout"];
                    int id = (int)reader["bill_id"];
                    int dis = (int)reader["discount"];
                    int tot = (int)reader["total"];
                    
                    DTO_Statistic statistic = new DTO_Statistic(checkin, checkout, dis, tot, id);

                    bills.Add(statistic);
                }

                reader.Close();
            }
            return bills;
        }
        public List<DTO_Statistic> GetDrinkNameAndCount(int bill_id)
        {

            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "select d.drink_name, bf.count from BillInfo bf " +
                    "join Bill b on bf.bill_id = b.bill_id  " +
                    "join Drink d on d.drink_id = bf.drink_id where b.bill_id = @bill_id and b.total != 0";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@bill_id", bill_id);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string name = reader["drink_name"].ToString();
                    int count = (int)reader["count"];
                    
                    DTO_Statistic statistic = new DTO_Statistic(name,count);

                    bills.Add(statistic);
                }

                reader.Close();
            }
            return bills;
        }
        public bool DeleteBillIn(int billinfo_id)
        {

            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "delete BillInfo where billinfo_id = @billinfo_id";
                SqlCommand command = new SqlCommand(query, connection);
                
                command.Parameters.AddWithValue("@billinfo_id", billinfo_id);
                connection.Open();

                int count = (int)command.ExecuteNonQuery();

                return count > 0;
            }
        }
        public bool DeleteBill(int bill_id)
        {

            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "delete Bill where bill_id = @bill_id";
                SqlCommand command = new SqlCommand(query, connection);
                
                command.Parameters.AddWithValue("@bill_id", bill_id);
                connection.Open();

                int count = (int)command.ExecuteNonQuery();

                return count > 0;
            }
        }

        public bool Delete()
        {

            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "exec delete_bill_0";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                int count = (int)command.ExecuteNonQuery();

                return count > 0;
            }
        }



        public int GetCount(string from, string to)
        {

            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "select sum(count) from BillInfo bi join Bill b on bi.bill_id = b.bill_id and cast((date_checkin) as date) >= @from and cast((date_checkout) as date) <= @to";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@from", from);
                command.Parameters.AddWithValue("@to", to);
                connection.Open();

                int count = (int)command.ExecuteScalar();

                return count;
            }
        }
        public int GetIDBill(int table_id)
        {

            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "select bill_id from Bill where table_id = @table_id";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@table_id", table_id);
                connection.Open();

                int count = (int)command.ExecuteScalar();

                return count;
            }
        }
        


    }
}
