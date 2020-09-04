using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Twitter.Api.Helpers;
using Twitter.Api.Models.Shared;
using Twitter.Api.Models.Users;
using Twitter.Data;
using Twitter.Domain.Entities;
using Twitter.Domain.Repositories;

namespace Twitter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(_userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/Users
        [HttpGet(Name = "GetUsers")]
        public ActionResult<IEnumerable<User>> GetUsers([FromQuery] int[] userId = null)
        {
            var usersFromRepo = _userRepository.GetUsers(userId);

            var users = _mapper
                .Map<IEnumerable<User>, IEnumerable<UserDto>>(usersFromRepo)
                .ShapeData();

            var linkedUsersToReturn = users.Select(user =>
            {
                var userAsDictionary = user as IDictionary<string, object>;
                var userLinks = CreateLinksForUser((int)userAsDictionary["Id"]);
                userAsDictionary.Add("links", userLinks);
                return userAsDictionary;
            });

            return Ok(linkedUsersToReturn);
        }

        // GET: api/Users/5
        [HttpGet("{userId}", Name = "GetUser")]
        public ActionResult<User> GetUser(int userId)
        {
            var userFromRepo = _userRepository.GetUser(userId);

            if (userFromRepo is null)
                return NotFound();

            var user = _mapper
                .Map<User, UserDto>(userFromRepo)
                .ShapeData()
                as IDictionary<string, object>;

            var userLinks = CreateLinksForUser(userFromRepo.Id);
            user.Add("links", userLinks);

            return Ok(user);
        }

        private IEnumerable<LinkDto> CreateLinksForUser(int userId)
        {
            var links = new List<LinkDto>();

            links.Add(
                new LinkDto(Url.Link("GetUser", new { userId }),
                    "self",
                    "GET"));

            links.Add(
                new LinkDto($"https://cdn.vuetifyjs.com/images/lists/{new Random().Next(1, 5)}.jpg",
                    "image",
                    "GET"));

            return links;
        }
    }
}
