using AluminiService.Interfaces;
using AluminiWebApp.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AluminiWebApp.Areas.Alumini.Controllers
{
    public class NewsRoomsController : BaseController
    {
        public NewsRoomsController(IUserDetailsViewService userDetailsViewService, IUserInfoService userInfoService, IEventCategoryService eventCategoryservices, IUserPostService userpostService, IUserPostPicturesService userPostPictureservice, IUserPostVisibleService userPostVisibleServices, IGenericMethodsService genericMethods, IUserSelectionEventsService userSelectionServices, IUserselectionBookingsService userselectionBookingServices, IUserPaymentService userPaymentservice, IUser_JobPostingService _userJobPostingservice, IEventService _eventService, INewsRoomService _newsRoomService, IMemoriesServices _mermoriesServices, IDonationService _donationService, IProfessionalDetailsService _ProfessionalDetails, ISaluationService _SaluationServices, IEducationalDetailService _educationalDetailService, ICourseCategoryService _courseCategoryService, IFacultyWorkInfoService _facultyWorkInfoService)
            : base(userDetailsViewService, userInfoService, eventCategoryservices, userpostService, userPostPictureservice, userPostVisibleServices, genericMethods, userSelectionServices, userselectionBookingServices, userPaymentservice, _userJobPostingservice, _eventService, _newsRoomService, _mermoriesServices, _donationService, _ProfessionalDetails, _SaluationServices, _educationalDetailService, _courseCategoryService, _facultyWorkInfoService)
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
