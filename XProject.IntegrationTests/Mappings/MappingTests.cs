using AutoMapper;
using System;
using System.Runtime.Serialization;
using XProject.API.Commands.Security;
using XProject.API.Mappings;
using XProject.API.Queries.Auth;
using XProject.Core.DTOs;
using XProject.Core.Entities;
using Xunit;

namespace XProject.IntegrationTests.Mappings
{
    public class MappingTests : Profile
    {
        IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GeneralProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Theory]
        [InlineData(typeof(Security), typeof(SecurityDto))]
        [InlineData(typeof(AuthorizationQuery), typeof(UserLogin))]
        [InlineData(typeof(SecurityRegisterCommand), typeof(Security))]
        public void Map_SourceToDestination_ExistConfiguration(Type origin, Type destination)
        {
            var instance = FormatterServices.GetUninitializedObject(origin);

            _mapper.Map(instance, origin, destination);
        }
    }
}
