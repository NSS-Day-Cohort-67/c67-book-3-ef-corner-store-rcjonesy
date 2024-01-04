using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CornerStore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cashiers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cashiers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CashierId = table.Column<int>(type: "integer", nullable: false),
                    PaidOnDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Cashiers_CashierId",
                        column: x => x.CashierId,
                        principalTable: "Cashiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cashiers",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "John", "Doe" },
                    { 2, "Alice", "Smith" },
                    { 3, "Bob", "Johnson" },
                    { 4, "Emily", "Williams" },
                    { 5, "Michael", "Brown" },
                    { 6, "Sophia", "Jones" },
                    { 7, "William", "Garcia" },
                    { 8, "Olivia", "Martinez" },
                    { 9, "James", "Miller" },
                    { 10, "Emma", "Davis" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Snacks" },
                    { 2, "Beverages" },
                    { 3, "Candy" },
                    { 4, "Frozen" },
                    { 5, "Household" },
                    { 6, "Beauty" },
                    { 7, "Bakery" },
                    { 8, "Dairy" },
                    { 9, "Produce" },
                    { 10, "Toiletries" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CashierId", "PaidOnDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 1, 4, 15, 42, 13, 810, DateTimeKind.Local).AddTicks(2759) },
                    { 2, 1, new DateTime(2024, 1, 4, 15, 42, 13, 810, DateTimeKind.Local).AddTicks(2794) },
                    { 3, 2, null },
                    { 4, 2, new DateTime(2024, 1, 4, 15, 42, 13, 810, DateTimeKind.Local).AddTicks(2796) },
                    { 5, 3, new DateTime(2024, 1, 4, 15, 42, 13, 810, DateTimeKind.Local).AddTicks(2798) },
                    { 6, 3, new DateTime(2024, 1, 4, 15, 42, 13, 810, DateTimeKind.Local).AddTicks(2799) },
                    { 7, 4, new DateTime(2024, 1, 4, 15, 42, 13, 810, DateTimeKind.Local).AddTicks(2801) },
                    { 8, 4, new DateTime(2024, 1, 4, 15, 42, 13, 810, DateTimeKind.Local).AddTicks(2802) },
                    { 9, 5, null },
                    { 10, 5, new DateTime(2024, 1, 4, 15, 42, 13, 810, DateTimeKind.Local).AddTicks(2804) },
                    { 11, 6, new DateTime(2024, 1, 4, 15, 42, 13, 810, DateTimeKind.Local).AddTicks(2805) },
                    { 12, 6, new DateTime(2024, 1, 4, 15, 42, 13, 810, DateTimeKind.Local).AddTicks(2806) },
                    { 13, 7, new DateTime(2024, 1, 4, 15, 42, 13, 810, DateTimeKind.Local).AddTicks(2808) },
                    { 14, 7, new DateTime(2024, 1, 4, 15, 42, 13, 810, DateTimeKind.Local).AddTicks(2810) },
                    { 15, 8, new DateTime(2024, 1, 4, 15, 42, 13, 810, DateTimeKind.Local).AddTicks(2811) },
                    { 16, 8, new DateTime(2024, 1, 4, 15, 42, 13, 810, DateTimeKind.Local).AddTicks(2812) },
                    { 17, 9, new DateTime(2024, 1, 4, 15, 42, 13, 810, DateTimeKind.Local).AddTicks(2814) },
                    { 18, 9, new DateTime(2024, 1, 4, 15, 42, 13, 810, DateTimeKind.Local).AddTicks(2815) },
                    { 19, 10, new DateTime(2024, 1, 4, 15, 42, 13, 810, DateTimeKind.Local).AddTicks(2816) },
                    { 20, 10, null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "CategoryId", "Price", "ProductName" },
                values: new object[,]
                {
                    { 1, "Pepsi Cola", 2, 2.00m, "Pepsi" },
                    { 2, "Frito-Lay", 1, 1.50m, "Doritos" },
                    { 3, "Mars, Incorporated", 3, 1.20m, "Snickers" },
                    { 4, "Ben & Jerry's", 4, 3.50m, "Ice Cream" },
                    { 5, "Bounty", 5, 2.80m, "Paper Towels" },
                    { 6, "Pantene", 6, 4.50m, "Shampoo" },
                    { 7, "Local Bakery", 7, 1.80m, "Baguette" },
                    { 8, "Organic Valley", 8, 2.20m, "Milk" },
                    { 9, "Washington Apples", 9, 1.00m, "Apples" },
                    { 10, "Colgate", 10, 3.00m, "Toothpaste" },
                    { 11, "The Coca-Cola Company", 2, 1.80m, "Coca-Cola" },
                    { 12, "Frito-Lay", 1, 1.50m, "Lays" },
                    { 13, "Mars, Incorporated", 3, 1.20m, "Twix" },
                    { 14, "DiGiorno", 4, 4.50m, "Frozen Pizza" },
                    { 15, "Dove", 6, 2.50m, "Soap" }
                });

            migrationBuilder.InsertData(
                table: "OrderProducts",
                columns: new[] { "Id", "OrderId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 3 },
                    { 2, 1, 3, 3 },
                    { 3, 1, 7, 2 },
                    { 4, 1, 8, 4 },
                    { 5, 1, 10, 1 },
                    { 6, 2, 12, 3 },
                    { 7, 2, 15, 2 },
                    { 8, 3, 9, 6 },
                    { 9, 3, 4, 1 },
                    { 10, 3, 11, 4 },
                    { 11, 4, 6, 2 },
                    { 12, 4, 13, 3 },
                    { 13, 4, 14, 1 },
                    { 14, 5, 2, 2 },
                    { 15, 5, 3, 3 },
                    { 16, 6, 5, 4 },
                    { 17, 6, 10, 1 },
                    { 18, 7, 12, 2 },
                    { 19, 7, 14, 3 },
                    { 20, 7, 15, 1 },
                    { 21, 8, 5, 2 },
                    { 22, 20, 7, 3 },
                    { 23, 20, 8, 1 },
                    { 24, 19, 10, 4 },
                    { 25, 8, 12, 2 },
                    { 26, 19, 15, 1 },
                    { 27, 18, 9, 3 },
                    { 28, 18, 4, 2 },
                    { 29, 17, 11, 5 },
                    { 30, 17, 6, 1 },
                    { 31, 16, 13, 2 },
                    { 32, 16, 14, 3 },
                    { 33, 15, 2, 4 },
                    { 34, 15, 3, 1 },
                    { 35, 14, 5, 2 },
                    { 36, 13, 10, 3 },
                    { 37, 12, 12, 2 },
                    { 38, 11, 14, 1 },
                    { 39, 10, 15, 4 },
                    { 40, 9, 11, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CashierId",
                table: "Orders",
                column: "CashierId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Cashiers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
