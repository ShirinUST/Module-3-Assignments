using CaseStudy_1_RestfulBooker.Models;
using CaseStudy_1_RestfulBooker.Utilities;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy_1_RestfulBooker.Tests.Endpoints
{
    [TestFixture]
    internal class RestfulBookerTest:CoreCodes
    {
        [Test,Order(1)]
        public void GetBookingIdsTest()
        {
            test = extent.CreateTest("Get Booking Id Test");
            Log.Information("Get Booking Id Test started");
            var request = new RestRequest("booking", Method.Get);
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("Got all booking id");
                JArray userData = (JArray)JsonConvert.DeserializeObject(response.Content);  
                Console.WriteLine(userData[0]["bookingid"]);
                Assert.NotNull(userData);
                Log.Information("Get Booking Id Test Passed all asserts");
                test.Pass("Get Booking Id Test Passed all asserts");

            }
            catch (AssertionException ex)
            {
                test.Fail("Get Booking Id Test Failed");
                Log.Information($"{ex.Message}");
                Log.Information("Get Booking Id Test Failed");
            }
        }
        [Test, Order(2)]
        [TestCase(2)]
        public void GetBookingTest(int id)
        {
            test = extent.CreateTest("Get Booking Test");
            Log.Information("Get Booking Test started");
            var request = new RestRequest("booking/"+id, Method.Get);
            request.AddHeader("Accept", "application/json");
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API response: {response.Content}");
                var userData = JsonConvert.DeserializeObject<Booking>(response.Content);
                Assert.NotNull(userData.FirstName);
                Log.Information("Returned Booking Information");
                Log.Information("Get Booking Test Passed");
                test.Pass("Get Booking Test passed all asserts");
               
            }
            catch (AssertionException ex)
            {
                test.Fail("Get Booking Test failed");
                Log.Information($"{ex.Message}");
                Log.Information("Get Booking Test failed");
            }

        }
        [Test, Order(3)]
        public void CreateBookingTest()
        {
            test = extent.CreateTest("Create Booking Test");
            Log.Information("Create Booking Test started");
            var createUserRequest = new RestRequest("booking", Method.Post);
            createUserRequest.AddHeader("Content-Type", "application/json");
            createUserRequest.AddHeader("Accept", "application/json");
            createUserRequest.AddJsonBody(new
            {
                firstname = "Manu",
                lastname = "John",
                totalprice = 200,
                depositpaid = true,
                bookingdates = new
                {
                    checkin = "13-10-2018",
                    checkout = "13-10-2019"
                },
                additionalneeds = "Lunch"
            });

            var createUserResponse = client.Execute(createUserRequest);
            try
            {
                Assert.That(createUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("Created User Successfully");
                var userData = JsonConvert.DeserializeObject<Booking>(createUserResponse.Content);
                Assert.NotNull(userData);
                Log.Information("Created and returned the booking details");
                Log.Information("Create User Test Passed");
                test.Pass("Create User Test Passed all asserts");
            }
            catch (AssertionException ex)
            {
                test.Fail("Create Booking Test- Failed");
                Log.Information($"{ex.Message}");
                Log.Information("Create Booking Test - Failed");
            }
        }
       
        [Test, Order(4)]
        [TestCase(2)]
        public void UpdateBookingTest(int id)
        {
            var token = UpdateToken();
            Log.Information("Token Generated");
            try
            {
                Log.Information("Update Booking test started");
                var requestput = new RestRequest("booking/"+id, Method.Put);
                requestput.AddHeader("Content-Type", "application/json");
                requestput.AddHeader("Accept", "application/json");
                requestput.AddHeader("Cookie", "token=" + token.Token);
                Log.Information("At header added cookie with token value");

                requestput.AddJsonBody(new
                {
                    firstname = "John",
                    lastname = "Smith",
                    totalprice = 111,
                    depositpaid = true,
                    bookingdates = new
                    {
                        checkin = "2018-01-01",
                        checkout = "2019-01-01"
                    },
                    additionalneeds = "Breakfast"
                });
                var responseput = client.Execute(requestput);
                Assert.That(responseput.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Status code is not 200");
                Log.Information("Update Booking test passed");
                Console.WriteLine(responseput.Content);
            }
            catch (AssertionException)
            {
                test.Fail("Update Booking test failed");
            }
        }
        /*
        [Test, Order(0)]
        public void GetTokenTest()
        {
            test = extent.CreateTest("GetToken Test");
            Log.Information("GetToken Test Started");
            var request = new RestRequest("auth", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                username = "admin",
                password = "password123"
            });
            try
            {
                var response = client.Execute(request);
                Console.WriteLine(response.Content);
                Assert.That(response.Content, Is.Not.Null, "Response is null");
                Log.Information("Request Intitiated");
                test.Pass("GetToken Test Passed");
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Status code doest match");
                Log.Information("Succees Response Recieved");
                test.Pass("Status code Test Successful");
            }
            catch (AssertionException)
            {
                test.Fail("GetToken test failed");
            }
        }
        */
        [Test, Order(5)]
        [TestCase(12)]
        public void DeleteBookingTest(int id)
        {
            test = extent.CreateTest("Delete Booking test");
            Log.Information("DeleteBooking Test started");
            var token = UpdateToken();
            Log.Information("Token Generated");
            var request = new RestRequest("booking/"+id, Method.Delete);
            try
            {
                request.AddHeader("Cookie", "token=" + token.Token);
                Log.Information("At header added cookie with token value");
                var response = client.Execute(request);
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created), "Status code is not 200");
                test.Pass("Status code test pass");
                Log.Information("Status code test passed");
                test.Pass("Booking id data test pass");
                Log.Information("Booking id data test passed");
                Log.Information("DeleteBooking test passed all Asserts");

                test.Pass("DeleteBooking test passed all Asserts.");
            }
            catch (AssertionException)
            {
                test.Fail("DeleteBooking test failed");
            }
        }
    }
}
