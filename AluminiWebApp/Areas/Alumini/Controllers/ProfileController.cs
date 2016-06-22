using Alumini.Core;
using AluminiService.Interfaces;
using AluminiWebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Ninject;
using System.Globalization;

namespace AluminiWebApp.Areas.Alumini.Controllers
{
    public class ProfileController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ProfileController(IUserDetailsViewService userDetailsViewService, ISaluationService _saluationService, IUserInfoService userInfoService, IEventCategoryService eventCategoryservices, IUserPostService userpostService, IUserPostPicturesService userPostPictureservice, IUserPostVisibleService userPostVisibleServices, IGenericMethodsService genericMethods, IUserSelectionEventsService userSelectionServices, IUserselectionBookingsService userselectionBookingServices, IUserPaymentService userPaymentservice, IUser_JobPostingService _userJobPostingservice, IEventService _eventService, INewsRoomService _newsRoomService, IMemoriesServices _mermoriesServices, IDonationService _donationService, IProfessionalDetailsService _ProfessionalDetails, ISaluationService _SaluationServices, IEducationalDetailService _educationalDetailService, ICourseCategoryService _courseCategoryService, IFacultyWorkInfoService _facultyWorkInfoService)
            : base(userDetailsViewService, userInfoService, eventCategoryservices, userpostService, userPostPictureservice, userPostVisibleServices, genericMethods, userSelectionServices, userselectionBookingServices, userPaymentservice, _userJobPostingservice, _eventService, _newsRoomService, _mermoriesServices, _donationService, _ProfessionalDetails, _SaluationServices, _educationalDetailService, _courseCategoryService, _facultyWorkInfoService)
        {

        }
        public ProfileController()
        {

        }

        public ProfileController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        public ActionResult AlumniProfile(int id)
        {
            View_UserDetails Userdetails = UserDetailsViewService.GetUserByUserId(id);

            return View(Userdetails);
        }

