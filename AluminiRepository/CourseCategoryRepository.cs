using Alumini.Core;
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
    public class CourseCategoryRepository : ICourseCategoryRepository
    {

        private readonly Alumini.Logger.ILogger _Logger;
        private readonly IDbConnectionFactory _dbContextFactory;

        public CourseCategoryRepository(Alumini.Logger.ILogger _Logger, IDbConnectionFactory _DbContextFactory)
        {
            this._Logger = _Logger;
            this._dbContextFactory = _DbContextFactory;
        }

        public CourseCategory Create(CourseCategory obj)
        {
            throw new NotImplementedException();
        }

        public CourseCategory Get(int id)
        {
            throw new NotImplementedException();
        }

        public CourseCategory Update(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CourseCategory> GetAllCourseCategories()
        {
           using (var context = _dbContextFactory.CreateConnection())
            {
                context.Configuration.ProxyCreationEnabled = false;
                List<CourseCategory> Coursescategorysdata = context.CourseCategories.Where(x=>x.Status==true).ToList();
                return Coursescategorysdata;

            }
        }
    }
}
