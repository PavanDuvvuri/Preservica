using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRESERVICA.NASA.DEMO.Drivers;
using Reqnroll;


namespace PRESERVICA.NASA.DEMO.Support
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly PlaywrightManager _playwrightManager;
        public Hooks(ScenarioContext scenarioContext, PlaywrightManager playwrightManager)
        {
            _scenarioContext = scenarioContext;
            _playwrightManager = playwrightManager;
        }
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            // This method runs once before all tests in the test run.
            Console.WriteLine("Before Test Run: Initializing resources...");
        }
        [BeforeScenario]
        public static void BeforeScenario()
        {
            // This method runs before each scenario.
            Console.WriteLine("Before Scenario: Setting up the environment...");
        }
        [AfterScenario]
        public async Task AfterScenario()
        {
            // This method runs after each scenario.
            Console.WriteLine("After Scenario: Cleaning up the environment...");
            await _playwrightManager.BrowserTearDown().ConfigureAwait(false);
        }
    }
}
