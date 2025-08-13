using PRESERVICA.NASA.DEMO.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRESERVICA.NASA.DEMO.Services
{
    public interface IPageService
    {
        SignUpPage SignUpPage { get; }
    }

    public class PageService(
        SignUpPage signUpPage) : IPageService
    {
        public SignUpPage SignUpPage { get; } = signUpPage;
    }
}
