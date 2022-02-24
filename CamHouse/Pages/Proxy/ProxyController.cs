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
    [Route("api/proxy")]
    [ApiController]
    public class ProxyController : ControllerBase
    {
        // GET api/<ProxyController>/"url"
        [HttpGet("url={url}")]
        public async Task<IActionResult> Get(string url)
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

                return File(System.IO.File.ReadAllBytes("~/Img/errorcam.png"), "image/png");

            }

            return File(bytes, "image/jpeg");

        }
        [HttpPost("post")]
        public async Task<string> Post(Byte[] bytes)
        {

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:5000/");
            //WebClient a = new WebClient();
            //a.Headers.Add("x-access-token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJwdWJsaWNfaWQiOiIyZTc0Y2JhMy0xMjFmLTQ4NTUtYmU3Mi01NmY2ZmNiZTJlMDQiLCJleHAiOjE2NzcyMjg1NDN9.eZ-bG6DO8OPLskX9wCS6a2fft1FDzSSiSmGSwqh4yr4");
            //a.pos("http://127.0.0.1:5000/", bytes)
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Headers.Add("x-access-token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJwdWJsaWNfaWQiOiIyZTc0Y2JhMy0xMjFmLTQ4NTUtYmU3Mi01NmY2ZmNiZTJlMDQiLCJleHAiOjE2NzcyMjg1NDN9.eZ-bG6DO8OPLskX9wCS6a2fft1FDzSSiSmGSwqh4yr4");
            httpWebRequest.ContentLength = bytes.Length;
            using (Stream dataStream = httpWebRequest.GetRequestStream())
            {
                dataStream.Write(bytes, 0, bytes.Length);

            }
            using (WebResponse response = httpWebRequest.GetResponse())
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(responseStream))
            {
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.

                string responseFromServer = reader.ReadToEnd();
                // Display the content.


            }
            //try
            //{
            //    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            //    {
            //        string json = JsonConvert.SerializeObject(request);

            //        streamWriter.Write(json);
            //    }

            //    var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
            //    CreaTelecameraResponse response;
            //    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            //    {
            //        var result = streamReader.ReadToEnd();
            //        response = JsonConvert.DeserializeObject<CreaTelecameraResponse>(result);
            //    }
            //    return response;
            //}
            //catch (Exception ex)
            //{
            //    return "";
            //}

            return "";

        }

    }
}
