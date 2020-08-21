using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Twitter.Api.Models.Posts;
using Twitter.Domain.Entities;

namespace Twitter.Api.Profiles
{
    public class PostsProfile : Profile
    {
        public PostsProfile()
        {
            CreateMap<Post, PostDto>();

            CreateMap<PostDto, Post>();
            CreateMap<PostForCreationDto, Post>();
        }
    }
}
