using AutoMapper;
using KS.Business.DataContract.Authorization;
using KS.Business.Engines.Authorization;
using KS.Database.Authorization.Invokers;
using KS.Database.DataContract.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KS.Business.Managers.Authorization
{
    public class LoginManager : ILoginManager
    {
        private readonly IExistingUserInvoker _existingUserInvoker;
        private readonly IMapper _mapper;

        public LoginManager(IExistingUserInvoker existingUserInvoker, IMapper mapper)
        {

            _existingUserInvoker = existingUserInvoker;
            _mapper = mapper;
        }
        public async Task<ReceivedExistingDTO> LoginUser(GetLoginUserDTO userDTO)
        {
            var rao = PrepareUserRAOForLogin(userDTO);
            var receivedUser =  await _existingUserInvoker.InvokeLoginUserCommand(rao);
            var verifyPasswordHashEngine = new VerifyPasswordHashEngine();

            if (verifyPasswordHashEngine.VerifyPasswordHash(userDTO.Password,receivedUser.PasswordHash,receivedUser.PasswordSalt))
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

