using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw11.Models
{
    public class CodeFirstContext : DbContext
    {
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<Medicament> Medicament { get; set; }
        public DbSet<PrescriptionMedicament> PrescriptionMedicament { get; set; }
        public DbSet<Doctor> Doctor { get; set; }

        public CodeFirstContext(DbContextOptions<CodeFirstContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.IdPatient).HasName("Patient_PK");

                entity.Property(e => e.IdPatient).ValueGeneratedNever();
                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Birthdate).IsRequired();
            });

            modelBuilder.Entity<Prescription>(entity => {
                entity.HasKey(e => e.IdPrescription).HasName("Prescription_PK");

                entity.Property(e => e.IdPrescription).ValueGeneratedNever();
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.DueDate).IsRequired();

                entity.HasOne(d => d.Patient)
                .WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.IdPatient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Prescription_Patient");
            });
            
            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.IdMedicament).HasName("Medicament_PK");
                entity.Property(e => e.IdMedicament).ValueGeneratedNever();
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Type).IsRequired();
            });
            
            modelBuilder.Entity<PrescriptionMedicament>(entity =>
            {

                entity.ToTable("PrescriptionMedicament");
                entity.HasKey(e => e.IdMedicament).HasName("PrescriptionMedicament_PK");
                entity.HasKey(e => e.IdPrescription).HasName("PrescriptionMedicament_PK");
                entity.Property(e => e.IdMedicament).ValueGeneratedNever();
                entity.Property(e => e.IdPrescription).ValueGeneratedNever();
                entity.Property(e => e.Details).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Dose).IsRequired();

                entity.HasOne(d => d.Medicament).
                WithMany(p => p.PrescriptionMedicaments).
                HasForeignKey(d => d.IdMedicament).
                OnDelete(DeleteBehavior.ClientSetNull).
                HasConstraintName("PrescriptionMedicament_Medicament");

                entity.HasOne(d => d.Prescription).
                WithMany(p => p.PrescriptionMedicaments).
                HasForeignKey(d => d.IdPrescription).
                OnDelete(DeleteBehavior.ClientSetNull).
                HasConstraintName("PrescriptionMedicament_Prescription");
            });
            
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.IdDoctor).HasName("Doctor_PK");
                entity.Property(e => e.IdDoctor).ValueGeneratedNever();
                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
            });

            //seeding method example
            modelBuilder.Entity<Doctor>().HasData(
            new Doctor { IdDoctor = 1, FirstName = "Igor", LastName = "Kowalski", Email = "i.kowalski@ik.com"},
            new Doctor { IdDoctor = 2, FirstName = "Grzegorz", LastName = "Fulara", Email = "g.fularai@gf.com" },
            new Doctor { IdDoctor = 3, FirstName = "Artur", LastName = "Gajowniczek", Email = "a.g@ag.com" }
            );
        }
    }
}
