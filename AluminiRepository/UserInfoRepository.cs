using Alumini.Core;
using Alumini.Logger;
using AluminiRepository.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiRepository.Interfaces
{
    public class UserInfoRepository : IUserInfoRepository
    {

        private readonly ILogger _Logger;
        private readonly IDbConnectionFactory _dbContextFactory;

        public UserInfoRepository(ILogger _Logger, IDbConnectionFactory _DbContextFactory)
        {
            this._Logger = _Logger;
            this._dbContextFactory = _DbContextFactory;
        }

        public UserDetail Create(UserDetail obj)
        {
            //  create context object here and perform insert operation
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    obj = context.UserDetails.Add(obj);
                    context.SaveChanges();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public UserDetail Get(int id)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    UserDetail Userdata = context.UserDetails.Where(x => x.Id == id).FirstOrDefault();
                    return Userdata;
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public UserDetailsDTO GetUserContactInformation(int id)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    UserDetailsDTO data = (from a in context.UserDetails
                                           join b in context.Cities
                                           on a.HomeCityId equals b.Id
                                           join c in context.States
                                          on b.Stateid equals c.Id
                                           join d in context.Countries
                                           on c.CountryId equals d.Id
                                           join e in context.Cities
                                           on a.LivesInCityId equals e.Id
                                           join f in context.States
                                           on e.Stateid equals f.Id
                                           join g in context.Countries
                                           on f.CountryId equals g.Id
                                           join h in context.Districts
                                           on c.Id equals h.StateId
                                           where a.Id == id
                                           select new UserDetailsDTO { HomePhoneNumber = a.HomePhoneNumber, PresentCity = a.PresentCity, PermanentCity = a.PermanentCity, Permanentdistid = d.Id, PresentDistid = h.Id, PermanentCountryId = g.Id, PresentStateid = c.Id, PermanentStateid = f.Id, PermenantCityId = a.LivesInCityId, PresentCityid = a.HomeCityId, CountryId = d.Id, CountryCode = a.CountryCode, AlternateEmailId = a.AlternateEmailId, PhoneNumber = a.PhoneNumber, HomeCityId = a.HomeCityId, LivesInCityId = a.LivesInCityId, Address = a.Address, PermanentAddress = a.PermanentAddress, StateId = c.Id, DistrictId = b.DisctirctId }).First();
                    return data;
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public UserDetail Update(int id)
        {
            throw new NotImplementedException();
        }

        public UserDetail UpdateUserbasicInformation(int UserId, UserDetail Userdata)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {

                    UserDetail Userdetails = context.UserDetails.Where(x => x.Id == UserId).First();
                    if (Userdata.ProfilePicture != "")
                    {
                        Userdetails.ProfilePicture = Userdata.ProfilePicture;
                    }
                    Userdetails.SalutationId = Userdata.SalutationId;
                    Userdetails.FirstName = Userdata.FirstName;
                    Userdetails.LastName = Userdata.LastName;
                    Userdetails.GenderId = Userdata.GenderId;
                    Userdetails.DOB = Userdata.DOB;
                    //Userdetails.CountryCode = Userdata.CountryCode;
                    //Userdetails.HomeCityId = Userdetails.HomeCityId;
                    //Userdetails.LivesInCityId = Userdetails.LivesInCityId;
                    context.SaveChanges();
                    return Userdetails;

                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }
        }


        public UserDetail UpdateUser(UserDetail obj)
        {
            //  create context object here and perform update operation
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    UserDetail userdata = context.UserDetails.Where(a => a.Id == obj.Id).FirstOrDefault();
                    //userdata.UserStatus = obj.UserStatus;
                    userdata.ProfileInfoPercentage = obj.ProfileInfoPercentage;
                    context.SaveChanges();
                    return userdata;
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        // Get User details by AspnetUsersId
        public UserDetail GetUser(string aspnetUserId)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    return context.UserDetails.Where(a => a.AspnetUsersId == aspnetUserId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
        public int? GetUserStatusWhenEmailIdExists(string EmailId)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    string AspnetUserId = context.AspNetUsers.Single(x => x.Email == EmailId).Id;
                    var Count = context.UserDetails.Where(x => x.AspnetUsersId == AspnetUserId && x.UserStatus == 0).Count();
                    if (Count != 0)
                    {
                        return context.UserDetails.Single(x => x.AspnetUsersId == AspnetUserId && x.UserStatus == 0).ProfileInfoPercentage;
                    }
                    return 0;
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
        // Get Users by User Satatus from Userdetails table
        public IEnumerable<UserDetail> GetAllRegistrationsByStatus(int UserStatus)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    return context.UserDetails.Where(a => a.UserStatus == UserStatus).ToList();
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
        // Update User Status by UserId
        public UserDetail UpdateUserProfileStatus(int UserId, int UserStatus)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    UserDetail userdata = context.UserDetails.Where(a => a.Id == UserId).FirstOrDefault();
                    userdata.UserStatus = UserStatus;
                    context.SaveChanges();
                    return userdata;
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }
        }



        public UserDetail UpdateContactInfo(UserDetail userDetails)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    db_Alumni_TestEntities Alumnientites = new db_Alumni_TestEntities();
                    UserDetail userdata = Alumnientites.UserDetails.Where(a => a.Id == userDetails.Id).FirstOrDefault();
                    userdata.AlternateEmailId = userDetails.AlternateEmailId;
                    userdata.PhoneNumber = userDetails.PhoneNumber;
                    userdata.PermanentAddress = userDetails.PermanentAddress;
                    userdata.HomePhoneNumber = userDetails.HomePhoneNumber;
                    userdata.PresentCity = userDetails.PresentCity;
                    userdata.PermanentCity = userDetails.PermanentCity;
                    userdata.PresentDistid = userDetails.PresentDistid;
                    userdata.PermanentDistid = userDetails.PermanentDistid;
                    userdata.Address = userDetails.Address;
                    if (userDetails.ProfileInfoPercentage != null)
                    {
                        userdata.ProfileInfoPercentage = (int)Alumini.Core.CustomStatus.ProfileInfoPercentage.ContactInformation;
                    }
                    Alumnientites.SaveChanges();
                    return userdata;
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public List<Images> getUserImages(int Userid)
        {
            List<Images> Images = new List<Images>();
            using (var context = _dbContextFactory.CreateConnection())
            {
                View_UserDetails Userdetails = context.View_UserDetails.Where(x => x.UserId == Userid).FirstOrDefault();
                var Educations = context.EducationalDetails.Where(x => x.Batch == Userdetails.Batch && x.Status == true && x.UserId != Userid).ToList();

                foreach (var data in Educations)
                {
                    UserDetail userdata = context.UserDetails.Where(x => x.Id == data.UserId).FirstOrDefault();
                    Images.Add(new Images { Userid = data.UserId, Image = userdata.ProfilePicture, UserName = userdata.FirstName + userdata.LastName });
                }


            }
            return Images;
        }
    }
}
