using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alumini.Core;
using AluminiRepository;

namespace AluminiService.Interfaces
{
    public interface INewsRoomService : GenericCRUDService<db_NewsRooms>
    {
        List<NewsRooms> GetNews();
        List<NewsRooms> GetNewsRooms(int Status);
        Alumini.Core.db_NewsRooms UpdateNewRooms(Alumini.Core.db_NewsRooms obj);
    }
}
