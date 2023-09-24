using CapaModels;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_User : connection
    {
        // Clave secreta utilizada para la encriptación y desencriptación
        private const string SecretPassphrase = "YourSecretPassphrase";

        public bool Login(string username, string password)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    // Consulta SQL para incluir la desencriptación
                    command.CommandText = "SELECT Name, CONVERT(NVARCHAR(100), DECRYPTBYPASSPHRASE(@SecretPassphrase, Pass)) AS DecryptedPassword, Email FROM Users WHERE Name = @username";
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@SecretPassphrase", SecretPassphrase);
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        // Obtiene la contraseña desencriptada directamente de la base de datos
                        string decryptedPassword = reader["DecryptedPassword"].ToString();

                        // Verifica la contraseña ingresada por el usuario
                        if (string.Equals(password, decryptedPassword))
                        {
                            // Autenticación exitosa
                            //User.IdUser = Convert.ToInt32(reader["Id"]);
                            Users.Name = reader["Name"].ToString();
                            Users.Email = reader["Email"].ToString();
                            //User.Pass = decryptedPassword;

                            return true;
                        }
                    }
                }
            }
            // Autenticación fallida
            return false;
        }

        public string GetEmailAndUsername(string username)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT Email, Name FROM Users;";
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Users.Name = reader["Name"].ToString();
                        return reader["Email"].ToString();
                    }
                }
            }
            return null; // El usuario no se encontró o no tiene un correo asociado
        }

        public void UpdatePassword(string newPassword)
        {
            string username = Users.Name;
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    // Actualiza la contraseña en la base de datos
                    command.CommandText = "UPDATE Users SET Pass = ENCRYPTBYPASSPHRASE(@SecretPassphrase, @newPassword) WHERE Name = @username";
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@SecretPassphrase", SecretPassphrase);
                    command.Parameters.AddWithValue("@newPassword", newPassword);
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }


    }
}
