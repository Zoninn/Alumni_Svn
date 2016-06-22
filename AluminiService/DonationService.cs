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
    public class DonationService : IDonationService
    {
        private readonly ILogger _logger;
        private readonly IDonationRepository _IDonationRepo;
        public DonationService(ILogger _logger, IDonationRepository _DonationRepo)
        {
            this._logger = _logger;
            this._IDonationRepo = _DonationRepo;
        }

        public Donation_Details Create(Donation_Details obj)
        {
            return _IDonationRepo.Create(obj);
        }

        public Donation_Details Get(int id)
        {
            return _IDonationRepo.Get(id);
        }

        public Donation_Details Update(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            return _IDonationRepo.Delete(id);
        }
        public List<Donation_Details> GetDonations()
        {
            return _IDonationRepo.GetDonations();
        }

        public Donation_Details DonationsUpdate(Donation_Details UpdateDonation)
        {
            return _IDonationRepo.DonationsUpdate(UpdateDonation);
        }

        public Donor_Details DonationStatus(int DonationId, int DonorId, decimal DonationAmount, string DonationStatus, string Comments)
        {
            return _IDonationRepo.DonationStatus(DonationId, DonorId, DonationAmount, DonationStatus, Comments);
        }
        public List<Donor_Details> UserDonationReport(int DonorId)
        {
            return _IDonationRepo.UserDonationReport(DonorId);
        }

        public List<Donor_Details> AdminDonationReport()
        {
            return _IDonationRepo.AdminDonationReport();
        }

       public List<Donations> GetDonationDetails(int DonationId)
        {
            return _IDonationRepo.AdminDonationDetails(DonationId);
        }
    }
}
