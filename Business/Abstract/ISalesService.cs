using ProductTrackingSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISalesService
    {
        void Add(Sales sales);
        void Delete(Sales sales);
        void Update(Sales sales);
        Sales GetById(int id);
        List<Sales> GetAll();
        List<Sales> GetAllByUser(int id);
        List<Sales> GetAllByProduct(int id);
        List<Sales> GetAllByCart(int id);
    }
}
