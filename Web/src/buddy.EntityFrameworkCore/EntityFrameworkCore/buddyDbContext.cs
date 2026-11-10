// ──────────────────────────────────────────────────────────────
//  buddyDbContext  – FULL VERSION (ALL mappings, no FK warnings)
// ──────────────────────────────────────────────────────────────
using Abp.Zero.EntityFrameworkCore;
using buddy.Authorization.Roles;
using buddy.Authorization.Users;
using buddy.MultiTenancy;
using buddy.Entities.Adoptions;
using buddy.Entities.DiseaseandDiagnosis;
using buddy.Entities.Facilities;
using buddy.Entities.InteractionsandRecords;
using buddy.Entities.Pets;
using buddy.Entities.Ratings;
using buddy.Entities.UserProfiles;
using Microsoft.EntityFrameworkCore;

namespace buddy.EntityFrameworkCore
{
    public class buddyDbContext : AbpZeroDbContext<Tenant, Role, User, buddyDbContext>
    {
        public buddyDbContext(DbContextOptions<buddyDbContext> options) : base(options) { }

        /* ───── DbSets ───────────────────────────────────────── */
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<AIDiagnosis> AIDiagnoses { get; set; }
        public DbSet<VeterinarianDiagnosis> VeterinarianDiagnoses { get; set; }

        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Clinic> Clinics { get; set; }

