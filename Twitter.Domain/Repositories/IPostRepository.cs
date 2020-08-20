using System;
using System.Collections.Generic;
using System.Text;
using Twitter.Domain.Entities;

namespace Twitter.Domain.Repositories
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetPosts(int userId);
        Post GetPost(int userId, int postId);
        void AddPost(int userId, Post post);
        void UpdatePost(Post post);
        void DeletePost(Post post);
        bool Save();
    }
}
