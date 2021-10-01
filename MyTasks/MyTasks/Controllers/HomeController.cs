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
            TempData["lo"] = 1;
            return View();
        }

        [LoginVerification]
        public ActionResult DeleteHistory()
        {
            TempData["lo"] = 2;
            return View();
        }

        [LoginVerification]
        public ActionResult GetAllDeleted()
        {
            db.Configuration.ProxyCreationEnabled = false;
            int id = (int)Session["uid"];
            var tasks = db.Tasks.Where(x => x.IsDelete == true && x.UserId == id).OrderByDescending(x => x.DeletedDate).ToList();
            return Json(tasks, JsonRequestBehavior.AllowGet);
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

        public ActionResult Logout()
        {
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Login");
        }

        [LoginVerification]
        public ActionResult GetAllTasks()
        {
            db.Configuration.ProxyCreationEnabled = false;
            int id = (int)Session["uid"];
            var tasks = db.Tasks.Where(x => x.IsDelete == false && x.UserId == id).OrderByDescending(x => x.Id).ToList();
            return Json(tasks, JsonRequestBehavior.AllowGet);
        }

        [LoginVerification]
        [HttpPost]
        public ActionResult AddTask(Task t)
        {
            int id = (int)Session["uid"];
            Task task = new Task();
            task.Description = t.Description;
            task.IsDelete = false;
            task.IsDone = false;
            task.UserId = id;
            db.Tasks.Add(task);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [LoginVerification]
        public ActionResult UpdateTask(Task t)
        {
            Task task = db.Tasks.SingleOrDefault(x => x.Id == t.Id);
            task.Description = t.Description;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [LoginVerification]
        public ActionResult UpdateStatus(int Id)
        {
            Task task = db.Tasks.SingleOrDefault(x => x.Id == Id);
            if(task.IsDone == true)
            {
                task.IsDone = false;
            }
            else
            {
                task.IsDone = true;
            }
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [LoginVerification]
        public ActionResult DeleteTask(int Id)
        {
            Task task = db.Tasks.SingleOrDefault(x => x.Id == Id);
            task.IsDelete = true;
            task.DeletedDate = DateTime.Now;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [LoginVerification]
        public ActionResult Undo(int Id)
        {
            Task task = db.Tasks.SingleOrDefault(x => x.Id == Id);
            task.IsDelete = false;
            task.DeletedDate = null;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [LoginVerification]
        public ActionResult DeleteCompletely(int Id)
        {
            Task task = db.Tasks.SingleOrDefault(x => x.Id == Id);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}