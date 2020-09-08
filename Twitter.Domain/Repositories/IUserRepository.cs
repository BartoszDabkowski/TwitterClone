using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Twitter.Domain.Entities;

namespace Twitter.Domain.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetFriends(string userName);
        void AddFriend(string userName, string friendUserName);
        void RemoveFriend(string userName, string friendUserName);
        IEnumerable<User> GetFollowers(string userName);
        IEnumerable<User> GetUsers(IEnumerable<string> userName = null);
        User GetUser(string userName);
        void Save();
    }
}
