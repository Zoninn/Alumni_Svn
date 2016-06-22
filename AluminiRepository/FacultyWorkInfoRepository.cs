using AluminiRepository.Factories;
using AluminiRepository.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alumini.Core;

namespace AluminiRepository
{
    public class FacultyWorkInfoRepository : IFacultyWorkInfoRepository
    {

        private readonly Alumini.Logger.ILogger _Logger;
        private readonly IDbConnectionFactory _dbContextFactory;

        public FacultyWorkInfoRepository(Alumini.Logger.ILogger _Logger, IDbConnectionFactory _DbContextFactory)
        {
            this._Logger = _Logger;
            this._dbContextFactory = _DbContextFactory;
        }

        public FacultyWorkInfo Create(FacultyWorkInfo FacultyInfoRepo)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                try
                {
                    FacultyInfoRepo = context.FacultyWorkInfoes.Add(FacultyInfoRepo);
                    context.SaveChanges();
                    CallNotificationModules(this, new UserRegistrationDoneEventArgs { Email = FacultyInfoRepo.Email, MobileNumber = FacultyInfoRepo.MobileNumber });
                    return FacultyInfoRepo;

                }
                catch (Exception ex)
                {
                    _Logger.Error(ex.Message, ex);
                    throw ex;
                }

            }
        }
        private void CallNotificationModules(object sender, UserRegistrationDoneEventArgs eventArgs)
        {
            if (InsertCompletedEvent != null)
                InsertCompletedEvent.Invoke(sender, eventArgs);
        }


        public FacultyWorkInfo Get(int id)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                try
                {
                    return context.FacultyWorkInfoes.Where(x => x.FacultyUserId == id).FirstOrDefault();

                }
                catch (Exception ex)
                {
                    _Logger.Error(ex.Message, ex);
                    throw ex;
                }

            }
        }

        public FacultyWorkInfo Update(int id)
        {
            throw new NotImplementedException();
        }

        public FacultyWorkInfo UpdateWorkInfo(int id, FacultyWorkInfo WorkInfo)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                try
                {
                    FacultyWorkInfo WorkingInfo = context.FacultyWorkInfoes.Where(x => x.FacultyUserId == id).FirstOrDefault();
                    WorkingInfo.DepartmentName = WorkInfo.DepartmentName;
                    WorkingInfo.DesignationName = WorkInfo.DesignationName;
                    WorkingInfo.WorkingFrom = WorkInfo.WorkingFrom;
                    WorkingInfo.WorkingTo = WorkingInfo.WorkingTo;
                    context.SaveChanges();
                    return WorkInfo;

                }
                catch (Exception ex)
                {
                    _Logger.Error(ex.Message, ex);
                    throw ex;
                }

            }
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        public event OnUserRegistrationCompleted InsertCompletedEvent;
    }
}
