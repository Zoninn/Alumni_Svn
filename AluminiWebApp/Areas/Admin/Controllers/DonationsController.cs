using Alumini.Core;
using AluminiRepository;
using AluminiService.Interfaces;
using AluminiWebApp.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace AluminiWebApp.Areas.Admin.Controllers
{
    public class DonationsController : BaseController
    {

        public DonationsController(IUserDetailsViewService _userDetailsViewService, IUserInfoService _userInfoService, IEventService _eventServices, IEventTicketTypesServices _eventTicketTypeservices, IGenericMethodsService _genericMetodsservices, IUserPaymentService _userPaymentsService, IUser_JobPostingService _userJobPostingservice, IUserPostService _userPostService, INewsRoomService _newsRoomservice, IMemoriesServices memoriesservices, IDonationService _donationservice, IAlbumGalleryService _albumGalleryserice)
            : base(_userDetailsViewService, _userInfoService, _eventServices, _eventTicketTypeservices, _genericMetodsservices, _userPaymentsService, _userJobPostingservice, _userPostService, _newsRoomservice, memoriesservices, _donationservice, _albumGalleryserice)
        {

        }

        //
        // GET: /Admin/Donations/
        [HttpGet]
        public ActionResult CreateDonation()
        {
            DonationModel DonationCreation = new DonationModel();
            return View(DonationCreation);
        }

        [HttpPost]
        public ActionResult CreateDonation(DonationModel AdminDonationModel, HttpPostedFileBase BannerImage)
        {
            DonationModel DonationCreation = new DonationModel();
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(BannerImage.FileName);
                var path = Path.Combine(Server.MapPath("~/UserProfilePictures/" + fileName));
                BannerImage.SaveAs(path);
                var FilePath = "/UserProfilePictures/" + fileName;


                Donation_Details Donations = new Donation_Details()
                {
                    Donation_Title = AdminDonationModel.DonationTitle,
                    Donation_Banner = FilePath,
                    Donation_Description = AdminDonationModel.DonationDescription,
                    Donation_Amount = AdminDonationModel.DonationAmount,
                    Status = true,
                    CreatedOn = DateTime.Now
                };

                Donation_Details AdminDonations = Donationservice.Create(Donations);
            }

            return RedirectToAction("GetDonations");
        }

        [HttpGet]
        public ActionResult GetDonations()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetDonationDetails()
        {
            var data = Donationservice.GetDonations();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteDonations(int DonationId)
        {
            Donationservice.Delete(DonationId);
            var data = GenericMetodsservices.GetAdminDonations();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult UpdateDonation(int DonationId)
        {
            DonationModel AdminDonation = new DonationModel();
            return View(AdminDonation);
        }

        [HttpGet]
        public JsonResult GetDonationDetailsOnid(int DonationId)
        {
            var data = GenericMetodsservices.GetUserDonations(DonationId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DonationDetails(int DonationId)
        {
            List<Donations> data = Donationservice.GetDonationDetails(DonationId);
            return View(data);
        }

        [HttpPost]
        public ActionResult UpdateDonation(DonationModel AdminDonationModel, HttpPostedFileBase BannerImage)
        {
            if (AdminDonationModel.DonationID != null)
            {
                var FilePath = "";
                if (BannerImage != null)
                {
                    var fileName = Path.GetFileName(BannerImage.FileName);
                    var path = Path.Combine(Server.MapPath("~/UserProfilePictures/" + fileName));
                    BannerImage.SaveAs(path);
                    FilePath = "/UserProfilePictures/" + fileName;
                }

                Donation_Details Donations = new Donation_Details()
                {
                    Donation_ID = AdminDonationModel.DonationID,
                    Donation_Title = AdminDonationModel.DonationTitle,
                    Donation_Description = AdminDonationModel.DonationDescription,
                    Donation_Amount = AdminDonationModel.DonationAmount,
                    Donation_Banner = FilePath,
                    Status = true,
                    UpdatedOn = DateTime.Now
                };
                Donationservice.DonationsUpdate(Donations);
            }

            else
            {

            }
            TempData["SuccessMessage"] = "Donation Updated Successfully..";
            return RedirectToAction("GetDonations", "Donations", new { area = "Admin" });
        }
        
        [HttpGet]
        public ActionResult DonationReport()
        {
            List<Donor_Details> data = Donationservice.AdminDonationReport();
            return View(data);
        }
        
        [HttpGet]
        public ActionResult DonorDonations(int DonorId)
        {
                List<Donor_Details> data = Donationservice.UserDonationReport(DonorId);
                return View(data);
           
        }
    }
}