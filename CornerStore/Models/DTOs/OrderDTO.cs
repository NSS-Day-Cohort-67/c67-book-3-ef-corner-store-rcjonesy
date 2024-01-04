using System.ComponentModel.DataAnnotations;

namespace CornerStore.Models.DTOs;

public class OrderDTO
{
    public int Id { get; set; }
    public int CashierId { get; set; }
    public DateTime? PaidOnDate { get; set; }
    public List<OrderProductDTO> OrderProducts { get; set; }
    public decimal Total
    {
        get
        {
            decimal total = 0M;

            foreach(OrderProductDTO OrderProduct in OrderProducts)
            {
                total += OrderProduct.Product.Price * OrderProduct.Quantity;
            }
            return total;
        }
    }
}