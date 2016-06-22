using Alumini.Core;
using Alumini.Logger;
using AluminiRepository.Factories;
using AluminiRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiRepository
{
    public class SaluationRepository : ISaluationRepository
    {
        private readonly Alumini.Logger.ILogger _Logger;
        private readonly IDbConnectionFactory _dbContextFactory;

        public SaluationRepository(Alumini.Logger.ILogger _Logger, IDbConnectionFactory _DbContextFactory)
        {
            this._Logger = _Logger;
            this._dbContextFactory = _DbContextFactory;
        }

        public IEnumerable<Alumini.Core.Salutation> GetSaluations()
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                try
                {
                    return context.Salutations.ToList();
                }
                catch (Exception ex)
                {
                    _Logger.Error(ex.Message, ex);
                    throw ex;
                }

            }
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
