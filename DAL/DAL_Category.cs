using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Category
    {
        Connection con = new Connection();
        List<DTO_Category> categories = new List<DTO_Category>();
        public List<DTO_Category> GetCategory()
        {
            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "select*from Category";
                SqlCommand command = new SqlCommand(query, connection);
                
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DTO_Category category = new DTO_Category();
                    category.ID = (int)reader["category_id"];
                    category.Name = reader["category_name"].ToString();
                    
                    categories.Add(category);
                }

                reader.Close();
            }
            return categories;
        }
        public int GetIDCategoryByName(string categoryname)
        {
            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "select category_id from category where category_name= @categoryname";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@categoryname", categoryname);
                connection.Open();

                int count = (int)command.ExecuteScalar();
                return count;
            }
            
        }
        public string GetNameCategoryByID(int id)
        {
            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "select category_name from category where category_id= @id";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", id);
                connection.Open();

                string name = command.ExecuteScalar().ToString();
                return name;
            }

        }
        public bool InsertCategory(string name)
        {
            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "Check_Exists_Category @name";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@name", name);
                connection.Open();

                int a = command.ExecuteNonQuery();
                return a > 0;
            }

        }
        public void DeleteCategory(int id)
        {
            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "Delete_Category @id";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", id);
                connection.Open();

                command.ExecuteNonQuery();

            }
        }
        public bool UpdateCategory(int id, string name)
        {
            using (SqlConnection connection = new SqlConnection(con.GetConnectionString()))
            {
                string query = "update Category set category_name = @name where category_id = @id";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@id", id);
                
                connection.Open();

                int a = command.ExecuteNonQuery();
                return a > 0;
            }
        }
    }
}
