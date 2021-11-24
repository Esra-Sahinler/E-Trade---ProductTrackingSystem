using DataAccess.Abstract;
using ProductTrackingSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Repositories
{
    public class ContactRepository : GenericRepository<Contact>, IContactDal
    {
    }
}
