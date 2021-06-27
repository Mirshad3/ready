using localshop.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localshop.ViewModels.Review
{
    public class ReviewViewModel
    {
        public string Name { get; set; }

        public ReviewDTO Review { get; set; }
    }
}