﻿using localshop.Core.DTO;
using localshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localshop.Areas.Admin.ViewModels
{
    public class ListUserViewModel
    {
        public ApplicationUser User { get; set; }
        public IList<string> Roles { get; set; }
    }
    public class ListUserBankViewModel
    {
        public ApplicationUser User { get; set; }
        public IList<string> Roles { get; set; }
        public BankAccountDTO bankAccounts { get; set; }
    }
}