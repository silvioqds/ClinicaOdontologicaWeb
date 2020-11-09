using ClinicaOdontologica.Desfalcados.Helpers;
using ClinicaOdontologica.Desfalcados.Models;
using ClinicaOdontologica.Desfalcados.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        [ActionName("Index")]
        public ActionResult Authentication(AuthenticationViewModel login)
        {

            Users user = new Users();

            using (ClinicaEntities db = new ClinicaEntities())
            {
                user = db.Users.FirstOrDefault(x => x.Username == login.username.Trim());

                if (user is null)
                {
                    ModelState.AddModelError("", "Usuario Inexistente");
                    return View(new AuthenticationViewModel());
                }

                string senha = EncriptKey.AcertaSenha(login.username, login.password);

                if (!user.Password.Equals(senha))
                {
                    ModelState.AddModelError("", "Usuario e/ou senha inválido(os)");
                    return View(new AuthenticationViewModel());
                }


                if (!user.active)
                {
                    ModelState.AddModelError("", "Usuário Desativado");
                    return View(new AuthenticationViewModel());
                }


            }

            var identity = new ClaimsIdentity(new[]
                                {
                                new Claim(ClaimTypes.Name, user.Username),
                                new Claim("USUARIOLOGADO", user.UsuarioID.ToString()),
                                }, "ApplicationCookie");

            Request.GetOwinContext().Authentication.SignIn(identity);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Login");
        }

    }
}