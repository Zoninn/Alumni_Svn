using Alumini.Core;
using AluminiService.Interfaces;
using AluminiWebApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Ninject;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AluminiWebApp.Controllers
{
    public class AlumniProfileUpdateController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        [Inject]
        public AlumniProfileUpdateController(IUserDetailsViewService _userDetailsViewService, IUserInfoService _userService, ISaluationService _saluationService, IStateDistrictCityService _statecitydistrictService, ICourseCategoryService _courseCategoryservice, ICourseService _courseServices, IEducationalDetailService _educationalDetailService, IFacultyWorkInfoService _facultyWorkInfoService, IGraduationYearService _graduationyearservice, IProfessionalDetailsService _Professionaldetailsservice, IGenericMethodsService _genericMethodsservices, IUser_JobPostingService _userJobPostingservice, IEventService _eventServices, INewsRoomService _newsroomService, IDonationService _donationService)
            : base(_userService, _saluationService, _statecitydistrictService, _courseCategoryservice, _courseServices, _educationalDetailService, _facultyWorkInfoService, _graduationyearservice, _Professionaldetailsservice, _genericMethodsservices, _userDetailsViewService, _userJobPostingservice, _eventServices, _newsroomService, _donationService)
        {
        }


        public AlumniProfileUpdateController()
        {

        }


        public AlumniProfileUpdateController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
        public ActionResult BasicInformation()
        {
            if (Session["UserId"] != null)
            {
                int UserId = Convert.ToInt32(Session["UserId"].ToString());
                UserDetail Userdata = UserService.Get(UserId);
                DateTime? dts = Userdata.DOB;
                string formatted = String.Format("{0:dd/MM/yyyy}", dts);
                ProfileBasicInformation BasicProfile = new ProfileBasicInformation()
                {
                    FirstName = Userdata.FirstName,
                    LastName = Userdata.LastName,


                    DOB = Userdata.DOB,
                    Salutation = SaluationService.GetSaluations(),
                    //States = StatecitydistrictService.GetAllStates(),
                    SalutationId = Userdata.SalutationId,
                    GenderId = Convert.ToInt32(Userdata.GenderId),
                    ProfilePicture = Userdata.ProfilePicture
                };
                return View(BasicProfile);
            }
            return RedirectToAction("Login", "Account");
        }
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
                    FilePath = "../UserProfilePictures/" + fileName;
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
                UserService.UpdateBasicInformation(UserId, UpdateUserData);
                TempData["Message"] = "Details updated successfully";
                return RedirectToAction("BasicInformation", "AlumniProfileUpdate", new { area = "" });
            }
            UserDetail Userdata = UserService.Get(Convert.ToInt32(Session["UserId"]));
            ProfileBasicInformation BasicProfile = new ProfileBasicInformation()
            {
                FirstName = Userdata.FirstName,
                LastName = Userdata.LastName,
                DOB = Userdata.DOB,
                Salutation = SaluationService.GetSaluations(),
                //States = StatecitydistrictService.GetAllStates(),
                SalutationId = Userdata.SalutationId,
                GenderId = Convert.ToInt32(Userdata.GenderId),
                ProfilePicture = Userdata.ProfilePicture,
            };

            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult ContactInformation()
        {
            if (Session["UserId"] != null)
            {
                UserDetailsDTO Userdata = UserService.GetUserContactInformation(Convert.ToInt32(Session["UserId"].ToString()));
                ContactInformation Info = new ContactInformation()
                {
                    PresentAddress = Userdata.PermanentAddress,
                    PermanentAddress = Userdata.Address,
                    Countrys = GenericMethodsservices.GetAllCountries(),
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
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public ActionResult ContactInformation(ContactInformation ContactHostelsInfoDTO)
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
                UserService.UpdateContactInfo(userDetails);
                TempData["Message"] = "Details updated successfully";
                return RedirectToAction("ContactInformation", "AlumniProfileUpdate", new { area = "" });

            }
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult ProfessionalDetails()
        {
            if (Session["UserId"] != null)
            {
                int UserId = Convert.ToInt32(Session["UserId"].ToString());
                ProfessionalDetail Profdetails = Professionaldetailsservice.Get(UserId);
                List<Graduation> _GraduationYears = new List<Graduation>();

                DateTime CurrentDate = DateTime.Now;
                int Year = CurrentDate.Year;
                for (int i = 1970; i <= Year; i++)
                    _GraduationYears.Add(new Graduation { GraduationYear = "" + i, GraduationYearId = i });
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
            return RedirectToAction("Login", "Account");
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
                    Professionaldetailsservice.UpdateUserProfDetails(UserId, Profdetail);
                    TempData["Message"] = "Details updated successfully";
                    return RedirectToAction("ProfessionalDetails", "AlumniProfileUpdate", new { area = "" });

                }
                ProfessionalDetail ProfDet = Professionaldetailsservice.Get(UserId);
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
                return RedirectToAction("Login", "Account");
            }
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
            return RedirectToAction("Login", "Account");
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
                    TempData["Message"] = "Details updated successfully";
                    return RedirectToAction("UpdateAlumni", "AlumniProfileUpdate", new { area = "" });
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
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult UpdateFaculty()
        {
            if (Session["UserId"] != null)
            {
                int UserId = Convert.ToInt32(Session["UserId"].ToString());
                List<GraduationYear> _GraduationYears = new List<GraduationYear>();
                for (int i = 1970; i <= 2015; i++)
                    _GraduationYears.Add(new GraduationYear { Year = "" + i, GraduationYearId = i });
                FacultyWorkInfo facultyWorkInfo = FacultyWorkInfoService.Get(UserId);
                FacultyRegistrationModel Faculty = new FacultyRegistrationModel()
                {
                    DesignationName = facultyWorkInfo.DesignationName,
                    DepartmentName = facultyWorkInfo.DepartmentName,
                    WorkingFrom = facultyWorkInfo.WorkingFrom,
                    WorkingTo = facultyWorkInfo.WorkingTo,
                    GraduationYears = _GraduationYears,
                };
                return View(Faculty);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult UpdateFaculty(FacultyRegistrationModel FacultyRegistration)
        {
            if (Session["UserId"] != null)
            {
                int UserId = Convert.ToInt32(Session["UserId"].ToString());
                if (ModelState.IsValid)
                {
                    FacultyWorkInfo details = new FacultyWorkInfo();
                    TryUpdateModel(details);
                    FacultyWorkInfoService.UpdateWorkInfo(UserId, details);
                    return RedirectToAction("UpdateProfile", "Home");
                }

                List<GraduationYear> _GraduationYears = new List<GraduationYear>();
                for (int i = 1970; i <= 2015; i++)
                    _GraduationYears.Add(new GraduationYear { Year = "" + i, GraduationYearId = i });
                FacultyWorkInfo facultyWorkInfo = FacultyWorkInfoService.Get(UserId);
                FacultyRegistrationModel Faculty = new FacultyRegistrationModel()
                {
                    DesignationName = facultyWorkInfo.DesignationName,
                    DepartmentName = facultyWorkInfo.DepartmentName,
                    WorkingFrom = facultyWorkInfo.WorkingFrom,
                    WorkingTo = facultyWorkInfo.WorkingTo,
                    GraduationYears = _GraduationYears,
                };
                return View(Faculty);

            }

            return RedirectToAction("Login", "Account");

        }
        [HttpGet]
        public ActionResult ViewAllDetails()
        {
            return View();
        }
        [HttpGet]
        public ActionResult RegisterAsAlumni()
        {
            if (Session["UserId"] != null)
            {
                StudentRegistrationModel userdto = new StudentRegistrationModel()
                {
                    Coursecategorys = CourseCategoryService.GetAllCourseCategories(),
                };
                return View(userdto);
            }
            return RedirectToAction("Login", "Account");

        }
        [HttpPost]
        public ActionResult RegisterAsAlumni(StudentRegistrationModel StudentDetails)
        {
            if (Session["UserId"] != null)
            {
                if (ModelState.IsValid)
                {
                    string UserId = "";
                    UserId = Session["UserId"].ToString();
                    EducationalDetail details = new EducationalDetail();
                    TryUpdateModel(details);
                    details.UserId = Convert.ToInt64(Session["UserId"]);
                    details.Status = true;
                    EducationalDetailService.Create(details);

                    View_UserDetails Userdetails = UserDetailsViewService.GetUserByUserId(Convert.ToInt32(UserId));
                    UserManager.RemoveFromRole(Userdetails.AspnetUsersId, Userdetails.Role);
                    UserManager.AddToRole(Userdetails.AspnetUsersId, "Alumni and Faculty");

                    return RedirectToAction("UpdateProfile", "Home");
                }
                StudentRegistrationModel userdto = new StudentRegistrationModel()
                {
                    Coursecategorys = CourseCategoryService.GetAllCourseCategories(),
                };
                return View(userdto);
            }
            return RedirectToAction("Login", "Account");
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
            return RedirectToAction("Login", "Account");
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

                    return RedirectToAction("UpdateProfile", "Home");
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
            return RedirectToAction("Login", "Account");
        }

    }
}