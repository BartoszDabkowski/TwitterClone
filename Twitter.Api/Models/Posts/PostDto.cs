using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitter.Api.Models.Posts
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public double Rating { get; set; }
        public int UserId { get; set; }
    }
}
