using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Twitter.Api.Models.Users;
using Twitter.Domain.Entities;
using Twitter.Domain.Repositories;

namespace Twitter.Api.Controllers
{
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class FollowersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public FollowersController(
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(_userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/users/{userId}/followers
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetFollowers(int userId)
        {
            var followers = _userRepository.GetFollowers(userId);

            return Ok(_mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(followers));
        }
    }
}
