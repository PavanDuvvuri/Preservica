using Reqnroll;
using PRESERVICA.NASA.DEMO.Extensions;
using Microsoft.Playwright;
using Microsoft.Extensions.Configuration;
using PRESERVICA.NASA.DEMO.Pages;
using PRESERVICA.NASA.DEMO.Services;
using PRESERVICA.NASA.DEMO.Models;

namespace PRESERVICA.NASA.DEMO.StepDefinitions
{
    [Binding]
    public class NASASignUpSteps
    {
        private readonly IPageService _pageService;
        private readonly IConfiguration _config;
        private readonly ScenarioContext _scenarioContext;


        public NASASignUpSteps(
            IPageService pageService,
            IConfiguration config,
            ScenarioContext scenarioContext)
        {
            _pageService = pageService;
            _config = config;
            _scenarioContext = scenarioContext;
        }

        [Given("I am on the NASA UI sign up page")]
        public async Task GivenIAmOnTheNASAUISignUpPage()
        {
            // Navigate to the sign up page using the base URL from the configuration
            await _pageService.SignUpPage.NavigateToSignUpPage(_config.BaseUrl());
        }

        [When("I fill in the sign up form with valid First Name {string}, Last Name {string}, Email {string}")]
        public async Task WhenIFillInTheSignUpFormWithValidFirstNameLastNameEmail(string firstName, string lastName, string email)
        {
           await _pageService.SignUpPage.FillSignUpForm(firstName, lastName, email);
        }

        [When("I submit the sign up form")]
        public async Task WhenISubmitTheSignUpForm()
        {
            await _pageService.SignUpPage.SubmitSignUpForm();
        }

        [Then("I should see a captcha challenge")]
        public async Task ThenIShouldSeeACaptchaChallenge()
        {
            await _pageService.SignUpPage.ExpectCaptchaAsync();
        }

        [When("I fill in the sign up form with invalid credentials")]
        public async Task WhenIFillInTheSignUpFormWithInvalidCredentials()
        {
            await _pageService.SignUpPage.FillSignUpForm("", "", "");
        }

        [Then("I should see an error message indicating the issues with my submission")]
        public async Task ThenIShouldSeeAnErrorMessageIndicatingTheIssuesWithMySubmission(DataTable dataTable)
        {
            var validationTests = dataTable.CreateSet<InputValidation>();
            foreach (var validationTest in validationTests)
            {
                var fieldName = validationTest.FieldName.ToLower();
                var errorMessage = validationTest.ErrorMessage;
  
                switch (fieldName.ToLower())
                {
                    case "first name":
                        await _pageService.SignUpPage.VerifyLocatorToBeVisibleAsync(_pageService.SignUpPage.FirstnameWarning);
                        break;
                    case "last name":
                        await _pageService.SignUpPage.VerifyLocatorToBeVisibleAsync(_pageService.SignUpPage.LastnameWarning);
                        break;
                    case "email":
                        await _pageService.SignUpPage.VerifyTextToBeVisibleAsync(errorMessage);
                        break;
                    default:
                        throw new ArgumentException($"Unknown field: {fieldName}");
                }
            }
        }
    }
}
