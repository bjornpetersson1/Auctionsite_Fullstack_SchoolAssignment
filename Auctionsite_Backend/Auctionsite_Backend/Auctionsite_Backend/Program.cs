using Auctionsite_Backend.Core.Interface;
using Auctionsite_Backend.Core.Service;
using Auctionsite_Backend.Data;
using Auctionsite_Backend.Data.Interface;
using Auctionsite_Backend.Data.Repo;
using Auctionsite_Backend.Data.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AuctionSiteDbContext>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepo, AuthRepo>();
builder.Services.AddScoped<IJWTservice, JWTservice>();
builder.Services.AddScoped<IJWTRepo, JWTrepo>();

builder.Services.AddAuthentication("Bearer")
     .AddJwtBearer(options =>
     {
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuerSigningKey = true,
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"])),
             ValidateIssuer = false,
             ValidateAudience = false
         };
     });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AuctionSiteDbContext>();
    await db.Database.MigrateAsync();
    await SeedData.SeedAsync(db);
}

app.UseRouting();
app.UseAuthentication();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
