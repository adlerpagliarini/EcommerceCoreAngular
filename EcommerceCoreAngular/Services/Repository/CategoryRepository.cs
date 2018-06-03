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
    public class CategoryRepository : ICategory
    {
        private readonly MyContext _db;
        public CategoryRepository(MyContext db)
        {
            _db = db;
        }
        public int Count() => _db.Category.Count();

        public void Delete(int id)
        {
            var category = GetById(id);
            if(category!=null)
            {
                _db.Category.Remove(category);
            }
        }

        public IEnumerable<Category> GetAll()
        {
            return _db.Category.Include(c => c.Products).Select(c => c);
        }

        public Category GetById(int id)
        {
            return _db.Category.FirstOrDefault(c => c.CategoryId == id);
        }

        public void Insert(Category cat)
        {
            _db.Category.Add(cat);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Category cat)
        {
            _db.Category.Update(cat);
        }
    }
}
