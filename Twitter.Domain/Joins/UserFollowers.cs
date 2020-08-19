using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;
using Twitter.Domain.Entities;

namespace Twitter.Domain.Joins
{
    public class UserFollowers
    {
        [Key]
        public int UserId { get; set; }

        [Key]
        public int FollowerId { get; set; }

        public User User { get; set; }
        public User Follower { get; set; }

    }
}
