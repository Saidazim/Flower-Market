using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FlowerMarket.Models;
using FlowerMarket.DataAccess;

namespace FlowerMarket.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {

        [HttpPost]
        public ActionResult SignUp(Client model)
        {
            ClientManager manager = new ClientManager();
            manager.New_Client(model);
            return RedirectToAction("Login");
        }
        
        public ActionResult SignUp()
        {
            return View();
        }
        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Login(Client model)
        {
            ClientManager manager = new ClientManager();  
            if (manager.UserExist(model))
            {
                FormsAuthentication.SetAuthCookie(model.Username, false);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "User with this login and password does not exist");
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
    }
}
