using localshop.Core.DTO;
using localshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Domain.Abstractions
{
    public interface IReturnCashRepository
    {
        IList<ReturnCashDTO> ReturnCashs { get; }

        ReturnCashDTO FindById(string orderId);

        IList<ReturnCashDTO> GetReturnCashs(string userId);

        IList<ReturnCashDTO> GetReturnCashsByOwner(string userId);
        string GetReturnCashStatus(string ReturnCashStatusId);

        string GetPaymentMethod(string paymentMethodId);         

        string AddPaymentMethod(ReturnCashDTO ReturnCashDTO, string paymentMethod);

        string UpdateStatus(ReturnCashDTO ReturnCashDTO, string statusName);
        
        string AddPaymentMethod(string ReturnCashId, string paymentMethod);

        string UpdateStatus(string ReturnCashId, string statusName);

        ReturnCashDTO Save(ReturnCashDTO ReturnCash); 
    }
}
