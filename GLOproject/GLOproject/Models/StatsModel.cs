using GLOproject.DataBase.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLOproject.Models
{
    public class StatsModel
    {
        public string SearchPoint { get; set; }
        public string SearchEmp { get; set; }

        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<PickupPoint> PickupPoints { get; set; }
    }
}
