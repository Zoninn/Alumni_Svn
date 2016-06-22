using AluminiRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alumini.Core;
using Alumini.Logger;
using AluminiRepository.Factories;

namespace AluminiRepository
{
    public class UserPostVisibleRepository : IUserPostVisibleRepository
    {
        private readonly ILogger _Logger;
        private readonly IDbConnectionFactory _dbContextFactory;

        public UserPostVisibleRepository(ILogger _Logger, IDbConnectionFactory _DbContextFactory)
        {
            this._Logger = _Logger;
            this._dbContextFactory = _DbContextFactory;
        }

        public UserPosts_Visisble Create(UserPosts_Visisble obj)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    obj = context.UserPosts_Visisble.Add(obj);
                    context.SaveChanges();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }
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
