using Alumini.Core;
using AluminiRepository.Factories;
using AluminiRepository.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiRepository
{
    public class StateDistrictCityRepository : IStateDistrictCityRepository
    {


        private readonly Alumini.Logger.ILogger _Logger;
        private readonly IDbConnectionFactory _dbContextFactory;

        public StateDistrictCityRepository(Alumini.Logger.ILogger _Logger, IDbConnectionFactory _DbContextFactory)
        {
            this._Logger = _Logger;
            this._dbContextFactory = _DbContextFactory;
        }

        public List<States> GetStates(int Countryid)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                List<States> States = (from a in context.States
                                       where a.CountryId == Countryid && a.Status == true
                                       select new States { StateId = a.Id, StateName = a.StateName }).ToList();
                return States;

            }
        }

        public List<District> GetDistricts(int stateId)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                context.Configuration.ProxyCreationEnabled = false;
                List<District> Districts = context.Districts.Where(x => x.StateId == stateId && x.Status == true).ToList();
                return Districts;

            }
        }

        public List<City> GetCities(int districtId)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                context.Configuration.ProxyCreationEnabled = false;
                List<City> Districts = context.Cities.Where(x => x.Stateid == districtId && x.Status == true).ToList();
                return Districts;
            }
        }
    }
}
