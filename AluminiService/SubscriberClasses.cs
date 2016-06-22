using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using AluminiRepository;

namespace AluminiService
{
    public class SubscriberClasses
    {

        /// <summary>
        /// Sending Emails to Users
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        public void SendEmail(object sender, UserRegistrationDoneEventArgs eventArgs)
        {

            try
            {

                MailMessage mailMessage = new MailMessage()

                {

                    Subject = "Hello",

                    Body = "hii your details",

                    IsBodyHtml = false

                };

                mailMessage.To.Add(new MailAddress(eventArgs.Email));

                using (var smtp = new SmtpClient())
                {
                    smtp.Send(mailMessage);
                    // return RedirectToAction("Sent");
                }


            }

            
            catch (Exception ex)
            {

                // Response.Write(ex.Message);

            }

            //MailMessage msg = new MailMessage();
            //// Sender e-mail address.
            //msg.From = new MailAddress("rallbandi2015@gmail.com");
            //// Recipient e-mail address.
            //msg.To.Add(eventArgs.Email);
            //msg.Subject = "Login Credentials";
            ////string url = "http://localhost:30074/Login.aspx";
            ////string url = "http://myhostels.zoninnovative.com/Login.aspx";
            ////msg.Body = "Login Credentials are LoginName: " + adminRegistration.EmailId + " Password " + password + "" + " <a href='" + url + "'>CLICKME</a>";
            //msg.IsBodyHtml = true;
            //// your remote SMTP server IP.
            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.gmail.com";
            //smtp.Port = 587;
            //smtp.Credentials = new System.Net.NetworkCredential("rallbandi2015@gmail.com", "Naveen@123");
            //smtp.EnableSsl = true;
            //smtp.Send(msg);
        }
        /// <summary>
        /// Sending SMS to USERS
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        public void SendSMS(object sender, UserRegistrationDoneEventArgs eventArgs)
        {

        }
    }
}
