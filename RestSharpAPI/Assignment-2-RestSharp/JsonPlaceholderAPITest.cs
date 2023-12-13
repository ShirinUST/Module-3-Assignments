using Assignment_2_RestSharpAPITest;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2_RestSharp
{
    internal class JsonPlaceholderAPITest
    {
        private RestClient client;
        private string baseUrl = "https://jsonplaceholder.typicode.com/";

        [SetUp]
        public void Setup()
        {
            client = new RestClient(baseUrl);
        }
        [Test, Order(1)]
        public void GetSingleUser()
        {
            var request = new RestRequest("posts/3", Method.Get);

            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var userData = JsonConvert.DeserializeObject<PostData>(response.Content);

            Assert.NotNull(userData);
            Assert.That(userData.Id, Is.EqualTo(3));
            Assert.IsNotEmpty(userData.UserId.ToString());

        }

        [Test, Order(2)]
        public void CreateUser()
        {
            var createUserRequest = new RestRequest("posts", Method.Post);
            createUserRequest.AddHeader("Content-Type", "application/json");
            createUserRequest.AddJsonBody(new { userId =1, id=3,title = "Wings of Fire",body= "Autobiography of A P J Abdulkalam" });

            var createUserResponse = client.Execute(createUserRequest);
            Assert.That(createUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
            var userData = JsonConvert.DeserializeObject<PostData>(createUserResponse.Content);
            Assert.NotNull(userData);
        }
        [Test, Order(3)]
        //PUT
        public void UpdateUser()
        {
            var updateUserRequest = new RestRequest("posts/2", Method.Put);
            updateUserRequest.AddHeader("Content-Type", "application/json");
            updateUserRequest.AddJsonBody(new { title = "Wings" });

            var updateUserResponse = client.Execute(updateUserRequest);
            Assert.That(updateUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            var userData = JsonConvert.DeserializeObject<PostData>(updateUserResponse.Content);
            Assert.NotNull(userData);
        }
        [Test, Order(4)]
        public void DeleteUser()
        {
            var deleteUserRequest = new RestRequest("posts/2", Method.Delete);
            var deleteUserResponse = client.Execute(deleteUserRequest);
            Assert.That(deleteUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
        }
        [Test, Order(5)]
        public void GetNonExistingUser()
        {
            var request = new RestRequest("posts/1000", Method.Get);
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
        }
        [Test, Order(6)]
        public void GetAllUsers()
        {
            var request = new RestRequest("posts", Method.Get);
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            List<PostData>? userData = JsonConvert.DeserializeObject<List<PostData>>(response.Content);
            int i = 1;
            foreach (var item in userData)
            {
                Console.WriteLine(item.Id);
                Assert.That(item.Id, Is.EqualTo(i));
                    i++;
            }
            Assert.NotNull(userData);

        }
    }
}
