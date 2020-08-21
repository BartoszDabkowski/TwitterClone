using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Twitter.Api.Models.Posts;
using Twitter.Domain.Entities;
using Twitter.Domain.Repositories;

namespace Twitter.Api.Controllers
{
    [Route("api/users/{userId}/posts")]
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
        [HttpGet]
        public ActionResult<IEnumerable<PostDto>> GetPosts(int userId)
        {
            var posts = _postRepository.GetPosts(userId);

            return Ok(_mapper.Map<IEnumerable<Post>, IEnumerable<PostDto>>(posts));
        }

        // GET: api/Users/{userId}/Posts
        [HttpGet]
        public ActionResult<IEnumerable<PostDto>> GetFollowingPosts(int userId)
        {
            var posts = _postRepository.GetPosts(userId);

            return Ok(_mapper.Map<IEnumerable<Post>, IEnumerable<PostDto>>(posts));
        }

        // GET: api/Users/{userId}/Posts/{postId}
        [HttpGet("{postId}", Name ="GetPost")]
        public ActionResult<Post> GetPost(int userId, int postId)
        {
            var post = _postRepository.GetPost(userId, postId);

            if (post is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Post, PostDto>(post));
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{postId}", Name = "UpdatePost")]
        //public async Task<IActionResult> PutPost(int postId, Post post)
        //{
        //    if (postId != post.Id)
        //    {
        //        return BadRequest();
        //    }

        //    //_context.Entry(post).State = EntityState.Modified;

        //    try
        //    {
        //        //await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PostExists(postId))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // GET: api/Users/{userId}/Posts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost(Name = "AddPost")]
        public ActionResult<Post> AddPost(int userId, PostForCreationDto postDto)
        {
            var post = _mapper.Map<PostForCreationDto, Post>(postDto);

            _postRepository.AddPost(userId, post);
            _postRepository.Save();

            var postToReturn = _mapper.Map<Post, PostDto>(post);

            return CreatedAtAction("GetPost", new { userId, postId = post.Id }, postToReturn);
        }

        // GET: api/Users/{userId}/Posts/{postId}
        [HttpDelete("{postId}")]
        public ActionResult<Post> DeletePost(int userId, int postId)
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
    }
}
