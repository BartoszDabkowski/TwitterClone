using System;
using System.Collections.Generic;
using System.Text;
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
            throw new NotImplementedException();
        }

        public Post GetPost(int userId, int postId)
        {
            throw new NotImplementedException();
        }

        public void AddPost(int userId, int postId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePost(Post post)
        {
            throw new NotImplementedException();
        }

        public void DeletePost(Post post)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }
    }
}
