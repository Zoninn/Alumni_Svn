﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AluminiWebApp.Controllers
{
    public class ErrorController : Controller
    {
        
        public ActionResult ErrorPage()
        {
            return View();
        }
    }
}