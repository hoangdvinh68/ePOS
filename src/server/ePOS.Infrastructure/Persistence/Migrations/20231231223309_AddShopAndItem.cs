using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ePOS.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddShopAndItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "Sequence_Category");

            migrationBuilder.CreateSequence<int>(
                name: "Sequence_Currency");

            migrationBuilder.CreateSequence<int>(
                name: "Sequence_Item");

            migrationBuilder.CreateSequence<int>(
                name: "Sequence_ItemImage");

            migrationBuilder.CreateSequence<int>(
                name: "Sequence_ItemProperty");

            migrationBuilder.CreateSequence<int>(
                name: "Sequence_ItemPropertyValue");

            migrationBuilder.CreateSequence<int>(
                name: "Sequence_ItemSize");

            migrationBuilder.CreateSequence<int>(
                name: "Sequence_Shop");

            migrationBuilder.CreateSequence<int>(
                name: "Sequence_Tenant");

            migrationBuilder.CreateSequence<int>(
                name: "Sequence_Topping");

            migrationBuilder.CreateSequence<int>(
                name: "Sequence_Unit");

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AspNetRoles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShopId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubId = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "NEXT VALUE FOR Sequence_Category"),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsoCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubId = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "NEXT VALUE FOR Sequence_Currency")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WifiPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubId = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "NEXT VALUE FOR Sequence_Shop"),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Toppings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShopId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubId = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "NEXT VALUE FOR Sequence_Topping"),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Toppings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubId = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "NEXT VALUE FOR Sequence_Unit"),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTaxAppliedAllItem = table.Column<bool>(type: "bit", nullable: false),
                    TaxRate = table.Column<int>(type: "int", nullable: true),
                    IsItemPriceIncludeTax = table.Column<bool>(type: "bit", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubId = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "NEXT VALUE FOR Sequence_Tenant"),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tenants_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShopId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sku = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TaxRate = table.Column<int>(type: "int", nullable: true),
                    IsTaxIncludePrice = table.Column<bool>(type: "bit", nullable: true),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaxTopping = table.Column<int>(type: "int", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubId = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "NEXT VALUE FOR Sequence_Item"),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CategoryItems",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryItems", x => new { x.CategoryId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_CategoryItems_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubId = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "NEXT VALUE FOR Sequence_ItemImage")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemImages_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemProperties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubId = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "NEXT VALUE FOR Sequence_ItemProperty")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemProperties_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemSizes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubId = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "NEXT VALUE FOR Sequence_ItemSize")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemSizes_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemToppings",
                columns: table => new
                {
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToppingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemToppings", x => new { x.ItemId, x.ToppingId });
                    table.ForeignKey(
                        name: "FK_ItemToppings_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemToppings_Toppings_ToppingId",
                        column: x => x.ToppingId,
                        principalTable: "Toppings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemPropertyOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemPropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubId = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "NEXT VALUE FOR Sequence_ItemPropertyValue")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPropertyOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemPropertyOptions_ItemProperties_ItemPropertyId",
                        column: x => x.ItemPropertyId,
                        principalTable: "ItemProperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SubId",
                table: "Categories",
                column: "SubId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryItems_ItemId",
                table: "CategoryItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_SubId",
                table: "Currencies",
                column: "SubId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemImages_ItemId",
                table: "ItemImages",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemImages_SubId",
                table: "ItemImages",
                column: "SubId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemProperties_ItemId",
                table: "ItemProperties",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemProperties_SubId",
                table: "ItemProperties",
                column: "SubId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPropertyOptions_ItemPropertyId",
                table: "ItemPropertyOptions",
                column: "ItemPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPropertyOptions_SubId",
                table: "ItemPropertyOptions",
                column: "SubId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Sku",
                table: "Items",
                column: "Sku");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SubId",
                table: "Items",
                column: "SubId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UnitId",
                table: "Items",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSizes_ItemId",
                table: "ItemSizes",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSizes_SubId",
                table: "ItemSizes",
                column: "SubId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemToppings_ToppingId",
                table: "ItemToppings",
                column: "ToppingId");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_SubId",
                table: "Shops",
                column: "SubId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_CurrencyId",
                table: "Tenants",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_SubId",
                table: "Tenants",
                column: "SubId");

            migrationBuilder.CreateIndex(
                name: "IX_Toppings_SubId",
                table: "Toppings",
                column: "SubId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_SubId",
                table: "Units",
                column: "SubId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryItems");

            migrationBuilder.DropTable(
                name: "ItemImages");

            migrationBuilder.DropTable(
                name: "ItemPropertyOptions");

            migrationBuilder.DropTable(
                name: "ItemSizes");

            migrationBuilder.DropTable(
                name: "ItemToppings");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ItemProperties");

            migrationBuilder.DropTable(
                name: "Toppings");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AspNetRoles");

            migrationBuilder.DropSequence(
                name: "Sequence_Category");

            migrationBuilder.DropSequence(
                name: "Sequence_Currency");

            migrationBuilder.DropSequence(
                name: "Sequence_Item");

            migrationBuilder.DropSequence(
                name: "Sequence_ItemImage");

            migrationBuilder.DropSequence(
                name: "Sequence_ItemProperty");

            migrationBuilder.DropSequence(
                name: "Sequence_ItemPropertyValue");

            migrationBuilder.DropSequence(
                name: "Sequence_ItemSize");

            migrationBuilder.DropSequence(
                name: "Sequence_Shop");

            migrationBuilder.DropSequence(
                name: "Sequence_Tenant");

            migrationBuilder.DropSequence(
                name: "Sequence_Topping");

            migrationBuilder.DropSequence(
                name: "Sequence_Unit");
        }
    }
}
