using Alumini.Core;
using AluminiRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiService.Interfaces
{
    public interface IUser_JobPostingService : GenericCRUDService<UserPosting_Jobs>
    {
        IEnumerable<UserJobPostings> GetJobs();
        IEnumerable<UserJobPostings> GetPendingApprovals(int Status);
        UserPosting_Jobs UpdateJobs(int Jobid, int Status);
        IEnumerable<UserJobPostings> GetJobsbyId(int JobId);
        IEnumerable<UserJobPostings> MyPostedJobs(int userid);
        Alumini.Core.UserPosting_Jobs UpdateMyPostedJobs(UserPosting_Jobs obj);
        IEnumerable<UserJobPostings> GetUserPosetdJobs(int Userid);
        IEnumerable<UserJobPostings> GetUserAutocomplete(string JobTitle, int Status);
        IEnumerable<UserJobPostings> GetJobsonserach(string JobTitle, string Compnay, string Skill);
      
    }
}
