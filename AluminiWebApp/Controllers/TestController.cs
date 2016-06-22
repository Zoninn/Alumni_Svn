using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AluminiWebApp.Controllers
{
    public class TestController : Controller
    {

        public ActionResult Index()
        {
            List<Employee> Emp = new List<Employee>();
            Emp.Add(new Employee { id = 1, Name = "ZON" });
            Emp.Add(new Employee { id = 1, Name = "ZON" });
            Emp.Add(new Employee { id = 1, Name = "ZON" });
            Emp.Add(new Employee { id = 1, Name = "ZON" });
            Emp.Add(new Employee { id = 1, Name = "ZON" });

            return View(Emp);
        }
    }
    public class Employee
    {
        public int id { get; set; }
        public string Name { get; set; }
    }
}