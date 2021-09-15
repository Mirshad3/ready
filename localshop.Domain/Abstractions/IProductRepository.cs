using localshop.Core.DTO;
using localshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Domain.Abstractions
{
    public interface IProductRepository : IDisposable
    {
        IEnumerable<ProductDTO> AllProducts { get; }

        IEnumerable<ProductDTO> Products { get; }

        IEnumerable<string> GetImages(string id);

        decimal GetRealPrice(ProductDTO product);

        ProductDTO FindById(string id);

        ProductDTO FindBySku(string sku);

        ProductDTO FindByMetaTitle(string metaTitle);
         

        void Save(ProductDTO product, ProductSpecificationDTO productSpecificationDTO);

        ProductDTO Delete(string id);
    }
}
