using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IWishlistRepository : IRepository<Wishlist>
    {
        Task AddWishlistAsync(int id);
        Task<List<WishlistProduct>> GetWishlistProducts();
        Task DeleteWishlist(int id);
        Task<int> GetWishlistCount();
    }
}
