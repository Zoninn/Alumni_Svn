using AluminiService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alumini.Core;
using NLog;
using AluminiRepository.Interfaces;

namespace AluminiService
{
    public class EducationalDetailService : IEducationalDetailService
    {

        private readonly Alumini.Logger.ILogger _logger;
        private readonly IEducationalDetailRepository _educationalDetailRepo;
        private readonly IUserInfoRepository _userDetailsRepo;
        private readonly SubscriberClasses objSubscriberClasses;
        UserDetail userObj = new UserDetail();

        public EducationalDetailService(Alumini.Logger.ILogger _logger, IEducationalDetailRepository _educationalDetailRepo, IUserInfoRepository _userDetailsRepo)
        {
            this._logger = _logger;
            this._educationalDetailRepo = _educationalDetailRepo;
            this._userDetailsRepo = _userDetailsRepo;
            objSubscriberClasses = new SubscriberClasses();
        }



        public EducationalDetail Create(EducationalDetail obj)
        {
            try
            {
                _educationalDetailRepo.InsertCompletedEvent += objSubscriberClasses.SendEmail;
                _educationalDetailRepo.InsertCompletedEvent += objSubscriberClasses.SendSMS;
                obj = _educationalDetailRepo.Create(obj);
                //userObj.Id = (long)obj.UserId;
                //userObj.ProfileInfoPercentage = (int)CustomStatus.ProfileInfoPercentage.Complete;
                //_userDetailsRepo.UpdateUser(userObj);
                return obj;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public EducationalDetail Get(int id)
        {
            return _educationalDetailRepo.Get(id);
        }
        public List<EducationdetailsDTO> GetEducationdetails(int id)
        {
            return _educationalDetailRepo.GetEducationdetails(id);
        }
        public EducationalDetail Update(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        public EducationalDetail UpdateEducationDetails(int id, EducationalDetail UpdateEducationDetails)
        {
            return _educationalDetailRepo.UpdateEducationDetails(id, UpdateEducationDetails);
        }
    }
}
