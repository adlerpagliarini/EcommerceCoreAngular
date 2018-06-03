using EcommerceCoreAngular.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceCoreAngular.Services.Infrastructure
{
    public interface ISubCategory
    {
        IEnumerable<SubCategory> GetAll();

        SubCategory GetById(int id);

        void Insert(SubCategory subCat);

        void Update(SubCategory subCat);

        void Delete(int id);

        int Count();

        void Save();
    }
}
