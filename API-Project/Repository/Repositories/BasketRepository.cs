using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;

using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Repository.Repositories.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Repository.Repositories
{
    public class BasketRepository : Repository<Basket>, IBasketRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _usMan;
        private readonly DbSet<Basket> _entities;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public BasketRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> usMan) : base(context)
        {
            _usMan = usMan;
            _context = context;
            _entities = _context.Set<Basket>();
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddBasketAsync(int id)
        {

            var user = _httpContextAccessor.HttpContext.User;

            if (user == null) throw new NullReferenceException();

            var userId = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (userId == null) throw new NullReferenceException();


            var basket = await _entities
                .Include(m => m.BasketProducts)
                .FirstOrDefaultAsync(m => m.AppUserId == userId);

            if (basket == null)
            {
                basket = new Basket
                {
                    AppUserId = userId
                };

                await _entities.AddAsync(basket);
                await _context.SaveChangesAsync();
            }
           


            var basketProduct = basket.BasketProducts
                .FirstOrDefault(bp => bp.ProductId == id && bp.BasketId == basket.Id);

            if (basketProduct != null)
            {
                basketProduct.Quantity++;
            }
            else
            {
                basketProduct = new BasketProduct
                {
                    BasketId = basket.Id,
                    ProductId = id,
                    Quantity = 1
                };
                basket.BasketProducts.Add(basketProduct);

            }
            _context.SaveChanges();
        }


        public async Task<List<BasketProduct>> GetBasketProducts()
        {
            var user = _httpContextAccessor.HttpContext.User;

            if (user == null) throw new UnauthorizedAccessException();

            var userId = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (userId == null) throw new UnauthorizedAccessException();

            

            var basket = await _entities
                .Include(m => m.BasketProducts)
                .ThenInclude(m => m.Product)
                .FirstOrDefaultAsync(m => m.AppUserId == userId);

            var basketProducts = basket.BasketProducts;

          
            
            return basketProducts;
        }


        public async Task<int> GetBasketSingleProd(int prodId)
        {
            var user = _httpContextAccessor.HttpContext.User;

            if (user == null) throw new UnauthorizedAccessException();

            var userId = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (userId == null) throw new UnauthorizedAccessException();

            var basket = await _entities
                .Include(m => m.BasketProducts)
                .ThenInclude(m => m.Product)
                .FirstOrDefaultAsync(m => m.AppUserId == userId);
            var basketProduct = basket.BasketProducts
              .FirstOrDefault(bp => bp.ProductId == prodId && bp.BasketId == basket.Id);
            var cnt = basketProduct.Quantity;
            return cnt;
           
        }


        //public async Task<BasketProduct> GetById(int id)
        //{
        //    var user = _httpContextAccessor.HttpContext.User;

        //    if (user == null) throw new UnauthorizedAccessException();

        //    var userId = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

        //    if (userId == null) throw new UnauthorizedAccessException();

        //    var basketProduct = await _entities
        //        .Include(m => m.Product)
        //        .ThenInclude(m => m.ProductImages)
        //        .FirstOrDefaultAsync(m => m.BasketId == id && m.AppUserId == userId);

        //    return basketProduct;
        //}









        public async Task DeleteBasket(int id)
        {
            var user = _httpContextAccessor.HttpContext.User;

            if (user == null) throw new NullReferenceException();

            var userId = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (userId == null) throw new NullReferenceException();

            var basket = await _entities
            .Include(m => m.BasketProducts)
            .FirstOrDefaultAsync(m => m.AppUserId == userId);

            if (basket == null) throw new NullReferenceException();

            var basketProduct = basket.BasketProducts
            .FirstOrDefault(bp => bp.ProductId == id && bp.BasketId == basket.Id);

            if (basketProduct == null) throw new NullReferenceException();

            basket.BasketProducts.Remove(basketProduct);

            await _context.SaveChangesAsync();

            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemBasket(int id)
        {
            var user = _httpContextAccessor.HttpContext.User;

            if (user == null)
                throw new NullReferenceException();

            var userId = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (userId == null)
                throw new NullReferenceException();

            var basket = await _entities
                .Include(m => m.BasketProducts)
                .FirstOrDefaultAsync(m => m.AppUserId == userId);

            if (basket == null)
                throw new NullReferenceException();

            var basketProduct = basket.BasketProducts
                .FirstOrDefault(bp => bp.ProductId == id && bp.BasketId == basket.Id);

            if (basketProduct == null)
                throw new NullReferenceException();

            if (basketProduct.Quantity > 1)
            {
                basketProduct.Quantity--;
            }
            else
            {
                //basket.BasketProducts.Remove(basketProduct);
            }

            await _context.SaveChangesAsync();
        }


        public async Task<int> GetBasketCount()
        {
            try
            {
                var user = _httpContextAccessor.HttpContext.User;

                if (user == null) throw new UnauthorizedAccessException();

                var userId = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

                if (userId == null) throw new UnauthorizedAccessException();

                var basket = await _entities
                  .Include(m => m.BasketProducts)
                  .ThenInclude(m => m.Product)
                  .FirstOrDefaultAsync(m => m.AppUserId == userId);

                var basketProducts = basket?.BasketProducts;

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



        public async Task<int> GetProductQuantity(int productId)
        {
            try
            {
                var user = _httpContextAccessor.HttpContext.User;

                if (user == null) throw new UnauthorizedAccessException();

                var userId = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

                if (userId == null) throw new UnauthorizedAccessException();

                var basket = await _entities
                    .Include(m => m.BasketProducts)
                    .ThenInclude(m => m.Product)
                    .FirstOrDefaultAsync(m => m.AppUserId == userId);

                var product = basket?.BasketProducts.FirstOrDefault(m => m.ProductId == productId);

                if (product == null)
                {
                    // İstediğiniz ProductId'ye sahip bir ürün bulunamadı.
                    // Burada bir hata yönetimi yapabilirsiniz.
                    return 0;
                }

                var productQuantity = product.Quantity;

                return productQuantity;
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
