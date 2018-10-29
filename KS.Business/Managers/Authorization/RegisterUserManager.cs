using KS.API.DataContract.Authorizaton;
using KS.Business.DataContract.Authorization;
using KS.Business.Engines.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KS.Business.Managers.Authorization
{
    public class RegisterUserManager : IRegisterUserManager
    {
        public Task<NewUserCreateDTO> RegisterUser(NewUserCreateRequest userRequest)
        {
            NewUserCreateDTO dto = PrepareUserDTOForRegister(userRequest);

            // -- Create an instance of the Engine
            // -- Call CreatePasswordHash Method
            // -- Pass variable into password hash
            // -- Prepare the DTO object for the next layer
            // -- Instantiate the class for the Database
            // -- Call the Invoker method in the DAL

            throw new Exception();
        }

        private NewUserCreateDTO PrepareUserDTOForRegister(NewUserCreateRequest userRequest)
        {
            byte[] passwordHash, passwordSalt;
            var hashEngine = new CreatePasswordHashEngine();
            hashEngine.CreatePasswordHash(userRequest.Password, out passwordHash, out passwordSalt);
            var userDTO = MapUserRequestObjectToDTO( userRequest, passwordHash, passwordSalt);

            return userDTO;
        }

        private NewUserCreateDTO MapUserRequestObjectToDTO(NewUserCreateRequest userRequest, byte[] passwordHash, byte[] passwordSalt)
        {
            var userDTO =
                new NewUserCreateDTO
                {
                    Username = userRequest.Username,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                return userDTO;
        }
    }
}
