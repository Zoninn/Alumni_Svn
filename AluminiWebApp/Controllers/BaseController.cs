using AluminiService.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace AluminiWebApp.Controllers
{
    public class BaseController : Controller
    {

        public readonly IUserInfoService UserService;
        public readonly ISaluationService SaluationService;
        public readonly IStateDistrictCityService StatecitydistrictService;
        public readonly ICourseCategoryService CourseCategoryService;
        public readonly ICourseService CourseServises;
        public readonly IEducationalDetailService EducationalDetailService;
        public readonly IFacultyWorkInfoService FacultyWorkInfoService;
        public readonly IGraduationYearService Graduationyearservice;
        public readonly IProfessionalDetailsService Professionaldetailsservice;
        public readonly IGenericMethodsService GenericMethodsservices;
        public readonly IUserDetailsViewService UserDetailsViewService;
        public readonly IUser_JobPostingService UserJobPostingservice;
        public readonly IEventService EventServices;
        public readonly INewsRoomService NewsroomService;
        public readonly IDonationService DonationService;

        public BaseController()
        { }

        public BaseController(IUserInfoService _userService, ISaluationService _saluationService, IStateDistrictCityService _statecitydistrictService, ICourseCategoryService _corseCategoryservices, ICourseService _courseServices, IEducationalDetailService _educationalDetailService, IFacultyWorkInfoService _facultyWorkInfoService, IGraduationYearService _graduationYearServive, IProfessionalDetailsService _professionaldetailsservice, IGenericMethodsService _genericMethodsservices, IUserDetailsViewService _userDetailsViewService, IUser_JobPostingService _userJobPostingservice, IEventService _eventServices, INewsRoomService _newsroomService,IDonationService _donationService)
        {
            UserService = _userService;
            SaluationService = _saluationService;
            StatecitydistrictService = _statecitydistrictService;
            CourseCategoryService = _corseCategoryservices;
            CourseServises = _courseServices;
            EducationalDetailService = _educationalDetailService;
            FacultyWorkInfoService = _facultyWorkInfoService;
            Graduationyearservice = _graduationYearServive;
            Professionaldetailsservice = _professionaldetailsservice;
            GenericMethodsservices = _genericMethodsservices;
            UserDetailsViewService = _userDetailsViewService;
            UserJobPostingservice = _userJobPostingservice;
            EventServices = _eventServices;
            NewsroomService = _newsroomService;
            DonationService = _donationService;
        }


    }


}
