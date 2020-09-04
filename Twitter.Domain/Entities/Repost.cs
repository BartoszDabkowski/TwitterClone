using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Twitter.Domain.Entities
{
    public class Repost
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; }
        public int PostId { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
