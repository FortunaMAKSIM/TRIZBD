using GLOproject.DataBase.Entity;
using GLOproject.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GLOproject.Commands;

namespace GLOproject.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly AppDbContext _dbContext;
        private readonly StoreViewModel _viewModelStore;
        private readonly StoreCommand _employeeStore;
        public BaseViewModel CurrentViewModel => _viewModelStore.CurrentViewModel;
        public Employee CurrentEmployee
        {
            get
            {
                Employee employee = _employeeStore.CurrentEmployee;
                if (employee != null)
                {
                    employee.Role = _dbContext.Roles.FirstOrDefault(r => r.ID == employee.RoleID);
                }
                return employee;
            }
        }

        public MainWindowViewModel(StoreViewModel viewModelStore, StoreCommand employeeStore, AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _viewModelStore = viewModelStore;
            _employeeStore = employeeStore;

            _viewModelStore.CurrentViewModel = new ProductViewModel(_dbContext, _employeeStore);
            _viewModelStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            _employeeStore.CurrentEmployee = new Employee();
            _employeeStore.CurrentEmployeeChanged += OnCurrentEmployeeChanged;

            ChangeViewModelCommand = new MainCommand(_viewModelStore, _employeeStore, _dbContext);
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        private void OnCurrentEmployeeChanged()
        {
            OnPropertyChanged(nameof(CurrentEmployee));
        }

        public ICommand ChangeViewModelCommand { get; }
    }
}
