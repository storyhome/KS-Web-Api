﻿using KS.Business.DataContract.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KS.Database.DataContract.Authorization
{
    public interface IExistingUserCommand
    {
        Task<ReceivedExistingDTO> Execute(LoginRAO userRAO);
    }
}

