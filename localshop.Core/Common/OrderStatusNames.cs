using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Core.Common
{
    public static class OrderStatusNames
    {
        public const string Pending = "Pending";
        public const string Approved = "Approved";
        public const string ReadyToShip = "ReadyToShip"; 
        public const string Picked = "Picked";
        public const string Delivered = "Delivered";
        public const string Cancelled = "Cancelled";
    }
}
