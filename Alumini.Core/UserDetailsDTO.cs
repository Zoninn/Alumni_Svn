using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alumini.Core
{
    public class UserDetailsDTO : UserDetail
    {
        public IEnumerable<State> States { get; set; }
        public IEnumerable<City> Citys { get; set; }
        public IEnumerable<CourseCategory> Coursecategorys { get; set; }
        public IEnumerable<Salutation> Salutation { get; set; }
        public int? StateId { get; set; }
        public int CityId { get; set; }
        public int? DistrictId { get; set; }
        public int Permanentdistid { get; set; }
        public int? CountryId { get; set; }
        public int? PermanentCountryId { get; set; }
       
        public int? PermanentStateid { get; set; }
        public int? PresentStateid { get; set; }
        public int? PresentCityid { get; set; }
        public int? PermenantCityId { get; set; }
        public string PresentCity { get; set; }
        public string PermanentCity { get; set; }
    }
}
