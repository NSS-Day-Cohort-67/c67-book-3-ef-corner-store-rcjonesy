using Microsoft.EntityFrameworkCore;
using CornerStore.Models;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
public class CornerStoreDbContext : DbContext
{
    //Each Db set corresponds to and represents a table in the database

    public DbSet<Cashier> Cashiers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<Product> Products { get; set; }

    public CornerStoreDbContext(DbContextOptions<CornerStoreDbContext> context) : base(context)
    {

    }

    //allows us to configure the schema when migrating as well as seed data
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cashier>().HasData(new Cashier[]
        {
            new Cashier { Id = 1, FirstName = "John", LastName = "Doe" },
            new Cashier { Id = 2, FirstName = "Alice", LastName = "Smith" },
            new Cashier { Id = 3, FirstName = "Bob", LastName = "Johnson" },
            new Cashier { Id = 4, FirstName = "Emily", LastName = "Williams" },
            new Cashier { Id = 5, FirstName = "Michael", LastName = "Brown" },
            new Cashier { Id = 6, FirstName = "Sophia", LastName = "Jones" },
            new Cashier { Id = 7, FirstName = "William", LastName = "Garcia" },
            new Cashier { Id = 8, FirstName = "Olivia", LastName = "Martinez" },
            new Cashier { Id = 9, FirstName = "James", LastName = "Miller" },
            new Cashier { Id = 10, FirstName = "Emma", LastName = "Davis" }
        });

        modelBuilder.Entity<Category>().HasData(new Category[]
        {
            new Category { Id = 1, CategoryName = "Snacks" },
            new Category { Id = 2, CategoryName = "Beverages" },
            new Category { Id = 3, CategoryName = "Candy" },
            new Category { Id = 4, CategoryName = "Frozen" },
            new Category { Id = 5, CategoryName = "Household" },
            new Category { Id = 6, CategoryName = "Beauty" },
            new Category { Id = 7, CategoryName = "Bakery" },
            new Category { Id = 8, CategoryName = "Dairy" },
            new Category { Id = 9, CategoryName = "Produce" },
            new Category { Id = 10, CategoryName = "Toiletries" }
        });
        modelBuilder.Entity<Product>().HasData(new Product[]
 {
            new Product { Id = 1, ProductName = "Pepsi", Price = 2.00M, Brand = "Pepsi Cola", CategoryId = 2 },
            new Product { Id = 2, ProductName = "Doritos", Price = 1.50M, Brand = "Frito-Lay", CategoryId = 1 },
            new Product { Id = 3, ProductName = "Snickers", Price = 1.20M, Brand = "Mars, Incorporated", CategoryId = 3 },
            new Product { Id = 4, ProductName = "Ice Cream", Price = 3.50M, Brand = "Ben & Jerry's", CategoryId = 4 },
            new Product { Id = 5, ProductName = "Paper Towels", Price = 2.80M, Brand = "Bounty", CategoryId = 5 },
            new Product { Id = 6, ProductName = "Shampoo", Price = 4.50M, Brand = "Pantene", CategoryId = 6 },
            new Product { Id = 7, ProductName = "Baguette", Price = 1.80M, Brand = "Local Bakery", CategoryId = 7 },
            new Product { Id = 8, ProductName = "Milk", Price = 2.20M, Brand = "Organic Valley", CategoryId = 8 },
            new Product { Id = 9, ProductName = "Apples", Price = 1.00M, Brand = "Washington Apples", CategoryId = 9 },
            new Product { Id = 10, ProductName = "Toothpaste", Price = 3.00M, Brand = "Colgate", CategoryId = 10 },
            new Product { Id = 11, ProductName = "Coca-Cola", Price = 1.80M, Brand = "The Coca-Cola Company", CategoryId = 2 },
            new Product { Id = 12, ProductName = "Lays", Price = 1.50M, Brand = "Frito-Lay", CategoryId = 1 },
            new Product { Id = 13, ProductName = "Twix", Price = 1.20M, Brand = "Mars, Incorporated", CategoryId = 3 },
            new Product { Id = 14, ProductName = "Frozen Pizza", Price = 4.50M, Brand = "DiGiorno", CategoryId = 4 },
            new Product { Id = 15, ProductName = "Soap", Price = 2.50M, Brand = "Dove", CategoryId = 6 }
 });

