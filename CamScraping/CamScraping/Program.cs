using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CamScraping
{
    class Program
    {
        static void Main(string[] args)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddExtension("extension_4_41_0_0.crx");
            options.AddArgument("load-extension" + Directory.GetCurrentDirectory() + @"\ohahllgiabjaoigichmmfljhkcfikeof");
            using (ChromeDriver driver = new ChromeDriver(options))
            {
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromMilliseconds(10000);
                try
                {
                    driver.Url = $"http://www.insecam.org";
                }
                catch
                {
                    return;
                }
                int numeroCam = 5;
                List<string> list = new List<string>();
                var countries = Wait(By.Id("countriesul"), driver, 10000).FindElements(By.TagName("li"));
                foreach (var country in countries)
                {
                     list.AddRange( GetCams(country.FindElement(By.TagName("a")).GetAttribute("href"), driver,numeroCam));
                    
                }
                foreach (var ip in list)
                {
                    Console.WriteLine(ip);
                }
            }

            Console.ReadKey();



        }
        public static void OpenNewTab(string url, ChromeDriver Driver)
        {
            var windowHandles = Driver.WindowHandles;
            IJavaScriptExecutor js = Driver;
            js.ExecuteScript(string.Format("window.open('{0}', '_blank');", url));
            

        }
        public static List<string> GetCams(string link, ChromeDriver Driver,int num)
        {
            int x = 0;
            List<string> opencams = new List<string>();
            OpenNewTab(link, Driver);
            var z = Driver.FindElements(By.ClassName("thumbnail-item__preview"));
            foreach (var item in z)
            {
                opencams.Add(item.FindElement(By.TagName("img")).GetAttribute("src").Split('/')[2]);
                x++;
            }
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
            Driver.Close();
            Driver.SwitchTo().Window(Driver.WindowHandles.First());

            return opencams;

        }
        static public IWebElement Wait(By by, ChromeDriver Driver, int Timeout)
        {
            int x = 0;
            string message = "";
            while (x < Timeout)
            {
                try
                {
                    return Driver.FindElement(by);
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    Thread.Sleep(1);

                    x++;
                }
            }
            throw new Exception(message);
        }
    }
}
