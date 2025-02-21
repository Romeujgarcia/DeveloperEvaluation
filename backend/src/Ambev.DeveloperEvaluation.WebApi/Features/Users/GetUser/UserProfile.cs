using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Users.GetUser ; // Namespace da classe GetUser  Result
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser ; // Namespace da classe GetUser  Response

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Mapeamento de GetUser  Result para GetUser  Response
            CreateMap<GetUserResult, GetUserResponse>();
        }
    }
}