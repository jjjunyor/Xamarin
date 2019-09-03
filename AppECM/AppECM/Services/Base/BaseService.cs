using AppECM.Contratos;
using AppECM.Helps;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppECM.Services.Base
{
    public class BaseService
    {
        private readonly string urlBase;
        public BaseService()
        {
            urlBase = ApiKeys._urlRest;
        }
        public async Task<HttpResponseMessage> PostAsync(string path, object param = null)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{urlBase}{path}";
                    string json = string.Empty;
                    if (param != null)
                    {
                        json = JsonConvert.SerializeObject(param,
                                                                  Formatting.None,
                                                                 new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    }
                    //   client.DefaultRequestHeaders.Add("app_token", ApiKeys._token);
                    //  client.DefaultRequestHeaders.Add("client_id", ApiKeys._clientId);

                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    var resultado = await  client.PostAsync(url, content);
  
                    return resultado;
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ConnectFailure)
                    return new HttpResponseMessage { StatusCode = (HttpStatusCode)(WebExceptionStatus.ConnectFailure), ReasonPhrase = "falha na conexão da internet!" };
                else
                    return new HttpResponseMessage { StatusCode = (HttpStatusCode)ex.Status, ReasonPhrase = ex.Message.ToString() };
            }
            catch (System.Exception ex)
            {
                return new HttpResponseMessage { StatusCode = (HttpStatusCode)(WebExceptionStatus.UnknownError), ReasonPhrase = ex.Message.ToString() };
            }

        }
        public async Task<T> PostAsync<T>(string path, T obj, object param = null) where T : IReturnService
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                obj.Error = true;
                obj.Message = "Sem Conexão com a Internet";
                return obj;
            }
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{urlBase}{path}";
                    string json = string.Empty;
                    if (param != null)
                    {
                        json = JsonConvert.SerializeObject(param,
                                                           Formatting.None,
                                                          new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    }
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                   // client.DefaultRequestHeaders.Add("app_token", ApiKeys._token);
                  //  client.DefaultRequestHeaders.Add("client_id", ApiKeys._clientId);
                  
                    var response = await client.PostAsync(url, content);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                     
                        var jsonString = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<T>(jsonString);
                    }
                    else
                    {
                        obj.Error = true;
                        obj.Message = $"Ocorreu um erro ao chamar o seviço \n{(int)response.StatusCode} - {response.ReasonPhrase}";
                        return obj;
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ConnectFailure)
                {
                    obj.Error = true;
                    obj.Message = $"falha na conexão da internet!";
                    return obj;
                }
                else
                {
                    obj.Error = true;
                    obj.Message = $"Ocorreu um Erro \n{(HttpStatusCode)ex.Status}-{ ex.Message.ToString()}";
                    return obj;
                }
            }
            catch (System.Exception ex)
            {
                obj.Error = true;
                obj.Message = $"Ocorreu um Erro \n{(HttpStatusCode)WebExceptionStatus.UnknownError}-{ ex.Message.ToString()}";
                return obj;
            }

        }
        public T Post<T>(string path, T obj, object param = null) where T : IReturnService
        {
            //if (!CrossConnectivity.Current.IsConnected)
            //{
            //    obj.Error = true;
            //    obj.Message = "Sem Conexão com a Internet";
            //    return obj;
            //}
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{urlBase}{path}";
                    string json = string.Empty;
                    if (param != null)
                    {
                        json = JsonConvert.SerializeObject(param,
                                                                  Formatting.None,
                                                                 new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    }
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    //client.DefaultRequestHeaders.Add("app_token", ApiKeys._token);
                    //client.DefaultRequestHeaders.Add("client_id", ApiKeys._clientId);
                  
                    var response = client.PostAsync(url, content).Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                     
                        var jsonString = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<T>(jsonString);
                    }
                    else
                    {
                        obj.Error = true;
                        obj.Message = $"Ocorreu um erro ao chamar o seviço \n{(int)response.StatusCode} - {response.ReasonPhrase}";
                        return obj;
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ConnectFailure)
                {
                    obj.Error = true;
                    obj.Message = $"falha na conexão da internet!";
                    return obj;
                }
                else
                {
                    obj.Error = true;
                    obj.Message = $"Ocorreu um Erro \n{(HttpStatusCode)ex.Status}-{ ex.Message.ToString()}";
                    return obj;
                }
            }
            catch (System.Exception ex)
            {
                obj.Error = true;
                obj.Message = $"Ocorreu um Erro \n{(HttpStatusCode)WebExceptionStatus.UnknownError}-{ ex.Message.ToString()}";
                return obj;
            }

        }
        public async Task<HttpResponseMessage> GetAsync(string path, object param = null)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{urlBase}{path}";
                    //client.DefaultRequestHeaders.Add("app_token", ApiKeys._token);
                    //client.DefaultRequestHeaders.Add("client_id", ApiKeys._clientId);
                    var resultado =  await client.GetAsync(url);
                    return resultado;
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ConnectFailure)
                    return new HttpResponseMessage { StatusCode = (HttpStatusCode)(WebExceptionStatus.ConnectFailure) };
                else
                    return new HttpResponseMessage { StatusCode = (HttpStatusCode)ex.Status, ReasonPhrase = ex.Message.ToString() };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage { StatusCode = (HttpStatusCode)(WebExceptionStatus.UnknownError), ReasonPhrase = ex.Message.ToString() };
            }

        }
        public HttpResponseMessage Get(string path, object param = null)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{urlBase}{path}";
                  //  client.DefaultRequestHeaders.Add("app_token", ApiKeys._token);
                  //  client.DefaultRequestHeaders.Add("client_id", ApiKeys._clientId);
                       return client.GetAsync(url).Result;
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ConnectFailure)
                    return new HttpResponseMessage { StatusCode = (HttpStatusCode)(WebExceptionStatus.ConnectFailure) };
                else
                    return new HttpResponseMessage { StatusCode = (HttpStatusCode)(WebExceptionStatus.UnknownError) };
            }
            catch (System.Exception)
            {
                return new HttpResponseMessage { StatusCode = (HttpStatusCode)(WebExceptionStatus.UnknownError) };
            }

        }
    }
}
