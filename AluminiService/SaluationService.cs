using Alumini.Core;
using Alumini.Logger;
using AluminiRepository.Interfaces;
using AluminiService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiService
{
    public class SaluationService : ISaluationService
    {
        private readonly Alumini.Logger.ILogger _logger;
        private readonly ISaluationRepository _courseCategoryRepo;

        public SaluationService(Alumini.Logger.ILogger _logger, ISaluationRepository _courseCategoryRepo)
        {
            this._logger = _logger;
            this._courseCategoryRepo = _courseCategoryRepo;
        }

        public IEnumerable<Alumini.Core.Salutation> GetSaluations()
        {

            return _courseCategoryRepo.GetSaluations();
        }

        public Alumini.Core.Salutation Create(Alumini.Core.Salutation obj)
        {
            throw new NotImplementedException();
        }

        public Alumini.Core.Salutation Get(int id)
        {
            throw new NotImplementedException();
        }

        public Alumini.Core.Salutation Update(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
