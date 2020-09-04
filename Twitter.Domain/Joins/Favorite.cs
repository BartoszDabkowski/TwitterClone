using System;
using System.ComponentModel.DataAnnotations;
using Twitter.Domain.Entities;

namespace Twitter.Domain.Joins
{
    public class Favorite
    {
        [Key]
        public int UserId { get; set; }
        [Key]
        public int PostId { get; set; }

        public User User { get; set; }

        public Post Post { get; set; }

        public DateTime CreatedAt { get; set; }


    }
}
