using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirportSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Email", "FirstName", "Gender", "LastName", "Password", "PersonalId", "PhoneNumber", "Role" },
                values: new object[] { 1, 30, "georgedoe123@gmail.com", "George", null, "Doe", "AQAAAAIAAYagAAAAEGKvSzHABUcWL8CKhu0GFT9oI5rRqDMJmwqJkmss59gNYgnYUx1ryqtI01ahh7gUyg==", "555555555", "00000000000", 3 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
