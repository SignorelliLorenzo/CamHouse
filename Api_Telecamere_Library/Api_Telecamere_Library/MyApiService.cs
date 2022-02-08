using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Api_Pcto.Models.DTOS.Requests;
using Api_Pcto.Models.DTOS.Responses;
using Newtonsoft.Json;

namespace Api_Telecamere_Library
{
    public class MyApiService
    {
        
        public UserRegistrationResponse Register(RegistrationRequest user)
        {
            var request = WebRequest.Create("");
            request.Method = "POST";
           
            var json = JsonConvert.SerializeObject(user);
            byte[] bytearray = Encoding.UTF8.GetBytes(json);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytearray.Length;

            var reqStream = request.GetRequestStream();
            reqStream.Write(bytearray, 0, bytearray.Length);

            var response = request.GetResponse();
            var respStream = response.GetResponseStream();

            var reader = new StreamReader(respStream);
            string data = reader.ReadToEnd();

            
        }
    }
}
