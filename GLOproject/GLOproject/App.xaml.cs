using GLOproject.Commands;
using GLOproject.DataBase;
using GLOproject.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace GLOproject
{
    public partial class App : Application
    {
        private readonly AppDbContext _context;
        private readonly StoreViewModel _viewModelStore;
        private readonly StoreCommand _employeeStore;

        public App()
        {
            _context = new AppDbContext();
            _viewModelStore = new StoreViewModel();
            _employeeStore = new StoreCommand();

            _viewModelStore.CurrentViewModel = new MainWindowViewModel(_viewModelStore, _employeeStore, _context);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel(_viewModelStore, _employeeStore, _context)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }

}
