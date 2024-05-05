using GLOproject.DataBase.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLOproject.Models
{
    public class PickUpModel
    {
        public string Search { get; set; }
        public IEnumerable<PickupEmployee> EmployeesOrders { get; set; }
    }
}
