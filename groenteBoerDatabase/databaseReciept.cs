using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace groenteBoerDatabase
{
    public class databaseReciept
    {
        public static DataTable GetReceipts()
        {
            string query = "SELECT * FROM receipt";
            return DatabaseHelper.ExecuteQuery(query);
        }

        public static void AddReceipt(DateTime date, decimal totalPrice)
        {
            string query = "INSERT INTO receipt (date, total_price) VALUES (@date, @totalPrice)";
            var parameters = new MySqlParameter[]
            {
            new MySqlParameter("@date", date),
            new MySqlParameter("@totalPrice", totalPrice)
            };
            DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        public static void UpdateReceipt(int id, DateTime date, decimal totalPrice)
        {
            string query = "UPDATE receipt SET date = @date, total_price = @totalPrice WHERE ID = @id";
            var parameters = new MySqlParameter[]
            {
            new MySqlParameter("@id", id),
            new MySqlParameter("@date", date),
            new MySqlParameter("@totalPrice", totalPrice)
            };
            DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        public static void DeleteReceipt(int id)
        {
            string query = "DELETE FROM receipt WHERE ID = @id";
            var parameters = new MySqlParameter[] { new MySqlParameter("@id", id) };
            DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
    }
}
