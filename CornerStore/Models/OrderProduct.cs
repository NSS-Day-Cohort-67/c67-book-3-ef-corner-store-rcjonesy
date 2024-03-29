using System.ComponentModel.DataAnnotations;

namespace CornerStore.Models;

public class OrderProduct
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    // what do we need to add here to GET the product that we bought?
    public Product Product { get; set; }
    public int OrderId { get; set; }
    public int Quantity { get; set; }
}