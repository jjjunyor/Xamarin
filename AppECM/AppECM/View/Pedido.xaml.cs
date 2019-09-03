using AppECM.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppECM.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pedido : ContentPage, INotifyPropertyChanged
    {
        PedidoViewModel viewModel;
        public Pedido()
        {
            InitializeComponent();
            BindingContext = viewModel = new PedidoViewModel();

            ServiceOrderListView.ItemTapped += (sender, e) => ServiceOrderListView.SelectedItem = null;
            ServiceOrderListView.ItemSelected += async (sender, e) =>
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                var item = ServiceOrderListView.SelectedItem as Model.Produto;
                if (item == null)
                    return;
             
                await Navigation.PushAsync(new PedidoDetalhe(item));
                ServiceOrderListView.SelectedItem = null;
                IsBusy = false;
            };
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.closePage)
                await Navigation.PopModalAsync();
            else
            {
                await viewModel.OnAppearing();
                var cancel = new ToolbarItem
                {
                    Text = "Cancel",
                    Command = new Command(async () =>
                    {
                        if (IsBusy)
                            return;
                        await Navigation.PopModalAsync();
                    })
                };
                ToolbarItems.Add(cancel);
                if (Device.OS != TargetPlatform.iOS)
                    if (Device.RuntimePlatform != Device.iOS)
                        cancel.Icon = "toolbar_close.png";
            }
        }
    }
}