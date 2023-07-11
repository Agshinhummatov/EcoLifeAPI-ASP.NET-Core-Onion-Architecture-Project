using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IProductRepository :IRepository<Product>
    {
        Task<List<Product>> GetAllProductsWithCategories();

        Task<List<Product>> GetCategoryProduct(int categorId);

        Task<int> GetCategoryProductCount(int categoryId);



    }
}
