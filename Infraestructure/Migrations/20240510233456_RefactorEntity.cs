using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DirectorName",
                table: "Anime",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "AnimeName",
                table: "Anime",
                newName: "Director");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Anime",
                newName: "DirectorName");

            migrationBuilder.RenameColumn(
                name: "Director",
                table: "Anime",
                newName: "AnimeName");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Anime",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
