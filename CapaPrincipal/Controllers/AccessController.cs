using CapaDatos;
using System.Web.Mvc;

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
        public ActionResult Send_pass()
        {
            return View();
        }

        public ActionResult New_pass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            bool isAuthenticated = _cdUser.login(username, password);
            if (isAuthenticated)
            {
                // Aquí puedes realizar alguna acción después de que el usuario se haya autenticado correctamente.
                // Por ejemplo, redirigirlo a una página de inicio o guardar información en una cookie de sesión.
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Credenciales inválidas. Inténtelo de nuevo.";
                return View();
            }
        }
    }
}
