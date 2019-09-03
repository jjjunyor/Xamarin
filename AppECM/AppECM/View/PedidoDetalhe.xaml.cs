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
    public partial class PedidoDetalhe : ContentPage
    {
        private PedidoDetalheModel viewModel;
        public PedidoDetalhe()
        {
            InitializeComponent();
        }
        public PedidoDetalhe(Model.Produto produto)
        {
            InitializeComponent();
            BindingContext = viewModel = new PedidoDetalheModel(produto, this.Navigation);
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