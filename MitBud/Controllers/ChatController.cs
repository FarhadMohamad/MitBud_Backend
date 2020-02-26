using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MitBud.Models;
using MitBud.Providers;
using MitBud.DAL;

namespace MitBud.Controllers
{
    public class ChatController : ApiController
    {


        [System.Web.Http.HttpPost]
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("api/saveConversation")]
        public async Task<HttpResponseMessage> Conversation(ConversationViewModel conversation)
        {

            var userId = RequestContext.Principal.Identity.GetUserId();
            CompanyProvider.SaveConversation(conversation, userId );

            var statusCode = HttpStatusCode.Accepted;
            var responseMsg = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent("", Encoding.UTF8, "application/json")
            };

            return responseMsg;
        }


        //atmar

        ///[AllowAnonymous]
        //[Route("sendVerificationByMail")]
        //public string sendVerificationByMail(string currentUser)
        //{
        //    //MailAddress address = new MailAddress(email);
        //    //string username = address.User;

        //    try
        //    {

        //        SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
        //        var mail = new System.Net.Mail.MailMessage();
        //        mail.From = new MailAddress("art_ismat@hotmail.com");
        //        mail.To.Add(currentUser);
        //        mail.Subject = "Your Authorization code.";
        //        mail.IsBodyHtml = true;
        //        string htmlBody;
        //        htmlBody = "Hi " + currentUser + "," + "<br />" + "<br />"
        //            + "This is an" + currentUser + "automatically generated email only to notify you – please do not reply to it." + "<br />" + "<br />"
        //            + "Regards, " + "<br />"
        //            + "MicroLendr.";
        //        mail.Body = htmlBody;
        //        SmtpServer.Port = 587;
        //        SmtpServer.UseDefaultCredentials = false;
        //        SmtpServer.Credentials = new NetworkCredential("art_ismat@hotmail.com", "", "hotmail.com");
        //        SmtpServer.EnableSsl = true;
        //        SmtpServer.Send(mail);

        //        return "sent";
        //    }
        //    catch (Exception ex)
        //    {

        //        return ex.Message;
        //    }

        //}

        [System.Web.Http.Authorize]
        [System.Web.Http.Route("api/getConversation")]
        public IHttpActionResult GetMessage()
        {

            IList<ConversationViewModel> conversation = null;
            var CurrentuserId = RequestContext.Principal.Identity.GetUserId();

            using (MitBudDBEntities mitBud = new MitBudDBEntities())
            {
                //Table name (Comments)
                conversation = (from conv in mitBud.Conversations
                        where conv.Client_Id == CurrentuserId
                        select new ConversationViewModel()
                        {

                            Company_Id = conv.Company_Id,
                            Client_id = conv.Client_Id,
                            Message = conv.Message
                      

                        }).ToList();
            }

            if (conversation.Count == 0)
            {
                return NotFound();
            }
            return Ok(conversation);
        }






        }
    }
