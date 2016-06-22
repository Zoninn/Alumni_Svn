using Alumini.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiRepository.Interfaces
{
    public interface IStateDistrictCityRepository
    {
        List<States> GetStates(int Countryid);
        List<District> GetDistricts(int stateId);
        List<City> GetCities(int cityId);
    }
}
