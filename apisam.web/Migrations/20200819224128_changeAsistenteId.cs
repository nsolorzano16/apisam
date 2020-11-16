using Microsoft.EntityFrameworkCore.Migrations;

namespace apisam.web.Migrations
{
    public partial class changeAsistenteId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AsistenteId",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AsistenteId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
