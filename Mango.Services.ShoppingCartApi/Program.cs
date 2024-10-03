using Mango.Services.ShoppingCartApi.Data;
using Mango.Services.ShoppingCartApi.IService;
using Mango.Services.ShoppingCartApi.Profiles;
using Mango.Services.ShoppingCartApi.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(CartProfile));
builder.Services.AddHttpClient("Product", h => h.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductApi"]));
builder.Services.AddHttpClient("Coupon", h => h.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CouponApi"]));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter Bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = JwtBearerDefaults.AuthenticationScheme
            },
            In = ParameterLocation.Header,
            Name = "Authorization",
            Scheme = JwtBearerDefaults.AuthenticationScheme
        },
        new string[]{}
    }
    });
});


var secret = builder.Configuration.GetValue<string>("ApiSettings:Secret");
var key = Encoding.ASCII.GetBytes(secret);
var issuer = builder.Configuration.GetValue<string>("ApiSettings:Issuer");
var audince = builder.Configuration.GetValue<string>("ApiSettings:Audience");

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuerSigningKey = true,
        ValidAudience = audince,
        ValidIssuer = issuer
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
await ApplyMigration();

app.Run();


async Task ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var migrations = await _db.Database.GetPendingMigrationsAsync();
        if (migrations.Count() > 0)
        {
            await _db.Database.MigrateAsync();
        }
    }
}
