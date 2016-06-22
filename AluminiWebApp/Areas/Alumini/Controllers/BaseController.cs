using AluminiService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AluminiWebApp.Areas.Alumini.Controllers
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
        public readonly IUserSelectionEventsService UserSelectionServices;
        public readonly IUserselectionBookingsService UserselectionBookingServices;
        public readonly IUserPaymentService UserpaymentsService;
        public readonly IUser_JobPostingService UserJobPostingservice;
        public readonly IEventService EventService;
        public readonly INewsRoomService NewsRoomService;
        public readonly IMemoriesServices MermoriesServices;
        public readonly IDonationService DonationService;
        public readonly IProfessionalDetailsService ProfessionalDetailsservice;
        public readonly ISaluationService SalutationService;
        public readonly IEducationalDetailService EducationalDetailService;
        public readonly ICourseCategoryService CourseCategoryService;
        public readonly IFacultyWorkInfoService FacultyWorkInfoService;
        public BaseController()
        { }
        public BaseController(IUserDetailsViewService _userDetailsViewService, IUserInfoService _userInfoService, IEventCategoryService _eventCategoryservice, IUserPostService _userpostService, IUserPostPicturesService _userPostPictureservice, IUserPostVisibleService _userPostVisibleService, IGenericMethodsService _genericMethods, IUserSelectionEventsService _userSelectionServices, IUserselectionBookingsService _userselectionBookingServices, IUserPaymentService _userPaymentservice, IUser_JobPostingService _userJobPostingservice, IEventService _eventService, INewsRoomService _newsRoomService, IMemoriesServices _mermoriesServices, IDonationService _donationService, IProfessionalDetailsService _ProfessionalDetails, ISaluationService _SalutationService, IEducationalDetailService _educationalDetailService, ICourseCategoryService _courseCategoryService, IFacultyWorkInfoService _facultyWorkInfoService)
        {
            UserDetailsViewService = _userDetailsViewService;
            UserInfoService = _userInfoService;
            EventCategoryService = _eventCategoryservice;
            UserPostService = _userpostService;
            UserpostPictureServices = _userPostPictureservice;
            UserPostVisibleServices = _userPostVisibleService;
            GenericMethods = _genericMethods;
            UserSelectionServices = _userSelectionServices;
            UserselectionBookingServices = _userselectionBookingServices;
            UserpaymentsService = _userPaymentservice;
            UserJobPostingservice = _userJobPostingservice;
            EventService = _eventService;
            NewsRoomService = _newsRoomService;
            MermoriesServices = _mermoriesServices;
            DonationService = _donationService;
            ProfessionalDetailsservice = _ProfessionalDetails;
            SalutationService = _SalutationService;
            EducationalDetailService = _educationalDetailService;
            CourseCategoryService = _courseCategoryService;
            FacultyWorkInfoService = _facultyWorkInfoService;
        }
    }
}