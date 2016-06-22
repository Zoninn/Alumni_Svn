using Alumini.Logger;
using AluminiRepository.Interfaces;
using AluminiService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alumini.Core;

namespace AluminiService
{
    public class UserPostVisibleServices : IUserPostVisibleService
    {
        private readonly ILogger _logger;
        private readonly IUserPostVisibleRepository _userInfoRepo;


        public UserPostVisibleServices(ILogger _logger, IUserPostVisibleRepository _userInfoRepo)
        {
            this._logger = _logger;
            this._userInfoRepo = _userInfoRepo;
        }

        public UserPosts_Visisble Create(UserPosts_Visisble obj)
        {
            return _userInfoRepo.Create(obj);
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public UserPosts_Visisble Get(int id)
        {
            throw new NotImplementedException();
        }

        public UserPosts_Visisble Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
