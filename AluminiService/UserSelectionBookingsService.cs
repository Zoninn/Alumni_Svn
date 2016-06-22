using Alumini.Logger;
using AluminiRepository.Interfaces;
using AluminiService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiService
{
    public class UserSelectionBookingsService : IUserselectionBookingsService
    {

          private readonly ILogger _logger;
        private readonly IuserEventBookingsRepository _IEventCategoryRepo;
        public UserSelectionBookingsService(ILogger _logger, IuserEventBookingsRepository _EventCategoryRepo)
        {
            this._logger = _logger;
            this._IEventCategoryRepo = _EventCategoryRepo;
        }

        public Alumini.Core.Events_UserBookings Create(Alumini.Core.Events_UserBookings obj)
        {
            return _IEventCategoryRepo.Create(obj);
        }

        public Alumini.Core.Events_UserBookings Get(int id)
        {
            throw new NotImplementedException();
        }

        public Alumini.Core.Events_UserBookings Update(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
