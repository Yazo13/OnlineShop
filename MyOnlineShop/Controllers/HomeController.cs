using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using MyOnlineShop.Models;

namespace MyOnlineShop.Controllers;

public class HomeController : Controller
{
	private readonly IHttpClientFactory _httpClientFactory;

	public HomeController(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory;
	}

	public async Task<IActionResult> Index()
	{
		var client = _httpClientFactory.CreateClient();
		var products = await client.GetFromJsonAsync<List<Product>>("http://localhost:5225/api/productsapi");
		return View(products);
	}
	public async Task<IActionResult> Details(int id)
	{
		var client = _httpClientFactory.CreateClient();
		var product = await client.GetFromJsonAsync<Product>($"http://localhost:5225/api/productsapi/{id}");

		if (product == null) return NotFound();
		return View(product);
	}
}