using eCommerceAPI.Models;
using System;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace eCommerceAPI.Services
{
    public class UserService
    {
        private readonly IConfiguration Configuration;
        private readonly SqlConnection DBConnection;
        public UserService( IConfiguration configuration )
        {

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.ConnectionString = configuration.GetSection("connectionStrings")["defaultConnectionString"];
            DBConnection = new SqlConnection(builder.ConnectionString);
        }
        public dynamic SignUp( User user )
        {
            User userToReturn = null;

            try
            {
                using ( SqlConnection con = DBConnection )
                {
                    con.Open();
                    using ( SqlCommand cmd = con.CreateCommand() )
                    {
                        cmd.CommandText =
                            $"INSERT INTO Users (Email,FullName, Password) VALUES ('{user.Email}','{user.FullName}','{user.Password}');";
                        using ( SqlDataReader reader = cmd.ExecuteReader() )
                        {
                            while ( reader.Read() )
                            {
                                userToReturn = new User(
                                     0,
                                     reader.GetString(reader.GetOrdinal("Email")),
                                     reader.GetString(reader.GetOrdinal("Password")),
                                     reader.GetString(reader.GetOrdinal("FullName"))
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

            return userToReturn;
        }

        public User SignIn( User user )
        {
            User userToReturn = null;
            try
            {
                using ( SqlConnection con = DBConnection )
                {
                    con.Open();
                    using ( SqlCommand cmd = con.CreateCommand() )
                    {
                        cmd.CommandText =
                            $"SELECT * FROM Users WHERE Email = '{user.Email}' AND Password = '{user.Password}';";
                        using ( SqlDataReader reader = cmd.ExecuteReader() )
                        {
                            while ( reader.Read() )
                            {
                                userToReturn = new User(
                                     reader.GetInt32(reader.GetOrdinal("ID")),
                                     reader.GetString(reader.GetOrdinal("Email")),
                                     reader.GetString(reader.GetOrdinal("Password")),
                                     reader.GetString(reader.GetOrdinal("FullName"))
                                    );
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

            return userToReturn;
        }

        public List<User> GetAll()
        {
            List<User> userList = new List<User>() { };
            try
            {
                using ( SqlConnection con = DBConnection )
                {
                    con.Open();
                    using ( SqlCommand cmd = con.CreateCommand() )
                    {
                        cmd.CommandText =
                            $"SELECT * FROM Users;";
                        using ( SqlDataReader reader = cmd.ExecuteReader() )
                        {
                            while ( reader.Read() )
                            {
                                userList.Add(new User(
                                     reader.GetInt32(reader.GetOrdinal("ID")),
                                     reader.GetString(reader.GetOrdinal("Email")),
                                     reader.GetString(reader.GetOrdinal("Password")),
                                     reader.GetString(reader.GetOrdinal("FullName"))
                                    ));
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

            return userList;
        }

        public User Update( User user )
        {
            User userToReturn = null;
            try
            {
                using ( SqlConnection con = DBConnection )
                {
                    con.Open();
                    using ( SqlCommand cmd = con.CreateCommand() )
                    {
                        cmd.CommandText =
                            $"UPDATE Users SET Email = '{user.Email}', Password = '{user.Password}' , FullName = '{user.FullName}' WHERE ID = {user.ID};";
                        using ( SqlDataReader reader = cmd.ExecuteReader() )
                        {
                            while ( reader.Read() )
                            {
                                userToReturn = new User(
                                     reader.GetInt32(reader.GetOrdinal("ID")),
                                     reader.GetString(reader.GetOrdinal("Email")),
                                     reader.GetString(reader.GetOrdinal("Password")),
                                     reader.GetString(reader.GetOrdinal("FullName"))
                                    );
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

            return userToReturn;
        }

        public void Delete( int ID )
        {
            try
            {
                using ( SqlConnection con = DBConnection )
                {
                    con.Open();
                    using ( SqlCommand cmd = con.CreateCommand() )
                    {
                        cmd.CommandText = $"DELETE FROM Users WHERE ID = {ID}";
                        cmd.ExecuteNonQuery();
                    }

                }
            }
            catch ( Exception ex )
            {

                throw ex;
            }
        }
    }
}