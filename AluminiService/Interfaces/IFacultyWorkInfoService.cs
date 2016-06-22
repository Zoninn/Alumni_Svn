using Alumini.Core;
using AluminiRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiService.Interfaces
{
    public interface IFacultyWorkInfoService : GenericCRUDService<FacultyWorkInfo>
    {
        FacultyWorkInfo UpdateWorkInfo(int id, FacultyWorkInfo WorkInfo);
    }
}
