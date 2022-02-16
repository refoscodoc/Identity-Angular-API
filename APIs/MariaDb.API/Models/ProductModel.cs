using System.ComponentModel.DataAnnotations;

namespace MariaDb.API.Models;

public class ProductModel
{
    [Key]
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public string Manufacturer { get; set; }
    public ushort Category { get; set; }
    public ushort Quantity { get; set; }
}