using Alumini.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiRepository.Interfaces
{
    public interface IMemoriesRepository : GenericCRUDRepository<db_Memories_Info>
    {
        Alumini.Core.db_Memories_images InsertImages(db_Memories_images Images);
        List<Memroeis> GetMemories(int Userid);
        List<Memroeis> GetAllMemories(int userid);
        List<Memroeis> GetSinglememorys(int Memoriesid);
        List<Memroeis> GetAllMemoriesforfaculty();
        List<Memroeis> GetDeleted();
        List<Memroeis> GetMemoriesforAdmin(int userid);
        Alumini.Core.db_Memories_Info UpdateMemories(db_Memories_Info Memories);
        Alumini.Core.db_Memories_images UpdateImages(db_Memories_images Images);
        List<Images> DeleteMeoryimage(int id, int Memoryid);
    }
}
