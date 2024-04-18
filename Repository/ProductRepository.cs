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
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db){_db = db;}

        public bool UpdateProduct(Product product)
        {
            _db.Update(product);
            return Guardar();
        }

        public bool CrearProduct(Product product)
        {
            _db.Add(product);
            return Guardar();
        }

        public bool DeleteProduct(Product product)
        {
            _db.Remove(product);
            return Guardar();
        }

        public Product GetProductById(int id)
        {
            return _db.product.FirstOrDefault(p => p.ProductId == id);               
        }

        public ICollection<Product> GetAllProducts()
        {
            return _db.product.OrderBy(p => p.ProductId).ToList();
        }

        private bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool ProductExist(int id)
        {
            bool valor = _db.product.Any(p => p.ProductId == id);
            return valor;
        }
     
    }
}
