using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Twitter.Api.Models.Posts
{
    public class PostForCreationDto
    {
        [Required(ErrorMessage = "You should fill out a title.")]
        [MaxLength(280, ErrorMessage = "The {0} shouldn't have more than {1} characters.")]
        public string Text { get; set; }

        public int InReplyToPostId { get; set; }
    }
}
