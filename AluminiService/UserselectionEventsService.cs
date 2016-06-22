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
    public class UserselectionEventsService : IUserSelectionEventsService
    {
          private readonly Alumini.Logger.ILogger _logger;
        private readonly IUserSelectionEventsRepository _UserselectionEventsRepo;

        public UserselectionEventsService(Alumini.Logger.ILogger _logger, IUserSelectionEventsRepository _userSelectionRepo)
        {
            this._logger = _logger;
            this._UserselectionEventsRepo = _userSelectionRepo;
        }

        public Alumini.Core.Event_UserSelections Create(Alumini.Core.Event_UserSelections obj)
        {
            return _UserselectionEventsRepo.Create(obj);
        }

        public Alumini.Core.Event_UserSelections Get(int id)
        {
            throw new NotImplementedException();
        }

        public Alumini.Core.Event_UserSelections Update(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Events> MyUserEvents(int Userid)
        {
            return _UserselectionEventsRepo.MyUserEvents(Userid);
        }
    }
}
