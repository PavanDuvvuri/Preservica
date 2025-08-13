using Microsoft.Playwright;
using Microsoft.Playwright.TestAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRESERVICA.NASA.DEMO.Drivers.Playwright
{
    public class BrowserTest : PlaywrightTest
    {
        public IBrowser Browser { get; private set; } = null!;
        private readonly List<IBrowserContext> _contexts = new();
        public async Task<IBrowserContext> NewContext(BrowserNewContextOptions? options =null)
        {
          var context = await Browser.NewContextAsync(options).ConfigureAwait(false);
            return context;
        }

        public async Task BrowserSetup()
        {
            Browser= await BrowserType.LaunchAsync(PlaywrightSettingsProvider.LaunchOptions).ConfigureAwait(false);
        }
        public async Task BrowserTearDown()
        {
           foreach (var context in _contexts)
            {
                await context.CloseAsync().ConfigureAwait(false);
            }
            _contexts.Clear();
            if (Browser != null)
            {
                await Browser.CloseAsync().ConfigureAwait(false);
                Browser = null!;
            }
        }
    }
}
