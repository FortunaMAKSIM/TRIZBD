using GLOproject.DataBase.Entity;
using GLOproject.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLOproject.ViewModels;
using System.Windows;

namespace GLOproject.Commands
{
    class LoginCommand : BaseCommand
    {
        private readonly AppDbContext _dbContext;
        private readonly StoreViewModel _viewModelStore;
        private readonly StoreCommand _employeeStore;
        private readonly LoginVewModel _loginViewModel;

        public LoginCommand(StoreViewModel viewModelStore, StoreCommand employeeStore, LoginVewModel loginViewModel, AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _viewModelStore = viewModelStore;
            _employeeStore = employeeStore;
            _loginViewModel = loginViewModel;
        }

        public override void Execute(object parameter)
        {
            try
            {
                Employee user = _dbContext.Employees.FirstOrDefault(e => e.Login == _loginViewModel.Login && e.Password == _loginViewModel.Password);

                if (user != null)
                {
                    _employeeStore.CurrentEmployee = user;
                    if (_employeeStore.CurrentEmployee.Role == _dbContext.Roles.FirstOrDefault(r => r.ID == 1))
                        _viewModelStore.CurrentViewModel = new StatsViewModel(_dbContext, _employeeStore);
                    else
                        _viewModelStore.CurrentViewModel = new ProductViewModel(_dbContext, _employeeStore);
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
