using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Insuarance_Management_System_webapp.Migrations
{
    public partial class addingPhotosToPolicyAndCompanies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeleteRoleViewModel");

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Policies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Insurance_Companies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Insurance_Companies");

            migrationBuilder.CreateTable(
                name: "DeleteRoleViewModel",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeleteRoleViewModel", x => x.Id);
                });
        }
    }
}
