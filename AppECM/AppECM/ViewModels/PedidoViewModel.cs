using AppECM.Model;
using AppECM.Model.ViewModels;
using AppECM.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppECM.ViewModels
{
    public class PedidoViewModel : ViewModelBase
    {
        private MainPageViewModel viewModel;
        public PedidoViewModel(MainPageViewModel ViewModel)
        {
            this.viewModel = ViewModel;
        }
        private ICommand _refreshCommand, _loadMoreCommand, forceRefreshCommand, loadSessionsCommand;
        public PedidoViewModel()
        {
            Produtos = new ObservableCollection<Produto>();
        }
        

        public ObservableCollection<Produto> Produtos { get; set; }
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
             var service = await new ProdutoService().GetProdutoAsync();
            service.ForEach(x => Produtos.Add(new Produto() { descricao = x.descricao, id=x.id,
                PrecoUnitario =x.PrecoUnitario, PrecoUnitarioFormatado = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", x.PrecoUnitario),
                qtdDisponivel =x.qtdDisponivel }));
        }
    }
}
