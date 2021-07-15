using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestAPI.Model;
using RestSharp;
using RestSharp.Serialization.Json;

namespace RestAPI
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts/{postid}", Method.GET);

            request.AddUrlSegment("postid", 1);

            var response = client.Execute(request);

            //deserialize using JsonDeserializer
            //
            //var deserialize = new JsonDeserializer();
            //var output = deserialize.Deserialize<Dictionary<string, string>>(response);
            //var result = output["author"];
            //Assert.AreEqual(result, "typicode", "Authors are not equal");

            //deserialize using JObject
            JObject obs = JObject.Parse(response.Content);
            Assert.AreEqual(obs["author"].ToString(), "typicode", "Authors are not equal");
        }
        [TestMethod]
        public void PostWithAnonumousBody()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts/{postid}/profile", Method.POST);

            request.AddJsonBody(new { name = "Jackie" })    ;
            request.AddUrlSegment("postid", 1);
            var response = client.Execute(request);

            var deserialize = new JsonDeserializer();
            var output = deserialize.Deserialize<Dictionary<string, string>>(response);
            var result = output["name"];

            Assert.AreEqual(result, "Jackie", "Names do not match.");
        }
        [Ignore]
        [TestMethod]
        public void PostWithTypeBody()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts", Method.POST);

            request.AddJsonBody(new Posts() { id = "16", author = "Execute automation!", title = "RestSharpDemo" });
            
            var response = client.Execute(request);

            var deserialize = new JsonDeserializer();
            var output = deserialize.Deserialize<Dictionary<string, string>>(response);
            var result = output["author"];
                      
            Assert.AreEqual(result, "Execute automation!", "Names do not match.");
        }
        [TestMethod]
        public void PostWithGenericTypeBody()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts", Method.POST);

            request.AddJsonBody(new Posts() { id = "17", author = "Execute automation!", title = "RestSharpDemo" });

            var response = client.Execute<Posts>(request);

            //we can deserialize like this, but if we use generics(class previously created),better deserialization is in assert.

            //var deserialize = new JsonDeserializer();
            //var output = deserialize.Deserialize<Dictionary<string, string>>(response);
            //var result = output["author"];
            //Assert.AreEqual(result, "Execute automation!", "Names do not match.");

            // to delete use DELETE method

            //request = new RestRequest("posts/{postid}", Method.DELETE);
            //request.AddUrlSegment("postid", 13);
            //var response1 = client.Execute(request);
            //Assert.AreEqual(result, "Execute automation!", "Names do not match.");
            Assert.AreEqual(response.Data.author, "Execute automation!", "Names do not match.");

        }
        [TestMethod]
        public void PostWithAsync()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts", Method.POST);

            request.AddJsonBody(new Posts() { id = "18", author = "Execute automation!", title = "RestSharpDemo" });
                       
            var response =  ExecuteAsyncRequest<Posts>(client, request).GetAwaiter().GetResult();
            Assert.AreEqual(response.Data.author, "Execute automation!", "Names do not match.");

        }

        
        private async Task<IRestResponse<T>> ExecuteAsyncRequest<T>(RestClient client, IRestRequest request) where T: class, new ()
        {
            var taskCompletionSource = new TaskCompletionSource<IRestResponse<T>>();

            client.ExecuteAsync<T>(request, restResponce =>
             {
                 if (restResponce.ErrorException != null) 
                 {
                     const string message = "Error retrieving response.";
                     throw new ApplicationException(message, restResponce.ErrorException);
                 }
                 taskCompletionSource.SetResult(restResponce);
             });
            return await taskCompletionSource.Task;
            }
       }
}
