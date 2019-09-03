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
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        public MainPage(MainPageViewModel viewmodel)
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
            MasterPage.userName.Text = viewmodel.user.Name;

        }
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Model.MenuItem;
            if (item == null)
                return;

            switch (item.Id)
            {
                case 2:
                    item.TargetType = typeof(HistPedidos);
                    break;

                 case 3:
                    new DataBase.UserDataBase().DeleteAllUser();
                  
                    return;
       
            }
            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;
            Detail = new NavigationPage(page);
            IsPresented = false;
            



        }
    }
}