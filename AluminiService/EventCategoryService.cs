using AluminiService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alumini.Core;
using Alumini.Logger;
using AluminiRepository;

namespace AluminiService
{
    public class EventCategoryService : IEventCategoryService
    {
        private readonly ILogger _logger;
        private readonly IEventCategoryRepository _IEventCategoryRepo;
        public EventCategoryService(ILogger _logger, IEventCategoryRepository _EventCategoryRepo)
        {
            this._logger = _logger;
            this._IEventCategoryRepo = _EventCategoryRepo;
        }
        public EventCategory Create(EventCategory obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public EventCategory Get(int id)
        {
            throw new NotImplementedException();
        }

        public EventCategory Update(int id)
        {
            throw new NotImplementedException();
        }

        public List<EventCategory> GetCategorys()
        {
            return _IEventCategoryRepo.GetCategorys();
        }

    }
}
