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
    public class DonationRepository : IDonationRepository
    {
        private readonly Alumini.Logger.ILogger _Logger;
        private readonly IDbConnectionFactory _dbContextFactory;

        public DonationRepository(Alumini.Logger.ILogger _Logger, IDbConnectionFactory _DbContextFactory)
        {
            this._Logger = _Logger;
            this._dbContextFactory = _DbContextFactory;
        }


        public Alumini.Core.Donation_Details Create(Alumini.Core.Donation_Details obj)
        {

            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    obj = context.Donation_Details.Add(obj);
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

        public Alumini.Core.Donation_Details Get(int id)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                Donation_Details Donations = context.Donation_Details.Where(x => x.Donation_ID == id).FirstOrDefault();
                return Donations;
            }
        }

        public Alumini.Core.Donation_Details Update(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                Donation_Details Donations = context.Donation_Details.Where(x => x.Donation_ID == id).FirstOrDefault();
                Donations.Status = false;
                Donations.UpdatedOn = DateTime.Now;
                context.SaveChanges();
                return true;
            }

        }
        public List<Donation_Details> GetDonations()
        {
            List<Donation_Details> DonationsList = new List<Donation_Details>();
            using (var context = _dbContextFactory.CreateConnection())
            {
                var QueryData = (from a in context.Donation_Details
                                 where a.Status == true
                                 select a).ToList();

                foreach (var item in QueryData) //retrieve each item and assign to model
                {
                    DonationsList.Add(new Donation_Details()
                    {
                        Donation_ID = item.Donation_ID,
                        Donation_Title = item.Donation_Title,
                        Donation_Description = item.Donation_Description,
                        Donation_Amount = item.Donation_Amount,
                        Donation_Banner = item.Donation_Banner
                    });
                }
                return DonationsList;
            }
        }

        public Donation_Details DonationsUpdate(Donation_Details UpdateDonation)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                Donation_Details Donations = context.Donation_Details.Where(x => x.Donation_ID == UpdateDonation.Donation_ID && x.Status == true).FirstOrDefault();
                Donations.Donation_Title = UpdateDonation.Donation_Title;
                if (UpdateDonation.Donation_Banner != "")
                {
                    Donations.Donation_Banner = UpdateDonation.Donation_Banner;
                }
                Donations.Donation_Description = UpdateDonation.Donation_Description;
                Donations.Donation_Amount = UpdateDonation.Donation_Amount;
                Donations.Status = true;
                Donations.UpdatedOn = DateTime.Now;
                context.SaveChanges();
                return UpdateDonation;

            }
        }

        public Donor_Details DonationStatus(int DonationId, int DonorId, decimal DonationAmount, string DonationStatus, string Comments)
        {
            Donor_Details DonorDetails = new Donor_Details();
            using (var context = _dbContextFactory.CreateConnection())
            {
                Transaction_Details Transaction = new Transaction_Details()
                {
                    Txn_Status = DonationStatus,
                    Currency_Code = "INR",
                    Comments = Comments,
                    Txn_Message = "Transaction Successfully Created.",
                    Txn_Amount = DonationAmount,
                    Service_Tax = 0,
                    CreatedOn = DateTime.Now,
                    CreatedBy = DonorId,
                    Status = true
                };
                context.Transaction_Details.Add(Transaction);
                context.SaveChanges();
                long TransactionID = Transaction.Transaction_ID;

                DonorDetails.Donation_ID = DonationId;
                DonorDetails.Donor_ID = DonorId;
                DonorDetails.Donation_Amount = DonationAmount;
                DonorDetails.Donation_Status = DonationStatus;
                DonorDetails.Comments = Comments;
                DonorDetails.Transaction_ID = TransactionID;
                DonorDetails.CreatedOn = DateTime.Now;
                DonorDetails.Status = true;
                context.Donor_Details.Add(DonorDetails);
                context.SaveChanges();

                return DonorDetails;
            }
        }

        public List<Donor_Details> UserDonationReport(int DonorId)
        {
            List<Donor_Details> DonationReportbyUserID = new List<Donor_Details>();
            using (var context = _dbContextFactory.CreateConnection())
            {
                var QueryData = (from Donor in context.Donor_Details
                                 join Donation in context.Donation_Details on Donor.Donation_ID equals Donation.Donation_ID
                                 join Transaction in context.Transaction_Details on Donor.Transaction_ID equals Transaction.Transaction_ID
                                 where Donor.Donor_ID == DonorId
                                 select new
                                 {
                                     Amount = Donor.Transaction_Details.Txn_Amount,
                                     Date = Donor.Transaction_Details.CreatedOn,
                                     Status = Donor.Transaction_Details.Txn_Status,
                                     Title = Donor.Donation_Details.Donation_Title,
                                     ID = Donor.ID
                                 }).ToList();


                foreach (var item in QueryData) //retrieve each item and assign to model
                {
                    DonationReportbyUserID.Add(new Donor_Details()
                    {
                        Donation_Amount = item.Amount,
                        CreatedOn = item.Date,
                        Donation_Status = item.Status,
                        Comments = item.Title,
                        ID = item.ID
                    });
                }
            }
            return DonationReportbyUserID;
        }

        public List<Donor_Details> AdminDonationReport()
        {
            List<Donor_Details> DonationReporttoAdmin = new List<Donor_Details>();
            using (var context = _dbContextFactory.CreateConnection())
            {
                var QueryData = (from Donor in context.Donor_Details
                                 join Donation in context.Donation_Details on Donor.Donation_ID equals Donation.Donation_ID
                                 join Transaction in context.Transaction_Details on Donor.Transaction_ID equals Transaction.Transaction_ID
                                 select new
                                 {
                                     Amount = Donor.Transaction_Details.Txn_Amount,
                                     Date = Donor.Transaction_Details.CreatedOn,
                                     Status = Donor.Transaction_Details.Txn_Status,
                                     Title = Donor.Donation_Details.Donation_Title,
                                     ID = Donor.ID
                                 }).ToList();


                foreach (var item in QueryData) //retrieve each item and assign to model
                {
                    DonationReporttoAdmin.Add(new Donor_Details()
                    {
                        Donation_Amount = item.Amount,
                        CreatedOn = item.Date,
                        Donation_Status = item.Status,
                        Comments = item.Title,
                        ID = item.ID
                    });
                }
            }
            return DonationReporttoAdmin;
        }

        public List<Donations> AdminDonationDetails(int DonationId)
        {
            List<Donations> DonationReporttoAdmin = new List<Donations>();
            using (var context = _dbContextFactory.CreateConnection())
            {
                DonationReporttoAdmin = (from Donor in context.Donor_Details
                                         join Donation in context.Donation_Details on Donor.Donation_ID equals Donation.Donation_ID
                                         join Transaction in context.Transaction_Details on Donor.Transaction_ID equals Transaction.Transaction_ID
                                         join User in context.UserDetails on Donor.Donor_ID equals User.Id
                                         where Donation.Donation_ID == DonationId
                                         select new Donations
                                         {
                                             DonationAmount = Donor.Transaction_Details.Txn_Amount.Value,
                                             DonationDate = Donor.Transaction_Details.CreatedOn.Value,
                                             DonationStatus = Donor.Transaction_Details.Txn_Status,
                                             DonationTitle = Donor.Donation_Details.Donation_Title,
                                             DonorName = User.FirstName,
                                             DonorID = Donor.Donor_ID.Value
                                         }).ToList();
            }
            return DonationReporttoAdmin;
        }
    }
}
