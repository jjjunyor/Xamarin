using AppECM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppECM.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login(LoginViewModel login)
        {
            InitializeComponent();
            BindingContext = login;
        }
        public Login()
        {
            InitializeComponent();
            var login = new LoginViewModel();
            
            BindingContext = login;
        }
    }
}