using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kanban.Migrations
{
    /// <inheritdoc />
    public partial class Kanban001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Stories",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Stories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Statuses",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Statuses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Boards",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Boards",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Boards");
        }
    }
}
