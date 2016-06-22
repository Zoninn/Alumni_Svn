using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Alumini.Core
{
    public partial class EducationalDetail
    {

        public string Email{ get; set; }
        public string MobileNumber { get; set; }
        public int DegreeId { get; set; }
    }
    public partial class FacultyWorkInfo
    {

        public string Email { get; set; }
        public string MobileNumber { get; set; }
    }

    public class EducationdetailsDTO
    {
        public long Id { get; set; }
        public Nullable<long> UserId { get; set; }
        public int CategoryId { get; set; }
        public string CollegeName { get; set; }
        public Nullable<int> CourseId { get; set; }
        public Nullable<int> Batch { get; set; }
        public Nullable<bool> Status { get; set; }


    }

}
