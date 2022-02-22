using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CamHouse.Pages.Proxy
{
    [Route("api")]
    [ApiController]
    public class ProxyController : ControllerBase
    {
        // GET api/<ProxyController>/"url"
        [HttpGet("url={url}")]
        public IActionResult Get(string url)
        {
            Byte[] bytes=null;
            try
            {
                url = url.Replace("%2F", "/").Replace("%3F", "?");
                WebClient client = new WebClient();
                
                Stream stream = client.OpenRead(url);
                MemoryStream ms = new MemoryStream();
                stream.CopyTo(ms);
                stream.Flush();
                stream.Close();
                bytes = ms.ToArray();
                
                
                //var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                //var httpResponse = (HttpWebResponse)httpWebRequest.GetResponseAsync().Result;
                //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                //{
                //    using (BinaryReader reader = new BinaryReader(httpResponse.GetResponseStream()))
                //    {
                //         bytes = reader.ReadBytes(1 * 1024 * 1024 * 10);

                //    }
                //}

            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
            }

            return File(bytes, "image/jpeg");

        }
        [HttpGet]
        public string Get()
        {
            return "ciao mondo";
        }
    }
}
