using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProQuestScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            ChromeDriver myBrowser = new ChromeDriver();
            //****************************************************************************
            myBrowser.Navigate().GoToUrl("http://www.hwpl.org/databases/");
            myBrowser.FindElementByLinkText("ABI/INFORM Complete").Click();

            myBrowser.FindElementById("barcode").SendKeys("21327001023680");
            myBrowser.FindElementById("barcodeSubmit").Click();
            var searchPageUrl = myBrowser.Url;
            myBrowser.FindElementById("searchTerm").SendKeys("(907) 225-1494");
            myBrowser.FindElementById("expandedSearch").Click();
           var items= myBrowser.FindElementByClassName("resultItems").FindElements(By.TagName("li"));
           bool found = false;
           foreach (var item in items)
           {
               var sourceItems=item.FindElement(By.ClassName("item")).FindElements(By.ClassName("titleAuthorETC"));
               foreach (var scrItem in sourceItems)
               {
                   if (scrItem.Text.Contains("Experian Commercial Risk Database"))
                   {
                       found = true;
                       break;
                   }
               }
               if (found)
               {
                   item.FindElement(By.TagName("h3")).FindElement(By.TagName("a")).Click();
                   break;
               }
           }
           if (!found) myBrowser.Navigate().GoToUrl(searchPageUrl);
           while (myBrowser.FindElementsById("emailLink").Count == 0) Thread.Sleep(500);
           Thread.Sleep(1000);
           myBrowser.FindElementById("emailLink").Click();
           while (myBrowser.FindElementsByClassName("emailBccTextField").Count == 0) Thread.Sleep(500);
           Thread.Sleep(1000);
           myBrowser.FindElementByClassName("emailBccTextField").FindElement(By.TagName("input")).SendKeys("danyalxiddiqui@gmail.com");
           myBrowser.FindElementByClassName("clsEmlUsrNm").SendKeys("Danyal");
            myBrowser.FindElementByLinkText("Continue").Click();
            Thread.Sleep(1000);
            myBrowser.Navigate().GoToUrl(searchPageUrl);

        }
        
    }
}
