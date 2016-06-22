using Alumini.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiRepository.Interfaces
{
    public interface ITicketTypesRepository : GenericCRUDRepository<Event_TicketTypes>
    {
        Alumini.Core.Event_TicketTypes UpdateEvent(Event_TicketTypes EventTypes);
    }
}
