using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace buddy.Migrations
{
    /// <inheritdoc />
    public partial class VeterinarianProfileClinicUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClinicId",
                table: "VeterinarianProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VeterinarianProfiles_ClinicId",
                table: "VeterinarianProfiles",
                column: "ClinicId");

            migrationBuilder.AddForeignKey(
                name: "FK_VeterinarianProfiles_Clinics_ClinicId",
                table: "VeterinarianProfiles",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VeterinarianProfiles_Clinics_ClinicId",
                table: "VeterinarianProfiles");

            migrationBuilder.DropIndex(
                name: "IX_VeterinarianProfiles_ClinicId",
                table: "VeterinarianProfiles");

            migrationBuilder.DropColumn(
                name: "ClinicId",
                table: "VeterinarianProfiles");
        }
    }
}
