/*validation for get request
  Author:V SHALINI
  Date : 20-8-2021
*/

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace WebservicesforGenericOne
{
    [TestClass]
    public class TestUsingGetEndPoint
    {
            public string token = "";
            public string userID = "";
            private static IRestResponse restResponse { get; set; }
            [TestInitialize]
            public void setup()
            {
                token = " Bearer BQD4RXTP0ZeX4c8tnUuh9uAT0CXOLqY35-oFuweTD2rYQV4kfqD4bNtNrKyYjhyFekj-i" +
                "sfz5UYon_F26csWf-kaGV_QCdHPnv48B2gt6jNR82SK0-k1tphMOg4Jxl1Q_FzhNErzSv9rjkVvva94qIAaWUrvrH" +
                "UU9WaTVSJf3roLTfp6HIbZQ8BcOxnjf-x2695dMf3d6adTsrTq90Kj7tGjLAzA9Lf1kRu2hANMgP8xJaSsLtZvOndd" +
                "_IA9pq2S9rmg1ILfq0-oHmIxq86yyTS_bjNmSQeNP8uZHe5M";
            }
            [TestMethod]
            public void get_CurrentUser()
            {

                IRestClient restClient = new RestClient();
                IRestRequest restRequest = new RestRequest("https://api.spotify.com/v1/me");
                //to check statuscode and content 
                restRequest.AddHeader("Authorization", "token" + token);
                IRestResponse restResponse = restClient.Get(restRequest);
                IRestResponse<List<JsonObjects>> restResponse1 = restClient.Get<List<JsonObjects>>(restRequest);
                var dataobjects = restResponse1.Data;
                foreach (var d in dataobjects)
                {
                    userID = d.id;
                }
                Console.WriteLine(userID);
            }

    }

}














