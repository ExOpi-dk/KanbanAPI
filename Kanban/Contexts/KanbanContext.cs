using Kanban.Models;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Contexts
{
    public class KanbanContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<Board> Boards { get; set; }

        public KanbanContext() : base() { }

        public KanbanContext(DbContextOptions<KanbanContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=Kanban");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Board>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.OwnerId).IsRequired();
                entity.HasOne(e => e.Owner)
                    .WithMany()
                    .HasForeignKey(e => e.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Story>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.OwnerId).IsRequired();
                entity.HasOne(e => e.Owner)
                    .WithMany()
                    .HasForeignKey(e => e.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.Property(e => e.BoardId).IsRequired();
                entity.HasOne(e => e.Board)
                    .WithMany()
                    .HasForeignKey(e => e.BoardId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.Property(e => e.AssigneeIds).IsRequired(false);
                entity.HasMany(e => e.Assignees)
                    .WithMany();
                entity.Property(e => e.StatusId).IsRequired(false);
                entity.HasOne(e => e.Status)
                    .WithMany()
                    .HasForeignKey(e => e.StatusId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.Property(e => e.Description).IsRequired(false);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });
        }
    }
}
