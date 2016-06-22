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
    public class UserDetailsViewService : IUserDetailsViewService
    {
        private readonly Alumini.Logger.ILogger _logger;
        private readonly IUserDetailsViewRepository _userDetailsViewRepo;
        public UserDetailsViewService(Alumini.Logger.ILogger _logger, IUserDetailsViewRepository _userDetailsViewRepo)
        {
            this._logger = _logger;
            this._userDetailsViewRepo = _userDetailsViewRepo;
        }
        public IEnumerable<View_UserDetails> GetAllUserDetailsByUserRoleAndStatus(int roleId, int userStatusId)
        {
            return _userDetailsViewRepo.GetAllUserDetailsByUserRoleAndStatus(roleId, userStatusId);
        }

        public View_UserDetails GetUserByUserId(int userId)
        {
            return _userDetailsViewRepo.GetUserByUserId(userId);
        }
        public IEnumerable<View_UserDetails> GetUserserachoncourse(string Coursecategory, string course, string year)
        {
            return _userDetailsViewRepo.GetUserserachoncourse(Coursecategory, course, year);
        }

        public List<View_UserDetails> GetUserRolesCount(int UserId)
        {
            return _userDetailsViewRepo.GetUserRolesCount(UserId);
        }
        public IEnumerable<View_UserDetails> GetAllUsers()
        {
            return _userDetailsViewRepo.GetAllUsers();
        }
        public IEnumerable<View_UserDetails>GetEmailsonEmailsearch(string Email,int id)
        {
            return _userDetailsViewRepo.GetEmailsonEmailsearch(Email,id);
        }
        public IEnumerable<View_UserDetails> GetAllDetailsonserachbyAdmin(string Degree, string Course, int Batch, int? Userid)
        {
            return _userDetailsViewRepo.GetAllDetailsonserachbyAdmin(Degree, Course, Batch, Userid);
        }
        //public List<View_UserDetails> GetUserDetailsonSearchbased(string Name, int id, string Designation)
        //{
        //    return _userDetailsViewRepo.GetUserDetailsonSearchbased(Name, id, Designation);
        //}
    }
}
