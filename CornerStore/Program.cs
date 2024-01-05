using CornerStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using System.Net.Http.Headers;
using CornerStore.Models.DTOs;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core and provides dummy value for testing
builder.Services.AddNpgsql<CornerStoreDbContext>(builder.Configuration["CornerStoreDbConnectionString"] ?? "testing");

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//endpoints go here

//-------------------------------------------------------------------------//

//Get All Cashiers

app.MapGet("/api/cashiers", (CornerStoreDbContext database) =>
{
    return database.Cashiers
    .Select(c => new CashierDTO
    {
        Id = c.Id,
        FirstName = c.FirstName,
        LastName = c.LastName,
    });
});

//-------------------------------------------------------------------------//

//Add New Cashier

app.MapPost("/api/cashiers", (CornerStoreDbContext database, Cashier cashierToAdd) =>
{
    database.Cashiers.Add(cashierToAdd);
    database.SaveChanges();
    return Results.Created($"/api/cashiers/{cashierToAdd.Id}", cashierToAdd);
});

//-------------------------------------------------------------------------//

//Get Cashier by id including Order List, Including Order Product List from Order List, and then including a Product Object from each Order Product

app.MapGet("/api/cashiers/{id}", (CornerStoreDbContext database, int id) =>
{
    return database.Cashiers
    .Include(c => c.Orders) //referencing cashiers
        .ThenInclude(order => order.OrderProducts) //referencing orders
            .ThenInclude(orderProduct => orderProduct.Product) // referencing order products
    .Where(cashier => cashier.Id == id)

    .Select(c => new CashierDTO
    {
        Id = c.Id,
        FirstName = c.FirstName,
        LastName = c.LastName,
        Orders = c.Orders.Select(order => new OrderDTO
        {
            Id = order.Id,
            CashierId = order.CashierId,
            PaidOnDate = order.PaidOnDate,
            OrderProducts = order.OrderProducts.Select(op => new OrderProductDTO
            {
                Id = op.Id,
                ProductId = op.ProductId,
                Product = new ProductDTO
                {
                    Id = op.Product.Id,
                    ProductName = op.Product.ProductName,
                    Price = op.Product.Price,
                    Brand = op.Product.Brand,
                    CategoryId = op.Product.CategoryId,
                },
                OrderId = op.OrderId,
                Quantity = op.Quantity
            }).ToList()
        }).ToList()
    })
    .Single(c => c.Id == id);
});

//-------------------------------------------------------------------------//

app.MapGet("/api/products", (CornerStoreDbContext database, string search) =>
{
    var products = database.Products
        .Include(product => product.Category)
        .OrderBy(p => p.Id)
        .Select(product => new ProductDTO
        {
            Id = product.Id,
            ProductName = product.ProductName,
            Price = product.Price,
            Brand = product.Brand,
            CategoryId = product.CategoryId,
            Category = new CategoryDTO
            {
                Id = product.Category.Id,
                CategoryName = product.Category.CategoryName
            }
        });

    if (!string.IsNullOrEmpty(search))
    {
        string searchLower = search.ToLower();
        products = products.Where(product =>
            product.ProductName.ToLower().Contains(searchLower) ||
            product.Category.CategoryName.ToLower().Contains(searchLower));
    }

    return products.ToList();
});

// .Where(p => !string.IsNullOrEmpty(search) &&  p.ProductName.ToLower().Contains(search.ToLower()) || p.Category.CategoryName.ToLower().Contains(search.ToLower()) )
//-------------------------------------------------------------------------//

//Create New Product
app.MapPost("/api/products", (CornerStoreDbContext database, Product productToAdd) =>
{
    database.Products.Add(productToAdd);
    database.SaveChanges();
    return Results.Created($"/api/products/{productToAdd.Id}", productToAdd);
});

//-------------------------------------------------------------------------//

//Update A Product
app.MapPut("/api/products/{id}", (CornerStoreDbContext database, int id, Product product) =>
{
    Product productToUpdate = database.Products.SingleOrDefault(p => p.Id == id);
    if (productToUpdate == null)
    {
        return Results.NotFound();
    }

    productToUpdate.ProductName = product.ProductName;
    productToUpdate.Price = product.Price;
    productToUpdate.Brand = product.Brand;
    productToUpdate.CategoryId = product.CategoryId;

    database.SaveChanges();
    return Results.NoContent();
});


//-------------------------------------------------------------------------//

// Get an order details, including the cashier, order products, and products on the order with their category.

app.MapGet("/api/orders/{id}", (CornerStoreDbContext database, int id) =>
{
    Order order = database.Orders

    .Include(order => order.Cashier)
    .Include(order => order.OrderProducts)
        .ThenInclude(OrderProduct => OrderProduct.Product)
            .ThenInclude(Product => Product.Category)
            .Single(order => order.Id == id);

    return new OrderDTO
    {
        Id = order.Id,
        CashierId = order.CashierId,
        Cashier = new CashierDTO
        {
            Id = order.Cashier.Id,
            FirstName = order.Cashier.FirstName,
            LastName = order.Cashier.LastName
        },
        PaidOnDate = order.PaidOnDate,
        OrderProducts = order.OrderProducts.Select(op => new OrderProductDTO
        {
            Id = op.Id,
            ProductId = op.ProductId,
            Product = new ProductDTO
            {
                Id = op.Product.Id,
                ProductName = op.Product.ProductName,
                Price = op.Product.Price,
                Brand = op.Product.Brand,
                CategoryId = op.Product.CategoryId,
                    Category = new CategoryDTO
                    {
                        Id = op.Product.Category.Id,
                        CategoryName = op.Product.Category.CategoryName
                    }
            },
            OrderId = op.OrderId,
            Quantity = op.Quantity

        }).ToList()
    };

});

//-------------------------------------------------------------------------//


app.MapGet("/api/orders", (CornerStoreDbContext database, string date) =>
{
    List<Order> orders = database.Orders.ToList();

    if (!string.IsNullOrEmpty(date) && DateTime.TryParse(date, out var parsedDate))
    {
        // If 'date' parameter is provided and successfully parsed into a DateTime object
        orders = orders.Where(order => order.PaidOnDate.HasValue && order.PaidOnDate.Value.Date == parsedDate.Date).ToList();
        // Filter orders based on the parsed date (comparing only the date parts)
    }

    return orders.Select((order) => new OrderDTO
    {
        Id = order.Id,
        CashierId = order.CashierId,
        PaidOnDate = order.PaidOnDate 
    });
});



//"2024-01-04T15:42:13.810279",
//we need to get back order objects that were paid on 1/04
//-------------------------------------------------------------------------//


//Delete Order
app.MapDelete("/api/orders/{id}", (CornerStoreDbContext database, int id) =>
{

    //fine the single order in the list that matches the id in the parameter
    Order OrderToDelete = database.Orders
    .Single(order => order.Id == id);

    database.Remove(OrderToDelete);
    database.SaveChanges();

    return Results.NoContent();
});


//-------------------------------------------------------------------------//

//Create An Order With Products
app.MapPost("/api/orders", (CornerStoreDbContext database, Order newOrder) =>
{

    database.Add(newOrder);
    database.SaveChanges();
    newOrder.OrderProducts = database.OrderProducts
    .Include(orderProduct => orderProduct.Product)
    .Where(orderProduct => orderProduct.OrderId == newOrder.Id).ToList();
    return Results.Created($"/api/orders/{newOrder.Id}", newOrder);
 
});


//-------------------------------------------------------------------------//


app.Run();

//don't move or change this!
public partial class Program { }