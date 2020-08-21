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

        public IEnumerable<User> GetFriends(int userId)
        {
            return _context.Users.Join(_context.Friendships,
                    user => user.Id,
                    friendship => friendship.FriendId,
                    (user, friend) => new { user, friend })
                .Where(x => x.friend.UserId == userId)
                .Select(x => x.user);
        }

        public void AddFriend(int userId, int friendUserId)
        {
            _context.Friendships.Add(new Friendships{UserId = userId, FriendId = friendUserId });
        }

        public void RemoveFriend(int userId, int friendUserId)
        {
            _context.Friendships.Remove(new Friendships { UserId = userId, FriendId = friendUserId });
        }

        public IEnumerable<User> GetFollowers(int userId)
        {
            return _context.Users.Join(_context.Friendships,
                    user => user.Id,
                    friendship => friendship.UserId,
                    (user, friend) => new { user, friend })
                .Where(x => x.friend.FriendId == userId)
                .Select(x => x.user);
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users;
        }

        public User GetUser(int userId)
        {
            return _context.Users
                .FirstOrDefault(x => x.Id == userId);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
