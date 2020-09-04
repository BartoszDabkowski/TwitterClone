using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Twitter.Domain;
using Twitter.Domain.Entities;
using Twitter.Domain.Repositories;

namespace Twitter.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly TwitterContext _context;

        public PostRepository(TwitterContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Post> GetPosts(int userId)
        {
            return _context.Posts
                .Include(x => x.User)
                .Include(x => x.Favorites)
                .Include(x => x.Reposts)
                .Include(x => x.Replies)
                .AsNoTracking()
                .Where(x => x.UserId == userId);
        }

        public IEnumerable<Post> GetFriendsPosts(int userId)
        {
            var usersFriends = _context.Users.Join(_context.Friendships,
                    user => user.Id,
                    friendships => friendships.UserId,
                    (user, friendships) => new
                    {
                        User = user,
                        Friendships = friendships
                    })
                .Where(x => x.Friendships.UserId == userId);

            return _context.Posts.Join(usersFriends,
                post => post.UserId,
                usersFriend => usersFriend.Friendships.FriendId,
                (posts, usersFriend) => posts)
                .Include(x => x.User)
                .Include(x => x.Favorites)
                .Include(x => x.Reposts)
                .Include(x => x.Replies);
        }

        public IEnumerable<Post> GetAllPost(int userId, int postId)
        {
            var post =  _context.Posts
                .Include(x => x.User)
                .Include(x => x.Favorites)
                .Include(x => x.Reposts)
                .Include(x => x.Replies)
                .Include(x => x.InReplyToPost).ThenInclude(x => x.User)
                .Include(x => x.InReplyToPost).ThenInclude(x => x.InReplyToPost)
                .SingleOrDefault(x => x.UserId == userId && x.Id == postId);

            var posts = new List<Post>{post};

            var p = post;
            while (p != null && p.InReplyToPostId.HasValue)
            {
                var p2 = _context.Posts
                    .Include(x => x.User)
                    .Include(x => x.Favorites)
                    .Include(x => x.Reposts)
                    .Include(x => x.Replies)
                    .Include(x => x.InReplyToPost).ThenInclude(x => x.User)
                    .Include(x => x.InReplyToPost).ThenInclude(x => x.InReplyToPost)
                    .SingleOrDefault(x => x.Id == p.InReplyToPostId);

                posts.Add(p2);
                p = p2;
            }

            return posts;
        }

        public Post GetPost(int userId, int postId)
        {
            return _context.Posts
                .Include(x => x.User)
                .Include(x => x.Favorites)
                .Include(x => x.Reposts)
                .Include(x => x.Replies)
                .FirstOrDefault(x => x.UserId == userId && x.Id == postId);
        }

        public void AddPost(int userId, Post post)
        {
            if (post is null)
                throw new ArgumentNullException(nameof(post));
            
            post.UserId = userId;
            _context.Posts.Add(post);
        }

        public void UpdatePost(Post post)
        {
            if (post is null)
                throw new ArgumentNullException(nameof(post));

            _context.Update(post);
        }

        public void DeletePost(Post post)
        {
            if (post is null)
                throw new ArgumentNullException(nameof(post));

            _context.Remove(post);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
