
using AppECM.Contratos;
using AppECM.Model;
using AppECM.Services.Base;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppECM.Services
{
    public class SSOServices : BaseService
    {
        private readonly string url = "/usuario/validarlogin";
        public async Task<Authenticate> LoginUserAsync(Authenticate authenticate)
        {
            object param = new { login = authenticate.User.UserName.Trim(), senha = authenticate.User.Password.Trim() };
            var response = await PostAsync(url, param);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var objJSON = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<Acess>(objJSON);
                if (obj.acesso)
                    authenticate.IsAuthenticated = true;
                else
                {
                    authenticate.Message = "Usuario ou Senha Invalidos!";
                    authenticate.IsAuthenticated = false;
                }
            }
            else
            {
                authenticate.IsAuthenticated = false;
                authenticate.Error = true;
            }
            return authenticate;
        }
        public Authenticate LoginUser(Authenticate authenticate)
        {
            var serviceAcess = new Acess();
            // var response =  PostAsync(url, new { login = authenticate.User.UserName.Trim(), senha = authenticate.User.Password.Trim() });
            var response = Post(url, serviceAcess, new { login = authenticate.User.UserName, senha = authenticate.User.Password });
            if (!response.Error)
            {
                if (response.acesso)
                    authenticate.IsAuthenticated = true;
                else
                {
                    response.Message = "Usuario ou Senha Invalidos!";
                    authenticate.IsAuthenticated = false;
                }
            }
            else
            {
                authenticate.IsAuthenticated = false;
                authenticate.Error = true;
                authenticate.Message = response.Message;
            }
            return authenticate;
        }
    }
    public class Acess : IReturnService
    {
        public string Acessar { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
        public bool acesso { get; set; }

    }
}
