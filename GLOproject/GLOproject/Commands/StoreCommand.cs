using GLOproject.DataBase.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLOproject.Commands
{
    public class StoreCommand
    {
        Employee _currentEmployee = new();

        public Employee CurrentEmployee
        {
            get { return _currentEmployee; }
            set
            {
                _currentEmployee = value;
                OnCurrentEmployeeChanged();
            }
        }

        public event Action CurrentEmployeeChanged;
        private void OnCurrentEmployeeChanged()
        {
            CurrentEmployeeChanged?.Invoke();
        }
    }
}
