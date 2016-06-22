using AluminiRepository.Interfaces;
using AluminiService.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alumini.Core;

namespace AluminiService
{
    public class StateDistrictCityService : IStateDistrictCityService
    {

        private readonly Alumini.Logger.ILogger _logger;
         private readonly IStateDistrictCityRepository _stateDistrictCityRepo;


         public StateDistrictCityService(IStateDistrictCityRepository _stateDistrictCityRepo, Alumini.Logger.ILogger _logger)
        {
            this._logger = _logger;
            this._stateDistrictCityRepo = _stateDistrictCityRepo;
        }

         public IEnumerable<States> GetAllStates(int Countryid)
        {
            try
            {
                return _stateDistrictCityRepo.GetStates(Countryid);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public IEnumerable<Alumini.Core.District> GetDistrictsByStateId(int stateId)
        {
            try
            {
                return _stateDistrictCityRepo.GetDistricts(stateId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public IEnumerable<Alumini.Core.City> GetCitiesByDistrictId(int districtId)
        {
            try
            {
                return _stateDistrictCityRepo.GetCities(districtId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                throw ex;
            }
        }


        public IEnumerable<City> GetCities(int StateId)
        {
            return _stateDistrictCityRepo.GetCities(StateId);
        }
    }
}
