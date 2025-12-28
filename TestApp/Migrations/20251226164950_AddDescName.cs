using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestApp.Migrations
{
    /// <inheritdoc />
    public partial class AddDescName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nameds",
                table: "Courses",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Descs",
                table: "Courses",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Courses",
                newName: "Named");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Courses",
                newName: "Desc");
        }
    }
}
