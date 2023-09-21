using CapaService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Web.Mvc;

public class AuthController : Controller
{
    private readonly CS_User _userService;

    public AuthController(IConfiguration configuration)
    {
        _userService = new CS_User(configuration);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return (IActionResult)View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        // Aquí verifica las credenciales utilizando _userService
        bool isAuthenticated = _userService.Login(username, password);

        if (isAuthenticated)
        {
            // Usuario autenticado, realiza alguna acción (por ejemplo, redirige a la página de inicio).
            return (IActionResult)RedirectToAction("Index", "Home");
        }
        else
        {
            // Autenticación fallida, muestra un mensaje de error.
            ViewBag.Error = "Credenciales inválidas.";
            return (IActionResult)View();
        }
    }
}
