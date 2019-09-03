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
    public partial class MainPageMaster : ContentPage
    {
        public Label userName;
        public ListView ListView;
        public MainPageMaster()
        {
            InitializeComponent();
            ListView = MenuItemsListView;
            userName = lblNameUser;
            
            BindingContext = new MainPageMasterViewModel();
        }
    }
}