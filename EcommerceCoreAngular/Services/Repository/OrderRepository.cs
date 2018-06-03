using EcommerceCoreAngular.DataContext;
using EcommerceCoreAngular.Models;
using EcommerceCoreAngular.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceCoreAngular.Services.Repository
{
    public class OrderRepository:IOrder
    {
        private readonly MyContext _db;

        public OrderRepository(MyContext db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.Order.Count();
        }

        public void Delete(int id)
        {
            var order = GetById(id);
            if (order != null)
            {
                _db.Order.Remove(order);
            }
        }

        public IEnumerable<Order> GetAll()
        {
            return _db.Order.Select(o => o);
        }

        public Order GetById(int id)
        {
            return _db.Order.FirstOrDefault(o => o.OrderId == id);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
