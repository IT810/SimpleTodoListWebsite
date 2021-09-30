using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyTasks.Models;
using MyTasks.MiddleWare;

namespace MyTasks.Controllers
{
    public class HomeController : Controller
    {
        private MyDBEntities db = new MyDBEntities();

        [LoginVerification]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var user = db.Users.SingleOrDefault(x => x.Email == email.Trim() && x.Password == password.Trim());
            if(user != null) {
                Session["uid"] = user.Id;
                Session["uname"] = user.Name;
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}