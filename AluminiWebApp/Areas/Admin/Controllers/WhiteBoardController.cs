
using AluminiService.Interfaces;
using AluminiWebApp.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AluminiWebApp.Areas.Admin.Controllers
{
    public class WhiteBoardController : BaseController
    {
        public WhiteBoardController(IUserDetailsViewService _userDetailsViewService, IUserInfoService _userInfoService, IEventService _eventServices, IEventTicketTypesServices _eventTicketTypeservices, IGenericMethodsService _genericMetodsservices, IUserPaymentService _userPaymentsService, IUser_JobPostingService _userJobPostingservice, IUserPostService _userPostService, INewsRoomService _newsRoomservice, IMemoriesServices memoriesservices, IDonationService _donationservice, IAlbumGalleryService _albumGalleryserice)
            : base(_userDetailsViewService, _userInfoService, _eventServices, _eventTicketTypeservices, _genericMetodsservices, _userPaymentsService, _userJobPostingservice, _userPostService, _newsRoomservice, memoriesservices, _donationservice, _albumGalleryserice)
        {

        }
        [HttpGet]
        public JsonResult GetUserPostForAdmin()
        {
            var Data = GenericMetodsservices.UserPostedFulldetailsforAdminDashBorad().Take(10);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult WhiteBoard()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetUserPosts()
        {
            var data = GenericMetodsservices.UserPostedFulldetailsforAdmin();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult deletePosts(int Id)
        {
            UserPostService.Update(Id);
            var data = GenericMetodsservices.UserPostedFulldetailsforAdmin();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EventDetails(int Id)
        {

            return View(GenericMetodsservices.UserPostedFulldetails(Id).FirstOrDefault());

        }

    }
}
