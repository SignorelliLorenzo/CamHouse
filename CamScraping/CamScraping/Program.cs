using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.ComponentModel.DataAnnotations;

namespace CamScraping
{
    class Program
    {
        static void Main(string[] args)
        {
            bool done = false;
            MyApiService.url = "https://localhost:5001/";
            if (!done)
            {
                var response = MyApiService.RegisterAsync(new RegistrationRequest() { Email = "admin.admin@gmail.it", Password = "Gennaro" }, "").Result;
                if(response.Success)
                {
                    Console.WriteLine(response.Token);
                    Console.ReadKey();
                    return;
                }
                Console.WriteLine(response.Errors[0]);
                Console.ReadKey();
                return;
            }
            
            ChromeOptions options = new ChromeOptions();
            //options.AddExtension("extension_4_41_0_0.crx");
            //options.AddArgument("load-extension" + Directory.GetCurrentDirectory() + @"\ohahllgiabjaoigichmmfljhkcfikeof");
            options.PageLoadStrategy = PageLoadStrategy.Eager;
            options.AddArgument("--headless");
            using (ChromeDriver driver = new ChromeDriver(options))
            {
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromMilliseconds(5000);
                try
                {
                    driver.Url = $"http://www.insecam.org";
                }
                catch
                {
                }
                int numeroCam = 5;
                int id = 0;
                List<string> list = new List<string>();
                using (var conn = new SQLiteConnection("Data Source=Telecamere.db"))
                {
                    conn.Open();
                    var countries = Wait(By.Id("countriesul"), driver, 10000).FindElements(By.TagName("li"));
                    foreach (var country in countries)
                    {
                        list.AddRange(GetCams(id,country.FindElement(By.TagName("a")).GetAttribute("href"), driver, numeroCam,conn));
                        

                    }
                    foreach (var ip in list)
                    {
                        Console.WriteLine(ip);
                    }
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
        public static List<string> GetCams(int id,string link, ChromeDriver Driver,int num,SQLiteConnection conn)
        {
            
            
            id++;
            List<string> opencams = new List<string>();
            try
            {
                OpenNewTab(link, Driver);


                Driver.SwitchTo().Window(Driver.WindowHandles.Last());

            }
            catch { }
            var z = Driver.FindElements(By.ClassName("thumbnail-item"));
            Console.WriteLine("----------------------------------------------------");
            foreach (var item in z)
            {

                var name = item.FindElement(By.ClassName("thumbnail-item__caption")).Text;
                var ext = item.FindElement(By.ClassName("thumbnail-item__preview")).FindElement(By.TagName("img")).GetAttribute("src");
                var ip = item.FindElement(By.ClassName("thumbnail-item__preview")).FindElement(By.TagName("img")).GetAttribute("src").Replace("COUNTER","");
                Console.WriteLine($"Name : {name}, Ip : {ip}");
                opencams.Add(item.FindElement(By.TagName("img")).GetAttribute("src").Split('/')[2]);
                id++;
            }
            Driver.Close();
            Driver.SwitchTo().Window(Driver.WindowHandles.First());
            Console.WriteLine("----------------------------------------------------");

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
