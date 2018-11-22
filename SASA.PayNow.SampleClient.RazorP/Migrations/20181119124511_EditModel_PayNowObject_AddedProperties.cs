using Microsoft.EntityFrameworkCore.Migrations;

namespace SASA.PayNow.SampleClient.RazorP.Migrations
{
    public partial class EditModel_PayNowObject_AddedProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JSDictionay",
                table: "PayNowObject",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "PayNowObject",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JSDictionay",
                table: "PayNowObject");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "PayNowObject");
        }
    }
}
