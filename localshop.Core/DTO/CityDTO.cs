using System.ComponentModel.DataAnnotations;

namespace localshop.Core.DTO
{
    public class CityDTO
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "City name")]
        public string Name { get; set; }

        public string Zone { get; set; }
        [Required]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }
    }
}