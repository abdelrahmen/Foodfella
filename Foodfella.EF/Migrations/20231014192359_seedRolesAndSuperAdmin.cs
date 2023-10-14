using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foodfella.EF.Migrations
{
    public partial class seedRolesAndSuperAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1", "f5df5677-42f7-4d1a-a9a3-735f9e59be7c", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2", "16a198f3-61d7-4464-ad74-0bbfbb9b52c8", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "address" },
                values: new object[] { "1", 0, "ecd2646b-bdc3-4e58-906d-7e8ba3666c31", "TemporaryEmail@example.com", true, "Temporary Full Name", false, null, "TEMPORARYEMAIL@EXAMPLE.COM", "TEMPORARY-USERNAME", "AQAAAAEAACcQAAAAEOfGEOaNGqmV0tAhCLNpJxWet3t081FgrB+mo5crLioFtlw+JnLWc2sUktS+UugYag==", null, false, "", false, "TemporaryUsername", "Temporary Address" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");
        }
    }
}
