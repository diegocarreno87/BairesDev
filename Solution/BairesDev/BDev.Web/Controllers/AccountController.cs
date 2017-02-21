using BDev.Services;
using BDev.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace BDev.Web.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Logon()
        {
            return View(new LogonModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Logon(LogonModel model)
        {
            IUserService client = new UserService();
            if (client.ValidateUserPassword(model.username, model.password))
            {
                var ident = new ClaimsIdentity(
                  new[] {
              new Claim(ClaimTypes.NameIdentifier, model.username),
              new Claim(ClaimTypes.Name,model.username),
              },
                  DefaultAuthenticationTypes.ApplicationCookie);

                HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);
                return RedirectToAction("Index", "Employees"); // auth succeed 
            }
            else
            {
                LogonModel newModel = new LogonModel();
                newModel.ValidationMessage = "Invalid Username/Password";
                return View(newModel);
            }
        }
    }
}