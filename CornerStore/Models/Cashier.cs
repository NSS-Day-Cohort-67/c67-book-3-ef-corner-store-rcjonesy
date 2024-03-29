using System.ComponentModel.DataAnnotations;

namespace CornerStore.Models;

public class Cashier
{
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }

    // a cashier can have MANY orders: MANY = List
    public List<Order> Orders { get; set; } // Order.CashierId == Id

    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }
}