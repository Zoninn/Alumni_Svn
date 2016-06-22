using Alumini.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiRepository.Interfaces
{
    public interface IUserInfoRepository : GenericCRUDRepository<UserDetail>
    {
        UserDetail UpdateUser(UserDetail obj);
        UserDetail GetUser(string aspnetUserId);
        IEnumerable<UserDetail> GetAllRegistrationsByStatus(int Status);
        UserDetail UpdateUserProfileStatus(int UserId, int UserStatus);
        UserDetail UpdateContactInfo(UserDetail userDetails);
        int? GetUserStatusWhenEmailIdExists(string EmailId);
        UserDetail UpdateUserbasicInformation(int UserId, UserDetail Userdata);
        UserDetailsDTO GetUserContactInformation(int id);
        List<Images> getUserImages(int Userid);
    }
}
