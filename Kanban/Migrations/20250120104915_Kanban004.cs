using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kanban.Migrations
{
    /// <inheritdoc />
    public partial class Kanban004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Users",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Stories",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Statuses",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Boards",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "getdate()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Boards");
        }
    }
}
