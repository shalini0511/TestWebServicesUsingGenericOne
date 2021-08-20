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
        public string playlistID = "";
        private static IRestResponse restResponse { get; set; }
        [TestInitialize]
        public void setup()
        {
            token = "Bearer BQD8hjmOg-6DbRKSox_znXK3yIFg4-e4UkfOlbhWjD52QFtK96vQ42ib564XOWUUfnogh0t" +
            "oF2PpmODMPoUnmSKCYqx42K26aTaQZZOZAEJV-N5PNbu78inbTNmH39CHS_PJ_zfwIZTTOV35mArI5kyr2wTrRP" +
            "BROJ1vo9BYar-C43uusqYWE-9GMniNmgkmqw0jPYOpYL14gtTlTZaKXU4ZZXPZLZc8X3bA9Fm1356pRC6b67fC0" +
            "GOh3HSq9wnvLj7ZLddwZ2JDQZu0rFK1UvM4HmfgNArTE60hpmoj";
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
        [TestMethod]
        public void CreatePlaylist()
        {
            get_CurrentUser();




            string JsonData = "{" +
                                    "\"name\": \"shalini Music Zone song's list\"," +
                                    "\"description\": \"New playlist created\"," +
                                    "\"public\" : \"false\"" +
                                    "}";
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest("https://api.spotify.com/v1/users/" + userID + "/playlists");



            restRequest.AddHeader("Authorization", "token" + token);



            //restRequest.AddHeader("content-Type", "application/json");
            restRequest.AddJsonBody(JsonData);
            restRequest.AddHeader("content-Type", "application/json");
            //Assert.AreEqual(201, 202, (int)restResponse.StatusCode);
            IRestResponse<List<JsonObjects>> restResponse2 = restClient.Post<List<JsonObjects>>(restRequest);
            Console.WriteLine((int)restResponse2.StatusCode);
            var dataobjects1 = restResponse2.Data;
            foreach (var d in dataobjects1)
            {
                //playlistName = d.name;
                playlistID = d.id;



            }
            Console.WriteLine(playlistID);
        }


        [TestMethod]
        public void ChangePlaylistDescription()
        {
            CreatePlaylist();
            string json = "{" +
                            "\"name\": \"Updated Music Zone song's list\"," +
                            "\"description\": \"My playlist Created\"," +
                            "\"public\" : false" +
                            "}";
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest("https://api.spotify.com/v1/playlists/" + playlistID + "/");
            restRequest.AddHeader("Authorization", "token" + token);
            restRequest.AddJsonBody(json);
            restRequest.AddHeader("content-Type", "application/json");
            restResponse = restClient.Put(restRequest);
            Console.WriteLine((int)restResponse.StatusCode);


        }
    }

}














