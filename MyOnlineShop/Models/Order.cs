using System.ComponentModel.DataAnnotations;

namespace MyOnlineShop.Models;

public class Order
{
	public int Id { get; set; }
	[Required(ErrorMessage = "სახელი სავალდებულოა")] 
	public string CustomerName { get; set; } = string.Empty;
	[Required]
	public string Address { get; set; } = string.Empty;
	[Phone]
	public string Phone { get; set; } = string.Empty;
	public decimal TotalAmount { get; set; }
	public DateTime OrderDate { get; set; } = DateTime.Now;
}