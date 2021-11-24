using ProductTrackingSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IContactService
    {
        List<Contact> GetAllSearch(string p);
        List<Contact> GetAll();
        void Add(Contact contact);
        Contact GetById(int contactId);
        void Delete(Contact contact);
        void Update(Contact contact);
    }
}
