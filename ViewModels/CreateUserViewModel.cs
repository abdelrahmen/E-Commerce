using System.ComponentModel.DataAnnotations;

namespace E_Commerce.ViewModels
{
    public class CreateUserViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MaxLength(100)]
        [DataType(DataType.Password), Compare("Password", ErrorMessage ="this field should be equal to Password Field")]
        [Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; }


        [Required]
        [MaxLength(100)]
        public string Fullname { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }
    }
}
