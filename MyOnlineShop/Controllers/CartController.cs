using Microsoft.AspNetCore.Mvc;
using MyOnlineShop.Data;
using MyOnlineShop.Models;

namespace MyOnlineShop.Controllers;

public class CartController : Controller
{
	private readonly AppDbContext _context;
	public CartController(AppDbContext context) => _context = context;

	public IActionResult Index()
	{
		var cartItems = _context.CartItems.ToList();
		return View(cartItems);
	}

	[HttpPost]
	public async Task<IActionResult> Add(int productId)
	{
		var product = await _context.Products.FindAsync(productId);
		if (product != null)
		{
			_context.CartItems.Add(new CartItem
			{
				ProductId = product.Id,
				ProductName = product.Name,
				Price = product.Price,
				Quantity = 1
			});
			await _context.SaveChangesAsync();
		}
		return RedirectToAction("Index");
	}
	// რაოდენობის შეცვლა (PUT/PATCH-ის იმიტაცია MVC-ში)
	[HttpPost]
	public async Task<IActionResult> UpdateQuantity(int id, int quantity)
	{
		var item = await _context.CartItems.FindAsync(id);
		if (item != null)
		{
			item.Quantity = quantity;
			await _context.SaveChangesAsync();
		}
		return RedirectToAction("Index");
	}

	// წაშლა (DELETE)
	[HttpPost]
	public async Task<IActionResult> Delete(int id)
	{
		var item = await _context.CartItems.FindAsync(id);
		if (item != null)
		{
			_context.CartItems.Remove(item);
			await _context.SaveChangesAsync();
		}
		return RedirectToAction("Index");
	}
	public IActionResult Checkout()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Checkout(Order order)
	{
		order.OrderDate = DateTime.Now;
		var cartItems = _context.CartItems.ToList();
		order.TotalAmount = cartItems.Sum(x => x.Price * x.Quantity);

		ModelState.Remove("OrderDate");
		ModelState.Remove("TotalAmount");
		ModelState.Remove("Phone");
		if (ModelState.IsValid)
		{
			_context.Orders.Add(order);
			await _context.SaveChangesAsync();

			_context.CartItems.RemoveRange(cartItems);
			await _context.SaveChangesAsync();

			return View("OrderSuccess");
		}

		return View(order);
	}
}