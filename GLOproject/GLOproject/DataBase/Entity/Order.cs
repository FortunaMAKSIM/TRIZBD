using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GLOproject.DataBase.Entity
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalAmount { get; set; }


        [ForeignKey("Customer")]
        public int CustomerID { get; set; }

        [ForeignKey("PickupPoint")]
        public int PickupPointID { get; set; }

        public Customer Customer { get; set; }
        public PickupPoint PickupPoint { get; set; }
    }
}
