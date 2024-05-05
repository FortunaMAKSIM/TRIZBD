using GLOproject.Commands;
using GLOproject.DataBase;
using GLOproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GLOproject.ViewModels
{
    public class LoginVewModel : BaseViewModel
    {
        public LoginModel _loginM;
        public LoginVewModel(StoreViewModel viewModelStore, StoreCommand employeeStore, AppDbContext dbContext)
        {
            _loginM = new LoginModel();
            LoginCommand = new LoginCommand(viewModelStore, employeeStore, this, dbContext);
        }

        public string Login
        {
            get { return _loginM.LoginEmail; }
            set
            {
                _loginM.LoginEmail = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get { return _loginM.Password; }
            set
            {
                _loginM.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand { get; set; }
    }
}
