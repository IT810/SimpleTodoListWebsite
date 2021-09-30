using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyTasks.MiddleWare
{
    public class LoginVerification : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["uid"] == null)
            {
                filterContext.Result = new RedirectResult("~/Home/Login");
                return;
            }
        }
    }
}