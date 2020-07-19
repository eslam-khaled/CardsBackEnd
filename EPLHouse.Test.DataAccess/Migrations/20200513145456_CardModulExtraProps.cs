using Microsoft.EntityFrameworkCore.Migrations;

namespace EPLHouse.Cards.DataAccess.Migrations
{
    public partial class CardModulExtraProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CCV",
                table: "Cards");

            migrationBuilder.AddColumn<int>(
                name: "CVV",
                table: "Cards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CVV_2",
                table: "Cards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ICVV",
                table: "Cards",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVV",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "CVV_2",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "ICVV",
                table: "Cards");

            migrationBuilder.AddColumn<int>(
                name: "CCV",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
