using EcommerceCoreAngular.DataContext;
using EcommerceCoreAngular.Models;
using EcommerceCoreAngular.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceCoreAngular.Services.Repository
{
    public class PictureRepository:IPicture
    {
        private readonly MyContext _db;

        public PictureRepository(MyContext db)
        {
            _db = db;
        }

        public void Delete(int id)
        {
            var picture = GetById(id);
            if (picture != null)
            {
                _db.Picture.Remove(picture);
            }
        }

        public Picture GetById(int id)
        {
            return _db.Picture.FirstOrDefault(p => p.PictureId == id);
        }

        public void Insert(Picture pict)
        {
            _db.Picture.Add(pict);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Picture pict)
        {
            _db.Picture.Update(pict);
        }
    }
}
