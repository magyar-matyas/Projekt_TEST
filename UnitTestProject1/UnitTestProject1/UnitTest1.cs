using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1 : IDisposable
    {

        private IWebDriver driver;

        public void Dispose()
        {
            driver.Quit();
        }


        public void GameTests()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://127.0.0.1:5500/index.html");
            Thread.Sleep(1000);

        }

        public void PressKeyGlobally(char key)
        {
            Actions action = new Actions(driver);
            action.SendKeys(key.ToString()).Perform();
        }


        [TestMethod]
        public void ReachedTheGoal()
        {
            GameTests();
            foreach (var charakter in "sddssssaaaaaaaawwaaaaaaassssddwwdddssddddssddddddssssssaaaaaaaaaaaaaaaaas")
            {
                PressKeyGlobally(charakter);
            }
            Assert.AreEqual("Gratulálunk, célba értél!", driver.FindElement(By.Id("popup-message")).Text);
        }

        [TestMethod]
        public void HitTheThorn()
        {
            GameTests();
            foreach (var charakter in "sddssssaaaaaawwwwa")
            {
                PressKeyGlobally(charakter);
            }
            Assert.AreEqual("Meghaltál egy tüskében!", driver.FindElement(By.Id("popup-message")).Text);
        }



    }
}
