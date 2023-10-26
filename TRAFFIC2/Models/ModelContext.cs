using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TRAFFIC2.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AboutU> AboutUs { get; set; }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<ContactU> ContactUs { get; set; }

    public virtual DbSet<HomePage> HomePages { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Testimonial> Testimonials { get; set; }

    public virtual DbSet<Userinformation> Userinformations { get; set; }

    public virtual DbSet<Violation> Violations { get; set; }

    public virtual DbSet<Violationsinquiry> Violationsinquiries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=DESKTOP-2592E5L)(PORT=1521))(CONNECT_DATA=(SID=xe)));User Id=C##TALA;Password=Test321;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("C##TALA")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<AboutU>(entity =>
        {
            entity.HasKey(e => e.AboutId).HasName("SYS_C008532");

            entity.ToTable("ABOUT_US");

            entity.Property(e => e.AboutId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ABOUT_ID");
            entity.Property(e => e.Content)
                .HasColumnType("CLOB")
                .HasColumnName("CONTENT");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TITLE");
        });

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.CarId).HasName("SYS_C008521");

            entity.ToTable("CAR");

            entity.Property(e => e.CarId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CAR_ID");
            entity.Property(e => e.Carname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CARNAME");
            entity.Property(e => e.Color)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("COLOR");
            entity.Property(e => e.LicensePlate)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("LICENSE_PLATE");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MODEL");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");
            entity.Property(e => e.Year)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("YEAR");

            entity.HasOne(d => d.User).WithMany(p => p.Cars)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_CAR_USERINFORMATION_ID");
        });

        modelBuilder.Entity<ContactU>(entity =>
        {
            entity.HasKey(e => e.ContactId).HasName("SYS_C008534");

            entity.ToTable("CONTACT_US");

            entity.Property(e => e.ContactId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CONTACT_ID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Firstname)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("FIRSTNAME");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PHONE_NUMBER");
        });

        modelBuilder.Entity<HomePage>(entity =>
        {
            entity.HasKey(e => e.HomeId).HasName("SYS_C008541");

            entity.ToTable("HOME_PAGE");

            entity.Property(e => e.HomeId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOME_ID");
            entity.Property(e => e.AboutId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ABOUT_ID");
            entity.Property(e => e.ContactId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CONTACT_ID");
            entity.Property(e => e.Content)
                .HasColumnType("CLOB")
                .HasColumnName("CONTENT");
            entity.Property(e => e.ImageId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("IMAGE_ID");
            entity.Property(e => e.TestimonialId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TESTIMONIAL_ID");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TITLE");

            entity.HasOne(d => d.About).WithMany(p => p.HomePages)
                .HasForeignKey(d => d.AboutId)
                .HasConstraintName("FK_HOMEPAGE_ABOUTUS");

            entity.HasOne(d => d.Contact).WithMany(p => p.HomePages)
                .HasForeignKey(d => d.ContactId)
                .HasConstraintName("FK_HOMEPAGE_CONTACTUS");

            entity.HasOne(d => d.Image).WithMany(p => p.HomePages)
                .HasForeignKey(d => d.ImageId)
                .HasConstraintName("FK_HOMEPAGE_IMAGE");

            entity.HasOne(d => d.Testimonial).WithMany(p => p.HomePages)
                .HasForeignKey(d => d.TestimonialId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_HOMEPAGE_TESTIMONIAL");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("SYS_C008539");

            entity.ToTable("IMAGE");

            entity.Property(e => e.ImageId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("IMAGE_ID");
            entity.Property(e => e.HomePageId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOME_PAGE_ID");
            entity.Property(e => e.ImagePath1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IMAGE_PATH1");
            entity.Property(e => e.ImagePath2)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IMAGE_PATH2");
            entity.Property(e => e.ImagePath3)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IMAGE_PATH3");
            entity.Property(e => e.ImagePath4)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IMAGE_PATH4");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.LoginId).HasName("SYS_C008514");

            entity.ToTable("LOGIN");

            entity.Property(e => e.LoginId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("LOGIN_ID");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PHONE_NUMBER");
            entity.Property(e => e.RoleId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("USERNAME");

            entity.HasOne(d => d.Role).WithMany(p => p.Logins)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_LOGIN_ROLE_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Logins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_LOGIN_USERINFORMATION_ID");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("SYS_C008528");

            entity.ToTable("PAYMENT");

            entity.Property(e => e.PaymentId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PAYMENT_ID");
            entity.Property(e => e.AmountPaid)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("AMOUNT_PAID");
            entity.Property(e => e.PaymentDate)
                .HasColumnType("DATE")
                .HasColumnName("PAYMENT_DATE");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAYMENT_METHOD");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PAYMENT_STATUS");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TRANSACTION_ID");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");
            entity.Property(e => e.ViolationId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("VIOLATION_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_PAYMENT_USERINFORMATION_ID");

            entity.HasOne(d => d.Violation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.ViolationId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_PAYMENT_VIOLATION_ID");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("SYS_C008510");

            entity.ToTable("ROLE");

            entity.Property(e => e.RoleId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ROLE_NAME");
        });

        modelBuilder.Entity<Testimonial>(entity =>
        {
			entity.HasKey(e => e.TestimonialId).HasName("SYS_C008518");

            entity.ToTable("TESTIMONIAL");

            entity.Property(e => e.TestimonialId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TESTIMONIAL_ID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("STATUS");
            entity.Property(e => e.TestimonialText)
                .HasColumnType("CLOB")
                .HasColumnName("TESTIMONIAL_TEXT");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Testimonials)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_TESTIMONIAL_USERINFORMATION_ID");
        });

        modelBuilder.Entity<Userinformation>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("SYS_C008512");

            entity.ToTable("USERINFORMATION");

            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("DATE")
                .HasColumnName("DATE_OF_BIRTH");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LAST_NAME");
        });

        modelBuilder.Entity<Violation>(entity =>
        {
            entity.HasKey(e => e.ViolationId).HasName("SYS_C008524");

            entity.ToTable("VIOLATION");

            entity.Property(e => e.ViolationId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("VIOLATION_ID");
            entity.Property(e => e.AdditionalNotes)
                .HasColumnType("CLOB")
                .HasColumnName("ADDITIONAL_NOTES");
            entity.Property(e => e.CarId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CAR_ID");
            entity.Property(e => e.Description)
                .HasColumnType("CLOB")
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("LOCATION");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");
            entity.Property(e => e.ViolationDate)
                .HasColumnType("DATE")
                .HasColumnName("VIOLATION_DATE");

            entity.HasOne(d => d.Car).WithMany(p => p.Violations)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_VIOLATION_CAR_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Violations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_VIOLATION_USER_ID");
        });

        modelBuilder.Entity<Violationsinquiry>(entity =>
        {
            entity.HasKey(e => e.EnquiryId).HasName("SYS_C008536");

            entity.ToTable("VIOLATIONSINQUIRY");

            entity.Property(e => e.EnquiryId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ENQUIRY_ID");
            entity.Property(e => e.CarId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CAR_ID");

            entity.HasOne(d => d.Car).WithMany(p => p.Violationsinquiries)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_INQUIRY_CAR_ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
