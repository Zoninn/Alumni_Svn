using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alumini.Core;
using AluminiRepository.Factories;

namespace AluminiRepository
{
    public class EventCategorysRepository : IEventCategoryRepository
    {
        private readonly Alumini.Logger.ILogger _Logger;
        private readonly IDbConnectionFactory _dbContextFactory;

        public EventCategorysRepository(Alumini.Logger.ILogger _Logger, IDbConnectionFactory _DbContextFactory)
        {
            this._Logger = _Logger;
            this._dbContextFactory = _DbContextFactory;
        }

        public EventCategory Create(EventCategory obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public EventCategory Get(int id)
        {
            throw new NotImplementedException();
        }

        public EventCategory Update(int id)
        {
            throw new NotImplementedException();
        }
        public List<EventCategory> GetCategorys()
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                try
                {
                    return context.EventCategorys.Where(x => x.Status == true).ToList();
                }
                catch (Exception ex)
                {
                    _Logger.Error(ex.Message, ex);
                    throw ex;
                }
            }
        }
    }
}
