using CaseStudy_1_RestfulBooker.Models;
using CaseStudy_1_RestfulBooker.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy_1_RestfulBooker.Tests.Endpoints
{
    [TestFixture]
    internal class RestfulBookerTest:CoreCodes
    {
        [Test,Order(1)]
        public void GetBookingIds()
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
        public void GetBooking()
        {
            test = extent.CreateTest("Get Booking Test");
            Log.Information("Get Booking Test started");
            var request = new RestRequest("booking/1", Method.Get);
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
        public void CreateBooking()
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
    }
}
