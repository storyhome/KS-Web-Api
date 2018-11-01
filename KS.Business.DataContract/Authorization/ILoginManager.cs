using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KS.Business.DataContract.Authorization
{
    public interface ILoginManager
//TODO: 1 generate token for user method (received DTO) Return string

    {
        Task<ReceivedExistingDTO> LoginUser(GetLoginUserDTO userDTO);
        string GenerateTokenForUser(ReceivedExistingDTO receivedExistinguser);
    }

}


