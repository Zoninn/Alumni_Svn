using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alumini.Core
{
    public class UserInfo
    {

        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Saluation { get; set; }
        public string EmailId { get; set; }
        public string none { get; set; }
        public string MR { get; set; }
        public string UserImage { get; set; }
        public List<Courses> Courses { get; set; }
        public List<CourseCategorys> CorseCategory { get; set; }
        public List<Graduation> GraduationYears { get; set; }
        public List<States> States { get; set; }
        public List<Districts> Districts { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public string Address { get; set; }
        public int StateId { get; set; }
        public List<Citys> Citys { get; set; }
        public int GraduationYear { get; set; }
        public int CourseCategoryId { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public int TimePeriodFrom { get; set; }
        public int TimePeriodTo { get; set; }
        public int CourseId { get; set; }
    }
    public class Courses
    {
        public int CorseId { get; set; }
        public string CourseName { get; set; }
    }
    public class CourseCategorys
    {
        public int CoureseCategoryId { get; set; }
        public string CourseCategoryName { get; set; }
    }
    public class Graduation
    {
        public int GraduationYearId { get; set; }
        public string GraduationYear { get; set; }
    }
    public class States
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
    }
    public class Districts
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
    }
    public class Citys
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
    }
}
