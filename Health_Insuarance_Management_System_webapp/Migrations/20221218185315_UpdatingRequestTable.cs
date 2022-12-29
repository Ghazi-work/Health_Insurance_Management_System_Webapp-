using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Insuarance_Management_System_webapp.Migrations
{
    public partial class UpdatingRequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Policy_Requests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Policy_Requests");
        }
    }
}
