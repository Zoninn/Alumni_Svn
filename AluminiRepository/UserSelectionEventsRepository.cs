using AluminiRepository.Factories;
using AluminiRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiRepository
{
    public class UserSelectionEventsRepository : IUserSelectionEventsRepository
    {
        private readonly Alumini.Logger.ILogger _Logger;
        private readonly IDbConnectionFactory _dbContextFactory;

        public UserSelectionEventsRepository(Alumini.Logger.ILogger _Logger, IDbConnectionFactory _DbContextFactory)
        {
            this._Logger = _Logger;
            this._dbContextFactory = _DbContextFactory;
        }


        public Alumini.Core.Event_UserSelections Create(Alumini.Core.Event_UserSelections obj)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                try
                {
                    obj = context.Event_UserSelections.Add(obj);
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
            using (var context = _dbContextFactory.CreateConnection())
            {
                List<Events> Events = new List<Events>();
                try
                {

                    Events = (from a in context.Def_Events
                              join b in context.Event_UserSelections
                               on a.EventId equals b.EventId
                              join c in context.Event_UserPayments
                              on b.EventSelectionId equals c.UserSelectionId
                              where ((b.UserId == Userid) && (a.Status == true))

                              select new Events { EventGoingDate = b.UserBookingDate, EventId = a.EventId, EventName = a.EventName, EventStartdate = a.EventStartdate, EndDate = a.EndDate, BannerImage = a.BannerImage, MobileNumber = a.MobileNumber, Email = a.Email, EventSelectionId = b.EventSelectionId }).ToList();

                    return Events.OrderByDescending(x => x.EventSelectionId);

                }
                catch (SystemException ex)
                {
                    _Logger.Error(ex.Message, ex);
                    throw ex;
                }

            }
        }
    }
}
