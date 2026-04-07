using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AuthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd74a5dc-80b4-4d1f-8b95-a4c966267aa4");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "40b3082b-7eba-4d9f-b185-bf1f363d0424", "ea73dbbc-149a-460c-9d9b-73d104bd5fd4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40b3082b-7eba-4d9f-b185-bf1f363d0424");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ea73dbbc-149a-460c-9d9b-73d104bd5fd4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "816d462f-6784-412a-a9b0-2d0d68ec7853", "12015894-9fd9-4c3e-82e8-438f8a8eaa9a", "User", "USER" },
                    { "a130f90e-1026-4844-8808-4282b56abee2", "116d48c4-786f-4a65-b37a-b1b7c902d3a8", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c351c216-0d22-42fa-ab70-f1cbdd2cda7b", 0, "18ed365f-9128-43b2-a983-eb286bd7ef85", "admin@gmail.com", true, "Admin", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEAdb6AtNbKmtZ8Hkd5EOhUcX9T1yMZfYfRWC5oC6LQdIH+2QUz0F1UjfyP1ggxxzTg==", null, false, "e4bbee8b-c3be-4f70-9a53-e700300a1a84", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a130f90e-1026-4844-8808-4282b56abee2", "c351c216-0d22-42fa-ab70-f1cbdd2cda7b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "816d462f-6784-412a-a9b0-2d0d68ec7853");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a130f90e-1026-4844-8808-4282b56abee2", "c351c216-0d22-42fa-ab70-f1cbdd2cda7b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a130f90e-1026-4844-8808-4282b56abee2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c351c216-0d22-42fa-ab70-f1cbdd2cda7b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "40b3082b-7eba-4d9f-b185-bf1f363d0424", null, "Admin", "ADMIN" },
                    { "bd74a5dc-80b4-4d1f-8b95-a4c966267aa4", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ea73dbbc-149a-460c-9d9b-73d104bd5fd4", 0, "4d3b1a50-c2d0-4fa7-ae12-4c872fcd587c", "admin@gmail.com", true, "Admin", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEAksHd44mBqB5IqAqwWjxyDqPsttr78lEvBvzto8GfD69Wbv2raJLuq8Kpg31MRw+Q==", null, false, "829b0fff-5b27-4d2b-bb97-d2bbd6c79b8a", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "40b3082b-7eba-4d9f-b185-bf1f363d0424", "ea73dbbc-149a-460c-9d9b-73d104bd5fd4" });
        }
    }
}
