using Alumini.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AluminiRepository
{
    public interface IGraduationYearsRepository : GenericCRUDRepository<GraduationYear>
    {

        IEnumerable<GraduationYear> GetGraduationYearByCourseId(int Id);
    }
}
