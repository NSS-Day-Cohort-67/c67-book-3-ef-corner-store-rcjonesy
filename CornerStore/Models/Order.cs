using System.ComponentModel.DataAnnotations;

namespace CornerStore.Models;

public class Order
{
    public int Id { get; set; }
    public int CashierId { get; set; }
    public Cashier Cashier { get; set; }
    public DateTime? PaidOnDate { get; set; }
    // we need to get the list corresponding to THIS order id
    public List<OrderProduct> OrderProducts { get; set; } // OrderProducts.Product.Price
    // we will have an ARRAY of order products
    // {
    //     Id
    //     ProductId
    //     Product
    //     OrderId
    //     Quantity
    // }
    public decimal Total
    {
        get
        {
            decimal total = 0M;

            if (OrderProducts != null)
            {
                foreach (OrderProduct OrderProduct in OrderProducts)
                {
                    total += OrderProduct.Product.Price * OrderProduct.Quantity;
                }
                //
            }


            // we need the price. where does the price exist? it exists in the product table. how do we get to the product table? via the order product table. 

            // "order is the starting key of C" c d e f g a b
            // "bridge key is B" b c# d# e f# g# a#
            // "final key is C# " c# d# e# f# g# a# b#

            // how do we get the product?
            // we need product id

            // how do we get the product id?
            // we get the product id from the order product table

            return total;
        }
    }
}