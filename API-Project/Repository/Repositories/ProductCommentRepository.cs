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
    public class ProductCommentRepository : Repository<ProdcutComment>, IProdcutCommentRepository
    {

        private readonly AppDbContext _context;
        //private readonly UserManager<AppUser> _usMan;
        private readonly DbSet<ProdcutComment> _entities;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ProductCommentRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor /*UserManager<AppUser> usMan*/) : base(context)
        {
            //_usMan = usMan;
            _context = context;
            _entities = _context.Set<ProdcutComment>();
            _httpContextAccessor = httpContextAccessor;
        }



        public async Task AddCommentToProductAsync(int productId, string comment)
        {
            var user = _httpContextAccessor.HttpContext.User;

            if (user == null) throw new NullReferenceException();

            var userId = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (userId == null) throw new NullReferenceException();

            var product = await _entities.Include(p => p.Product)
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
            {
                throw new ArgumentException("Invalid product ID.");
            }

            var productComment = new ProdcutComment
            {
                Context = comment,
                AppUserId = userId,
                ProductId = productId
            };

            _entities.Add(productComment); // Doğru özellik kullanıldı

            await _context.SaveChangesAsync();
        }





    }

}
