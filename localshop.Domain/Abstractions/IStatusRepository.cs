using localshop.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Domain.Abstractions
{
    public interface IStatusRepository : IDisposable
    {
        IEnumerable<StatusDTO> Statuses { get; }
        bool SetStatus(string productId, string statusName);
        string GetStatus(string statusId);
    }
}
