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
using MitBud.Services;
using System.Net.Mail;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;

namespace MitBud.Controllers
{
    public class ClientController : ApiController
    {

        [System.Web.Http.HttpPost]
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("api/CreateTaskForLoggedInUser")]
        public async Task<HttpResponseMessage> CreateTaskForLoggedInUser(TaskViewModel taskViewModel)
        {

            var userId = RequestContext.Principal.Identity.GetUserId();

            if (userId != null)
            {
                TaskProvider.SaveTaskForLoggedInUser(taskViewModel, userId);

                var dd = HttpStatusCode.Accepted;
                var responseMsg = new HttpResponseMessage(dd)
                {
                    Content = new StringContent("", Encoding.UTF8, "application/json")
                };

               Email.sendVerificationByMail(taskViewModel.ClientEmail, taskViewModel.ClientName);

            }
            //else
            //{
            //    AccountController account = new AccountController();

            //    await account.SaveTaskNotLoggedIn(taskViewModel);
            //}
            return Request.CreateResponse(HttpStatusCode.OK);

        }

        [System.Web.Http.Route("api/GetclientTask")]
        [System.Web.Http.Authorize]
        public IHttpActionResult GetClientTaskList()
        {

            IList<GetClientTaskListViewModel> taskViewModel = null;

            var CurrentuserId = RequestContext.Principal.Identity.GetUserId();

            using (MitBudDBEntities mitBud = new MitBudDBEntities())
            {
                taskViewModel = (from task in mitBud.Tasks

                                 where task.Client_id == CurrentuserId
                                 select new GetClientTaskListViewModel()
                                 {
                                     TaskId = task.TaskId,
                                     Title = task.Title,
                                     Description = task.Description,
                                     ClientStreetName = task.ClientStreetName,
                                     ClientHouseNumber = task.ClientHouseNumber,
                                     ClientPostCode = task.ClientPostCode,
                                     ClientCity = task.ClientCity,
                                     Region = task.Region,
                                     Status = task.Status,
                                 }).Distinct().ToList();

            }
            if (taskViewModel.Count == 0)
            {
                return NotFound();
            }
            return Ok(taskViewModel);

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

      
        }
    }

    







