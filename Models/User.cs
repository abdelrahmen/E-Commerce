using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class User:IdentityUser
    {
        //[Required]
        //[MaxLength(50)]
        //public string Username { get; set; }

        //[Required]
        //[MaxLength(100)]
        //[EmailAddress]
        //public string Email { get; set; }

        //[Required]
        //[MaxLength(100)]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }

        [Required]
        [MaxLength(100)]
        public string Fullname { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
