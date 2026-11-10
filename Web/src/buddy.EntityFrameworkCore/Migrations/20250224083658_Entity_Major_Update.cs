using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace buddy.Migrations
{
    /// <inheritdoc />
    public partial class Entity_Major_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Access_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId1 = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminProfiles_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdminProfiles_AbpUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Diagnoses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordId = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnoses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Average_Rate = table.Column<float>(type: "real", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PetOwnerProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Birth_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetOwnerProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PetOwnerProfiles_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PetOwnerProfiles_AbpUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VeterinarianProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Access_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId1 = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeterinarianProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VeterinarianProfiles_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VeterinarianProfiles_AbpUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

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
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Species = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<float>(type: "real", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    Is_vaccinated = table.Column<bool>(type: "bit", nullable: false),
                    Race = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PetOwnerProfileId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_PetOwnerProfiles_PetOwnerProfileId",
                        column: x => x.PetOwnerProfileId,
                        principalTable: "PetOwnerProfiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pets_PetOwnerProfiles_UserId",
                        column: x => x.UserId,
                        principalTable: "PetOwnerProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetOwnerUserId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<float>(type: "real", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PetOwnerProfileId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_PetOwnerProfiles_PetOwnerProfileId1",
                        column: x => x.PetOwnerProfileId1,
                        principalTable: "PetOwnerProfiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ratings_PetOwnerProfiles_PetOwnerUserId",
                        column: x => x.PetOwnerUserId,
                        principalTable: "PetOwnerProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VeterinarianDiagnoses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    VetUserId = table.Column<int>(type: "int", nullable: false),
                    Treatment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "Surveys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Surveys_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
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
                name: "AIDiagnoses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DiagnosisRatingId = table.Column<int>(type: "int", nullable: false),
                    Cost_Estimation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosisRatingId1 = table.Column<int>(type: "int", nullable: true)
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
                    VeterinarianDiagnosisId = table.Column<int>(type: "int", nullable: false),
                    AIDiagnosisId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Records",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetId = table.Column<int>(type: "int", nullable: false),
                    PetOwnerUserId = table.Column<int>(type: "int", nullable: false),
                    VetUserId = table.Column<int>(type: "int", nullable: false),
                    ClinicId = table.Column<int>(type: "int", nullable: false),
                    VeterinarianDiagnosisId = table.Column<int>(type: "int", nullable: true),
                    AIDiagnosisId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Video = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Records_AIDiagnoses_AIDiagnosisId",
                        column: x => x.AIDiagnosisId,
                        principalTable: "AIDiagnoses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Records_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Records_PetOwnerProfiles_PetOwnerUserId",
                        column: x => x.PetOwnerUserId,
                        principalTable: "PetOwnerProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Records_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Records_VeterinarianDiagnoses_VeterinarianDiagnosisId",
                        column: x => x.VeterinarianDiagnosisId,
                        principalTable: "VeterinarianDiagnoses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Records_VeterinarianProfiles_VetUserId",
                        column: x => x.VetUserId,
                        principalTable: "VeterinarianProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetId = table.Column<int>(type: "int", nullable: false),
                    PetOwnerUserId = table.Column<int>(type: "int", nullable: false),
                    VetUserId = table.Column<int>(type: "int", nullable: false),
                    ClinicId = table.Column<int>(type: "int", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: true),
                    RecordId = table.Column<int>(type: "int", nullable: true),
                    SurveyId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_PetOwnerProfiles_PetOwnerUserId",
                        column: x => x.PetOwnerUserId,
                        principalTable: "PetOwnerProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Records_RecordId",
                        column: x => x.RecordId,
                        principalTable: "Records",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_VeterinarianProfiles_VetUserId",
                        column: x => x.VetUserId,
                        principalTable: "VeterinarianProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateIndex(
                name: "IX_AdminProfiles_UserId",
                table: "AdminProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdminProfiles_UserId1",
                table: "AdminProfiles",
                column: "UserId1",
                unique: true,
                filter: "[UserId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AIDiagnoses_DiagnosisRatingId1",
                table: "AIDiagnoses",
                column: "DiagnosisRatingId1");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointmentId",
                table: "Appointments",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ClinicId",
                table: "Appointments",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PetId",
                table: "Appointments",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PetOwnerUserId",
                table: "Appointments",
                column: "PetOwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_RecordId",
                table: "Appointments",
                column: "RecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_SurveyId",
                table: "Appointments",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_VetUserId",
                table: "Appointments",
                column: "VetUserId");

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
                name: "IX_PetOwnerProfiles_UserId",
                table: "PetOwnerProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PetOwnerProfiles_UserId1",
                table: "PetOwnerProfiles",
                column: "UserId1",
                unique: true,
                filter: "[UserId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_PetOwnerProfileId",
                table: "Pets",
                column: "PetOwnerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_UserId",
                table: "Pets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_PetOwnerProfileId1",
                table: "Ratings",
                column: "PetOwnerProfileId1");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_PetOwnerUserId",
                table: "Ratings",
                column: "PetOwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordRatings_RecordId",
                table: "RecordRatings",
                column: "RecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_AIDiagnosisId",
                table: "Records",
                column: "AIDiagnosisId",
                unique: true,
                filter: "[AIDiagnosisId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Records_ClinicId",
                table: "Records",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_PetId",
                table: "Records",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_PetOwnerUserId",
                table: "Records",
                column: "PetOwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_VeterinarianDiagnosisId",
                table: "Records",
                column: "VeterinarianDiagnosisId",
                unique: true,
                filter: "[VeterinarianDiagnosisId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Records_VetUserId",
                table: "Records",
                column: "VetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_PetId",
                table: "Surveys",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_VeterinarianDiagnoses_VeterinarianProfileId",
                table: "VeterinarianDiagnoses",
                column: "VeterinarianProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_VeterinarianDiagnoses_VetUserId",
                table: "VeterinarianDiagnoses",
                column: "VetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VeterinarianProfiles_UserId",
                table: "VeterinarianProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VeterinarianProfiles_UserId1",
                table: "VeterinarianProfiles",
                column: "UserId1",
                unique: true,
                filter: "[UserId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_VeterinarianRatings_VetUserId",
                table: "VeterinarianRatings",
                column: "VetUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AIDiagnoses_DiagnosisRatings_DiagnosisRatingId1",
                table: "AIDiagnoses",
                column: "DiagnosisRatingId1",
                principalTable: "DiagnosisRatings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AIDiagnoses_Diagnoses_Id",
                table: "AIDiagnoses");

            migrationBuilder.DropForeignKey(
                name: "FK_VeterinarianDiagnoses_Diagnoses_Id",
                table: "VeterinarianDiagnoses");

            migrationBuilder.DropForeignKey(
                name: "FK_AIDiagnoses_DiagnosisRatings_DiagnosisRatingId1",
                table: "AIDiagnoses");

            migrationBuilder.DropTable(
                name: "AdminProfiles");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "ClinicRatings");

            migrationBuilder.DropTable(
                name: "RecordRatings");

            migrationBuilder.DropTable(
                name: "VeterinarianRatings");

            migrationBuilder.DropTable(
                name: "Surveys");

            migrationBuilder.DropTable(
                name: "Records");

            migrationBuilder.DropTable(
                name: "Clinics");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "Diagnoses");

            migrationBuilder.DropTable(
                name: "DiagnosisRatings");

            migrationBuilder.DropTable(
                name: "AIDiagnoses");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "VeterinarianDiagnoses");

            migrationBuilder.DropTable(
                name: "PetOwnerProfiles");

            migrationBuilder.DropTable(
                name: "VeterinarianProfiles");
        }
    }
}
