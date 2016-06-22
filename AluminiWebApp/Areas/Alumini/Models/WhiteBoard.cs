using Alumini.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AluminiWebApp.Areas.Alumini.Models
{
    public class WhiteBoardModel
    {
       
        public int? EventId { get; set; }
        public IEnumerable<EventCategory> Events { get; set; }
        public string RoleId { get; set; }
        public int? Batch { get; set; }
        public string Stream { get; set; }
        public List<Batches> Viewdetails { get; set; }
        [Required(ErrorMessage = "Please select Batch")]
        public string Batchyear { get; set; }        
        public IEnumerable<string> Images { get; set; }
        public List<string> Files { get; set; }
        //[Required(ErrorMessage = "Please write your Post")]
        public string Message { get; set; }
        public List<YearsforFaculty> yearsList { get; set; }
        public string WorkFromYear { get; set; }
        public string WorkFromTo { get; set; }
        public IEnumerable<CourseCategory> Coursecategorys { get; set; }
        public string Degree { get; set; }
    }
    public class Batches
    {
        public string Batch { get; set; }
        public string BatchName { get; set; }
    }
    public class YearsforFaculty
    {
        public string Year { get; set; }
    }

}