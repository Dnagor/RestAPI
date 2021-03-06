using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
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
            Assert.AreEqual("-68.6102", response.Data.address.geo.lat, "lattitude is not equal");
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
            Assert.AreEqual("Keebler LLC", user.company.name, "Smth went wrong.");
        }
        [TestMethod]
        //in this test we take all data and check for unique value
        public void GetfromAllTest()
        {
            var client = new RestClient("https://jsonplaceholder.typicode.com/");
            var request = new RestRequest("users", Method.GET);
            var response = client.Execute<List<User>>(request);
            bool flag = false;
            foreach (User item in response.Data)
            {
                if(item.address.zipcode.Equals("31428-2261"))
                    flag = true;
            }
            Assert.IsTrue(flag, "There is no user with such zipcode.");
        }
    }
}
