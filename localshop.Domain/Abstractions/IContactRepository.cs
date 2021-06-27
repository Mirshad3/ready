using localshop.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Domain.Abstractions
{
    public interface IContactRepository
    {
        IEnumerable<ContactDTO> Contacts { get; }

        ContactDTO FindById(int contactId);

        ContactDTO Save(ContactDTO contactDTO);

        bool Delete(int contactId);

        void SetRead(int contactId);
    }
}
