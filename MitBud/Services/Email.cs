using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
namespace MitBud.Services
{
    public class Email
    {
        //Send notification when a new user has created a new task without loggin in for the first time
        //[AllowAnonymous]
        //[Route("sendCreatePasswordByEmail")]
        public static string sendCreatePasswordByEmail(string ToEmail, string UserName, string token)
        {
            try
            {

                SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
                var mail = new System.Net.Mail.MailMessage();
                mail.From = new MailAddress("mitbud@outlook.com");
                mail.To.Add(ToEmail);
                mail.Subject = "Your Authorization code.";
                mail.IsBodyHtml = true;
                string htmlBody;
                htmlBody = "Hi " + UserName + "," + "<br />" + "<br />"
                    + "Please create a password by clicking the following link" + "<br />" + "<br />"
                    + "http://localhost:60355/api/Account/CreatePassword?token=" + token +"&Email=" + ToEmail + "<br />" + "<br />"
                    + "Regards, " + "<br />"
                    + "MitBud.";
                mail.Body = htmlBody;
                SmtpServer.Port = 587;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new NetworkCredential("mitbud@outlook.com", "m42929264.", "Outlook.com");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                var deliveryStatus = mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                return "sent";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }

        //Send notification while chatting
        [AllowAnonymous]
        [Route("sendVerificationByMail")]
        public static string sendNotificationEmail(string clientName, string clientEmail, string companyName)
        {
            //MailAddress address = new MailAddress(email);
            //string username = address.User;

            try
            {

                SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
                var mail = new System.Net.Mail.MailMessage();
                mail.From = new MailAddress("mitbud@outlook.com");
                mail.To.Add(clientEmail);
                mail.Subject = "Your Authorization code.";
                mail.IsBodyHtml = true;
                string htmlBody;
                htmlBody = "Hi " + clientName + "," + "<br />" + "<br />"
                    + "You have received an offer from " + companyName + "<br />" + "<br />"
                    + "Regards, " + "<br />"
                    + "MicroLendr.";
                mail.Body = htmlBody;
                SmtpServer.Port = 587;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new NetworkCredential("mitbud@outlook.com", "m42929264.", "outlook.cm");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                //mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

                return "sent";


            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }

        //Send notification to the user when they created a new task
        [AllowAnonymous]
        [Route("sendVerificationByMail")]
        public static string sendVerificationByMail(string currentUser, string name)
        {
            //MailAddress address = new MailAddress(email);
            //string username = address.User;

            try
            {

                SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
                var mail = new System.Net.Mail.MailMessage();
                mail.From = new MailAddress("mitbud@Outlook.com");
                mail.To.Add(currentUser);
                mail.Subject = "Your Authorization code.";
                mail.IsBodyHtml = true;
                string htmlBody;
                htmlBody = "Hi " + name + "," + "<br />" + "<br />"
                    + "This is an automatically generated email only to notify you – please do not reply to it." + "<br />" + "<br />"
                    + "Regards, " + "<br />"
                    + "MitBud.";
                mail.Body = htmlBody;
                SmtpServer.Port = 587;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new NetworkCredential("mitbud@outlook.com", "m42929264.", "Outlook.com");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);

                return "sent";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }
    }
}