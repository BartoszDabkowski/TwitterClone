using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;
using Twitter.Domain.Entities;

namespace Twitter.Domain.Entities
{
    public class Post
    {
        public Post()
        {
            Ratings = new List<Rating>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(280)]
        public string Text { get; set; }
        public List<Rating> Ratings { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
