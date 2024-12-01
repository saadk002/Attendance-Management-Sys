using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ams.Models;
using System.Web.Security;

namespace Ams.Controllers
{
    public class LoginController : Controller
    {
        FINALEntities1 db = new FINALEntities1();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(user log)
        {
            var user  =  db.users.Where(x => x.username == log.username && x.password == log.password).Count();
            if (user > 0)
            {
                return RedirectToAction("dashboard");
            }
            else
            {
                return View();
            }           
        }
      
        public ActionResult Logout()
        {
        

            return RedirectToAction("Login");
        }
        public ActionResult dashboard()
        {
            return View();
        }
    }
}