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
    public class ContactRepository : IContactRepository
    {
        private IMapper _mapper;
        private ApplicationDbContext _context;

        public ContactRepository(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public IEnumerable<ContactDTO> Contacts
        {
            get
            {
                return _context.Contacts.AsEnumerable().Select(c => _mapper.Map<Contact, ContactDTO>(c)).ToList();
            }
        }

        public ContactDTO FindById(int contactId)
        {
            var contact = _context.Contacts.FirstOrDefault(c => c.Id == contactId);
            if (contact == null)
            {
                return null;
            }

            return _mapper.Map<Contact, ContactDTO>(contact);
        }

        public ContactDTO Save(ContactDTO contactDTO)
        {
            var contact = _mapper.Map<ContactDTO, Contact>(contactDTO);

            try
            {
                _context.Contacts.Add(contact);
                _context.SaveChanges();

                contactDTO.Id = contact.Id;
                return contactDTO;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Delete(int contactId)
        {
            var contact = _context.Contacts.FirstOrDefault(c => c.Id == contactId);
            if (contact == null)
            {
                return false;
            }

            try
            {
                _context.Contacts.Remove(contact);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SetRead(int contactId)
        {
            var contact = _context.Contacts.First(c => c.Id == contactId);
            contact.IsRead = true;
            _context.SaveChanges();
        }
    }
}
