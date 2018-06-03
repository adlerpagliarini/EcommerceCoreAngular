using EcommerceCoreAngular.DataContext;
using EcommerceCoreAngular.Models;
using EcommerceCoreAngular.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceCoreAngular.Services.Repository
{
    public class SubCategoryRepository : ISubCategory
    {
        private readonly MyContext _db;
        public SubCategoryRepository(MyContext db)
        {
            _db = db;
        }
        public int Count() => _db.SubCategory.Count();

        public void Delete(int id)
        {
            var subCategory = GetById(id);
            if(subCategory!=null)
            {
                _db.SubCategory.Remove(subCategory);
            }
        }

        public IEnumerable<SubCategory> GetAll()
        {
            return _db.SubCategory.Select(c => c);
        }

        public SubCategory GetById(int id)
        {
            return _db.SubCategory.FirstOrDefault(c => c.SubCategoryId == id);
        }

        public void Insert(SubCategory subCat)
        {
            _db.SubCategory.Add(subCat);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(SubCategory subCat)
        {
            _db.SubCategory.Update(subCat);
        }
    }
}
