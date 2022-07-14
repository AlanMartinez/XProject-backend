using AutoMapper;
using XProject.API.Commands.Security;
using XProject.API.Queries.Auth;
using XProject.Core.DTOs;
using XProject.Core.Entities;

namespace XProject.API.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region Dtos
            CreateMap<Security, SecurityDto>().ReverseMap();
            #endregion
            #region Commands
          
            CreateMap<AuthorizationQuery, UserLogin>()
                .ForMember(dest =>
                    dest.UserName,
                    opt => opt.Ignore()
                 )
                .ForMember(dest =>
                    dest.Password,
                    opt => opt.Ignore()
                 );
            CreateMap<SecurityRegisterCommand, Security>()
                .ForMember(dest =>
                    dest.UserName,
                    opt => opt.Ignore()
                 )
                .ForMember(dest =>
                    dest.Password,
                    opt => opt.Ignore()
                 )
                .ForMember(dest =>
                    dest.Role,
                    opt => opt.Ignore()
                 )
                .ForMember(dest =>
                    dest.Id,
                    opt => opt.Ignore()
                 );
            #endregion
        }
    }
}
