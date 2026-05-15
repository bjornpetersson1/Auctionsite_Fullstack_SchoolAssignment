using Auctionsite_Backend.Core.Interface;
using Auctionsite_Backend.Core.Service;
using Auctionsite_Backend.Data;
using Auctionsite_Backend.Data.Interface;
using Auctionsite_Backend.Data.Repo;
using Auctionsite_Backend.Data.Seeders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AuctionSiteDbContext>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepo, AuthRepo>();
builder.Services.AddScoped<IJWTservice, JWTservice>();
builder.Services.AddScoped<IJWTRepo, JWTrepo>();
builder.Services.AddScoped<IAuctionsService, AuctionsService>();
builder.Services.AddScoped<IAuctionsRepo, AuctionRepo>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAdminRepo, AdminRepo>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAny", policy =>
    {
        policy.WithOrigins("https://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

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
         options.Events = new JwtBearerEvents
         {
             OnMessageReceived = ctx =>
             {
                 ctx.Token = ctx.Request.Cookies["accessToken"];
                 return Task.CompletedTask;
             }
         };
     });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
    options.AddPolicy("UserOrAdmin", policy => policy.RequireRole("admin", "user"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("user"));
});

var app = builder.Build();

app.UseCors("AllowAny");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AuctionSiteDbContext>();
    await db.Database.MigrateAsync();
    await SeedData.SeedAsync(db);
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