        [HttpGet]
        public ActionResult PasswordChange()
        {
            ChangePasswordModel Passwrod = new ChangePasswordModel();

            if (Session["UserId"] != null)
            {
            }
            else
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }
            return View(Passwrod);
        }
        [HttpPost]
        public ActionResult PasswordChange(ChangePasswordModel ChangePassword)
        {

            if (Session["UserId"] != null)
            {
                View_UserDetails GetUserRolesCount = UserDetailsViewService.GetUserByUserId(Convert.ToInt32(Session["UserId"]));


                if (ChangePassword.NewPassword.Length != 6)
                {
                    TempData["Successmessage"] = "Password must be 6 characters long";
                }
                else
                {
                    var hashedNewPassword = UserManager.ChangePassword(GetUserRolesCount.AspnetUsersId, ChangePassword.OldPassword, ChangePassword.NewPassword);
                    if (hashedNewPassword.Succeeded == false)
                    {
                        TempData["Successmessage"] = "invalid old password";

                    }
                    else
                    {
                        TempData["Successmessage"] = "Password Changed successfully";
                    }
                }

            }
            else
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }
            return View();
        }


        [HttpGet]
        public ActionResult ProfessionalDetails()
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    int UserId = Convert.ToInt32(Session["UserId"].ToString());

                    ProfessionalDetail Profdetails = ProfessionalDetailsservice.Get(UserId);
                    List<Graduation> _GraduationYears = new List<Graduation>();
                    DateTime CurrentDate = DateTime.Now;
                    int Year = CurrentDate.Year;
                    for (int i = 1970; i <= Year; i++)
                        _GraduationYears.Add(new Graduation { GraduationYear = "" + i, GraduationYearId = i });
                    if (Profdetails != null)
                    {
                        ProfessionalDetails details = new ProfessionalDetails()
                        {
                            Company = Profdetails.Company,
                            Designation = Profdetails.Designation,
                            WorkedFrom = Profdetails.WorkedFrom,
                            WorkedTill = Profdetails.WorkedTill,
                            GraduationYears = _GraduationYears
                        };
                        return View(details);
                    }
                    else
                    {
                        ProfessionalDetails details = new ProfessionalDetails()
                        {
                            Company = null,
                            Designation = null,
                            WorkedFrom = null,
                            WorkedTill = null,
                            GraduationYears = _GraduationYears
                        };
                        return View(details);
                    }

                }
                else
                {
                    return RedirectToAction("Login", "Account", new { area = "" });
                }
            }
            catch (SystemException ex)
            {

            }
            return View();

        }

        [HttpGet]
        public ActionResult UpdateAlumni()
        {
            if (Session["UserId"] != null)
            {
                StudentRegistrationModel userdto = null;
                int UserId = Convert.ToInt32(Session["UserId"].ToString());
                List<EducationdetailsDTO> educationdetails = EducationalDetailService.GetEducationdetails(UserId);
                List<GraduationYear> _GraduationYears = new List<GraduationYear>();
                for (int i = 1970; i <= 2015; i++)
                    _GraduationYears.Add(new GraduationYear { Year = "" + i, GraduationYearId = i });
                foreach (var educationdetail in educationdetails)
                {
                    userdto = new StudentRegistrationModel()
                    {
                        CourseId = educationdetail.CourseId,
                        CourseCategoryId = educationdetail.CategoryId,
                        Batch = educationdetail.Batch,

                        Coursecategorys = CourseCategoryService.GetAllCourseCategories(),
                    };
                }
                int? RoleId = null;

                List<View_UserDetails> GetUserRolesCount = UserDetailsViewService.GetUserRolesCount(UserId);

                foreach (var Roles in GetUserRolesCount)
                {
                    RoleId = Convert.ToInt32(Roles.RoleId);
                }
                if (RoleId == 1)
                {
                    return View(userdto);
                }
                else if (RoleId == 2)
                {
                    return RedirectToAction("UpdateFaculty", "Profile");
                }
                else if (RoleId == 4)
                {

                    return RedirectToAction("ViewAllDetails", "Profile");
                }

            }
            return RedirectToAction("Login", "Account", new { area = "" });
        }

        [HttpPost]
        public ActionResult UpdateAlumni(StudentRegistrationModel StudentRegistration)
        {
            if (Session["UserId"] != null)
            {
                int UserId = Convert.ToInt32(Session["UserId"].ToString());
                StudentRegistrationModel userdto = null;
                if (ModelState.IsValid)
                {
                    EducationalDetail Education = new EducationalDetail();
                    TryUpdateModel(Education);
                    EducationalDetailService.UpdateEducationDetails(UserId, Education);
                    TempData["Success"] = UtilitiesClass.SuccessMessage;
                    return RedirectToAction("Profile", "Profile", new { area = "Alumini" });
                }

                List<EducationdetailsDTO> educationdetails = EducationalDetailService.GetEducationdetails(UserId);
                List<GraduationYear> _GraduationYears = new List<GraduationYear>();
                for (int i = 1970; i <= 2015; i++)
                    _GraduationYears.Add(new GraduationYear { Year = "" + i, GraduationYearId = i });
                foreach (var educationdetail in educationdetails)
                {
                    userdto = new StudentRegistrationModel()
                    {
                        CourseId = educationdetail.CourseId,
                        CourseCategoryId = educationdetail.CategoryId,
                        Batch = educationdetail.Batch,
                        Coursecategorys = CourseCategoryService.GetAllCourseCategories(),
                    };
                }
                return View(userdto);
            }
            return RedirectToAction("Login", "Account", new { area = "" });
        }


        [HttpGet]
        public ActionResult RegisterAsFaculty()
        {
            if (Session["UserId"] != null)
            {
                List<GraduationYear> _GraduationYears = new List<GraduationYear>();
                for (int i = 1970; i <= 2015; i++)
                    _GraduationYears.Add(new GraduationYear { Year = "" + i, GraduationYearId = i });
                FacultyRegistrationModel Faculty = new FacultyRegistrationModel()
                {
                    GraduationYears = _GraduationYears,
                };
                return View(Faculty);
            }
            return RedirectToAction("Login", "Account", new { area = "" });
        }

        [HttpPost]
        public ActionResult RegisterAsFaculty(FacultyRegistrationModel FacultyRegistration)
        {
            if (Session["UserId"] != null)
            {
                if (ModelState.IsValid)
                {
                    int UserId = Convert.ToInt32(Session["UserId"].ToString());
                    FacultyWorkInfo details = new FacultyWorkInfo();
                    TryUpdateModel(details);
                    details.FacultyUserId = Convert.ToInt64(Session["UserId"]);
                    details.Status = true;
                    FacultyWorkInfoService.Create(details);
                    View_UserDetails Userdetails = UserDetailsViewService.GetUserByUserId(Convert.ToInt32(UserId));
                    UserManager.RemoveFromRole(Userdetails.AspnetUsersId, Userdetails.Role);
                    UserManager.AddToRole(Userdetails.AspnetUsersId, "Alumni and Faculty");
                    TempData["Success"] = UtilitiesClass.SuccessMessage;
                    return RedirectToAction("Profile", "Profile", new { area = "Alumini" });
                }
                List<GraduationYear> _GraduationYears = new List<GraduationYear>();
                for (int i = 1970; i <= 2015; i++)
                    _GraduationYears.Add(new GraduationYear { Year = "" + i, GraduationYearId = i });
                FacultyRegistrationModel Faculty = new FacultyRegistrationModel()
                {
                    GraduationYears = _GraduationYears,
                };
                return View(Faculty);
            }
            return RedirectToAction("Login", "Account", new { area = "" });
        }




        [HttpPost]
        public ActionResult ProfessionalDetails(ProfessionalDetails Profdetails)
        {
            if (Session["UserId"] != null)
            {
                int UserId = Convert.ToInt32(Session["UserId"].ToString());
                if (ModelState.IsValid)
                {


                    ProfessionalDetail Profdetail = new ProfessionalDetail()
                    {
                        Company = Profdetails.Company,
                        Designation = Profdetails.Designation,
                        WorkedFrom = Profdetails.WorkedFrom,
                        WorkedTill = Profdetails.WorkedTill

                    };
                    ProfessionalDetailsservice.UpdateUserProfDetails(UserId, Profdetail);
                    TempData["Success"] = UtilitiesClass.SuccessMessage;
                    return RedirectToAction("Profile", "Profile", new { area = "Alumini" });
                }
                ProfessionalDetail ProfDet = ProfessionalDetailsservice.Get(UserId);
                List<Graduation> _GraduationYears = new List<Graduation>();
                for (int i = 1970; i <= 2015; i++)
                    _GraduationYears.Add(new Graduation { GraduationYear = "" + i, GraduationYearId = i });
                ProfessionalDetails details = new ProfessionalDetails()
                {
                    Company = Profdetails.Company,
                    Designation = ProfDet.Designation,
                    WorkedFrom = ProfDet.WorkedFrom,
                    WorkedTill = ProfDet.WorkedTill,
                    GraduationYears = _GraduationYears
                };
                return View(details);
            }
            else
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }
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

        [HttpGet]
        public JsonResult GetUserprofile(int Userid)
        {
            var Userdetails = UserDetailsViewService.GetUserByUserId(Userid);
            return Json(Userdetails, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetUserImages()
        {

            var Data = UserInfoService.getUserImages(Convert.ToInt32(Session["UserId"].ToString()));
            return Json(Data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ProfileUpdate()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BasicInformation()
        {
            if (Session["UserId"] != null)
            {
                int UserId = Convert.ToInt32(Session["UserId"].ToString());
                UserDetail Userdata = UserInfoService.Get(UserId);
                ProfileBasicInformation BasicProfile = new ProfileBasicInformation()
                {
                    FirstName = Userdata.FirstName,
                    LastName = Userdata.LastName,
                    DOB = Userdata.DOB,
                    Salutation = SalutationService.GetSaluations(),
                    //States = StatecitydistrictService.GetAllStates(),
                    SalutationId = Userdata.SalutationId,
                    GenderId = Convert.ToInt32(Userdata.GenderId),
                    ProfilePicture = Userdata.ProfilePicture
                };
                return View(BasicProfile);
            }
            return RedirectToAction("Login", "Account", new { area = "" });
        }

        /// <summary>
        /// Update Location and Contact Info
        /// </summary>
        /// <param name="RegisterDTO"></param>
        /// <param name="ProfilePicture"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ContactInformation()
        {
            if (Session["UserId"] != null)
            {
                UserDetailsDTO Userdata = UserInfoService.GetUserContactInformation(Convert.ToInt32(Session["UserId"].ToString()));
                ContactInformation Info = new ContactInformation()
                {
                    //CountryCodes = Convert.ToInt32(Userdata.CountryCode),
                    //Mobile = Userdata.PhoneNumber,
                    //AlternateEmail = Userdata.AlternateEmailId,

                    //CountryId = Userdata.CountryId.Value,
                    ////States = StatecitydistrictService.GetAllStates(),
                    //PresentAddress = Userdata.Address,
                    //PermanentAddress = Userdata.PermanentAddress,
                    //PermenantDistrictId = Convert.ToInt32(Userdata.DistrictId),
                    ////PermanentCityId = Userdata.HomeCityId,
                    ////CityId = Userdata.LivesInCityId,
                    //StateId = Convert.ToInt32(Userdata.StateId),
                    //PermanentStateId = Convert.ToInt32(Userdata.StateId),
                    //PermanentCountryid = Userdata.PermanentCountryId.Value
                    Countrys = GenericMethods.GetAllCountries(),


                };
                return View(Info);
            }
            return RedirectToAction("Login", "Account", new { area = "" });
        }
        [HttpGet]
        public ActionResult UpdateContactDetails()
        {
            if (Session["UserId"] != null)
            {
                UserDetailsDTO Userdata = UserInfoService.GetUserContactInformation(Convert.ToInt32(Session["UserId"].ToString()));
                ContactInformation Info = new ContactInformation()
                {
                    PresentAddress = Userdata.PermanentAddress,
                    PermanentAddress = Userdata.Address,
                    Countrys = GenericMethods.GetAllCountries(),
                    CountryId = Userdata.CountryId.Value,
                    PermanentCountryid = Userdata.PermanentCountryId.Value,
                    Mobile = Userdata.PhoneNumber,
                    StateId = Convert.ToInt32(Userdata.StateId),
                    PermanentStateId = Convert.ToInt32(Userdata.StateId),
                    Districtid = Userdata.PresentDistid.Value,
                    PermenantDistrictId = Userdata.Permanentdistid,
                    AlternateEmail = Userdata.AlternateEmailId,
                    AlternameMobile = Userdata.HomePhoneNumber,
                    cityName = Userdata.PresentCity,
                    AlternatecityName = Userdata.PermanentCity
                };
                return View(Info);
            }
            return RedirectToAction("Login", "Account", new { area = "" });

        }
        [HttpPost]
        public ActionResult UpdateContactDetails(ContactInformation ContactHostelsInfoDTO)
        {
            if (Session["UserId"] != null)
            {
                UserDetail userDetails = new UserDetail()
                {
                    Id = Convert.ToInt32(Session["UserId"].ToString()),
                    AlternateEmailId = ContactHostelsInfoDTO.AlternateEmail,
                    PhoneNumber = ContactHostelsInfoDTO.Mobile,
                    PermanentAddress = ContactHostelsInfoDTO.PermanentAddress,
                    PermanentCity = ContactHostelsInfoDTO.AlternatecityName,
                    PresentCity = ContactHostelsInfoDTO.cityName,
                    PermanentDistid = ContactHostelsInfoDTO.Districtid,
                    PresentDistid = ContactHostelsInfoDTO.PermenantDistrictId,
                    Address = ContactHostelsInfoDTO.PresentAddress,
                    HomePhoneNumber = ContactHostelsInfoDTO.AlternameMobile,
                };
                UserInfoService.UpdateContactInfo(userDetails);
                TempData["Success"] = UtilitiesClass.SuccessMessage;
                return RedirectToAction("Profile", "Profile", new { area = "Alumini" });
            }
            return RedirectToAction("Login", "Account", new { area = "" });
        }

        [HttpPost]
        public ActionResult ContactInformation(ContactInformation ContactHostelsInfoDTO)
        {
            if (Session["UserId"] != null)
            {
                if (ModelState.IsValid)
                {
                    UserDetail userDetails = new UserDetail()
                    {
                        Id = Convert.ToInt32(Session["UserId"].ToString()),
                        AlternateEmailId = ContactHostelsInfoDTO.AlternateEmail,
                        PhoneNumber = ContactHostelsInfoDTO.Mobile,
                        PermanentAddress = ContactHostelsInfoDTO.PermanentAddress,
                        //HomeCityId = ContactHostelsInfoDTO.CityId,
                        //LivesInCityId = ContactHostelsInfoDTO.PermanentCityId,
                        Address = ContactHostelsInfoDTO.PresentAddress,
                        CountryCode = ContactHostelsInfoDTO.CountryCodes,
                        HomePhoneNumber = ContactHostelsInfoDTO.AlternameMobile,
                        //ProfileInfoPercentage = (int)CustomStatus.ProfileInfoPercentage.ContactInformation,

                    };

                    UserInfoService.UpdateContactInfo(userDetails);
                    TempData["Success"] = UtilitiesClass.SuccessMessage;
                    return RedirectToAction("Profile", "Profile", new { area = "Alumini" });

                }
            }
            return RedirectToAction("Login", "Account", new { area = "" });
        }

        /// <summary>
        /// End Location
        /// </summary>
        /// <param name="RegisterDTO"></param>
        /// <param name="ProfilePicture"></param>
        /// <returns></returns>



        [HttpPost]
        public ActionResult BasicInformation(ProfileBasicInformation RegisterDTO, HttpPostedFileBase ProfilePicture)
        {

            if (Session["UserId"] != null)
            {
                int UserId = Convert.ToInt32(Session["UserId"].ToString());

                var FilePath = "";
                if (ProfilePicture != null)
                {
                    var fileName = Path.GetFileName(ProfilePicture.FileName);
                    var path = Path.Combine(Server.MapPath("~/UserProfilePictures/" + fileName));
                    // model.ImageServerPath = path;
                    ProfilePicture.SaveAs(path);
                    FilePath = "/UserProfilePictures/" + fileName;
                }

                UserDetail UpdateUserData = new UserDetail()
                {
                    FirstName = RegisterDTO.FirstName,
                    LastName = RegisterDTO.LastName,
                    SalutationId = RegisterDTO.SalutationId,
                    GenderId = RegisterDTO.GenderId,
                    DOB = DateTime.Parse(RegisterDTO.DateofBirth, new CultureInfo("en-CA")),
                    ProfilePicture = FilePath


                };
                UserInfoService.UpdateBasicInformation(UserId, UpdateUserData);
                TempData["Success"] = UtilitiesClass.SuccessMessage;
                return RedirectToAction("Profile", "Profile", new { area = "Alumini" });

                UserDetail Userdata = UserInfoService.Get(UserId);
                ProfileBasicInformation BasicProfile = new ProfileBasicInformation()
                {
                    FirstName = Userdata.FirstName,
                    LastName = Userdata.LastName,
                    DOB = Userdata.DOB,
                    Salutation = SalutationService.GetSaluations(),
                    //States = StatecitydistrictService.GetAllStates(),
                    SalutationId = Userdata.SalutationId,
                    GenderId = Convert.ToInt32(Userdata.GenderId),
                    ProfilePicture = Userdata.ProfilePicture,
                };
                return View(BasicProfile);
            }
            return RedirectToAction("Login", "Account", new { area = "" });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }

}
