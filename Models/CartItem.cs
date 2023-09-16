using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
    }
}
