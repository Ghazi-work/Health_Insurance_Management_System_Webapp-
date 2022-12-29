using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Insuarance_Management_System_webapp.Migrations
{
    public partial class policymodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Budget",
                table: "Policies",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Budget",
                table: "Policies");
        }
    }
}
