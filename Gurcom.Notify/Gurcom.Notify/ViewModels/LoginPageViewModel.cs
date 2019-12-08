using Gurcom.Notify.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Gurcom.Notify.ViewModels
{
    public class LoginPageViewModel : BasePageViewModel
    {
        private readonly INavigation Navigation;

        public LoginPageViewModel(INavigation _navigation)
        {
            Navigation = _navigation;

        }

        #region Properties

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                if (SetPropertyValue(ref _email, value))
                {
                    ((Command)LoginCommand).ChangeCanExecute();
                }
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (SetPropertyValue(ref _password, value))
                {
                    ((Command)LoginCommand).ChangeCanExecute();
                }
            }
        }

        private bool _isShowCancel;
        public bool IsShowCancel
        {
            get { return _isShowCancel; }
            set { SetPropertyValue(ref _isShowCancel, value); }
        }

        #endregion


        #region Commands

        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get { return _loginCommand = _loginCommand ?? new Command(LoginAction, CanLoginAction); }
        }

        private ICommand _cancelLoginCommand;
        public ICommand CancelLoginCommand
        {
            get { return _cancelLoginCommand = _cancelLoginCommand ?? new Command(CancelLoginAction); }
        }

        private ICommand _forgotPasswordCommand;
        public ICommand ForgotPasswordCommand
        {
            get { return _forgotPasswordCommand = _forgotPasswordCommand ?? new Command(ForgotPasswordAction); }
        }

        private ICommand _newAccountCommand;
        public ICommand NewAccountCommand
        {
            get { return _newAccountCommand = _newAccountCommand ?? new Command(NewAccountAction); }
        }

        #endregion


        #region Methods

        bool CanLoginAction()
        {
            //Perform your "Can Login?" logic here...

            if (string.IsNullOrWhiteSpace(this.Email) || string.IsNullOrWhiteSpace(this.Password))
                return false;

            return true;
        }

        async void LoginAction()
        {
            IsBusy = true;

            if (this.Email == "1" && this.Password == "11")
            {
                IsBusy = false;
                App.Current.MainPage = new MainPage();              
                IsLogin = true;
            }
            else
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Uyarı", "Kullanıcı Adı veya Şifre Hatalı", "Tamam");
            }


        }

        void CancelLoginAction()
        {
            //TODO - perform cancellation logic

            IsBusy = false;
            IsShowCancel = false;
        }

        void ForgotPasswordAction()
        {
            //TODO - navigate to your forgotten password page
            //Navigation.PushAsync(XXX);
        }

        void NewAccountAction()
        {
            //TODO - navigate to your registration page
            //Navigation.PushAsync(XXX);
        }

        #endregion
    }
}
