using Alumini.Core;
using AluminiRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiService.Interfaces
{
    public interface IUserPaymentService : GenericCRUDService<Event_UserPayments>
    {
        List<Events> GetUserPayments();
        List<Events> GetUserPaymentsonEventId(int EVentId);
    }
}
