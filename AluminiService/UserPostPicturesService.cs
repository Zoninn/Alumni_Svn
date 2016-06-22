using AluminiService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alumini.Core;
using AluminiRepository.Interfaces;
using Alumini.Logger;

namespace AluminiService
{
    public class UserPostPicturesService : IUserPostPicturesService
    {
        private readonly ILogger _logger;
        private readonly IUserPostPicturesRepository _userInfoRepo;


        public UserPostPicturesService(ILogger _logger, IUserPostPicturesRepository _userInfoRepo)
        {
            this._logger = _logger;
            this._userInfoRepo = _userInfoRepo;
        }

        public UserPost_Images Create(UserPost_Images obj)
        {
            return _userInfoRepo.Create(obj);
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public UserPost_Images Get(int id)
        {
            throw new NotImplementedException();
        }

        public UserPost_Images Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
