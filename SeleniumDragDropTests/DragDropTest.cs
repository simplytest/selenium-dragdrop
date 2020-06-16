using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;

namespace SeleniumDragDropTest
{
    [TestClass]
    public class DragDropTest
    {
        /// This new style HTML 5 Drag & Drop implementation does not work with native Selenium Actions, but DragDropHelper version works.
        [TestMethod]
        public void TestDragDrop()
        {

            IWebDriver driver = null;
            try
            {
                driver = new ChromeDriver(Environment.CurrentDirectory);
                driver.Navigate().GoToUrl("https://www.w3schools.com/html/html5_draganddrop.asp");

                IWebElement sourceEl = driver.FindElement(By.Id("drag1"));
                IWebElement targetEl = driver.FindElement(By.Id("div2"));

                SeleniumDragDrop.DragDropHelper dragdrop = new SeleniumDragDrop.DragDropHelper(driver);
                dragdrop.DragAndDrop(sourceEl, targetEl);

                Assert.IsNotNull(targetEl.FindElement(By.Id("drag1")));
            }
            finally
            {
                if (driver != null)
                    driver.Quit();
            }
        }


        /// This old style JQuery Drag & Drop implementation works even with native Selenium Actions
        [TestMethod]
        public void SeleniumDragDropNonHTML5()
        {

            IWebDriver driver = null;
            try
            {
                driver = new ChromeDriver(Environment.CurrentDirectory);
                driver.Navigate().GoToUrl("http://jqueryui.com/droppable");
                driver.SwitchTo().Frame(0);

                IWebElement sourceEl = driver.FindElement(By.Id("draggable"));
                IWebElement targetEl = driver.FindElement(By.Id("droppable"));

                Actions action = new Actions(driver);
                action
                    .ClickAndHold(sourceEl)
                    .MoveToElement(targetEl)
                    .Release()
                    .Build().Perform();

                Assert.IsNotNull(targetEl.FindElement(By.XPath("//p[text() = 'Dropped!']")));
            }
            finally
            {
                if (driver != null)
                    driver.Quit();
            }
        }


    }
}
