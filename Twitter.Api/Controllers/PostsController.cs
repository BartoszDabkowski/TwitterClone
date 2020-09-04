using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MoreLinq.Extensions;
using Twitter.Api.Helpers;
using Twitter.Api.Models.Posts;
using Twitter.Api.Models.Shared;
using Twitter.Domain.Entities;
using Twitter.Domain.Repositories;

namespace Twitter.Api.Controllers
{
    [Route("api/users/{userId}")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostsController(
            IPostRepository postRepository,
            IMapper mapper)
        {
            _postRepository = postRepository ?? throw new ArgumentNullException(nameof(_postRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/Users/{userId}/Posts
        [HttpGet("posts")]
        public ActionResult<PostCollectionDto> GetPosts(int userId)
        {
            var postsFromRepo = _postRepository.GetPosts(userId);

            var links = CreateLinksForPosts(postsFromRepo
                .Select(x => x.UserId)
                .Distinct());

            var posts = _mapper
                .Map<IEnumerable<Post>, IEnumerable<PostDto>>(postsFromRepo)
                .ShapeData();

            var linkedPostsToReturn = posts.Select(post =>
            {
                var postAsDictionary = post as IDictionary<string, object>;
                var postLinks = CreateLinksForPost(userId, (int)postAsDictionary["Id"]);
                postAsDictionary.Add("links", postLinks);
                return postAsDictionary;
            });

            var linkedCollectionResource = new
            {
                value = linkedPostsToReturn,
                links
            };

            return Ok(linkedCollectionResource);
        }

        // GET: api/Users/{userId}/Friends/-/Posts
        [HttpGet("friends/-/posts")]
        public ActionResult<PostCollectionDto> GetFriendsPosts(int userId)
        {
            var postsFromRepo = _postRepository.GetFriendsPosts(userId);

            var links = CreateLinksForPosts(postsFromRepo
                .Select(x => x.UserId)
                .Distinct());

            var posts = _mapper
                .Map<IEnumerable<Post>, IEnumerable<PostDto>>(postsFromRepo)
                .ShapeData();

            var linkedPostsToReturn = posts.Select(post =>
            {
                var postAsDictionary = post as IDictionary<string, object>;
                var postLinks = CreateLinksForPost(userId, (int)postAsDictionary["Id"]);
                postAsDictionary.Add("links", postLinks);
                return postAsDictionary;
            });

            var linkedCollectionResource = new
            {
                value = linkedPostsToReturn,
                links
            };

            return Ok(linkedCollectionResource);
        }

        // GET: api/Users/{userId}/Posts/{postId}
        [HttpGet("posts/{postId}", Name ="GetPost")]
        public ActionResult<PostDto> GetPost(int userId, int postId)
        {
            var post = _postRepository.GetAllPost(userId, postId);

            if (post is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<Post>, PostCollectionDto>(post));
        }

        // GET: api/Users/{userId}/Posts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("posts", Name = "AddPost")]
        public ActionResult AddPost(int userId, PostForCreationDto postDto)
        {
            var post = _mapper.Map<PostForCreationDto, Post>(postDto);

            _postRepository.AddPost(userId, post);
            _postRepository.Save();

            var postToReturn = _mapper.Map<Post, PostDto>(post);

            return CreatedAtAction("GetPost", new { userId, postId = post.Id }, postToReturn);
        }

        // GET: api/Users/{userId}/Posts/{postId}
        [HttpDelete("posts/{postId}")]
        public ActionResult DeletePost(int userId, int postId)
        {
            var post = _postRepository.GetPost(userId, postId);

            if (post == null)
            {
                return NotFound();
            }

            _postRepository.DeletePost(post);
            _postRepository.Save();

            return NoContent();
        }

        private IEnumerable<LinkDto> CreateLinksForPost(int userId, int postId)
        {
            var links = new List<LinkDto>();

            links.Add(
                new LinkDto(Url.Link("GetPost", new {userId, postId }),
                    "self",
                    "GET"));

            return links;
        }

        private IEnumerable<LinkDto> CreateLinksForPosts(IEnumerable<int> userId)
        {
            var links = new List<LinkDto>();

            links.Add(
                new LinkDto(Url.Link("GetUsers", new { userId }),
                    "users",
                    "GET"));

            return links;
        }

    }
}
