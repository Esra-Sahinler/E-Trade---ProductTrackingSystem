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
    public class SalesManager : ISalesService
    {
        ISalesDal _salesDal;

        public SalesManager(ISalesDal salesDal)
        {
            _salesDal = salesDal;
        }

        public void Add(Sales sales)
        {
            _salesDal.Add(sales);
        }

        public void Delete(Sales sales)
        {
            _salesDal.Delete(sales);
        }

        public List<Sales> GetAll()
        {
            return _salesDal.GetAll();
        }

        public List<Sales> GetAllByCart(int id)
        {
            return _salesDal.GetAll(x => x.CartId == id);
        }

        public List<Sales> GetAllByProduct(int id)
        {
            return _salesDal.GetAll(x => x.ProductId == id);
        }

        public List<Sales> GetAllByUser(int id)
        {
            return _salesDal.GetAll(x => x.UserId == id);
        }

        public Sales GetById(int id)
        {
            return _salesDal.Get(x => x.SalesId == id);
        }

        public void Update(Sales sales)
        {
            _salesDal.Update(sales);
        }
    }
}
