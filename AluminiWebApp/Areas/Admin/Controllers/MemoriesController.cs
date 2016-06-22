using AluminiService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AluminiWebApp.Areas.Admin.Controllers
{
    public class MemoriesController : BaseController
    {
        public MemoriesController(IUserDetailsViewService _userDetailsViewService, IUserInfoService _userInfoService, IEventService _eventServices, IEventTicketTypesServices _eventTicketTypeservices, IGenericMethodsService _genericMetodsservices, IUserPaymentService _userPaymentsService, IUser_JobPostingService _userJobPostingservice, IUserPostService _userPostService, INewsRoomService _newsRoomservice, IMemoriesServices _memoriesservices, IDonationService _donationservice, IAlbumGalleryService _albumGalleryserice)
            : base(_userDetailsViewService, _userInfoService, _eventServices, _eventTicketTypeservices, _genericMetodsservices, _userPaymentsService, _userJobPostingservice, _userPostService, _newsRoomservice, _memoriesservices, _donationservice, _albumGalleryserice)
        {

        }
        [HttpGet]
        public ActionResult Memories()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetMemories()
        {
            var Data = Memoriesservices.GetMemoriesforAdmin(0);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDeletedMemories()
        {
            var Data = Memoriesservices.GetDeleted();
            return Json(Data, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult DeleteMemories(int id)
        {
            Memoriesservices.Delete(id);
            var Data = Memoriesservices.GetMemoriesforAdmin(0);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult AlumniMemories(int? id)
        {
            return View();
        }


        [HttpGet]
        public JsonResult SingleMemories(int id)
        {
            
            var Data = Memoriesservices.GetSinglememorys(id);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult Activate(int id)
        {
            Memoriesservices.Update(id);
            var Data = Memoriesservices.GetDeleted();
            return Json(Data, JsonRequestBehavior.AllowGet);
        }
    }
}
