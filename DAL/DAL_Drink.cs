using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Drink
    {
        Connection con = new Connection();
        List<DTO_Drink> drinks= new List<DTO_Drink>();

        public List<DTO_Drink> GetDrink(int id)
        {
            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "select * from Drink where category_id=@val1";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@val1", id);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DTO_Drink drink = new DTO_Drink();
                    drink.ID = (int)reader["drink_id"];
                    drink.DrinkName = reader["drink_name"].ToString();
                    drink.CategoryID = (int)reader["category_id"];
                    drink.Price = float.Parse(reader["price"].ToString());
                    
                    drinks.Add(drink);
                }

                reader.Close();
            }
            return drinks;
        }

        public List<DTO_Drink> GetDrinkIDFromBillID(int bill_id)
        {
            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "select bi.drink_id, d.drink_name from Drink d join BillInfo bi on d.drink_id = bi.drink_id where bi.bill_id = @bill_id";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@bill_id", bill_id);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DTO_Drink drink = new DTO_Drink();
                    drink.ID = (int)reader["drink_id"];
                    drink.DrinkName = reader["drink_name"].ToString();

                    drinks.Add(drink);
                }

                reader.Close();
            }
            return drinks;
        }
        
        public List<DTO_Drink> GetAllDrink()
        {
            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "select * from Drink";
                SqlCommand command = new SqlCommand(query, connection);
                
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DTO_Drink drink = new DTO_Drink();
                    drink.ID = (int)reader["drink_id"];
                    drink.DrinkName = reader["drink_name"].ToString();
                    drink.CategoryID = (int)reader["category_id"];
                    drink.Price = float.Parse(reader["price"].ToString());

                    drinks.Add(drink);
                }

                reader.Close();
            }
            return drinks;
        }
        public bool InsertDrink(string name, int category, float price)
        {
            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "Check_Exists_Drink @name, @category, @price";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@category", category);
                command.Parameters.AddWithValue("@price", price);
                connection.Open();

                int a = command.ExecuteNonQuery();
                return a > 0;
            }
        }
        public void DeleteDrink(int id)
        {
            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "delete Drink where drink_id=@id";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", id);
                connection.Open();

                command.ExecuteNonQuery();

            }
        }
        public bool UpdateDrink(int id, string name, int idcategory, float price)
        {
            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "update Drink set drink_name = @name, category_id = @idcategory, price = @price where drink_id = @id";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@idcategory", idcategory);
                command.Parameters.AddWithValue("@price", price);
                connection.Open();

                int a = command.ExecuteNonQuery();
                return a > 0;
            }
        }
    }
}
