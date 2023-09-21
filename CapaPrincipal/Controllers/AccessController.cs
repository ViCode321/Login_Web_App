using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_Application.Controllers
{
    public class AccessController : Controller
    {
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
    }
}