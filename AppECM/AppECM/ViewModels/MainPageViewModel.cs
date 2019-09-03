using AppECM.Model;
using AppECM.Model.ViewModels;

namespace AppECM.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(User user)
        {

            if (user != null)
                base.user = user;

        }
    }
}
