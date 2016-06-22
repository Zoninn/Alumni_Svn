using Alumini.Core;
using AluminiRepository;
using AluminiRepository.Interfaces;
using AluminiService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiService
{
    public class EventTicketTypesServices : IEventTicketTypesServices
    {
        private readonly Alumini.Logger.ILogger _logger;
        private readonly EventTicketTypesRepository _facultyWorkInfoRepo;
        private readonly SubscriberClasses objSubscriberClasses;

        public EventTicketTypesServices(Alumini.Logger.ILogger _logger, EventTicketTypesRepository _facultyWorkInfoRepo)
        {
            this._logger = _logger;
            this._facultyWorkInfoRepo = _facultyWorkInfoRepo;
            objSubscriberClasses = new SubscriberClasses();
        }

        public Alumini.Core.Event_TicketTypes Create(Alumini.Core.Event_TicketTypes obj)
        {
            return _facultyWorkInfoRepo.Create(obj);
        }

        public Alumini.Core.Event_TicketTypes Get(int id)
        {
            throw new NotImplementedException();
        }

        public Alumini.Core.Event_TicketTypes Update(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        public Alumini.Core.Event_TicketTypes UpdateEvent(Event_TicketTypes EventTypes)
        {
            return _facultyWorkInfoRepo.UpdateEvent(EventTypes);
        }
    }
}
