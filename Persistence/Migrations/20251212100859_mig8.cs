using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvosancomAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "ContractEndDate",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "District",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "TaxNumber",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "TaxOffice",
                table: "Dealers");

            migrationBuilder.RenameColumn(
                name: "MonthlySalesQuota",
                table: "Dealers",
                newName: "SalesQuota");

            migrationBuilder.RenameColumn(
                name: "ContractStartDate",
                table: "Dealers",
                newName: "ContractDate");

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentPeriodSales",
                table: "Dealers",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPeriodSales",
                table: "Dealers");

            migrationBuilder.RenameColumn(
                name: "SalesQuota",
                table: "Dealers",
                newName: "MonthlySalesQuota");

            migrationBuilder.RenameColumn(
                name: "ContractDate",
                table: "Dealers",
                newName: "ContractStartDate");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Dealers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Dealers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractEndDate",
                table: "Dealers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Dealers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Dealers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Dealers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaxNumber",
                table: "Dealers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaxOffice",
                table: "Dealers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
