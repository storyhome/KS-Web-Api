using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KS.Database.DataContract.Authorization
{
   public interface IExistingUserCommand
    {
        Task<bool> Execute(LoginRAO userRAO);
    }
}
