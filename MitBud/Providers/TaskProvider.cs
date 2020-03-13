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
        public static void SaveTaskLoggedIn(TaskViewModel TaskViewModel, string userId)
        {
            MitBudDBEntities db = new MitBudDBEntities();
            Task Task = new Task();
            Task.Title = TaskViewModel.Title;
            Task.Client_id = userId;
            Task.Description = TaskViewModel.Description;
            Task.Category = TaskViewModel.Category;
            Task.ClientName = TaskViewModel.ClientName;
            Task.ClientStreetName = TaskViewModel.ClientStreetName;
            Task.ClientHouseNumber = TaskViewModel.ClientHouseNumber;
            Task.ClientPostCode = TaskViewModel.ClientPostCode;
            Task.Region = TaskViewModel.Region;
            Task.ClientCity = TaskViewModel.ClientCity;
            Task.ClientTelephone = TaskViewModel.ClientTelephone;
            Task.ClientEmail = TaskViewModel.ClientEmail;
            Task.WhoAreYou = TaskViewModel.WhoAreYou;
            Task.TaskCost = TaskViewModel.TaskCost;
            db.Tasks.Add(Task);
            db.SaveChanges();


        }


        //Save Task to the database in the Tasks table
        public static void SaveTask(TaskViewModel TaskViewModel, string userId)
        {


            MitBudDBEntities db = new MitBudDBEntities();
            Task Task = new Task();
            Task.Client_id = userId;
            Task.Title = TaskViewModel.Title;
            Task.Description = TaskViewModel.Description;
            Task.Category = TaskViewModel.Category;
            Task.ClientName = TaskViewModel.ClientName;
            Task.ClientStreetName = TaskViewModel.ClientStreetName;
            Task.ClientHouseNumber = TaskViewModel.ClientHouseNumber;
            Task.ClientPostCode = TaskViewModel.ClientPostCode;
            Task.Region = TaskViewModel.Region;
            Task.ClientCity = TaskViewModel.ClientCity;
            Task.ClientTelephone = TaskViewModel.ClientTelephone;
            Task.ClientEmail = TaskViewModel.ClientEmail;
            Task.WhoAreYou = TaskViewModel.WhoAreYou;
            Task.TaskCost = TaskViewModel.TaskCost;
            db.Tasks.Add(Task);
            db.SaveChanges();

            //SendPasswordResetEmail(TaskViewModel.ClientEmail, TaskViewModel.ClientName);

            
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
