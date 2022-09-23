using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SmartMeterConsumerManagement.Models.DBContext
{
    public partial class SMCM_DBContext : DbContext
    {
        public SMCM_DBContext(DbContextOptions<SMCM_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<Complaint> Complaints { get; set; }
        public virtual DbSet<UserRequest> UserRequests { get; set; }
        public virtual DbSet<SmartMeter> SmartMeters { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserLocation> UserLocations { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=ITT-SACHINSVAZE\\SQLEXPRESS;Initial Catalog=SMCM_DB;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValue(null);

                entity.Property(e => e.ConsumerEmailId)
                    .HasMaxLength(100)
                    .IsFixedLength(true);

                entity.Property(e => e.CurrentBillingMonth)
                    .HasMaxLength(4)
                    .IsFixedLength(true);

                entity.Property(e => e.Date)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentBillingMonth);

                entity.Property(e => e.CurrentBillingAmount).HasDefaultValue(0.0);

                entity.Property(e => e.PreviousReadingUnit).HasDefaultValue(0.0);

                entity.Property(e => e.CurrentReadingUnit).HasDefaultValue(0.0);

                entity.Property(e => e.PreviousBillingAmount).HasDefaultValue(0.0);

                entity.Property(e => e.PaymentStatus)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentState)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ConsumerEmail)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.ConsumerEmailId)
                    .HasConstraintName("Fk_Bill_emailId");
            });

            modelBuilder.Entity<Complaint>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValue(null);

                entity.Property(e => e.ConsumerEmailId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsFixedLength(true);

                entity.Property(e => e.Date)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Message)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ConsumerEmail)
                    .WithMany(p => p.Complaints)
                    .HasForeignKey(d => d.ConsumerEmailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Complaints_emailId");
            });

            modelBuilder.Entity<UserRequest>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValue(null);

                entity.Property(e => e.UserEmailId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsFixedLength(true);

                entity.Property(e => e.Date)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.ConsumerEmail)
                    .WithMany(p => p.UserRequests)
                    .HasForeignKey(d => d.UserEmailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_UserRequest");
            });

            modelBuilder.Entity<SmartMeter>(entity =>
            {
                entity.Property(e => e.PurchaseDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.SmartMeters)
                    .HasForeignKey(d => d.VendorId)
                    .HasConstraintName("Fk_VendorID");
            });

            modelBuilder.Entity<UserLocation>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

                entity.Property(e => e.ApartmentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.AreaCode).HasDefaultValueSql("((100))");

                entity.Property(e => e.BlockNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.Property(e => e.City)
                    .HasMaxLength(80)
                    .IsFixedLength(true);

                entity.Property(e => e.Pincode)
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.Property(e => e.State)
                    .HasMaxLength(80)
                    .IsFixedLength(true);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.EmailAddress)
                    .HasName("PK__tmp_ms_x__49A147416E17D5A3");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(100)
                    .IsFixedLength(true);

                entity.Property(e => e.CreatedAt).IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.LanguageSelected)
                    .HasMaxLength(30)
                    .IsFixedLength(true);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.LocationId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsFixedLength(true);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_UserLocation");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
