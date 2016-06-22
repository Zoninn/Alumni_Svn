using Alumini.Logger;
using AluminiRepository.Factories;
using AluminiRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiRepository
{
    public class UserPaymentRepository : IUserPaymentsRepository
    {
        private readonly ILogger _Logger;
        private readonly IDbConnectionFactory _dbContextFactory;

        public UserPaymentRepository(ILogger _Logger, IDbConnectionFactory _DbContextFactory)
        {
            this._Logger = _Logger;
            this._dbContextFactory = _DbContextFactory;
        }
        public Alumini.Core.Event_UserPayments Create(Alumini.Core.Event_UserPayments obj)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    obj = context.Event_UserPayments.Add(obj);
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
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {

                    List<Events> Result = (from a in context.Event_UserPayments
                                           join b in context.Event_UserSelections
                                               on a.UserSelectionId equals b.EventSelectionId
                                           join c in context.Events_UserBookings
                                             on b.EventSelectionId equals c.EventSelectionId
                                           join d in context.Event_TicketTypes
                                               on c.TicketTypeId equals d.TypeId
                                           join e in context.Def_Events
                                               on d.EventId equals e.EventId
                                           join f in context.UserDetails
                                           on a.UserId equals f.Id
                                           where (a.PaymentStatus == "Paid" && a.Status == true)
                                           select new Events { paymentId = a.PaymentId, UserName = f.FirstName + " " + f.LastName, EventId = e.EventId, EventName = e.EventName, TicketTypeHeading = d.Heading, TotalNoOfTickets = c.TotalTickets, TotalAmount = c.TotalAmount.Value, ActualQuantity = d.Quantity.Value, ActualTicketAmount = d.Price, MobileNumber = f.PhoneNumber }).ToList();
                    return Result;
                }
            }
            catch (SystemException ex)
            {
                throw ex;
            }
        }


        public List<Events> GetUserPaymentsonEventId(int EVentId)
        {
            List<Events> EventsRes = new List<Events>();
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {

                    List<Events> Result = (from a in context.Event_UserPayments
                                           join b in context.Event_UserSelections
                                               on a.UserSelectionId equals b.EventSelectionId
                                           join c in context.Events_UserBookings
                                             on b.EventSelectionId equals c.EventSelectionId
                                           join d in context.Event_TicketTypes
                                               on c.TicketTypeId equals d.TypeId
                                           join e in context.Def_Events
                                               on d.EventId equals e.EventId
                                           join f in context.UserDetails
                                           on a.UserId equals f.Id
                                           where (a.PaymentStatus == "Paid" && a.Status == true && b.EventId == EVentId)
                                           select new Events { paymentId = a.PaymentId, UserName = f.FirstName + " " + f.LastName, EventId = e.EventId, EventName = e.EventName, TicketTypeHeading = d.Heading, TotalNoOfTickets = c.TotalTickets, TotalAmount = c.TotalAmount.Value, ActualQuantity = d.Quantity.Value, ActualTicketAmount = d.Price, MobileNumber = f.PhoneNumber }).ToList();
                    return Result;
                  
                }
            }
            catch (SystemException ex)
            {
                throw ex;
            }
        }

    }
}
