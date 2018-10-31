using AutoMapper;
using KS.Database.Contexts;
using KS.Database.DataContract.Authorization;
using KS.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KS.Database.Authorization.Receivers
{
    public class ExistingUserReceiver : IExistingUserReceiver
    {

        private readonly KSContext _context;
        private readonly IMapper _mapper;

        public async Task<bool> LoginUser(LoginRAO userRAO)
        {
            var userEntity = _mapper.Map<UserEntity>(userRAO);
             return await GetUserByUserName(userEntity);
        }
        public async Task<bool> GetUserByUserName(UserEntity userEntity)
        {
            await _context.UserTableAccess.AddAsync(userEntity);
            return await _context.SaveChangesAsync() == 1;

        }

       
    }
}
