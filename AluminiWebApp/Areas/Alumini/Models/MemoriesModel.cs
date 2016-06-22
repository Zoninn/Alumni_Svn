using AluminiRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AluminiWebApp.Areas.Alumini.Models
{
    public class MemoriesModel
    {
        [Required(ErrorMessage = "Please enter Heading")]
        public string Heading { get; set; }
        [Required(ErrorMessage = "Please enter date")]
        public DateTime date { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Please Select")]
        public string visible { get; set; }  
        [Required]
        public string Image { get; set; }
        public List<Visible> Visibleto { get; set; }
        public List<Memroeis> Memories { get; set; }
        public int memoriesId { get; set; }
    }

    public class Visible
    {
        public string visibleTo { get; set; }
    }
}