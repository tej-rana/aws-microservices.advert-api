using System.ComponentModel.DataAnnotations;

namespace AdvertApi.Management.Web.Models.Accounts
{
    public class SignUpModel
    {
        [Required]
        [EmailAddress]
        [Display(Name="Email")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(6, ErrorMessage = "Password must be 6 characters long.")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm Password must match Password.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        
    }
}