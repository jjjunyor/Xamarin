using FormsToolkit;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using AppECM.Contratos;
using AppECM.Helps;
using AppECM.Model;

namespace AppECM.Model.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected IToast Toast { get; } = DependencyService.Get<IToast>();
        private User _user;
        private bool _isBusy;
        private bool _isNotBusy = true;
        private int _page;
        private bool _isEmpty;
        public bool isEmpty
        {
            get { return _isEmpty; }
            set
            {
                _isEmpty = value;
                OnPropertyChanged();
            }
        }
        public int page
        {
            get { return _page; }
            set
            {
                _page = value;

            }
        }
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (value)
                    IsNotBusy = false;

                else
                    IsNotBusy = true;

                _isBusy = value;
                OnPropertyChanged();
            }
        }
        public bool IsNotBusy
        {
            get { return _isNotBusy; }
            set
            {
                _isNotBusy = value;
                OnPropertyChanged();
            }
        }
        public User user
        {
            get
            {
                if (_user != null)
                    return _user;
                else
                    return new User().getUser();
            }
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }
        public static void Init()
        {

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propretyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propretyName));
            }

        }
        protected void messageError(string title, string message)
        {
            MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.Message, new MessagingServiceAlert
            {
                Title = title,
                Message = message,
                Cancel = "OK",
            });
        }
    }
}
