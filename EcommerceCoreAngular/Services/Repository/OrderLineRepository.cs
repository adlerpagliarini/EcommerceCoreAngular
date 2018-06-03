using EcommerceCoreAngular.DataContext;
using EcommerceCoreAngular.Models;
using EcommerceCoreAngular.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceCoreAngular.Services.Repository
{
    public class OrderLineRepository:IOrderLine
    {
        private readonly MyContext _db;

        public OrderLineRepository(MyContext db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.OrderLine.Count();
        }

        public IEnumerable<OrderLine> GetAll()
        {
            return _db.OrderLine.Select(o => o);
        }

        public OrderLine GetById(int id)
        {
            return _db.OrderLine.FirstOrDefault(o => o.OrderLineId == id);
        }

        public void Insert(OrderLine orderLine)
        {
            _db.OrderLine.Add(orderLine);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(OrderLine orderLine)
        {
            _db.OrderLine.Update(orderLine);
        }
    }
}
