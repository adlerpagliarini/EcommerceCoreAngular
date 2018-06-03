using EcommerceCoreAngular.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceCoreAngular.Models;
using EcommerceCoreAngular.DataContext;

namespace EcommerceCoreAngular.Services.Repository
{
    public class CartItemRepository : ICartItem
    {
        private readonly MyContext _db;

        public CartItemRepository(MyContext db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.CartItem.Count();
        }

        public void Delete(int id)
        {
            var cartItem = GetById(id);
            if (cartItem != null)
            {
                _db.CartItem.Remove(cartItem);
            }
        }

        public IEnumerable<CartItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public CartItem GetById(int id)
        {
            return _db.CartItem.FirstOrDefault(c => c.CartId == id);
        }

        public void Insert(CartItem cartIt)
        {
            _db.CartItem.Add(cartIt);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(CartItem cartIt)
        {
            _db.CartItem.Update(cartIt);
        }
    }
}
