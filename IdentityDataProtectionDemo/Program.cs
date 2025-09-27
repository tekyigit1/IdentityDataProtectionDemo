using Microsoft.Extensions.DependencyInjection;

using IdentityDataProtectionDemo.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// SQLite
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseSqlite(builder.Configuration.GetConnectionString("Default")));

// Parola hash’lemek için Identity’nin PasswordHasher’ýný kullanacaðýz
builder.Services.AddScoped<Microsoft.AspNetCore.Identity.PasswordHasher<IdentityDataProtectionDemo.Models.User>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
