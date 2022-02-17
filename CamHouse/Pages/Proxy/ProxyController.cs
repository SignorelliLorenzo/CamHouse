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
        public HttpResponse Get(string url)
        {
            try
            {
                url = url.Replace("%2F","/");
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                foreach (var key in httpWebRequest.Headers.AllKeys)
                {
                    httpWebRequest.Headers.Remove(httpWebRequest.Headers[key]);
                }
                
                foreach (var header in this.Request.Headers)
                {
                    httpWebRequest.Headers.Add(header.Key,);
                }
                httpWebRequest.ContentType = this.Request.ContentType;
                httpWebRequest.Method = this.Request.Method;

            
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponseAsync().Result;
                this.Response.StatusCode = (int)httpResponse.StatusCode;
                this.Response.ContentType = httpResponse.ContentType;
                foreach (var key in httpResponse.Headers.AllKeys)    
                {
                    this.Response.Headers.Add(key,httpResponse.Headers[key]);
                }
                this.Response.Body = httpResponse.GetResponseStream();
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
            }

            return this.Response;

        }
        [HttpGet]
        public string Get()
        {
            return "ciao mondo";
        }
    }
}
