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
    [Route("api/users/{userName}")]
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

        // GET: api/Users/{userName}/Posts
        [HttpGet("posts")]
        public ActionResult<PostDto> GetPosts(string userName)
        {
            var postsFromRepo = _postRepository.GetPosts(userName);

            var links = CreateLinksForPosts(postsFromRepo
                .Select(x => x.User.UserName)
                .Distinct());

            var posts = _mapper
                .Map<IEnumerable<Post>, IEnumerable<PostDto>>(postsFromRepo)
                .ShapeData();

            var linkedPostsToReturn = posts.Select(post =>
            {
                var postAsDictionary = post as IDictionary<string, object>;
                var postLinks = CreateLinksForPost(userName, (int)postAsDictionary["Id"]);
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

        // GET: api/Users/{userName}/Friends/-/Posts
        [HttpGet("friends/-/posts")]
        public ActionResult<PostDto> GetFriendsPosts(string userName)
        {
            var postsFromRepo = _postRepository.GetFriendsPosts(userName);

            var links = CreateLinksForPosts(postsFromRepo
                .Select(x => x.User.UserName)
                .Distinct());

            var posts = _mapper
                .Map<IEnumerable<Post>, IEnumerable<PostDto>>(postsFromRepo)
                .ShapeData();

            var linkedPostsToReturn = posts.Select(post =>
            {
                var postAsDictionary = post as IDictionary<string, object>;
                var postLinks = CreateLinksForPost(userName, (int)postAsDictionary["Id"]);
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

        // GET: api/Users/{userName}/Posts/{postId}
        [HttpGet("posts/{postId}", Name ="GetPost")]
        public ActionResult<IEnumerable<PostDto>> GetPost(string userName, int postId)
        {
            var postsFromRepo = _postRepository.GetAllPost(userName, postId);

            if (postsFromRepo is null)
            {
                return NotFound();
            }

            var links = CreateLinksForPosts(postsFromRepo
                .Select(x => x.User.UserName)
                .Distinct());

            var posts = _mapper
                .Map<IEnumerable<Post>, IEnumerable<PostDto>>(postsFromRepo)
                .ShapeData();

            var linkedPostsToReturn = posts.Select(post =>
            {
                var postAsDictionary = post as IDictionary<string, object>;
                var postLinks = CreateLinksForPost(userName, (int)postAsDictionary["Id"]);
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

        // GET: api/Users/{userName}/Posts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("posts", Name = "AddPost")]
        public ActionResult AddPost(string userName, PostForCreationDto postDto)
        {
            var post = _mapper.Map<PostForCreationDto, Post>(postDto);

            _postRepository.AddPost(userName, post);
            _postRepository.Save();

            var postToReturn = _mapper.Map<Post, PostDto>(post);

            return CreatedAtAction("GetPost", new { userName, postId = post.Id }, postToReturn);
        }

        // GET: api/Users/{userName}/Posts/{postId}
        [HttpDelete("posts/{postId}")]
        public ActionResult DeletePost(string userName, int postId)
        {
            var post = _postRepository.GetPost(userName, postId);

            if (post == null)
            {
                return NotFound();
            }

            _postRepository.DeletePost(post);
            _postRepository.Save();

            return NoContent();
        }

        private IEnumerable<LinkDto> CreateLinksForPost(string userName, int postId)
        {
            var links = new List<LinkDto>();

            links.Add(
                new LinkDto(Url.Link("GetPost", new { userName = userName, postId }),
                    "self",
                    "GET"));

            return links;
        }

        private IEnumerable<LinkDto> CreateLinksForPosts(IEnumerable<string> userName)
        {
            var links = new List<LinkDto>();

            links.Add(
                new LinkDto(Url.Link("GetUsers", new { userName }),
                    "users",
                    "GET"));

            return links;
        }

    }
}
