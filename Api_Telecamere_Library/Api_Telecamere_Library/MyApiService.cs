using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Api_Pcto;
using Api_Pcto.Models.DTOS.Requests;
using Api_Pcto.Models.DTOS.Responses;
using Newtonsoft.Json;

namespace Api_Telecamere_Library
{
    public class MyApiService
    {
        private readonly static string url = "https://localhost:44302/api/";
        public async Task<UserRegistrationResponse> RegisterAsync(RegistrationRequest user, string token)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var myContent = JsonConvert.SerializeObject(user);

            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);


            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            var result = await client.PostAsync(url + "Authentication/Register", byteContent);

            string resultContent = result.Content.ReadAsStringAsync().Result;
        }

        public static async Task<List<Telecamera_Data>> GetAllAsync(string token)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var result = await client.GetStringAsync(url + "Telecamere");

                List<Telecamera_Data> lista = JsonConvert.DeserializeObject<List<Telecamera_Data>>(result);

                return lista;
            }
            catch
            {
                return null;
            }
        }

        public static async Task<GetTelecameraPerIdResponse> GetByIdAsync(int id, string token)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {
                var result = await client.GetStringAsync(url + $"Telecamere/id {id}");

                GetTelecameraPerIdResponse response = JsonConvert.DeserializeObject<GetTelecameraPerIdResponse>(result);

                return response;
            }
            catch (Exception ex)
            {
                return new GetTelecameraPerIdResponse()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        ex.Message
                    }
                };
            }
        }

        public static async Task<GetTelecameraPerNomeResponse> GetByNameAsync(string name, string token)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {
                var result = await client.GetStringAsync(url + $"Telecamere/nome {name}");

                GetTelecameraPerNomeResponse response = JsonConvert.DeserializeObject<GetTelecameraPerNomeResponse>(result);
                return response;
            }
            catch (Exception ex)
            {
                return new GetTelecameraPerNomeResponse()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        ex.Message
                    }
                };
            }
        }

        public static async Task<GetTelecameraRandomResponse> GetRandomAsync(string token)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {
                var result = await client.GetStringAsync(url + $"Telecamere/random");

                GetTelecameraRandomResponse response = JsonConvert.DeserializeObject<GetTelecameraRandomResponse>(result);
                return response;
            }
            catch (Exception ex)
            {
                return new GetTelecameraRandomResponse()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        ex.Message
                    }
                };
            }
        }

        public static async Task<CreaTelecameraResponse> PostAsync(CreaTelecameraRequest request, string token)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "Telecamere");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Authorization", "Bearer " + token);
            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(request);

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
                CreaTelecameraResponse response;
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    response = JsonConvert.DeserializeObject<CreaTelecameraResponse>(result);
                }
                return response;
            }
            catch (Exception ex)
            {
                return new CreaTelecameraResponse()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        ex.Message
                    }
                };
            }
        }

        public static async Task<ModificaTelecameraResponse> PutAsync(ModificaTelecameraRequest request, string token)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "Telecamere");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";
            httpWebRequest.Headers.Add("Authorization", "Bearer " + token);
            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(request);

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
                ModificaTelecameraResponse response;
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    response = JsonConvert.DeserializeObject<ModificaTelecameraResponse>(result);
                }
                return response;
            }
            catch (Exception ex)
            {
                return new ModificaTelecameraResponse()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        ex.Message
                    }
                };
            }
        }

        public static async Task<EliminaTelecameraResponse> DeleteAsync(int id, string token)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + $"Telecamere/{id}");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "DELETE";
            httpWebRequest.Headers.Add("Authorization", "Bearer " + token);
            try
            {
                var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
                EliminaTelecameraResponse response;
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    response = JsonConvert.DeserializeObject<EliminaTelecameraResponse>(result);
                }
                return response;
            }
            catch (Exception ex)
            {
                return new EliminaTelecameraResponse()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        ex.Message
                    }
                };
            }
        }
    }
}
