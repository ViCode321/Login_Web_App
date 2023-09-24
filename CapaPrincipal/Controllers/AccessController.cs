using CapaDatos;
using System.Text;
using System;
using System.Web.Mvc;
using CapaPrincipal;
using Microsoft.Ajax.Utilities;
using CapaModels;

namespace Web_Application.Controllers
{
    public class AccessController : Controller
    {
        private readonly CD_User _cdUser;

        public AccessController()
        {
            _cdUser = new CD_User();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Create_code()
        {
            return View();
        }

        public ActionResult Send_pass()
        {
            return View();
        }

        public ActionResult New_pass()
        {
            return View();
        }

        // Login
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            bool isAuthenticated = _cdUser.Login(username, password);
            if (isAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Contraseña o Usuario incorrecto. Inténtelo de nuevo.";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create_code(FormCollection form, EmailService emailService)
        {
            string action = form["action"];

            if (action == "Cancelar")
            {
                // Redirige al usuario de regreso a la página de inicio de sesión
                return RedirectToAction("Login", "Access");
            }
            else if (action == "Aceptar")
            {
                try
                {
                    // Obtiene el correo electrónico del usuario
                    string email = _cdUser.GetEmailAndUsername(Users.Name);

                    if (string.IsNullOrEmpty(email))
                    {
                        ViewBag.Error = "No se pudo encontrar el correo electrónico del usuario.";
                        return View();
                    }

                    // Almacena el código generado en una propiedad

                    // Envía el código por correo electrónico y verifica si se envió con éxito
                    bool envioExitoso = emailService.EnviarCodigoPorCorreo(email, Users.savecode.ToString());

                    if (envioExitoso)
                    {
                        ViewBag.Error = "Error al enviar el código por correo electrónico. Inténtelo de nuevo.";
                        return View();
                    }
                    else
                    {
                        // Redirige al usuario a la página Send_pass.cshtml
                        return RedirectToAction("Send_pass", "Access");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Error al enviar el código por correo electrónico: " + ex.Message;
                    return View();
                }
            }
            return View(); // Retornar la vista actual si no se presionó ningún botón o se presionó uno desconocido
        }

        [HttpPost]
        public ActionResult Send_pass(string code)
        {
            // Obtiene el código almacenado en la propiedad
            int codigoGenerado = Users.savecode;

            // Convierte el código ingresado por el usuario a un número entero
            if (int.TryParse(code, out int codigoIngresado))
            {
                // Compara el código ingresado con el código almacenado en la propiedad
                if (codigoIngresado == codigoGenerado)
                {
                    // El código es válido, permite que el usuario restablezca la contraseña
                    return RedirectToAction("New_pass", "Access");
                }
            }
            // El código es inválido o no coincide, muestra un mensaje de error
            ViewBag.Error = "Código de activación inválido. Inténtelo de nuevo.";
            return View();
        }
        //Actualizar la contraseña del usuario
        [HttpPost]
        public ActionResult New_pass(string new_password, string conf_password)
        {
            // Verifica si la nueva contraseña y la confirmación coinciden
            if (new_password != conf_password)
            {
                ViewBag.Error = "La nueva contraseña y la confirmación no coinciden.";
                return View();
            }

            try
            {
                _cdUser.UpdatePassword(new_password);

                // Después de actualizar la contraseña, puedes redirigir al usuario a una página de inicio de sesión o a donde desees.
                return RedirectToAction("Login", "Access");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al actualizar la contraseña: " + ex.Message;
                return View();
            }
        }

    }
}
