using Alumini.Core;
using AluminiService.Interfaces;
using AluminiWebApp.Areas.Alumini.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AluminiWebApp.Areas.Faculty.Controllers
{
    public class JobsController : BaseController
    {
        public JobsController(IUserDetailsViewService userDetailsViewService, IUserInfoService userInfoService, IEventCategoryService eventCategoryservices, IUserPostService userpostService, IUserPostPicturesService userPostPictureservice, IUserPostVisibleService userPostVisibleServices, IGenericMethodsService genericMethods, ICourseCategoryService coursecategoryservices, IUserSelectionEventsService userSelectionServices, IUserselectionBookingsService userselectionBookingServices, IUserPaymentService userPaymentservice, IUser_JobPostingService _userJobPostingservice, IEventService _eventService, INewsRoomService _newsRoomService, IMemoriesServices _mermoriesServices)
            : base(userDetailsViewService, userInfoService, eventCategoryservices, userpostService, userPostPictureservice, userPostVisibleServices, genericMethods, coursecategoryservices, userSelectionServices, userselectionBookingServices, userPaymentservice, _userJobPostingservice, _eventService, _newsRoomService, _mermoriesServices)
        {

        }
        [HttpGet]
        public JsonResult Jobs()
        {
            var Data = UserJobPostingservice.GetJobs();
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MyPostedJobs()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }

        [HttpGet]
        public ActionResult SingleJob(int id)
        {
            JobsModel Jobs = new JobsModel()
            {
                singleJobs = UserJobPostingservice.GetJobsbyId(id)
            };

            return View(Jobs);
        }
        [HttpGet]
        public ActionResult ViewJobs()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
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

        [HttpGet]
        public JsonResult FunctionalAreas()
        {
            var data = GenericMethods.FunactionalAreasforjobs();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetJobsonid(int id)
        {
            var data = UserJobPostingservice.GetJobsbyId(id);
            return Json(data, JsonRequestBehavior.AllowGet);
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
                    return RedirectToAction("MyPostedJobs", "Jobs", new { area = "Faculty" });

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

                return RedirectToAction("JobPosting", "Jobs", new { area = "Faculty" });
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
    }
}
