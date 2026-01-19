using Microsoft.EntityFrameworkCore;
using MyOnlineShop.Data;
using MyOnlineShop.Models;
using MyOnlineShop.Repositories;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	context.Database.EnsureCreated(); // ეს შექმნის ბაზას ავტომატურად

	if (!context.Products.Any())
	{
		context.Products.AddRange(
			new Product { Name = "iPhone 15", Price = 1200, Description = "Latest Apple Phone", ImageUrl = "https://placehold.co/200" },
			new Product { Name = "Samsung S24", Price = 1100, Description = "Flagship Android", ImageUrl = "https://placehold.co/200" },
			new Product { Name = "MacBook Air", Price = 1500, Description = "M3 Chip Laptop", ImageUrl = "https://placehold.co/200" }
		);
		context.SaveChanges();
	}
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
