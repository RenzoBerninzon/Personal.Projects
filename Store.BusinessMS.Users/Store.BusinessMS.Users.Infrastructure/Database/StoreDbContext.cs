using Microsoft.EntityFrameworkCore;
using Store.BusinessMS.Users.Domain.Otp;
using Store.BusinessMS.Users.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessMS.Users.Infrastructure.Database
{
    public partial class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }

        public virtual DbSet<ApplicationUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<Otp> OTP { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(entity =>
            {

                entity.HasIndex(e => e.DocTypeId, "IX_DocTypeId");

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.Gender).HasMaxLength(1);

                entity.Property(e => e.LastName).UseCollation("Modern_Spanish_CI_AI");

                entity.Property(e => e.MothersLastName).UseCollation("Modern_Spanish_CI_AI");

                entity.Property(e => e.Name).UseCollation("Modern_Spanish_CI_AI");

            });

            modelBuilder.Entity<Otp>(entity =>
            {
                entity.ToTable("Otps", "Otp");

                entity.HasIndex(e => new { e.UserId })
                    .IsUnique();

                entity.Property(e => e.Code).HasMaxLength(5);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
