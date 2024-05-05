using GLOproject.DataBase.Entity;
using GLOproject.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GLOproject.Models;
using GLOproject.Commands;

namespace GLOproject.ViewModels
{
    public class StatsViewModel : BaseViewModel
    {
        private readonly AppDbContext _dbContext;
        private StatsModel _statisticM;
        private readonly StoreCommand _employeeStore;
        private ObservableCollection<PickupPoint> _allPickupPoints;
        private ObservableCollection<Employee> _allEmployees;
        private bool _isAdmin;

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                if (_isAdmin != value)
                {
                    _isAdmin = value;
                    OnPropertyChanged(nameof(IsAdmin));
                }
            }
        }
        private bool _isWorker;

        public bool IsWorker
        {
            get { return _isWorker; }
            set
            {
                if (_isWorker != value)
                {
                    _isWorker = value;
                    OnPropertyChanged(nameof(IsWorker));
                }
            }
        }
        private bool _isSave;

        public bool IsSave
        {
            get { return _isSave; }
            set
            {
                if (_isSave != value)
                {
                    _isSave = value;
                    OnPropertyChanged(nameof(IsSave));
                }
            }
        }
        public ObservableCollection<Employee> EmployeesDataList { get; set; } = new ObservableCollection<Employee>();
        public ObservableCollection<PickupPoint> PickupPointsDataList { get; set; } = new ObservableCollection<PickupPoint>();

        public StatsViewModel(AppDbContext dbContext, StoreCommand employeeStore)
        {
            _dbContext = dbContext;
            _employeeStore = employeeStore;
            _statisticM = new StatsModel();
            _allEmployees = new ObservableCollection<Employee>(_dbContext.Employees);
            _allPickupPoints = new ObservableCollection<PickupPoint>(_dbContext.PickupPoints);

            empList();
            pointList();

            IsAdmin = _employeeStore.CurrentEmployee.RoleID != 2;
            IsWorker = _employeeStore.CurrentEmployee.RoleID != 3;
            IsSave = _employeeStore.CurrentEmployee.RoleID == 2;

            PropertyChanged += StatisticsVM_PropertyChanged;
            SaveCommand = new SaveCommand(_dbContext);
        }

        private void empList()
        {
            var employees = _dbContext.Employees;

            foreach (var emp in employees)
            {
                EmployeesDataList.Add(emp);
            }
        }

        private void pointList()
        {
            var pickupPoints = _dbContext.PickupPoints;

            if (_employeeStore.CurrentEmployee.RoleID == 3)
            {
                var employeePickupPoint = pickupPoints.FirstOrDefault(pp => pp.ID == _employeeStore.CurrentEmployee.PickupPointID);
                PickupPointsDataList.Clear();
                if (employeePickupPoint != null)
                {
                    PickupPointsDataList.Add(employeePickupPoint);
                }
            }
            else
            {
                foreach (var point in pickupPoints)
                {
                    PickupPointsDataList.Add(point);
                }
            }
        }

        private void StatisticsVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SearchPoint))
            {
                UpdateFilteredPoint();
            }
            else if (e.PropertyName == nameof(SearchEmp))
            {
                UpdateFilteredEmployee();
            }
        }

        private void UpdateFilteredPoint()
        {
            PickupPointsDataList.Clear();

            foreach (var point in _allPickupPoints)
            {
                if (MatchesPointSearch(point))
                {
                    PickupPointsDataList.Add(point);
                }
            }
        }

        private void UpdateFilteredEmployee()
        {
            EmployeesDataList.Clear();

            foreach (var employee in _allEmployees)
            {
                if (MatchesEmployeeSearch(employee))
                {
                    EmployeesDataList.Add(employee);
                }
            }
        }

        private bool MatchesPointSearch(PickupPoint point)
        {
            return string.IsNullOrWhiteSpace(SearchPoint) || point.Location.Contains(SearchPoint, StringComparison.OrdinalIgnoreCase);
        }

        private bool MatchesEmployeeSearch(Employee employee)
        {
            return string.IsNullOrWhiteSpace(SearchEmp) || employee.Name.Contains(SearchEmp, StringComparison.OrdinalIgnoreCase);
        }

        public string SearchPoint
        {
            get { return _statisticM.SearchPoint; }
            set { _statisticM.SearchPoint = value; OnPropertyChanged(nameof(SearchPoint)); }
        }

        public string SearchEmp
        {
            get { return _statisticM.SearchEmp; }
            set { _statisticM.SearchEmp = value; OnPropertyChanged(nameof(SearchEmp)); }
        }

        public ICommand SaveCommand { get; set; }
    }
}
