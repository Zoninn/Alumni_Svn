using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alumini.Core;
using Alumini.Logger;
using AluminiRepository.Interfaces;
using AluminiRepository.Factories;
namespace AluminiRepository
{
    public class UserDetailsViewRepository : IUserDetailsViewRepository
    {
        private readonly Alumini.Logger.ILogger _Logger;
        private readonly IDbConnectionFactory _dbContextFactory;
        private readonly IUserDetailsViewRepository _userDetailsViewRepository;

        public UserDetailsViewRepository(Alumini.Logger.ILogger _Logger, IDbConnectionFactory _DbContextFactory)
        {
            this._Logger = _Logger;
            this._dbContextFactory = _DbContextFactory;
        }
        public IEnumerable<View_UserDetails> GetAllUserDetailsByUserRoleAndStatus(int roleId, int userStatusId)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    return context.View_UserDetails.Where(a => (a.RoleId == roleId.ToString() || roleId == 0) && (a.UserStatus == userStatusId || userStatusId == 3)).ToList();
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public IEnumerable<View_UserDetails> GetEmailsonEmailsearch(string Email,int id)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                if (id == 1)
                {
                    return context.View_UserDetails.Where(x => x.Email.Contains(Email)).ToList();
                }
                else
                {
                    return context.View_UserDetails.Where(x => x.FirstName.Contains(Email)).ToList();
                }
            }
        }

        public IEnumerable<View_UserDetails> GetAllDetailsonserachbyAdmin(string Degree, string Course, int Batch, int? Userid)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                if (Degree != null && Course != "" && Batch != 0)
                {
                    return context.View_UserDetails.Where(a => a.CourseCategoryName == Degree && a.CourseName == Course && a.Batch == Batch).ToList();
                }
                else if (Degree != null && Course != "")
                {
                    return context.View_UserDetails.Where(a => a.CourseCategoryName == Degree && a.CourseName == Course).ToList();
                }
                else if (Degree != null)
                {
                    return context.View_UserDetails.Where(a => a.CourseCategoryName == Degree).ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        public View_UserDetails GetUserByUserId(int userId)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    return context.View_UserDetails.Where(a => a.UserId == userId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;

            }
        }
        public List<View_UserDetails> GetUserRolesCount(int UserId)
        {
            List<View_UserDetails> Roles = new List<View_UserDetails>();
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    int Count = context.View_UserDetails.Where(a => a.UserId == UserId).Count();
                    return Roles = context.View_UserDetails.Where(x => x.UserId == UserId).ToList();
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;

            }
        }

        public IEnumerable<View_UserDetails> GetAllUsers()
        {

            using (var context = _dbContextFactory.CreateConnection())
            {
                IEnumerable<View_UserDetails> Userdetails = context.View_UserDetails.Where(x => x.UserStatus == 1).ToList().OrderBy(x => x.UserId);
                return Userdetails;
            }

        }

        public IEnumerable<View_UserDetails> GetUserserachoncourse(string Coursecategory, string course, string year)
        {

            using (var context = _dbContextFactory.CreateConnection())
            {
                int Yeras = Convert.ToInt32(year);
                IEnumerable<View_UserDetails> Userdetails = context.View_UserDetails.Where(x => x.CourseCategoryName == Coursecategory && x.CourseName == course && x.Batch == Yeras).ToList();
                return Userdetails;
            }

        }

    }
}
