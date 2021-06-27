using System.ComponentModel.DataAnnotations;

namespace localshop.Core.DTO
{
    public class UpdateProfileDTO
    {
        [Required]
        [Display(Name = "First name *")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name *")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Phone number *")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Country *")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "City *")]
        public string City { get; set; }

        public string State { get; set; }

        [Required]
        [Display(Name = "Zip *")]
        public string Zip { get; set; }

        [Required]
        [Display(Name = "Address 1 *")]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Image { get; set; }

        public string FullName
        {
            get
            {
                return $"{LastName} {FirstName}";
            }
        }
    }
}