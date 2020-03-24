using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace MitBud.Services
{
    public class Denmark_addressess
    {
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.AllowAnonymous]
        //[System.Web.Http.Route("GetMuncipalityCode")]
        public static string GetMunicipalityCode(string address, string postCode, string streetNr, string cityName)
        {

            //TaskViewModel taskViewModel = new TaskViewModel();
            string streetName = address;
            string streetNumber = streetNr;
            string postNr = postCode;
            string city = cityName;


            string url = "https://dawa.aws.dk/autocomplete?caretpos=28&fuzzy=&q=" + streetName + " " + streetNr + "," +
                         " " + postNr + " " + city + "&startfra=adresse&type=adresse";
            string urlResult = url;
            string data = "";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            if (resp.StatusCode == HttpStatusCode.OK)
            {
                Stream recStream = resp.GetResponseStream();
                StreamReader readStream = null;
                if (resp.CharacterSet == null)
                {
                    readStream = new StreamReader(recStream);
                }
                else
                {
                    readStream = new StreamReader(recStream, Encoding.GetEncoding(resp.CharacterSet));
                }

                data = readStream.ReadToEnd();
                resp.Close();
                readStream.Close();
            }

            List<dynamic> json = JsonConvert.DeserializeObject<List<dynamic>>(data);
            var test = (string)json[0]["data"]["kommunekode"];

            var regionName = GetRegionName(test);
            return regionName;

        }

        //[System.Web.Http.HttpPost]
        //[System.Web.Http.AllowAnonymous]
        //[System.Web.Http.Route("GetRegionName")]
        public static string GetRegionName(string muncipalityCode)
        {

            string url = "https://dawa.aws.dk/kommuner/" + muncipalityCode;
            string urlResult = url;
            string data = "";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            if (resp.StatusCode == HttpStatusCode.OK)
            {
                Stream recStream = resp.GetResponseStream();
                StreamReader readStream = null;
                if (resp.CharacterSet == null)
                {
                    readStream = new StreamReader(recStream);
                }
                else
                {
                    readStream = new StreamReader(recStream, Encoding.GetEncoding(resp.CharacterSet));
                }

                data = readStream.ReadToEnd();
                resp.Close();
                readStream.Close();
            }

            var userObj = JObject.Parse(data);

            var userGuid = Convert.ToString(userObj["region"]["navn"]);

            return userGuid;

        }
    }
}