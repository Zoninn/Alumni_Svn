using Alumini.Core;
using AluminiRepository;
using AluminiRepository.Interfaces;
using AluminiService.Interfaces;
using AluminiWebApp.Areas.Admin.Models;
using AluminiWebApp.Areas.Alumini.Models;
using AluminiWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AluminiWebApp.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUserDetailsViewService _userDetailsViewService, IUserInfoService _userService, ISaluationService _saluationService, IStateDistrictCityService _statecitydistrictService, ICourseCategoryService _courseCategoryservice, ICourseService _courseServices, IEducationalDetailService _educationalDetailService, IFacultyWorkInfoService _facultyWorkInfoService, IGraduationYearService _graduationyearservice, IProfessionalDetailsService _Professionaldetailsservice, IGenericMethodsService _genericMethodsservices, IUser_JobPostingService _userJobPostingservice, IEventService _eventServices, INewsRoomService _newsroomService, IDonationService _donationService)
            : base(_userService, _saluationService, _statecitydistrictService, _courseCategoryservice, _courseServices, _educationalDetailService, _facultyWorkInfoService, _graduationyearservice, _Professionaldetailsservice, _genericMethodsservices, _userDetailsViewService, _userJobPostingservice, _eventServices, _newsroomService, _donationService)
        {
        }



        public JsonResult GetProfDetails(string Auto, string Designation)
        {
            var Data = Professionaldetailsservice.GetCompanyDetailsforAutosearch(Auto, Designation);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                Session.Remove("UserId");
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            EventsModel Model = new EventsModel()
            {
                UserNews = NewsroomService.GetNews(),
                UserJobs = UserJobPostingservice.GetJobs().ToList().Take(6),
                DisplayHome = EventServices.GetEventsforHome(),
                Activities = GenericMethodsservices.GetActivities()

            };

            return View(Model);
        }
        [HttpGet]
        public ActionResult Conatctus()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Conatctus(Alumni_ContactUs Contactus)
        {
            try
            {
                Contactus.CreatedOn = DateTime.Now;
                Contactus.Status = true;
                GenericMethodsservices.ContactUs(Contactus);
                Emails.ContactUs(Contactus.Email, Contactus.Mobile, Contactus.Message, Contactus.Name);
                TempData["Message"] = "Thanks for contact us.we will respond shortly..";
                return RedirectToAction("Conatctus", "Home", new { Area = "" });

            }
            catch (SystemException ex)
            {

            }
            return View();
        }

        public JsonResult GetUserImages()
        {
            var Data = UserDetailsViewService.GetAllUsers();
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Members()
        {
            UserMembersModel users = new UserMembersModel()
            {
                GetUsers = UserDetailsViewService.GetAllUsers()

            };

            return View(users);
        }

        public ActionResult Gallery(int? id)
        {
            Gallery Model = new Gallery()
            {
                Galleries = GenericMethodsservices.UserGallery(id)
            };

            return View(Model);
        }

        public ActionResult Aboutus()
        {
            return View();
        }

        public ActionResult AllEvents()
        {
            MyEventsModel events = new MyEventsModel()
            {

                UserPurchasedEvents = GenericMethodsservices.GetAdminEvents()
            };
            return View(events);
        }


        public ActionResult Test2()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult ExecutiveBoard()
        {
            ExecutiveBoard Exe = new ExecutiveBoard()
            {
                ExecutiveBoardDetail = EventServices.ExecutiveBoardList(),
            };

            ViewBag.Message = "Your application description page.";

            return View(Exe);
        }

        [HttpGet]
        public ActionResult ProfileCompleted()
        {
            return View();
        }

        [HttpPost]
        [ActionName("ProfileCompleted")]
        public ActionResult UpdateUserProfile()
        {
            return RedirectToAction("BasicInformation", "", new { area = "" });
        }
        [HttpGet]
        public ActionResult ExtLogin()
        {
            return RedirectToAction("UserRegistration", "Account");
        }
        public ActionResult Test1()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult UpdateProfile()
        {
            return View();
        }
        [HttpGet]
        public ActionResult BasicInformation()
        {
            RegistrationStep2ViewModel RegisterDTO = new RegistrationStep2ViewModel();
            return View(RegisterDTO);
        }
        [HttpPost]
        public ActionResult BasicInformation(RegistrationStep2ViewModel RegisterDTO, HttpPostedFileBase ProfilePicture)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }

        [HttpGet]
        public JsonResult Jobs()
        {
            var Data = UserJobPostingservice.GetJobs();
            return Json(Data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult JobDetails(int id)
        {
            var Jobs = UserJobPostingservice.Get(id);
            if (id != null)
            {
                Session["Jobid"] = id;
            }
            JobPosting Jobsposted = new JobPosting()
            {
                JobTitle = Jobs.JobTitle,
                JobId = Jobs.Jobid

            };
            return View(Jobsposted);
        }
        [HttpGet]
        public JsonResult Events()
        {
            var Data = EventServices.GetEVents();
            return Json(Data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult EventGoing(int id)
        {
            Def_Events events = EventServices.Get(id);
            Session["Jobid"] = id;
            EventsModel Events = new EventsModel()
            {

                Eventid = events.EventId,
                EventAddress = events.EventVenue,
                EventDesc = events.EventDescription,
                EventHeading = events.EventName,
                EventsGoing = EventServices.GetGoingUsers(id),
                StartDate = events.EventStartdate,
                EndDate = events.EndDate,
                Bannerimage = events.BannerImage,
                StartTime = events.StartTime,
                EndTime = events.EndTime
            };
            return View(Events);
        }

        [HttpGet]
        public JsonResult GetNews()
        {
            var Data = NewsroomService.GetNews();
            return Json(Data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult News(int id)
        {
            var Data = NewsroomService.Get(id);
            NewsRoomModel Model = new NewsRoomModel()
            {
                Description = Data.Description,
                Title = Data.Title,
                NewsId = Data.NewRoomId,
                Image = Data.Image,
                PostedOn = Data.CreatedOn

            };

            return View(Model);
        }

        [HttpGet]
        public ActionResult Donations()
        {
            List<Donation_Details> data = GenericMethodsservices.GetAdminDonations();
            return View(data);

        }

    }

}