using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Insuarance_Management_System_webapp.Migrations
{
    public partial class CreatingNewPolicyReqTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Policy_Request",
                table: "Policy_Request");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Policy_Request");

            migrationBuilder.RenameTable(
                name: "Policy_Request",
                newName: "Policy_Requests");

            migrationBuilder.AlterColumn<int>(
                name: "PolicyId",
                table: "Policy_Requests",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CompId",
                table: "Policy_Requests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FnameLname",
                table: "Policy_Requests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Policy_Requests",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Policy_Requests",
                table: "Policy_Requests",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Policy_Requests_CompId",
                table: "Policy_Requests",
                column: "CompId");

            migrationBuilder.CreateIndex(
                name: "IX_Policy_Requests_PolicyId",
                table: "Policy_Requests",
                column: "PolicyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Policy_Requests_Insurance_Companies_CompId",
                table: "Policy_Requests",
                column: "CompId",
                principalTable: "Insurance_Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Policy_Requests_Policies_PolicyId",
                table: "Policy_Requests",
                column: "PolicyId",
                principalTable: "Policies",
                principalColumn: "PolicyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policy_Requests_Insurance_Companies_CompId",
                table: "Policy_Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Policy_Requests_Policies_PolicyId",
                table: "Policy_Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Policy_Requests",
                table: "Policy_Requests");

            migrationBuilder.DropIndex(
                name: "IX_Policy_Requests_CompId",
                table: "Policy_Requests");

            migrationBuilder.DropIndex(
                name: "IX_Policy_Requests_PolicyId",
                table: "Policy_Requests");

            migrationBuilder.DropColumn(
                name: "CompId",
                table: "Policy_Requests");

            migrationBuilder.DropColumn(
                name: "FnameLname",
                table: "Policy_Requests");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Policy_Requests");

            migrationBuilder.RenameTable(
                name: "Policy_Requests",
                newName: "Policy_Request");

            migrationBuilder.AlterColumn<int>(
                name: "PolicyId",
                table: "Policy_Request",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Policy_Request",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Policy_Request",
                table: "Policy_Request",
                column: "Id");
        }
    }
}
