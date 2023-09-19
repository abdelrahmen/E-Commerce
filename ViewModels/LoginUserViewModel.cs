using System.ComponentModel.DataAnnotations;

namespace E_Commerce.ViewModels
{
    public class LoginUserViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
