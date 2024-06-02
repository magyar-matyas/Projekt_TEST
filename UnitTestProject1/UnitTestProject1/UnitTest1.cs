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
        public void InitialLifePoints()
        {
            GameTests();
            var initialLifeValue = (long)((IJavaScriptExecutor)driver).ExecuteScript("return lifeValue;");
            Assert.AreEqual(3, initialLifeValue);
        }
        [TestMethod]
        public void CollisionWithObstacle()
        {
            GameTests();
            PressKeyGlobally('w');
            Thread.Sleep(500);
            var lifeAfterCollision = (long)((IJavaScriptExecutor)driver).ExecuteScript("return lifeValue;");
            Assert.AreEqual(2, lifeAfterCollision);
        }

        [TestMethod]
        public void GameOverAfterThreeCollisions()
        {
            GameTests();
            PressKeyGlobally('w');
            Thread.Sleep(500);
            PressKeyGlobally('w');
            Thread.Sleep(500);
            PressKeyGlobally('w');
            Thread.Sleep(500);
            PressKeyGlobally('w');
            Thread.Sleep(500);

            var lifeAfterThreeCollisions = (long)((IJavaScriptExecutor)driver).ExecuteScript("return lifeValue;");
            Assert.AreEqual(0, lifeAfterThreeCollisions);

            var gameOverMessage = driver.FindElement(By.Id("popup-message")).Text;
            Assert.AreEqual("Összetört a kocsid!", gameOverMessage);
        }

        [TestMethod]
        public void TimerStartsOnPlayButtonClick()
        {
            GameTests();
            driver.FindElement(By.Id("play-button")).Click();
            Thread.Sleep(1000);

            var elapsedTime = (long)((IJavaScriptExecutor)driver).ExecuteScript("return elapsedTime;");
            Assert.IsTrue(elapsedTime > 0);
        }
        [TestMethod]
        public void UpdateRecordTime()
        {
            GameTests();
            ((IJavaScriptExecutor)driver).ExecuteScript("recordTime = 1000;");
            foreach (var charakter in "sddssssaaaaaaaawwaaaaaaassssddwwdddssddddssddddddssssssaaaaaaaaaaaaaaaaas")
            {
                PressKeyGlobally(charakter);
            }
            var newRecordTime = int.Parse(System.IO.File.ReadAllText("record.txt"));
            Assert.IsTrue(newRecordTime < 1000);
        }

        [TestMethod]
        public void CarStaysStillWithoutKeys()
        {
            GameTests();
            var initialPosition = (string)((IJavaScriptExecutor)driver).ExecuteScript("return JSON.stringify(KocsiMegtalal());");
            Thread.Sleep(2000);
            var newPosition = (string)((IJavaScriptExecutor)driver).ExecuteScript("return JSON.stringify(KocsiMegtalal());");
            Assert.AreEqual(initialPosition, newPosition);
        }

        [TestMethod]
        public void BackgroundColorChange()
        {
            GameTests();
            driver.FindElement(By.Id("menu-button")).Click();
            driver.FindElement(By.Id("background-color-selector")).SendKeys("blue");
            driver.FindElement(By.Id("apply-button")).Click();
            var backgroundColor = (string)((IJavaScriptExecutor)driver).ExecuteScript("return document.body.style.backgroundColor;");
            Assert.AreEqual("blue", backgroundColor);
        }
    }
}

