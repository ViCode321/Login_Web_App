using CapaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace CapaPrincipal
{
    // Clase en la capa de aplicación
    public class EmailService
    {
        const string USER = "correo@gmail.com";
        const string PASSWORD = "1234";

        public void EnviarCorreo(StringBuilder message, DateTime dateTime, string of, string by, string affair, out string Error)
        {
            Error = "";
            try
            {
                message.Append(Environment.NewLine);
                message.Append(string.Format("\nEste correo ha sido enviado el día {0:dd/MM/yyyy} a las {0:HH:mm:ss} Hrs: \n\n", dateTime));
                message.Append(Environment.NewLine);
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(of);
                mail.To.Add(by);
                mail.Subject = affair;
                mail.Body = message.ToString();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(USER, PASSWORD);
                smtp.EnableSsl = true;
                smtp.Send(mail);
                Error = "Envío exitoso";
                Console.WriteLine(Error);
            }
            catch (Exception ex)
            {
                Error = "Error al enviar el correo electrónico: " + ex.Message;
                throw new Exception(Error, ex); // Lanza una excepción con un mensaje más descriptivo
            }

        }

        public static int GenerarCodigo()
        {
            Random random = new Random();

            // Genera un número aleatorio entre 1000 y 9999 (ambos inclusive)
            int codigo = random.Next(1000, 10000);

            return codigo;
        }


        public bool EnviarCodigoPorCorreo(string destinatario, string codigo)
        {
            // Mensaje con el código y otros detalles
            Users.savecode = GenerarCodigo(); // Inicializa el código
            string message = "Hola, " + Users.Name + "\nHemos recibido tu solicitud de recuperación de contraseña para tu cuenta.\nTu código de verificación es: " + Users.savecode;
            string of = USER;
            string by = Users.Email;
            string affair = "Recuperación de Contraseña";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(message);

            try
            {
                // Llama al método para enviar el correo
                EnviarCorreo(stringBuilder, DateTime.Now, USER, destinatario, "Recuperar Contraseña", out string error);

                // Verifica si ocurrió un error al enviar el correo
                if (string.IsNullOrEmpty(error))
                {
                    // El correo se envió con éxito
                    return true;
                }
                else
                {
                    // Ocurrió un error al enviar el correo
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Maneja excepciones
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
    }

}
