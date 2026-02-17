using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirportSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OwnerAcc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Gender", "Password" },
                values: new object[] { "male", "AQAAAAIAAYagAAAAEJd8EThBFpYcpDslkKwyv9iK3EIn0BNhO1n9phzZmmQc8Xl99GfgCchGv0F96t71qA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Gender", "Password" },
                values: new object[] { null, "AQAAAAIAAYagAAAAEFp0gfLQctnR9/1SVhMuxI8Btu4PThY7LG84lksAJIrFumBc2Lfe240KfQMpp8mPJg==" });
        }
    }
}
