using Alumini.Core;
using Alumini.Logger;
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
    public class User_jobPostingServices : IUser_JobPostingService
    {
        private readonly ILogger _logger;
        private readonly IUser_JobPostingsREpository _IEventCategoryRepo;
        public User_jobPostingServices(ILogger _logger, IUser_JobPostingsREpository _EventCategoryRepo)
        {
            this._logger = _logger;
            this._IEventCategoryRepo = _EventCategoryRepo;
        }


        public IEnumerable<UserJobPostings> GetJobsonserach(string JobTitle, string Compnay, string Skill)
        {
            return _IEventCategoryRepo.GetJobsonserach(JobTitle, Compnay, Skill);
        }

        public Alumini.Core.UserPosting_Jobs Create(Alumini.Core.UserPosting_Jobs obj)
        {
            return _IEventCategoryRepo.Create(obj);
        }

        public Alumini.Core.UserPosting_Jobs Get(int id)
        {
            return _IEventCategoryRepo.Get(id);
        }

        public Alumini.Core.UserPosting_Jobs Update(int id)
        {
            return _IEventCategoryRepo.Update(id);
        }
        public Alumini.Core.UserPosting_Jobs UpdateJobs(int Jobid, int Status)
        {
            return _IEventCategoryRepo.UpdateJobs(Jobid, Status);
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<UserJobPostings> GetJobs()
        {
            return _IEventCategoryRepo.GetJobs();
        }
        public IEnumerable<UserJobPostings> GetPendingApprovals(int Status)
        {
            return _IEventCategoryRepo.GetPendingApprovals(Status);
        }
        public IEnumerable<UserJobPostings> GetJobsbyId(int JobId)
        {
            return _IEventCategoryRepo.GetJobsbyId(JobId);
        }
        public IEnumerable<UserJobPostings> MyPostedJobs(int userid)
        {
            return _IEventCategoryRepo.MyPostedJobs(userid);
        }
        public UserPosting_Jobs UpdateMyPostedJobs(UserPosting_Jobs obj)
        {
            return _IEventCategoryRepo.UpdateMyPostedJobs(obj);
        }
        public IEnumerable<UserJobPostings> GetUserPosetdJobs(int Userid)
        {
            return _IEventCategoryRepo.GetUserPosetdJobs(Userid);
        }
        public IEnumerable<UserJobPostings> GetUserAutocomplete(string JobTitle, int Status)
        {
            return _IEventCategoryRepo.GetUserAutocomplete(JobTitle, Status);
        }
    }
}
