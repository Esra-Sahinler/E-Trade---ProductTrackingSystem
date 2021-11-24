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
    public class CartManager : ICartService
    {
        ICartDal _cartDal;
        public CartManager(ICartDal cartDal)
        {
            _cartDal = cartDal;
        }

        public void Add(Cart cart)
        {
            _cartDal.Add(cart);
        }

        public void Delete(Cart cart)
        {
            _cartDal.Delete(cart);
        }

        public List<Cart> Getall()
        {
            return _cartDal.GetAll();
        }

        public List<Cart> GetAllByProduct(int id)
        {
            return _cartDal.GetAll(x => x.ProductId == id);
        }

        public List<Cart> GetAllByUser(int id)
        {
            return _cartDal.GetAll(x => x.UserId == id);
        }

        public Cart GetById(int id)
        {
            return _cartDal.Get(x => x.CartId == id);
        }

        public void Update(Cart cart)
        {
            _cartDal.Update(cart);
        }
    }
}
