using AluminiService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alumini.Core;
using NLog;
using AluminiRepository.Interfaces;

namespace AluminiService
{
   public class CoursesService:ICourseService
    {

        private readonly Alumini.Logger.ILogger _logger;
        private readonly ICoursesRepository _coursesRepo;

        public CoursesService(Alumini.Logger.ILogger _logger, ICoursesRepository _coursesRepo)
        {
            this._logger = _logger;
            this._coursesRepo = _coursesRepo;
        }

        public Courses Create(Courses obj)
        {
            throw new NotImplementedException();
        }

        public Courses Get(int id)
        {
            throw new NotImplementedException();
        }

        public Courses Update(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cours> GetAllCoursesByCategoryId(int id)
        {
          try {
              return _coursesRepo.GetAllCoursesbyCategoryId(id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
