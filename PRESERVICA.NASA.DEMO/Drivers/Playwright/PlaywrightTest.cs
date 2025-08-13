using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.TestAdapter;

namespace PRESERVICA.NASA.DEMO.Drivers.Playwright
{
    public class PlaywrightTest
    {
        public string BrowserName { get; internal set; } = null!;

        public static readonly IPlaywright _playwrightTask = Microsoft.Playwright.Playwright.CreateAsync().GetAwaiter().GetResult();

        public IPlaywright Playwright { get; private set; } = null!;
        public IBrowserType BrowserType { get; private set; } = null!;

        public void PlaywrightSetup()
        {
            Playwright = _playwrightTask;
            BrowserName = PlaywrightSettingsProvider.BrowserName;
            BrowserType = Playwright[BrowserName];  
        }

        public void SetDefaultExpectTimeout(float timeout) => Assertions.SetDefaultExpectTimeout(timeout);

        public ILocatorAssertions Expect(ILocator locator) => Assertions.Expect(locator);   

        public IPageAssertions Expect(IPage page) =>Assertions.Expect(page);
      
        public IAPIResponseAssertions Expect(IAPIResponse response) => Assertions.Expect(response);


    }
}
