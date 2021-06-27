using System.ComponentModel.DataAnnotations;

namespace localshop.Core.DTO
{
    public class CategoryDTO
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Category name")]
        public string Name { get; set; }

        public string ParentId { get; set; }
    }
}