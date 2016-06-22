using AluminiService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AluminiWebApp.Areas.Faculty.Controllers
{
    public class BaseController : Controller
    {

        public readonly IUserDetailsViewService UserDetailsViewService;
        public readonly IUserInfoService UserInfoService;
        public readonly IEventCategoryService EventCategoryService;
        public readonly IUserPostService UserPostService;
        public readonly IUserPostPicturesService UserpostPictureServices;
        public readonly IUserPostVisibleService UserPostVisibleServices;
        public readonly IGenericMethodsService GenericMethods;
        public readonly ICourseCategoryService CategoryServices;
        public readonly IUserSelectionEventsService UserSelectionServices;
        public readonly IUserselectionBookingsService UserselectionBookingServices;
        public readonly IUserPaymentService UserpaymentsService;
        public readonly IUser_JobPostingService UserJobPostingservice;
        public readonly IEventService EventService;
        public readonly INewsRoomService NewsRoomService;
        public readonly IMemoriesServices MermoriesServices;
        public BaseController(IUserDetailsViewService _userDetailsViewService, IUserInfoService _userInfoService, IEventCategoryService _eventCategoryservice, IUserPostService _userpostService, IUserPostPicturesService _userPostPictureservice, IUserPostVisibleService _userPostVisibleService, IGenericMethodsService _genericMethods, ICourseCategoryService _categoryservices, IUserSelectionEventsService _userSelectionServices, IUserselectionBookingsService _userselectionBookingServices, IUserPaymentService _userPaymentservice, IUser_JobPostingService _userJobPostingservice, IEventService _eventService, INewsRoomService _newsRoomService, IMemoriesServices _mermoriesServices)
        {
            UserDetailsViewService = _userDetailsViewService;
            UserInfoService = _userInfoService;
            EventCategoryService = _eventCategoryservice;
            UserPostService = _userpostService;
            UserpostPictureServices = _userPostPictureservice;
            UserPostVisibleServices = _userPostVisibleService;
            GenericMethods = _genericMethods;
            CategoryServices = _categoryservices;
            UserSelectionServices = _userSelectionServices;
            UserselectionBookingServices = _userselectionBookingServices;
            UserpaymentsService = _userPaymentservice;
            UserJobPostingservice = _userJobPostingservice;
            EventService = _eventService;
            NewsRoomService = _newsRoomService;
            MermoriesServices = _mermoriesServices;
        }
    }
}