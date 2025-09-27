using IdentityDataProtectionDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityDataProtectionDemo.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
}
