using Alumini.Core;
using AluminiRepository;
using AluminiRepository.Interfaces;
using AluminiService.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiService
{
    public class GenericMethodsService : IGenericMethodsService
    {
        private readonly Alumini.Logger.ILogger _logger;
        private readonly IGenericMethodsRepository _genericMethodsRepo;
        public GenericMethodsService(Alumini.Logger.ILogger _logger, IGenericMethodsRepository _genericMethodsRepo)
        {
            this._logger = _logger;
            this._genericMethodsRepo = _genericMethodsRepo;
        }

        public IEnumerable<Alumini.Core.Country> GetAllCountries()
        {
            return _genericMethodsRepo.GetAllCountries();
        }
        public IEnumerable<UserPostsData> GetUserPostsonId(int UserId, string Batch, int Years, string Course, int? page, int Pagesize)
        {
            return _genericMethodsRepo.GetUserPostsonId(UserId, Batch, Years, Course, page, Pagesize);
        }
        public IEnumerable<UserPostsData> GetUsersonBatches(int UserId, string Batch)
        {
            return _genericMethodsRepo.GetUsersonBatches(UserId, Batch);
        }

        public IEnumerable<UserPostsData> GetUserdetailsonFaculty(int UserId, string Batch)
        {
            return _genericMethodsRepo.GetUserdetailsonFaculty(UserId, Batch);
        }
        public IEnumerable<UserPostsData> UserPostedFulldetails(int PostId)
        {
            return _genericMethodsRepo.UserPostedFulldetails(PostId);
        }

        public int UserPostLikes(UserPost_Likes Obj)
        {
            return _genericMethodsRepo.UserPostLikes(Obj);
        }
        public IEnumerable<CourseCategory> GetAllCourseCategories()
        {
            return _genericMethodsRepo.GetAllCourseCategories();
        }
        public List<UserdetailsDTO> UserComments(UserPost_Comments Obj)
        {
            return _genericMethodsRepo.UserComments(Obj);
        }
        public int UserUnPostPostLikes(UserPost_Likes Obj)
        {
            return _genericMethodsRepo.UserUnPostPostLikes(Obj);
        }
        public IEnumerable<UserPostsData> AlumniAdnFacultyData(int UserId, string Batch, int UserYears, string Course)
        {
            return _genericMethodsRepo.AlumniAdnFacultyData(UserId, Batch, UserYears, Course);
        }

        public List<Events> GetAdminEvents()
        {
            return _genericMethodsRepo.GetAdminEvents();
        }
        public List<EventTypes> EventTypes(int Id)
        {
            return _genericMethodsRepo.EventTypes(Id);
        }
        public List<Events> GetUserEvents(int EventId)
        {
            return _genericMethodsRepo.GetUserEvents(EventId);
        }
        public Events UserBookedEvents(int UserId, int SelectionId)
        {
            return _genericMethodsRepo.UserBookedEvents(UserId, SelectionId);
        }
        public Event_TicketTypes EventUpdateTickets(int EventId, int TicketTypeId, int TicketsPurchased)
        {
            return _genericMethodsRepo.EventUpdateTickets(EventId, TicketTypeId, TicketsPurchased);
        }
        public IEnumerable<UserPostsData> UserPostedFulldetailsforAdmin()
        {
            return _genericMethodsRepo.UserPostedFulldetailsforAdmin();
        }
        public List<FunactionalAreasforjobs> FunactionalAreasforjobs()
        {
            return _genericMethodsRepo.FunactionalAreasforjobs();
        }

        public List<Donation_Details> GetAdminDonations()
        {
            return _genericMethodsRepo.GetAdminDonations();
        }

        public Donation_Details GetUserDonations(int DonationId)
        {
            return _genericMethodsRepo.GetUserDonations(DonationId);
        }
        public Custome_Templates InsertTemplates(Custome_Templates Template)
        {
            return _genericMethodsRepo.InsertTemplates(Template);
        }
        public List<Alumni_Gallery> UserGallery(int? id)
        {
            return _genericMethodsRepo.UserGallery(id);
        }
        public ActivitiesCounts GetActivities()
        {
            return _genericMethodsRepo.GetActivities();
        }
        public Alumni_ContactUs ContactUs(Alumni_ContactUs ContactUs)
        {
            return _genericMethodsRepo.ContactUs(ContactUs);
        }
        public dashboardCounts GetdashboardData()
        {
            return _genericMethodsRepo.GetdashboardData();
        }
        public IEnumerable<UserPostsData> UserPostedFulldetailsforAdminDashBorad()
        {
            return _genericMethodsRepo.UserPostedFulldetailsforAdminDashBorad();
        }
        public IEnumerable<UserPostsData> GetUserDataserach(int UserId, string Batch, int UserYears, string Course, int? Type)
        {
            return _genericMethodsRepo.GetUserDataserach(UserId, Batch, UserYears, Course, Type);
        }
        public List<Custome_Templates> GetTemplates()
        {
            return _genericMethodsRepo.GetTemplates();
        }
        public Custome_Templates GetTmplatesonid(int id)
        {
            return _genericMethodsRepo.GetTmplatesonid(id);
        }
        public IEnumerable<EventsId> GetAllEventsonserach(string EVents)
        {
            return _genericMethodsRepo.GetAllEventsonserach(EVents);
        }
        public List<Events> GetAdminEventsforsearch(string Name)
        {
            return _genericMethodsRepo.GetAdminEventsforsearch(Name);
        }

    }
}
