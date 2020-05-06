using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace SeleniumDragDrop
{
    public class DragDropHelper
    {
        private IWebDriver _driver;
        private string jsFile;

        public DragDropHelper(IWebDriver driver)
        {
            var assembly = typeof(DragDropHelper).GetTypeInfo().Assembly;
            using (var stream = assembly.GetManifestResourceStream("SeleniumDragDrop.dragAndDrop.js"))
            {
                using (var reader = new StreamReader(stream))
                {
                    jsFile = reader.ReadToEnd();
                }
            }

            this._driver = driver;

            /*
            using (StreamReader sr = new StreamReader("dragAndDrop.js"))
            {
                // Read the stream to a string, and write the string to the console.
                jsFile = sr.ReadToEnd();

            }
            */
        }

        public enum Position
        {
            Top_Left,
            Top,
            Top_Right,
            Left,
            Center,
            Right,
            Bottom_Left,
            Bottom,
            Bottom_Right
        }

        private int getX(Position pos, int width)
        {
            if (Position.Top_Left.Equals(pos) || Position.Left.Equals(pos) || Position.Bottom_Left.Equals(pos))
            {
                return 1;
            }
            else if (Position.Top.Equals(pos) || Position.Center.Equals(pos) || Position.Bottom.Equals(pos))
            {
                return width / 2;
            }
            else if (Position.Top_Right.Equals(pos) || Position.Right.Equals(pos) || Position.Bottom_Right.Equals(pos))
            {
                return width - 1;
            }
            else
            {
                return 0;
            }
        }

        private int getY(Position pos, int height)
        {
            if (Position.Top_Left.Equals(pos) || Position.Top.Equals(pos) || Position.Top_Right.Equals(pos))
            {
                return 1;
            }
            else if (Position.Left.Equals(pos) || Position.Center.Equals(pos) || Position.Right.Equals(pos))
            {
                return height / 2;
            }
            else if (Position.Bottom_Left.Equals(pos) || Position.Bottom.Equals(pos) || Position.Bottom_Right.Equals(pos))
            {
                return height - 1;
            }
            else
            {
                return 0;
            }
        }


        /**
         * Drags and drops a web element from source to target
         *
         * @param driver
         *            The WebDriver to execute on
         * @param dragFrom
         *            The WebElement to drag from
         * @param dragTo
         *            The WebElement to drag to
         * @param dragFromX
         *            The position to click relative to the top-left-corner of the
         *            client
         * @param dragFromY
         *            The position to click relative to the top-left-corner of the
         *            client
         * @param dragToX
         *            The position to release relative to the top-left-corner of the
         *            client
         * @param dragToY
         *            The position to release relative to the top-left-corner of the
         *            client
         */
        private void _dragAndDropJS(IWebElement dragFrom, IWebElement dragTo, int dragFromX, int dragFromY, int dragToX, int dragToY)
        {
            this.jsFile = this.jsFile + "simulateHTML5DragAndDrop(arguments[0], arguments[1], arguments[2], arguments[3], arguments[4], arguments[5]);";
            ((IJavaScriptExecutor)_driver).ExecuteScript(this.jsFile, dragFrom, dragTo, dragFromX, dragFromY, dragToX, dragToY);
        }

        private void _dragAndDropJQuery(IWebElement dragFrom, IWebElement dragTo, int dragFromX, int dragFromY, int dragToX, int dragToY)
        {
            //todo: implement
        }


        /**
         * Drags and drops a web element from source to target
         *
         * @param driver
         *            The WebDriver to execute on
         * @param dragFrom
         *            The WebElement to drag from
         * @param dragTo
         *            The WebElement to drag to
         * @param dragFromPosition
         *            The place to click on the dragFrom
         * @param dragToPosition
         *            The place to release on the dragTo
         */
        public void DragAndDrop(IWebElement dragFrom, IWebElement dragTo, Position dragFromPosition = Position.Center, Position dragToPosition = Position.Center)
        {
            Point fromLocation = dragFrom.Location;
            Point toLocation = dragTo.Location;
            Size fromSize = dragFrom.Size;
            Size toSize = dragTo.Size;

            // Get Client X and Client Y locations
            int dragFromX = fromLocation.X + (fromSize == null ? 0 : getX(dragFromPosition, fromSize.Width));
            int dragFromY = fromLocation.Y + (fromSize == null ? 0 : getY(dragFromPosition, fromSize.Height));
            int dragToX = toLocation.X + (toSize == null ? 0 : getX(dragToPosition, toSize.Width));
            int dragToY = toLocation.Y + (toSize == null ? 0 : getY(dragToPosition, toSize.Height));

            _dragAndDropJS(dragFrom, dragTo, dragFromX, dragFromY, dragToX, dragToY);
        }
    }
}
