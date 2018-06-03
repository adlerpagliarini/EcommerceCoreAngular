using EcommerceCoreAngular.DataContext;
using EcommerceCoreAngular.Models;
using EcommerceCoreAngular.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceCoreAngular.Services.Repository
{
    public class ProductRepository : IProduct
    {
        private readonly MyContext _db;
        public ProductRepository(MyContext db)
        {
            _db = db;
        }
        public int Count()
        {
            return _db.Product.Count();
        }

        public void Delete(int id)
        {
            var product = GetById(id);

            if(product != null)
            {
                _db.Product.Remove(product);
            }
        }

        public IEnumerable<Product> GetAll()
        {
            return _db.Product.Include(c => c.Categories).Select(p => p);
        }

        public Product GetById(int id)
        {
            return _db.Product.FirstOrDefault(p => p.ProductId == id);
        }

        public void Insert(Product prod)
        {
            _db.Product.Add(prod);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Product prod)
        {
            //_db.Product.Update(prod);
            _db.Entry(prod).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        }
    }
}
