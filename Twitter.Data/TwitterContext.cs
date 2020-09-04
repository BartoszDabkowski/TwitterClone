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
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Friendships> Friendships { get; set; }

        public TwitterContext(DbContextOptions<TwitterContext> options)
        :base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(x => x.Reposts)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasIndex(i => i.UserName)
                .IsUnique();

            modelBuilder.Entity<Post>()
                .HasOne(x => x.InReplyToPost)
                .WithMany(x => x.Replies)
                .HasForeignKey(x => x.InReplyToPostId);

            modelBuilder.Entity<Favorite>()
                .HasKey(k => new { k.UserId, PostId = k.PostId });

            modelBuilder.Entity<Favorite>()
                .HasOne(l => l.User)
                .WithMany(a => a.Favorites)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Favorite>()
                .HasOne(l => l.Post)
                .WithMany(a => a.Favorites)
                .HasForeignKey(l => l.PostId);

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
