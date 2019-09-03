using AppECM.Model;
using AppECM.Model.ViewModels;
using AppECM.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppECM.ViewModels
{
    public class PedidoDetalheModel : ViewModelBase
    {
        #region Atributos
        private int id { get; set; }
        public int ID
        {
            get { return id; }
            set
            {
                if (value == id) return;
                id = value;
                OnPropertyChanged("ID");
            }
        }
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
        private string descricao;
        public string Descricao
        {
            get { return descricao; }
            set
            {
                descricao = value;
                OnPropertyChanged("Descricao");
            }

        }


        private int qtdDisponivel;
        public int QtdDisponivel
        {
            get { return qtdDisponivel; }
            set
            {
                qtdDisponivel = value;
                OnPropertyChanged("QtdDisponivel");
            }

        }

        private double precoUnitario;
        public double PrecoUnitario
        {
            get { return precoUnitario; }
            set
            {
                precoUnitario = value;
                OnPropertyChanged("PrecoUnitario");
            }
        }
        private string precoUnitarioFormatado;
        public string PrecoUnitarioFormatado
        {
            get { return precoUnitarioFormatado; }
            set
            {
                precoUnitarioFormatado = value;
                OnPropertyChanged("PrecoUnitarioFormatado");
            }
        }
        private int qtdSolicitada;
        public int QtdSolicitada
        {
            get { return qtdSolicitada; }
            set
            {
                qtdSolicitada = value;
                OnPropertyChanged("QtdSolicitada");
            }
        }
        private string total;
        public string Total
        {
            get { return total; }
            set
            {
                total = value;
                OnPropertyChanged("Total");
            }
        }
        #endregion
        #region Eventos
        private Model.Produto viewModel;
        private ICommand _btnAddComand, _btnSubComand;
        public ICommand btnAddComand => (_btnAddComand = new Command(async () => await btnAddComandAsync()));
        public ICommand btnSubComand => (_btnSubComand = new Command(async () => await btnSubComandAsync()));
        public ICommand btnFinalizarCompraCommand => (_btnSubComand = new Command(async () => await btnFinalizarCompraCommandAsync()));
        async Task btnAddComandAsync()
        {

            QtdSolicitada += 1;
            var strTotal = (PrecoUnitario * QtdSolicitada);
            Total = "Valor Total: " + string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", strTotal);

        }
        async Task btnFinalizarCompraCommandAsync()
        {
            Pedido objPedido = new Pedido()
            {
                idProduto = ID,
                quantidade = QtdSolicitada,
                idUsuario = user.Id
            };
            var json = JsonConvert.SerializeObject(objPedido);
            await new ProdutoService().EnviarPedidoAsync(json);

        }
        async Task btnSubComandAsync()
        {
            QtdSolicitada -= 1;
            var strTotal = (PrecoUnitario * QtdSolicitada);
            Total = "Valor Total: " + string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", strTotal);
        }
        public async Task OnAppearing()
        {

            await loadInput();
        }
        public async Task loadInput()
        {


        }
        #endregion
        public PedidoDetalheModel(Model.Produto produto, INavigation navigation)
        {
            this.viewModel = produto;
            qtdDisponivel = viewModel.qtdDisponivel;
            descricao = viewModel.descricao;
            PrecoUnitarioFormatado = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", viewModel.PrecoUnitario);
            PrecoUnitario = viewModel.PrecoUnitario;
            QtdSolicitada = 1;
            ID = produto.id;

        }
    }

}
