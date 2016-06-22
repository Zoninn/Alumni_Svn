using Alumini.Core;
using AluminiService.Interfaces;
using AluminiWebApp.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace AluminiWebApp.Areas.Alumini.Controllers
{
    public class DonationsController : BaseController
    {

        public DonationsController(IUserDetailsViewService userDetailsViewService, IUserInfoService userInfoService, IEventCategoryService eventCategoryservices, IUserPostService userpostService, IUserPostPicturesService userPostPictureservice, IUserPostVisibleService userPostVisibleServices, IGenericMethodsService genericMethods, IUserSelectionEventsService userSelectionServices, IUserselectionBookingsService userselectionBookingServices, IUserPaymentService userPaymentservice, IUser_JobPostingService _userJobPostingservice, IEventService _eventService, INewsRoomService _newsRoomService, IMemoriesServices _mermoriesServices, IDonationService _donationService, IProfessionalDetailsService _ProfessionalDetails, ISaluationService _SaluationServices, IEducationalDetailService _educationalDetailService, ICourseCategoryService _courseCategoryService, IFacultyWorkInfoService _facultyWorkInfoService)
            : base(userDetailsViewService, userInfoService, eventCategoryservices, userpostService, userPostPictureservice, userPostVisibleServices, genericMethods, userSelectionServices, userselectionBookingServices, userPaymentservice, _userJobPostingservice, _eventService, _newsRoomService, _mermoriesServices, _donationService, _ProfessionalDetails, _SaluationServices, _educationalDetailService, _courseCategoryService, _facultyWorkInfoService)
        {

        }
        //
        // GET: /Alumini/Donations/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DonationDetails(int id)
        {
            var data = GenericMethods.GetUserDonations(id);
            if (data != null)
            {
                Session["Jobid"] = data.Donation_ID;
            }

            ViewBag.DonorsCount = data.Donor_Details.Count;
            ViewBag.DonatedAmount = data.Donor_Details.Sum(x => x.Donation_Amount);
            ViewBag.DonatedPercentage =(((data.Donor_Details.Sum(x => x.Donation_Amount)) / data.Donation_Amount) * 100);
            return View(data);
        }


        public string action1 = string.Empty;
        public string hash1 = string.Empty;
        public string txnid1 = string.Empty;

        [HttpGet]
        public ActionResult PaymentGateway(int DonationId, int DonorId, string DonationStatus, string DonationAmount, string Comments)
        {
            decimal Amount = Convert.ToDecimal(DonationAmount);
            Donor_Details data1 = DonationService.DonationStatus(DonationId, DonorId, Amount, DonationStatus, Comments);
            Donation_Details Donationdata = GenericMethods.GetUserDonations(DonationId);
            View_UserDetails UserDetails = UserDetailsViewService.GetUserByUserId(DonorId);


            string hash_string = string.Empty;
            string[] hashVarsSeq;
            var Key = ConfigurationManager.AppSettings["MERCHANT_KEY"];
            Random rnd = new Random();
            string strHash = Generatehash512(rnd.ToString() + DateTime.Now);
            txnid1 = strHash.ToString().Substring(0, 20);

            hashVarsSeq = ConfigurationManager.AppSettings["hashSequence"].Split('|'); // spliting hash sequence from config
            hash_string = "";
            foreach (string hash_var in hashVarsSeq)
            {
                if (hash_var == "key")
                {
                    hash_string = hash_string + ConfigurationManager.AppSettings["MERCHANT_KEY"];
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "txnid")
                {
                    hash_string = hash_string + txnid1;
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "amount")
                {
                    hash_string = hash_string + Convert.ToDecimal(DonationAmount).ToString("g29");
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "productinfo")
                {
                    hash_string = hash_string + Donationdata.Donation_Title;
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "firstname")
                {
                    hash_string = hash_string + UserDetails.FirstName;
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "email")
                {
                    hash_string = hash_string + "chowdaryteja1223@gmail.com";
                    hash_string = hash_string + '|';
                }
                else
                {
                    hash_string = hash_string + (Request.Form[hash_var] != null ? Request.Form[hash_var] : "");// isset if else
                    hash_string = hash_string + '|';
                }
            }

            hash_string += ConfigurationManager.AppSettings["SALT"];// appending SALT
            hash1 = Generatehash512(hash_string).ToLower();         //generating hash
            action1 = ConfigurationManager.AppSettings["PAYU_BASE_URL"] + "/_payment";// setting URL

            System.Collections.Hashtable data = new System.Collections.Hashtable(); // adding values in gash table for data post
            data.Add("hash", hash1);
            data.Add("txnid", txnid1);
            data.Add("key", Key);
            string AmountForm = Convert.ToDecimal(DonationAmount).ToString("g29");// eliminating trailing zeros
            data.Add("amount", AmountForm);
            data.Add("firstname", UserDetails.FirstName);
            data.Add("email", "chowdaryteja1223@gmail.com");
            data.Add("phone", "8985143792");
            data.Add("productinfo", Donationdata.Donation_Title);
            data.Add("surl", "http://www.google.com");
            data.Add("furl", "http://www.google.com");

            string strForm = PreparePOSTForm(action1, data);
            ViewBag.Form = strForm;
            return View();

            
        }

        [HttpGet]
        public ActionResult DonationStatus(int DonationId, int DonorId, string DonationStatus, string DonationAmount, string Comments)
        {
            decimal Amount = Convert.ToDecimal(DonationAmount);
            Donor_Details data1 = DonationService.DonationStatus(DonationId, DonorId, Amount, DonationStatus, Comments);
            Donation_Details Donationdata = GenericMethods.GetUserDonations(DonationId);
            View_UserDetails UserDetails = UserDetailsViewService.GetUserByUserId(DonorId);

            ViewBag.Amount = data1.Donation_Amount;
            ViewBag.Date = data1.CreatedOn;
            ViewBag.DonationTitle = Donationdata.Donation_Title;
            ViewBag.DonorName = UserDetails.FirstName;

            return View();
        }

        public string Generatehash512(string text)
        {
            byte[] message = Encoding.UTF8.GetBytes(text);

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }

        private string PreparePOSTForm(string url, System.Collections.Hashtable data)
        {
            //Set a name for the form
            string formID = "PostForm";
            //Build the form using the specified data to be posted.
            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" +
                           formID + "\" action=\"" + url +
                           "\" method=\"POST\">");

            foreach (System.Collections.DictionaryEntry key in data)
            {

                strForm.Append("<input type=\"hidden\" name=\"" + key.Key +
                               "\" value=\"" + key.Value + "\">");
            }


            strForm.Append("</form>");
            //Build the JavaScript which will do the Posting operation.
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("var v" + formID + " = document." +
                             formID + ";");

            strScript.Append("v" + formID + ".submit();");
            strScript.Append("</script>");
            //Return the form and the script concatenated.
            //(The order is important, Form then JavaScript)
            return strForm.ToString() + strScript.ToString();
        }


        [HttpGet]
        public ActionResult DonationReport()
        {
            if (Session["UserId"].ToString() != null)
            {
                int DonorId = Convert.ToInt32(Session["UserId"]);

                List<Donor_Details> data = DonationService.UserDonationReport(DonorId);
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

	}
}