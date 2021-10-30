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
    public class CategoryRepository : ICategoryRepository
    {
        private IMapper _mapper;
        private ApplicationDbContext _context;

        public CategoryRepository(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public IEnumerable<CategoryDTO> Categories
        {
            get
            {
                return _context.Categories.AsEnumerable().Select(c => _mapper.Map<Category, CategoryDTO>(c));
            }
        }

        public string GetId(string categoryName)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Name == categoryName);
            if (category == null)
            {
                return null;
            }

            return category.Id;
        }

        public string GetCategory(string categoryId)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == categoryId);
            if (category == null)
            {
                return null;
            }

            return category.Name;
        }

        public int CountProduct(string id)
        {
            return _context.Products.Count(p => p.CategoryId == id);
        }
        public int InactiveProduct(string id,bool isActive)
        {
            var product = _context.Products.Where(p => p.CategoryId == id);
            if (product != null) {
                foreach (var item in product)
                {
                    item.IsActive = isActive;
                }
                _context.SaveChanges();
            }
            return product.Count();
        }
        public CategoryDTO FindById(string id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return null;
            }

            return _mapper.Map<Category, CategoryDTO>(category);
        }

        public CategoryDTO Delete(string id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return null;
            }

            var categoryDTO = _mapper.Map<Category, CategoryDTO>(category);

            // Set null
            var products = _context.Products.Where(p => p.CategoryId == id);
            foreach (var product in products)
            {
                product.CategoryId = null;
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return categoryDTO;
        }

        public bool Save(CategoryDTO categoryDTO)
        {
            if (string.IsNullOrWhiteSpace(categoryDTO.Id))
            {
                var category = _context.Categories.FirstOrDefault(c => c.Name == categoryDTO.Name);
                if (category != null)
                {
                    return false;
                }

                var newCategory = _mapper.Map<CategoryDTO, Category>(categoryDTO);
                newCategory.Id = NewId.Next().ToString();

                _context.Categories.Add(newCategory);
            }
            else
            {
                var editedCategory = _context.Categories.FirstOrDefault(c => c.Id == categoryDTO.Id);
                if (editedCategory == null)
                {
                    return false;
                }

                var checkName = _context.Categories.FirstOrDefault(c => c.Id != editedCategory.Id && c.Name == categoryDTO.Name);
                if (checkName != null)
                {
                    return false;
                }

                editedCategory = _mapper.Map(categoryDTO, editedCategory);
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
