using AluminiService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AluminiWebApp.Areas.Admin.Controllers
{
    public class JobsController : BaseController
    {
        public JobsController(IUserDetailsViewService _userDetailsViewService, IUserInfoService _userInfoService, IEventService _eventServices, IEventTicketTypesServices _eventTicketTypeservices, IGenericMethodsService _genericMetodsservices, IUserPaymentService _userPaymentsService, IUser_JobPostingService _userJobPostingservice, IUserPostService _userPostService, INewsRoomService _newsRoomservice, IMemoriesServices _memoriesservice, IDonationService _donationservice, IAlbumGalleryService _albumGalleryserice)
            : base(_userDetailsViewService, _userInfoService, _eventServices, _eventTicketTypeservices, _genericMetodsservices, _userPaymentsService, _userJobPostingservice, _userPostService, _newsRoomservice, _memoriesservice, _donationservice, _albumGalleryserice)
        {

        }
        [HttpGet]
        public ActionResult PendingJobs()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Jobs(int Status)
        {
            var data = UserJobPostingservice.GetPendingApprovals(Status);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult UpdateJobs(int Jobid, int Status)
        {
            UserJobPostingservice.UpdateJobs(Jobid, Status);
            var data = UserJobPostingservice.GetPendingApprovals(2);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetApprovedJobs()
        {
            var Data = UserJobPostingservice.GetJobs();
            return Json(Data, JsonRequestBehavior.AllowGet);
        }
    }
}
