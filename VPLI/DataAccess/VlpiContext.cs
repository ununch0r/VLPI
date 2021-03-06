using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public partial class VLPIContext : DbContext
    {
        public VLPIContext()
        {
        }

        public VLPIContext(DbContextOptions<VLPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Continuation> Continuation { get; set; }
        public virtual DbSet<ExecutionMode> ExecutionMode { get; set; }
        public virtual DbSet<Explanation> Explanation { get; set; }
        public virtual DbSet<Requirement> Requirement { get; set; }
        public virtual DbSet<RequirementType> RequirementType { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Task> Task { get; set; }
        public virtual DbSet<TaskTip> TaskTip { get; set; }
        public virtual DbSet<TaskType> TaskType { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserAnswer> UserAnswer { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Continuation>(entity =>
            {
                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ExecutionMode>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Explanation>(entity =>
            {
                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Requirement>(entity =>
            {
                entity.Property(e => e.Description).IsRequired();

                entity.HasOne(d => d.Continuation)
                    .WithMany(p => p.Requirement)
                    .HasForeignKey(d => d.ContinuationId)
                    .HasConstraintName("FK_Requirement_Continuation");

                entity.HasOne(d => d.Explanation)
                    .WithMany(p => p.Requirement)
                    .HasForeignKey(d => d.ExplanationId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Requirement_Explanation");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Requirement)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_RequirementAnalysisTaskContent_Task");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Requirement)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Requirement_RequirementType");
            });

            modelBuilder.Entity<RequirementType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.Property(e => e.Objective)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task_TaskType");
            });

            modelBuilder.Entity<TaskTip>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.TaskTip)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_TaskTip_Task");
            });

            modelBuilder.Entity<TaskType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<UserAnswer>(entity =>
            {
                entity.Property(e => e.Answer).IsRequired();

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.UserAnswer)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_UserAnswer_Task");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAnswer)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserAnswer_User");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRole_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRole_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
