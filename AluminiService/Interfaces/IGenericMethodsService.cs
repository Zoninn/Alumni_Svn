using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alumini.Core;
using AluminiRepository;

namespace AluminiService.Interfaces
{
    public interface IGenericMethodsService
    {
        IEnumerable<Country> GetAllCountries();
        IEnumerable<UserPostsData> GetUserPostsonId(int UserId, string Batch, int Years, string Course, int? page, int Pagesize);
        IEnumerable<UserPostsData> GetUsersonBatches(int UserId, string Batch);
        IEnumerable<UserPostsData> GetUserdetailsonFaculty(int UserId, string Batch);
        IEnumerable<UserPostsData> UserPostedFulldetails(int PostId);
        int UserPostLikes(UserPost_Likes Obj);
        List<UserdetailsDTO> UserComments(UserPost_Comments Obj);
        int UserUnPostPostLikes(UserPost_Likes Obj);
        IEnumerable<UserPostsData> AlumniAdnFacultyData(int UserId, string Batch, int UserYears, string Course);
        List<Events> GetAdminEvents();
        List<EventTypes> EventTypes(int Id);
        List<Events> GetUserEvents(int EventId);
        Events UserBookedEvents(int UserId, int SelectionId);
        Event_TicketTypes EventUpdateTickets(int EventId, int TicketTypeId, int TicketsPurchased);
        IEnumerable<UserPostsData> UserPostedFulldetailsforAdmin();
        List<FunactionalAreasforjobs> FunactionalAreasforjobs();
        List<Donation_Details> GetAdminDonations();
        Donation_Details GetUserDonations(int DonationId);
        List<Alumni_Gallery> UserGallery(int? id);
        ActivitiesCounts GetActivities();
        Alumni_ContactUs ContactUs(Alumni_ContactUs ContactUs);
        dashboardCounts GetdashboardData();
        IEnumerable<UserPostsData> UserPostedFulldetailsforAdminDashBorad();
        IEnumerable<UserPostsData> GetUserDataserach(int UserId, string Batch, int UserYears, string Course, int? Type);
        Custome_Templates InsertTemplates(Custome_Templates Template);
        List<Custome_Templates> GetTemplates();
        Custome_Templates GetTmplatesonid(int id);
        IEnumerable<CourseCategory> GetAllCourseCategories();
        IEnumerable<EventsId> GetAllEventsonserach(string EVents);
        List<Events> GetAdminEventsforsearch(string Name);

    }
}
