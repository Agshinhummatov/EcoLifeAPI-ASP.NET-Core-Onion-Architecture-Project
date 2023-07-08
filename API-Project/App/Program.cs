using Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository.Data;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Services.Helpers;
using Services.Mappings;
using Services.Services;
using Services.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<AppUser,IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();


builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ISliderRepository, SliderRepository>();
builder.Services.AddScoped<IAdvertisingRepository, AdvertisingRepository>();
builder.Services.AddScoped<IBannerRepository, BannerRepository>();
builder.Services.AddScoped<IBenefitRepository, BenefitRepository>();
builder.Services.AddScoped<IAboutInfoRepository, AboutInfoRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IProdcutCommentRepository, ProductCommentRepository >();

builder.Services.AddScoped<IContactRepository, ContactRepository>();


builder.Services.AddScoped<IAdvertisingService, AdvertisingService>();
builder.Services.AddScoped<IBannerService, BannerService>();
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IBenefitService, BenefitService>();
builder.Services.AddScoped<IAboutInfoService, AboutInfoService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IWishlistService, WishlistService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IProdcutCommentService, ProductCommentService>();
builder.Services.AddScoped<IContactService, ContactService>();


builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));

builder.Services.AddScoped<JWTSettings>();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {

            //you can configure your custom policy
            builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

builder.Services.AddHttpContextAccessor();

//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
//builder.Services.AddHttpContextAccessor();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: "mycors",
//        policy =>
//        {
//            policy.WithOrigins("http://localhost:3000")
//            .AllowAnyHeader()
//            .AllowAnyMethod();
//        });
//});

var authBuilder = builder.Services.AddAuthentication("CookieAuth");
authBuilder.AddCookie("CookieAuth", options =>
{
    options.Cookie.Name = "CookieAuth";
});

builder.Services.AddControllers().AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(cfg =>
    {
        cfg.RequireHttpsMetadata = false;
        cfg.SaveToken = true;
        cfg.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ClockSkew = TimeSpan.Zero // remove delay of token when expire
        };
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors("mycors");
app.UseSession();

app.UseCors();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
