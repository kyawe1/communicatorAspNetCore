using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace communicator.Migrations
{
    public partial class reaction_and_save_blog_tables_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLike",
                table: "reactions",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLike",
                table: "reactions");
        }
    }
}
