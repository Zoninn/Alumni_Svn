using AluminiService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alumini.Core;
using AluminiRepository.Interfaces;
using NLog;

namespace AluminiService
{
    public class CourseCategoriesService : ICourseCategoryService
    {

        private readonly Alumini.Logger.ILogger _logger;
        private readonly ICourseCategoryRepository _courseCategoryRepo;

        public CourseCategoriesService(Alumini.Logger.ILogger _logger, ICourseCategoryRepository _courseCategoryRepo)
        {
            this._logger = _logger;
            this._courseCategoryRepo = _courseCategoryRepo;
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
            try
            {
                return _courseCategoryRepo.GetAllCourseCategories();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
