using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MitBud.DAL;
using MitBud.Models;

namespace MitBud.Providers
{
    public class ClientProvider
    {
        public static void SaveClientInfo(RegisterBindingModel registerViewModel, string UserId)
        {
            MitBudDBEntities db = new MitBudDBEntities();
            Client client = new Client();
            client.Client_Id = UserId;
            client.Name = registerViewModel.Name;
            client.Email = registerViewModel.Email;
          
            db.Clients.Add(client);
            db.SaveChanges();


        }
    }
}