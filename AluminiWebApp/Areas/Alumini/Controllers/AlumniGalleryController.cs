using Alumini.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AluminiWebApp.Areas.Alumini.Controllers
{
    public class AlumniGalleryController : Controller
    {
        [HttpGet]
        public ActionResult Gallery()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });

        }
    }
}