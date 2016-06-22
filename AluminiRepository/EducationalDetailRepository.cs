using Alumini.Core;
using AluminiRepository.Factories;
using AluminiRepository.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiRepository
{
    public class EducationalDetailRepository : IEducationalDetailRepository
    {

        private readonly Alumini.Logger.ILogger _Logger;
        private readonly IDbConnectionFactory _dbContextFactory;
        private readonly IUserInfoRepository _userInfoRepository;

        public EducationalDetailRepository(Alumini.Logger.ILogger _Logger, IDbConnectionFactory _DbContextFactory)
        {
            this._Logger = _Logger;
            this._dbContextFactory = _DbContextFactory;
        }

        public EducationalDetail Create(EducationalDetail educationalDetails)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                try
                {
                    educationalDetails = context.EducationalDetails.Add(educationalDetails);
                    context.SaveChanges();
                    CallNotificationModules(this, new UserRegistrationDoneEventArgs { Email = educationalDetails.Email, MobileNumber = educationalDetails.MobileNumber });
                    return educationalDetails;
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

        public EducationalDetail Get(int id)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                try
                {
                    return context.EducationalDetails.Where(x => x.UserId == id).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    _Logger.Error(ex.Message, ex);
                    throw ex;
                }

            }
        }

        public List<EducationdetailsDTO> GetEducationdetails(int id)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                try
                {
                    List<EducationdetailsDTO> Getdata = (from a in context.CourseCategories
                                                         join b in context.Courses
                                                       on a.Id equals b.CourseCategoryId
                                                         join c in context.EducationalDetails
                                                         on b.Id equals c.CourseId
                                                         where c.UserId == id
                                                         where (c.UserId == id)
                                                         select new EducationdetailsDTO { CategoryId = a.Id, CourseId = b.Id, Batch = c.Batch }).ToList();
                    return Getdata;
                }
                catch (Exception ex)
                {
                    _Logger.Error(ex.Message, ex);
                    throw ex;
                }

            }
        }

        public EducationalDetail Update(int id)
        {
            throw new NotImplementedException();
        }

        public EducationalDetail UpdateEducationDetails(int id, EducationalDetail UpdateEducationDetails)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                try
                {
                    EducationalDetail education = context.EducationalDetails.Where(x => x.UserId == id).First();
                    education.Batch = UpdateEducationDetails.Batch;
                    education.CourseId = UpdateEducationDetails.CourseId;
                    context.SaveChanges();
                    return UpdateEducationDetails;
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
