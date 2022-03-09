using localshop.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Domain.Abstractions
{
    public interface IAccountRepository : IDisposable
    {
        IEnumerable<BankAccountDTO> accounts { get; }

        int CountProduct(string id);

        BankAccountDTO FindById(string id);

        BankAccountDTO Delete(string id);

        bool Save(BankAccountDTO BankAccountDTO);

        string GetAccount(string AccountId);

        string GetId(string BankAccountName);
    }
}
