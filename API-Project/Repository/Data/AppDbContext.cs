using Domain.Common;
using Domain.Configurations;
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Repository.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //public DbSet<Employee> Employees { get; set; }
        //public DbSet<Country> Countries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            modelBuilder.ApplyConfiguration(new SliderConfiguration());

            modelBuilder.ApplyConfiguration(new AdvertisingConfiguration());

            modelBuilder.ApplyConfiguration(new BannerConfiguration());

            modelBuilder.ApplyConfiguration(new BenefitConfiguration());

            modelBuilder.ApplyConfiguration(new AboutInfoConfiguration());

            modelBuilder.ApplyConfiguration(new ProductConfiguration());


            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BasketProductConfiguration());
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BasketConfiguration());
            base.OnModelCreating(modelBuilder);


            modelBuilder.ApplyConfiguration(new WishlistConfiguration());
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new WishlistProductConfiguration());
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BlogConfiguration());
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProdcutCommentConfiguration());
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Slider>().HasQueryFilter(m => !m.SoftDelete);

            modelBuilder.Entity<Benefit>().HasQueryFilter(m => !m.SoftDelete);

            modelBuilder.Entity<Advertising>().HasQueryFilter(m => !m.SoftDelete);

            modelBuilder.Entity<Banner>().HasQueryFilter(m => !m.SoftDelete);

            modelBuilder.Entity<Blog>().HasQueryFilter(m => !m.SoftDelete);

            modelBuilder.Entity<Category>().HasQueryFilter(m => !m.SoftDelete);

            modelBuilder.Entity<AboutInfo>().HasQueryFilter(m => !m.SoftDelete);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            base.OnModelCreating(modelBuilder);
        }



        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entity in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        entity.Entity.CreatedAt = DateTime.UtcNow.AddHours(4);
                        break;
                    case EntityState.Modified:
                        entity.Entity.UpdateDate = DateTime.UtcNow.AddHours(4);
                        break;
                    case EntityState.Deleted:
                        entity.Entity.SoftDelete = true;
                        break;
                    default:
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
