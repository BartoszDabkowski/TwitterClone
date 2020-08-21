using Microsoft.EntityFrameworkCore;
using Twitter.Domain;
using Twitter.Domain.Entities;
using Twitter.Domain.Joins;

namespace Twitter.Data
{
    public class TwitterContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Friendships> Friendships { get; set; }

        public TwitterContext(DbContextOptions<TwitterContext> options)
        :base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(i => i.UserName)
                .IsUnique();

            modelBuilder.Entity<Friendships>()
                .HasKey(k => new { k.UserId, FollowerId = k.FriendId });

            modelBuilder.Entity<Friendships>()
                .HasOne(l => l.User)
                .WithMany(a => a.Followers)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friendships>()
                .HasOne(l => l.Friend)
                .WithMany(a => a.Friends)
                .HasForeignKey(l => l.FriendId);
        }
    }
}
