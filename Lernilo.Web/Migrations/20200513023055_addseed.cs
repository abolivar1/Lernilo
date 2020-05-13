using Microsoft.EntityFrameworkCore.Migrations;

namespace Lernilo.Web.Migrations
{
    public partial class addseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tutorials_AspNetUsers_UserId",
                table: "Tutorials");

            migrationBuilder.DropIndex(
                name: "IX_Tutorials_UserId",
                table: "Tutorials");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tutorials");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Tutorials",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tutorials_UserId",
                table: "Tutorials",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tutorials_AspNetUsers_UserId",
                table: "Tutorials",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
