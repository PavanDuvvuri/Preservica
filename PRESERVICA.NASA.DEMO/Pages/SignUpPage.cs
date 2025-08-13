using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRESERVICA.NASA.DEMO.Drivers;

namespace PRESERVICA.NASA.DEMO.Pages
{
    public class SignUpPage(PlaywrightManager playwrightManager)
    {
        private readonly PlaywrightManager _playwrightManager = playwrightManager;

        public ILocator SignUpButton => _playwrightManager.Page.GetByRole(AriaRole.Button, new() { Name = "Sign up" });
        public ILocator FirstnameField => _playwrightManager.Page.GetByRole(AriaRole.Textbox, new() { Name = "First Name *" });
        public ILocator FirstnameWarning => _playwrightManager.Page.Locator("#user_first_name_feedback");
        public ILocator LastnameField => _playwrightManager.Page.GetByRole(AriaRole.Textbox, new() { Name = "Last Name *" });
        public ILocator LastnameWarning => _playwrightManager.Page.Locator("#user_last_name_feedback");
        public ILocator EmailField => _playwrightManager.Page.GetByRole(AriaRole.Textbox, new() { Name = "Email *" });
        public ILocator HowWillYouUseAPIs => _playwrightManager.Page.GetByRole(AriaRole.Textbox, new() { Name = "How will you use the APIs? (" });
        public ILocator Captcha => _playwrightManager.Page.Locator("//iframe[@title='recaptcha challenge expires in two minutes']");
        public ILocator Confirmation(string email) => _playwrightManager.Page.GetByText("Your API key for "+ email +" has");
        public async Task NavigateToSignUpPage(string url)
        {
            await _playwrightManager.Setup().ConfigureAwait(false);
            await _playwrightManager.Page.GotoAsync(url);
        }
        public async Task FillSignUpForm(string username, string password, string email)
        {
            await FirstnameField.FillAsync(username);
            await LastnameField.FillAsync(password);
            await EmailField.FillAsync(email);
        }
        public async Task FieldInputValidationAsync(string fieldName, string errorMessage)
        {
            switch (fieldName.ToLower())
            {
                case "username":
                    await VerifyTextToBeVisibleAsync(errorMessage);
                    break;
                case "password":
                    await VerifyTextToBeVisibleAsync(errorMessage);
                    break;
                case "email":
                    await VerifyTextToBeVisibleAsync(errorMessage);
                    break;
                default:
                    throw new ArgumentException("Invalid field name");
            }
            var errorLocator = _playwrightManager.Page.Locator($"#error-{fieldName}");
            await errorLocator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            var actualErrorMessage = await errorLocator.InnerTextAsync();
            if (actualErrorMessage != errorMessage)
            {
                throw new Exception($"Expected error message '{errorMessage}' but got '{actualErrorMessage}'");
            }
        }
        public async Task SubmitSignUpForm()
        {
            await SignUpButton.ClickAsync();
        }
        public async Task VerifyTextToBeVisibleAsync(string expectedText, bool exact = true, float timeout = 5000)
        {
            await _playwrightManager.Expect(_playwrightManager.Page.GetByText(expectedText, new() { Exact = exact })).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions() { Timeout = timeout });
        }
        public async Task VerifyLocatorToBeVisibleAsync(ILocator locator, float timeout = 5000)
        {
            await _playwrightManager.Expect(locator).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions() { Timeout = timeout });
        }
        public async Task ExpectCaptchaAsync()
        {
            if(await Confirmation("testuser@example.com").IsVisibleAsync())
            {
                return; // If confirmation message is visible, no captcha is expected
            }
            else await _playwrightManager.Expect(Captcha).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = 10000 });
        }
    }
}
