using ProductTrackingSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICartService
    {
        void Add(Cart cart);
        void Delete(Cart cart);
        void Update(Cart cart);
        List<Cart> Getall();
        Cart GetById(int id);
        List<Cart> GetAllByUser(int id);
        List<Cart> GetAllByProduct(int id);
    }
}
