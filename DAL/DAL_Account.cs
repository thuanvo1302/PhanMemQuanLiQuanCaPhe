using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAL
{
    public class DAL_Account
    {
        
        Connection con = new Connection();
        List<DTO_Account> accounts = new List<DTO_Account>();

        public DTO_Account GetAccounts(string tk, string mk)
        {

            DTO_Account account = new DTO_Account();
            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "[dbo].[Ktra_Account] @taikhoan, @matkhau ";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@taikhoan", tk);
                command.Parameters.AddWithValue("@matkhau", mk);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    account.TaiKhoan = reader["TaiKhoan"].ToString();
                    account.MatKhau = reader["MatKhau"].ToString();
                    account.QuyenHan = reader["PhanQuyen"].ToString();

                }

                reader.Close();
            }

            return account;
        }
        public DTO_Account GetAccount(string tk)
        {

            DTO_Account account = new DTO_Account();
            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "select * from Account where TaiKhoan =  @taikhoan";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@taikhoan", tk);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    account.TaiKhoan = reader["TaiKhoan"].ToString();
                    account.MatKhau = reader["MatKhau"].ToString();
                    account.QuyenHan = reader["PhanQuyen"].ToString();

                }

                reader.Close();
            }

            return account;
        }
        public bool CheckExistsAccount(string tk)
        {
            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = " select count(*) from Account where TaiKhoan = @taikhoan";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@taikhoan", tk);
                connection.Open();

                int res = (int)command.ExecuteScalar();
                return res > 0;
            }
        }
        public void InsertAccount(string tk, string mk)
        {
            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "insert into Account values(@taikhoan, @matkhau,N'Nhân viên')";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@taikhoan", tk);
                command.Parameters.AddWithValue("@matkhau", mk);
                connection.Open();

                command.ExecuteNonQuery();

            }
        }

        public bool InsertAccountAdmin(string tk, string mk)
        {
            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "insert into Account values(@taikhoan, @matkhau,N'Admin')";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@taikhoan", tk);
                command.Parameters.AddWithValue("@matkhau", mk);
                connection.Open();

                int res = command.ExecuteNonQuery();
                return res > 0;
            }
        }
        public void UpdateAccount(string tk, string new_mk)
        {
            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "update Account set MatKhau = @matkhau where TaiKhoan = @taikhoan";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@matkhau", new_mk);
                command.Parameters.AddWithValue("@taikhoan", tk);
                
                connection.Open();
                command.ExecuteNonQuery();

            }
        }
        public List<DTO_Account> GetAllAccount()
        {
            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "select * from Account";
                SqlCommand command = new SqlCommand(query, connection);
                
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DTO_Account account = new DTO_Account();
                    account.TaiKhoan = reader["TaiKhoan"].ToString();
                    account.MatKhau = reader["MatKhau"].ToString();
                    account.QuyenHan = reader["PhanQuyen"].ToString();
                    accounts.Add(account);
                }

                reader.Close();
            }
            return accounts;
        }

        public bool DeleteAccount(string tk)
        {
            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "delete From Account where TaiKhoan = @taikhoan";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@taikhoan", tk);

                connection.Open();

                int res = command.ExecuteNonQuery();
                return res > 0;
            }
        }
       
    }
}
