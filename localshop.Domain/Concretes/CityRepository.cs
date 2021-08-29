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
    public class CityRepository : ICityRepository
    {
        private IMapper _mapper;
        private ApplicationDbContext _context;

        public CityRepository(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public IEnumerable<CityDTO> Cities
        {
            get
            {
                return _context.Cities.AsEnumerable().Select(c => _mapper.Map<City, CityDTO>(c));
                
            }
        }

        public string GetId(string CityName)
        {
            var City = _context.Cities.FirstOrDefault(c => c.Name == CityName);
            if (City == null)
            {
                return null;
            }

            return City.Id;
        }

        public string GetCity(string CityId)
        {
            var City = _context.Cities.FirstOrDefault(c => c.Id == CityId);
            if (City == null)
            {
                return null;
            }

            return City.Name;
        }

        public int CountProduct(string id)
        {
            return _context.Products.Count(p => p.Id == id);
        }

        public CityDTO FindById(string id)
        {
            var City = _context.Cities.FirstOrDefault(c => c.Id == id);
            if (City == null)
            {
                return null;
            }

            return _mapper.Map<City, CityDTO>(City);
        }

        public CityDTO Delete(string id)
        {
            var City = _context.Cities.FirstOrDefault(c => c.Id == id);

            if (City == null)
            {
                return null;
            }

            var CityDTO = _mapper.Map<City, CityDTO>(City);

            // Set null
            var products = _context.Products.Where(p => p.Id == id);
            foreach (var product in products)
            {
                product.Id = null;
            }

            _context.Cities.Remove(City);
            _context.SaveChanges();

            return CityDTO;
        }

        public bool Save(CityDTO CityDTOs)
        {
            if (string.IsNullOrWhiteSpace(CityDTOs.Id))
            {
                var Citys = _context.Cities.FirstOrDefault(c => c.Name == CityDTOs.Name);
                if (Citys != null)
                {
                    return false;
                }

                var newCity = _mapper.Map<CityDTO, City>(CityDTOs);
                newCity.Id = NewId.Next().ToString();

                _context.Cities.Add(newCity);
            }
            else
            {
                var editedCity = _context.Cities.FirstOrDefault(c => c.Id == CityDTOs.Id);
                if (editedCity == null)
                {
                    return false;
                }

                var checkName = _context.Cities.FirstOrDefault(c => c.Id != editedCity.Id && c.Name == CityDTOs.Name);
                if (checkName != null)
                {
                    return false;
                }

                editedCity = _mapper.Map(CityDTOs, editedCity);
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
