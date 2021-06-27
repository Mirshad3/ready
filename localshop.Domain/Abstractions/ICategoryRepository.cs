using localshop.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Domain.Abstractions
{
    public interface ICategoryRepository : IDisposable
    {
        IEnumerable<CategoryDTO> Categories { get; }

        int CountProduct(string id);

        CategoryDTO FindById(string id);

        CategoryDTO Delete(string id);

        bool Save(CategoryDTO categoryDTO);

        string GetCategory(string categoryId);
        
        string GetId(string categoryName);
    }
}
