using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3_Asp.Net_MVC.Migrations
{
    public partial class capnhat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Recipient",
                table: "Bill",
                type: "nvarchar(256)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(256)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Recipient",
                table: "Bill",
                type: "varchar(256)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)");
        }
    }
}
