using AluminiWebApp.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AluminiService;
using AluminiService.Interfaces;
using AluminiWebApp.Controllers;
using Alumini.Core;
using Ninject;
using AluminiWebApp.Models;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace AluminiWebApp.Areas.Admin.Controllers
{
    [Authorize]

    public class UserRegistrationsController : BaseController
    {


        public UserRegistrationsController(IUserDetailsViewService _userDetailsViewService, IUserInfoService _userInfoService, IEventService _eventServices, IEventTicketTypesServices _eventTicketTypeservices, IGenericMethodsService _genericMetodsservices, IUserPaymentService _userPaymentsService, IUser_JobPostingService _userJobPostingservice, IUserPostService _userPostService, INewsRoomService _newsRoomservice, IMemoriesServices memoriesservices, IDonationService _donationservice, IAlbumGalleryService _albumGalleryserice)
            : base(_userDetailsViewService, _userInfoService, _eventServices, _eventTicketTypeservices, _genericMetodsservices, _userPaymentsService, _userJobPostingservice, _userPostService, _newsRoomservice, memoriesservices, _donationservice, _albumGalleryserice)
        {

        }
        [HttpPost]
        public ActionResult SerachUsers(List<int> Userids)
        {
            try
            {
                View_UserDetails Users = new View_UserDetails();
                var products = new System.Data.DataTable("teste");
                products.Columns.Add("Id", typeof(int));
                products.Columns.Add("FIrst Name", typeof(string));
                products.Columns.Add("Last Name", typeof(string));
                products.Columns.Add("Email", typeof(string));
                products.Columns.Add("Mobile", typeof(string));
                products.Columns.Add("Course Category", typeof(string));
                products.Columns.Add("Course", typeof(string));
                products.Columns.Add("Year", typeof(string));
                for (int i = 0; i < Userids.Count; i++)
                {
                    Users = UserDetailsViewService.GetUserByUserId(Userids[i]);
                    products.Rows.Add(Users.UserId, Users.FirstName, Users.LastName, Users.Email, Users.PhoneNumber, Users.CourseCategoryName, Users.CourseName, Users.Batch);
                }
                var grid = new GridView();
                grid.DataSource = products;
                grid.DataBind();
                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=AlumniDetails.xls");
                //Response.AddHeader("Content-Type", "application/vnd.ms-excel");
                Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                grid.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            catch (SystemException ex)
            {
                TempData["Error"] = "Please check the users and Export";
                return RedirectToAction("SerachUsers", "UserRegistrations", new { Area = "Admin" });
            }
            return View();
        }


        [HttpPost]
        public JsonResult SENDEmail(int userid, string Message, string Heading, string Subject, int Status)
        {
            var Result = UserDetailsViewService.GetUserByUserId(userid);
            Emails.SendEmails(Result.Email, Message, Result.FirstName + Result.LastName, Heading, Subject);
            var Data = "";
            return Json(Data, JsonRequestBehavior.DenyGet);
        }
        [HttpGet]
        public ActionResult SerachUsers()
        {
            DateTime CurrentDate = DateTime.Now;
            int Year = CurrentDate.Year;
            StudentRegistrationModel userdto = new StudentRegistrationModel()
            {
                Coursecategorys = GenericMetodsservices.GetAllCourseCategories(),
            };
            return View(userdto);
        }

        public JsonResult GetDashboard(int Status)
        {
            var data = GenericMetodsservices.GetdashboardData();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserdetailsonId(int Userid)
        {
            var Data = UserDetailsViewService.GetUserByUserId(Userid);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }
        // GET: Admin/UserRegistrations
        public ActionResult Index()
        {
            List<View_UserDetails> model = new List<View_UserDetails>();
            return View(model);
        }
        // POST: /Account/Register
        [HttpGet]
        public JsonResult GetUsers(int Role, int UserStatus)
        {
            IEnumerable<View_UserDetails> model = UserDetailsViewService.GetAllUserDetailsByUserRoleAndStatus(Role, UserStatus);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateUserProfileStatus(int UserId, int UserStatus)
        {
            UserInfoService.UpdateUserProfileStatus(UserId, UserStatus);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult BindYears()
        {
            DateTime CurrentDate = DateTime.Now;
            int Year = CurrentDate.Year;
            List<Graduation> _GraduationYears = new List<Graduation>();
            for (int i = 1970; i <= Year; i++)
                _GraduationYears.Add(new Graduation { GraduationYear = "" + i, GraduationYearId = i });
            return Json(_GraduationYears, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUsersonserach(string Degree, string Course, int? Batch, int? Userid)
        {
            var data = UserDetailsViewService.GetAllDetailsonserachbyAdmin(Degree, Course, Convert.ToInt32(Batch), Userid);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public class Employee
        {
            public int id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}