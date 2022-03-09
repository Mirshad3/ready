using AutoMapper;
using localshop.Core.DTO;
using localshop.Domain.Abstractions;
using localshop.Domain.Entities;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Domain.Concretes
{
    public class AccountRepository : IAccountRepository
    {
        private IMapper _mapper;
        private ApplicationDbContext _context;

        public AccountRepository(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public IEnumerable<BankAccountDTO> accounts
        {
            get
            {
                return _context.BankAccounts.AsEnumerable().Select(c => _mapper.Map<BankAccount, BankAccountDTO>(c));
                
            }
        }

        public string GetId(string AccountName)
        {
            var Account = _context.Cities.FirstOrDefault(c => c.Name == AccountName);
            if (Account == null)
            {
                return null;
            }

            return Account.Id;
        }

        public string GetAccount(string AccountId)
        {
            var Account = _context.Cities.FirstOrDefault(c => c.Id == AccountId);
            if (Account == null)
            {
                return null;
            }

            return Account.Name;
        }

        public int CountProduct(string id)
        {
            return _context.Products.Count(p => p.Id == id);
        }

        public BankAccountDTO FindById(string id)
        {
            var Account = _context.BankAccounts.Where(c => c.UserId == id).FirstOrDefault();
            if (Account == null)
            {
                return null;
            }

            return _mapper.Map<BankAccount, BankAccountDTO>(Account);
        }

        public BankAccountDTO Delete(string id)
        {
            var Account = _context.BankAccounts.FirstOrDefault(c => c.Id == id);

            if (Account == null)
            {
                return null;
            }

            var AccountDTO = _mapper.Map<BankAccount, BankAccountDTO>(Account);

            // Set null
            var products = _context.Products.Where(p => p.Id == id);
            foreach (var product in products)
            {
                product.Id = null;
            }

            _context.BankAccounts.Remove(Account);
            _context.SaveChanges();

            return AccountDTO;
        }

        public bool Save(BankAccountDTO AccountDTOs)
        {
            if (string.IsNullOrWhiteSpace(AccountDTOs.Id))
            {
                var Accounts = _context.BankAccounts.Where(m=>m.AccountNumber == AccountDTOs.AccountNumber).FirstOrDefault();
                if (Accounts != null)
                {
                    return false;
                }

                var newAccount = _mapper.Map<BankAccountDTO, BankAccount>(AccountDTOs);
                newAccount.Id = NewId.Next().ToString();

                _context.BankAccounts.Add(newAccount);
            }
            else
            {
                var editedAccount = _context.Cities.FirstOrDefault(c => c.Id == AccountDTOs.Id);
                if (editedAccount == null)
                {
                    return false;
                }

                var checkName = _context.BankAccounts.FirstOrDefault(c => c.Id != editedAccount.Id);
                if (checkName != null)
                {
                    return false;
                }

                editedAccount = _mapper.Map(AccountDTOs, editedAccount);
            }

            _context.SaveChanges();
            return true;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_context != null)
                    {
                        _context.Dispose();
                        _context = null;
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
