using AutoMapper;
using interview_prep.Dto.UserDTO;
using interview_prep.Models;
using interview_prep.Models.user;

namespace interview_prep.Helper;

public class ProfileMapper : Profile
{
    public ProfileMapper()
    {
        CreateMap<User, LoginResponseDto>();
    }
}