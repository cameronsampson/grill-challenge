using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;

namespace SeleniumWebDriverWrapper
{
	public static class Extensions
	{
        private IWebDriver driver = new ChromeDriver();

        public static IWebElement ScrollIntoView(this IWebElement element)
        {
            IWebDriver currentDriver = ((IWrapsDriver)element).WrappedDriver;
            currentDriver.ExecuteJavascript("arguments[0].scrollIntoView(false)", element);
            return element;
        }
    }
}
