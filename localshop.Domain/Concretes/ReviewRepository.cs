using AutoMapper;
using localshop.Core.DTO;
using localshop.Domain.Abstractions;
using localshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Domain.Concretes
{
    public class ReviewRepository : IReviewRepository
    {
        private IMapper _mapper;
        private ApplicationDbContext _context;

        public ReviewRepository(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public IEnumerable<ReviewDTO> Reviews
        {
            get
            {
                return _context.Reviews.AsEnumerable().Select(r => _mapper.Map<Review, ReviewDTO>(r));
            }
        }

        public IEnumerable<ReviewDTO> GetReviews(string productId)
        {
            return _context.Reviews.Where(r => r.ProductId == productId).AsEnumerable().Select(r => _mapper.Map<Review, ReviewDTO>(r));
        }

        public bool Add(ReviewDTO reviewDTO)
        {
            reviewDTO.DateAdded = DateTime.Now;
            var review = _mapper.Map<ReviewDTO, Review>(reviewDTO);

            try
            {
                _context.Reviews.Add(review);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Approve(string userId, string productId)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.UserId == userId && r.ProductId == productId);
            if (review == null)
            {
                return false;
            }

            try
            {
                review.IsApproved = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(string userId, string productId)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.UserId == userId && r.ProductId == productId);
            if (review == null)
            {
                return false;
            }

            try
            {
                _context.Reviews.Remove(review);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
