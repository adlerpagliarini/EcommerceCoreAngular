using EcommerceCoreAngular.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceCoreAngular.Services.Infrastructure
{
    public interface IProduct
    {
        IEnumerable<Product> GetAll();

        Product GetById(int id);

        void Insert(Product prod);

        void Update(Product prod);

        void Delete(int id);

        int Count();

        void Save();
    }
}
