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
using MitBud.Services;
using System.Net.Mail;

namespace MitBud.Controllers
{
    public class ChatController : ApiController
    {


        [System.Web.Http.HttpPost]
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("api/Conversation")]
        public async Task<HttpResponseMessage> Conversation(ConversationViewModel conversation)
        {
            MitBudDBEntities mitBudDB = new MitBudDBEntities();

            var userId = RequestContext.Principal.Identity.GetUserId();

            var CompanyEmail = mitBudDB.Companies.Where(x => x.UserId == userId).SingleOrDefault();

            //var clientEmail = mitBudDB.Clients.Where(x => x.Client_Id == conversation.Client_id).SingleOrDefault();

          

            var clientId = mitBudDB.Tasks.Where(x => x.TaskId == conversation.TaskID).SingleOrDefault();

            //var companyName = CompanyEmail.CompanyName;

            CompanyProvider.SaveConversation(conversation, userId, clientId.Client_id );

            var statusCode = HttpStatusCode.Accepted;

            var responseMsg = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent("", Encoding.UTF8, "application/json")
            };
         
           
           Email.sendNotificationEmail(clientId.ClientName, clientId.ClientEmail, CompanyEmail.CompanyName);
            return responseMsg;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("api/getMessage")]
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
                            TaskID = conv.Task_Id,
                            Company_id = conv.Company_Id,
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
