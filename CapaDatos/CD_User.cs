using CapaModels;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CapaDatos
{
    public class CD_User : connection
    {
        public bool VerifyPassword(string enteredPassword, byte[] storedPassword, byte[] salt)
        {
            // Encripta la contraseña ingresada por el usuario utilizando la misma clave y sal utilizada en SQL Server
            byte[] enteredPasswordBytes = EncryptPassword(enteredPassword, salt);

            // Compara la contraseña encriptada del usuario con la contraseña almacenada
            return CompareByteArrays(enteredPasswordBytes, storedPassword);
        }

        public bool login(string user, string pass)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT Pass, Salt FROM Users WHERE Name = @user";
                    command.Parameters.AddWithValue("@user", user);
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        // Obtiene la contraseña encriptada y la sal almacenada en la base de datos
                        byte[] storedPassword = (byte[])reader["Pass"];
                        byte[] salt = (byte[])reader["Salt"];

                        // Verifica la contraseña ingresada por el usuario
                        if (VerifyPassword(pass, storedPassword, salt))
                        {
                            // Autenticación exitosa
                            return true;
                        }
                    }
                }
            }
            // Autenticación fallida
            return false;
        }

        // Función para encriptar la contraseña del usuario
        public byte[] EncryptPassword(string password, byte[] salt)
        {
            using (Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = deriveBytes.GetBytes(aesAlg.KeySize / 8);
                    aesAlg.IV = deriveBytes.GetBytes(aesAlg.BlockSize / 8);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            using (StreamWriter sw = new StreamWriter(cs))
                            {
                                sw.Write(password);
                            }
                        }
                        return ms.ToArray();
                    }
                }
            }
        }

        // Función para comparar dos matrices de bytes
        public bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1 == null || array2 == null || array1.Length != array2.Length)
                return false;

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                    return false;
            }
            return true;
        }
    }
}
