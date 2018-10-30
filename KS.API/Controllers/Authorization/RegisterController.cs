﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KS.API.DataContract.Authorizaton;
using KS.Business.DataContract.Authorization;
using KS.Business.Managers.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KS.API.Controllers.Authorization
{
    [Route("api/[controller]")]
    [ApiControllerAttribute]

    public class RegisterController : Controller
        
    {
        private readonly IRegisterUserManager  _registerUserManager;
        private readonly IMapper _mapper;

        public RegisterController(IRegisterUserManager registerUserManager, IMapper mapper)
        {
            _registerUserManager = registerUserManager;
            _mapper = mapper;
        }

        [HttpPost("RegisterUser")]
       public async Task<IActionResult> Register([FromBody] NewUserCreateRequest userForRegister)
        {
            userForRegister.Username = userForRegister.Username.ToLower();
            var dto = _mapper.Map<NewUserCreateDTO>(userForRegister);
            await _registerUserManager.RegisterUser(dto);
            return StatusCode(201);
        }

    }
}
