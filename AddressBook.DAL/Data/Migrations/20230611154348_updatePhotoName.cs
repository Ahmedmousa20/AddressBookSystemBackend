using Microsoft.EntityFrameworkCore.Migrations;

namespace AddressBook.DAL.Data.Migrations
{
    public partial class updatePhotoName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Addressbook",
                newName: "PhotoUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoUrl",
                table: "Addressbook",
                newName: "Photo");
        }
    }
}
