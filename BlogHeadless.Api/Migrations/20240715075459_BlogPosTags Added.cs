using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogHeadless.Data.Migrations
{
    /// <inheritdoc />
    public partial class BlogPosTagsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "blogPostTags",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "blogPostTags",
                table: "BlogPosts");
        }
    }
}
