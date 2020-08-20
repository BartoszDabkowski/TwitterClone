using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Twitter.Data;
using Twitter.Domain.Entities;
using Twitter.Domain.Repositories;

namespace Twitter.Api.Controllers
{
    [Route("api/users/{userId}/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        // GET: api/Users/{userId}/Posts
        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetPosts(int userId)
        {
            return Ok(_postRepository.GetPosts(userId));
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

            return Ok(post);
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
        public ActionResult<Post> AddPost(int userId, Post post)
        {
            _postRepository.AddPost(userId, post);
            _postRepository.Save();

            return CreatedAtAction("GetPost", new { id = post.Id }, post);
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
