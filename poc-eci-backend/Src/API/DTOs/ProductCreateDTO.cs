using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class ProductCreateDTO
    {
        [Required(ErrorMessage = "The product name is required")]
        public string Name { get; set; }
        public string Photo { get; set; }

        [Required(ErrorMessage = "The product description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The product stock is required")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "The product price is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The product expiration date is required")]
        public DateTime ExpireDate { get; set; }

        [Required(ErrorMessage = "The product category id is required")]
        public int CategoryId { get; set; }
    }
}
