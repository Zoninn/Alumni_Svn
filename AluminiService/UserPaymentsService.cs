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
    public class UserPaymentsService : IUserPaymentService
    {
        private readonly Alumini.Logger.ILogger _logger;
        private readonly IUserPaymentsRepository _professionaldetailreo;

        public UserPaymentsService(Alumini.Logger.ILogger _logger, IUserPaymentsRepository _professionaldetailreo)
        {
            this._logger = _logger;
            this._professionaldetailreo = _professionaldetailreo;
        }

        public Alumini.Core.Event_UserPayments Create(Alumini.Core.Event_UserPayments obj)
        {
            return _professionaldetailreo.Create(obj);
        }

        public Alumini.Core.Event_UserPayments Get(int id)
        {
            throw new NotImplementedException();
        }

        public Alumini.Core.Event_UserPayments Update(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        public List<Events> GetUserPayments()
        {
            return _professionaldetailreo.GetUserPayments();
        }
        public List<Events> GetUserPaymentsonEventId(int EVentId)
        {
            return _professionaldetailreo.GetUserPaymentsonEventId(EVentId);
        }
    }
}
