using Alumini.Core;
using Alumini.Logger;
using AluminiRepository.Interfaces;
using AluminiService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiService
{
    public class UserPostService : IUserPostService
    {
        private readonly ILogger _logger;
        private readonly IUserPostRepository _userInfoRepo;


        public UserPostService(ILogger _logger, IUserPostRepository _userInfoRepo)
        {
            this._logger = _logger;
            this._userInfoRepo = _userInfoRepo;
        }



        public UserPost Create(UserPost obj)
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

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        public int DeleteUserComment(int id)
        {
            return _userInfoRepo.DeleteUserComment(id);
        }

        public UserPost Get(int id)
        {
            throw new NotImplementedException();
        }

        public int InsertUserPosts(UserPost obj)
        {
            return _userInfoRepo.InsertUserPosts(obj);
        }

        public UserPost Update(int id)
        {
            return _userInfoRepo.Update(id);
        }
    }
}
