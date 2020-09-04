using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twitter.Api.Models.Users;
using Twitter.Domain.Entities;

namespace Twitter.Api.Models.Posts
{
    public class PostCollectionDto
    {
        public IEnumerable<PostDto> Posts { get; set; }
        public IEnumerable<UserDto> Users { get; set; }
    }
}
