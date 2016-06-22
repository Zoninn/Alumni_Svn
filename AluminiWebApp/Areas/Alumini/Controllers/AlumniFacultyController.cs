using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AluminiWebApp.Areas.Faculty.Controllers
{
    public class AlumniFacultyController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}