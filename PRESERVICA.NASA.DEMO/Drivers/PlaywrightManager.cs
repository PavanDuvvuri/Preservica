using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRESERVICA.NASA.DEMO.Drivers.Playwright;
using Microsoft.Extensions.Configuration;

namespace PRESERVICA.NASA.DEMO.Drivers
{
    public class PlaywrightManager(IConfiguration config) : PageTest
    {
        private readonly IConfiguration _config = config;

        public async Task Setup()
        {
            float timeout = 5000; // Default timeout in milliseconds
            SetDefaultExpectTimeout(timeout);

            PlaywrightSetup();
            await BrowserSetup();
            await ContextSetup();
            await PageSetup();
        }
    }
}
