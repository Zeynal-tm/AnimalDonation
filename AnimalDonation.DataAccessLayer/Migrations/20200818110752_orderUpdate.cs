using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimalDonation.DataAccessLayer.Migrations
{
    public partial class orderUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descriptin",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Descriptin",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
