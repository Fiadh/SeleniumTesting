using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class FormTest
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        
        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "https://secure.avaaz.org/";
            verificationErrors = new StringBuilder();
        }
        
        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }
        
        [Test]
        public void TheFormTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/en/save_the_bees_us_pet_loc/?tCkhLbb");
            Assert.AreEqual("Avaaz - Before honey bees are extinct", driver.Title);
            Assert.AreEqual("We call on you to immediately ban the use of neonicotinoid pesticides until and unless new independent scientific studies prove they are safe. The catastrophic demise of bee colonies could put our whole food chain in danger. If you act urgently with precaution now, we could save bees from extinction.", driver.FindElement(By.CssSelector("blockquote")).Text);
            Assert.IsTrue(IsElementPresent(By.Id("biogems-petition-formEmail")));
            driver.FindElement(By.Id("biogems-petition-formEmail")).Clear();
            driver.FindElement(By.Id("biogems-petition-formEmail")).SendKeys("fiadhnaitmcd@gmail.com");
            driver.FindElement(By.CssSelector("button.medium.form-submit")).Click();
            Assert.AreEqual("Avaaz - Before honey bees are extinct", driver.Title);
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        
        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}
