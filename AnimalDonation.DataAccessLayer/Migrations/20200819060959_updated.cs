using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimalDonation.DataAccessLayer.Migrations
{
    public partial class updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderResponses");

            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PaymentSystemOrderId",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paid",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentSystemOrderId",
                table: "Orders");

            migrationBuilder.CreateTable(
                name: "OrderResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    formUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    orderId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderResponses", x => x.Id);
                });
        }
    }
}
