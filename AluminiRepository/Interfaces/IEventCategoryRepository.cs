using Alumini.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiRepository
{
    public interface IEventCategoryRepository : GenericCRUDRepository<EventCategory>
    {
        List<EventCategory> GetCategorys();
       
    }
}
