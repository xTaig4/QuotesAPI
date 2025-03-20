using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuoteAPI.Migrations
{
    /// <inheritdoc />
    public partial class addAnime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_Quote",
                table: "Quotes");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Quotes",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Quotes",
                newName: "image");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Quotes",
                newName: "firstName");

            migrationBuilder.AlterColumn<string>(
                name: "lastName",
                table: "Quotes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "image",
                table: "Quotes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "anime",
                table: "Quotes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "quote",
                table: "Quotes",
                type: "TEXT",
                nullable: true,
                defaultValue: "quote placeholder");
            }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Quotes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "anime",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "quote",
                table: "Quotes");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Quotes",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "image",
                table: "Quotes",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Quotes",
                newName: "FirstName");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Quotes",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Quotes",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_Quote",
                table: "Quotes",
                type: "TEXT",
                nullable: false,
                defaultValue: "quote placeholder");
        }
    }
}
