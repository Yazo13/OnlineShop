using Microsoft.EntityFrameworkCore;
using MyOnlineShop.Models;

namespace MyOnlineShop.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	public DbSet<Product> Products { get; set; }
	public DbSet<CartItem> CartItems { get; set; }
	public DbSet<Order> Orders { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Product>()
			.Property(p => p.Price)
			.HasColumnType("decimal(18,2)");

		modelBuilder.Entity<CartItem>()
			.Property(c => c.Price)
			.HasColumnType("decimal(18,2)");
	}
}