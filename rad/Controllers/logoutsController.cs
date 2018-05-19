using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rad.Controllers
{
    public class logoutsController : Controller
    {
        // GET: logouts
        public ActionResult Index()
        {
            Session.Remove("UserName");
            return RedirectToAction("index", "home");
            //return View();
        }
    }
}


