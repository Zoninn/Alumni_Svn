using Alumini.Core;
using Alumini.Logger;
using AluminiRepository;
using AluminiRepository.Interfaces;
using AluminiService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AluminiService
{
    public class UserInfoService : IUserInfoService
    {

        private readonly ILogger _logger;
        private readonly IUserInfoRepository _userInfoRepo;


        public UserInfoService(ILogger _logger, IUserInfoRepository _userInfoRepo)
        {
            this._logger = _logger;
            this._userInfoRepo = _userInfoRepo;
        }


        public UserDetail Create(UserDetail obj)
        {
            try
            {
                return _userInfoRepo.Create(obj);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);

                throw ex;
            }
        }
        // Update user details
        public UserDetail UpdateUser(UserDetail obj)
        {
            try
            {
                return _userInfoRepo.UpdateUser(obj);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);

                throw ex;
            }
        }
        public int? GetUserProfileStatusonUserId(string EmailId)
        {
            try
            {
                return _userInfoRepo.GetUserStatusWhenEmailIdExists(EmailId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);

                throw ex;
            }
        }

        public UserDetail Get(int id)
        {
            try
            {
                return _userInfoRepo.Get(id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);

                throw ex;
            }
        }

        public UserDetail Update(int id)
        {
            throw new NotImplementedException();
        }
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        public UserDetail UpdateBasicInformation(int id, UserDetail Userdata)
        {
            try
            {
                return _userInfoRepo.UpdateUserbasicInformation(id, Userdata);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);

                throw ex;
            }
        }


        //  Get User details by AspnetUsersId
        public UserDetail GetUser(string aspnetUserId)
        {
            try
            {
                return _userInfoRepo.GetUser(aspnetUserId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);

                throw ex;
            }
        }
        // Get users by User status
        public IEnumerable<UserDetail> GetAllRegistrationsByStatus(int UserStatus)
        {
            try
            {
                return _userInfoRepo.GetAllRegistrationsByStatus(UserStatus);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                throw ex;
            }
        }
        public UserDetail UpdateUserProfileStatus(int UserId, int UserStatus)
        {
            try
            {
                return _userInfoRepo.UpdateUserProfileStatus(UserId, UserStatus);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);

                throw ex;
            }
        }


        public UserDetail UpdateContactInfo(UserDetail userDetails)
        {
            try
            {
                return _userInfoRepo.UpdateContactInfo(userDetails);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);

                throw ex;
            }
        }

        public UserDetailsDTO GetUserContactInformation(int id)
        {
            try
            {
                return _userInfoRepo.GetUserContactInformation(id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);

                throw ex;
            }
        }
        public List<Images> getUserImages(int Userid)
        {
            return _userInfoRepo.getUserImages(Userid);
        }
    }
}
