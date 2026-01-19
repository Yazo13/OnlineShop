using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyOnlineShop.Data;
using MyOnlineShop.Models;

namespace MyOnlineShop.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class ProductsApiController : ControllerBase
{
	private readonly AppDbContext _context;
	public ProductsApiController(AppDbContext context) => _context = context;

	// GET: api/ProductsApi
	[HttpGet]
	public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
	{
		return await _context.Products.ToListAsync();
	}

	// GET: api/ProductsApi/5
	[HttpGet("{id}")]
	public async Task<ActionResult<Product>> GetProduct(int id)
	{
		var product = await _context.Products.FindAsync(id);
		return product == null ? NotFound() : product;
	}
}