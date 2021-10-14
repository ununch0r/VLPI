using Core.Entities;
using Microsoft.EntityFrameworkCore;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataAccess
{
    public partial class VlpiContext : DbContext
    {
        public VlpiContext()
        {
        }

        public VlpiContext(DbContextOptions<VlpiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ExecutionMode> ExecutionMode { get; set; }
        public virtual DbSet<Requirement> Requirement { get; set; }
        public virtual DbSet<RequirementType> RequirementType { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<StandardAnswer> StandardAnswer { get; set; }
        public virtual DbSet<Task> Task { get; set; }
        public virtual DbSet<TaskTip> TaskTip { get; set; }
        public virtual DbSet<TaskType> TaskType { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserAnswer> UserAnswer { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Requirement>(entity =>
            {
                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Requirement)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequirementAnalysisTaskContent_Task");
            });

            modelBuilder.Entity<StandardAnswer>(entity =>
            {
                entity.Property(e => e.TaskId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.StandardAnswer)
                    .WithOne(p => p.Task)
                    .HasForeignKey<Task>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task_StandardAnswer");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task_TaskType");
            });

            modelBuilder.Entity<TaskTip>(entity =>
            {
                entity.HasOne(d => d.Task)
                    .WithMany(p => p.TaskTip)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskTip_Task");
            });

            modelBuilder.Entity<UserAnswer>(entity =>
            {
                entity.HasOne(d => d.Task)
                    .WithMany(p => p.UserAnswer)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAnswer_Task");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAnswer)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
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
