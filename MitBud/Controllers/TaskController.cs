using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MitBud.Providers;
using MitBud.Models;
using System.Text;
using MitBud.DAL;
using System.Net.Mail;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;

namespace MitBud.Controllers
{
    public class TaskController : ApiController
    {

        [System.Web.Http.HttpPost]
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("api/SaveTaskLoggedIn")]
        public async Task<HttpResponseMessage> SaveTaskLoggedIn(TaskViewModel taskViewModel)
        {

            var userId = RequestContext.Principal.Identity.GetUserId();

            if (userId != null)
            {
                TaskProvider.SaveTaskLoggedIn(taskViewModel, userId);

                var dd = HttpStatusCode.Accepted;
                var responseMsg = new HttpResponseMessage(dd)
                {
                    Content = new StringContent("", Encoding.UTF8, "application/json")
                };

                sendVerificationByMail(taskViewModel.ClientEmail, taskViewModel.ClientName);

            }
            //else
            //{
            //    AccountController account = new AccountController();

            //    await account.SaveTaskNotLoggedIn(taskViewModel);
            //}
            return Request.CreateResponse(HttpStatusCode.OK);

        }



        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/UpdateTaskStatus")]
        [System.Web.Http.Authorize]
        public async Task<HttpResponseMessage> TaskStatus(int taskId, int? status)
        {

            var userId = RequestContext.Principal.Identity.GetUserId();

            if (userId != null)
            {
                TaskProvider.SaveTaskStatus(taskId, userId, status);

                var dd = HttpStatusCode.Accepted;
                var responseMsg = new HttpResponseMessage(dd)
                {
                    Content = new StringContent("", Encoding.UTF8, "application/json")
                };


            }

            return Request.CreateResponse(HttpStatusCode.OK);


        }

        [System.Web.Http.Route("api/GetclientTask")]
        [System.Web.Http.Authorize]
        public IHttpActionResult GetClientTask()
        {
            
            IList<TaskViewModel> taskViewModel = null;           

            var CurrentuserId = RequestContext.Principal.Identity.GetUserId();

            using (MitBudDBEntities mitBud = new MitBudDBEntities())
            {
                taskViewModel = (from task in mitBud.Tasks
                              
                                 where task.Client_id == CurrentuserId
                                 select new TaskViewModel()
                                 {
                                     TaskId = task.TaskId,
                                     Title = task.Title,
                                     Description = task.Description,
                                     ClientStreetName = task.ClientStreetName,
                                     ClientHouseNumber = task.ClientHouseNumber,
                                     ClientPostCode = task.ClientPostCode,
                                     ClientCity = task.ClientCity,
                                     Region = task.Region,                                
                                 }).Distinct().ToList();

            }
            if (taskViewModel.Count == 0)
            {
                return NotFound();
            }
            return Ok(taskViewModel);

        }





        [AllowAnonymous]
        [Route("sendVerificationByMail")]
        public string sendVerificationByMail(string currentUser, string name)
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
    







