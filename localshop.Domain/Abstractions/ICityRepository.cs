using localshop.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Domain.Abstractions
{
    public interface ICityRepository : IDisposable
    {
        IEnumerable<CityDTO> Cities { get; }

        int CountProduct(string id);

        CityDTO FindById(string id);

        CityDTO Delete(string id);

        bool Save(CityDTO CityDTO);

        string GetCity(string CityId);

        string GetId(string CityName);
    }
}
