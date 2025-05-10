using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookReviewSite.Migrations
{
    /// <inheritdoc />
    public partial class AddDatePostedToReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DatePosted",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow); // Set a default value if needed
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatePosted",
                table: "Reviews");
        }
    }
}
