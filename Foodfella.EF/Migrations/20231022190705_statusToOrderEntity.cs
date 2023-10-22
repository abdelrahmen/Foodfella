using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foodfella.EF.Migrations
{
    public partial class statusToOrderEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "28106569-63d0-43f4-aefb-7b4625a28cfa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "c85b8116-6ab4-4e4b-bc6f-d8d3bf628862");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "38242178-085f-48d5-be55-320c698a04ca", "AQAAAAEAACcQAAAAELVWTRd4ntW6L5ip2AbJMeatSNKroLb4pXq/jL5kbAGjDPklAuP/VB1Ode3n1DhFGw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "f5df5677-42f7-4d1a-a9a3-735f9e59be7c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "16a198f3-61d7-4464-ad74-0bbfbb9b52c8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ecd2646b-bdc3-4e58-906d-7e8ba3666c31", "AQAAAAEAACcQAAAAEOfGEOaNGqmV0tAhCLNpJxWet3t081FgrB+mo5crLioFtlw+JnLWc2sUktS+UugYag==" });
        }
    }
}
