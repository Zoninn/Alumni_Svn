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
    public class NewsRoomService : INewsRoomService
    {
        private readonly ILogger _logger;
        private readonly INewsRoomRepository _NewsRoomRepo;
        public NewsRoomService(ILogger _logger, INewsRoomRepository _NewsRoomRepository)
        {
            this._logger = _logger;
            this._NewsRoomRepo = _NewsRoomRepository;
        }
        public Alumini.Core.db_NewsRooms Create(Alumini.Core.db_NewsRooms obj)
        {
            return _NewsRoomRepo.Create(obj);
        }

        public Alumini.Core.db_NewsRooms Get(int id)
        {
            return _NewsRoomRepo.Get(id);
        }

        public Alumini.Core.db_NewsRooms Update(int id)
        {
            return _NewsRoomRepo.Update(id);
        }

        public bool Delete(int id)
        {
            return _NewsRoomRepo.Delete(id);
        }
        public List<NewsRooms> GetNews()
        {
            return _NewsRoomRepo.GetNews();
        }
        public List<NewsRooms> GetNewsRooms(int Status)
        {
            return _NewsRoomRepo.GetNewsRooms(Status);
        }
        public Alumini.Core.db_NewsRooms UpdateNewRooms(Alumini.Core.db_NewsRooms obj)
        {
            return _NewsRoomRepo.UpdateNewRooms(obj);
        }
    }
}
