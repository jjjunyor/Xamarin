using AppECM.Model;
using AppECM.Model.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppECM.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public Authenticate NowAuthenticate { get; set; }
        public Command CommandLogin { get; set; }

        public LoginViewModel()
        {
            NowAuthenticate = new Authenticate();
            CommandLogin = new Command(Login);
        }
        private async void Login()
        {
            IsBusy = true;
            if (NowAuthenticate.User != null)
            {
                if (String.IsNullOrEmpty(NowAuthenticate.User.UserName) || String.IsNullOrEmpty(NowAuthenticate.User.Password))
                {
                    NowAuthenticate.Message = "Campo nome e senha devem ser preenchidos!";
                    NowAuthenticate.IsAuthenticated = false;
                    OnPropertyChanged("NowAuthenticate");
                    IsBusy = false;
                    return;
                }
                var returnService = await new Services.SSOServices().LoginUserAsync(NowAuthenticate);
                if (returnService.IsAuthenticated)
                {
                    user = await new Services.UserServices().GetUserAsync(NowAuthenticate.User);
                    if (!string.IsNullOrEmpty(user.Name))
                    {
                        saveUser();
                        IsBusy = false;
                        
                        App.Current.MainPage = new View.MainPage(new MainPageViewModel(user));
                    }
                    else
                    {
                        NowAuthenticate.Error = true;
                        NowAuthenticate.Message = "Usuario não encontrado!";
                        NowAuthenticate.IsAuthenticated = false;
                    }
                }
            }
            OnPropertyChanged("NowAuthenticate");
            IsBusy = false;
            return;
        }
        public   async void logon(User user)
        {
            NowAuthenticate.User = user;
            var returnService = new Services.SSOServices().LoginUser(NowAuthenticate);
            if (!returnService.IsAuthenticated && !returnService.Error)
                NowAuthenticate.IsAuthenticated = false;
        }
        public void saveUser(string token = null)
        {
            user.saveUser(user);
        }
    }
}
