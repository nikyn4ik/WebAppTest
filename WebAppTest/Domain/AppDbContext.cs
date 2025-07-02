using Microsoft.EntityFrameworkCore;
using WebAppTest.Models;

namespace WebAppTest.Domain;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Orders> Orders { get; set; }
}