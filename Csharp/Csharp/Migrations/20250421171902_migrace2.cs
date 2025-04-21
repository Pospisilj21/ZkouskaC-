using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Csharp.Migrations
{
    /// <inheritdoc />
    public partial class migrace2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Souhlas",
                table: "Uzivatele",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Souhlas",
                table: "Uzivatele");
        }
    }
}
