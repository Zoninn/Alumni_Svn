using Alumini.Logger;
using AluminiRepository.Factories;
using AluminiRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alumini.Core;

namespace AluminiRepository
{
    public class UserPostsRepository : IUserPostRepository
    {
        private readonly ILogger _Logger;
        private readonly IDbConnectionFactory _dbContextFactory;

        public UserPostsRepository(ILogger _Logger, IDbConnectionFactory _DbContextFactory)
        {
            this._Logger = _Logger;
            this._dbContextFactory = _DbContextFactory;
        }

        public UserPost Create(UserPost obj)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    obj = context.UserPosts.Add(obj);
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

        public int DeleteUserComment(int id)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    UserPost_Comments comment = context.UserPost_Comments.Where(x => x.CommentId == id).FirstOrDefault();
                    comment.Status = false;
                    context.SaveChanges();
                    return id;
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }
        }



        public int InsertUserPosts(UserPost obj)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    obj = context.UserPosts.Add(obj);
                    context.SaveChanges();
                    return obj.PostId;
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public UserPost Get(int id)
        {
            throw new NotImplementedException();
        }

        public UserPost Update(int id)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    UserPost User = context.UserPosts.Where(x => x.PostId == id).First();
                    User.Status = false;
                    context.SaveChanges();
                    return User;
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
    }
}
