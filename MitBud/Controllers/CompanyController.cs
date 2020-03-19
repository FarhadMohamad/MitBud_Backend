using Microsoft.AspNet.Identity;
using MitBud.DAL;
using MitBud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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
                                 from Company_Category in mitBud.Company_Category
                                 where task.Category == Company_Category.Name && Company_Category.CompanyUserId == CurrentuserId
                                 from Company in mitBud.Companies
                                 where Company.Region == task.Region
                                 select new CompanyTaskViewModel()
                                 {
                                     TaskId = task.TaskId,
                                     Title = task.Title,
                                     Category = task.Category,
                                     ClientPostCode = task.ClientPostCode,
                                     Region = Company.Region,
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
    }
}
