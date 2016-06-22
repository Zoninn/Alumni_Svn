using Alumini.Core;
using AluminiWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alumini.Core;
using System.ComponentModel.DataAnnotations;
using AluminiRepository;

namespace AluminiWebApp.Areas.Admin.Models
{
    public class UserRegistrationModel
    {
        public CustomStatus.UserTypes UserTypes { get; set; }
        public CustomStatus.UserStatus UserStatus { get; set; }

    }


    public class BannerImages
    {
        [Required(ErrorMessage = "Required")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Text is required")]
        public string Text { get; set; }
        [Required(ErrorMessage = "Tags is required")]
        public string Tags { get; set; }
        public int id { get; set; }
        public List<Banner_Imags> HomeBannerImages { get; set; }
    }
    public class UserInfoViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int HomeCityId { get; set; }
        public string HomeVillegeOrAreaName { get; set; }
        public int LivesInCityId { get; set; }
        public Nullable<int> LivesInVillegeOrAreaName { get; set; }
        public Nullable<int> SalutationId { get; set; }
        public int GenderId { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string ProfilePicture { get; set; }
        public int StateId { get; set; }
        public IEnumerable<State> States { get; set; }
        public IEnumerable<City> Citys { get; set; }
        public IEnumerable<Salutation> Salutation { get; set; }
    }


    public class ExecutiveBoard
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Enter full name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Role")]
        public string Role { get; set; }
        [Required(ErrorMessage = "Enter email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter mobile")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Enter image")]
        public string Image { get; set; }
        public List<Executive_board> ExecutiveBoardDetail { get; set; }
    }


    public class NewsRoomModel
    {
        [Required(ErrorMessage = "Please write the Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please write the description")]
        public string Description { get; set; }
        public string Image { get; set; }
        public List<db_NewsRooms> NewsRoom { get; set; }
        public int NewsId { get; set; }
        public DateTime? PostedOn { get; set; }
        public List<NewsRooms> GetNews { get; set; }
    }

    public class GalleryModel
    {
        public string Images { get; set; }
        public string Videos { get; set; }
        [Required(ErrorMessage = "Album Name is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Album date is required")]
        public DateTime GalleryDate { get; set; }
        [Required(ErrorMessage = "Please select Upload type")]
        public int selectionType { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public List<UserAlbumGallery> Albums { get; set; }
    }

}