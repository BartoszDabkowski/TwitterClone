using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;
using Twitter.Domain.Entities;
using Twitter.Domain.Joins;

namespace Twitter.Domain.Entities
{
    public class Post
    {
        public Post()
        {
            Favorites = new List<Favorite>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(280)]
        public string Text { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public List<Favorite> Favorites { get; set; }
        public List<Repost> Reposts { get; set; }
        public List<Post> Replies { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(InReplyToPostId))]
        public Post InReplyToPost { get; set; }
        public int? InReplyToPostId { get; set; }
    }
}
