using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PRESERVICA.NASA.DEMO.Extensions;
using PRESERVICA.NASA.DEMO.Support;
using Reqnroll;
using RestSharp;
using System.Net;


namespace PRESERVICA.NASA.DEMO.StepDefinitions
{
    [Binding]
    public class NASAAPISteps
    {
        private readonly IConfiguration _config;
        private readonly ScenarioContext _context;

        public NASAAPISteps(IConfiguration config, ScenarioContext scenarioContext)
        {
            _config = config;
            _context = scenarioContext;
        }

        [Given("I have created a Coronal Mass Ejection Get Request for (.*)")]
        public void GivenIHaveCreatedACoronalMassEjectionGetRequestForValidRequest(string testScenario)
        {
            var startTime = DateTime.UtcNow.AddDays(-30).ToString("yyyy-MM-dd");
            _context.Add("startTime", startTime);
            var endTime = DateTime.UtcNow.ToString("yyyy-MM-dd");
            var apiKey = _config.APIKey();
            switch (testScenario)
            {
                case "ValidRequest":
                    break;
                case "InvalidDateFormat":
                    startTime = " "; // Invalid month
                    break;
                case "InvalidAPIKey":
                    apiKey = "INVALID_API_KEY"; // Invalid API key
                    break;
                case "IncorrectDate":
                    startTime = DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-dd"); // Future date
                    endTime = DateTime.UtcNow.AddDays(2).ToString("yyyy-MM-dd"); // Future date
                    break;
                default:
                    throw new ArgumentException($"Unknown test scenario: {testScenario}");
            }

            var request = Helper.CreateRestRequest(Constants.Constants.CoronalMassEjection+"?startDate=" + startTime + "&endDate=" + endTime + "&api_key=" + apiKey, Method.Get, alwaysMultipartFormData: false);
            _context.Add("request", request);
        }

        [Given("I have created a Solar Flare Ejection Get Request for (.*)")]
        public void GivenIHaveCreatedASolarFlareEjectionGetRequestForValidRequest(string testScenario)
        {
            var startTime = DateTime.UtcNow.AddDays(-30).ToString("yyyy-MM-dd");
            _context.Add("startTime", startTime);
            var endTime = DateTime.UtcNow.ToString("yyyy-MM-dd");
            var apiKey = _config.APIKey();
            switch (testScenario)
            {
                case "ValidRequest":
                    break;
                case "InvalidDateFormat":
                    startTime = " "; // Empty start date
                    break;
                case "InvalidAPIKey":
                    apiKey = "INVALID_API_KEY"; // Invalid API key
                    break;
                case "IncorrectDate":
                    startTime = DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-dd"); // Future date
                    endTime = DateTime.UtcNow.AddDays(2).ToString("yyyy-MM-dd"); // Future date
                    break;
                default:
                    throw new ArgumentException($"Unknown test scenario: {testScenario}");
            }

            var request = Helper.CreateRestRequest(Constants.Constants.SolarFlare + "?startDate=" + startTime + "&endDate=" + endTime + "&api_key=" + apiKey, Method.Get, alwaysMultipartFormData: false);
            _context.Add("request", request);
        }

        [When("the request is sent to the NASA API")]
        public void WhenTheRequestIsSentToTheNASAAPI()
        {
            var endpoint = _config.APIHost();
            var request = _context.Get<RestRequest>("request");
            var client =  Helper.CreateRestClient(endpoint);

            try
            {
                var response = Helper.ExecuteAsync(client, request).GetAwaiter().GetResult();
                _context.Add("response", response);
            }
            catch (Exception ex)
            {
                _context.Add("error", ex.Message);
            }
        }

        [Then("no exceptions should be thrown")]
        public void ThenNoExceptionsShouldBeThrown()
        {
            Assert.That(_context.TryGetValue("response-exception", out HttpRequestException _), Is.False);
        }

        [Then("the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(string statusCode)
        {
           var response = _context.Get<RestResponse>("response");
          
            HttpStatusCode expectedStatusCode = statusCode switch
            {
                "OK" => HttpStatusCode.OK,
                "BadRequest" => HttpStatusCode.BadRequest,
                "Forbidden" => HttpStatusCode.Forbidden,
                "InternalServerError" => HttpStatusCode.InternalServerError,
                _ => throw new ArgumentException($"Unknown status code: {statusCode}")
            };

            Assert.That(response.StatusCode, Is.EqualTo(expectedStatusCode),
                $"Status code not matched, expecting status code {expectedStatusCode} but was {response.StatusCode}");
        }

        [Then("the response should contain (.*) with (.*)")]
        public void ThenTheResponseShouldContainWith(string responseData, string message)
        {
            if(responseData == "Success")
            {
                var response = _context.Get<RestResponse>("response");
                if (string.IsNullOrWhiteSpace(response.Content))
                {
                    Assert.Fail("Response content is null or empty.");
                }
                var array = JArray.Parse(response.Content!);
                string? id = array[0]?[message]?.ToString();

                Assert.That(!string.IsNullOrEmpty(id) && (id.Contains("CME") || id.Contains("FLR")), "ID should not be null or empty.");
            }
            else if (responseData == "Failed")
            {
                var response = _context.Get<RestResponse>("response");
                if (string.IsNullOrWhiteSpace(response.Content))
                {
                    Assert.Fail("Response content is null or empty.");
                }
                var jObject = JObject.Parse(response.Content!);
                string? errorMessage = jObject["error"]?["code"]?.ToString();
                Assert.That(errorMessage, Is.EqualTo(message), $"Expected error message '{message}' but got '{errorMessage}'");
            }
            else if(responseData == "")
            {
                var response = _context.Get<RestResponse>("response");
                Assert.That(response.Content, Is.EqualTo("[]"), "Response content should be null or empty.");
            }
        }

    }
}
