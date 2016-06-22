using Alumini.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiRepository.Interfaces
{
    public interface IUserPostRepository : GenericCRUDRepository<UserPost>
    {
         int InsertUserPosts(UserPost obj);
         int DeleteUserComment(int id);
    }
}
