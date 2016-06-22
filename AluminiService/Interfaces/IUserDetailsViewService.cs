using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alumini.Core;

namespace AluminiService.Interfaces
{
    public interface IUserDetailsViewService
    {
        IEnumerable<View_UserDetails> GetAllUserDetailsByUserRoleAndStatus(int roleId, int userStatusId);
        View_UserDetails GetUserByUserId(int userId);
        List<View_UserDetails> GetUserRolesCount(int UserId);
        IEnumerable<View_UserDetails> GetAllUsers();
        IEnumerable<View_UserDetails> GetAllDetailsonserachbyAdmin(string Degree, string Course, int Batch, int? Userid);
        IEnumerable<View_UserDetails> GetEmailsonEmailsearch(string Email,int id);
        IEnumerable<View_UserDetails> GetUserserachoncourse(string Coursecategory, string course, string year);
      
      
    }
}
