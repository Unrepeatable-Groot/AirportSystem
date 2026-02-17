using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirportSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnerAcc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Email", "FirstName", "Gender", "LastName", "Password", "PersonalId", "PhoneNumber", "Role" },
                values: new object[] { 1, 30, "georgedoe123@gmail.com", "George", null, "Doe", "AQAAAAIAAYagAAAAEFp0gfLQctnR9/1SVhMuxI8Btu4PThY7LG84lksAJIrFumBc2Lfe240KfQMpp8mPJg==", "00000000000", "555555555", 3 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Email", "FirstName", "Gender", "LastName", "Password", "PersonalId", "PhoneNumber", "Role" },
                values: new object[] { 2, 30, "georgedoe123@gmail.com", "George", null, "Doe", "AQAAAAIAAYagAAAAEExgU60jvKBkoFo7fXFu9Uc8XFPQV2m0ChhFt6m4864aHK5fiXJMDrlW9/GAQ8V1qA==", "00000000000", "555555555", 3 });
        }
    }
}
