using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using AppECM.Services.Base;
using AppECM.Model;
using AppECM.Helps;
using System.Net.Http;
using System.Collections.Generic;
using System.Text;
using FormsToolkit;

namespace AppECM.Services
{
    public class ProdutoService : BaseService
    {
        
        public async Task<List<Produto>> GetProdutoAsync()
        {
            var produtos = new List<Produto>();
            using (var client = new HttpClient())
            {
                var uri = ApiKeys._urlRest + "/produto/";
                //client.DefaultRequestHeaders.Add("app_token", ApiKeys._token);
                // client.DefaultRequestHeaders.Add("client_id", ApiKeys._clientId);
                var response = client.GetAsync(uri).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var objJSON = response.Content.ReadAsStringAsync().Result;
                    produtos = JsonConvert.DeserializeObject<List<Produto>>(objJSON);

                }
            }
            return produtos;
        }
        public async Task<List<Produto>> EnviarPedidoAsync(string json)
        {
            var produtos = new List<Produto>();
            using (var client = new HttpClient())
            {
                var uri = ApiKeys._urlRest + "/pedido/";

                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                //client.DefaultRequestHeaders.Add("app_token", ApiKeys._token);
                //client.DefaultRequestHeaders.Add("client_id", ApiKeys._clientId);

                var response = client.PostAsync(uri, content).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.Message, new MessagingServiceAlert
                    {
                        Title = "Mensagem",
                        Message = "Compra efetuada com sucesso.",
                        Cancel = "OK",
                    });
                }
              

            }
            return produtos;
        }

    }
}
