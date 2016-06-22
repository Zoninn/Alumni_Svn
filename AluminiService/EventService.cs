using Alumini.Core;
using Alumini.Logger;
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
    public class EventService : IEventService
    {
        private readonly ILogger _logger;
        private readonly IEventsRepository _IEventCategoryRepo;
        public EventService(ILogger _logger, IEventsRepository _EventCategoryRepo)
        {
            this._logger = _logger;
            this._IEventCategoryRepo = _EventCategoryRepo;
        }

        public Def_Events Create(Def_Events obj)
        {
            return _IEventCategoryRepo.Create(obj);
        }
        public List<Banner_Imags> HomeBannerImages()
        {
            return _IEventCategoryRepo.HomeBannerImages();
        }
        public Executive_board GetExecutiveBoardDetails(int id)
        {
            return _IEventCategoryRepo.GetExecutiveBoardDetails(id);
        }
        public Banner_Imags UpdateBannerImages(Banner_Imags Banners)
        {
            return _IEventCategoryRepo.UpdateBannerImages(Banners);
        }

        public Banner_Imags BannerImages(Banner_Imags Banners)
        {
            return _IEventCategoryRepo.BannerImages(Banners);
        }
        public Executive_board DeleteExecutiveBoardDetails(int id)
        {
            return _IEventCategoryRepo.DeleteExecutiveBoardDetails(id);
        }

        public Banner_Imags EditBannerImages(int id)
        {
            return _IEventCategoryRepo.EditBannerImages(id);
        }
        public List<Executive_board> ExecutiveBoardList()
        {
            return _IEventCategoryRepo.ExecutiveBoardList();
        }

        public Banner_Imags DeletBannerImages(int id)
        {
            return _IEventCategoryRepo.DeletBannerImages(id);
        }

        public Def_Events Get(int id)
        {
            return _IEventCategoryRepo.Get(id);
        }

        public Executive_board RegisterExe(Executive_board ExeBoard)
        {
            return _IEventCategoryRepo.RegisterExe(ExeBoard);
        }
        public Def_Events Update(int id)
        {
            throw new NotImplementedException();
        }

        public List<Event_AttendingStatusUsers> GetUserforAdmin(string Status)
        {
            return _IEventCategoryRepo.GetUserforAdmin(Status);
        }

        public bool Delete(int id)
        {
            return _IEventCategoryRepo.Delete(id);
        }
        public List<Events> GetEVents()
        {
            return _IEventCategoryRepo.GetEVents();
        }

        public Counts EventGoingList(int Eventid, int Userid)
        {
            return _IEventCategoryRepo.EventGoingList(Eventid, Userid);
        }
        public Event_AttendingStatus EventGoingInsert(int Userid, Event_AttendingStatus obj)
        {
            return _IEventCategoryRepo.EventGoingInsert(Userid, obj);
        }
        public List<Events> GetGoingUsers(int Eventid)
        {
            return _IEventCategoryRepo.GetGoingUsers(Eventid);
        }
        public IEnumerable<Events> GetEventsforHome()
        {
            return _IEventCategoryRepo.GetEventsforHome();
        }
        public Def_Events EVentsUpdate(Def_Events UpdateEvent)
        {
            return _IEventCategoryRepo.EVentsUpdate(UpdateEvent);
        }
    }
}
