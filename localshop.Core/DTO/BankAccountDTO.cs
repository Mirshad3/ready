using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Core.DTO
{
    public class BankAccountDTO
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string BankName { get; set; }
        public bool IsActive { get; set; }
        public string AccountName { get; set; }
        public string Branch { get; set; }
        public string AccountNumber { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
