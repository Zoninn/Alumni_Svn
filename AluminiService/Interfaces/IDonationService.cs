using Alumini.Core;
using AluminiRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiService.Interfaces
{
    public interface IDonationService : GenericCRUDService<Donation_Details>
    {
        List<Donation_Details> GetDonations();

        Donation_Details DonationsUpdate(Donation_Details UpdateDonation);

        Donor_Details DonationStatus(int DonationId, int DonorId, decimal DonationAmount, string DonationStatus, string Comments);

        List<Donor_Details> UserDonationReport(int DonorId);

        List<Donor_Details> AdminDonationReport();

        List<Donations> GetDonationDetails(int DonationId);
    }
}
