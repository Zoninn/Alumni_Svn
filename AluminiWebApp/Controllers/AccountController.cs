using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using AluminiWebApp.Models;
using AluminiService.Interfaces;
using Alumini.Core;
using Ninject;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;
using DotNetOpenAuth.AspNet;
using Microsoft.Owin.Security.Facebook;
using Facebook;
using System.Security.Claims;
using System.Web.Script.Serialization;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Globalization;

namespace AluminiWebApp.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        [Inject]
        public AccountController(IUserDetailsViewService _userDetailsViewService, IUserInfoService _userService, ISaluationService _saluationService, IStateDistrictCityService _statecitydistrictService, ICourseCategoryService _courseCategoryservice, ICourseService _courseServices, IEducationalDetailService _educationalDetailService, IFacultyWorkInfoService _facultyWorkInfoService, IGraduationYearService _graduationyearservice, IProfessionalDetailsService _Professionaldetailsservice, IGenericMethodsService _genericMethodsservices, IUser_JobPostingService _userJobPostingservice, IEventService _eventServices, INewsRoomService _newsroomService, IDonationService _donationService)
            : base(_userService, _saluationService, _statecitydistrictService, _courseCategoryservice, _courseServices, _educationalDetailService, _facultyWorkInfoService, _graduationyearservice, _Professionaldetailsservice, _genericMethodsservices, _userDetailsViewService, _userJobPostingservice, _eventServices, _newsroomService, _donationService)
        {
        }



        public AccountController()
        {

        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]

        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl, int? id)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    int? ProfileStatus = UserService.GetUserProfileStatusonUserId(model.Email);
                    var userid = UserManager.FindByEmail(model.Email);
                    UserDetail detailsusers = UserService.GetUser(userid.Id);
                    int? userStatus = UserService.GetUser(userid.Id).UserStatus;
                    Session["UserId"] = detailsusers.Id;
                    Session["AspnetUserId"] = userid.Id;
                    if (userStatus == 0)
                    {
                        if (detailsusers != null)
                        {

                        }
                        else
                        {

                        }
                        if (ProfileStatus == 2)
                        {
                            return RedirectToAction("Step3Registration");
                        }
                        else if (ProfileStatus == 3)
                        {
                            return RedirectToAction("ContactInformation");
                        }
                        else if (ProfileStatus == 4)
                        {
                            return RedirectToAction("ProfessionalDetails");
                        }

                    }
                    else if (userStatus == 2)
                    {
                        ModelState.AddModelError("", "Your Account is rejected.");
                        return View(model);
                    }

                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    switch (result)
                    {
                        case SignInStatus.Success:
                            string AspnetUsersId = UserManager.FindByEmail(model.Email).Id;
                            //int? userStatus = UserService.GetUser(AspnetUsersId).UserStatus;
                            if (userStatus == (int)CustomStatus.UserStatus.Approved)
                            {
                                if (returnUrl != null)
                                    return RedirectToLocal(returnUrl);
                                else
                                {
                                    string userRole = UserManager.GetRoles(AspnetUsersId)[0];
                                    Session["UserRole"] = userRole;
                                    if (userRole == "Admin")
                                        return RedirectToAction("Index", "Dashboard", new { Area = "Admin" });
                                    else if (userRole == "Alumini")
                                        if (id == 1)
                                        {
                                            if (Session["Jobid"] != null)
                                            {
                                                int Jobsid = Convert.ToInt32(Session["Jobid"].ToString());
                                                return RedirectToAction("SingleEvent", "AlumniEvents", new { Area = "Alumini", EventId = Jobsid });
                                            }
                                            return RedirectToAction("Events", "AlumniEvents", new { Area = "Alumini" });
                                        }
                                        else if (id == 2)
                                        {
                                            if (Session["Jobid"] != null)
                                            {
                                                int Jobsid = Convert.ToInt32(Session["Jobid"].ToString());
                                                return RedirectToAction("SingleJob", "Jobs", new { Area = "Alumini", id = Jobsid });
                                            }
                                            return RedirectToAction("Index", "WhiteBoard", new { Area = "Alumini" });

                                        }
                                        else if (id == 3)
                                        {
                                            if (Session["Jobid"] != null)
                                            {
                                                int Jobsid = Convert.ToInt32(Session["Jobid"].ToString());
                                                return RedirectToAction("DonationDetails", "Donations", new { Area = "Alumini", id = Jobsid });
                                            }
                                            return RedirectToAction("Index", "WhiteBoard", new { Area = "Alumini" });

                                        }
                                        else if (id == 4)
                                        {
                                            return RedirectToAction("Members", "AlumniMembers", new { Area = "Alumini" });
                                        }
                                        else
                                        {
                                            return RedirectToAction("Profile", "Profile", new { Area = "Alumini" });
                                        }
                                    else if (userRole == "Faculty")
                                    {
                                        if (id == 1)
                                        {

                                            if (Session["Jobid"] != null)
                                            {
                                                int Jobsid = Convert.ToInt32(Session["Jobid"].ToString());
                                                return RedirectToAction("SingleEvent", "FacultyEvents", new { Area = "Faculty", EventId = Jobsid });
                                            }
                                            return RedirectToAction("Events", "FacultyEvents", new { Area = "Faculty" });

                                        }
                                        else if (id == 2)
                                        {

                                            if (Session["Jobid"] != null)
                                            {
                                                int Jobsid = Convert.ToInt32(Session["Jobid"].ToString());
                                                return RedirectToAction("SingleJob", "Jobs", new { Area = "Faculty", id = Jobsid });
                                            }
                                            return RedirectToAction("Index", "WhiteBoard", new { Area = "Faculty" });

                                        }
                                        else if (id == 3)
                                        {

                                            if (Session["Jobid"] != null)
                                            {
                                                int Jobsid = Convert.ToInt32(Session["Jobid"].ToString());
                                                return RedirectToAction("DonationDetails", "Donations", new { Area = "Alumini", id = Jobsid });
                                            }
                                            return RedirectToAction("Index", "WhiteBoard", new { Area = "Faculty" });

                                        }
                                        else
                                        {

                                            return RedirectToAction("Index", "WhiteBoard", new { Area = "Faculty" });
                                        }
                                    }
                                    else if (userRole == "Alumni and Faculty")
                                        return RedirectToAction("Index", "WhiteBoard", new { Area = "AlumniFaculty" });
                                    else
                                        return View();

                                }

                            }
                            else
                            {
                                //ModelState.AddModelError("", "'your registration is not approved/pending.");
                                //return View(model);
                                return RedirectToAction("ProfileCompleted", "Home");
                            }
                        case SignInStatus.LockedOut:
                            return View("Lockout");
                        case SignInStatus.RequiresVerification:
                            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                        case SignInStatus.Failure:
                        default:

                            ModelState.AddModelError("", "Invalid login attempt.");
                            return View(model);
                    }
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public JsonResult ResetuserPassword(string Email)
        {
            UserManager<IdentityUser> userManager =
      new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            var Details = UserManager.FindByEmail(Email);
            if (Details != null)
            {
                string Password = AccountController.RandomString(8);
                userManager.RemovePassword(Details.Id);
                userManager.AddPassword(Details.Id, Password);
                Emails.SendEmails(Email, "", Password, "");
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            return Json("your email not exists in our records.please register", JsonRequestBehavior.AllowGet);
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]

        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult UserRegistration()
        {
            try
            {
                if (Session["ExternalLogin"] != null)
                {
                    var user = new ApplicationUser { UserName = Session["EmailId"].ToString(), Email = Session["EmailId"].ToString() };
                    var CheckEmailId = UserManager.FindByEmail(Session["EmailId"].ToString());
                    if (CheckEmailId != null)
                    {
                        int? ProfileStatus = UserService.GetUserProfileStatusonUserId(Session["EmailId"].ToString());
                        var userid = UserManager.FindByEmail(Session["EmailId"].ToString());
                        UserDetail detailsusers = UserService.GetUser(userid.Id);
                        int? userStatus = 0;
                        if (detailsusers != null)
                        {
                            userStatus = UserService.GetUser(userid.Id).UserStatus;
                            Session["UserId"] = detailsusers.Id;
                        }

                        if (userStatus != 1)
                        {
                            Session["AspnetUserId"] = userid.Id;
                            Session["UserId"] = detailsusers.Id;
                            if (detailsusers != null)
                            {
                                Session["UserId"] = detailsusers.Id;
                            }
                            else
                            {

                            }
                            if (ProfileStatus == 2)
                            {
                                return RedirectToAction("Step3Registration");
                            }
                            else if (ProfileStatus == 3)
                            {
                                return RedirectToAction("ContactInformation");
                            }
                            else if (ProfileStatus == 4)
                            {
                                return RedirectToAction("ProfessionalDetails");
                            }
                            else if (ProfileStatus == 5)
                            {
                                return RedirectToAction("ProfileCompleted", "Home");
                            }
                        }
                        else
                        {
                            return RedirectToAction("Index", "WhiteBoard", new { Area = "Alumini" });
                        }

                    }
                }
                RegistrationStep2ViewModel UserDTO = new RegistrationStep2ViewModel()
                {
                    Salutation = SaluationService.GetSaluations(),
                    //States = StatecitydistrictService.GetAllStates(),
                    Email = Session["EmailId"].ToString()

                };

                return View(UserDTO);

            }
            catch (SystemException ex)
            {
                return RedirectToAction("Register");
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult UserRegistration(RegistrationStep2ViewModel RegisterDTO, HttpPostedFileBase ProfilePicture)
        {
            try
            {

                string email = RegisterDTO.Email;
                var user = new ApplicationUser { UserName = email, Email = email };
                var result = UserManager.Create(user, RegisterDTO.Password);
                if (result.Succeeded)
                {
                    var UserId = UserManager.FindByEmail(email);
                    Session["AspnetUserId"] = UserId.Id;
                    var fileName = Path.GetFileName(ProfilePicture.FileName);
                    var path = Path.Combine(Server.MapPath("~/UserProfilePictures/" + fileName));
                    // model.ImageServerPath = path;
                    ProfilePicture.SaveAs(path);
                    var FilePath = "/UserProfilePictures/" + fileName;
                    UserDetail userDetails = new UserDetail();
                    TryUpdateModel(userDetails);
                    userDetails.AspnetUsersId = UserId.Id;
                    //error is supressed here , need to log this error.
                    userDetails.LivesInCityId = 1;
                    userDetails.HomeCityId = 1;
                    userDetails.ProfilePicture = FilePath;
                    userDetails.UserStatus = 0;
                    userDetails.DOB = DateTime.Parse(RegisterDTO.DateofBirth, new CultureInfo("en-CA"));
                    userDetails.ProfileInfoPercentage = (int)CustomStatus.ProfileInfoPercentage.BasicAndPersonal;
                    TempData["UserId"] = UserService.Create(userDetails).Id;
                    Session["UserId"] = TempData["UserId"].ToString();
                    return RedirectToAction("AlumniStep3");
                }



                RegistrationStep2ViewModel UserDTO = new RegistrationStep2ViewModel()
                {
                    Email = Session["EmailId"].ToString(),
                    Salutation = SaluationService.GetSaluations(),
                    //States = StatecitydistrictService.GetAllStates()
                };

                return View(UserDTO);
            }
            catch (SystemException ex)
            {
                return RedirectToAction("Register");

            }

        }

        [AllowAnonymous]
        public ActionResult ContactInformation()
        {
            ContactInformation Info = new Models.ContactInformation()
            {
                Countrys = GenericMethodsservices.GetAllCountries(),
            };
            return View(Info);
        }
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetStates(int Countryid)
        {
            IEnumerable<States> State = StatecitydistrictService.GetAllStates(Countryid);
            return Json(State, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
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
                    //HomeCityId = ContactHostelsInfoDTO.CityId,
                    //LivesInCityId = ContactHostelsInfoDTO.PermanentCityId,
                    PermanentCity = ContactHostelsInfoDTO.AlternatecityName,
                    PresentCity = ContactHostelsInfoDTO.cityName,
                    PermanentDistid = ContactHostelsInfoDTO.Districtid,
                    PresentDistid = ContactHostelsInfoDTO.PermenantDistrictId,
                    Address = ContactHostelsInfoDTO.PresentAddress,

                    //  CountryCode = ContactHostelsInfoDTO.CountryCodes,
                    HomePhoneNumber = ContactHostelsInfoDTO.AlternameMobile,
                    ProfileInfoPercentage = (int)CustomStatus.ProfileInfoPercentage.ContactInformation,

                };
                UserService.UpdateContactInfo(userDetails);
                return RedirectToAction("ProfessionalDetails");
            }
            else
            {
                return RedirectToAction("Login");
            }

            ContactInformation Info = new Models.ContactInformation()
            {
                Countrys = GenericMethodsservices.GetAllCountries(),
                //States = StatecitydistrictService.GetAllStates()
            };
            return View(Info);
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ProfessionalDetails()
        {
            DateTime CurrentDate = DateTime.Now;
            int Year = CurrentDate.Year;
            List<Graduation> _GraduationYears = new List<Graduation>();
            for (int i = 1970; i <= Year; i++)
                _GraduationYears.Add(new Graduation { GraduationYear = "" + i, GraduationYearId = i });
            ProfessionalDetails details = new ProfessionalDetails()
            {
                GraduationYears = _GraduationYears
            };
            return View(details);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ProfessionalDetails(ProfessionalDetails Profdetails)
        {
            if (ModelState.IsValid)
            {
                if (Session["UserId"] != null)
                {

                    UserDetail userdetails = new UserDetail()
                    {
                        Id = Convert.ToInt32(Session["UserId"].ToString()),
                        ProfileInfoPercentage = (int)CustomStatus.ProfileInfoPercentage.Complete
                    };
                    ProfessionalDetail Profdetail = new ProfessionalDetail()
                    {
                        UserId = Convert.ToInt32(Session["UserId"].ToString()),
                        Status = true,
                        Company = Profdetails.Company,
                        Designation = Profdetails.Designation,
                        WorkedFrom = Profdetails.WorkedFrom,
                        WorkedTill = Profdetails.WorkedTill

                    };
                    UserService.UpdateUser(userdetails);
                    Professionaldetailsservice.Create(Profdetail);
                    Session.Clear();
                    TempData["Success"] = "your registration process is sucessfully completed..";
                    return RedirectToAction("Login");
                }
                else
                {
                    return RedirectToAction("Login");
                }

            }
            var Date = DateTime.Now.Year;
            List<Graduation> _GraduationYears = new List<Graduation>();
            for (int i = 1970; i <= Date; i++)
                _GraduationYears.Add(new Graduation { GraduationYear = "" + i, GraduationYearId = i });
            ProfessionalDetails details = new ProfessionalDetails()
            {
                GraduationYears = _GraduationYears
            };
            return View(details);
        }

        /// <summary>
        /// Step3 Registration form
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Step3Registration()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult AlumniStep3()
        {
            if (Session["AspnetUserId"] != null)
            {

                StudentRegistrationModel userdto = new StudentRegistrationModel()
                {
                    Coursecategorys = CourseCategoryService.GetAllCourseCategories(),
                };

                return View(userdto);
            }
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult AlumniStep3(StudentRegistrationModel StudentDetails)
        {
            string userid = "";
            if (Session["AspnetUserId"] != null)
            {
                userid = Session["AspnetUserId"].ToString();

                if (ModelState.IsValid)
                {
                    EducationalDetail details = new EducationalDetail();
                    TryUpdateModel(details);
                    details.UserId = Convert.ToInt64(Session["UserId"]);
                    details.Status = true;
                    ApplicationUser applicationUser = new ApplicationUser();
                    applicationUser = UserManager.FindById(userid);
                    details.Email = applicationUser.UserName;
                    details.MobileNumber = applicationUser.PhoneNumber;
                    EducationalDetailService.Create(details);
                    UserDetail userDetails = new UserDetail()
                    {
                        Id = Convert.ToInt64(details.UserId),
                        ProfileInfoPercentage = (int)CustomStatus.ProfileInfoPercentage.EducationDetails,
                    };
                    UserService.UpdateUser(userDetails);
                    UserManager.AddToRole(userid, "Alumini");

                    return RedirectToAction("Contactinformation", "Account", new { area = "" });

                }
                else
                {
                    StudentRegistrationModel userdto = new StudentRegistrationModel()
                    {
                        Coursecategorys = CourseCategoryService.GetAllCourseCategories(),
                    };
                    return View(userdto);

                }
            }

            return RedirectToAction("Login");
        }




        //[HttpPost]
        //[AllowAnonymous]
        //public ActionResult AlumniStep3()
        //{
        //    return View();
        //}



        [AllowAnonymous]
        public PartialViewResult StudentRegistration()
        {
            StudentRegistrationModel userdto = new StudentRegistrationModel()
            {
                Coursecategorys = CourseCategoryService.GetAllCourseCategories(),
            };
            return PartialView("StudentRegistrationForm", userdto);
        }


        [AllowAnonymous]
        public PartialViewResult FacultyRegistration()
        {
            List<GraduationYear> _GraduationYears = new List<GraduationYear>();
            for (int i = 1970; i <= 2015; i++)
                _GraduationYears.Add(new GraduationYear { Year = "" + i, GraduationYearId = i });
            FacultyRegistrationModel Faculty = new FacultyRegistrationModel()
            {
                GraduationYears = _GraduationYears,
            };
            return PartialView("FacultyRegistrationForm", Faculty);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult FacultyRegistration(FacultyRegistrationModel FacultyRegistration)
        {
            string userid = "";
            if (null != Session["AspnetUserId"])
                userid = Session["AspnetUserId"].ToString();
            if (ModelState.IsValid)
            {
                FacultyWorkInfo details = new FacultyWorkInfo();
                TryUpdateModel(details);
                details.FacultyUserId = Convert.ToInt64(Session["UserId"]);
                details.Status = true;
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser = UserManager.FindById(userid);
                details.Email = applicationUser.UserName;
                details.MobileNumber = applicationUser.PhoneNumber;
                FacultyWorkInfoService.Create(details);
                UserDetail userDetails = new UserDetail()
                {
                    Id = Convert.ToInt64(details.FacultyUserId),
                    ProfileInfoPercentage = (int)CustomStatus.ProfileInfoPercentage.EducationDetails,
                };
                UserService.UpdateUser(userDetails);
                UserManager.AddToRole(userid, "Faculty");
                return JavaScript("window.location = '../Account/Contactinformation'");
            }
            else
            {
                List<GraduationYear> _GraduationYears = new List<GraduationYear>();
                for (int i = 1970; i <= 2015; i++)
                    _GraduationYears.Add(new GraduationYear { Year = "" + i, GraduationYearId = i });
                FacultyRegistrationModel Faculty = new FacultyRegistrationModel()
                {
                    GraduationYears = _GraduationYears,
                };
                return PartialView("FacultyRegistrationForm", Faculty);
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetDistricts(int StateId)
        {
            IEnumerable<District> districts = StatecitydistrictService.GetDistrictsByStateId(StateId);
            return Json(districts, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetCourses(int CourseCategoryId)
        {
            IEnumerable<Cours> Courses = CourseServises.GetAllCoursesByCategoryId(CourseCategoryId);
            return Json(Courses, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetCities(int DistrictId)
        {
            IEnumerable<City> districts = StatecitydistrictService.GetCitiesByDistrictId(DistrictId);
            return Json(districts, JsonRequestBehavior.AllowGet);

        }
        //
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetYears(int CourseId)
        {
            IEnumerable<GraduationYear> _GraduationYears = Graduationyearservice.GetGraduationYearByCourseId(CourseId);
            return Json(_GraduationYears, JsonRequestBehavior.AllowGet);
        }
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            if (Session["AspnetUserId"] != null)
            {
                UserDetail detailsusers = UserService.GetUser(Session["AspnetUserId"].ToString());
                ViewBag.Profilestatus = detailsusers.ProfileInfoPercentage;
                return View();
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {

            //var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();

            //if (loginInfo == null)
            //{
            //    return RedirectToAction("Login");
            //}

            //if (loginInfo.Login.LoginProvider == "Facebook")
            //{
            //    var identity = AuthenticationManager.GetExternalIdentity(DefaultAuthenticationTypes.ExternalCookie);
            //    var access_token = identity.FindFirstValue("FacebookAccessToken");
            //    var fb = new FacebookClient(access_token);
            //    dynamic myInfo = fb.Get("/me?fields=email"); // specify the email field
            //    loginInfo.Email = myInfo.email;
            //}


            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var CheckEmailId = UserManager.FindByEmail(model.Email);
            if (CheckEmailId != null)
            {
                TempData["ErrorMessage"] = "Email already exists Please click To login ..";
                return RedirectToAction("Register");
            }
            RegistrationStep2ViewModel UserDTO = new RegistrationStep2ViewModel()
            {
                Salutation = SaluationService.GetSaluations(),
                //States = StatecitydistrictService.GetAllStates(),
                Email = model.Email

            };
            //TempData["EmailId"] = model.Email;
            Session["EmailId"] = model.Email;
            return RedirectToAction("UserRegistration");
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]

        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]

        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]

        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]

        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        //[AllowAnonymous]
        //public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        //{
        //    //try
        //    //{
        //    //  AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        //    try
        //    {
        //        var loginInfo = await AuthenticationManager.AuthenticateAsync(DefaultAuthenticationTypes.ExternalCookie);

        //        if (loginInfo == null)
        //        {
        //            return RedirectToAction("Login");
        //        }
        //        // var idClaim = loginInfo.Identity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

        //        //if (loginInfo.Login.LoginProvider == "Facebook")
        //        //{

        //        var identity = AuthenticationManager.GetExternalIdentity(DefaultAuthenticationTypes.ExternalCookie);
        //        var access_token = identity.FindFirstValue("FacebookAccessToken");
        //        var fb = new FacebookClient(access_token);
        //        dynamic myInfo = fb.Get("/me?fields=email");
        //        var emailId = myInfo[0];
        //        Session["EmailId"] = emailId;
        //        Session["ExternalLogin"] = "Facebook";

        //      //  return RedirectToAction("UserRegistration");
        //    }
        //    catch(SystemException ex)
        //    {
        //        throw ex;
        //        //return RedirectToAction("UserRegistration");
        //    }
        //    return RedirectToAction("UserRegistration");
        //    //catch (SystemException ex)
        //    //{
        //    //    return RedirectToAction("UserRegistration");
        //    //}
        //    //// Sign in the user with this external login provider if the user already has a login
        //    //var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
        //    //var UserName = User.Identity.GetUserName();

        //    //switch (result)
        //    //{
        //    //    case SignInStatus.Success:
        //    //        return RedirectToLocal(returnUrl);
        //    //    case SignInStatus.LockedOut:
        //    //        return View("Lockout");
        //    //    case SignInStatus.RequiresVerification:
        //    //        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
        //    //    case SignInStatus.Failure:
        //    //    default:
        //    //        // If the user does not have an account, then prompt the user to create an account
        //    //        ViewBag.ReturnUrl = returnUrl;
        //    //        ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
        //    //        return View("Register", new ApplicationUser { Email = loginInfo.Email });
        //    //}
        //}

        //
        // GET: /Account/ExternalLoginCallback






        //ect

        //
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            try
            {
                ExternalLoginInfo LoginEx = null;
                var VarExternalLogin = await AuthenticationManager.AuthenticateAsync(DefaultAuthenticationTypes.ExternalCookie);
                if (VarExternalLogin == null)
                {
                    return RedirectToAction("Register");
                }
                //  AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));

                var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (VarExternalLogin != null && VarExternalLogin.Identity != null)
                {
                    var idClaim = VarExternalLogin.Identity.FindFirst(ClaimTypes.NameIdentifier);
                }
                else
                {

                    return RedirectToAction("Register");
                }


                Session["EmailId"] = loginInfo.Email;
                Session["ExternalLogin"] = "Facebook";


                ViewBag.LoginProvider = "Welcome";
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });

            }
            catch (SystemException ex)
            {
                return RedirectToAction("Register");
            }

        }

        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]

        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            //if (User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Index", "Manage");
            //}

            //if (ModelState.IsValid)
            //{
            //    // Get the information about the user from the external login provider
            // var info = await AuthenticationManager.GetExternalLoginInfoAsync();
            //    if (info == null)
            //    {
            //        return View("ExternalLoginFailure");
            //    }
            //    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            //    var result = await UserManager.CreateAsync(user);
            //    if (result.Succeeded)
            //    {
            //        result = await UserManager.AddLoginAsync(user.Id, info.Login);
            //        if (result.Succeeded)
            //        {
            //            await SignInManager.SignInAsync(user, isPersistent: true, rememberBrowser: true);
            //            return RedirectToAction("UserRegistration");

            //        }
            //    }

            //}
            //return JavaScript("window.location = '../Account/UserRegistration'");
            return RedirectToAction("UserRegistration");
        }

        //
        // POST: /Account/LogOff

        public ActionResult LogOff()
        {
            Session.RemoveAll();
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
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

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}