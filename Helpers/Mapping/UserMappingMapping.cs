using AutoMapper;
using DTOs;
using Models;

namespace Helpers.Mapping
{
    public class UserMapping:Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserModel>().ReverseMap();
        }
    }
}
