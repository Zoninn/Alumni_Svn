using Alumini.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AluminiWebApp.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }


    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Enter old Password")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Enter new Password")]
        public string NewPassword { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Re-enter password")]
        [Compare("NewPassword")]
        public string ReenterPassword { get; set; }
    }
    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }



    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        ////[Required]
        ////[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        ////[DataType(DataType.Password)]
        ////[Display(Name = "Password")]
        //public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }
    }



    public class RegistrationStep2ViewModel
    {
        [Required(ErrorMessage = "Please enter the first name.")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter the last name.")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        public string LastName { get; set; }
        //[Required(ErrorMessage = "Please enter Address.")]
        public string Address { get; set; }
        public int HomeCityId { get; set; }
        public string HomeVillegeOrAreaName { get; set; }
        public int LivesInCityId { get; set; }
        public Nullable<int> LivesInVillegeOrAreaName { get; set; }
        [Required(ErrorMessage = "Please select salutation.")]
        public Nullable<int> SalutationId { get; set; }
        [Required(ErrorMessage = "Please select Gender.")]
        public int GenderId { get; set; }
        public string DateofBirth { get; set; }
        public Nullable<bool> Status { get; set; }
        [Required(ErrorMessage = "Please enter Date of Birth.")]       
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd-MM-yyyy}")]
        public Nullable<System.DateTime> DOB { get; set; }
        [Required(ErrorMessage = "Please select Image")]
        public string ProfilePicture { get; set; }
        public int StateId { get; set; }
        public IEnumerable<State> States { get; set; }
        public IEnumerable<City> Citys { get; set; }
        public IEnumerable<Salutation> Salutation { get; set; }
        [Required(ErrorMessage = "Please set your password.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class StudentRegistrationModel
    {
        public string CollegeName { get; set; }
        [Required(ErrorMessage = "Please select Course")]
        public Nullable<int> CourseId { get; set; }
        [Required(ErrorMessage = "Please select Graduation year")]
        public Nullable<int> Batch { get; set; }
        public Nullable<bool> Status { get; set; }
        [Required(ErrorMessage = "Please select degree")]
        public int CourseCategoryId { get; set; }
        public IEnumerable<CourseCategory> Coursecategorys { get; set; }
    }
    public class FacultyRegistrationModel
    {
        [Required(ErrorMessage = "Please enter designation")]
        public string DesignationName { get; set; }
        [Required(ErrorMessage = "Please enter department")]
        public string DepartmentName { get; set; }
        [Required(ErrorMessage = "Please enter working from")]
        public Nullable<int> WorkingFrom { get; set; }
        [Required(ErrorMessage = "Please enter working To")]
        public Nullable<int> WorkingTo { get; set; }
        public IEnumerable<GraduationYear> GraduationYears { get; set; }


    }
    public class ContactInformation
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Telephone Number Required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string Mobile { get; set; }
        public int PermanentCountryid { get; set; }
        public int CountryId { get; set; }
       // public int DistrictId { get; set; }
        //[Required(ErrorMessage = "Please select City")]
        //public int CityId { get; set; }
        public string AlternateEmail { get; set; }
        public IEnumerable<State> States { get; set; }
        public IEnumerable<City> Citys { get; set; }
        [Required(ErrorMessage = "Please select State")]
        public int PermanentStateId { get; set; }
        [Required(ErrorMessage = "Please select district")]
        public int PermenantDistrictId { get; set; }
        //[Required(ErrorMessage = "Please select city")]
        //public int PermanentCityId { get; set; }
        public int StateId { get; set; }
        [Required]
        public string PresentAddress { get; set; }
        [Required]
        public string PermanentAddress { get; set; }
        public IEnumerable<Country> Countrys { get; set; }
        //[Required(ErrorMessage = "Country code is Required")]
        public int CountryCodes { get; set; }
        public string AlternameMobile { get; set; }
        [Required(ErrorMessage="Please enter City Name")]
        public string cityName { get; set; }

        [Required(ErrorMessage = "Please enter City Name")]
        public string AlternatecityName { get; set; }
        [Required(ErrorMessage = "District is required")]
        public int Districtid { get; set; }
    }
    public class ProfessionalDetails
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please enter Company name")]
        public string Company { get; set; }
        [Required(ErrorMessage = "Please enter your Designation")]
        public string Designation { get; set; }
        [Required(ErrorMessage = "Please select Work from")]
        public string WorkedFrom { get; set; }
        [Required(ErrorMessage = "Please select Work To")]
        public string WorkedTill { get; set; }
        public List<Graduation> GraduationYears { get; set; }
        public List<Country> Country { get; set; }
    }

    public class ProfileBasicInformation
    {

        [Required(ErrorMessage = "Please enter the first name.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter the last name.")]
        public string LastName { get; set; }
        //[Required(ErrorMessage = "Please enter Address.")]
        public string Address { get; set; }
        public int HomeCityId { get; set; }
        public string DateofBirth { get; set; }
        public string HomeVillegeOrAreaName { get; set; }
        public int LivesInCityId { get; set; }
        public Nullable<int> LivesInVillegeOrAreaName { get; set; }
        [Required(ErrorMessage = "Please select salutation.")]
        public Nullable<int> SalutationId { get; set; }
        [Required(ErrorMessage = "Please select Gender.")]
        public int GenderId { get; set; }
        public Nullable<bool> Status { get; set; }
        [Required(ErrorMessage = "Please enter Date of Birth.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<System.DateTime> DOB { get; set; }
        // [Required(ErrorMessage = "Please select Image")]

      
        public string ProfilePicture { get; set; }
        public int StateId { get; set; }
        public IEnumerable<State> States { get; set; }
        public IEnumerable<City> Citys { get; set; }
        public IEnumerable<Salutation> Salutation { get; set; }
        public string Email { get; set; }
    }

}
