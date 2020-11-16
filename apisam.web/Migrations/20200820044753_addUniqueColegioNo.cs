using Microsoft.EntityFrameworkCore.Migrations;

namespace apisam.web.Migrations
{
    public partial class addUniqueColegioNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ColegioNumero",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ColegioNumero",
                table: "AspNetUsers",
                column: "ColegioNumero",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ColegioNumero",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ColegioNumero",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
