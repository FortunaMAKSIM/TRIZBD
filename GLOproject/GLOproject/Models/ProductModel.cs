using GLOproject.DataBase.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLOproject.Models
{
    public class ProductModel
    {
        public string Search { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
