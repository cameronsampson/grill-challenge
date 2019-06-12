using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using YelpHomepage;
using YelpResults;
using YelpRestaurant;
using YelpLaunch;

namespace GrillChallenge
{
    [TestClass]
    public class GrillChallengeTest
    {
        static IWebDriver driver;
        string appURL;
        YelpHomepageSearch yelpHomepageSearch;
        YelpSearchResults yelpSearchResults;
        YelpRestaurantSchedule yelpRestaurantSchedule;
        YelpLaunchHomepage yelpLaunchHomepage;

        [TestInitialize]
        public void TestInit()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized"); // maximize browser before launch
            driver = new ChromeDriver(options);
        }

        [TestMethod]
        public void YelpSearchTest()
        {
            yelpLaunchHomepage = new YelpLaunchHomepage(driver);
            yelpHomepageSearch = new YelpHomepageSearch(driver);
            yelpSearchResults = new YelpSearchResults(driver);
            yelpRestaurantSchedule = new YelpRestaurantSchedule(driver);

            // Launch Chrome to go defined url (eg. appURL from YelpLaunchHomepage class)
            yelpLaunchHomepage.LaunchBrowser("http://yelp.com");

            // ---- Page Objects for Homepage, Search Results & Restaurant Schedule ----

            // Set search credentials on Yelp homepage (search item, search location, submit)
            yelpHomepageSearch.setSearchCredentials("Teds Montana Grill", "Denver, CO");

            // Explicit wait to allow page loading to find specific search result
            Thread.Sleep(2000);

            // filter search results that contain "Ted's Montana Grill" within the link
            yelpSearchResults.verifySpecificResults();

            // check to make sure we have scrolled to the right set of results
            yelpSearchResults.verifyAllResults();

            // retrieve and log open and closing hours of Ted's Montana Grill
            yelpRestaurantSchedule.getRestaurantHours();

            // Check if Ted's Montana Grill is open for lunch, dinner or closed at this
            // current time
            yelpRestaurantSchedule.verifyIfOpenOrClosed();

        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Dispose();
        }
    }
}
