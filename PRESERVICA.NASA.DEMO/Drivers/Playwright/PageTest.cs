using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PRESERVICA.NASA.DEMO.Drivers.Playwright
{
    public class PageTest : ContextTest
    {
        public List<IPage> Pages { get; private set; } = new List<IPage>();
        public IPage Page { get; private set; } = null!;
        public async Task PageSetup()
        {
           var firstPage = await Context.NewPageAsync().ConfigureAwait(false);
            Pages.Add(firstPage);
            Page = firstPage;
        }
    }
}
