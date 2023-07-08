using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Product> _entities;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
            _entities = _context.Set<Product>();
        }

        public async Task<List<Product>> GetAllProductsWithCategories()
        {
            var products = await _entities
                .Where(m => m.SoftDelete == false)
                .Include(m => m.Category)
                .ToListAsync();
            return products;
        }


    }
}
