using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alumini.Core;

namespace AluminiRepository.Interfaces
{
    public interface IEventsRepository : GenericCRUDRepository<Def_Events>
    {
        List<Events> GetEVents();
        Counts EventGoingList(int Eventid,int Userid);
        Event_AttendingStatus EventGoingInsert(int Userid, Event_AttendingStatus obj);
        List<Events> GetGoingUsers(int Eventid);
        IEnumerable<Events> GetEventsforHome();
        Def_Events EVentsUpdate(Def_Events UpdateEvent);
        List<Event_AttendingStatusUsers> GetUserforAdmin(string Status);
         Executive_board RegisterExe(Executive_board ExeBoard);
         List<Executive_board> ExecutiveBoardList();
         Executive_board GetExecutiveBoardDetails(int id);
         Executive_board DeleteExecutiveBoardDetails(int id);
         Banner_Imags BannerImages(Banner_Imags Banners);
         List<Banner_Imags> HomeBannerImages();
         Banner_Imags EditBannerImages(int id);
         Banner_Imags UpdateBannerImages(Banner_Imags Banners);
         Banner_Imags DeletBannerImages(int id);
    }
}
