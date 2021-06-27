using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace localshop.Areas.Admin.Models
{
    public class Configuration
    {
        public string Mail { get; set; }
        public string Phone { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Github { get; set; }
        public string Address { get; set; }

        [Display(Name = "Opening time")]
        public string OpeningTime { get; set; }
    }
}