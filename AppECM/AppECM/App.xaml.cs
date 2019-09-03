using AppECM.Helps;
using AppECM.Model;
using AppECM.ViewModels;
using FormsToolkit;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppECM
{
    public partial class App : Application
    {
        LoginViewModel loginViewModel;
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
            loginViewModel = new LoginViewModel();
        }

        protected override void OnStart()
        {
            OnResume();
            var lastUser = new DataBase.UserDataBase().GetLastUser();
            if (lastUser == null)
            {
                var viewLogin = new View.Login(loginViewModel);
                MainPage = viewLogin;
            }
            else
                logon(lastUser);
        }
        private async void logon(User user)
        {
            
              loginViewModel.logon(user);
            if (loginViewModel.NowAuthenticate.IsAuthenticated)
                MainPage = new View.MainPage(new MainPageViewModel(loginViewModel.user));
            else
                MainPage = new View.Login(loginViewModel);
        }
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            CrossConnectivity.Current.ConnectivityChanged += ConnectivityChanged;
            // Handle when your app resumes
            MessagingService.Current.Subscribe<MessagingServiceAlert>(MessageKeys.Message, async (m, info) =>
            {
                var task = Application.Current?.MainPage?.DisplayAlert(info.Title, info.Message, info.Cancel);

                if (task == null)
                    return;

                await task;
                info?.OnCompleted?.Invoke();
            });
            MessagingService.Current.Subscribe<MessagingServiceQuestion>(MessageKeys.Question, async (m, q) =>
            {
                var task = Application.Current?.MainPage?.DisplayAlert(q.Title, q.Question, q.Positive, q.Negative);
                if (task == null)
                    return;
                var result = await task;
                q?.OnCompleted?.Invoke(result);
            });
            MessagingService.Current.Subscribe<MessagingServiceChoice>(MessageKeys.Choice, async (m, q) =>
            {
                var task = Application.Current?.MainPage?.DisplayActionSheet(q.Title, q.Cancel, q.Destruction, q.Items);
                if (task == null)
                    return;
                var result = await task;
                q?.OnCompleted?.Invoke(result);
            });
        }
        protected async void ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {

            if (!e.IsConnected)
            {
                //we went offline, should alert the user and also update ui (done via settings)
                var task = Application.Current?.MainPage?.DisplayAlert("Offline", "O aplicativo não esta conectado em uma rede, Por favor verificar a conexão com a internet.", "OK");
                if (task != null)
                    await task;
            }
        }
    }
}
