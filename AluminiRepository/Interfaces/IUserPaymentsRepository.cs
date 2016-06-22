using Alumini.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiRepository.Interfaces
{
    public interface IUserPaymentsRepository : GenericCRUDRepository<Event_UserPayments>
    {
        List<Events> GetUserPayments();
        List<Events> GetUserPaymentsonEventId(int EVentId);
    }
}
