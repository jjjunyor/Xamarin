using AppECM.Model;
using AppECM.Model.ViewModels;
using AppECM.Services;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppECM.ViewModels
{
    public class HistPedidoViewModel : ViewModelBase
    {
        private MainPageViewModel viewModel;
        public HistPedidoViewModel(MainPageViewModel ViewModel)
        {
            this.viewModel = ViewModel;
        }
        
        public HistPedidoViewModel()
        {
            Pedidos = new ObservableCollection<Pedido>();
        }


        public ObservableCollection<Pedido> Pedidos { get; set; }
        private bool _closePage;
        public bool closePage
        {
            get { return _closePage; }
            set
            {
                if (value == _closePage) return;
                _closePage = value;
                OnPropertyChanged("closePage");
            }
        }
        public async Task OnAppearing()
        {

            await loadInput();
        }
        public async Task loadInput()
        {
            var service = await new PedidoService().GetPedidobyUser(user.Id);


            service.ForEach(x => Pedidos.Add(new Pedido()
            {
                pedidoformatado = x.pedidoformatado

          
            }));
        }
    }
}
