using System;
using System.ComponentModel.DataAnnotations;

public class Products
{
	[Key]
	public int ProdId { get; set; }
	public string ProductName { get; set; }
	public string ProductDescr { get; set; }

	public decimal Price { get; set; }
}
