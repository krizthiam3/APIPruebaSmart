using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositorio
{
    public interface IProductRepository
    {
        ICollection<Product> GetAllProducts();
        Product GetProductById(int id);
        bool CrearProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(Product product);
        bool ProductExist(int id);

    }
}
