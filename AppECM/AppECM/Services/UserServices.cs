using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using AppECM.Services.Base;
using AppECM.Model;

namespace AppECM.Services
{
    public class UserServices : BaseService
    {
        private readonly string url = "/usuario/";
        public async Task<User> GetUserAsync(User user)
        {
            Users userAcess = null;
            var request = await GetAsync(url + user.UserName);
            if (request.StatusCode == HttpStatusCode.OK)
            {
                var response = await request.Content.ReadAsStringAsync();
                userAcess = JsonConvert.DeserializeObject<Users>(response);
                if (userAcess != null)
                {
                  
                    user.Name = userAcess.login;
                }
                return user;
            }
            else
            {
                new Error() { Success = false, Message = $"Ocorreu um erro ao chamar o seviço \n{(int)request.StatusCode} - {request.ReasonPhrase}" };
            }
            return new User();
        }

    }
}
