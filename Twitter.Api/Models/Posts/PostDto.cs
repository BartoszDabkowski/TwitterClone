using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twitter.Api.Models.Users;

namespace Twitter.Api.Models.Posts
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? InReplyToPostId { get; set; }
        public string InReplyToUserName { get; set; }
        public int FavoriteCount { get; set; }
        public int ResquawkCount { get; set; }
        public int ReplyCount { get; set; }
        public string UserName { get; set; }
        public List<UserDto> UserMentions { get; set; }
    }
}
