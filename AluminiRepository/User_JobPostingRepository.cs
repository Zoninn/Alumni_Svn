using Alumini.Core;
using AluminiRepository.Factories;
using AluminiRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiRepository
{
    public class User_JobPostingRepository : IUser_JobPostingsREpository
    {
        private readonly Alumini.Logger.ILogger _Logger;
        private readonly IDbConnectionFactory _dbContextFactory;

        public User_JobPostingRepository(Alumini.Logger.ILogger _Logger, IDbConnectionFactory _DbContextFactory)
        {
            this._Logger = _Logger;
            this._dbContextFactory = _DbContextFactory;
        }

        public Alumini.Core.UserPosting_Jobs Create(Alumini.Core.UserPosting_Jobs obj)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    context.UserPosting_Jobs.Add(obj);
                    context.SaveChanges();
                    return obj;
                }
            }
            catch (SystemException ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public Alumini.Core.UserPosting_Jobs Get(int id)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    UserPosting_Jobs Jobs = context.UserPosting_Jobs.Where(x => x.Jobid == id && x.Status == 1).SingleOrDefault();
                    return Jobs;
                }
            }
            catch (SystemException ex)
            {
                throw ex;
            }
        }


        public IEnumerable<UserJobPostings> GetPendingApprovals(int Status)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    context.Configuration.ProxyCreationEnabled = false;
                    List<UserJobPostings> Jobs = (from a in context.UserDetails
                                                  join b in context.UserPosting_Jobs
                                                  on a.Id equals b.Userid
                                                  where b.Status == Status
                                                  select new UserJobPostings { Jobid = b.Jobid, PostedBy = a.FirstName + a.LastName, JobTitle = b.JobTitle, ComanyName = b.ComanyName, ExperienceRequired = b.ExperienceRequired, Mobile = b.Mobile, Email = b.Email, Description = b.Description }).ToList();
                    return Jobs.OrderByDescending(x => x.Jobid);
                }
            }
            catch (SystemException ex)
            {
                throw ex;
            }
        }

        public Alumini.Core.UserPosting_Jobs Update(int id)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                UserPosting_Jobs Jobs = context.UserPosting_Jobs.Where(x => x.Jobid == id).FirstOrDefault();
                Jobs.Status = 1;
                Jobs.UpdatedOn = DateTime.Now;
                context.SaveChanges();
                return Jobs;
            }
        }


        public Alumini.Core.UserPosting_Jobs UpdateJobs(int Jobid, int Status)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                UserPosting_Jobs Jobs = context.UserPosting_Jobs.Where(x => x.Jobid == Jobid).FirstOrDefault();
                Jobs.Status = Status;
                Jobs.UpdatedOn = DateTime.Now;
                context.SaveChanges();
                return Jobs;
            }
        }

        public Alumini.Core.UserPosting_Jobs UpdateMyPostedJobs(UserPosting_Jobs obj)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                UserPosting_Jobs PostedJobs = context.UserPosting_Jobs.Where(x => x.Jobid == obj.Jobid && x.Status == 1).FirstOrDefault();
                PostedJobs.JobTitle = obj.JobTitle;
                PostedJobs.ComanyName = obj.ComanyName;
                PostedJobs.ComanyUrl = obj.ComanyUrl;
                PostedJobs.ExperienceRequired = obj.ExperienceRequired;
                PostedJobs.Mobile = obj.Mobile;
                PostedJobs.Email = obj.Email;
                PostedJobs.Location = obj.Location;
                PostedJobs.Skills = obj.Skills;
                PostedJobs.Salary = obj.Salary;
                PostedJobs.FunctionalId = obj.FunctionalId;
                PostedJobs.Role = obj.Role;
                PostedJobs.Qualification = obj.Qualification;
                PostedJobs.Description = obj.Description;
                PostedJobs.UpdatedOn = DateTime.Now;
                context.SaveChanges();
                return PostedJobs;
            }
        }




        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<UserJobPostings> GetJobs()
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    context.Configuration.ProxyCreationEnabled = false;
                    List<UserJobPostings> Jobs = (from a in context.UserDetails
                                                  join b in context.UserPosting_Jobs
                                                  on a.Id equals b.Userid
                                                  where b.Status == 1
                                                  select new UserJobPostings { Skills = b.Skills, Userid = a.Id, Jobid = b.Jobid, PostedBy = a.FirstName + a.LastName, JobTitle = b.JobTitle, ComanyName = b.ComanyName, ExperienceRequired = b.ExperienceRequired, Mobile = b.Mobile, Email = b.Email, Description = b.Description }).OrderByDescending(x => x.Jobid).ToList();
                    return Jobs.OrderByDescending(x => x.Jobid);
                }
            }
            catch (SystemException ex)
            {
                throw ex;
            }
        }


        public IEnumerable<UserJobPostings> GetJobsonserach(string JobTitle, string Compnay, string Skill)
        {
            try
            {
                List<UserJobPostings> Jobs = new List<UserJobPostings>();
                using (var context = _dbContextFactory.CreateConnection())
                {
                    context.Configuration.ProxyCreationEnabled = false;


                    if (JobTitle != "" && Compnay != "")
                    {
                        Jobs = (from a in context.UserDetails
                                join b in context.UserPosting_Jobs
                                  on a.Id equals b.Userid
                                where (b.Status == 1 && b.JobTitle == JobTitle && b.ComanyName == Compnay)
                                select new UserJobPostings { Skills = b.Skills, Userid = a.Id, Jobid = b.Jobid, PostedBy = a.FirstName + a.LastName, JobTitle = b.JobTitle, ComanyName = b.ComanyName, ExperienceRequired = b.ExperienceRequired, Mobile = b.Mobile, Email = b.Email, Description = b.Description }).OrderByDescending(x => x.Jobid).ToList();
                    }
                    else if (JobTitle != "" && Skill != "")
                    {
                        Jobs = (from a in context.UserDetails
                                join b in context.UserPosting_Jobs
                                  on a.Id equals b.Userid
                                where (b.Status == 1 && b.JobTitle == JobTitle && b.Skills == Skill)
                                select new UserJobPostings { Skills = b.Skills, Userid = a.Id, Jobid = b.Jobid, PostedBy = a.FirstName + a.LastName, JobTitle = b.JobTitle, ComanyName = b.ComanyName, ExperienceRequired = b.ExperienceRequired, Mobile = b.Mobile, Email = b.Email, Description = b.Description }).OrderByDescending(x => x.Jobid).ToList();
                    }
                    else if (JobTitle != "")
                    {
                        Jobs = (from a in context.UserDetails
                                join b in context.UserPosting_Jobs
                                  on a.Id equals b.Userid
                                where (b.Status == 1 && b.JobTitle == JobTitle)
                                select new UserJobPostings { Skills = b.Skills, Userid = a.Id, Jobid = b.Jobid, PostedBy = a.FirstName + a.LastName, JobTitle = b.JobTitle, ComanyName = b.ComanyName, ExperienceRequired = b.ExperienceRequired, Mobile = b.Mobile, Email = b.Email, Description = b.Description }).OrderByDescending(x => x.Jobid).ToList();
                    }

                    else if (Skill != "")
                    {
                        Jobs = (from a in context.UserDetails
                                join b in context.UserPosting_Jobs
                                  on a.Id equals b.Userid
                                where (b.Status == 1 && b.Skills == Skill)
                                select new UserJobPostings { Skills = b.Skills, Userid = a.Id, Jobid = b.Jobid, PostedBy = a.FirstName + a.LastName, JobTitle = b.JobTitle, ComanyName = b.ComanyName, ExperienceRequired = b.ExperienceRequired, Mobile = b.Mobile, Email = b.Email, Description = b.Description }).OrderByDescending(x => x.Jobid).ToList();
                    }
                    else if (Compnay != "")
                    {
                        Jobs = (from a in context.UserDetails
                                join b in context.UserPosting_Jobs
                                  on a.Id equals b.Userid
                                where (b.Status == 1 && b.ComanyName == Compnay)
                                select new UserJobPostings { Skills = b.Skills, Userid = a.Id, Jobid = b.Jobid, PostedBy = a.FirstName + a.LastName, JobTitle = b.JobTitle, ComanyName = b.ComanyName, ExperienceRequired = b.ExperienceRequired, Mobile = b.Mobile, Email = b.Email, Description = b.Description }).OrderByDescending(x => x.Jobid).ToList();
                    }

                    return Jobs.OrderByDescending(x => x.Jobid);
                }
            }
            catch (SystemException ex)
            {
                throw ex;
            }
        }


        public IEnumerable<UserJobPostings> GetUserAutocomplete(string JobTitle, int Status)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    context.Configuration.ProxyCreationEnabled = false;
                    List<UserJobPostings> Jobs = new List<UserJobPostings>();
                    if (Status == 1)
                    {
                        Jobs = (from a in context.UserPosting_Jobs

                                where (a.Status == 1 && a.JobTitle.Contains(JobTitle))
                                select new UserJobPostings { JobTitle = a.JobTitle }).Distinct().ToList();
                    }
                    else if (Status == 2)
                    {
                        Jobs = (from a in context.UserPosting_Jobs

                                where (a.Status == 1 && a.ComanyName.Contains(JobTitle))
                                select new UserJobPostings { ComanyName = a.ComanyName }).Distinct().ToList();
                    }
                    else if (Status == 3)
                    {
                        Jobs = (from a in context.UserPosting_Jobs

                                where (a.Status == 1 && a.Skills.Contains(JobTitle))
                                select new UserJobPostings { Skills = a.Skills }).Distinct().ToList();
                    }
                    return Jobs;

                }
            }
            catch (SystemException ex)
            {
                throw ex;
            }
        }

        public IEnumerable<UserJobPostings> GetUserPosetdJobs(int Userid)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    context.Configuration.ProxyCreationEnabled = false;
                    List<UserJobPostings> Jobs = (from a in context.UserDetails
                                                  join b in context.UserPosting_Jobs
                                                  on a.Id equals b.Userid
                                                  where (b.Status == 1 && b.Userid == Userid)
                                                  select new UserJobPostings { Userid = a.Id, Jobid = b.Jobid, PostedBy = a.FirstName + a.LastName, JobTitle = b.JobTitle, ComanyName = b.ComanyName, ExperienceRequired = b.ExperienceRequired, Mobile = b.Mobile, Email = b.Email, Description = b.Description }).OrderByDescending(x => x.Jobid).ToList();
                    return Jobs.OrderByDescending(x => x.Jobid);
                }
            }
            catch (SystemException ex)
            {
                throw ex;
            }
        }


        public IEnumerable<UserJobPostings> GetJobsbyId(int JobId)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    context.Configuration.ProxyCreationEnabled = false;
                    List<UserJobPostings> Jobs = (from a in context.UserDetails
                                                  join b in context.UserPosting_Jobs
                                                  on a.Id equals b.Userid
                                                  join c in context.Job_FunctionalArea
                                                  on b.FunctionalId equals c.FunctionalId
                                                  where (b.Status == 1 && b.Jobid == JobId)
                                                  select new UserJobPostings { FunctionalId = b.FunctionalId, Role = b.Role, Qualification = b.Qualification, FunactionalName = c.Name, Jobid = b.Jobid, PostedBy = a.FirstName + a.LastName, JobTitle = b.JobTitle, ComanyName = b.ComanyName, ExperienceRequired = b.ExperienceRequired, Mobile = b.Mobile, Email = b.Email, Description = b.Description, Salary = b.Salary, ComanyUrl = b.ComanyUrl, Location = b.Location, Skills = b.Skills }).ToList();
                    return Jobs;
                }
            }
            catch (SystemException ex)
            {
                throw ex;
            }
        }


        public IEnumerable<UserJobPostings> MyPostedJobs(int userid)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    context.Configuration.ProxyCreationEnabled = false;
                    List<UserJobPostings> Jobs = (from a in context.UserDetails
                                                  join b in context.UserPosting_Jobs
                                                  on a.Id equals b.Userid
                                                  join c in context.Job_FunctionalArea
                                                  on b.FunctionalId equals c.FunctionalId
                                                  where (b.Status == 1 && b.Userid == userid)
                                                  select new UserJobPostings { FunctionalId = b.FunctionalId, Role = b.Role, Qualification = b.Qualification, FunactionalName = c.Name, Jobid = b.Jobid, PostedBy = a.FirstName + a.LastName, JobTitle = b.JobTitle, ComanyName = b.ComanyName, ExperienceRequired = b.ExperienceRequired, Mobile = b.Mobile, Email = b.Email, Description = b.Description, Salary = b.Salary, ComanyUrl = b.ComanyUrl, Location = b.Location, Skills = b.Skills }).ToList();
                    return Jobs;
                }
            }
            catch (SystemException ex)
            {
                throw ex;
            }
        }


    }
    public class UserJobPostings : UserPosting_Jobs
    {

        public string PostedBy { get; set; }
        public string FunactionalName { get; set; }
    }
}
