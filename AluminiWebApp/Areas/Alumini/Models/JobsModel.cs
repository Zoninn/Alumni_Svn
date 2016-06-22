using Alumini.Core;
using AluminiRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AluminiWebApp.Areas.Alumini.Models
{
    public class JobsModel
    {
        public IEnumerable<UserJobPostings> singleJobs { get; set; }
    }
}