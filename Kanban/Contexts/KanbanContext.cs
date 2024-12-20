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
                entity.HasKey(e => e.UserId);
                entity.HasMany(e => e.OwnedBoards)
                    .WithOne(e => e.Owner);
                entity.HasMany(e => e.OwnedStories)
                    .WithOne(e => e.Owner);
                entity.HasMany(e => e.AssignedStories)
                    .WithMany(e => e.Assignees);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Board>(entity =>
            {
                entity.HasKey(e => e.BoardId);
                entity.HasOne(e => e.Owner)
                    .WithMany(e => e.OwnedBoards)
                    .HasForeignKey(e => e.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(e => e.Stories)
                    .WithOne(e => e.Board);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Story>(entity =>
            {
                entity.HasKey(e => e.StoryId);
                entity.HasOne(e => e.Owner)
                    .WithMany(e => e.OwnedStories)
                    .HasForeignKey(e => e.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Board)
                    .WithMany(e => e.Stories)
                    .HasForeignKey(e => e.BoardId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(e => e.Assignees)
                    .WithMany(e => e.AssignedStories);
                entity.HasOne(e => e.Status)
                    .WithMany(e => e.Stories)
                    .HasForeignKey(e => e.StatusId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.Property(e => e.Description).IsRequired(false);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.StatusId);
                entity.HasMany(e => e.Stories)
                    .WithOne(e => e.Status);
                entity.Property(e => e.Name).IsRequired();
            });
        }
    }
}
