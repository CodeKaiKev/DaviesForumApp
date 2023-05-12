using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DaviesForumApp.Migrations
{
    /// <inheritdoc />
    public partial class AddUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserOption",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserPhone",
                table: "Users",
                newName: "ConfirmPassword");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConfirmPassword",
                table: "Users",
                newName: "UserPhone");

            migrationBuilder.AddColumn<string>(
                name: "UserOption",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
