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
    public class ProfessionalDetailsRepository : IProfessionalDetailsRepository
    {
        private readonly Alumini.Logger.ILogger _Logger;
        private readonly IDbConnectionFactory _dbContextFactory;
        public ProfessionalDetailsRepository(Alumini.Logger.ILogger _Logger, IDbConnectionFactory _DbContextFactory)
        {
            this._Logger = _Logger;
            this._dbContextFactory = _DbContextFactory;
        }

        public Alumini.Core.ProfessionalDetail Create(Alumini.Core.ProfessionalDetail obj)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                try
                {
                    obj = context.ProfessionalDetails.Add(obj);
                    context.SaveChanges();
                    return obj;
                }
                catch (Exception ex)
                {
                    _Logger.Error(ex.Message, ex);
                    throw ex;
                }

            }
        }

        public List<View_UserDetails> GetUserDetailsonSearchbased(string Name, int id, string Designation)
        {
            List<View_UserDetails> Displaydetails = new List<View_UserDetails>();
            using (var context = _dbContextFactory.CreateConnection())
            {
                switch (id)
                {
                    case 1:
                        var Data = context.ProfessionalDetails.Where(x => x.Company == Name && x.Designation == Designation).ToList();
                        foreach (var details in Data)
                        {
                            var Fulldetails = context.View_UserDetails.Where(x => x.UserId == details.UserId && x.UserStatus == 1).FirstOrDefault();

                            int Count = context.View_UserDetails.Where(x => x.UserId == details.UserId && x.UserStatus == 1).Count();
                            if (Count != 0)
                            {
                                Displaydetails.Add(new View_UserDetails { FirstName = Fulldetails.FirstName, LastName = Fulldetails.LastName, ProfilePicture = Fulldetails.ProfilePicture, CourseName = Fulldetails.CourseName, CourseCategoryName = Fulldetails.CourseCategoryName, Company = Fulldetails.Company, Designation = Fulldetails.Designation, WorkedFrom = Fulldetails.WorkedFrom, WorkedTill = Fulldetails.WorkedTill, Batch = Fulldetails.Batch, DepartmentName = Fulldetails.DepartmentName });
                            }
                        }
                        break;
                }
                return Displaydetails;
            }
        }


        public List<View_UserDetails> GetUserDetails(string Name, int id)
        {
            List<View_UserDetails> Displaydetails = new List<View_UserDetails>();
            using (var context = _dbContextFactory.CreateConnection())
            {
                switch (id)
                {
                    case 1:
                        var Data = context.ProfessionalDetails.Where(x => x.Company == Name).ToList();
                        foreach (var details in Data)
                        {
                            var Fulldetails = context.View_UserDetails.Where(x => x.UserId == details.UserId && x.UserStatus == 1).FirstOrDefault();

                            int Count = context.View_UserDetails.Where(x => x.UserId == details.UserId && x.UserStatus == 1).Count();
                            if (Count != 0)
                            {
                                Displaydetails.Add(new View_UserDetails { FirstName = Fulldetails.FirstName, LastName = Fulldetails.LastName, ProfilePicture = Fulldetails.ProfilePicture, CourseName = Fulldetails.CourseName, CourseCategoryName = Fulldetails.CourseCategoryName, Company = Fulldetails.Company, Designation = Fulldetails.Designation, WorkedFrom = Fulldetails.WorkedFrom, WorkedTill = Fulldetails.WorkedTill, Batch = Fulldetails.Batch, DepartmentName = Fulldetails.DepartmentName });
                            }
                        }
                        break;
                    case 2:
                        var Namesearch = context.ProfessionalDetails.Where(x => x.Designation == Name).ToList();
                        foreach (var details in Namesearch)
                        {
                            var Fulldetails = context.View_UserDetails.Where(x => x.UserId == details.UserId && x.UserStatus == 1).FirstOrDefault();

                            int Count = context.View_UserDetails.Where(x => x.UserId == details.UserId && x.UserStatus == 1).Count();
                            if (Count != 0)
                            {
                                Displaydetails.Add(new View_UserDetails { FirstName = Fulldetails.FirstName, LastName = Fulldetails.LastName, ProfilePicture = Fulldetails.ProfilePicture, CourseName = Fulldetails.CourseName, CourseCategoryName = Fulldetails.CourseCategoryName, Company = Fulldetails.Company, Designation = Fulldetails.Designation, WorkedFrom = Fulldetails.WorkedFrom, WorkedTill = Fulldetails.WorkedTill, Batch = Fulldetails.Batch, DepartmentName = Fulldetails.DepartmentName });
                            }
                        }
                        break;
                    case 3:

                        return context.View_UserDetails.Where(x => x.Email == Name).ToList();
                        break;
                    case 4:
                        return context.View_UserDetails.Where(x => x.FirstName == Name).ToList();
                        break;
                        case 5:
                         var UseDataDetails = context.ProfessionalDetails.Where(x => x.Designation == Name).ToList();
                         foreach (var details in UseDataDetails)
                        {
                            var Fulldetails = context.View_UserDetails.Where(x => x.UserId == details.UserId && x.UserStatus == 1).FirstOrDefault();

                            int Count = context.View_UserDetails.Where(x => x.UserId == details.UserId && x.UserStatus == 1).Count();
                            if (Count != 0)
                            {
                                Displaydetails.Add(new View_UserDetails { FirstName = Fulldetails.FirstName, LastName = Fulldetails.LastName, ProfilePicture = Fulldetails.ProfilePicture, CourseName = Fulldetails.CourseName, CourseCategoryName = Fulldetails.CourseCategoryName, Company = Fulldetails.Company, Designation = Fulldetails.Designation, WorkedFrom = Fulldetails.WorkedFrom, WorkedTill = Fulldetails.WorkedTill, Batch = Fulldetails.Batch, DepartmentName = Fulldetails.DepartmentName });
                            }
                        }
                        break;

                }
                return Displaydetails;
            }

        }



        public Alumini.Core.ProfessionalDetail Get(int id)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                try
                {
                    context.Configuration.ProxyCreationEnabled = false;
                    int Coint = context.ProfessionalDetails.Where(x => x.UserId == id).Count();
                    if (Coint != 0)
                    {
                        return context.ProfessionalDetails.Where(x => x.UserId == id).First();
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    _Logger.Error(ex.Message, ex);
                    throw ex;
                }

            }
        }


        public List<Companys> GetAllCompanys(int id)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                List<Companys> CompanyDetails = new List<Companys>();
                try
                {
                    context.Configuration.ProxyCreationEnabled = false;

                    if (id == 1)
                    {
                        var Company = context.ProfessionalDetails.Where(x => x.Status == true).Distinct().ToList();

                        foreach (var Info in Company)
                        {

                            int Result = CompanyDetails.Where(x => x.ComapnyName == Info.Company).Count();

                            var Details = context.ProfessionalDetails.Where(x => x.Status == true && x.Company == Info.Company).ToList();
                            int UserCount = 0;
                            int UserDetailsCount = 0;
                            foreach (var Useridscheck in Details)
                            {
                                int UserChecksCount = context.View_UserDetails.Where(x => x.UserStatus == 1 && x.UserId == Useridscheck.UserId).Count();
                                if (UserChecksCount != 0)
                                {
                                    UserDetailsCount += 1;
                                }
                            }
                            if (Result == 0)
                            {
                                int Count = context.View_UserDetails.Where(x => x.UserStatus == 1 && x.UserId == Info.UserId).Count();
                                if (Count != 0)
                                {
                                    CompanyDetails.Add(new Companys { ComapnyName = Info.Company, CompnyId = Info.Id, TotalMembers = UserDetailsCount });
                                }
                            }

                        }
                    }
                    else if (id == 2)
                    {
                        var Company = context.ProfessionalDetails.Where(x => x.Status == true).ToList();

                        foreach (var Info in Company)
                        {
                            int Result = CompanyDetails.Where(x => x.ComapnyName == Info.Designation).Count();
                            var Details = context.ProfessionalDetails.Where(x => x.Status == true && x.Designation == Info.Designation).ToList();
                            int UserCount = 0;
                            int UserDetailsCount = 0;
                            foreach (var Useridscheck in Details)
                            {
                                int UserChecksCount = context.View_UserDetails.Where(x => x.UserStatus == 1 && x.UserId == Useridscheck.UserId).Count();
                                if (UserChecksCount != 0)
                                {
                                    UserDetailsCount += 1;
                                }
                            }
                            if (Result == 0)
                            {
                                int Count = context.View_UserDetails.Where(x => x.UserStatus == 1 && x.UserId == Info.UserId).Count();
                                if (Count != 0)
                                {
                                    CompanyDetails.Add(new Companys { ComapnyName = Info.Designation, CompnyId = Info.Id, TotalMembers = UserDetailsCount });
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _Logger.Error(ex.Message, ex);
                    throw ex;
                }

                return CompanyDetails;
            }
        }

        public Alumini.Core.ProfessionalDetail UpdateUserProfDetails(int id, ProfessionalDetail Prof)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                try
                {
                    int Count = context.ProfessionalDetails.Where(x => x.UserId == id).Count();
                    if (Count != 0)
                    {
                        ProfessionalDetail Profdetails = context.ProfessionalDetails.Where(x => x.UserId == id).First();
                        Profdetails.Company = Prof.Company;
                        Profdetails.Designation = Prof.Designation;
                        Profdetails.WorkedFrom = Prof.WorkedFrom;
                        Profdetails.WorkedTill = Prof.WorkedTill;
                        context.SaveChanges();
                    }
                    else
                    {
                        Prof.Status = true;
                        Prof.UserId = id;
                        context.ProfessionalDetails.Add(Prof);
                        context.SaveChanges();
                    }
                    return Prof;
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

        public ProfessionalDetail Update(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Companys> GetCompanyDetailsforAutosearch(string Auto, string Designation)
        {
            List<Companys> Details = new List<Companys>();
            using (var context = _dbContextFactory.CreateConnection())
            {
                if (Auto != "")
                {
                    Details = (from a in context.ProfessionalDetails
                               where a.Company.Contains(Auto)
                               select new Companys { ComapnyName = a.Company, CompnyId = a.Id, Designation = a.Designation }).Distinct().ToList();
                }
                else
                {
                    Details = (from a in context.ProfessionalDetails
                               where a.Designation.Contains(Designation)
                               select new Companys { CompnyId = a.Id, Designation = a.Designation }).Distinct().ToList();
                }
                return Details;
            }
        }
    }

    public class Companys
    {

        public int userid { get; set; }
        public long CompnyId { get; set; }
        public string ComapnyName { get; set; }
        public int TotalMembers { get; set; }
        public string Designation { get; set; }

        public long UserId { get; set; }
    }
}
