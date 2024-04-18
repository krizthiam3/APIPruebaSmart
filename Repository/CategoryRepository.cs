using Entities;
using PruebaAPI.Data;
using Repository.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) {_db = db;}

        public bool UpdateCategory(Category category)
        {
            _db.Update(category);
            return Guardar();
        }

        public bool CrearCategory(Category category)
        {
            _db.Add(category);
            return Guardar();
        }

        public bool DeleteCategory(Category category)
        {
            _db.Remove(category);
            return Guardar();
        }

        public Category GetCategoryById(int id)
        {
            return _db.category.FirstOrDefault(c => c.CategoryId == id);               
        }

        public ICollection<Category> GetAllCategory()
        {
            return _db.category.OrderBy(c => c.CategoryId).ToList();
        }

        private bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool CategoryExist(int id)
        {
            bool valor = _db.category.Any(p => p.CategoryId == id);
            return valor;
        }

      
    }
}
