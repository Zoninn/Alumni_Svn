using AluminiRepository;
using AluminiWebApp.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AluminiWebApp.Areas.Admin.Models
{
    public class EventModel
    {
        [Required(ErrorMessage = "Event heading is Required")]
        public string EventName { get; set; }
        [Required(ErrorMessage = "Banner Image is Required")]
        public string BannerImage { get; set; }
        [Required(ErrorMessage = "Start date is Required")]
        public DateTime? EventStartdate { get; set; }
        [Required(ErrorMessage = "End date is Required")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "please provide a PhoneNumber")]
        [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string MobileNumber { get; set; }
        public string Landline { get; set; }
        [Required(ErrorMessage = "Email is Reguired")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Venue is Required")]
        public string EventVenue { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        public string EventDescription { get; set; }
        public int? VisitorsCount { get; set; }
        [Required(ErrorMessage = "No of Tickets is Required")]
        public int? TotalNoOfTickets { get; set; }
        [Required(ErrorMessage = "Ticket Type is Required")]
        public List<string> Heading { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        public List<string> Description { get; set; }
        [Required(ErrorMessage = "Price is Required")]
        public List<decimal> Price { get; set; }
        [Required(ErrorMessage = "Tickets Quantity is Required")]
        public List<int> Quantity { get; set; }
        public List<int> TicketTypeId { get; set; }       
        public string Gallery { get; set; }
        public int EventId { get; set; }
        [Required(ErrorMessage = "Please select")]
        public string StartTime { get; set; }
        [Required(ErrorMessage = "Please select")]
        public string StartAfter { get; set; }
        [Required(ErrorMessage = "Please select")]
        public string StartEve { get; set; }
        [Required(ErrorMessage = "Please select")]
        public string EndTime { get; set; }
        public List<EventDates> EventStartDates { get; set; }
        public List<EventsAMS> AMS { get; set; }
    }


    public class EventTypesModel
    {
        public int TypeId { get; set; }
        public string Heading { get; set; }
        public int EventId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public bool Status { get; set; }
    }

    public class WhiteBoard
    {
        public IEnumerable<UserPostsData> UserPosts { get; set; }
    }


}