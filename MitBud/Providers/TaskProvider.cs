using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MitBud.Models;
using MitBud.DAL;

namespace MitBud.Providers
{
    public class TaskProvider
    {


        //Save Tasks to the database to the Tasks table
        public static void SaveTask(TaskViewModel TaskViewModel, string userId)
        {
            MitBudDBEntities db = new MitBudDBEntities();
            Task Task = new Task();
            Task.Title = TaskViewModel.Title;
            Task.Client_id = userId;
            Task.Description = TaskViewModel.Description;
            Task.Category = TaskViewModel.Category;
            Task.ClientName = TaskViewModel.ClientName;
            Task.ClientAddress = TaskViewModel.ClientName;
            Task.ClientPostCode = TaskViewModel.ClientPostCode;
            Task.ClientTelephone = TaskViewModel.ClientTelephone;
            Task.ClientEmail = TaskViewModel.ClientEmail;
            Task.WhoAreYou = TaskViewModel.WhoAreYou;
            Task.TaskCost = TaskViewModel.TaskCost;
            db.Tasks.Add(Task);
            db.SaveChanges();

         
        }

    }
}