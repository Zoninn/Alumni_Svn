using AluminiRepository.Factories;
using AluminiRepository.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alumini.Core;

namespace AluminiRepository
{
    public class CoursesRepository : ICoursesRepository
    {
        private readonly Alumini.Logger.ILogger _Logger;
        private readonly IDbConnectionFactory _dbContextFactory;

        public CoursesRepository(Alumini.Logger.ILogger _Logger, IDbConnectionFactory _DbContextFactory)
        {
            this._Logger = _Logger;
            this._dbContextFactory = _DbContextFactory;
        }
        public IEnumerable<Cours> GetAllCoursesbyCategoryId(int categoryId)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                try {
                    context.Configuration.ProxyCreationEnabled = false;
                    List<Cours> Courses = context.Courses.Where(x => x.CourseCategoryId == categoryId && x.Status == true).ToList();
                    return Courses;
                }
                catch (Exception ex)
                {
                    _Logger.Error(ex.Message, ex);
                    throw ex;
                }
            }
        }

        public Cours Create(Cours obj)
        {
            throw new NotImplementedException();
        }

        public Cours Get(int id)
        {
            throw new NotImplementedException();
        }

        public Cours Update(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
