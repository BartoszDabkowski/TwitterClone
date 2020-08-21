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
    public class FriendsController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public FriendsController(
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(_userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/users/{userId}/friends
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetFriends(int userId)
        {
            var friends = _userRepository.GetFriends(userId);

            return Ok(_mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(friends));
        }

        // GET: api/users/{userId}/friends
        [HttpPost]
        public ActionResult AddFriend(int userId, [FromBody] int friendUserId)
        {
            _userRepository.AddFriend(userId, friendUserId);
            _userRepository.Save();

            return NoContent();
        }

        // GET: api/users/{userId}/friends
        [HttpDelete]
        public ActionResult RemoveFriend(int userId, [FromBody] int friendUserId)
        {
            _userRepository.RemoveFriend(userId, friendUserId);
            _userRepository.Save();

            return NoContent();
        }
    }
}
