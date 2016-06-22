using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Alumini.Core
{
    public class Emails
    {
        public static void SendEmails(string Email, String Mobile, string Message, string Name)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("rallbandi2015@gmail.com");
            msg.To.Add(Email);
            msg.Subject = "Message";
            msg.Body = "Hi your password details are Email =" + Email + " Password =" + Message;
            msg.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("rallbandi2015@gmail.com", "Naveen@123");
            smtp.EnableSsl = true;
            smtp.Send(msg);
        }


        public static void ContactUs(string Email, String Mobile, string Message, string Name)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("rallbandi2015@gmail.com");
            msg.To.Add("coordinator@srkrecalumni.org");
            msg.Subject = "Message";
            msg.Body = "Hi Name =" + Message + "<br> Email" + Email + "<br> Mobile" + Mobile;
            msg.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("rallbandi2015@gmail.com", "Naveen@123");
            smtp.EnableSsl = true;
            smtp.Send(msg);
        }



        public static void SendEmails(string Email, string Message, string Name, string Heading, string Subject)
        {

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("rallbandi2015@gmail.com");
            msg.To.Add(Email);
            msg.Subject = Subject;
            msg.Body = "Hi new Post: Name" + Name + " Email " + Email + "Heading" + Heading + " Message" + Message + "";
            msg.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("rallbandi2015@gmail.com", "Naveen@123");
            smtp.EnableSsl = true;
            smtp.Send(msg);
        }
    }
}
