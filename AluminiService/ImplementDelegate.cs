using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AluminiService
{
    public class ImplementDelegate
    {
       
          
  
        /// <summary>
        /// Sending Emails to Users
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        public void SendEmails(string Email, string Password)
        {
            MailMessage msg = new MailMessage();
            // Sender e-mail address.
            msg.From = new MailAddress("rallbandi2015@gmail.com");
            // Recipient e-mail address.
            msg.To.Add(Email);
            msg.Subject = "Login Credentials";
            //string url = "http://localhost:30074/Login.aspx";
            //string url = "http://myhostels.zoninnovative.com/Login.aspx";
            //msg.Body = "Login Credentials are LoginName: " + adminRegistration.EmailId + " Password " + password + "" + " <a href='" + url + "'>CLICKME</a>";
            msg.IsBodyHtml = true;
            // your remote SMTP server IP.
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("rallbandi2015@gmail.com", "Naveen@123");
            smtp.EnableSsl = true;
            smtp.Send(msg);
        }
        /// <summary>
        /// Sending SMS to USERS
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        public void SendSMS(string Email, string Password)
        {

        }
    }
}
