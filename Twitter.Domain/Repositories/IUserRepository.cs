using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Twitter.Domain.Entities;

namespace Twitter.Domain.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetFriends(int userId);
        void AddFriend(int userId, int friendUserId);
        void RemoveFriend(int userId, int friendUserId);
        IEnumerable<User> GetFollowers(int userId);
        IEnumerable<User> GetUsers(IEnumerable<int> userId = null);
        User GetUser(int userId);
        void Save();
    }
}
