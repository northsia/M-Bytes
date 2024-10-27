using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace M_Bytes.Core.WebApi
{
    internal class Requste
    {
        public static string GET(string url)
        {
            var options = new ChromeOptions();
            options.AddArgument("--incognito"); 
            options.AddArgument("--headless"); 

            using (IWebDriver driver = new ChromeDriver(options))
            {
                driver.Navigate().GoToUrl(url);

                Thread.Sleep(2000);

                string pageContent = driver.PageSource;

                driver.Quit();

                return pageContent;
            }
        }
    }
}
