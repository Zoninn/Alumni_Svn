using Alumini.Core;
using AluminiRepository.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiRepository
{
    public class GraduationYearsRepository : IGraduationYearsRepository
    {
        private readonly Alumini.Logger.ILogger _Logger;
        private readonly IDbConnectionFactory _dbContextFactory;
        public GraduationYearsRepository(Alumini.Logger.ILogger _Logger, IDbConnectionFactory _DbContextFactory)
        {
            this._Logger = _Logger;
            this._dbContextFactory = _DbContextFactory;
        }
        public GraduationYear Create(GraduationYear obj)
        {
            throw new NotImplementedException();
        }

        public GraduationYear Get(int id)
        {
            throw new NotImplementedException();
        }

        public GraduationYear Update(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<GraduationYear> GetGraduationYearByCourseId(int Id)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                context.Configuration.ProxyCreationEnabled = false;
                return context.GraduationYears.Where(x => x.CourseId == Id).ToList();

            }
        }
    }
}
