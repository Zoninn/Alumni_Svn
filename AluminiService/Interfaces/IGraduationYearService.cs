using Alumini.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiService.Interfaces
{
    public interface IGraduationYearService : GenericCRUDService<GraduationYear>
    {
        IEnumerable<GraduationYear> GetGraduationYearByCourseId(int Id);
    }
}
