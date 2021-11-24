using Business.Abstract;
using DataAccess.Abstract;
using ProductTrackingSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ContactManager : IContactService
    {
        IContactDal _contactDal;
        public ContactManager(IContactDal contactDal)
        {
            _contactDal = contactDal;
        }
        public void Add(Contact contact)
        {
            _contactDal.Add(contact);
        }

        public void Delete(Contact contact)
        {
            _contactDal.Delete(contact);
        }

        public List<Contact> GetAll()
        {
            return _contactDal.GetAll();
        }

        public List<Contact> GetAllSearch(string p)
        {
            return _contactDal.GetAll(x => x.Subject.Contains(p));
        }

        public Contact GetById(int contactId)
        {
            return _contactDal.Get(x => x.ContactId == contactId);
        }

        public void Update(Contact contact)
        {
            _contactDal.Update(contact);
        }
    }
}
