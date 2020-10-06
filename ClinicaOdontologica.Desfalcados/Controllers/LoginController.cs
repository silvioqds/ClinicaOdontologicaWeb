using ClinicaOdontologica.Desfalcados.Models;
using ClinicaOdontologica.Desfalcados.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicaOdontologica.Desfalcados.Controllers
{
    public class LoginController : Controller
    {

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Authentication")]
        public ActionResult Authentication(AuthenticationViewModel login)
        {

            using (ClinicaEntities db = new ClinicaEntities())
            {
                Users user = db.Users.FirstOrDefault(x => x.Username == login.username.Trim());

                if(user is null)
                {
                    ModelState.AddModelError("", "Usuario Inexistente");
                    return View(new AuthenticationViewModel());
                }

                if (!user.Password.Equals(login.password))
                {
                    ModelState.AddModelError("", "Usuario e/ou senha inválido(os)");
                    return View(new AuthenticationViewModel());
                }
            }


            return RedirectToAction("Index", "Home");
        }
    }
}