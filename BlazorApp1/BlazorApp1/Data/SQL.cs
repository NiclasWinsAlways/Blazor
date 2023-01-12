using BlazorApp1.Data;
using System.Data;
using System.Data.SqlClient;

namespace BlazorApp1.Data
{
    public class SQL
    {
        SqlConnection con = new SqlConnection(
               "Data Source =.; " +
               "Initial Catalog = Breakfasth1; " +
               "Integrated Security = True;");
        public bool CreateFood(Food f)
        {
            SqlCommand cmd = new("INSERT INTO [food] VALUES (@item, @amount, @Cost)", con);
            cmd.Parameters.Add("@item", SqlDbType.NVarChar).Value = f.Item;
            cmd.Parameters.Add("@amount", SqlDbType.Int).Value = f.Amount;
            cmd.Parameters.Add("@Cost", SqlDbType.Decimal).Value = f.Cost;
            con.Open();
            bool isSuccess = cmd.ExecuteNonQuery() == 1 ? true : false;
            con.Close();
            return isSuccess;
        }

        public List<Food> ReadFood()
        {
            List<Food> list = new();
            SqlCommand cmd = new("SELECT * FROM [food]", con);
            con.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Food f = new()
                    {
                        Id = reader.GetInt32(0),
                        Item = reader.GetString(1),
                        Amount = reader.GetInt32(2),
                        Cost = (double)reader.GetDecimal(3)
                    };
                    list.Add(f);
                }
            }
            con.Close();
            return list;
        }

        public bool DeleteFood(int id)
        {
            SqlCommand cmd = new("DELETE FROM [food] WHERE Id = @id", con);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            con.Open();
            bool isSuccess = cmd.ExecuteNonQuery() == 1 ? true : false;
            con.Close();
            return isSuccess;
        }
    }
}