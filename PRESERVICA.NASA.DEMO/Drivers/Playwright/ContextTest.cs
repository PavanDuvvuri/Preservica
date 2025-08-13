using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRESERVICA.NASA.DEMO.Drivers.Playwright
{
    public class ContextTest : BrowserTest
    {
        public IBrowserContext Context { get; private set; } = null!;

        public virtual BrowserNewContextOptions ContextOPtions()
        {
            return new()
            {
                Locale = "en-US",
                ColorScheme = ColorScheme.Light,
            };
        }

        public async Task ContextSetup()
        {
            Context = await NewContext(ContextOPtions()).ConfigureAwait(false);
        }
    }
}
