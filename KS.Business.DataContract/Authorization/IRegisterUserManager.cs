using KS.API.DataContract.Authorizaton;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KS.Business.DataContract.Authorization
{
    public interface IRegisterUserManager
    {
        Task<NewUserCreateDTO> RegisterUser(NewUserCreateRequest userRequest);
    }
}
