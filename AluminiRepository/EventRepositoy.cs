using Alumini.Core;
using AluminiRepository.Factories;
using AluminiRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiRepository
{
    public class EventRepositoy : IEventsRepository
    {
        private readonly Alumini.Logger.ILogger _Logger;
        private readonly IDbConnectionFactory _dbContextFactory;

        public EventRepositoy(Alumini.Logger.ILogger _Logger, IDbConnectionFactory _DbContextFactory)
        {
            this._Logger = _Logger;
            this._dbContextFactory = _DbContextFactory;
        }


        public Alumini.Core.Def_Events Create(Alumini.Core.Def_Events obj)
        {

            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    obj = context.Def_Events.Add(obj);
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

        public Banner_Imags BannerImages(Banner_Imags Banners)
        {

            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    Banners = context.Banner_Imags.Add(Banners);
                    context.SaveChanges();
                    return Banners;
                }

            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }


        }

        public Banner_Imags UpdateBannerImages(Banner_Imags Banners)
        {

            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    Banner_Imags UpdateBanners = context.Banner_Imags.Where(x => x.Id == Banners.Id).FirstOrDefault();
                    UpdateBanners.Text = Banners.Text;
                    UpdateBanners.Tages = Banners.Tages;
                    if (Banners.Image != "")
                    {
                        UpdateBanners.Image = Banners.Image;
                    }
                    else
                    {

                    }

                    context.SaveChanges();
                    return Banners;
                }

            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }


        }

        public Banner_Imags DeletBannerImages(int id)
        {

            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    Banner_Imags Banners = context.Banner_Imags.Where(x => x.Id == id).FirstOrDefault();
                    context.Banner_Imags.Remove(Banners);
                    context.SaveChanges();
                    return Banners;
                }

            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }


        }




        public Banner_Imags EditBannerImages(int id)
        {

            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    Banner_Imags Banners = context.Banner_Imags.Where(x => x.Id == id).FirstOrDefault();
                    return Banners;
                }

            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }


        }
        public List<Banner_Imags> HomeBannerImages()
        {

            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    List<Banner_Imags> Banners = context.Banner_Imags.Where(x => x.Status == true).ToList();
                    return Banners;
                }

            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }


        }

        public List<Event_AttendingStatusUsers> GetUserforAdmin(string Status)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    List<Event_AttendingStatusUsers> EventGoing = (from a in context.Event_AttendingStatus
                                                                   join b in context.Def_Events
                                                                       on a.EventId equals b.EventId
                                                                   join c in context.View_UserDetails
                                                                       on a.UserId equals c.UserId
                                                                   where a.UserGoingStatus == Status
                                                                   select new Event_AttendingStatusUsers { UserName = c.FirstName + c.LastName, EventName = b.EventName, Mobile = c.PhoneNumber, EMail = c.Email }).ToList();
                    return EventGoing;
                }

            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public Alumini.Core.Def_Events Get(int id)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                Def_Events Events = context.Def_Events.Where(x => x.EventId == id).FirstOrDefault();
                return Events;
            }
        }

        public Executive_board GetExecutiveBoardDetails(int id)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                Executive_board BoardMembers = context.Executive_board.Where(x => x.id == id && x.Status == true).FirstOrDefault();
                return BoardMembers;
            }
        }

        public Executive_board DeleteExecutiveBoardDetails(int id)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                Executive_board BoardMembers = context.Executive_board.Where(x => x.id == id && x.Status == true).FirstOrDefault();

                BoardMembers.Status = false;
                context.SaveChanges();
                return BoardMembers;
            }
        }

        public Executive_board RegisterExe(Executive_board ExeBoard)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                if (ExeBoard.id != 0)
                {
                    Executive_board BoardMems = context.Executive_board.Where(x => x.id == ExeBoard.id && x.Status == true).FirstOrDefault();
                    BoardMems.Name = ExeBoard.Name;
                    BoardMems.Email = ExeBoard.Email;
                    BoardMems.Mobile = BoardMems.Mobile;
                    BoardMems.ROle = ExeBoard.ROle;
                    if (ExeBoard.Image != "")
                    {
                        BoardMems.Image = ExeBoard.Image;
                    }
                    else
                    {

                    }
                    context.SaveChanges();

                }
                else
                {
                    context.Executive_board.Add(ExeBoard);
                    context.SaveChanges();
                }
                return ExeBoard;
            }
        }

        public List<Executive_board> ExecutiveBoardList()
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                List<Executive_board> Data = context.Executive_board.Where(x => x.Status == true).ToList();
                return Data;
            }
        }

        public IEnumerable<Events> GetEventsforHome()
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                List<Events> EventsFor = new List<Events>();
                List<Events> Events = (from a in context.Def_Events
                                       where a.Status == true
                                       select new Events { EventDescription = a.EventDescription, EventId = a.EventId, BannerImage = a.BannerImage, EventName = a.EventName, EventStartdate = a.EventStartdate, EndDate = a.EndDate, StartTime = a.StartTime, EndTime = a.EndTime, EventVenue = a.EventVenue }).OrderByDescending(a => a.EventId).Take(4).ToList();

                foreach (var Data in Events)
                {
                    List<EventGoingCount> IwillJoin = (from a in context.Event_UserSelections
                                                       join b in context.Event_UserPayments
                                                         on a.EventSelectionId equals b.UserSelectionId
                                                       where (a.EventId == Data.EventId && b.PaymentStatus == "Paid")
                                                       select new EventGoingCount { Userid = a.UserId }).Distinct().ToList();
                    if (IwillJoin.Count != 0)
                    {

                        EventsFor.Add(new Events { EventDescription = Data.EventDescription, EventId = Data.EventId, BannerImage = Data.BannerImage, EventName = Data.EventName, EventStartdate = Data.EventStartdate, EndDate = Data.EndDate, StartTime = Data.StartTime, EndTime = Data.EndTime, EventVenue = Data.EventVenue, Count = IwillJoin.Count() });

                    }
                    else
                    {
                        EventsFor.Add(new Events { EventDescription = Data.EventDescription, EventId = Data.EventId, BannerImage = Data.BannerImage, EventName = Data.EventName, EventStartdate = Data.EventStartdate, EndDate = Data.EndDate, StartTime = Data.StartTime, EndTime = Data.EndTime, EventVenue = Data.EventVenue, Count = 0 });
                    }
                }

                return EventsFor.OrderByDescending(x => x.EventId);
            }
        }

        public Alumini.Core.Def_Events Update(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                Def_Events Events = context.Def_Events.Where(x => x.EventId == id).FirstOrDefault();
                Events.Status = false;
                Events.UpdatedOn = DateTime.Now;
                context.SaveChanges();
                return true;
            }

        }
        public List<Events> GetEVents()
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                List<Events> Events = (from a in context.Def_Events
                                       where a.Status == true
                                       select new Events { EventId = a.EventId, EventName = a.EventName }).ToList();
                return Events;
            }
        }

        public Counts EventGoingList(int Eventid, int userid)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                int MaybeCount = 0;
                int DeclineCount = 0;
                int AmGoing = 0;
                AmGoing = context.Event_AttendingStatus.Where(x => x.Status == true && x.UserGoingStatus == "AmGoing" && x.EventId == Eventid).Count();
                MaybeCount = context.Event_AttendingStatus.Where(x => x.Status == true && x.UserGoingStatus == "Maybe" && x.EventId == Eventid).Count();
                DeclineCount = context.Event_AttendingStatus.Where(x => x.Status == true && x.UserGoingStatus == "Decline" && x.EventId == Eventid).Count();
                int DeclinedUser = context.Event_AttendingStatus.Where(x => x.Status == true && x.UserGoingStatus == "Decline" && x.EventId == Eventid && x.UserId == userid).Count();
                List<Events> IwillJoin = (from a in context.Event_UserSelections
                                          join b in context.Event_UserPayments
                                            on a.EventSelectionId equals b.UserSelectionId
                                          where (a.EventId == Eventid && b.PaymentStatus == "Paid")
                                          select new Events { paymentId = b.PaymentId }).ToList();



                Counts Count = new Counts()
                {
                    DeclineCount = DeclineCount,
                    MaybeCount = MaybeCount,
                    UserDeclined = DeclinedUser,
                    IwillJoin = IwillJoin.Count(),
                    AmGoing = AmGoing
                };
                return Count;
            }
        }

        public Event_AttendingStatus EventGoingInsert(int Userid, Event_AttendingStatus obj)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                if (obj.UserGoingStatus == "Maybe")
                {
                    var Count = context.Event_AttendingStatus.Where(x => x.Status == true && x.UserGoingStatus == obj.UserGoingStatus && x.EventId == obj.EventId && x.UserId == obj.UserId).Count();
                    if (Count == 0)
                    {
                        context.Event_AttendingStatus.Add(obj);
                        context.SaveChanges();
                    }
                    var DeclineCount = context.Event_AttendingStatus.Where(x => x.Status == true && x.UserGoingStatus == "Decline" && x.EventId == obj.EventId && x.UserId == obj.UserId).Count();
                    if (DeclineCount >= 1)
                    {
                        Event_AttendingStatus atteningstaus = context.Event_AttendingStatus.Where(x => x.Status == true && x.EventId == obj.EventId && x.UserGoingStatus == "Decline" && x.UserId == obj.UserId).FirstOrDefault();
                        context.Event_AttendingStatus.Remove(atteningstaus);
                        context.SaveChanges();
                    }

                }
                else if (obj.UserGoingStatus == "Decline")
                {
                    var DeclineCount = context.Event_AttendingStatus.Where(x => x.Status == true && x.UserGoingStatus == "Maybe" && x.EventId == obj.EventId && x.UserId == obj.UserId).Count();
                    var Maybedecline = context.Event_AttendingStatus.Where(x => x.Status == true && x.UserGoingStatus == "Decline" && x.EventId == obj.EventId && x.UserId == obj.UserId).Count();

                    if (Maybedecline >= 1)
                    {

                    }
                    else
                    {

                        context.Event_AttendingStatus.Add(obj);
                        context.SaveChanges();
                    }

                    if (DeclineCount >= 1)
                    {
                        Event_AttendingStatus attendingstaus = context.Event_AttendingStatus.Where(x => x.Status == true && x.EventId == obj.EventId && x.UserGoingStatus == "Maybe" && x.UserId == obj.UserId).FirstOrDefault();
                        context.Event_AttendingStatus.Remove(attendingstaus);
                        context.SaveChanges();

                    }
                }
                else if (obj.UserGoingStatus == "AmGoing")
                {
                    var DeclineCount = context.Event_AttendingStatus.Where(x => x.Status == true && x.UserGoingStatus == "Maybe" && x.EventId == obj.EventId && x.UserId == obj.UserId).Count();
                    var AmGoing = context.Event_AttendingStatus.Where(x => x.Status == true && x.UserGoingStatus == "AmGoing" && x.EventId == obj.EventId && x.UserId == obj.UserId).Count();
                    var Maybedecline = context.Event_AttendingStatus.Where(x => x.Status == true && x.UserGoingStatus == "Decline" && x.EventId == obj.EventId && x.UserId == obj.UserId).Count();

                    if (AmGoing >= 1)
                    {

                    }
                    else
                    {

                        context.Event_AttendingStatus.Add(obj);
                        context.SaveChanges();
                    }
                }

            }
            return obj;
        }

        public List<Events> GetGoingUsers(int Eventid)
        {
            List<Events> Eventsusers = new List<Events>();
            using (var context = _dbContextFactory.CreateConnection())
            {
                List<Events> GoingUsers = (from a in context.Event_UserPayments
                                           join b in context.Event_UserSelections
                                               on a.UserSelectionId equals b.EventSelectionId
                                           where (b.EventId == Eventid && a.Status == true)
                                           select new Events { UserId = a.UserId }).Distinct().Take(10).ToList();
                Def_Events Events = context.Def_Events.Where(x => x.EventId == Eventid && x.Status == true).FirstOrDefault();
                foreach (var GoingUsres in GoingUsers)
                {
                    View_UserDetails details = context.View_UserDetails.Where(x => x.UserId == GoingUsres.UserId).FirstOrDefault();
                    if (details.Batch != null)
                    {
                        Eventsusers.Add(new Events { UserName = details.FirstName, LName = details.LastName, UserImage = details.ProfilePicture, Batch = details.Batch, Branch = details.CourseCategoryName, Course = details.CourseName });
                    }
                    else
                    {
                        Eventsusers.Add(new Events { UserName = details.FirstName, LName = details.LastName, UserImage = details.ProfilePicture, Batch = details.WorkingFrom, WorkingTO = details.WorkingTo, Course = details.DepartmentName });
                    }
                }

            }
            return Eventsusers;
        }

        public Def_Events EVentsUpdate(Def_Events UpdateEvent)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                Def_Events Events = context.Def_Events.Where(x => x.EventId == UpdateEvent.EventId && x.Status == true).FirstOrDefault();
                Events.EventName = UpdateEvent.EventName;
                if (UpdateEvent.BannerImage != "")
                {
                    Events.BannerImage = UpdateEvent.BannerImage;
                }
                Events.EventStartdate = UpdateEvent.EventStartdate;
                Events.EndDate = UpdateEvent.EndDate;
                Events.MobileNumber = UpdateEvent.MobileNumber;
                Events.Email = UpdateEvent.Email;
                Events.EventVenue = UpdateEvent.EventVenue;
                Events.EventDescription = UpdateEvent.EventDescription;
                if (UpdateEvent.StartTime != "00")
                {
                    Events.StartTime = UpdateEvent.StartTime;
                }
                if (UpdateEvent.EndTime != "00")
                {
                    Events.EndTime = UpdateEvent.EndTime;
                }
                Events.Status = true;
                Events.UpdatedOn = DateTime.Now;
                context.SaveChanges();
                return UpdateEvent;

            }
        }
    }
    public class Counts
    {
        public int MaybeCount { get; set; }
        public int DeclineCount { get; set; }
        public int UserDeclined { get; set; }
        public int IwillJoin { get; set; }
        public int AmGoing { get; set; }
    }
    public class Event_AttendingStatusUsers
    {
        public string Eventid { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string EMail { get; set; }
        public DateTime CreatedOn { get; set; }
        public string EventName { get; set; }
    }

}
