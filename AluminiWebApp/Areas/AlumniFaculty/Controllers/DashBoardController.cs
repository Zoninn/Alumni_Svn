using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AluminiWebApp.Areas.AlumniFaculty.Controllers
{
    public class DashBoardController : Controller
    {
        // GET: AlumniFaculty/DashBoard
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}