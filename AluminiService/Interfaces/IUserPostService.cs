using Alumini.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiService.Interfaces
{
    public interface IUserPostService : GenericCRUDService<UserPost>
    {
        int InsertUserPosts(UserPost obj);
        int DeleteUserComment(int id);
    }
}
