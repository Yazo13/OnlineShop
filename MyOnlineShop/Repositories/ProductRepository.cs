using Microsoft.EntityFrameworkCore;
using MyOnlineShop.Data;
using MyOnlineShop.Models;

namespace MyOnlineShop.Repositories;

public class ProductRepository : IProductRepository
{
	private readonly AppDbContext _context;
	public ProductRepository(AppDbContext context) => _context = context;

	public async Task<IEnumerable<Product>> GetAllAsync() => await _context.Products.ToListAsync();
	public async Task<Product> GetByIdAsync(int id) => await _context.Products.FindAsync(id);
	public async Task AddAsync(Product product) { _context.Products.Add(product); await _context.SaveChangesAsync(); }
	public async Task UpdateAsync(Product product) { _context.Entry(product).State = EntityState.Modified; await _context.SaveChangesAsync(); }
	public async Task DeleteAsync(int id)
	{
		var p = await _context.Products.FindAsync(id);
		if (p != null) { _context.Products.Remove(p); await _context.SaveChangesAsync(); }
	}
}