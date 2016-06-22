using Alumini.Core;
using AluminiRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiService.Interfaces
{
    public interface IUserSelectionEventsService : GenericCRUDService<Event_UserSelections>
    {
        IEnumerable<Events> MyUserEvents(int Userid);
    }
}
