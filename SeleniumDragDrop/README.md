# selenium-dragdrop
.Net Standard implementation for proper Selenium Drag and Drop Functionality  

### Documentation

This project provides a .NET Standard library for automation of Selenium drag &d rop operations based on javascript routines, because the default Selenium Actions Drag&Drop won't unfortunately work for the msot HTML 5 sited.

### Usage example:

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

Try it out and enjoy, your SimplyTest team.


### License

MIT
