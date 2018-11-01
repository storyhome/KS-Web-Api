using AutoMapper;
using KS.API.DataContract.Authorization;
using KS.API.DataContract.Authorizaton;
using KS.Business.DataContract.Authorization;
using KS.Database.DataContract.Authorization;
using KS.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KS.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //-- USER REGISTRATION
            CreateMap<NewUserCreateRequest,NewUserCreateDTO>();
            CreateMap<NewUserCreateDTO, UserRegisterRAO>();
            CreateMap<UserRegisterRAO, UserEntity>();
            //-- USER 
            CreateMap<LoginRequest,GetLoginUserDTO>();
            CreateMap<GetLoginUserDTO, LoginRAO>();
            CreateMap<LoginRAO, UserEntity>();
            CreateMap<UserEntity, ReceivedExistingRAO>();
            CreateMap<ReceivedExistingRAO, ReceivedExistingDTO>();


        }
    }
}
