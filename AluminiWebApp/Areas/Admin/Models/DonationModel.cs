using AluminiRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace AluminiWebApp.Areas.Admin.Models
{
    public class DonationModel
    {
        public long DonationID { get; set; }
        [Required(ErrorMessage = "Donation Title is Required")]
        public string DonationTitle { get; set; }

        [Required(ErrorMessage = "Donation Description is Required")]
        public string DonationDescription { get; set; }


        [Required(ErrorMessage = "Donation Amount is Required")]
        public decimal DonationAmount { get; set; }

        [Required(ErrorMessage = "Banner Image is Required")]
        public HttpPostedFileBase BannerImage { get; set; }
    }
}