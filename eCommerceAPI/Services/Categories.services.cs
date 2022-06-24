using eCommerceAPI.Models;
using System;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace eCommerceAPI.Services
{
    public class CategoriesService
    {
        private readonly IConfiguration Configuration;
        private readonly SqlConnection DBConnection;
        public CategoriesService( IConfiguration configuration )
        {

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.ConnectionString = configuration.GetSection("connectionStrings")["defaultConnectionString"];
            DBConnection = new SqlConnection(builder.ConnectionString);
        }

        public List<Category> GetAll()
        {
            var categories = new List<Category>() { };
            try
            {
                using ( SqlConnection con = DBConnection )
                {
                    con.Open();
                    using ( SqlCommand cmd = con.CreateCommand() )
                    {
                        cmd.CommandText = "SELECT * FROM Categories;";
                        using ( SqlDataReader reader = cmd.ExecuteReader() )
                        {
                            while ( reader.Read() )
                            {
                                categories.Add(new Category(
                                    reader.GetInt32(0),
                                    reader.GetString(1)
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
            return categories;
        }

        public Category Get(int ID)
        {
            Category category = null;
            try
            {

                using ( SqlConnection con = DBConnection )
                {
                    con.Open();
                    using ( SqlCommand cmd = con.CreateCommand() )
                    {
                        cmd.CommandText = $"SELECT * FROM Categories WHERE ID = {ID}";
                        using ( SqlDataReader reader = cmd.ExecuteReader() )
                        {
                            while ( reader.Read() )
                            {
                                category = new Category(
                                    reader.GetInt32(0),
                                    reader.GetString(1)
                                    )
                                 ;
                            }
                        }
                    }

                }
            }
            catch ( Exception ex )
            {

                throw ex.GetBaseException();
            }

            return category;
        }

        public void Create( Category category )
        {
            try
            {
                using ( SqlConnection con = DBConnection )
                {
                    con.Open();
                    using ( SqlCommand cmd = con.CreateCommand() )
                    {
                        cmd.CommandText =
                            $"INSERT INTO Categories (CategoryName) VALUES ('{category.CategoryName}');";
                        cmd.ExecuteNonQuery();
                    }

                }

            }
            catch ( Exception ex )
            {
                throw ex.GetBaseException();
            }
        }

        public void Update( Category category, int id )
        {

            try
            {
                using ( SqlConnection con = DBConnection )
                {
                    con.Open();
                    using ( SqlCommand cmd = con.CreateCommand() )
                    {
                        cmd.CommandText =
                            $"UPDATE Categories SET CategoryName = '{category.CategoryName}' WHERE ID = {id};";
                        cmd.ExecuteNonQuery();
                    }

                }

            }
            catch ( Exception ex )
            {
                throw ex.GetBaseException();
            }
        }

        public void Delete( int ID )
        {
            try
            {
                try
                {
                    using ( SqlConnection con = DBConnection )
                    {
                        con.Open();
                        using ( SqlCommand cmd = con.CreateCommand() )
                        {
                            cmd.CommandText = $"DELETE FROM Categories WHERE ID = {ID}";
                            cmd.ExecuteNonQuery();
                        }

                    }
                }
                catch ( Exception ex )
                {

                    throw ex;
                }
            }
            catch ( Exception ex )
            {

                throw ex;
            }
        }

    }
}