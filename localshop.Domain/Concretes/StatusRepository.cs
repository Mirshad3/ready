using AutoMapper;
using localshop.Core.DTO;
using localshop.Domain.Abstractions;
using localshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Domain.Concretes
{
    public class StatusRepository : IStatusRepository
    {
        private IMapper _mapper;
        private ApplicationDbContext _context;

        public StatusRepository(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public IEnumerable<StatusDTO> Statuses
        {
            get
            {
                return _context.Statuses.AsEnumerable().Select(s => _mapper.Map<Status, StatusDTO>(s));
            }
        }

        public bool SetStatus(string productId, string statusName)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            var statusIds = _context.Statuses.Where(s => s.Name == statusName).Select(s => s.Id);

            if (product != null && statusIds.Count() > 0)
            {
                product.StatusId = statusIds.First();
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public string GetStatus(string statusId)
        {
            var status = _context.Statuses.FirstOrDefault(s => s.Id == statusId);
            if (status == null)
            {
                return null;
            }

            return status.Name;
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
