using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Project.DTOs;
using Project.Models;

namespace Project.Profile
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            this.CreateMap<User, UserUDTO>()
      .ReverseMap();
        }
    }
}
