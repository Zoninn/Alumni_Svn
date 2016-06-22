using Alumini.Core;
using AluminiRepository;
using AluminiService.Interfaces;
using AluminiWebApp.Areas.Alumini.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AluminiWebApp.Areas.Alumini.Controllers
{
    public class JobsController : BaseController
    {
        public JobsController(IUserDetailsViewService userDetailsViewService, IUserInfoService userInfoService, IEventCategoryService eventCategoryservices, IUserPostService userpostService, IUserPostPicturesService userPostPictureservice, IUserPostVisibleService userPostVisibleServices, IGenericMethodsService genericMethods, IUserSelectionEventsService userSelectionServices, IUserselectionBookingsService userselectionBookingServices, IUserPaymentService userPaymentservice, IUser_JobPostingService _userJobPostingservice, IEventService _eventService, INewsRoomService _newsRoomService, IMemoriesServices _mermoriesServices, IDonationService _donationService, IProfessionalDetailsService _ProfessionalDetails, ISaluationService _SaluationServices, IEducationalDetailService _educationalDetailService, ICourseCategoryService _courseCategoryService, IFacultyWorkInfoService _facultyWorkInfoService)
            : base(userDetailsViewService, userInfoService, eventCategoryservices, userpostService, userPostPictureservice, userPostVisibleServices, genericMethods, userSelectionServices, userselectionBookingServices, userPaymentservice, _userJobPostingservice, _eventService, _newsRoomService, _mermoriesServices, _donationService, _ProfessionalDetails, _SaluationServices, _educationalDetailService, _courseCategoryService, _facultyWorkInfoService)
        {

        }
        [HttpGet]
        public JsonResult Jobs()
        {
            var Data = UserJobPostingservice.GetJobs();
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult JobsAutosearch(string JobTitle, int Status)
       {
            var Data = UserJobPostingservice.GetUserAutocomplete(JobTitle, Status);
            return Json(Data, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetUserpostedJobs()
        {
            var Data = UserJobPostingservice.GetUserPosetdJobs(Convert.ToInt32(Session["UserId"]));
            return Json(Data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult JobPosting()
        {
            if (Session["UserId"] != null)
            {

                return View();
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }

        [HttpPost]
        public ActionResult JobPosting(UserPosting_Jobs Jobs, HttpPostedFileBase File)
        {

            if (Session["UserId"] != null)
            {
                if (Jobs.Jobid != 0)
                {
                    Jobs.Userid = Convert.ToInt64(Session["UserId"].ToString());
                    Jobs.UpdatedOn = DateTime.Now;
                    UserJobPostingservice.UpdateMyPostedJobs(Jobs);
                    TempData["Message"] = "Job details successfully Updated";
                    return RedirectToAction("MyPostedJobs", "Jobs", new { area = "Alumini" });

                }
                else
                {
                    var FilePath = "";
                    if (File != null)
                    {
                        var fileName = Path.GetFileName(File.FileName);
                        var path = Path.Combine(Server.MapPath("~/UserPostingImages/" + fileName));
                        File.SaveAs(path);
                        FilePath = "/UserPostingImages/" + fileName;
                    }
                    Jobs.Userid = Convert.ToInt64(Session["UserId"].ToString());
                    Jobs.CreatedOn = DateTime.Now;
                    Jobs.Status = 0;
                    Jobs.Filepath = FilePath;
                    UserJobPostingservice.Create(Jobs);
                    TempData["Message"] = "you successfully posted the job and awating for the admin approval..";
                }
                return RedirectToAction("JobPosting", "Jobs", new { area = "Alumini" });
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }

        [HttpGet]
        public JsonResult FunctionalAreas()
        {
            var data = GenericMethods.FunactionalAreasforjobs();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ViewJobs()
        {
            if (Session["UserId"] != null)
            {
                var model = UserJobPostingservice.GetJobs();
                return View(model);
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }

        [HttpPost]
        public ActionResult ViewJobs(string JobTitle, string ComapnyName, string Skills)
        {
            if (Session["UserId"] != null)
            {
                var model = UserJobPostingservice.GetJobsonserach(JobTitle, ComapnyName, Skills);
                return View(model);
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }

        [HttpGet]
        public JsonResult GetJobsonid(int id)
        {
            var data = UserJobPostingservice.GetJobsbyId(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SingleJob(int id)
        {
            if (Session["UserId"] != null)
            {
                JobsModel Jobs = new JobsModel()
                {
                    singleJobs = UserJobPostingservice.GetJobsbyId(id)
                };
                return View(Jobs);
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }
        [HttpGet]
        public ActionResult MyPostedJobs()
        {
            if (Session["UserId"] != null)
            {
                var model = UserJobPostingservice.GetUserPosetdJobs(Convert.ToInt32(Session["UserId"].ToString()));
                return View(model);
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }
        [HttpGet]
        public JsonResult Myjobs()
        {
            var data = UserJobPostingservice.MyPostedJobs(Convert.ToInt32(Session["UserId"].ToString()));
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteMyJob(int Jobid)
        {
            int Status = 3;
            UserJobPostingservice.UpdateJobs(Jobid, Status);
            var Data = UserJobPostingservice.GetJobs();
            return Json(Data, JsonRequestBehavior.AllowGet);
        }
        public class Skillsserach : UserJobPostings
        {
            public string JobTitle { get; set; }
            public string ComapnyName { get; set; }
            public string Skills { get; set; }
        }

    }
}
