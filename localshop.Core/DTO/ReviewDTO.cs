using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Core.DTO
{
    public class ReviewDTO
    {
        public ReviewDTO()
        {
            Rating = 4;
            IsApproved = false;
        }

        public string UserId { get; set; }

        [Required]
        public string ProductId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [MinLength(6)]
        [MaxLength(1500)]
        [Display(Name = "Review")]
        public string Body { get; set; }

        public bool IsApproved { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
