using eCommerceAPI.Models;
using System;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace eCommerceAPI.Services
{
    public class ProductServices
    {
        private readonly IConfiguration Configuration;
        private readonly SqlConnection DBConnection;
        public ProductServices(IConfiguration configuration)
        {

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.ConnectionString = configuration.GetSection("connectionStrings")["defaultConnectionString"];
            DBConnection = new SqlConnection(builder.ConnectionString);
        }
        public void Create(Product product)
        {
            try
            {
                using (SqlConnection con = DBConnection)
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText =
                            $"INSERT INTO Products (ProductName, Price, Description, CategoryID, ImageURL) " +
                                        $"VALUES ( '{product.ProductName}', {product.Price}, '{product.Description}', {product.CategoryID}, '{product.ImageURL}');";
                        cmd.ExecuteNonQuery();
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex.GetBaseException();
            }
        }
        public void Delete(int ID)
        {
          
          try
            {
                using ( SqlConnection con = DBConnection )
                {
                    con.Open();
                    using ( SqlCommand cmd = con.CreateCommand() )
                    {
                        cmd.CommandText = $"DELETE FROM Products WHERE ID = {ID}";
                        cmd.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
        public Product Get(int ID)
        {
            Product product = null;
            try
            {
                using ( SqlConnection con = DBConnection )
                {
                    con.Open();
                    using ( SqlCommand cmd = con.CreateCommand() )
                    {
                        cmd.CommandText = $"SELECT * FROM Products WHERE ID = {ID}";
                        using ( SqlDataReader reader = cmd.ExecuteReader() )
                        {
                            while ( reader.Read() )
                            {
                                product = new Product(
                                     reader.GetInt32(reader.GetOrdinal("ID")),
                                    reader.GetString(reader.GetOrdinal("ProductName")),
                                    reader.GetDouble(reader.GetOrdinal("Price")),
                                    reader.GetString(reader.GetOrdinal("Description")),
                                    reader.GetInt32(reader.GetOrdinal("CategoryID")),
                                    reader.GetString(reader.GetOrdinal("ImageURL"))

                                    )
                                 ;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return product;
        }
        public List<Product> GetAll()
        {
            var products = new List<Product>() { };
            try
            {
                using (SqlConnection con = DBConnection)
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM Products";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                products.Add(new Product(
                                    reader.GetInt32(reader.GetOrdinal("ID")),
                                    reader.GetString(reader.GetOrdinal("ProductName")),
                                    reader.GetDouble(reader.GetOrdinal("Price")),
                                    reader.GetString(reader.GetOrdinal("Description")),
                                    reader.GetInt32(reader.GetOrdinal("CategoryID")),
                                    reader.GetString(reader.GetOrdinal("ImageURL"))
                                    )
                                 );
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return products;
        }
        public void Update(Product product, int id)
        {

            try
            {
                using ( SqlConnection con = DBConnection )
                {
                    con.Open();
                    using ( SqlCommand cmd = con.CreateCommand() )
                    {
                        cmd.CommandText =
                            $"UPDATE Products " +
                                        $"SET " +
                                        $"ProductName = '{product.ProductName}', " +
                                        $"Price = {product.Price}, " +
                                        $"Description = '{product.Description}', " +
                                        $"CategoryID = {product.CategoryID}, " +
                                        $"ImageURL ='{product.ImageURL}'" +
                                        $"WHERE ID = {id};";
                        cmd.ExecuteNonQuery();
                    }

                }

            }
            catch ( Exception ex )
            {
                throw ex.GetBaseException();
            }
        }
        public List<Product> SearchProduct(string searchTerm) {

            var productFounds = new List<Product>() { };
            try
            {
                using ( SqlConnection con = DBConnection )
                {
                    con.Open();
                    using ( SqlCommand cmd = con.CreateCommand() )
                    {
                        cmd.CommandText = $"SELECT * FROM Products WHERE ProductName LIKE '%{searchTerm}%'";
                        using ( SqlDataReader reader = cmd.ExecuteReader() )
                        {
                            while ( reader.Read() )
                            {
                                productFounds.Add(new Product(
                                    reader.GetInt32(reader.GetOrdinal("ID")),
                                    reader.GetString(reader.GetOrdinal("ProductName")),
                                    reader.GetDouble(reader.GetOrdinal("Price")),
                                    reader.GetString(reader.GetOrdinal("Description")),
                                    reader.GetInt32(reader.GetOrdinal("CategoryID")),
                                    reader.GetString(reader.GetOrdinal("ImageURL"))
                                    )
                                 );
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex.GetBaseException();
            }

            return productFounds;
        }
        public List<Product> GetByCategory(int catID)
        {
            var products = new List<Product>() { };
            try
            {
                using ( SqlConnection con = DBConnection )
                {
                    con.Open();
                    using ( SqlCommand cmd = con.CreateCommand() )
                    {
                        cmd.CommandText = $"SELECT * FROM Products WHERE CategoryID = {catID}";
                        using ( SqlDataReader reader = cmd.ExecuteReader() )
                        {
                            while ( reader.Read() )
                            {
                                products.Add(new Product(
                                    reader.GetInt32(reader.GetOrdinal("ID")),
                                    reader.GetString(reader.GetOrdinal("ProductName")),
                                    reader.GetDouble(reader.GetOrdinal("Price")),
                                    reader.GetString(reader.GetOrdinal("Description")),
                                    reader.GetInt32(reader.GetOrdinal("CategoryID")),
                                    reader.GetString(reader.GetOrdinal("ImageURL"))
                                    )
                                 );
                            }
                        }
                    }

                }

            }
            catch ( Exception ex )
            {

                throw ex.GetBaseException();
            }
            return products;
        }
    }
}