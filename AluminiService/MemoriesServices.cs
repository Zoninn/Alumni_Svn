using Alumini.Core;
using AluminiRepository;
using AluminiRepository.Interfaces;
using AluminiService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiService
{
    public class MemoriesServices : IMemoriesServices
    {

        private readonly Alumini.Logger.ILogger _logger;
        private readonly IMemoriesRepository _genericMethodsRepo;
        public MemoriesServices(Alumini.Logger.ILogger _logger, IMemoriesRepository _genericMethodsRepo)
        {
            this._logger = _logger;
            this._genericMethodsRepo = _genericMethodsRepo;
        }

        public Alumini.Core.db_Memories_Info Create(Alumini.Core.db_Memories_Info obj)
        {
            return _genericMethodsRepo.Create(obj);
        }

        public Alumini.Core.db_Memories_Info Get(int id)
        {
            throw new NotImplementedException();
        }

        public Alumini.Core.db_Memories_Info Update(int id)
        {
            return _genericMethodsRepo.Update(id);
        }

        public List<Images> DeleteMeoryimage(int id, int Memoryid)
        {
            return _genericMethodsRepo.DeleteMeoryimage(id, Memoryid);
        }
        public bool Delete(int id)
        {
            return _genericMethodsRepo.Delete(id);
        }
        public Alumini.Core.db_Memories_images InsertImages(db_Memories_images Images)
        {
            return _genericMethodsRepo.InsertImages(Images);
        }
        public List<Memroeis> GetMemories(int Userid)
        {
            return _genericMethodsRepo.GetMemories(Userid);
        }
        public List<Memroeis> GetAllMemories(int userid)
        {
            return _genericMethodsRepo.GetAllMemories(userid);
        }
        public List<Memroeis> GetSinglememorys(int Memoriesid)
        {
            return _genericMethodsRepo.GetSinglememorys(Memoriesid);
        }
        public List<Memroeis> GetAllMemoriesforfaculty()
        {
            return _genericMethodsRepo.GetAllMemoriesforfaculty();
        }
        public List<Memroeis> GetDeleted()
        {
            return _genericMethodsRepo.GetDeleted();
        }
        public List<Memroeis> GetMemoriesforAdmin(int userid)
        {
            return _genericMethodsRepo.GetMemoriesforAdmin(userid);
        }
        public Alumini.Core.db_Memories_Info UpdateMemories(db_Memories_Info Memories)
        {
            return _genericMethodsRepo.UpdateMemories(Memories);
        }
        public Alumini.Core.db_Memories_images UpdateImages(db_Memories_images Images)
        {
            return _genericMethodsRepo.UpdateImages(Images);
        }

    }
}
