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
using Api_Telecamere_Library;
using Api_Telecamere_Library.Models.DTOS.Requests;

namespace CamScraping
{
    class Program
    {
        public static string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjZhYjJiZWI4LTU1MjktNDcyYS05MTc3LWFkYjg5MjA3ZTAxZiIsImVtYWlsIjoidXNlckBleGFtcGxlLmNvbSIsInN1YiI6InVzZXJAZXhhbXBsZS5jb20iLCJqdGkiOiIyZWE1MzIxYS0xZWJhLTQyNjYtOTA0OC1hYjJlNWM5MWEyMTciLCJuYmYiOjE2NDU0NTA3NDYsImV4cCI6MTY0NTQ1NDM0NiwiaWF0IjoxNjQ1NDUwNzQ2fQ.AwbMlRAN8A6lhQDHCbXqZ5i4RPbOOoPzAyYGAHj-tzY";
        static void Main(string[] args)
        {
           
            bool done = false;
            MyApiService.url = "https://localhost:44302/";
            
            
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
                var NewTelecameraModel = new CreaTelecameraRequest();
                NewTelecameraModel.nome = item.FindElement(By.ClassName("thumbnail-item__caption")).Text;
                NewTelecameraModel.link = item.FindElement(By.ClassName("thumbnail-item__preview")).FindElement(By.TagName("img")).GetAttribute("src").Replace("COUNTER","");
                NewTelecameraModel.num_like = 0; NewTelecameraModel.num_salvati = 0;
                Console.WriteLine($"Name : {NewTelecameraModel.nome}, Ip : {NewTelecameraModel.link}");
                var response= MyApiService.PostAsync(NewTelecameraModel, token).Result;
                if(!response.Success)
                {
                    Console.WriteLine(response.Errors[0]);
                    Console.ReadKey();
                    
                }
               
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
