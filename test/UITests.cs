using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace PortTime.Tests
{
    [Trait("Category", "Skip")]
    public class UITests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly string _baseUrl;

        public UITests()
        {
            _driver = new ChromeDriver();
            _baseUrl = "https://localhost:7143/";
        }

        [Fact]
        public void SearchForCity_ReturnsSearchResults()
        {
            _driver.Navigate().GoToUrl(_baseUrl);
            var searchInput = _driver.FindElement(By.Id("search-input"));
            searchInput.SendKeys("New York");
            var searchButton = _driver.FindElement(By.Id("search-button"));
            searchButton.Click();

            var cityHeader = _driver.FindElement(By.Id("city"));
            Assert.True(cityHeader.Displayed);

        }


        public void Dispose()
        {
            _driver.Quit();
        }
    }
}

