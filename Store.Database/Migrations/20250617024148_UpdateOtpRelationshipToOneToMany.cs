using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOtpRelationshipToOneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Otps_UserId",
                schema: "Otp",
                table: "Otps");

            migrationBuilder.CreateIndex(
                name: "IX_Otps_UserId",
                schema: "Otp",
                table: "Otps",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Otps_UserId",
                schema: "Otp",
                table: "Otps");

            migrationBuilder.CreateIndex(
                name: "IX_Otps_UserId",
                schema: "Otp",
                table: "Otps",
                column: "UserId",
                unique: true);
        }
    }
}
