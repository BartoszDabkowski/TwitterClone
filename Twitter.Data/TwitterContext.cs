using Microsoft.EntityFrameworkCore;
using Twitter.Domain.Entities;
using Twitter.Domain.Joins;

namespace Twitter.Data
{
    public class TwitterContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        public TwitterContext(DbContextOptions<TwitterContext> options)
        :base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserFollowers>()
                .HasKey(k => new { k.UserId, k.FollowerId });

            modelBuilder.Entity<UserFollowers>()
                .HasOne(l => l.User)
                .WithMany(a => a.Followers)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserFollowers>()
                .HasOne(l => l.Follower)
                .WithMany(a => a.Following)
                .HasForeignKey(l => l.FollowerId);
        }
    }
}
