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
    public class UserPostsPicturesRepository : IUserPostPicturesRepository
    {
        private readonly ILogger _Logger;
        private readonly IDbConnectionFactory _dbContextFactory;

        public UserPostsPicturesRepository(ILogger _Logger, IDbConnectionFactory _DbContextFactory)
        {
            this._Logger = _Logger;
            this._dbContextFactory = _DbContextFactory;
        }
        public UserPost_Images Create(UserPost_Images obj)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    obj = context.UserPost_Images.Add(obj);
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
