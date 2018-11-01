using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KS.API.DataContract.Authorization;
using KS.Business.DataContract.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//TODO: 8 Call GTFU and capture the token string
//TODO: 9 Change return from status code to OK and return (token string, receive user)
namespace KS.API.Controllers.Authorization
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginManager _loginManager;
        private readonly IMapper _mapper;

        public LoginController(ILoginManager loginManager, IMapper mapper)
        {
            _loginManager = loginManager;
            _mapper = mapper;
        }

        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequest loginRequest)
        {
            
            loginRequest.Username = loginRequest.Username.ToLower();
            var dto = _mapper.Map<GetLoginUserDTO>(loginRequest);
            var receivedExistinguser = await _loginManager.LoginUser(dto);
            string tokenString = _loginManager.GenerateTokenForUser(receivedExistinguser);
            return Ok(new { tokenString, receivedExistinguser });
        }   
        

    }
}