using ProductTrackingSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAllSearch(string p);
        List<Product> GetAll();
        void Add(Product product);
        Product GetById(int productId);
        void Delete(Product product);
        void Update(Product product);
    }
}
