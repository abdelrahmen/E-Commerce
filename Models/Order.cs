using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }//without tax or additional fees

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }//with additional fees

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        public string status { get; set; } = "Pending";


		public User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
