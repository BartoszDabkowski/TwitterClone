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
        public ActionResult<IEnumerable<UserDto>> GetFriends(string userName)
        {
            var friends = _userRepository.GetFriends(userName);

            return Ok(_mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(friends));
        }

        // GET: api/users/{userId}/friends
        [HttpPost]
        public ActionResult AddFriend(string userName, [FromBody] string friendUserName)
        {
            _userRepository.AddFriend(userName, friendUserName);
            _userRepository.Save();

            return NoContent();
        }

        // GET: api/users/{userId}/friends
        [HttpDelete]
        public ActionResult RemoveFriend(string userName, [FromBody] string friendUserName)
        {
            _userRepository.RemoveFriend(userName, friendUserName);
            _userRepository.Save();

            return NoContent();
        }
    }
}
