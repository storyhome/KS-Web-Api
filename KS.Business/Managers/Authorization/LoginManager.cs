
using AutoMapper;
using KS.Business.DataContract.Authorization;
using KS.Business.Engines.Authorization;
using KS.Database.Authorization.Invokers;
using KS.Database.DataContract.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace KS.Business.Managers.Authorization
{
    //TODO: 2 Implement generate for token user method
    //TODO: 3 Add IConfiguration parameter to ctor and create read-only field
    //TODO: 4 Create GenerateTokenEngine (see slack)
    //TODO: 5 Inside GenerateToken - create new instance generatetokeninstance
    //TODO: 6 call GTFU method return token string.
    public class LoginManager : ILoginManager
    {
        private readonly IExistingUserInvoker _existingUserInvoker;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public LoginManager(IExistingUserInvoker existingUserInvoker, IConfiguration configuration, IMapper mapper)
        {

            _existingUserInvoker = existingUserInvoker;
            _configuration = configuration;
            _mapper = mapper;
        }

        public string GenerateTokenForUser(ReceivedExistingDTO receivedExistinguser)
        {
            var tokenEngine = new GenerateTokenEngine(_configuration);
            var tokenString = tokenEngine.GenerateTokenString(receivedExistinguser);
            return tokenString;
        }

        public async Task<ReceivedExistingDTO> LoginUser(GetLoginUserDTO userDTO)
        {
            var rao = PrepareUserRAOForLogin(userDTO);
            var receivedUser = await _existingUserInvoker.InvokeLoginUserCommand(rao);
            var verifyPasswordHashEngine = new VerifyPasswordHashEngine();

            if (verifyPasswordHashEngine.VerifyPasswordHash(userDTO.Password, receivedUser.PasswordHash, receivedUser.PasswordSalt))
            {
                var receivedUserDTO = _mapper.Map<ReceivedExistingDTO>(receivedUser);
                return receivedUserDTO;
            }

            return null;
        }

        private LoginRAO PrepareUserRAOForLogin(GetLoginUserDTO userDTO)
        {
            byte[] passwordHash, passwordSalt;
            var hashEngine = new CreatePasswordHashEngine();
            hashEngine.CreatePasswordHash(userDTO.Password, out passwordHash, out passwordSalt);
            var rao = _mapper.Map<LoginRAO>(userDTO);
            rao.PasswordHash = passwordHash;
            rao.PasswordSalt = passwordSalt;
            return rao;
        }

       
    }
}

