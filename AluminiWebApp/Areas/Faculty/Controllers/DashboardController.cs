using Alumini.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AluminiWebApp.Areas.Faculty.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
            }
        }
    }
}