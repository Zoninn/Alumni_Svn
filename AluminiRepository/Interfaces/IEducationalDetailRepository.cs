using Alumini.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiRepository.Interfaces
{
    public interface IEducationalDetailRepository : GenericCRUDRepository<EducationalDetail>
    {

        event OnUserRegistrationCompleted InsertCompletedEvent;
        List<EducationdetailsDTO> GetEducationdetails(int id);
        EducationalDetail UpdateEducationDetails(int id, EducationalDetail UpdateEducationDetails);
    }
}
