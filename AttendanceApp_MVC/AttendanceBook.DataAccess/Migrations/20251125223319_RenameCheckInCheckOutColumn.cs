using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceBook.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenameCheckInCheckOutColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CheckOutTime",
                table: "Attendances",
                newName: "CheckOut");

            migrationBuilder.RenameColumn(
                name: "CheckInTime",
                table: "Attendances",
                newName: "CheckIn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CheckOut",
                table: "Attendances",
                newName: "CheckOutTime");

            migrationBuilder.RenameColumn(
                name: "CheckIn",
                table: "Attendances",
                newName: "CheckInTime");
        }
    }
}
