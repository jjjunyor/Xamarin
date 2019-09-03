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
    public class PedidoService : BaseService
    {

        public async Task<List<Pedido>> GetPedidobyUser(int idUsuario)
        {
            var pedidos = new List<Pedido>();
            using (var client = new HttpClient())
            {
                var uri = ApiKeys._urlRest + string.Format("/pedido/usuario/", idUsuario);
                //client.DefaultRequestHeaders.Add("app_token", ApiKeys._token);
                // client.DefaultRequestHeaders.Add("client_id", ApiKeys._clientId);
                var response = client.GetAsync(uri).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var objJSON = response.Content.ReadAsStringAsync().Result;
                    pedidos = JsonConvert.DeserializeObject<List<Pedido>>(objJSON);

                }
            }
            return pedidos;
        }
     

    }
}
