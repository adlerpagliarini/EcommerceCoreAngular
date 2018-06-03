using EcommerceCoreAngular.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceCoreAngular.Services.Infrastructure
{
    public interface IPicture
    {
        Picture GetById(int id);

        void Insert(Picture pict);

        void Update(Picture pict);

        void Delete(int id);

        void Save();
    }
}
