using Alumini.Core;
using AluminiService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AluminiWebApp.Areas.Faculty.Controllers
{
    public class ProfileController : BaseController
    {
        public ProfileController(IUserDetailsViewService userDetailsViewService, IUserInfoService userInfoService, IEventCategoryService eventCategoryservices, IUserPostService userpostService, IUserPostPicturesService userPostPictureservice, IUserPostVisibleService userPostVisibleServices, IGenericMethodsService genericMethods, ICourseCategoryService coursecategoryservices, IUserSelectionEventsService userSelectionServices, IUserselectionBookingsService userselectionBookingServices, IUserPaymentService userPaymentservice, IUser_JobPostingService _userJobPostingservice, IEventService _eventService, INewsRoomService _newsRoomService, IMemoriesServices _mermoriesServices)
            : base(userDetailsViewService, userInfoService, eventCategoryservices, userpostService, userPostPictureservice, userPostVisibleServices, genericMethods, coursecategoryservices, userSelectionServices, userselectionBookingServices, userPaymentservice, _userJobPostingservice, _eventService, _newsRoomService, _mermoriesServices)
        {

        }

        [HttpGet]
        public ActionResult Profile()
        {
            if (Session["UserId"] != null)
            {
                View_UserDetails Userdetails = UserDetailsViewService.GetUserByUserId(Convert.ToInt32(Session["UserId"].ToString()));

                return View(Userdetails);
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });

        }      
    }
}
