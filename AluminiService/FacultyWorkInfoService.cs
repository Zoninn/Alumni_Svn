using AluminiService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alumini.Core;
using NLog;
using AluminiRepository.Interfaces;

namespace AluminiService
{
    public class FacultyWorkInfoService : IFacultyWorkInfoService
    {

        private readonly Alumini.Logger.ILogger _logger;
        private readonly IFacultyWorkInfoRepository _facultyWorkInfoRepo;
        private readonly SubscriberClasses objSubscriberClasses;

        public FacultyWorkInfoService(Alumini.Logger.ILogger _logger, IFacultyWorkInfoRepository _facultyWorkInfoRepo)
        {
            this._logger = _logger;
            this._facultyWorkInfoRepo = _facultyWorkInfoRepo;
            objSubscriberClasses = new SubscriberClasses();
        }


        public FacultyWorkInfo Create(FacultyWorkInfo obj)
        {
            try
            {
                _facultyWorkInfoRepo.InsertCompletedEvent += objSubscriberClasses.SendEmail;
                _facultyWorkInfoRepo.InsertCompletedEvent += objSubscriberClasses.SendSMS;
                return _facultyWorkInfoRepo.Create(obj);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public FacultyWorkInfo Get(int id)
        {
            return _facultyWorkInfoRepo.Get(id);
        }
        public FacultyWorkInfo UpdateWorkInfo(int id, FacultyWorkInfo WorkInfo)
        {
            return _facultyWorkInfoRepo.UpdateWorkInfo(id, WorkInfo);
        }
        public FacultyWorkInfo Update(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
