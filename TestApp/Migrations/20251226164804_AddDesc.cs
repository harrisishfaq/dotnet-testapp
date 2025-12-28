using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestApp.Migrations
{
    /// <inheritdoc />
    public partial class AddDesc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Courses",
                newName: "Nameds");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Courses",
                newName: "Descs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Named",
                table: "Courses",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Desc",
                table: "Courses",
                newName: "Description");
        }
    }
}
