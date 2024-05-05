using GLOproject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace GLOproject.Commands
{
    public class NavigCommand : BaseCommand
    {
        private readonly StoreViewModel _viewModelStore;
        private readonly StoreCommand _employeeStore;
        private readonly Func<StoreViewModel, StoreCommand, BaseViewModel> _createViewModel;

        public NavigCommand(StoreViewModel viewModelStore, StoreCommand employeeStore, Func<StoreViewModel, StoreCommand, BaseViewModel> createViewModel)
        {
            _viewModelStore = viewModelStore;
            _employeeStore = employeeStore;
            _createViewModel = createViewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModelStore.CurrentViewModel = _createViewModel(_viewModelStore, _employeeStore);
        }
    }
}
