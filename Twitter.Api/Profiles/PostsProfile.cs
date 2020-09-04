using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MoreLinq;
using Twitter.Api.Models.Posts;
using Twitter.Domain.Entities;

namespace Twitter.Api.Profiles
{
    public class PostsProfile : Profile
    {
        public PostsProfile()
        {
            CreateMap<Post, PostDto>()
                .ForMember(dest => dest.FavoriteCount,
                    opt => opt.MapFrom(src => src.Favorites.Count))
                .ForMember(dest => dest.ResquawkCount, 
                    opt => opt.MapFrom(src => src.Reposts.Count))
                .ForMember(dest => dest.ReplyCount,
                    opt => opt.MapFrom(src => src.Replies.Count));

            CreateMap<IEnumerable<Post>, PostCollectionDto>()
                .ForMember(dest => dest.Users,
                    opt => opt.MapFrom(src => src
                        .Select(x => x.User)
                        .DistinctBy(x => x.Id)
                    ))
                .ForMember(dest => dest.Posts,
                    opt => opt.MapFrom(src => src));


            CreateMap<PostDto, Post>();
            CreateMap<PostForCreationDto, Post>();
        }
    }
}
