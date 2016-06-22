using Alumini.Core;
using Alumini.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace AluminiService
{

    public interface GenericCRUDService<T>
    {
        T Create(T obj);
        T Get(int id);
        T Update(int id);
        bool Delete(int id);
    }

 
}
