using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Health_Insuarance_Management_System_webapp.Migrations
{
    public partial class claimpolicy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Claim_Policy",
                columns: table => new
                {
                    ClaimId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    FnameLname = table.Column<string>(nullable: true),
                    PolicyId = table.Column<int>(nullable: true),
                    CompId = table.Column<int>(nullable: true),
                    Photopath = table.Column<string>(nullable: true),
                    PostingDate = table.Column<DateTime>(nullable: false),
                    ClaimAmount = table.Column<int>(nullable: false),
                    UserReason = table.Column<string>(nullable: true),
                    AdminReasons = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claim_Policy", x => x.ClaimId);
                    table.ForeignKey(
                        name: "FK_Claim_Policy_Insurance_Companies_CompId",
                        column: x => x.CompId,
                        principalTable: "Insurance_Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Claim_Policy_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "PolicyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Claim_Policy_CompId",
                table: "Claim_Policy",
                column: "CompId");

            migrationBuilder.CreateIndex(
                name: "IX_Claim_Policy_PolicyId",
                table: "Claim_Policy",
                column: "PolicyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Claim_Policy");
        }
    }
}
