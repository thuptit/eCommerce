using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace eCommerce.EntityFrameworkCore.Migrations
{
    public partial class addnewentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDiscount_Products_ProductId",
                table: "ProductDiscount");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventory_Products_ProductId",
                table: "ProductInventory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInventory",
                table: "ProductInventory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductDiscount",
                table: "ProductDiscount");

            migrationBuilder.RenameTable(
                name: "ProductInventory",
                newName: "ProductInventories");

            migrationBuilder.RenameTable(
                name: "ProductDiscount",
                newName: "ProductDiscounts");

            migrationBuilder.RenameIndex(
                name: "IX_ProductInventory_ProductId",
                table: "ProductInventories",
                newName: "IX_ProductInventories_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductInventory_IsDeleted",
                table: "ProductInventories",
                newName: "IX_ProductInventories_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_ProductDiscount_ProductId",
                table: "ProductDiscounts",
                newName: "IX_ProductDiscounts_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductDiscount_IsDeleted",
                table: "ProductDiscounts",
                newName: "IX_ProductDiscounts_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInventories",
                table: "ProductInventories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductDiscounts",
                table: "ProductDiscounts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiorId = table.Column<long>(type: "bigint", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletorId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Provider = table.Column<string>(type: "text", nullable: false),
                    PaymentStatus = table.Column<byte>(type: "smallint", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiorId = table.Column<long>(type: "bigint", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletorId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiorId = table.Column<long>(type: "bigint", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletorId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPayments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    PaymentType = table.Column<byte>(type: "smallint", nullable: false),
                    Provider = table.Column<string>(type: "text", nullable: false),
                    AccountNo = table.Column<string>(type: "text", nullable: false),
                    Expiry = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiorId = table.Column<long>(type: "bigint", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletorId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPayments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    PaymentId = table.Column<long>(type: "bigint", nullable: false),
                    Total = table.Column<int>(type: "integer", nullable: false),
                    OrderStatus = table.Column<byte>(type: "smallint", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiorId = table.Column<long>(type: "bigint", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletorId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_PaymentDetails_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "PaymentDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    ProductDescription = table.Column<string>(type: "text", nullable: false),
                    Discount = table.Column<decimal>(type: "numeric", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: true),
                    CreatorId = table.Column<long>(type: "bigint", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiorId = table.Column<long>(type: "bigint", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletorId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_IsDeleted",
                table: "CartItems",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_UserId",
                table: "CartItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_IsDeleted",
                table: "OrderItems",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IsDeleted",
                table: "Orders",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentId",
                table: "Orders",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetails_IsDeleted",
                table: "PaymentDetails",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_IsDeleted",
                table: "ProductImages",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPayments_IsDeleted",
                table: "UserPayments",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UserPayments_UserId",
                table: "UserPayments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDiscounts_Products_ProductId",
                table: "ProductDiscounts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventories_Products_ProductId",
                table: "ProductInventories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDiscounts_Products_ProductId",
                table: "ProductDiscounts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventories_Products_ProductId",
                table: "ProductInventories");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "UserPayments");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "PaymentDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInventories",
                table: "ProductInventories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductDiscounts",
                table: "ProductDiscounts");

            migrationBuilder.RenameTable(
                name: "ProductInventories",
                newName: "ProductInventory");

            migrationBuilder.RenameTable(
                name: "ProductDiscounts",
                newName: "ProductDiscount");

            migrationBuilder.RenameIndex(
                name: "IX_ProductInventories_ProductId",
                table: "ProductInventory",
                newName: "IX_ProductInventory_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductInventories_IsDeleted",
                table: "ProductInventory",
                newName: "IX_ProductInventory_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_ProductDiscounts_ProductId",
                table: "ProductDiscount",
                newName: "IX_ProductDiscount_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductDiscounts_IsDeleted",
                table: "ProductDiscount",
                newName: "IX_ProductDiscount_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInventory",
                table: "ProductInventory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductDiscount",
                table: "ProductDiscount",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDiscount_Products_ProductId",
                table: "ProductDiscount",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventory_Products_ProductId",
                table: "ProductInventory",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
