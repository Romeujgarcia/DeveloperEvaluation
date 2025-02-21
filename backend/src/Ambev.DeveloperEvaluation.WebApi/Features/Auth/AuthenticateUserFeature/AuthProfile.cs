using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Auth
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            // Mapeamento de AuthenticateUser Request para AuthenticateUser Command
            CreateMap<AuthenticateUserRequest, AuthenticateUserCommand>();
            // Mapeamento de AuthenticateUser Result para AuthenticateUser Response
            CreateMap<AuthenticateUserResult, AuthenticateUserResponse>();
        }
        
    }
}