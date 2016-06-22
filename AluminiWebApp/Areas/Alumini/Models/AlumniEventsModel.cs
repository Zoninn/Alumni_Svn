using AluminiRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AluminiWebApp.Areas.Alumini.Models
{
    public class AlumniEventsModel
    {
        public List<int> EventTypeId { get; set; }
        public List<decimal> TicketAmount { get; set; }
        public List<int> TicketsQuantity { get; set; }     
        public DateTime EventSelectDate { get; set; }
        public int EventId { get; set; }
    }
    public class MyEventsModel
    {
        public IEnumerable<Events> UserPurchasedEvents { get; set; }
    }

    public class JobPosting
    {
        [Required(ErrorMessage = "Company name is Required")]
        public string ComanyName { get; set; }
        [Required(ErrorMessage = "Job title is Required")]
        public string JobTitle { get; set; }
        [Required(ErrorMessage = "Experience is Required")]
        public string ExperienceRequired { get; set; }
        [Required(ErrorMessage = "Location is Required")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Comapny url is required")]
        public string ComanyUrl { get; set; }
        [Required(ErrorMessage = "Contact Number is Required")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Contact Email is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Job Description is Required")]
        public string Description { get; set; }
        public string Date { get; set; }
        public string Filepath { get; set; }
        public Int64 Userid { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Status { get; set; }
        public int JobId { get; set; }
        [Required(ErrorMessage = "Please enter skills")]
        public string Skills { get; set; }
        [Required(ErrorMessage = "Please enter Salary")]
        public string Salary { get; set; }
        [Required(ErrorMessage = "Qualification is Required")]
        public string Qualification { get; set; }
        [Required(ErrorMessage = "Role is Required")]
        public string Role { get; set; }
        public int Jobid { get; set; }

    }
}