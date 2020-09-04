using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Twitter.Domain.Joins;

namespace Twitter.Domain.Entities
{
    public class User
    {
        public User()
        {
            Posts = new List<Post>();
            Reposts = new List<Repost>();
            Friends = new List<Friendships>();
            Followers = new List<Friendships>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public List<Post> Posts { get; set; }
        public List<Repost> Reposts { get; set; }
        public List<Friendships> Friends { get; set; }
        public List<Friendships> Followers { get; set; }
        public List<Favorite> Favorites { get; set; }
    }
}
