using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Core.DTO
{
    public class BannerDTO
    {
        public string Id { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string Link { get; set; }
    }
}
