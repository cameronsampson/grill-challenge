using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using TedsChallenge;

namespace YelpLaunch
{
    class YelpLaunchHomepage
    {
        private IWebDriver driver;

        public YelpLaunchHomepage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void LaunchBrowser(string appURL)
        {
            driver.Navigate().GoToUrl(appURL);
        }
    }
}
