using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositorio
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetAllCategory();
        Category GetCategoryById(int id);
        bool CrearCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        bool CategoryExist(int id);

    }
}
