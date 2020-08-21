﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Twitter.Api.Models.Users;
using Twitter.Domain.Entities;

namespace Twitter.Api.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<UserDto, User>();

        }
    }
}
