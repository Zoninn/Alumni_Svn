using Alumini.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace AluminiRepository
{
    public interface GenericCRUDRepository<T>
    {
        T Create(T obj);
        T Get(int id);
        T Update(int id);
        bool Delete(int id);
    }





    // public class TestContextFactory:IDatabaseContextFactory<TestContext>

}
