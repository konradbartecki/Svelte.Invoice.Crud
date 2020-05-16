using Microsoft.EntityFrameworkCore.Migrations;

namespace Me.Bartecki.InvoiceApp.Web.Migrations
{
    public partial class NameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPayed",
                table: "Invoice");

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Invoice",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Invoice");

            migrationBuilder.AddColumn<bool>(
                name: "IsPayed",
                table: "Invoice",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
