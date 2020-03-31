using Microsoft.AspNet.Identity;
using MitBud.DAL;
using MitBud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MitBud.Services;
using System.Text;
using System.Threading.Tasks;
using MitBud.Models;
using MitBud.Providers;

namespace MitBud.Controllers
{
    public class CompanyController : ApiController
    {

        [System.Web.Http.Route("api/GetTask")]
        [System.Web.Http.Authorize]
        public IHttpActionResult GetTask()
        {
            // var mitBud = new MitBudDBEntities();

            IList<CompanyTaskViewModel> companyTaskVM = null;

            //List<string> ITEMS = new List<string>();

            var CurrentuserId = RequestContext.Principal.Identity.GetUserId();

            using (MitBudDBEntities mitBud = new MitBudDBEntities())
            {
                companyTaskVM = (from task in mitBud.Tasks
                                 join c in mitBud.Companies on task.Region equals c.Region

                                 where c.Company_Category.Select(x => x.Name).Contains(task.Category) && c.Company_Category.Select(x => x.CompanyUserId).Contains(CurrentuserId)
                                 
                                 select new CompanyTaskViewModel()
                                 {
                                     TaskId = task.TaskId,
                                     Title = task.Title,
                                     Category = task.Category,
                                     ClientPostCode = task.ClientPostCode,
                                     Region = task.Region,
                                     ClientName = task.ClientName,
                                     Description = task.Description,


                                 }).Distinct().ToList();



            }
            if (companyTaskVM.Count == 0)
            {
                return NotFound();
            }
            return Ok(companyTaskVM);

        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/RegisterCompany")]
        public IHttpActionResult RegisterCompany(RegisterCompany registerCompany)
        {

     
         
         
            Email.sendEmailToAdmin(registerCompany);


            return Ok();


        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/CompanyCredit")]
        [System.Web.Http.Authorize]
        public IHttpActionResult CompanyCredit()
        {
            MitBudDBEntities mitBud = new MitBudDBEntities();
            var userId = RequestContext.Principal.Identity.GetUserId();

            
            var findCompanyCredit = mitBud.Companies.Where(x => x.UserId == userId).SingleOrDefault();
           

            if (findCompanyCredit != null)
            {
                var substractCompanyCredit = findCompanyCredit.CompanyCredit = findCompanyCredit.CompanyCredit - 1;

                if (substractCompanyCredit >-1)
                {
                    CompanyProvider.updateCredit(substractCompanyCredit, userId);
                }
                else
                {

                    return Content(HttpStatusCode.BadRequest, "You have no credit left");
                }
            
            }
            else
            {
                return NotFound();
            }


            return Ok();

            //    var dd = HttpStatusCode.Accepted;
            //    var responseMsg = new HttpResponseMessage(dd)
            //    {
            //        Content = new StringContent("", Encoding.UTF8, "application/json")
            //    };


            //}



        }


    }


}
