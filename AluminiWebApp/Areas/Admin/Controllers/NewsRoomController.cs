using Alumini.Core;
using AluminiService.Interfaces;
using AluminiWebApp.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
namespace AluminiWebApp.Areas.Admin.Controllers
{
    public class NewsRoomController : BaseController
    {
        public NewsRoomController(IUserDetailsViewService _userDetailsViewService, IUserInfoService _userInfoService, IEventService _eventServices, IEventTicketTypesServices _eventTicketTypeservices, IGenericMethodsService _genericMetodsservices, IUserPaymentService _userPaymentsService, IUser_JobPostingService _userJobPostingservice, IUserPostService _userPostService, INewsRoomService _newsRoomservice, IMemoriesServices memoriesservices, IDonationService _donationservice, IAlbumGalleryService _albumGalleryserice)
            : base(_userDetailsViewService, _userInfoService, _eventServices, _eventTicketTypeservices, _genericMetodsservices, _userPaymentsService, _userJobPostingservice, _userPostService, _newsRoomservice, memoriesservices, _donationservice, _albumGalleryserice)
        {

        }
        [HttpGet]
        public ActionResult PostNews()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostNews(db_NewsRooms NewsRoom, HttpPostedFileBase Image)
        {
            var FilePath = "";

            if (Image != null)
            {
                var fileName = Path.GetFileName(Image.FileName);
                var path = Path.Combine(Server.MapPath("~/UserProfilePictures/" + fileName));
                Image.SaveAs(path);
                 FilePath = "/UserProfilePictures/" + fileName;
            }


            if (NewsRoom.NewRoomId != 0)
            {
                NewsRoom.Image = FilePath;
                NewsRoomService.UpdateNewRooms(NewsRoom);
                TempData["SuccessMessage"] = "News Updated successfully";
            }
            else
            {
                NewsRoom.CreatedOn = DateTime.Now;
                NewsRoom.Image = FilePath;
                NewsRoom.Status = true;
                NewsRoomService.Create(NewsRoom);
                TempData["SuccessMessage"] = "News Added successfully";
            }
            return RedirectToAction("PostNews", "NewsRoom", new { area = "Admin" });
        }
        [HttpGet]
        public JsonResult GetNews(int Status)
        {
            var Data = NewsRoomService.GetNewsRooms(Status);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteNews(int Id)
        {
            NewsRoomService.Delete(Id);
            var Data = NewsRoomService.GetNewsRooms(1);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Edit(int Id)
        {
            var Data = NewsRoomService.Get(Id);
            NewsRoomModel Model = new NewsRoomModel()
            {
                Description = Data.Description,
                Title = Data.Title,
                NewsId = Data.NewRoomId
            };
            return Json(Model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult UpdateNews(int Id)
        {
            NewsRoomService.Update(Id);
            var Data = NewsRoomService.GetNewsRooms(1);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

    }
}
