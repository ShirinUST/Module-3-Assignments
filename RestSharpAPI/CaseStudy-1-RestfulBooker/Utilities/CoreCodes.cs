using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaseStudy_1_RestfulBooker.Models;
using Newtonsoft.Json;

namespace CaseStudy_1_RestfulBooker.Utilities
{
    internal class CoreCodes
    {
        protected RestClient client;
        protected ExtentReports extent;
        protected ExtentTest? test;
        ExtentSparkReporter sparkReporter;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            string currdir = Directory.GetParent(@"../../../").FullName;

            extent = new ExtentReports();
            sparkReporter = new ExtentSparkReporter(currdir + "/Reports/ExtentReports/extent-report-" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".html");
            extent.AttachReporter(sparkReporter);
            string? logfilepath = currdir + "/Logs/log-" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            Log.Logger = new LoggerConfiguration().WriteTo.File(logfilepath, rollingInterval: RollingInterval.Day).CreateLogger();
        }
        protected Authentication UpdateToken()
        {
            var request = new RestRequest("auth", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                username = "admin",
                password = "password123"
            });
            var response = client.Execute(request);
            var token = JsonConvert.DeserializeObject<Authentication>(response.Content);
            return token;
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            extent.Flush();
            Log.CloseAndFlush();
        }
        [SetUp]
        public void Setup()
        {
            client = new RestClient("https://restful-booker.herokuapp.com/");
        }
    }
}
