using AluminiService.Interfaces;
using AluminiWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AluminiWebApp.Controllers
{
    public class DesignController : BaseController
    {
        public DesignController(IUserDetailsViewService _userDetailsViewService, IUserInfoService _userService, ISaluationService _saluationService, IStateDistrictCityService _statecitydistrictService, ICourseCategoryService _courseCategoryservice, ICourseService _courseServices, IEducationalDetailService _educationalDetailService, IFacultyWorkInfoService _facultyWorkInfoService, IGraduationYearService _graduationyearservice, IProfessionalDetailsService _Professionaldetailsservice, IGenericMethodsService _genericMethodsservices, IUser_JobPostingService _userJobPostingservice, IEventService _eventServices, INewsRoomService _newsroomService, IDonationService _donationService)
            : base(_userService, _saluationService, _statecitydistrictService, _courseCategoryservice, _courseServices, _educationalDetailService, _facultyWorkInfoService, _graduationyearservice, _Professionaldetailsservice, _genericMethodsservices, _userDetailsViewService, _userJobPostingservice, _eventServices, _newsroomService, _donationService)
        {
        }

        public ActionResult Index()
        {
            EventsModel Model = new EventsModel()
            {
                UserNews = NewsroomService.GetNews(),
                UserJobs = UserJobPostingservice.GetJobs().ToList().Take(6),

                DisplayHome = EventServices.GetEventsforHome()
            };

            return View(Model);
        }
        public ActionResult Menu()
        {
            return View();
        }
    }
}