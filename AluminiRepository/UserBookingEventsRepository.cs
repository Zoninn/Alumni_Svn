using AluminiRepository.Factories;
using AluminiRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiRepository
{
    public class UserBookingEventsRepository : IuserEventBookingsRepository
    {
        private readonly Alumini.Logger.ILogger _Logger;
        private readonly IDbConnectionFactory _dbContextFactory;

        public UserBookingEventsRepository(Alumini.Logger.ILogger _Logger, IDbConnectionFactory _DbContextFactory)
        {
            this._Logger = _Logger;
            this._dbContextFactory = _DbContextFactory;
        }



        public Alumini.Core.Events_UserBookings Create(Alumini.Core.Events_UserBookings obj)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    obj = context.Events_UserBookings.Add(obj);
                    context.SaveChanges();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }
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
