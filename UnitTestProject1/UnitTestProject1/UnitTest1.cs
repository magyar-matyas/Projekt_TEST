using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
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
            var Timer = driver.FindElement(By.Id("timer")).Text;
            var minutes = int.Parse(Timer.Split(':')[1]);
            var seconds = int.Parse(Timer.Split(':')[2]);
            var time = minutes * 60 + seconds;
            Assert.IsTrue(time > 0);
        }


        [TestMethod]
        public void TimerStopsOnGameOver()
        {
            GameTests();
            Thread.Sleep(5000);
            PressKeyGlobally('w');
            PressKeyGlobally('w');
            PressKeyGlobally('w');
            var Timer = driver.FindElement(By.Id("timer")).Text;
            Thread.Sleep(1000);
            var Timer2 = driver.FindElement(By.Id("timer")).Text;
            Assert.AreEqual(Timer, Timer2);
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
            PressKeyGlobally('w');
            Thread.Sleep(100);
            var initialSytle = driver.FindElement(By.XPath("//*[@id=\"shot-effect\"]")).GetCssValue("display");
            Assert.AreEqual("block", initialSytle);
        }



    }
}

