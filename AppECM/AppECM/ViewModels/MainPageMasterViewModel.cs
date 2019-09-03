using AppECM.Model;
using AppECM.Model.ViewModels;
using System.Collections.ObjectModel;

namespace AppECM.ViewModels
{
    public class MainPageMasterViewModel : ViewModelBase
    {

        public ObservableCollection<PageTypeGroup> Groups { get; set; }
           
        public MainPageMasterViewModel()
        {


            Groups = new ObservableCollection<PageTypeGroup> {
            new PageTypeGroup("Principal", "A")
            {
                      new MenuItem { Id = 0,
                                    Title = "Alterar Pedido",
                                    Image ="ic_action_ballot" },//add image
                      new MenuItem { Id = 2,
                                    Title = "Histórico de Pedidos",
                                    Image ="ic_action_assignment_late" },


            },
             new PageTypeGroup("Segundary", "B")
             {
                 new MenuItem { Id =4 , Title = "Suporte Técnico", Image ="ic_suport"},
                 new MenuItem { Id =3 , Title = "Sair", Image ="ic_action_power_settings_new"},
             }
          };
        }
    }
}
