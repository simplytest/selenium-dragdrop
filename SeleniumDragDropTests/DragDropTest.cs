using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace SeleniumDragDropTest
{
    [TestClass]
    public class DragDropTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            
            IWebDriver driver = new ChromeDriver(Environment.CurrentDirectory);
            try
            {
                driver.Navigate().GoToUrl("https://www.w3schools.com/html/html5_draganddrop.asp");

                IWebElement sourceEl = driver.FindElement(By.Id("drag1"));
                IWebElement targetEl = driver.FindElement(By.Id("div2"));

                SeleniumDragDrop.DragDropHelper dragdrop = new SeleniumDragDrop.DragDropHelper(driver);
                dragdrop.DragAndDrop(sourceEl, targetEl);

                Assert.IsNotNull(targetEl.FindElement(By.Id("drag1")));
            }
            finally
            {
                driver.Quit();
            }
        }
    }
}
