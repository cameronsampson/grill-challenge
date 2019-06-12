using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace YelpRestaurant
{
    class YelpRestaurantSchedule
    {
        private IWebDriver driver;
        string openTime;
        string closingTime;

        By restaurantName = By.XPath("//h1[text() = 'Ted’s Montana Grill -']");
        By restaurantLocation = By.XPath("//h1[text() = 'Aurora']");
        By openHours = By.XPath("//li[@class = 'biz-hours iconed-list-item']/*//span[@class = 'nowrap'][1]");
        By closingHours = By.XPath("//li[@class = 'biz-hours iconed-list-item']/*//span[@class = 'nowrap'][2]");

        public YelpRestaurantSchedule(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void getRestaurantHours()
        {
            // Check to make sure the landing page for the second search result 
            // is "Ted's Montana Grill - Aurora"
            Assert.IsTrue(driver.FindElement(restaurantName).Displayed);
            Assert.IsTrue(driver.FindElement(restaurantLocation).Displayed);

            // Read open and closing times
            var elementOpenTime = driver.FindElement(openHours);
            var elementClosingTime = driver.FindElement(closingHours);

            // Log times restaurant opens and closes
            openTime = elementOpenTime.Text;
            closingTime = elementClosingTime.Text;

            Console.WriteLine("This restaurant is open today at: " + openTime + " and closes at: " + closingTime);
        }

        public void verifyIfOpenOrClosed()
        {
            // Convert strings of open and closing time into actual datetime
            DateTime openTimeConvert = DateTime.Parse(openTime, System.Globalization.CultureInfo.CurrentCulture);
            DateTime closeTimeConvert = DateTime.Parse(closingTime, System.Globalization.CultureInfo.CurrentCulture);
            DateTime lunchTime = DateTime.Now.Date.Add(new TimeSpan(15, 00, 0));

            // Check from system time if:
            // 1) it's open or closed
            // 2) appropriate for lunch or dinner, if open
            // 3) check how many hours and minutes until it opens, if closed
            if (openTimeConvert < DateTime.Now && DateTime.Now < lunchTime)
            {
                Console.WriteLine("It is time to go to Ted's for lunch!");
            }
            else if (lunchTime < DateTime.Now && DateTime.Now < closeTimeConvert)
            {
                Console.WriteLine("It is time to go to Ted's for dinner!");
            }
            else
            {
                // Allows the previous day hours remaining to carry over into the next day
                var hours24 = new TimeSpan(24, 0, 0);
                TimeSpan timeDiff = openTimeConvert.Subtract(DateTime.Now);
                timeDiff = (timeDiff.Duration() != timeDiff) ? hours24.Subtract(timeDiff.Duration()) : timeDiff;

                Console.WriteLine("Bummer, Ted's is closed. Ted’s will open in " + (int)timeDiff.TotalHours + " hours and " + timeDiff.Minutes + " minutes. ");
            }
        }
    }
}
