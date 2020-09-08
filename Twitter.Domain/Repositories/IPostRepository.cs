using System;
using System.Collections.Generic;
using System.Text;
using Twitter.Domain.Entities;

namespace Twitter.Domain.Repositories
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetPosts(string userName);
        IEnumerable<Post> GetFriendsPosts(string userName);
        IEnumerable<Post> GetAllPost(string userName, int postId);
        Post GetPost(string userName, int postId);
        void AddPost(string userName, Post post);
        void UpdatePost(Post post);
        void DeletePost(Post post);
        bool Save();
    }
}
