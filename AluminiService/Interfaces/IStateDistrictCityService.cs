using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alumini.Core;

namespace AluminiService.Interfaces
{
    public interface IStateDistrictCityService
    {
        IEnumerable<States> GetAllStates(int Countryid);
        IEnumerable<District> GetDistrictsByStateId(int stateId);
        IEnumerable<City> GetCitiesByDistrictId(int districtId);
        IEnumerable<City> GetCities(int StateId);
    }
}
