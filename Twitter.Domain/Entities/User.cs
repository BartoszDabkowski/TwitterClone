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
            Following = new List<UserFollowers>();
            Followers = new List<UserFollowers>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }
        public List<Post> Posts { get; set; }
        public List<UserFollowers> Following { get; set; }
        public List<UserFollowers> Followers { get; set; }
    }
}
