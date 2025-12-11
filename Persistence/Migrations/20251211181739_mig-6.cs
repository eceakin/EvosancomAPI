using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvosancomAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DealerNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DealerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false),
                    ReadDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReferenceOrderId = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealerNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DealerNotifications_Dealers_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceCalculationHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DealerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Width = table.Column<decimal>(type: "numeric", nullable: false),
                    Height = table.Column<decimal>(type: "numeric", nullable: false),
                    Depth = table.Column<decimal>(type: "numeric", nullable: false),
                    BasePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    CustomDimensionCost = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    DiscountedPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    ConvertedToOrder = table.Column<bool>(type: "boolean", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceCalculationHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceCalculationHistories_Dealers_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriceCalculationHistories_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PriceCalculationHistories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DealerNotifications_DealerId",
                table: "DealerNotifications",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceCalculationHistories_DealerId",
                table: "PriceCalculationHistories",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceCalculationHistories_OrderId",
                table: "PriceCalculationHistories",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceCalculationHistories_ProductId",
                table: "PriceCalculationHistories",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DealerNotifications");

            migrationBuilder.DropTable(
                name: "PriceCalculationHistories");
        }
    }
}
