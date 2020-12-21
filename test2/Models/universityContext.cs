using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace University.Models
{
    public partial class universityContext : DbContext
    {
        public universityContext()
        {
        }

        public universityContext(DbContextOptions<universityContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AcademicDegree> AcademicDegree { get; set; }
        public virtual DbSet<AcademicGroup> AcademicGroup { get; set; }
        public virtual DbSet<AcademicTitle> AcademicTitle { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Direction> Direction { get; set; }
        public virtual DbSet<Gqw> Gqw { get; set; }
        public virtual DbSet<Graduate> Graduate { get; set; }
        public virtual DbSet<Institute> Institute { get; set; }
        public virtual DbSet<Partner> Partner { get; set; }
        public virtual DbSet<Sec> Sec { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=inu07040;database=university", x => x.ServerVersion("8.0.19-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcademicDegree>(entity =>
            {
                entity.ToTable("academic_degree");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Degree)
                    .HasColumnName("degree")
                    .HasColumnType("varchar(128)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<AcademicGroup>(entity =>
            {
                entity.ToTable("academic_group");

                entity.HasIndex(e => e.DirectionId)
                    .HasName("direction_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DirectionId).HasColumnName("direction_id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Direction)
                    .WithMany(p => p.AcademicGroup)
                    .HasForeignKey(d => d.DirectionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("academic_group_ibfk_1");
            });

            modelBuilder.Entity<AcademicTitle>(entity =>
            {
                entity.ToTable("academic_title");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("varchar(128)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("company");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasColumnType("varchar(128)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(128)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasColumnType("varchar(16)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Site)
                    .HasColumnName("site")
                    .HasColumnType("varchar(128)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department");

                entity.HasIndex(e => e.InstituteId)
                    .HasName("institute_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.InstituteId).HasColumnName("institute_id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(128)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Institute)
                    .WithMany(p => p.Department)
                    .HasForeignKey(d => d.InstituteId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("department_ibfk_1");
            });

            modelBuilder.Entity<Direction>(entity =>
            {
                entity.ToTable("direction");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("department_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.FormOfEducation)
                    .HasColumnName("form_of_education")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NumberOfDirection)
                    .HasColumnName("number_of_direction")
                    .HasColumnType("varchar(8)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Direction)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("direction_ibfk_1");
            });

            modelBuilder.Entity<Gqw>(entity =>
            {
                entity.ToTable("gqw");

                entity.HasIndex(e => e.GraduateId)
                    .HasName("graduate_id");

                entity.HasIndex(e => e.ReviewerId)
                    .HasName("reviewer_id");

                entity.HasIndex(e => e.SecId)
                    .HasName("sec_id");

                entity.HasIndex(e => e.TeacherId)
                    .HasName("teacher_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateOfDefence)
                    .HasColumnName("date_of_defence")
                    .HasColumnType("date");

                entity.Property(e => e.Grade).HasColumnName("grade");

                entity.Property(e => e.GraduateId).HasColumnName("graduate_id");

                entity.Property(e => e.IsArchived).HasColumnName("is_archived");

                entity.Property(e => e.ProtocolNumber).HasColumnName("protocol_number");

                entity.Property(e => e.ReviewerGrade).HasColumnName("reviewer_grade");

                entity.Property(e => e.ReviewerId).HasColumnName("reviewer_id");

                entity.Property(e => e.SecId).HasColumnName("sec_id");

                entity.Property(e => e.TeacherId).HasColumnName("teacher_id");

                entity.Property(e => e.Theme)
                    .HasColumnName("theme")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Graduate)
                    .WithMany(p => p.Gqw)
                    .HasForeignKey(d => d.GraduateId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("gqw_ibfk_1");

                entity.HasOne(d => d.Reviewer)
                    .WithMany(p => p.Gqw)
                    .HasForeignKey(d => d.ReviewerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("gqw_ibfk_2");

                entity.HasOne(d => d.Sec)
                    .WithMany(p => p.Gqw)
                    .HasForeignKey(d => d.SecId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("gqw_ibfk_4");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Gqw)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("gqw_ibfk_3");
            });

            modelBuilder.Entity<Graduate>(entity =>
            {
                entity.ToTable("graduate");

                entity.HasIndex(e => e.AcademicDegreeId)
                    .HasName("academic_degree_id");

                entity.HasIndex(e => e.CompanyId)
                    .HasName("company_id");

                entity.HasIndex(e => e.GroupId)
                    .HasName("group_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AcademicDegreeId).HasColumnName("academic_degree_id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.CurrentCity)
                    .HasColumnName("current_city")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DisciplineLaboratoryWorks).HasColumnName("discipline_laboratory_works");

                entity.Property(e => e.DisciplineLecture).HasColumnName("discipline_lecture");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(128)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasColumnType("varchar(128)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasColumnType("varchar(128)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Patronymic)
                    .HasColumnName("patronymic")
                    .HasColumnType("varchar(128)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasColumnType("varchar(16)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.AcademicDegree)
                    .WithMany(p => p.Graduate)
                    .HasForeignKey(d => d.AcademicDegreeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("graduate_ibfk_3");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Graduate)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("graduate_ibfk_2");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Graduate)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("graduate_ibfk_1");
            });

            modelBuilder.Entity<Institute>(entity =>
            {
                entity.ToTable("institute");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(128)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Partner>(entity =>
            {
                entity.ToTable("partner");

                entity.HasIndex(e => e.AcademicDegreeId)
                    .HasName("academic_degree_id");

                entity.HasIndex(e => e.AcademicTitleId)
                    .HasName("academic_title_id");

                entity.HasIndex(e => e.CompanyId)
                    .HasName("company_id");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("department_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AcademicDegreeId).HasColumnName("academic_degree_id");

                entity.Property(e => e.AcademicTitleId).HasColumnName("academic_title_id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Patronymic)
                    .HasColumnName("patronymic")
                    .HasColumnType("varchar(54)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasColumnType("varchar(16)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.AcademicDegree)
                    .WithMany(p => p.Partner)
                    .HasForeignKey(d => d.AcademicDegreeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("partner_ibfk_3");

                entity.HasOne(d => d.AcademicTitle)
                    .WithMany(p => p.Partner)
                    .HasForeignKey(d => d.AcademicTitleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("partner_ibfk_2");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Partner)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("partner_ibfk_1");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Partner)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("partner_ibfk_4");
            });

            modelBuilder.Entity<Sec>(entity =>
            {
                entity.ToTable("sec");

                entity.HasIndex(e => e.ChairmanId)
                    .HasName("chairman_id");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("department_id");

                entity.HasIndex(e => e.SecretaryId)
                    .HasName("secretary_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChairmanId).HasColumnName("chairman_id");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.SecretaryId).HasColumnName("secretary_id");

                entity.Property(e => e.Year)
                    .HasColumnName("year")
                    .HasColumnType("date");

                entity.HasOne(d => d.Chairman)
                    .WithMany(p => p.Sec)
                    .HasForeignKey(d => d.ChairmanId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("sec_ibfk_2");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Sec)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("sec_ibfk_3");

                entity.HasOne(d => d.Secretary)
                    .WithMany(p => p.Sec)
                    .HasForeignKey(d => d.SecretaryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("sec_ibfk_1");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("teacher");

                entity.HasIndex(e => e.AcademicDegreeId)
                    .HasName("academic_degree_id");

                entity.HasIndex(e => e.AcademicTitleId)
                    .HasName("academic_title_id");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("department_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AcademicDegreeId).HasColumnName("academic_degree_id");

                entity.Property(e => e.AcademicTitleId).HasColumnName("academic_title_id");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Patronymic)
                    .HasColumnName("patronymic")
                    .HasColumnType("varchar(54)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasColumnType("varchar(16)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.AcademicDegree)
                    .WithMany(p => p.Teacher)
                    .HasForeignKey(d => d.AcademicDegreeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("teacher_ibfk_1");

                entity.HasOne(d => d.AcademicTitle)
                    .WithMany(p => p.Teacher)
                    .HasForeignKey(d => d.AcademicTitleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("teacher_ibfk_2");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Teacher)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("teacher_ibfk_3");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
