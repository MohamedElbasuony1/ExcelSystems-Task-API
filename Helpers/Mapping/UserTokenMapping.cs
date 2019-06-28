using AutoMapper;
using DTOs;
using Models;

namespace Helpers.Mapping
{
    public class UserTokenMapping:Profile
    {
        public UserTokenMapping()
        {
            CreateMap<User, UserTokenModel>().ReverseMap();
        }
    }
}
