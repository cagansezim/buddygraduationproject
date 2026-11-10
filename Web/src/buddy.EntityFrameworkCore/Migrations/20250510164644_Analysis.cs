using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace buddy.Migrations
{
    /// <inheritdoc />
    public partial class Analysis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adoptions_Clinics_ClinicId",
                table: "Adoptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Clinics_ClinicId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_PetOwnerProfiles_PetOwnerProfileId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_PetOwnerProfiles_PetOwnerProfileId1",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_AIDiagnoses_AIDiagnosisId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Clinics_ClinicId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_VeterinarianDiagnoses_VeterinarianDiagnosisId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Pets_PetId",
                table: "Surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_VeterinarianProfiles_Clinics_ClinicId",
                table: "VeterinarianProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_AIDiagnoses_DiagnosisRatings_DiagnosisRatingId1",
                table: "AIDiagnoses");

            migrationBuilder.DropTable(
                name: "ClinicRatings");

            migrationBuilder.DropTable(
                name: "RecordRatings");

            migrationBuilder.DropTable(
                name: "VeterinarianRatings");

            migrationBuilder.DropTable(
                name: "Clinics");

            migrationBuilder.DropTable(
                name: "DiagnosisRatings");

            migrationBuilder.DropTable(
                name: "AIDiagnoses");

            migrationBuilder.DropTable(
                name: "VeterinarianDiagnoses");

            migrationBuilder.DropIndex(
                name: "IX_Records_AIDiagnosisId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_VeterinarianDiagnosisId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Pets_PetOwnerProfileId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "PetOwnerProfileId",
                table: "Pets");

            migrationBuilder.RenameColumn(
                name: "PetOwnerProfileId1",
                table: "Ratings",
                newName: "VeterinarianDiagnosisId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_PetOwnerProfileId1",
                table: "Ratings",
                newName: "IX_Ratings_VeterinarianDiagnosisId");

            migrationBuilder.AlterColumn<int>(
                name: "ClinicId",
                table: "VeterinarianProfiles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AIDiagnosisId",
                table: "Ratings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AccuracyScore",
                table: "Ratings",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClinicId",
                table: "Ratings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Ratings",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PetOwnerProfileId",
                table: "Ratings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecordId",
                table: "Ratings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VetUserId",
                table: "Ratings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Facilities",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cost_Estimation",
                table: "Diagnoses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Diagnoses",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TreatmentPlan",
                table: "Diagnoses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VetUserId",
                table: "Diagnoses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VeterinarianId",
                table: "Diagnoses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_AIDiagnosisId",
                table: "Ratings",
                column: "AIDiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ClinicId",
                table: "Ratings",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_PetOwnerProfileId",
                table: "Ratings",
                column: "PetOwnerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_RecordId",
                table: "Ratings",
                column: "RecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_VetUserId",
                table: "Ratings",
                column: "VetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnoses_RecordId",
                table: "Diagnoses",
                column: "RecordId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Diagnoses_VeterinarianId",
                table: "Diagnoses",
                column: "VeterinarianId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adoptions_Facilities_ClinicId",
                table: "Adoptions",
                column: "ClinicId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Facilities_ClinicId",
                table: "Appointments",
                column: "ClinicId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnoses_Records_RecordId",
                table: "Diagnoses",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnoses_VeterinarianProfiles_VeterinarianId",
                table: "Diagnoses",
                column: "VeterinarianId",
                principalTable: "VeterinarianProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Diagnoses_AIDiagnosisId",
                table: "Ratings",
                column: "AIDiagnosisId",
                principalTable: "Diagnoses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Diagnoses_VeterinarianDiagnosisId",
                table: "Ratings",
                column: "VeterinarianDiagnosisId",
                principalTable: "Diagnoses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Facilities_ClinicId",
                table: "Ratings",
                column: "ClinicId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_PetOwnerProfiles_PetOwnerProfileId",
                table: "Ratings",
                column: "PetOwnerProfileId",
                principalTable: "PetOwnerProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Records_RecordId",
                table: "Ratings",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_VeterinarianProfiles_VetUserId",
                table: "Ratings",
                column: "VetUserId",
                principalTable: "VeterinarianProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Facilities_ClinicId",
                table: "Records",
                column: "ClinicId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Pets_PetId",
                table: "Surveys",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VeterinarianProfiles_Facilities_ClinicId",
                table: "VeterinarianProfiles",
                column: "ClinicId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adoptions_Facilities_ClinicId",
                table: "Adoptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Facilities_ClinicId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Diagnoses_Records_RecordId",
                table: "Diagnoses");

            migrationBuilder.DropForeignKey(
                name: "FK_Diagnoses_VeterinarianProfiles_VeterinarianId",
                table: "Diagnoses");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Diagnoses_AIDiagnosisId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Diagnoses_VeterinarianDiagnosisId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Facilities_ClinicId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_PetOwnerProfiles_PetOwnerProfileId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Records_RecordId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_VeterinarianProfiles_VetUserId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Facilities_ClinicId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Pets_PetId",
                table: "Surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_VeterinarianProfiles_Facilities_ClinicId",
                table: "VeterinarianProfiles");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_AIDiagnosisId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_ClinicId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_PetOwnerProfileId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_RecordId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_VetUserId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Diagnoses_RecordId",
                table: "Diagnoses");

            migrationBuilder.DropIndex(
                name: "IX_Diagnoses_VeterinarianId",
                table: "Diagnoses");

            migrationBuilder.DropColumn(
                name: "AIDiagnosisId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "AccuracyScore",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "ClinicId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "PetOwnerProfileId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "RecordId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "VetUserId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "Cost_Estimation",
                table: "Diagnoses");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Diagnoses");

            migrationBuilder.DropColumn(
                name: "TreatmentPlan",
                table: "Diagnoses");

            migrationBuilder.DropColumn(
                name: "VetUserId",
                table: "Diagnoses");

            migrationBuilder.DropColumn(
                name: "VeterinarianId",
                table: "Diagnoses");

            migrationBuilder.RenameColumn(
                name: "VeterinarianDiagnosisId",
                table: "Ratings",
                newName: "PetOwnerProfileId1");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_VeterinarianDiagnosisId",
                table: "Ratings",
                newName: "IX_Ratings_PetOwnerProfileId1");

            migrationBuilder.AlterColumn<int>(
                name: "ClinicId",
                table: "VeterinarianProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PetOwnerProfileId",
                table: "Pets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Clinics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clinics_Facilities_Id",
                        column: x => x.Id,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecordRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    RecordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordRatings_Ratings_Id",
                        column: x => x.Id,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecordRatings_Records_RecordId",
                        column: x => x.RecordId,
                        principalTable: "Records",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VeterinarianDiagnoses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    VetUserId = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Treatment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VeterinarianProfileId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeterinarianDiagnoses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VeterinarianDiagnoses_Diagnoses_Id",
                        column: x => x.Id,
                        principalTable: "Diagnoses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VeterinarianDiagnoses_VeterinarianProfiles_VetUserId",
                        column: x => x.VetUserId,
                        principalTable: "VeterinarianProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VeterinarianDiagnoses_VeterinarianProfiles_VeterinarianProfileId",
                        column: x => x.VeterinarianProfileId,
                        principalTable: "VeterinarianProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VeterinarianRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    VetUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeterinarianRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VeterinarianRatings_Ratings_Id",
                        column: x => x.Id,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VeterinarianRatings_VeterinarianProfiles_VetUserId",
                        column: x => x.VetUserId,
                        principalTable: "VeterinarianProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClinicRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ClinicId = table.Column<int>(type: "int", nullable: false),
                    PetOwnerProfileId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClinicRatings_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClinicRatings_PetOwnerProfiles_PetOwnerProfileId",
                        column: x => x.PetOwnerProfileId,
                        principalTable: "PetOwnerProfiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClinicRatings_Ratings_Id",
                        column: x => x.Id,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AIDiagnoses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DiagnosisRatingId1 = table.Column<int>(type: "int", nullable: true),
                    Cost_Estimation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosisRatingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIDiagnoses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AIDiagnoses_Diagnoses_Id",
                        column: x => x.Id,
                        principalTable: "Diagnoses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosisRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AIDiagnosisId = table.Column<int>(type: "int", nullable: false),
                    VeterinarianDiagnosisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosisRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiagnosisRatings_AIDiagnoses_AIDiagnosisId",
                        column: x => x.AIDiagnosisId,
                        principalTable: "AIDiagnoses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiagnosisRatings_Ratings_Id",
                        column: x => x.Id,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiagnosisRatings_VeterinarianDiagnoses_VeterinarianDiagnosisId",
                        column: x => x.VeterinarianDiagnosisId,
                        principalTable: "VeterinarianDiagnoses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Records_AIDiagnosisId",
                table: "Records",
                column: "AIDiagnosisId",
                unique: true,
                filter: "[AIDiagnosisId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Records_VeterinarianDiagnosisId",
                table: "Records",
                column: "VeterinarianDiagnosisId",
                unique: true,
                filter: "[VeterinarianDiagnosisId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_PetOwnerProfileId",
                table: "Pets",
                column: "PetOwnerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_AIDiagnoses_DiagnosisRatingId1",
                table: "AIDiagnoses",
                column: "DiagnosisRatingId1");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicRatings_ClinicId",
                table: "ClinicRatings",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicRatings_PetOwnerProfileId",
                table: "ClinicRatings",
                column: "PetOwnerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisRatings_AIDiagnosisId",
                table: "DiagnosisRatings",
                column: "AIDiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisRatings_VeterinarianDiagnosisId",
                table: "DiagnosisRatings",
                column: "VeterinarianDiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordRatings_RecordId",
                table: "RecordRatings",
                column: "RecordId");

            migrationBuilder.CreateIndex(
                name: "IX_VeterinarianDiagnoses_VeterinarianProfileId",
                table: "VeterinarianDiagnoses",
                column: "VeterinarianProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_VeterinarianDiagnoses_VetUserId",
                table: "VeterinarianDiagnoses",
                column: "VetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VeterinarianRatings_VetUserId",
                table: "VeterinarianRatings",
                column: "VetUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adoptions_Clinics_ClinicId",
                table: "Adoptions",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Clinics_ClinicId",
                table: "Appointments",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_PetOwnerProfiles_PetOwnerProfileId",
                table: "Pets",
                column: "PetOwnerProfileId",
                principalTable: "PetOwnerProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_PetOwnerProfiles_PetOwnerProfileId1",
                table: "Ratings",
                column: "PetOwnerProfileId1",
                principalTable: "PetOwnerProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_AIDiagnoses_AIDiagnosisId",
                table: "Records",
                column: "AIDiagnosisId",
                principalTable: "AIDiagnoses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Clinics_ClinicId",
                table: "Records",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_VeterinarianDiagnoses_VeterinarianDiagnosisId",
                table: "Records",
                column: "VeterinarianDiagnosisId",
                principalTable: "VeterinarianDiagnoses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Pets_PetId",
                table: "Surveys",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VeterinarianProfiles_Clinics_ClinicId",
                table: "VeterinarianProfiles",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AIDiagnoses_DiagnosisRatings_DiagnosisRatingId1",
                table: "AIDiagnoses",
                column: "DiagnosisRatingId1",
                principalTable: "DiagnosisRatings",
                principalColumn: "Id");
        }
    }
}
