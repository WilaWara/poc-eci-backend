using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CategoryCreateDTO
    {
        [Required(ErrorMessage = "The category name is required")]
        public string Name { get; set; }
    }
}
