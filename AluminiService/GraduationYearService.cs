using Alumini.Core;
using Alumini.Logger;
using AluminiRepository;
using AluminiService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiService
{
    public class GraduationYearService : IGraduationYearService
    {
        private readonly ILogger _logger;
        private readonly IGraduationYearsRepository _graduationYearsRepo;
        public GraduationYearService(ILogger _logger, IGraduationYearsRepository _graduationYearsRepo)
        {
            this._logger = _logger;
            this._graduationYearsRepo = _graduationYearsRepo;
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
            try
            {
                return _graduationYearsRepo.GetGraduationYearByCourseId(Id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
