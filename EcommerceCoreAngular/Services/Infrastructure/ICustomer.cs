using EcommerceCoreAngular.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceCoreAngular.Services.Infrastructure
{
    public interface ICustomer
    {
        IEnumerable<Customer> GetAll();

        Customer GetById(int id);

        void Insert(Customer cat);

        void Update(Customer cat);

        void Delete(int id);

        int Count();

        void Save();
    }
}
