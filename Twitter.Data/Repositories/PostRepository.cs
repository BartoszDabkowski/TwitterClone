using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                .Where(x => x.UserId == userId);
        }

        public IEnumerable<Post> GetFriendsPosts(int userId)
        {
            return _context.Posts.Join(_context.Friendships,
                post => post.Id,
                user => user.FriendId,
                (post, user) => new
                {
                    Post = post,
                    User = user
                })
                .Where(x => x.User.FriendId == userId)
                .Select(x => x.Post);
        }

        public Post GetPost(int userId, int postId)
        {
            return _context.Posts
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
