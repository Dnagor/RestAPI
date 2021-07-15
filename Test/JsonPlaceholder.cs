using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestAPI.Model;
using RestSharp;
using RestSharp.Serialization.Json;

namespace RestAPI.Test
{
    [TestClass]
    public class JsonPlaceholder
    {
        [TestMethod]
        public void UserReadTest()
        {
            var client = new RestClient("https://jsonplaceholder.typicode.com/");
            var request = new RestRequest("users/{user}", Method.GET);
            request.AddUrlSegment("user", 2);
            var response = client.Execute(request);
            JObject job = JObject.Parse(response.Content);
            Assert.AreEqual(job["name"].ToString(), "Ervin Howell", "names dont match");
        }
        [TestMethod]
        public void CompareCityTest()
        {
            var client = new RestClient("https://jsonplaceholder.typicode.com/");
            var request = new RestRequest("users/{user}", Method.GET);
            request.AddUrlSegment("user", 3);
            var response = client.Execute<User>(request);
            Assert.AreEqual("ramiro.info", response.Data.website, "Websites are not equal");
            Assert.AreEqual("-68.6102", response.Data.address[0].geo[0].lat, "lattitude is not equal");
        }
        [TestMethod]
        public void CompareCompanyNameTest()
        {
            var client = new RestClient("https://jsonplaceholder.typicode.com/");
            var request = new RestRequest("users/{user}", Method.GET);
            request.AddUrlSegment("user", 5);
            var response = client.Execute(request);
            User user = new JsonDeserializer().
                Deserialize<User>(response);
            Assert.AreEqual("Keebler LLC", user.company[0].name, "Smt went wrong.");
        }
    }
}
