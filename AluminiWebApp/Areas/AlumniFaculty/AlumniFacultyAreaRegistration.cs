using System.Web.Mvc;

namespace AluminiWebApp.Areas.AlumniFaculty
{
    public class AlumniFacultyAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AlumniFaculty";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AlumniFaculty_default",
                "AlumniFaculty/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}