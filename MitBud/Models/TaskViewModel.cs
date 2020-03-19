using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MitBud.Models
{
    public class TaskViewModel
    {

        public int  TaskId { get; set; }
        public string Title { get; set; }
        //public string Client_id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ClientName { get; set; }
        public string ClientPostCode { get; set; }
        public string Region { get; set; }
        public string ClientStreetName { get; set; }
        public string ClientHouseNumber { get; set; }
        public string ClientCity { get; set; }
        public string ClientTelephone { get; set; }
        public string ClientEmail { get; set; }
        public string WhoAreYou { get; set; }
        public decimal TaskCost { get; set; }
        public DateTime? Date { get; set; }
        public string DesiredDate { get; set; }
        public byte[] Image { get; set; }
        public int Status { get; set; } = 0;
    }
}