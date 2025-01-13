using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace groenteBoerDatabase
{
    public class databaseProduct
    {
        public static DataTable GetProducts()
        {
            string query = "SELECT * FROM product";
            return DatabaseHelper.ExecuteQuery(query);
        }

        public static void AddProduct(string name, decimal price, byte[] image, int categoryId)
        {
            string query = "INSERT INTO product (name, price, image, category_id) VALUES (@name, @price, @image, @categoryId)";
            var parameters = new MySqlParameter[]
            {
            new MySqlParameter("@name", name),
            new MySqlParameter("@price", price),
            new MySqlParameter("@image", image),
            new MySqlParameter("@categoryId", categoryId)
            };
            DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        public static void UpdateProduct(int id, string name, decimal price, byte[] image, int categoryId)
        {
            string query = "UPDATE product SET name = @name, price = @price, image = @image, category_id = @categoryId WHERE ID = @id";
            var parameters = new MySqlParameter[]
            {
            new MySqlParameter("@id", id),
            new MySqlParameter("@name", name),
            new MySqlParameter("@price", price),
            new MySqlParameter("@image", image),
            new MySqlParameter("@categoryId", categoryId)
            };
            DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        public static void DeleteProduct(int id)
        {
            string query = "DELETE FROM product WHERE ID = @id";
            var parameters = new MySqlParameter[] { new MySqlParameter("@id", id) };
            DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
    }
}
