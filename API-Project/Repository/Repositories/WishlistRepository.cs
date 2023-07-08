using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;

using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Repository.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class WishlistRepository : Repository<Wishlist>, IWishlistRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _usMan;
        private readonly DbSet<Wishlist> _entities;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public WishlistRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> usMan) : base(context)
        {
            _usMan = usMan;
            _context = context;
            _entities = _context.Set<Wishlist>();
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddWishlistAsync(int id)
        {

            var user = _httpContextAccessor.HttpContext.User;

            if (user == null) throw new NullReferenceException();

            var userId = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (userId == null) throw new NullReferenceException();


            var basket = await _entities
                .Include(m => m.WishlistProducts)
                .FirstOrDefaultAsync(m => m.AppUserId == userId);

            if (basket == null)
            {
                basket = new Wishlist
                {
                    AppUserId = userId
                };

                await _entities.AddAsync(basket);
                await _context.SaveChangesAsync();
            }



            var basketProduct = basket.WishlistProducts
                .FirstOrDefault(bp => bp.ProductId == id && bp.WishlistId == basket.Id);

            if (basketProduct != null)
            {
                basketProduct.Quantity++;
            }
            else
            {
                basketProduct = new WishlistProduct
                {
                    WishlistId = basket.Id,
                    ProductId = id,
                    Quantity = 1
                };
                basket.WishlistProducts.Add(basketProduct);

            }
            _context.SaveChanges();
        }


          public async Task<List<WishlistProduct>> GetWishlistProducts()
        {
            var user = _httpContextAccessor.HttpContext.User;

            if (user == null) throw new UnauthorizedAccessException();

            var userId = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (userId == null) throw new UnauthorizedAccessException();

            

            var basket = await _entities
                .Include(m => m.WishlistProducts)
                .ThenInclude(m => m.Product)
                .FirstOrDefaultAsync(m => m.AppUserId == userId);

            var basketProducts = basket.WishlistProducts;

          
            
            return basketProducts;
        }




        public async Task DeleteWishlist(int id)
        {
            var user = _httpContextAccessor.HttpContext.User;

            if (user == null) throw new NullReferenceException();

            var userId = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (userId == null) throw new NullReferenceException();

            var basket = await _entities
            .Include(m => m.WishlistProducts)
            .FirstOrDefaultAsync(m => m.AppUserId == userId);

            if (basket == null) throw new NullReferenceException();

            var basketProduct = basket.WishlistProducts
            .FirstOrDefault(bp => bp.ProductId == id && bp.WishlistId == basket.Id);

            if (basketProduct == null) throw new NullReferenceException();

            basket.WishlistProducts.Remove(basketProduct);

            await _context.SaveChangesAsync();

            await _context.SaveChangesAsync();
        }


        public async Task<int> GetWishlistCount()
        {
            try
            {
                var user = _httpContextAccessor.HttpContext.User;

                if (user == null) throw new UnauthorizedAccessException();

                var userId = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

                if (userId == null) throw new UnauthorizedAccessException();

                var basket = await _entities
                  .Include(m => m.WishlistProducts)
                  .ThenInclude(m => m.Product)
                  .FirstOrDefaultAsync(m => m.AppUserId == userId);

                var basketProducts = basket?.WishlistProducts;

                var uniqeProducts = basketProducts?.GroupBy(m => m.Id)
                    .Select(m => m.First())
                    .ToList();

                var uniqueProductCount = uniqeProducts?.Count() ?? 0;
                return uniqueProductCount;
            }
            catch (UnauthorizedAccessException)
            {
                // Kullanıcı yetkilendirme hatası durumunda yapılacak işlemler
                return 0;
            }
            catch (Exception ex)
            {
                // Diğer hatalar için yapılacak işlemler
                // Örneğin, hata loglama, istemciye hata mesajı dönme, vb.
                throw ex;
            }
        }





    }
}
