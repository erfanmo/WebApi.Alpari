using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Alpari.Migrations
{
    public partial class addrefreshtoken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "userTokens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpir",
                table: "userTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "userTokens");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpir",
                table: "userTokens");
        }
    }
}
