using Microsoft.AspNetCore.Mvc;
using MyOnlineShop.Models;
using MyOnlineShop.Repositories;

[Route("SecretService")]
public class AdminController : Controller
{
	private readonly IProductRepository _repo;

	public AdminController(IProductRepository repo)
	{
		_repo = repo;
	}

	[Route("")] // მისამართი: /SecretService
	public async Task<IActionResult> Index()
	{
		var products = await _repo.GetAllAsync();
		return View(products);
	}

	[Route("Create")] // მისამართი: /SecretService/Create
	public IActionResult Create() => View();

	[HttpPost]
	[Route("Create")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(Product product)
	{
		if (ModelState.IsValid)
		{
			await _repo.AddAsync(product);
			return RedirectToAction(nameof(Index));
		}
		return View(product);
	}

	[Route("Edit/{id}")] // მისამართი: /SecretService/Edit/5
	public async Task<IActionResult> Edit(int id)
	{
		var product = await _repo.GetByIdAsync(id);
		if (product == null) return NotFound();
		return View(product);
	}

	[HttpPost]
	[Route("Edit/{id}")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(int id, Product product)
	{
		if (id != product.Id) return NotFound();
		if (ModelState.IsValid)
		{
			await _repo.UpdateAsync(product);
			return RedirectToAction(nameof(Index));
		}
		return View(product);
	}

	[Route("Delete/{id}")] // მისამართი: /SecretService/Delete/5
	public async Task<IActionResult> Delete(int id)
	{
		var product = await _repo.GetByIdAsync(id);
		if (product == null) return NotFound();
		return View(product);
	}

	[HttpPost, ActionName("Delete")]
	[Route("Delete/{id}")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(int id)
	{
		await _repo.DeleteAsync(id);
		return RedirectToAction(nameof(Index));
	}
}