using Alumini.Core;
using AluminiRepository;
using AluminiService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AluminiWebApp.Models
{
    public class EventsModel
    {
        public int Eventid { get; set; }
        public string EventHeading { get; set; }
        public string EventAddress { get; set; }
        public string EventDesc { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string EventStartTime { get; set; }
        public string EndTime { get; set; }
        public string Bannerimage { get; set; }
        public List<Events> EventsGoing { get; set; }
        public IEnumerable<Events> DisplayHome { get; set; }
        public string StartTime { get; set; }
        public IEnumerable<UserJobPostings> UserJobs { get; set; }
        public List<NewsRooms> UserNews { get; set; }
        public ActivitiesCounts Activities { get; set; }
        public List<Banner_Imags> BannerImages { get; set; }

    }
    public class Gallery
    {
        public List<Alumni_Gallery> Galleries { get; set; }
    }
    public class UserMembersModel
    {
        public IEnumerable<View_UserDetails> GetUsers { get; set; }
        public IEnumerable<CourseCategory> Coursecategorys { get; set; }
        public int CourseCategoryId { get; set; }
    }
    public class ContactUsModel
    {
        [Required(ErrorMessage = "Enter your Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter your Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter your Mobile")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Enter your Message")]
        public string Message { get; set; }

    }

}