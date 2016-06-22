using Alumini.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiService.Interfaces
{
    public interface IEventTicketTypesServices : GenericCRUDService<Event_TicketTypes>
    {
        Alumini.Core.Event_TicketTypes UpdateEvent(Event_TicketTypes EventTypes);
    }
}
