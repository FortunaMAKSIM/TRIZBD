using GLOproject.DataBase.Entity;
using GLOproject.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using GLOproject.ViewModels;

namespace GLOproject.Commands
{
    public class MainCommand : BaseCommand
    {
        private readonly AppDbContext _dbContext;
        private readonly StoreViewModel _viewModelStore;
        private readonly StoreCommand _employeeStore;

        public MainCommand(StoreViewModel viewModelStore, StoreCommand employeeStore, AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _viewModelStore = viewModelStore;
            _employeeStore = employeeStore;
        }

        public override void Execute(object parameter)
        {
            if (parameter is string viewModelName)
            {
                Func<StoreViewModel, StoreCommand, BaseViewModel> createViewModel = viewModelName switch
                {
                    "LoginVewModel" => (vmStore, empStore) => new LoginVewModel(vmStore, empStore, _dbContext),
                    "PickUpViewModel" => (vmStore, empStore) => new PickUpViewModel(_dbContext, _employeeStore),
                    "ProductViewModel" => (vmStore, empStore) => new ProductViewModel(_dbContext, _employeeStore),
                    "StatsViewModel" => (vmStore, empStore) => new StatsViewModel(_dbContext, _employeeStore),
                    _ => throw new ArgumentException("Invalid ViewModel name"),
                };

                NavigCommand navigateCommand = new(_viewModelStore, _employeeStore, createViewModel);
                navigateCommand.Execute(null);
            }
            else
            {
                throw new ArgumentException("Invalid parameter type");
            }
        }
    }
}
