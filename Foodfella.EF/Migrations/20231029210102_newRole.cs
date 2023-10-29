using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foodfella.EF.Migrations
{
    public partial class newRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "3032eff4-6698-4452-8715-48c4d0e2ab60");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "e1f9d660-631e-44ac-ac83-8b53c6e09abd");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3", "5905a17d-eebf-4137-88d0-79d8d1f68ab4", "Customer", "CUSTOMER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9d703fe4-791e-4768-98ce-a57b0dff4a63", "AQAAAAEAACcQAAAAECXMe7btLqhw06VCFAwElprPvGZmQBZksO+grgQ71ifovzTqRy7gtQGMMjNp6nojQA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3");

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
    }
}
