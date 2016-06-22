using Alumini.Core;
using AluminiRepository;
using AluminiService.Interfaces;
using AluminiWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AluminiWebApp.Areas.Alumini.Controllers
{
    public class AlumniMembersController : BaseController
    {
        public AlumniMembersController(IUserDetailsViewService userDetailsViewService, IUserInfoService userInfoService, IEventCategoryService eventCategoryservices, IUserPostService userpostService, IUserPostPicturesService userPostPictureservice, IUserPostVisibleService userPostVisibleServices, IGenericMethodsService genericMethods, IUserSelectionEventsService userSelectionServices, IUserselectionBookingsService userselectionBookingServices, IUserPaymentService userPaymentservice, IUser_JobPostingService _userJobPostingservice, IEventService _eventService, INewsRoomService _newsRoomService, IMemoriesServices _mermoriesServices, IDonationService _donationService, IProfessionalDetailsService _ProfessionalDetails, ISaluationService _SaluationServices, IEducationalDetailService _educationalDetailService, ICourseCategoryService _courseCategoryService, IFacultyWorkInfoService _facultyWorkInfoService)
            : base(userDetailsViewService, userInfoService, eventCategoryservices, userpostService, userPostPictureservice, userPostVisibleServices, genericMethods, userSelectionServices, userselectionBookingServices, userPaymentservice, _userJobPostingservice, _eventService, _newsRoomService, _mermoriesServices, _donationService, _ProfessionalDetails, _SaluationServices, _educationalDetailService, _courseCategoryService, _facultyWorkInfoService)
        {

        }
        [HttpGet]
        public ActionResult Members()
        {
            if (Session["UserId"] != null)
            {
                UserMembersModel users = new UserMembersModel()
                {
                    GetUsers = UserDetailsViewService.GetAllUsers(),
                    Coursecategorys = CourseCategoryService.GetAllCourseCategories(),
                };
                return View(users);
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });

        }

        public JsonResult GetUsersBySearch(string ComanyName, int id)
        {
            List<View_UserDetails> Data = new List<View_UserDetails>();
            Data = ProfessionalDetailsservice.GetUserDetails(ComanyName, id);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Gallery(int id)
        {
            if (Session["UserId"] != null)
            {
                Gallery Model = new Gallery()
                {
                    Galleries = GenericMethods.UserGallery(id)
                };

                return View(Model);
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }
        [HttpGet]
        public ActionResult Aboutus()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }

        public JsonResult SearchBy(int id)
        {

            List<Companys> Data = new List<Companys>();

            Data = ProfessionalDetailsservice.GetAllCompanys(id);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetsearchbasedonCourse(string Coursecategory, string Course, string Year)
        {
            var Data = UserDetailsViewService.GetUserserachoncourse(Coursecategory, Course, Year);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ExecutiveBoard()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }
        [HttpGet]
        public JsonResult EmailSearch(string Email, int id)
        {
            var Data = UserDetailsViewService.GetEmailsonEmailsearch(Email, id);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult EmailSearchandDesignation(string Email, int id,string Designation)
        {
            var Data = ProfessionalDetailsservice.GetUserDetailsonSearchbased(Email, id, Designation);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

    }
}