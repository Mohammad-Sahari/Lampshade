using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogManagement.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedArticleCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureAlt",
                table: "ArticleCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PictureTitle",
                table: "ArticleCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureAlt",
                table: "ArticleCategories");

            migrationBuilder.DropColumn(
                name: "PictureTitle",
                table: "ArticleCategories");
        }
    }
}
