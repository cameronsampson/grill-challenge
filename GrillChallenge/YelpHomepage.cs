using System;
using OpenQA.Selenium;

namespace YelpHomepage
{
    public class YelpHomepageSearch
    {
        private IWebDriver driver;

        By searchItemField = By.Id("find_desc");
        By searchLocationField = By.Id("dropperText_Mast");
        By submitButton = By.Id("header-search-submit");

        public YelpHomepageSearch(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Set search item in search item field
        public void setSearchCredentials(string searchItemValue, string searchLocationValue)
        {
            driver.FindElement(searchItemField).SendKeys(searchItemValue);
            driver.FindElement(searchLocationField).Clear();
            driver.FindElement(searchLocationField).SendKeys(searchLocationValue);
            driver.FindElement(submitButton).Click();
        }

    }
}
