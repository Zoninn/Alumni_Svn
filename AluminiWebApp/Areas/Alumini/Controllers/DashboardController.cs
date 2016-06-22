using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AluminiWebApp.Areas.Alumini.Controllers
{
    public class DashboardController : Controller
    {
       //For Only Alumni
        public ActionResult Index()
        {
            return View();
        }    
    }
}