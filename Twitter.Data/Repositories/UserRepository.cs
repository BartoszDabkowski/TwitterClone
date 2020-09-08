using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twitter.Domain.Entities;
using Twitter.Domain.Joins;
using Twitter.Domain.Repositories;

namespace Twitter.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TwitterContext _context;

        public UserRepository(TwitterContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<User> GetFriends(string userName)
        {
            var userId = _context.Users.SingleOrDefault(x => x.UserName.Equals(userName))?.Id;

            return _context.Users.Join(_context.Friendships,
                    user => user.Id,
                    friendship => friendship.FriendId,
                    (user, friend) => new { user, friend })
                .Where(x => x.friend.UserId == userId)
                .Select(x => x.user);
        }

        public void AddFriend(string userName, string friendUserName)
        {
            var userId = _context.Users.SingleOrDefault(x => x.UserName.Equals(userName))?.Id;
            var friendUserId = _context.Users.SingleOrDefault(x => x.UserName.Equals(friendUserName))?.Id;

            if (userId is null || friendUserId is null)
                throw new Exception();

            _context.Friendships.Add(new Friendships{UserId = userId.Value, FriendId = friendUserId.Value });
        }

        public void RemoveFriend(string userName, string friendUserName)
        {
            var userId = _context.Users.SingleOrDefault(x => x.UserName.Equals(userName))?.Id;
            var friendUserId = _context.Users.SingleOrDefault(x => x.UserName.Equals(friendUserName))?.Id;

            if(userId is null || friendUserId is null)
                throw new Exception();

            _context.Friendships.Remove(new Friendships { UserId = userId.Value, FriendId = friendUserId.Value });
        }

        public IEnumerable<User> GetFollowers(string userName)
        {
            var userId = _context.Users.SingleOrDefault(x => x.UserName.Equals(userName))?.Id;

            return _context.Users.Join(_context.Friendships,
                            user => user.Id,
                            friendship => friendship.UserId,
                            (user, friend) => new { user, friend })
                        .Where(x => x.friend.FriendId == userId)
                        .Select(x => x.user);
        }

        public IEnumerable<User> GetUsers(IEnumerable<string> userNames = null)
        {
            if (userNames is null)
                return _context.Users;

            return _context.Users.Where(x => userNames.Contains(x.UserName));
        }

        public User GetUser(string userName)
        {
            return _context.Users
                .FirstOrDefault(x => x.UserName == userName);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
