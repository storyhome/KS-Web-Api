using KS.Database.DataContract.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KS.Database.Authorization.Invokers
{
    public class LoginUserInvoker : IExistingUserInvoker
    {
        private readonly IExistingUserCommand _command;

        public LoginUserInvoker(IExistingUserCommand command)
        {
            _command = command;
        }
        public async Task<bool> InvokeLoginUserCommand(LoginRAO userRAO)
        {
            return await _command.Execute(userRAO);
        }
    }
}
