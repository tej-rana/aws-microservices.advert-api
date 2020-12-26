using System.ComponentModel.DataAnnotations;

namespace AdvertApi.Management.Web.Models.Accounts
{
    public class ConfirmModel
    {
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; }
    }
}