using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace groenteBoerDatabase
{
    public class databaseCategory
    {
        public static DataTable GetCategories()
        {
            string query = "SELECT * FROM category";
            return DatabaseHelper.ExecuteQuery(query);
        }

        public static void AddCategory(string name)
        {
            string query = "INSERT INTO category (category_name) VALUES (@name)";
            var parameters = new MySqlParameter[] { new MySqlParameter("@name", name) };
            DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        public static void UpdateCategory(int id, string name)
        {
            string query = "UPDATE category SET category_name = @name WHERE ID = @id";
            var parameters = new MySqlParameter[]
            {
            new MySqlParameter("@id", id),
            new MySqlParameter("@name", name)
            };
            DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        public static void DeleteCategory(int id)
        {
            string query = "DELETE FROM category WHERE ID = @id";
            var parameters = new MySqlParameter[] { new MySqlParameter("@id", id) };
            DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
    }
}
