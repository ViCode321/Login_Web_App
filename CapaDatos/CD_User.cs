using CapaModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_User:connection
    {

        public bool login(string user, string pass)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select * from Users where Name = @user and Pass= @pass";
                    command.Parameters.AddWithValue("@user", user);
                    command.Parameters.AddWithValue("@pass", pass);
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            User.IdUser = reader.GetInt32(0);
                            User.Name = reader.GetString(1);
                            User.Pass = reader.GetString(2);
                            User.Email = reader.GetString(3);
                        }
                        return true;
                    }
                    else return false;
                }
            }
        }
    }
}
