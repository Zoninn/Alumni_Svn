using AluminiService.Interfaces;
using AluminiWebApp.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AluminiWebApp.Areas.Faculty.Controllers
{
    public class NewsRoomsController : BaseController
    {
        public NewsRoomsController(IUserDetailsViewService _userDetailsViewService, IUserInfoService _userInfoService, IEventCategoryService _eventCategoryservices, IUserPostService _userpostService, IUserPostPicturesService _userPostPictureservice, IUserPostVisibleService _userPostVisibleServices, IGenericMethodsService _genericMethods, ICourseCategoryService _coursecategoryservices, IUserSelectionEventsService _userSelectionServices, IUserselectionBookingsService _userselectionBookingServices, IUserPaymentService _userPaymentservice, IUser_JobPostingService _userJobPostingservice, IEventService _eventService, INewsRoomService _newsRoomService, IMemoriesServices _mermoriesServices)
            : base(_userDetailsViewService, _userInfoService, _eventCategoryservices, _userpostService, _userPostPictureservice, _userPostVisibleServices, _genericMethods, _coursecategoryservices, _userSelectionServices, _userselectionBookingServices, _userPaymentservice, _userJobPostingservice, _eventService, _newsRoomService, _mermoriesServices)
        {

        }

        [HttpGet]
        public ActionResult News()
        {
            NewsRoomModel Model = new NewsRoomModel()
            {
                GetNews = NewsRoomService.GetNewsRooms(1)
            };


            return View(Model);
        }
        [HttpGet]
        public ActionResult SingleNews(int id)
        {
            var Data = NewsRoomService.Get(id);
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
    }
}