        modelBuilder.Entity<OrderProduct>().HasData(new OrderProduct[]
    {
            new OrderProduct { Id = 1, ProductId = 1, OrderId = 1, Quantity = 3 },
            new OrderProduct { Id = 2, ProductId = 3, OrderId = 1, Quantity = 3 },
            new OrderProduct { Id = 3, ProductId = 7, OrderId = 1, Quantity = 2 },
            new OrderProduct { Id = 4, ProductId = 8, OrderId = 1, Quantity = 4 },
            new OrderProduct { Id = 5, ProductId = 10, OrderId = 1, Quantity = 1 },
            new OrderProduct { Id = 6, ProductId = 12, OrderId = 2, Quantity = 3 },
            new OrderProduct { Id = 7, ProductId = 15, OrderId = 2, Quantity = 2 },
            new OrderProduct { Id = 8, ProductId = 9, OrderId = 3, Quantity = 6 },
            new OrderProduct { Id = 9, ProductId = 4, OrderId = 3, Quantity = 1 },
            new OrderProduct { Id = 10, ProductId = 11, OrderId = 3, Quantity = 4 },
            new OrderProduct { Id = 11, ProductId = 6, OrderId = 4, Quantity = 2 },
            new OrderProduct { Id = 12, ProductId = 13, OrderId = 4, Quantity = 3 },
            new OrderProduct { Id = 13, ProductId = 14, OrderId = 4, Quantity = 1 },
            new OrderProduct { Id = 14, ProductId = 2, OrderId = 5, Quantity = 2 },
            new OrderProduct { Id = 15, ProductId = 3, OrderId = 5, Quantity = 3 },
            new OrderProduct { Id = 16, ProductId = 5, OrderId = 6, Quantity = 4 },
            new OrderProduct { Id = 17, ProductId = 10, OrderId = 6, Quantity = 1 },
            new OrderProduct { Id = 18, ProductId = 12, OrderId = 7, Quantity = 2 },
            new OrderProduct { Id = 19, ProductId = 14, OrderId = 7, Quantity = 3 },
            new OrderProduct { Id = 20, ProductId = 15, OrderId = 7, Quantity = 1 },
            new OrderProduct { Id = 21, ProductId = 5, OrderId = 8, Quantity = 2 },
            new OrderProduct { Id = 22, ProductId = 7, OrderId = 20, Quantity = 3 },
            new OrderProduct { Id = 23, ProductId = 8, OrderId = 20, Quantity = 1 },
            new OrderProduct { Id = 24, ProductId = 10, OrderId = 19, Quantity = 4 },
            new OrderProduct { Id = 25, ProductId = 12, OrderId = 8, Quantity = 2 },
            new OrderProduct { Id = 26, ProductId = 15, OrderId = 19, Quantity = 1 },
            new OrderProduct { Id = 27, ProductId = 9, OrderId = 18, Quantity = 3 },
            new OrderProduct { Id = 28, ProductId = 4, OrderId = 18, Quantity = 2 },
            new OrderProduct { Id = 29, ProductId = 11, OrderId = 17, Quantity = 5 },
            new OrderProduct { Id = 30, ProductId = 6, OrderId = 17, Quantity = 1 },
            new OrderProduct { Id = 31, ProductId = 13, OrderId = 16, Quantity = 2 },
            new OrderProduct { Id = 32, ProductId = 14, OrderId = 16, Quantity = 3 },
            new OrderProduct { Id = 33, ProductId = 2, OrderId = 15, Quantity = 4 },
            new OrderProduct { Id = 34, ProductId = 3, OrderId = 15, Quantity = 1 },
            new OrderProduct { Id = 35, ProductId = 5, OrderId = 14, Quantity = 2 },
            new OrderProduct { Id = 36, ProductId = 10, OrderId = 13, Quantity = 3 },
            new OrderProduct { Id = 37, ProductId = 12, OrderId = 12, Quantity = 2 },
            new OrderProduct { Id = 38, ProductId = 14, OrderId = 11, Quantity = 1 },
            new OrderProduct { Id = 39, ProductId = 15, OrderId = 10, Quantity = 4 },
            new OrderProduct { Id = 40, ProductId = 11, OrderId = 9, Quantity = 1 }
});

        modelBuilder.Entity<Order>().HasData(new Order[]
 {
            new Order { Id = 1, CashierId = 1, PaidOnDate = DateTime.Now },
            new Order { Id = 2, CashierId = 1, PaidOnDate = DateTime.Now },
            new Order { Id = 3, CashierId = 2},
            new Order { Id = 4, CashierId = 2, PaidOnDate = DateTime.Now },
            new Order { Id = 5, CashierId = 3, PaidOnDate = DateTime.Now },
            new Order { Id = 6, CashierId = 3, PaidOnDate = DateTime.Now },
            new Order { Id = 7, CashierId = 4, PaidOnDate = DateTime.Now },
            new Order { Id = 8, CashierId = 4, PaidOnDate = DateTime.Now },
            new Order { Id = 9, CashierId = 5},
            new Order { Id = 10, CashierId = 5, PaidOnDate = DateTime.Now },
            new Order { Id = 11, CashierId = 6, PaidOnDate = DateTime.Now },
            new Order { Id = 12, CashierId = 6, PaidOnDate = DateTime.Now },
            new Order { Id = 13, CashierId = 7, PaidOnDate = DateTime.Now },
            new Order { Id = 14, CashierId = 7, PaidOnDate = DateTime.Now },
            new Order { Id = 15, CashierId = 8, PaidOnDate = DateTime.Now },
            new Order { Id = 16, CashierId = 8, PaidOnDate = DateTime.Now },
            new Order { Id = 17, CashierId = 9, PaidOnDate = DateTime.Now },
            new Order { Id = 18, CashierId = 9, PaidOnDate = DateTime.Now },
            new Order { Id = 19, CashierId = 10, PaidOnDate = DateTime.Now },
            new Order { Id = 20, CashierId = 10}
 });

    }
}