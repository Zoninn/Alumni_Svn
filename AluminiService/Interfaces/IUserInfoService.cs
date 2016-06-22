using Alumini.Core;
using Alumini.Logger;
using AluminiRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alumini.Core;

namespace AluminiService.Interfaces
{
        /// <summary>
    /// This service is used to manage Bussiness operations on User Entity
    /// </summary>
    public interface IUserInfoService : GenericCRUDService<UserDetail>
    {
        UserDetail UpdateUser(UserDetail obj);
        UserDetail GetUser(string aspnetUserId);
        IEnumerable<UserDetail> GetAllRegistrationsByStatus(int UserStatus);
        UserDetail UpdateUserProfileStatus(int UserId, int UserStatus);
        UserDetail UpdateContactInfo(UserDetail userDetails);
        int? GetUserProfileStatusonUserId(string EmailId);
        UserDetail UpdateBasicInformation(int id, UserDetail Userdata);
        UserDetailsDTO GetUserContactInformation(int id);
        List<Images> getUserImages(int Userid);
    }
}