        public DbSet<Record> Records { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Survey> Surveys { get; set; }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<Adoption> Adoptions { get; set; }

        public DbSet<Rating> Ratings { get; set; }
        public DbSet<ClinicRating> ClinicRatings { get; set; }
        public DbSet<DiagnosisRating> DiagnosisRatings { get; set; }
        public DbSet<RecordRating> RecordRatings { get; set; }
        public DbSet<VeterinarianRating> VeterinarianRatings { get; set; }

        public DbSet<AdminProfile> AdminProfiles { get; set; }
        public DbSet<PetOwnerProfile> PetOwnerProfiles { get; set; }
        public DbSet<VeterinarianProfile> VeterinarianProfiles { get; set; }

        /* ───── model mapping ───────────────────────────────── */
        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            // ======= TPH Inheritance ====================================
            mb.Entity<Facility>().ToTable("Facilities");
            mb.Entity<Clinic>().HasBaseType<Facility>();

            mb.Entity<Diagnosis>().ToTable("Diagnoses");
            mb.Entity<AIDiagnosis>().HasBaseType<Diagnosis>();
            mb.Entity<VeterinarianDiagnosis>().HasBaseType<Diagnosis>();

            mb.Entity<Rating>().ToTable("Ratings");
            mb.Entity<ClinicRating>().HasBaseType<Rating>();
            mb.Entity<DiagnosisRating>().HasBaseType<Rating>();
            mb.Entity<RecordRating>().HasBaseType<Rating>();
            mb.Entity<VeterinarianRating>().HasBaseType<Rating>();

            // ======= 1‑1 Profile ↔ User =================================
            mb.Entity<AdminProfile>()
              .HasOne(p => p.User).WithOne()
              .HasForeignKey<AdminProfile>(p => p.UserId);

            mb.Entity<PetOwnerProfile>()
              .HasOne(p => p.User).WithOne()
              .HasForeignKey<PetOwnerProfile>(p => p.UserId);

            mb.Entity<VeterinarianProfile>()
              .HasOne(p => p.User).WithOne()
              .HasForeignKey<VeterinarianProfile>(p => p.UserId);

            // ======= Clinic optional on VeterinarianProfile 1‑N =========
            mb.Entity<VeterinarianProfile>()
              .HasOne(v => v.Clinic)
              .WithMany(c => c.Veterinarians)          // add ICollection<VeterinarianProfile> in Clinic
              .HasForeignKey(v => v.ClinicId)
              .OnDelete(DeleteBehavior.SetNull);

            // ======= Record 1‑1 ↔ AIDiagnosis ===========================
            mb.Entity<AIDiagnosis>()
              .HasOne(ad => ad.Record)
              .WithOne(r => r.AIDiagnosis)
              .HasForeignKey<AIDiagnosis>(ad => ad.RecordId)
              .OnDelete(DeleteBehavior.Cascade);

            // ======= Record 1‑1 ↔ VeterinarianDiagnosis ================
            mb.Entity<VeterinarianDiagnosis>()
              .HasOne(vd => vd.Record)
              .WithOne(r => r.VeterinarianDiagnosis)
              .HasForeignKey<VeterinarianDiagnosis>(vd => vd.RecordId)
              .OnDelete(DeleteBehavior.Cascade);

            // ======= DiagnosisRating links (each side optional) =========
            mb.Entity<DiagnosisRating>()
              .HasOne(dr => dr.AIDiagnosis)
              .WithMany(ad => ad.DiagnosisRatings)
              .HasForeignKey(dr => dr.AIDiagnosisId)
              .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<DiagnosisRating>()
              .HasOne(dr => dr.VeterinarianDiagnosis)
              .WithMany(vd => vd.DiagnosisRatings)
              .HasForeignKey(dr => dr.VeterinarianDiagnosisId)
              .OnDelete(DeleteBehavior.Restrict);

            // ======= PetOwnerProfile 1‑N Pets ===========================
            mb.Entity<Pet>()
              .HasOne(p => p.PetOwner)
              .WithMany(po => po.Pets)
              .HasForeignKey(p => p.UserId)
              .OnDelete(DeleteBehavior.Restrict);

            // ======= Pet 1‑N Records / Appointments ====================
            mb.Entity<Record>()
              .HasOne(r => r.Pet)
              .WithMany(p => p.Records)
              .HasForeignKey(r => r.PetId)
              .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Appointment>()
              .HasOne(a => a.Pet)
              .WithMany(p => p.Appointments)
              .HasForeignKey(a => a.PetId)
              .OnDelete(DeleteBehavior.Restrict);

            // ======= Record → PetOwner / Vet / Clinic ===================
            mb.Entity<Record>()
              .HasOne(r => r.PetOwner)
              .WithMany(po => po.Records)
              .HasForeignKey(r => r.PetOwnerUserId)
              .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Record>()
              .HasOne(r => r.Veterinarian)
              .WithMany(v => v.Records)
              .HasForeignKey(r => r.VetUserId)
              .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Record>()
              .HasOne(r => r.Clinic)
              .WithMany(c => c.Records)
              .HasForeignKey(r => r.ClinicId)
              .OnDelete(DeleteBehavior.Restrict);

            // ======= Appointment foreign‑keys (clinic, vet, owner) ======
            mb.Entity<Appointment>()
              .HasOne(a => a.PetOwner)
              .WithMany(po => po.Appointments)
              .HasForeignKey(a => a.PetOwnerUserId)
              .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Appointment>()
              .HasOne(a => a.Veterinarian)
              .WithMany(v => v.Appointments)
              .HasForeignKey(a => a.VetUserId)
              .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Appointment>()
              .HasOne(a => a.Clinic)
              .WithMany(c => c.Appointments)
              .HasForeignKey(a => a.ClinicId)
              .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Appointment>() // optional links
              .HasOne(a => a.Record)
              .WithMany()
              .HasForeignKey(a => a.RecordId)
              .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Appointment>()
              .HasOne(a => a.Survey)
              .WithMany()
              .HasForeignKey(a => a.SurveyId)
              .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Appointment>()
              .HasOne(a => a.ParentAppointment)
              .WithMany()
              .HasForeignKey(a => a.AppointmentId)
              .OnDelete(DeleteBehavior.Restrict);

            // ======= Rating base FK to PetOwner =========================
            mb.Entity<Rating>()
              .HasOne(r => r.PetOwner)
              .WithMany(po => po.Ratings)
              .HasForeignKey(r => r.PetOwnerUserId)
              .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<ClinicRating>()
              .HasOne(cr => cr.Clinic)
              .WithMany(c => c.ClinicRatings)
              .HasForeignKey(cr => cr.ClinicId)
              .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<RecordRating>()
              .HasOne(rr => rr.Record)
              .WithMany()
              .HasForeignKey(rr => rr.RecordId)
              .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<VeterinarianRating>()
              .HasOne(vr => vr.VeterinarianProfile)
              .WithMany()
              .HasForeignKey(vr => vr.VetUserId)
              .OnDelete(DeleteBehavior.Restrict);

            // ======= Primary Keys (explicit) ============================
            mb.Entity<Facility>().HasKey(e => e.Id);
            mb.Entity<Diagnosis>().HasKey(e => e.Id);
            mb.Entity<Rating>().HasKey(e => e.Id);
            mb.Entity<Record>().HasKey(e => e.Id);
            mb.Entity<Appointment>().HasKey(e => e.Id);
            mb.Entity<Pet>().HasKey(e => e.Id);
            mb.Entity<AdminProfile>().HasKey(e => e.Id);
            mb.Entity<PetOwnerProfile>().HasKey(e => e.Id);
            mb.Entity<VeterinarianProfile>().HasKey(e => e.Id);
        }
    }
}
