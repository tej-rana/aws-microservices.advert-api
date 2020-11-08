using System.ComponentModel.DataAnnotations;

namespace AdvertApi.Management.Web.Models
{
    public class CreateAdvertViewModel
    {
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        public string Description { get; set; }
        
        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
    }
}