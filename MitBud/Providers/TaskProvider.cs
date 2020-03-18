using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MitBud.Models;
using MitBud.DAL;
using Microsoft.AspNetCore.Identity;
using MitBud.Controllers;
using Microsoft.AspNet.Identity;
using System.Web.Http;
using System.Net.Mail;
using System.Text;
using System.Web.Routing;
using System.Net;

namespace MitBud.Providers
{
    public class TaskProvider
    {


        //Save Tasks to the database to the Tasks table
        public static void SaveTaskLoggedIn(TaskViewModel taskViewModel, string userId)
        {
            AccountController accountController = new AccountController();
            var regionName = accountController.GetMunicipalityCode(taskViewModel.ClientStreetName, taskViewModel.ClientPostCode.ToString(), taskViewModel.ClientHouseNumber, taskViewModel.ClientCity);
            MitBudDBEntities db = new MitBudDBEntities();
            Task Task = new Task();
            Task.Title = taskViewModel.Title;
            Task.Client_id = userId;
            Task.Description = taskViewModel.Description;
            Task.Category = taskViewModel.Category;
            Task.ClientName = taskViewModel.ClientName;
            Task.ClientStreetName = taskViewModel.ClientStreetName;
            Task.ClientHouseNumber = taskViewModel.ClientHouseNumber;
            Task.ClientPostCode = taskViewModel.ClientPostCode;
            Task.Region = regionName;
            Task.ClientCity = taskViewModel.ClientCity;
            Task.ClientTelephone = taskViewModel.ClientTelephone;
            Task.ClientEmail = taskViewModel.ClientEmail;
            Task.WhoAreYou = taskViewModel.WhoAreYou;
            Task.TaskCost = taskViewModel.TaskCost;
            Task.Date = taskViewModel.Date;
            Task.DesiredDate = taskViewModel.DesiredDate;
            Task.Image = taskViewModel.Image;
            db.Tasks.Add(Task);
            db.SaveChanges();


        }


        //Save Task to the database in the Tasks table
        public static void SaveTaskNotLoggedIn(TaskViewModel taskViewModel, string userId)
        {


            MitBudDBEntities db = new MitBudDBEntities();
            Task Task = new Task();
            Task.Client_id = userId;
            Task.Title = taskViewModel.Title;
            Task.Description = taskViewModel.Description;
            Task.Category = taskViewModel.Category;
            Task.ClientName = taskViewModel.ClientName;
            Task.ClientStreetName = taskViewModel.ClientStreetName;
            Task.ClientHouseNumber = taskViewModel.ClientHouseNumber;
            Task.ClientPostCode = taskViewModel.ClientPostCode;
            Task.Region = taskViewModel.Region;
            Task.ClientCity = taskViewModel.ClientCity;
            Task.ClientTelephone = taskViewModel.ClientTelephone;
            Task.ClientEmail = taskViewModel.ClientEmail;
            Task.WhoAreYou = taskViewModel.WhoAreYou;
            Task.TaskCost = taskViewModel.TaskCost;
            Task.Date = taskViewModel.Date;
            Task.DesiredDate = taskViewModel.DesiredDate;
            Task.Image = taskViewModel.Image;
            db.Tasks.Add(Task);
            db.SaveChanges();

            //SendPasswordResetEmail(taskViewModel.ClientEmail, taskViewModel.ClientName);

            
            //ChangePasswordBindingModel ch = new ChangePasswordBindingModel();
            //ch.OldPassword = randomPass;



        }



        //public static void SendPasswordResetEmail(string ToEmail, string UserName)
        //{
        //    // MailMessage class is present is System.Net.Mail namespace
        //    MailMessage mailMessage = new MailMessage("atmar@hotmail.dk", ToEmail);


        //    // StringBuilder class is present in System.Text namespace
        //    StringBuilder sbEmailBody = new StringBuilder();
        //    sbEmailBody.Append("Dear " + UserName + ",<br/><br/>");
        //    sbEmailBody.Append("Please click on the following link to reset your password");
        //    sbEmailBody.Append("<br/>"); sbEmailBody.Append("http://localhost/WebApplication1/Registration/ChangePassword.aspx?uid=");
        //    sbEmailBody.Append("<br/><br/>");
        //    sbEmailBody.Append("<b>Pragim Technologies</b>");

        //    mailMessage.IsBodyHtml = true;

        //    mailMessage.Body = sbEmailBody.ToString();
        //    mailMessage.Subject = "Reset Your Password";
        //    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

        //    smtpClient.Credentials = new System.Net.NetworkCredential()
        //    {
        //        UserName = "atmar@hotmail.dk",
        //        Password = "mursal1506"
        //    };

        //    smtpClient.EnableSsl = true;
        //    smtpClient.Send(mailMessage);
        //}
      
        }

    }
