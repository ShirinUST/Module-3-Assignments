
using Assignment_3_RestSharpAPITest.Models;
using Assignment_3_RestSharpAPITest.Utilities;
using AventStack.ExtentReports.Model;
using Newtonsoft.Json;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log = Serilog.Log;

namespace Assignment_3_RestSharpAPITest.Tests.Endpoints
{
    [TestFixture]
    internal class JsonPlaceholderTest:CoreCodes
    {
        [Test, Order(1)]
        public void GetSingleUser()
        {
            test = extent.CreateTest("Get single user Test");
            Log.Information("Get Single User Test started");
            var request = new RestRequest("posts/3", Method.Get);

            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API response: {response.Content}");
                var userData = JsonConvert.DeserializeObject<PostData>(response.Content);

                Assert.NotNull(userData);
                Log.Information("Returned User Information");
                Assert.That(userData.Id, Is.EqualTo(3));
                Log.Information("Id is same as what we are checking");
                Assert.IsNotEmpty(userData.UserId.ToString());
                Log.Information("Email is not empty");
                Log.Information("Get single user test passed");
                test.Pass("Test Passed all asserts");
            }
            catch(AssertionException ex)
            {
                test.Fail("Get single user test failed");
                Log.Information($"{ex.Message}");
                Log.Information("Get single user test failed");
            }

        }

        [Test, Order(2)]
        public void CreateUser()
        {
            test = extent.CreateTest("Create User Test");
            Log.Information("Create User Test started");
            var createUserRequest = new RestRequest("posts", Method.Post);
            createUserRequest.AddHeader("Content-Type", "application/json");
            createUserRequest.AddJsonBody(new { userId = 1, id = 3, title = "Wings of Fire", body = "Autobiography of A P J Abdulkalam" });

            var createUserResponse = client.Execute(createUserRequest);
            try
            {
                Assert.That(createUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                Log.Information("Created User Successfully");
                var userData = JsonConvert.DeserializeObject<PostData>(createUserResponse.Content);
                Assert.NotNull(userData);
                Log.Information("Created and returned the user");
                Log.Information("Create User Test Passed");
                test.Pass("Create User Test Passed all asserts");
            }
            catch(AssertionException ex)
            {
                test.Fail("Create User Test- Failed");
                Log.Information($"{ex.Message}");
                Log.Information("Create User Test- Failed");
            }
        }
        [Test, Order(3)]
        //PUT
        public void UpdateUser()
        {
            test = extent.CreateTest("Update User Test");
            Log.Information("Update User Test Started");
            var updateUserRequest = new RestRequest("posts/2", Method.Put);
            updateUserRequest.AddHeader("Content-Type", "application/json");
            updateUserRequest.AddJsonBody(new { title = "Wings" });

            var updateUserResponse = client.Execute(updateUserRequest);
            try
            {
                Assert.That(updateUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"User updated as {updateUserResponse.Content}");
                var userData = JsonConvert.DeserializeObject<PostData>(updateUserResponse.Content);
                Assert.NotNull(userData);
                Log.Information("Updated Successfully");
                Log.Information("Update User Test passed");
                test.Pass("Update User Test passed all asserts");
            }
            catch(AssertionException ex)
            {
                test.Fail("Update User Test-failed");
                Log.Information($"{ex.Message}");
                Log.Information("Test failed");
            }
        }
        [Test, Order(4)]
        public void DeleteUser()
        {
            test = extent.CreateTest("Delete User Test");
            Log.Information("Delete User Test Started");
            var deleteUserRequest = new RestRequest("posts/2", Method.Delete);
            var deleteUserResponse = client.Execute(deleteUserRequest);
            try
            {
                Assert.That(deleteUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("User Deleted");
                Log.Information("Delete User test passed all Asserts");

                test.Pass("Delete User test passed all Asserts.");
            }
            catch(AssertionException ex)
            {
                test.Fail("Delete User test failed");
                Log.Information($"{ex.Message}");
                Log.Information("Test failed");
            }
        }
        [Test, Order(5)]
        public void GetNonExistingUser()
        {
            test = extent.CreateTest("Get Non Existing User Test");
            Log.Information("Get Non Existing User Test Started");
            var request = new RestRequest("posts/1000", Method.Get);
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
                Log.Information("Get Non Existing User test passed all Asserts");

                test.Pass("Get Non Existing test passed all Asserts.");
            }
            catch(AssertionException ex)
            {
                test.Fail("Get Non Existing test failed");
                Log.Information($"{ex.Message}");
                Log.Information("Test failed");
            }
        }
        [Test, Order(6)]
        public void GetAllUsers()
        {
            test = extent.CreateTest("Get All User Test");
            Log.Information("Get all user test started");
            var request = new RestRequest("posts", Method.Get);
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("Got all user");
                List<PostData>? userData = JsonConvert.DeserializeObject<List<PostData>>(response.Content);
                int i = 1;
                foreach (var item in userData)
                {
                    Console.WriteLine(item.Id);
                    Assert.That(item.Id, Is.EqualTo(i));
                    i++;
                }
                Assert.NotNull(userData);
                Log.Information("Get all User Test Passed all asserts");
                test.Pass("Get all User Test Passed all asserts");

            }catch(AssertionException ex)
            {
                test.Fail("Get all user test failed");
                Log.Information($"{ex.Message}");
                Log.Information("Get all user test failed");
            }

        }
    }
}
