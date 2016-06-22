using Alumini.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiService.Interfaces
{

    public interface IEducationalDetailService : GenericCRUDService<EducationalDetail>
    {
        List<EducationdetailsDTO> GetEducationdetails(int id);
        EducationalDetail UpdateEducationDetails(int id, EducationalDetail UpdateEducationDetails);
    }
}
