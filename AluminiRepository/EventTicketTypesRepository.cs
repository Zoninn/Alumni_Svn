using Alumini.Core;
using AluminiRepository.Factories;
using AluminiRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiRepository
{
    public class EventTicketTypesRepository : ITicketTypesRepository
    {
        private readonly Alumini.Logger.ILogger _Logger;
        private readonly IDbConnectionFactory _dbContextFactory;

        public EventTicketTypesRepository(Alumini.Logger.ILogger _Logger, IDbConnectionFactory _DbContextFactory)
        {
            this._Logger = _Logger;
            this._dbContextFactory = _DbContextFactory;
        }
        public Alumini.Core.Event_TicketTypes Create(Alumini.Core.Event_TicketTypes obj)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                try
                {
                    obj = context.Event_TicketTypes.Add(obj);
                    context.SaveChanges();
                    return obj;

                }

                catch (Exception ex)
                {
                    _Logger.Error(ex.Message, ex);
                    throw ex;
                }
            }
        }


        public Alumini.Core.Event_TicketTypes Get(int id)
        {
            throw new NotImplementedException();
        }

        public Event_TicketTypes UpdateEvent(Event_TicketTypes EventTypes)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                Event_TicketTypes TicketTypes = context.Event_TicketTypes.Where(x => x.TypeId == EventTypes.TypeId && x.Status == true).FirstOrDefault();
                TicketTypes.Heading = EventTypes.Heading;
                TicketTypes.Description = EventTypes.Description;
                TicketTypes.Price = EventTypes.Price;
                TicketTypes.Quantity = EventTypes.Quantity;
                TicketTypes.Description = EventTypes.Description;
                TicketTypes.UpdatedOn = DateTime.Now;
                context.SaveChanges();
                return EventTypes;

            }
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }


        public Event_TicketTypes Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
