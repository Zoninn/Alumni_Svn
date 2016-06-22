using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alumini.Core;

namespace AluminiService.Interfaces
{
    public interface ICourseCategoryService : GenericCRUDService<CourseCategory>
    {
        IEnumerable<CourseCategory> GetAllCourseCategories();
    }
}
