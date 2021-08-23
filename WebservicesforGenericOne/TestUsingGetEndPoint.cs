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
        public static string userID = "";
        public static string playlistID = "";
        public static string uri = "spotify:track:7mtYsNBYTDPa8Mscf166hg";
       

        private static IRestResponse restResponse { get; set; }
        [TestInitialize]
        public void setup()
        {
            token = "Bearer BQBnsuTmUcPcZaESycpuwPZlZZIkIg8tMyh7s4xK5t-JjLcmyTgoyupmWyyx86QNus3647y-n-Dgr" +
                "uJVbBKLPwEXX9M2KdLYDWdmGCrGV7fLKNZbar_9mZSMVYUq_1e-bkX4tNys_nG3yVZ04McdPR3EqlpxHV18XDsQ2A" +
                "f27CvvbQW6gduMAQJuMomtZskIn4WsTaCIcHs9-UPkEPfWElYpRPpJg93UAIS8XbGKK63v-I4M1GmIeNaPp5Tox0" +
                "oqOWjkHnBuH9uPTjuVMrPBZuFtCNbgE403AM4KtOjM";
        }
        [Priority(1)]
        [TestMethod]
        public void A_getCurrentUser()
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
            Console.WriteLine("Response: " + restResponse.Content);
            Console.WriteLine("userID:"+userID);
        }
        [Priority(2)]
        [TestMethod]
        public void B_CreatePlaylist()
        {
            A_getCurrentUser();




            string JsonData = "{" +
                                    "\"name\": \"shalini's Music Zone song's list\"," +
                                    "\"description\": \"New playlists created\"," +
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
            Console.WriteLine("Response: " + restResponse2.Content);
            Console.WriteLine("PlaylistID:"+playlistID);
        }

        [Priority(3)]
        [TestMethod]
        public void C_PlaylistDescriptionChanging()
        {
            //CreatePlaylist();



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
        [Priority(4)]
        [TestMethod]
        public void D_TrackAdded()
        {
            //B_CreatePlaylist();



            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest("https://api.spotify.com/v1/playlists/" + playlistID + "/tracks?uris=" + uri);
            restRequest.AddHeader("Authorization", "token" + token);
            restRequest.AddParameter("uris", uri);
            restResponse = restClient.Post(restRequest);
            Console.WriteLine((int)restResponse.StatusCode);
            Console.WriteLine(restResponse.Content);
            
        }
        [Priority(5)]



        [TestMethod]



        public void TrackDeletion()
        {
            string json = "{ \"tracks\":" +
                         "[{ \"uri\": \"spotify:track:7mtYsNBYTDPa8Mscf166hg\"}]}";



            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest("https://api.spotify.com/v1/playlists/" + playlistID + "/tracks");
            restRequest.AddHeader("Authorization", "token" + token);
            restRequest.AddJsonBody(json);
            restResponse = restClient.Delete(restRequest);
            Console.WriteLine((int)restResponse.StatusCode);
        }






    }

}














