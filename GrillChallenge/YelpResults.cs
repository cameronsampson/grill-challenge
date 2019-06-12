using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace YelpResults
{
    public class YelpSearchResults
    {
        private IWebDriver driver;
        private IWebElement elementFirst;
        private IWebElement elementSecond;
        private IWebElement elementThird;
        string resultFirst;
        string resultThird;

        // looking specifically for search results containing "Ted's Montana Grill" 
        // and making sure page is viewing "All Results" and not "Sponsored Results"
        By searchSpecificResult = By.PartialLinkText("Ted’s Montana Grill - "); 
        By searchAllResults = By.XPath("//h3[text() = 'All Results']");

        public YelpSearchResults(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void verifySpecificResults()
        {
            var elements = driver.FindElements(searchSpecificResult);

            // define index of search results through the partial link text 
            elementFirst = elements[0];
            elementSecond = elements[1];
            elementThird = elements[2];

            // Scroll to the third result for Ted's Montana Grill to verify first and third search results
            Actions actions = new Actions(driver);
            actions.MoveToElement(elementThird);
            actions.Perform();

            resultFirst = elementFirst.Text;
            resultThird = elementThird.Text;
        }

        public void verifyAllResults()
        {
            // Check to make sure the right results are being viewed
            Assert.IsTrue(driver.FindElement(searchAllResults).Displayed);

            // Log the first and third search results
            Console.WriteLine("Found first search result: " + resultFirst);
            Console.WriteLine("Found third search result: " + resultThird);

            // Click on the link of the second search result 
            elementSecond.Click();
        }
    }
}